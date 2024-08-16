using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTransferScreen : MonoBehaviour {

	public string[] storageSlots = new string[]{"null","null","null","null","null","null","null","null","null"};
	public GeneralPlacedItem shedScript;
	public string buildingID;
	public int shedX;
	public int shedY;
	public CameraLocation cameraScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0;i<storageSlots.Length;i++){
			storageSlots [i] = storageSlots [i].Replace ('~',';').Replace('`',':');
		}
	}

	public void UpdateInfoInChunk(){
		cameraScript.removeFromChunk ("building",shedX,shedY);
		cameraScript.addToChunk (cameraScript.MainChunkPath + CameraLocation.chunkLat + " " + CameraLocation.chunkLon, "type:building;buildingid:" + BuildingCatalog.getBuildingItemID(buildingID) + ";x:" + shedX + ";y:" + shedY + ";item:"+BuildingCatalog.getBuildingItemID(buildingID)+";slot1:"+storageSlots[0].Replace(';','~').Replace(':','`')+";slot2:"+storageSlots[1].Replace(';','~').Replace(':','`')+";slot3:"+storageSlots[2].Replace(';','~').Replace(':','`')+";slot4:"+storageSlots[3].Replace(';','~').Replace(':','`')+";slot5:"+storageSlots[4].Replace(';','~').Replace(':','`')+";slot6:"+storageSlots[5].Replace(';','~').Replace(':','`')+";slot7:"+storageSlots[6].Replace(';','~').Replace(':','`')+";slot8:"+storageSlots[7].Replace(';','~').Replace(':','`')+";slot9:"+storageSlots[8].Replace(';','~').Replace(':','`'));
	}

	public string getSlotItemID(int slot){
		string item = storageSlots[slot];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="id"){
				return attribute[1];
			}
		}
		return "null";
	}
	public int getItemQuantity(int slot){
		string item = storageSlots [slot];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="qty"){
				return int.Parse(attribute[1]);
			}
		}
		print ("Coudn't get item quantity...");
		return -1;
	}
	public int getItemDurability(int slot){
		string item = storageSlots [slot];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="dur"){
				return int.Parse(attribute[1]);
			}
		}
		return -1;
		print ("Coudn't get item durability...");
	}
	public void removeItem(string id,int amount){
		for(int a = 0; a < amount; a++){
			changeQtyForSlot (getSlotWithLeastAmount(id), -1);
		}
	}
	public void removeSlotItem(int slot,string id,int amount){
		for(int a = 0; a < amount; a++){
			if (getSlotItemID (slot) != "null") {
				changeQtyForSlot (slot, -1);
			} else {
				changeQtyForSlot (getSlotWithLeastAmount(id), -1);
			}
		}
	}
	public void addItem(string id,int amount){
		for(int a = 0; a < amount; a++){
			bool finished = false;
			for(int i = 0;i<storageSlots.Length;i++){
				if (storageSlots [i].Contains ("id:" + id) && !finished) {
					string[] attributeList = storageSlots [i].Split (';');
					for (int i2 = 0; i2 < attributeList.Length; i2++) {
						string[] attribute = attributeList [i2].Split (':');
						if (attribute [0] == "qty") {
							if (int.Parse (attribute [1]) < ItemCatalog.getItemMax (id)) {
								changeQtyForSlot (i, 1);
								finished = true;
							}
						}
					}
				}
			}
			if(!finished){
				storageSlots [getEmptySlot ()] = ItemCatalog.constructItem (id);
			}
		}
	}
	public int getSlotWithLeastAmount(string id){
		int lowestAmount = 1000;
		int slot = 0;
		for(int i = 0;i<storageSlots.Length;i++){
			if(storageSlots[i].Contains("id:"+id)){
				bool idMatch = false;
				int amountTemp = 0;
				string[] attributeList = storageSlots [i].Split (';');
				for(int i2 = 0;i2<attributeList.Length;i2++){
					string[] attribute = attributeList [i2].Split (':');
					if(attribute[0]=="id"){
						if(attribute[1]==id){
							idMatch = true;
						}
					}
					if(attribute[0]=="qty"){
						amountTemp = int.Parse(attribute[1]);
					}
				}
				if(idMatch){
					if(amountTemp < lowestAmount){
						lowestAmount = amountTemp;
						slot = i;
					};
				}
			}
		}
		return slot;
	}
	public bool checkForItem(string id,int amount){
		int totalAmount = 0;
		for(int i = 0;i<storageSlots.Length;i++){
			if(storageSlots[i].Contains("id:"+id)){
				bool idMatch = false;
				int amountTemp = 0;
				string[] attributeList = storageSlots [i].Split (';');
				for(int i2 = 0;i2<attributeList.Length;i2++){
					string[] attribute = attributeList [i2].Split (':');
					if(attribute[0]=="id"){
						if(attribute[1]==id){
							idMatch = true;
						}
					}
					if(attribute[0]=="qty"){
						amountTemp = int.Parse(attribute[1]);
					}
				}
				if(idMatch){
					totalAmount += amountTemp;
				}
			}
		}
		if (totalAmount >= amount) {
			return true;
		} else {
			return false;
		}
	}
	public void changeQtyForSlot(int slot,int amount){
		bool nothing = false;
		string item = storageSlots[slot];
		string[] attributeList = item.Split (';');
		string newItem = "";
		for(int i = 0;i<attributeList.Length;i++){
			string[] attribute = attributeList [i].Split (':');
			if (attribute [0] == "qty") {
				newItem += "qty:" + (int.Parse (attribute [1]) + amount).ToString ();
				if((int.Parse (attribute [1])+amount)<=0){
					nothing = true;
				}
			} else {
				newItem += attribute [0] + ":" + attribute [1];
			}
			if(i<attributeList.Length-1){
				newItem += ";";
			}
		}
		if (!nothing) {
			storageSlots [slot] = newItem;
		} else {
			storageSlots [slot] = "null";
		}
	}
	public int getEmptySlot(){
		for(int i = 0;i<storageSlots.Length;i++){
			if(!storageSlots[i].Contains("id")){
				return i;
			}
		}
		print ("Couldn't find empty slot...");
		return -1;
	}
}
