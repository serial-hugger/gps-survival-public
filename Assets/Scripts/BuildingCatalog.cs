using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCatalog : MonoBehaviour {

	public static string[] buildings = new string[]{
		"id:0000;name:Storage Shed;itemid:0105;part:0033x128;part:0106x64;part:0107x4;part:0108x4",
		"id:0001;name:House;itemid:0109;part:0033x128;part:0106x64;part:0107x4;part:0108x4"
	};
	public static Sprite[] buildingSpriteSheet;
	public Sprite[] buildingSpriteSheetTemp;

	// Use this for initialization
	void Start () {
		buildingSpriteSheet = buildingSpriteSheetTemp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//USED TO FIND ITEMS INDEX BY ID
	public static int getBuildingSlot(string id){
		for(int i = 0;i<buildings.Length;i++){
			if(buildings[i].Contains("id:"+id)){
				return i;
			}
		}
		return -1;
	}
	//USED TO FIND ITEMS ID BY SLOT
	public static string getBuildingID(int slot){
		string fishs = buildings [slot];
		string[] attributeList = fishs.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="id"){
				return attribute[1];
			}
		}
		return "null";
	}
	public static string getBuildingItemID(string id){
		string fishs = buildings [getBuildingSlot(id)];
		string[] attributeList = fishs.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="itemid"){
				return attribute[1];
			}
		}
		return "";
	}
	public static string getBuildingName(string id){
		string fishs = buildings [getBuildingSlot(id)];
		string[] attributeList = fishs.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="name"){
				return attribute[1];
			}
		}
		return "";
	}
	public static string[] getNeededItems(int slot){
		string[] items = new string[]{"null","null","null","null","null","null"};
		int currentItem = 0;
			items [currentItem] = "null";
			string item = buildings [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="part"){
					string[] splitx = attribute [1].Split ('x');
					items[currentItem] = splitx[0];
					currentItem += 1;
				}
			}
		return items;
	}
	public static int[] getNeededAmounts(int slot){
		int[] items = new int[6];
		int currentItem = 0;
		items [currentItem] = 0;
		string item = buildings [slot];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="part"){
				string[] splitx = attribute [1].Split ('x');
				items[currentItem] = int.Parse(splitx[1]);
				currentItem += 1;
			}
		}
		return items;
	}
}
