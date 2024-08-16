using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCatalog : MonoBehaviour {
	public static string[] pets = new string[]{
		"id:0000;name:Scouter;ability:Increases entity\nspawn rate.;img1:0;img2:0;img3:0;" +
		"costume1img1:1;costume1img2:1;costume1img3:1;" +
		"costume2img1:2;costume2img2:2;costume2img3:2;" +
		"costume3img1:3;costume3img2:3;costume3img3:3;" +
		"costume4img1:4;costume4img2:4;costume4img3:4",
		"id:0001;name:Roly;ability:Increases entity\nitem drops.;img1:5;img2:6;img3:7;" +
		"costume1img1:8;costume1img2:9;costume1img3:10;" +
		"costume2img1:11;costume2img2:12;costume2img3:13;" +
		"costume3img1:14;costume3img2:15;costume3img3:16;" +
		"costume4img1:17;costume4img2:18;costume4img3:19",
		"id:0002;name:Morn;ability:Increases wood\ncutting experience.;img1:20;img2:20;img3:20;" +
		"costume1img1:21;costume1img2:21;costume1img3:21;" +
		"costume2img1:22;costume2img2:22;costume2img3:22;" +
		"costume3img1:23;costume3img2:23;costume3img3:23;" +
		"costume4img1:24;costume4img2:24;costume4img3:24",
		"id:0003;name:Koda;ability:An extra crate\ncan be carried.;img1:25;img2:26;img3:27;" +
		"costume1img1:28;costume1img2:29;costume1img3:30;" +
		"costume2img1:31;costume2img2:32;costume2img3:33;" +
		"costume3img1:34;costume3img2:35;costume3img3:36;" +
		"costume4img1:37;costume4img2:38;costume4img3:39",
		"id:0004;name:Morty;ability:Increases hole\ndigging experience.;img1:40;img2:45;img3:46;" +
		"costume1img1:41;costume1img2:45;costume1img3:46;" +
		"costume2img1:42;costume2img2:45;costume2img3:46;" +
		"costume3img1:43;costume3img2:45;costume3img3:46;" +
		"costume4img1:44;costume4img2:45;costume4img3:46",
		"id:0005;name:Kimi;ability:Slimes can drop\njello.;img1:47;img2:47;img3:47;" +
		"costume1img1:48;costume1img2:48;costume1img3:48;" +
		"costume2img1:49;costume2img2:49;costume2img3:49;" +
		"costume3img1:50;costume3img2:50;costume3img3:50;" +
		"costume4img1:51;costume4img2:51;costume4img3:51",
		"id:0006;name:Clam;ability:Increases fishing\nexperience.;img1:52;img2:53;img3:54;" +
		"costume1img1:55;costume1img2:56;costume1img3:57;" +
		"costume2img1:58;costume2img2:59;costume2img3:60;" +
		"costume3img1:61;costume3img2:62;costume3img3:63;" +
		"costume4img1:64;costume4img2:65;costume4img3:66",
		"id:0007;name:Fairy;ability:Increases duration\nof spells.;img1:67;img2:68;img3:69;" +
		"costume1img1:70;costume1img2:71;costume1img3:72;" +
		"costume2img1:73;costume2img2:74;costume2img3:75;" +
		"costume3img1:76;costume3img2:77;costume3img3:78;" +
		"costume4img1:79;costume4img2:80;costume4img3:81",
		"id:0008;name:Fire;ability:Increases smelting\nexperience.;img1:82;img2:82;img3:82;" +
		"costume1img1:83;costume1img2:83;costume1img3:83;" +
		"costume2img1:84;costume2img2:84;costume2img3:84;" +
		"costume3img1:85;costume3img2:85;costume3img3:85;" +
		"costume4img1:86;costume4img2:86;costume4img3:86",
		"id:0009;name:Rock;ability:Increases mining\nexperience.;img1:87;img2:88;img3:89;" +
		"costume1img1:90;costume1img2:91;costume1img3:92;" +
		"costume2img1:93;costume2img2:94;costume2img3:95;" +
		"costume3img1:96;costume3img2:97;costume3img3:98;" +
		"costume4img1:99;costume4img2:100;costume4img3:101",
		"id:0010;name:Spirit;ability:Increases slaying\nexperience.;img1:102;img2:102;img3:102;" +
		"costume1img1:103;costume1img2:103;costume1img3:103;" +
		"costume2img1:104;costume2img2:104;costume2img3:104;" +
		"costume3img1:105;costume3img2:105;costume3img3:105;" +
		"costume4img1:106;costume4img2:106;costume4img3:106",
		"id:0011;name:Stick;ability:Chance of showing\nright dungeon choice.;img1:107;img2:107;img3:107;" +
		"costume1img1:108;costume1img2:108;costume1img3:108;" +
		"costume2img1:109;costume2img2:109;costume2img3:109;" +
		"costume3img1:110;costume3img2:110;costume3img3:110;" +
		"costume4img1:111;costume4img2:111;costume4img3:111"

	};
	public static Sprite[] petSpriteSheet;
	public Sprite[] petSpriteSheetTemp;

	// Use this for initialization
	void Start () {
		petSpriteSheet = petSpriteSheetTemp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//USED TO FIND PLANTS INDEX BY ID
	public static int getPetSlot(string id){
		for(int i = 0;i<pets.Length;i++){
			if(pets[i].Contains("id:"+id)){
				return i;
			}
		}
		return -1;
	}
	public static int getPetImage(string id,int stage, int costume){
		if(id == "null"){
			return 0;
		}
		string pet = pets [getPetSlot (id)];
		string[] attributeList = pet.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if (costume == 0) {
				if (attribute [0] == "img1" && stage == 0) {
					return int.Parse (attribute [1]);
				}
				if (attribute [0] == "img2" && stage == 1) {
					return int.Parse (attribute [1]);
				}
				if (attribute [0] == "img3" && stage == 2) {
					return int.Parse (attribute [1]);
				}
			} else {
				string costumeFind = "costume" + costume + "img" + (stage+1).ToString ();
				if (attribute [0] == costumeFind) {
					return int.Parse (attribute [1]);
				}
			}
		}
		return 0;
	}
	public static string getPetName(string id){
		string pet = pets [getPetSlot (id)];
		string[] attributeList = pet.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="name"){
				return attribute[1];
			}
		}
		return "null";
	}
	public static string getPetAbility(string id){
		string pet = pets [getPetSlot (id)];
		string[] attributeList = pet.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="ability"){
				return attribute[1];
			}
		}
		return "null";
	}
	public static string getPetID(int slot){
		string pet = pets [slot];
		string[] attributeList = pet.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="id"){
				return attribute[1];
			}
		}
		return "";
	}
}
