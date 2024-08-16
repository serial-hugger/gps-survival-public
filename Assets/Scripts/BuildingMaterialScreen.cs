using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMaterialScreen : MonoBehaviour {

	public string buildingID = "";
	public SpriteRenderer buildingDisplay;
	public int baseX;
	public int baseY;
	public GameObject baseObject;
	public TextMesh name;
	public TextMesh needed1;
	public TextMesh needed2;
	public TextMesh needed3;
	public TextMesh needed4;
	public TextMesh needed5;
	public TextMesh needed6;
	public SpriteRenderer button1;
	public SpriteRenderer button2;
	public SpriteRenderer button3;
	public SpriteRenderer button4;
	public SpriteRenderer button5;
	public SpriteRenderer button6;
	public Sprite buttonAdd;
	public Sprite buttonDone;
	public Sprite buttonNull;
	public string[] neededItems = new string[]{"","","","","",""};
	public int[] neededAmounts = new int[]{0,0,0,0,0,0};
	public int[] aquiredAmounts = new int[]{0,0,0,0,0,0};
	public CameraLocation cameraScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void UpdateBoxInfo(){
		neededAmounts = BuildingCatalog.getNeededAmounts (BuildingCatalog.getBuildingSlot(buildingID));
		neededItems = BuildingCatalog.getNeededItems (BuildingCatalog.getBuildingSlot(buildingID));
		if(neededItems[0]=="null"){
			button1.sprite = buttonNull;
		}
		if(neededItems[1]=="null"){
			button2.sprite = buttonNull;
		}
		if(neededItems[2]=="null"){
			button3.sprite = buttonNull;
		}
		if(neededItems[3]=="null"){
			button4.sprite = buttonNull;
		}
		if(neededItems[4]=="null"){
			button5.sprite = buttonNull;
		}
		if(neededItems[5]=="null"){
			button6.sprite = buttonNull;
		}
		if (!cameraScript.getMaterialsAmount (1, baseX, baseY).Split ('x') [0].Contains("null")) {
			aquiredAmounts [0] = int.Parse (cameraScript.getMaterialsAmount (1, baseX, baseY).Split ('x') [1]);
			if (aquiredAmounts [0] >= neededAmounts[0]) {
				button1.sprite = buttonDone;
			}else{
				button1.sprite = buttonAdd;
			}
		}
		if (!cameraScript.getMaterialsAmount (2, baseX, baseY).Split ('x') [0].Contains("null")) {
			aquiredAmounts [1] = int.Parse (cameraScript.getMaterialsAmount (2, baseX, baseY).Split ('x') [1]);
			if (aquiredAmounts [1] >= neededAmounts[1]) {
				button2.sprite = buttonDone;
			} else {
				button2.sprite = buttonAdd;
			}
		}
		if (!cameraScript.getMaterialsAmount (3, baseX, baseY).Split ('x') [0].Contains("null")) {
			aquiredAmounts [2] = int.Parse (cameraScript.getMaterialsAmount (3, baseX, baseY).Split ('x') [1]);
			if (aquiredAmounts [2] >= neededAmounts[2]) {
				button3.sprite = buttonDone;
			} else {
				button3.sprite = buttonAdd;
			}
		}
		if (!cameraScript.getMaterialsAmount (4, baseX, baseY).Split ('x') [0].Contains("null")) {
			aquiredAmounts [3] = int.Parse (cameraScript.getMaterialsAmount (4, baseX, baseY).Split ('x') [1]);
			if (aquiredAmounts [3] >= neededAmounts[3]) {
				button4.sprite = buttonDone;
			} else {
				button4.sprite = buttonAdd;
			}
		}
		if (!cameraScript.getMaterialsAmount (5, baseX, baseY).Split ('x') [0].Contains("null")) {
			aquiredAmounts [4] = int.Parse (cameraScript.getMaterialsAmount (5, baseX, baseY).Split ('x') [1]);
			if (aquiredAmounts [4] >= neededAmounts[4]) {
				button5.sprite = buttonDone;
			} else {
				button5.sprite = buttonAdd;
			}
		}
		if (!cameraScript.getMaterialsAmount (6, baseX, baseY).Split ('x') [0].Contains("null")) {
			aquiredAmounts [5] = int.Parse (cameraScript.getMaterialsAmount (6, baseX, baseY).Split ('x') [1]);
			if (aquiredAmounts [5] >= neededAmounts[5]) {
				button6.sprite = buttonDone;
			} else {
				button6.sprite = buttonAdd;
			}
		}
		needed1.text = "";
		needed2.text = "";
		needed3.text = "";
		needed4.text = "";
		needed5.text = "";
		needed6.text = "";
		name.text = "";
		buildingDisplay.sprite = null;
		if (neededAmounts [0] != 0) {
			needed1.text = ItemCatalog.getItemName (neededItems [0]) + " (" + aquiredAmounts[0] + "/" + neededAmounts [0]+")"; 
		}
		if (neededAmounts [1] != 0) {
			needed2.text = ItemCatalog.getItemName (neededItems [1]) + " (" + aquiredAmounts[1] + "/" +  + neededAmounts [1]+")"; 
		}
		if (neededAmounts [2] != 0) {
			needed3.text = ItemCatalog.getItemName (neededItems [2]) + " (" + aquiredAmounts[2] + "/" +  + neededAmounts [2]+")"; 
		}
		if (neededAmounts [3] != 0) {
			needed4.text = ItemCatalog.getItemName (neededItems [3]) + " (" + aquiredAmounts[3] + "/" +  + neededAmounts [3]+")"; 
		}
		if (neededAmounts [4] != 0) {
			needed5.text = ItemCatalog.getItemName (neededItems [4]) + " (" + aquiredAmounts[4] + "/" +  + neededAmounts [4]+")"; 
		}
		if (neededAmounts [5] != 0) {
			needed6.text = ItemCatalog.getItemName (neededItems [5]) + " (" + aquiredAmounts[5] + "/" +  + neededAmounts [5]+")"; 
		}
		name.text = BuildingCatalog.getBuildingName (buildingID);
		buildingDisplay.sprite = ItemCatalog.itemSpriteSheet [ItemCatalog.getItemImage (BuildingCatalog.getBuildingItemID (buildingID))];
	}
	public void UpdateMaterials(){
		if (!(aquiredAmounts [0] >= neededAmounts [0] && aquiredAmounts [1] >= neededAmounts [1] && aquiredAmounts [2] >= neededAmounts [2] && aquiredAmounts [3] >= neededAmounts [3] && aquiredAmounts [4] >= neededAmounts [4] && aquiredAmounts [5] >= neededAmounts [5])) {
			cameraScript.removeFromChunk ("buildingsite", baseX, baseY);
			cameraScript.addToChunk (cameraScript.MainChunkPath + CameraLocation.chunkLat + " " + CameraLocation.chunkLon, "type:buildingsite;buildingid:" + buildingID + ";x:" + baseX + ";y:" + baseY + ";item:0104;part1:" + neededItems [0] + "x" + aquiredAmounts [0] + ";part2:" + neededItems [1] + "x" + aquiredAmounts [1] + ";part3:" + neededItems [2] + "x" + aquiredAmounts [2] + ";part4:" + neededItems [3] + "x" + aquiredAmounts [3] + ";part5:" + neededItems [4] + "x" + aquiredAmounts [4] + ";part6:" + neededItems [5] + "x" + aquiredAmounts [5]);
		}
		if(aquiredAmounts [0] >= neededAmounts[0]&&aquiredAmounts [1] >= neededAmounts[1]&&aquiredAmounts [2] >= neededAmounts[2]&&aquiredAmounts [3] >= neededAmounts[3]&&aquiredAmounts [4] >= neededAmounts[4]&&aquiredAmounts [5] >= neededAmounts[5]){
			Build ();
		}
	}
	public void Build(){
		cameraScript.removeFromChunk ("buildingsite",baseX,baseY);
		if(buildingID == "0000"){
			Achievements.UnlockAchievement (GPGSIds.achievement_pack_rat);
			cameraScript.addToChunk (cameraScript.MainChunkPath + CameraLocation.chunkLat + " " + CameraLocation.chunkLon, "type:building;buildingid:" + BuildingCatalog.getBuildingItemID(buildingID) + ";x:" + baseX + ";y:" + baseY + ";item:"+BuildingCatalog.getBuildingItemID(buildingID)+";slot1:null;slot2:null;slot3:null;slot4:null;slot5:null;slot6:null;slot7:null;slot8:null;slot9:null");
		}
		if(buildingID == "0001"){
			Achievements.UnlockAchievement (GPGSIds.achievement_home_sweet_home);
			cameraScript.addToChunk (cameraScript.MainChunkPath + CameraLocation.chunkLat + " " + CameraLocation.chunkLon, "type:building;buildingid:" + BuildingCatalog.getBuildingItemID(buildingID) + ";x:" + baseX + ";y:" + baseY + ";item:"+BuildingCatalog.getBuildingItemID(buildingID)+";lastused:"+System.DateTime.Now.Year + (System.DateTime.Now.Month * 40) + System.DateTime.Now.Day);
		}
		GameObject item = (GameObject)Instantiate (Resources.Load ("Placeable/GeneralPlaced"), new Vector3 (baseObject.transform.position.x, baseObject.transform.position.y, 9), Quaternion.identity);
		GeneralPlacedItem itemScript = item.GetComponent<GeneralPlacedItem> ();
		itemScript.item = BuildingCatalog.getBuildingItemID(buildingID);
		itemScript.buildingID = buildingID;
		itemScript.r = baseY;
		itemScript.c = baseX;
		GameObject.Destroy (baseObject);
		cameraScript.inventoryScript.closeWindows ();
	}
	public void Reset(){
		buildingID = "";
		neededItems = new string[]{"","","","","",""};
		neededAmounts = new int[]{0,0,0,0,0,0};
		aquiredAmounts = new int[]{0,0,0,0,0,0};
	}
}
