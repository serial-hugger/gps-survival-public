using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCatalog : MonoBehaviour {

	public static string[] fish = new string[]{
		"id:0000;name:Bass;img:0;speed:5.0;itemid:0067",
		"id:0001;name:Catfish;img:1;speed:3.0;itemid:0068",
		"id:0002;name:Carp;img:2;speed:3.0;itemid:0069",
		"id:0003;name:Salmon;img:3;speed:5.0;itemid:0070",
		"id:0004;name:Koi;img:4;speed:1.0;itemid:0071",
		"id:0005;name:Bluegill;img:5;speed:3.0;itemid:0072",
		"id:0006;name:Sweetfish;img:6;speed:3.0;itemid:0073",
		"id:0007;name:Freshwater Drum;img:7;speed:3.0;itemid:0074",
		"id:0008;name:White Bass;img:8;speed:5.0;itemid:0075",
		"id:0009;name:Yellow Bass;img:9;speed:3.0;itemid:0076",
		"id:0010;name:Striped Bass;img:10;speed:3.0;itemid:0077",
		"id:0011;name:Soul Bass;img:11;speed:1.0;itemid:0115",
		"id:0012;name:Soul Carp;img:12;speed:1.0;itemid:0116",
		"id:0013;name:Soul Koi;img:13;speed:1.0;itemid:0117",
		"id:0014;name:Soul Bluegill;img:14;speed:1.0;itemid:0118",
		"id:0015;name:Soul Sweetfish;img:15;speed:1.0;itemid:0119",
		"id:0016;name:Makky;img:16;speed:1.0;itemid:0120",
		"id:0017;name:Goldfish;img:17;speed:1.0;itemid:0121",
		"id:0018;name:Guppy;img:18;speed:1.0;itemid:0122",
		"id:0019;name:Jellyfish;img:19;speed:1.0;itemid:0123",
		"id:0020;name:Squid;img:20;speed:1.0;itemid:0124",
	};
	public static int[]  normalFishIndexes = new int[]{0,1,2,3,4,5,6,7,8,9,10};
	public static int[]  rareFishIndexes = new int[]{11,12,13,14,15,16,17,18,19,20};
	public static Sprite[] fishSpriteSheet;
	public Sprite[] fishSpriteSheetTemp;

	// Use this for initialization
	void Start () {
		fishSpriteSheet = fishSpriteSheetTemp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//USED TO FIND ITEMS INDEX BY ID
	public static int getFishSlot(string id){
		for(int i = 0;i<fish.Length;i++){
			if(fish[i].Contains("id:"+id)){
				return i;
			}
		}
		return -1;
	}
	//USED TO FIND ITEMS ID BY SLOT
	public static string getFishID(int slot){
		string fishs = fish [slot];
		string[] attributeList = fishs.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="id"){
				return attribute[1];
			}
		}
		return "";
	}
	public static string getFishItemID(string id){
		string fishs = fish [getFishSlot(id)];
		string[] attributeList = fishs.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="itemid"){
				return attribute[1];
			}
		}
		return "";
	}
	public static int getFishImage(string id){
		string fishs = fish [getFishSlot(id)];
		string[] attributeList = fishs.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="img"){
				return int.Parse(attribute[1]);
			}
		}
		return 0;
	}
	public static float getFishSpeed(string id){
		string fishs = fish [getFishSlot(id)];
		string[] attributeList = fishs.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="speed"){
				return float.Parse(attribute[1]);
			}
		}
		return 0.0f;
	}
}
