using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class QuestInfo : MonoBehaviour {

	public Sprite[] npcSprites;
	public static Sprite[] npcSpritesGlobal;
	public string heldQuests = "";
	public static string questInfoPath = "";
	public static Texture2D map;
	public static int chunkLatAway;
	public static int chunkLonAway;
	public int day;

	void Start(){
		npcSpritesGlobal = npcSprites;
		questInfoPath = (Application.persistentDataPath + Controller.slot + "/questinfo");
		day = System.DateTime.Now.Day;
		RemoveOldQuests ();
		if(HasMapQuest()){
			print (GetMapURL());
			StartCoroutine(GetMapImage(GetMapURL(),0f));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(System.DateTime.Now.Day!=day){
			RemoveOldQuests ();
			day = System.DateTime.Now.Day;
		}
	}
	public static bool IsHeldQuestsFull(){
		string line = null;
		int amount = 0;
		StreamReader theReader = new StreamReader(questInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),29)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "status") {
						if(int.Parse(attribute[1])==1){
							amount += 1;
						}
					}
				}
			}
		}
		if(amount >= 3){
			return true;
		}
		return false;
	}
	public static bool HasMapQuest(){
		string line = null;
		StreamReader theReader = new StreamReader(questInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),29)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "type") {
						if(attribute[1]=="map"){
							return true;
						}
					}
				}
			}
		}
		return false;
	}
	public static bool HoldingQuestOfNPC(string npcId, int statusFind){
		string line = null;
		bool held = false;
		string npcID = "";
		int status = 0;
		StreamReader theReader = new StreamReader(questInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),29)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "type") {
						if(attribute[1]=="held"){
							held = true;
						}
					}
					if (attribute [0] == "npc") {
						npcID = attribute[1];
					}
					if (attribute [0] == "status") {
						status = int.Parse(attribute[1]);
					}
				}
			}
		}
		if (statusFind != -1) {
			if (held == true && npcID == npcId && status == statusFind) {
				return true;
			}
		} else {
			if (held == true && npcID == npcId) {
				return true;
			}
		}
		return false;
	}
	public static bool IsQuestExpired(int time){
		if((System.DateTime.Now.Year + (System.DateTime.Now.Month*40) + System.DateTime.Now.Day)>time){
			return true;
		}
		return false;
	}
	public static void AddHeldQuest(string npcID, string npcName,int npcSprite, int time, string item, int amount){
		if(!IsHeldQuestsFull()){
			StreamWriter file = new StreamWriter (questInfoPath,true);
			file.WriteLine (Security.Rot39("type:held;"+"npc:"+npcID+";name:"+npcName+";npcimage:" + npcSprite + ";time:"+time+";item:"+item+";qty:"+amount+";status:"+0,29));
			file.Close ();
		}
	}
	public void AddMapQuest(int myChunkLatAway,int myChunkLonAway, float longSize,int time){
		if(!HasMapQuest()){
			chunkLatAway = myChunkLatAway;
			chunkLonAway = myChunkLonAway;
			StartCoroutine(GetMapImage("null",longSize));
			StreamWriter file = new StreamWriter (questInfoPath,true);
			file.WriteLine (Security.Rot39("type:map;time:"+time+";lat:"+(CameraLocation.actualChunkLat+(chunkLatAway*.001f)).ToString()+";lon:"+(CameraLocation.actualChunkLon+(longSize * chunkLonAway)).ToString()+";map:"+"https$//maps.googleapis.com/maps/api/staticmap?center=" + ((CameraLocation.actualChunkLat) + (.001f * chunkLatAway)).ToString () + "," + ((CameraLocation.actualChunkLon) + (.001f * chunkLonAway)).ToString () + "&maptype=satellite&zoom=17&size=112x112&key=REDACTED"+";status:"+1,29));
			file.Close ();
		}
	}
	public static IEnumerator GetMapImage(string url, float longSize){
		if (map == null && GetMapStatus()!=2) {
			if (Application.internetReachability != NetworkReachability.NotReachable) {
				WWW www;
				map = new Texture2D (4, 4, TextureFormat.DXT1, false);
				if (url == "null") {
					www = new WWW ("https://maps.googleapis.com/maps/api/staticmap?center=" + ((CameraLocation.actualChunkLat) + (.001f * chunkLatAway)).ToString () + "," + ((CameraLocation.actualChunkLon) + (longSize * chunkLonAway)).ToString () + "&maptype=satellite&zoom=14&size=112x112&key=REDACTED");	
				} else {
					print (url);
					www = new WWW (url);
				}
				yield return www;
				if(www.isDone){
					www.LoadImageIntoTexture (map);
				}
			} else {
				map = null;
			}
		}
	}
	public static void ChangeStatusOfQuest(string npcID,int status){
		string line = null;
		bool npc = false;
		int oldStatus = 0;
		List<string> newText = new List<string>();
		StreamReader theReader = new StreamReader(questInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),29)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "npc") {
						if (attribute[1] == npcID) {
							npc = true;
						} 
					}
					if(attribute[0] == "status"){
						oldStatus = int.Parse(attribute [1]);
					}
				}
				if (npc) {
					line = line.Replace ("status:"+oldStatus, "status:"+status);
				}
				newText.Add(Security.Rot39(line,29));
				npc = false;
				oldStatus = 0;
			}
		}
		theReader.Close ();
		StreamWriter theWriter = new StreamWriter(questInfoPath);
		for(int i = 0;i < newText.Count; i++){
			theWriter.WriteLine (newText[i]);
		}
		theWriter.Close ();
	}
	public static void ChangeStatusOfMapQuest(int status){
		string line = null;
		bool map = false;
		int oldStatus = 0;
		List<string> newText = new List<string>();
		StreamReader theReader = new StreamReader(questInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),29)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "type") {
						if (attribute[1] == "map") {
							map = true;
						} 
					}
					if(attribute[0] == "status"){
						oldStatus = int.Parse(attribute [1]);
					}
				}
				if (map) {
					line = line.Replace ("status:"+oldStatus, "status:"+status);
				}
				newText.Add(Security.Rot39(line,29));
				map = false;
				oldStatus = 0;
			}
		}
		theReader.Close ();
		StreamWriter theWriter = new StreamWriter(questInfoPath);
		for(int i = 0;i < newText.Count; i++){
			theWriter.WriteLine (newText[i]);
		}
		theWriter.Close ();
	}
	public static void RemoveOldQuests(){
		string line = null;
		bool held = false;
		int time = 0;
		bool old = false;
		List<string> newText = new List<string>();
		StreamReader theReader = new StreamReader(questInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),29)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "type") {
						if(attribute[1]=="held"){
							held = true;
						}
						if(attribute[1]=="map"){
							held = true;
						}
					}
					if (attribute [0] == "time") {
						time = int.Parse (attribute [1]);
					}
				}
				if(held && IsQuestExpired(time)){
					old = true;
				}
				held = false;
				time = 0;
			}
		}
		theReader.Close ();
		if(old){
			File.WriteAllText(questInfoPath,"");
			map = null;
		}
	}
	public static void RemoveQuest(int number){
		string line = null;
		int index = 0;
		List<string> newText = new List<string>();
		StreamReader theReader = new StreamReader(questInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),29)).Contains(":")) {
				if(line.Contains("type:")){
					if(index == number){
						line = "";
					}
					newText.Add (Security.Rot39(line,29));
					index++;
				}
			}
		}
		theReader.Close ();
		File.Delete (questInfoPath);
		var file = File.CreateText (questInfoPath);
		for(int i = 0; i < newText.Count;i++){
			file.WriteLine (newText[i]);
		}
		file.Close ();
	}
	public static string LoadHeldQuest(int number){
		string line = null;
		string finalLine = "";
		bool held = false;
		int index = 0;
		StreamReader theReader = new StreamReader(questInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),29)).Contains(":")) {
				if(line.Contains("status:1") && line.Contains("type:held")){
					if (index == number) {
						finalLine = line;
					}
					index += 1;
				}
			}
		}
		theReader.Close ();
		return finalLine;
	}
	public static string LoadMapQuest(){
		string line = null;
		string finalLine = "";
		StreamReader theReader = new StreamReader(questInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),29)).Contains(":")) {
				if(line.Contains("type:map")){
					finalLine = line;
				}
			}
		}
		theReader.Close ();
		return finalLine;
	}
	public static string GetNpcID(int number){
		string line = LoadHeldQuest(number);
		var attributeGroups = line.Split (new char[]{ ';' });
		for (int i = 0; i < attributeGroups.Length; i++) {
			var attribute = attributeGroups [i].Split (new char[]{ ':' });
			if (attribute [0] == "npc") {
				return attribute [1];
			}
		}
		return "null";
	}
	public static string GetNpcName(int number){
		string line = LoadHeldQuest(number);
		var attributeGroups = line.Split (new char[]{ ';' });
		for (int i = 0; i < attributeGroups.Length; i++) {
			var attribute = attributeGroups [i].Split (new char[]{ ':' });
			if (attribute [0] == "name") {
				return attribute [1];
			}
		}
		return "null";
	}
	public static int GetNpcImage(int number){
		string line = LoadHeldQuest(number);
		var attributeGroups = line.Split (new char[]{ ';' });
		for (int i = 0; i < attributeGroups.Length; i++) {
			var attribute = attributeGroups [i].Split (new char[]{ ':' });
			if (attribute [0] == "npcimage") {
				return int.Parse(attribute [1]);
			}
		}
		return -1;
	}
	public static string GetItem(int number){
		string line = LoadHeldQuest(number);
		var attributeGroups = line.Split (new char[]{ ';' });
		for (int i = 0; i < attributeGroups.Length; i++) {
			var attribute = attributeGroups [i].Split (new char[]{ ':' });
			if (attribute [0] == "item") {
				return attribute [1];
			}
		}
		return "null";
	}
	public static int GetItemQty(int number){
		string line = LoadHeldQuest(number);
		var attributeGroups = line.Split (new char[]{ ';' });
		for (int i = 0; i < attributeGroups.Length; i++) {
			var attribute = attributeGroups [i].Split (new char[]{ ':' });
			if (attribute [0] == "qty") {
				return int.Parse(attribute [1]);
			}
		}
		return -1;
	}
	public static string GetMapURL(){
		string line = LoadMapQuest();
		var attributeGroups = line.Split (new char[]{ ';' });
		for (int i = 0; i < attributeGroups.Length; i++) {
			var attribute = attributeGroups [i].Split (new char[]{ ':' });
			if (attribute [0] == "map") {
				return attribute [1].Replace('$',':');
			}
		}
		return "null";
	}
	public static float GetMapLat(){
		string line = LoadMapQuest();
		var attributeGroups = line.Split (new char[]{ ';' });
		for (int i = 0; i < attributeGroups.Length; i++) {
			var attribute = attributeGroups [i].Split (new char[]{ ':' });
			if (attribute [0] == "lat") {
				return float.Parse(attribute [1]);
			}
		}
		return 0.0f;
	}
	public static float GetMapLon(){
		string line = LoadMapQuest();
		var attributeGroups = line.Split (new char[]{ ';' });
		for (int i = 0; i < attributeGroups.Length; i++) {
			var attribute = attributeGroups [i].Split (new char[]{ ':' });
			if (attribute [0] == "lon") {
				return float.Parse(attribute [1]);
			}
		}
		return 0.0f;
	}
	public static int GetMapStatus(){
		string line = LoadMapQuest();
		var attributeGroups = line.Split (new char[]{ ';' });
		for (int i = 0; i < attributeGroups.Length; i++) {
			var attribute = attributeGroups [i].Split (new char[]{ ':' });
			if (attribute [0] == "status") {
				return int.Parse(attribute [1]);
			}
		}
		return 0;
	}
}
