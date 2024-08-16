using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class PetInfo : MonoBehaviour {

	public static string petInfoPath = "";
	public long nextUpdate;

	// Use this for initialization
	void Start () {
		petInfoPath = (Application.persistentDataPath + Controller.slot + "/petinfo");
		nextUpdate = System.DateTime.Now.Ticks + (10000000*10);
		//UnlockPet ("0004");
	}
	
	// Update is called once per frame
	void Update () {
		if(System.DateTime.Now.Ticks >= nextUpdate){
			nextUpdate = System.DateTime.Now.Ticks + (10000000*10);
			UpdatePetInfo (10000000*10);
			AccountInfo.saveInfo ();
		}
		
	}
	public static void UnlockPet(string petID){
		if(!HasPet(petID)){
			StreamWriter file = new StreamWriter (petInfoPath,true);
			file.WriteLine (Security.Rot39("pet:"+petID+";fullness:86400;happiness:86400;fitness:86400;tickmet:"+System.DateTime.Now.Ticks+";costume1:false;costume2:false;costume3:false;costume4:false",395445));
			file.Close ();
			print (GetPetFullness("0002"));
		}
	}
	public static void UnlockCostume(string petID,int costume){
		string line = null;
		bool pet = false;
		List<string> newText = new List<string>();
		StreamReader theReader = new StreamReader(petInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),395445)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "pet") {
						if (attribute[1] == petID) {
							pet = true;
						} 
					}
				}
				if (pet) {
					line = line.Replace ("costume" + costume +":false", "costume"+costume+":true");
				}
				newText.Add(Security.Rot39(line,395445));
				pet = false;
			}
		}
		theReader.Close ();
		StreamWriter theWriter = new StreamWriter(petInfoPath);
		for(int i = 0;i < newText.Count; i++){
			theWriter.WriteLine (newText[i]);
		}
		theWriter.Close ();
	}
	public static string[] GetPets(){
		string line = null;
		int index = 0;
		StreamReader theReader = new StreamReader(petInfoPath, Encoding.Default);
		string[] pets = new string[1000];
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),395445)).Contains(":")) {
				pets [index] = line;
				index += 1;
			}
		}
		theReader.Close ();
		return pets;
	}
	public static bool HasPetCostume(string petId, int costume){
		string line = null;
		bool rightPet = false;
		bool rightCostume = false;
		if(costume == 0){
			return true;
		}
		StreamReader theReader = new StreamReader(petInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),395445)).Contains(":")) {
				if(line.Contains("costume"+costume+":true") && line.Contains("pet:"+petId)){
					theReader.Close ();
					return true;
				}
			}
		}
		theReader.Close ();
		return false;
	}
	public static bool HasPet(string petId){
		string line = null;
		bool rightPet = false;
		StreamReader theReader = new StreamReader(petInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),395445)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "pet") {
						if (attribute [1] == petId) {
							rightPet = true;
						};
					}
				}
			}
		}
		theReader.Close ();
		if(rightPet){
			return true;
		}else{
			return false;
		}
	}
	public static int GetPetFullness(string petId){
		string line = null;
		bool rightPet = false;
		int fullness = 0;
		StreamReader theReader = new StreamReader(petInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),395445)).Contains(":")) {
				print (line);
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "pet") {
						if (attribute [1] == petId) {
							rightPet = true;
						};
					}
					if (attribute [0] == "fullness") {
						fullness = int.Parse(attribute [1]);
					}
				}
				if(rightPet){
					theReader.Close ();
					return fullness;
				}
				rightPet = false;
				fullness = 0;
			}
		}
		theReader.Close ();
		return 0;
	}
	public static int GetPetFitness(string petId){
		string line = null;
		bool rightPet = false;
		int fitness = 0;
		StreamReader theReader = new StreamReader(petInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),395445)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "pet") {
						if (attribute [1] == petId) {
							rightPet = true;
						};
					}
					if (attribute [0] == "fitness") {
						fitness = int.Parse(attribute [1]);
					}
				}
				if(rightPet){
					theReader.Close ();
					return fitness;
				}
				fitness = 0;
				rightPet = false;
			}
		}
		theReader.Close ();
		return 0;
	}
	public static int GetPetHappiness(string petId){
		string line = null;
		bool rightPet = false;
		int happiness = 0;
		StreamReader theReader = new StreamReader(petInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),395445)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "pet") {
						if (attribute [1] == petId) {
							rightPet = true;
						};
					}
					if (attribute [0] == "happiness") {
						happiness = int.Parse(attribute [1]);
					}
				}
				if(rightPet){
					theReader.Close ();
					return happiness;
				}
				happiness = 0;
				rightPet = false;
			}
		}
		theReader.Close ();
		return 0;
	}
	public static void FeedPet(string petID,int amount){
		string line = null;
		bool pet = false;
		int oldFullness = 0;
		List<string> newText = new List<string>();
		StreamReader theReader = new StreamReader(petInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),395445)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "pet") {
						if (attribute[1] == petID) {
							pet = true;
						} 
					}
					if(attribute[0] == "fullness"){
						oldFullness = int.Parse(attribute [1]);
					}
				}
				if (pet) {
					if ((oldFullness + amount) <= 86400) {
						line = line.Replace ("fullness:" + oldFullness, "fullness:" + (oldFullness + amount));
					} else {
						line = line.Replace ("fullness:" + oldFullness, "fullness:86400");
					}
				}
				newText.Add(Security.Rot39(line,395445));
				pet = false;
				oldFullness = 0;
			}
		}
		theReader.Close ();
		StreamWriter theWriter = new StreamWriter(petInfoPath);
		for(int i = 0;i < newText.Count; i++){
			theWriter.WriteLine (newText[i]);
		}
		theWriter.Close ();
	}
	public static bool IsPetHappy(string petId){
		string line = null;
		bool rightPet = false;
		int happiness = 0;
		StreamReader theReader = new StreamReader(petInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),395445)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "pet") {
						if (attribute [1] == petId) {
							rightPet = true;
						};
					}
					if (attribute [0] == "happiness") {
						happiness = int.Parse(attribute [1]);
					}
				}
				if(rightPet){
					theReader.Close ();
					if (happiness >= 43200) {
						return true;
					} else {
						return false;
					}
				}
				rightPet = false;
				happiness = 0;
			}
		}
		theReader.Close ();
		return false;
	}
	public static void SatisfyPet(string petID,int amount){
		string line = null;
		bool pet = false;
		int oldFullness = 0;
		List<string> newText = new List<string>();
		StreamReader theReader = new StreamReader(petInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),395445)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "pet") {
						if (attribute[1] == petID) {
							pet = true;
						} 
					}
					if(attribute[0] == "happiness"){
						oldFullness = int.Parse(attribute [1]);
					}
				}
				if (pet) {
					if ((oldFullness + amount) <= 86400) {
						line = line.Replace ("happiness:" + oldFullness, "happiness:" + (oldFullness + amount));
					} else {
						line = line.Replace ("happiness:" + oldFullness, "happiness:86400");
					}
				}
				newText.Add(Security.Rot39(line,395445));
				pet = false;
				oldFullness = 0;
			}
		}
		theReader.Close ();
		StreamWriter theWriter = new StreamWriter(petInfoPath);
		for(int i = 0;i < newText.Count; i++){
			theWriter.WriteLine (newText[i]);
		}
		theWriter.Close ();
	}
	public static void WalkPet(string petID,int amount){
		string line = null;
		bool pet = false;
		int oldFullness = 0;
		List<string> newText = new List<string>();
		StreamReader theReader = new StreamReader(petInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),395445)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "pet") {
						if (attribute[1] == petID) {
							pet = true;
						} 
					}
					if(attribute[0] == "fitness"){
						oldFullness = int.Parse(attribute [1]);
					}
				}
				if (pet) {
					if ((oldFullness + amount) <= 86400) {
						line = line.Replace ("fitness:" + oldFullness, "fitness:" + (oldFullness + amount));
					} else {
						line = line.Replace ("fitness:" + oldFullness, "fitness:86400");
					}
				}
				newText.Add(Security.Rot39(line,395445));
				pet = false;
				oldFullness = 0;
			}
		}
		theReader.Close ();
		StreamWriter theWriter = new StreamWriter(petInfoPath);
		for(int i = 0;i < newText.Count; i++){
			theWriter.WriteLine (newText[i]);
		}
		theWriter.Close ();
	}
	public static void StartUpdatePetInfo(long lastVisit){
		petInfoPath = (Application.persistentDataPath + Controller.slot + "/petinfo");
		string line = null;
		bool pet = false;
		int oldFullness = 0;
		int oldHappiness = 0;
		int oldFitness = 0;
		int newFullness = 0;
		int newHappiness = 0;
		int newFitness = 0;
		int secondsSinceLastVisit = (int)((System.DateTime.Now.Ticks - lastVisit)/10000000);
		if(secondsSinceLastVisit>86400){
			secondsSinceLastVisit = 86400;
		}
		List<string> newText = new List<string>();
		StreamReader theReader = new StreamReader(petInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),395445)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				if(line.Contains("pet:")){
					pet = true;
				}
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if(attribute[0] == "fullness"){
						oldFullness = int.Parse(attribute [1]);
						newFullness = int.Parse(attribute [1]);
					}
				}
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if(attribute[0] == "fitness"){
						oldFitness = int.Parse(attribute [1]);
						newFitness = int.Parse(attribute [1]);
					}
				}
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if(attribute[0] == "happiness"){
						oldHappiness = int.Parse(attribute [1]);
						newHappiness = int.Parse(attribute [1]);
					}
				}
				for(int i = 0; i < secondsSinceLastVisit; i++){
					if(newFullness > 0){
						newFullness -= 1;
					}
					if(newFitness > 0){
						newFitness -= 1;
					}
					if(newHappiness > 0 && newFullness < 43200 && newFitness < 43200){
						newHappiness -= 2;
						if(newHappiness < 0){
							newHappiness = 0;
						}
					}
				}
				if (pet) {
					line = line.Replace ("fullness:" + oldFullness, "fullness:" + newFullness);
					line = line.Replace ("fitness:" + oldFitness, "fitness:" + newFitness);
					line = line.Replace ("happiness:" + oldHappiness, "happiness:" + newHappiness);
				}
				newText.Add(Security.Rot39(line,395445));
				pet = false;
				oldFullness = 0;
				oldFitness = 0;
				oldHappiness = 0;
			}
		}
		theReader.Close ();
		StreamWriter theWriter = new StreamWriter(petInfoPath);
		for(int i = 0;i < newText.Count; i++){
			theWriter.WriteLine (newText[i]);
		}
		theWriter.Close ();
	}
	public static void UpdatePetInfo(long ticks){
		string line = null;
		bool pet = false;
		int oldFullness = 0;
		int oldHappiness = 0;
		int oldFitness = 0;
		int newFullness = 0;
		int newHappiness = 0;
		int newFitness = 0;
		int secondsSinceLastVisit = (int)((ticks)/10000000);
		if(ticks>86400){
			ticks = 86400;
		}
		List<string> newText = new List<string>();
		StreamReader theReader = new StreamReader(petInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),395445)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				if(line.Contains("pet:")){
					pet = true;
				}
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if(attribute[0] == "fullness"){
						oldFullness = int.Parse(attribute [1]);
						newFullness = int.Parse(attribute [1]);
					}
				}
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if(attribute[0] == "fitness"){
						oldFitness = int.Parse(attribute [1]);
						newFitness = int.Parse(attribute [1]);
					}
				}
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if(attribute[0] == "happiness"){
						oldHappiness = int.Parse(attribute [1]);
						newHappiness = int.Parse(attribute [1]);
					}
				}
				for(int i = 0; i < secondsSinceLastVisit; i++){
					if(newFullness > 0){
						newFullness -= 1;
					}
					if(newFitness > 0){
						newFitness -= 1;
					}
					if(newHappiness > 0 && newFullness < 43200 && newFitness < 43200){
						newHappiness -= 2;
						if(newHappiness < 0){
							newHappiness = 0;
						}
					}
				}
				if (pet) {
					line = line.Replace ("fullness:" + oldFullness, "fullness:" + newFullness);
					line = line.Replace ("fitness:" + oldFitness, "fitness:" + newFitness);
					line = line.Replace ("happiness:" + oldHappiness, "happiness:" + newHappiness);
				}
				newText.Add(Security.Rot39(line,395445));
				pet = false;
				oldFullness = 0;
				oldFitness = 0;
				oldHappiness = 0;
			}
		}
		theReader.Close ();
		StreamWriter theWriter = new StreamWriter(petInfoPath);
		for(int i = 0;i < newText.Count; i++){
			theWriter.WriteLine (newText[i]);
		}
		theWriter.Close ();
	}
}
