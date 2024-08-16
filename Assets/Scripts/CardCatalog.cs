using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCatalog : MonoBehaviour {
	public static string[] weaponCards = new string[]{
		"id:0000;name:Wood\nSword;img:0;atk:1;weaponid:0004",
		"id:0001;name:Wood\nSword;img:1;atk:5;weaponid:0004",
		"id:0002;name:Aluminum\nSword;img:2;atk:5;weaponid:0009",
		"id:0003;name:Aluminum\nSword;img:3;atk:10;weaponid:0009",
		"id:0004;name:Iron\nSword;img:4;atk:10;weaponid:0014",
		"id:0005;name:Iron\nSword;img:5;atk:15;weaponid:0014",
		"id:0006;name:Copper\nSword;img:6;atk:15;weaponid:0019",
		"id:0007;name:Copper\nSword;img:7;atk:20;weaponid:0019",
		"id:0008;name:Gold\nSword;img:8;atk:20;weaponid:0024",
		"id:0009;name:Gold\nSword;img:9;atk:25;weaponid:0024",
		"id:0010;name:Diamond\nSword;img:10;atk:25;weaponid:0029",
		"id:0011;name:Diamond\nSword;img:11;atk:30;weaponid:0029",
		"id:0012;name:Club;img:30;atk:5;weaponid:0136",
		"id:0013;name:Club;img:31;atk:10;weaponid:0136",
		"id:0014;name:Spiked Club;img:32;atk:10;weaponid:0137",
		"id:0015;name:Spiked Club;img:33;atk:15;weaponid:0137",
		"id:0016;name:Bone Sword;img:34;atk:5;weaponid:0138",
		"id:0017;name:Bone Sword;img:35;atk:10;weaponid:0138",
		"id:0018;name:Toxic Bone Sword;img:36;atk:10;weaponid:0146",
		"id:0019;name:Toxic Bone Sword;img:37;atk:15;weaponid:0146",
		"id:0020;name:Crab Beater;img:38;atk:5;weaponid:0155",
		"id:0021;name:Crab Beater;img:39;atk:10;weaponid:0155",
		"id:0022;name:Plasma Beam;img:40;atk:10;weaponid:0147",
		"id:0023;name:Plasma Beam;img:41;atk:15;weaponid:0147",
		"id:0024;name:Viral Sword;img:42;atk:5;weaponid:0152",
		"id:0025;name:Viral Sword;img:43;atk:10;weaponid:0152",
		"id:0026;name:Umbrella;img:44;rainatk:10;atk:5;weaponid:0171",
		"id:0027;name:Umbrella;img:45;rainatk:15;atk:10;weaponid:0171",
		"id:0028;name:Icycle;img:46;snowatk:10;atk:5;weaponid:0175",
		"id:0029;name:Icycle;img:47;snowatk:15;atk:10;weaponid:0175"
	};
	public static string[] bondCards = new string[]{
		"id:0000;name:Chef;img:12;npc:chef",
		"id:0001;name:Builder;img:13;npc:construction",
		"id:0002;name:Farmer;img:14;npc:farmer",
		"id:0003;name:Fisherman;img:15;npc:fisherman",
		"id:0004;name:Boy;img:16;npc:boy",
		"id:0005;name:Knight;img:17;npc:knight",
		"id:0005;name:Lumberjack;img:18;npc:lumberjack"
	};
	public static string[] skillCards = new string[]{
		"id:0000;name:Cutting;img:19;skill:cutting",
		"id:0001;name:Mining;img:20;skill:mining",
		"id:0002;name:Digging;img:21;skill:digging",
		"id:0003;name:Farming;img:22;skill:farming",
		"id:0004;name:Slaying;img:23;skill:slaying",
		"id:0005;name:Fishing;img:24;skill:fishing",
		"id:0006;name:Crafting;img:25;skill:crafting",
		"id:0007;name:Smelting;img:26;skill:smelting",
		"id:0008;name:Smithing;img:27;skill:smithing",
		"id:0009;name:Cooking;img:28;skill:cooking",
		"id:0010;name:Questing;img:29;skill:questing",
	};
	public static Sprite[] cardSpriteSheet;
	public Sprite[] cardSpriteSheetTemp;
	// Use this for initialization
	void Start () {
		cardSpriteSheet = cardSpriteSheetTemp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//USED TO FIND ITEMS INDEX BY ID
	public static int getCardSlot(string id,string type){
		if(type == "weapon"){
			for(int i = 0;i<weaponCards.Length;i++){
				if(weaponCards[i].Contains("id:"+id)){
					return i;
				}
			}
		}
		if(type == "bond"){
			for(int i = 0;i<bondCards.Length;i++){
				if(weaponCards[i].Contains("id:"+id)){
					return i;
				}
			}
		}
		if(type == "skill"){
			for(int i = 0;i<skillCards.Length;i++){
				if(skillCards[i].Contains("id:"+id)){
					return i;
				}
			}
		}
		return -1;
	}

	//USED TO FIND ITEMS ID BY SLOT
	public static string getCardID(int slot,string type){
		if(type == "weapon"){
			string cards = weaponCards [slot];
			string[] attributeList = cards.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="id"){
					return attribute[1];
				}
			}
		}
		if(type == "bond"){
			string cards = bondCards [slot];
			string[] attributeList = cards.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="id"){
					return attribute[1];
				}
			}
		}
		if(type == "skill"){
			string cards = skillCards [slot];
			string[] attributeList = cards.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="id"){
					return attribute[1];
				}
			}
		}
		return "";
	}

	public static int getSlotWithWeaponID(string id){
		for(int i = 0;i < weaponCards.Length;i++){
			if(weaponCards[i].Contains("weaponid:" + id)){
				return i;
			}
		}
		return 0;
	}
	public static int getCardImage(string id,string type){
		if(id == "null"){
			return 0;
		}
		if(type == "weapon"){
			string card = weaponCards [getCardSlot (id,"weapon")];
			string[] attributeList = card.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="img"){
					return int.Parse(attribute[1]);
				}
			}
		}
		if(type == "bond"){
			string card = bondCards [getCardSlot (id,"bond")];
			string[] attributeList = card.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="img"){
					return int.Parse(attribute[1]);
				}
			}
		}
		if(type == "skill"){
			string card = skillCards [getCardSlot (id,"skill")];
			string[] attributeList = card.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="img"){
					return int.Parse(attribute[1]);
				}
			}
		}
		return 0;
	}
	public static int getCardAttack(string id,string type){
		if(id == "null"){
			return 0;
		}
		if(type == "weapon"){
			string card = weaponCards [getCardSlot (id,"weapon")];
			string[] attributeList = card.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="snowatk" && CameraLocation.mainWeather == "snow"){
					return int.Parse(attribute[1]);
				}
				if(attribute[0]=="rainatk" && CameraLocation.mainWeather == "rain"){
					return int.Parse(attribute[1]);
				}
				if(attribute[0]=="atk"){
					return int.Parse(attribute[1]);
				}
			}
		}
		if(type == "bond"){
			string card = bondCards [getCardSlot (id,"bond")];
			string[] attributeList = card.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="npc"){
					return int.Parse(attribute[1]);
				}
			}
		}
		if(type == "skill"){
			string card = skillCards [getCardSlot (id,"skill")];
			string[] attributeList = card.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="img"){
					return int.Parse(attribute[1]);
				}
			}
		}
		return 0;
	}
	public static string getCardName(string id,string type){
		if(type == "weapon"){
			string card = weaponCards [getCardSlot (id,"weapon")];
			string[] attributeList = card.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="name"){
					return attribute[1];
				}
			}
		}
		if(type == "bond"){
			string card = bondCards [getCardSlot (id,"bond")];
			string[] attributeList = card.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="name"){
					return attribute[1];
				}
			}
		}
		if(type == "skill"){
			string card = skillCards [getCardSlot (id,"skill")];
			string[] attributeList = card.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="name"){
					return attribute[1];
				}
			}
		}
		return "";
	}
	public static string getCardNpc(string id){
		string card = bondCards [getCardSlot (id,"bond")];
		string[] attributeList = card.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="npc"){
				return attribute[1];
			}
		}
		return "";
	}
	public static string getCardSkill(string id){
		string card = skillCards [getCardSlot (id,"skill")];
		string[] attributeList = card.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="skill"){
				return attribute[1];
			}
		}
		return "";
	}
}
