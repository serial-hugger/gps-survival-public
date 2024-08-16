using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScreen : MonoBehaviour {

	public int buildingSlot = 0;
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
	public string[] neededItems = new string[]{"","","","","",""};
	public int[] neededAmounts = new int[]{0,0,0,0,0,0};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(BuildingCatalog.getBuildingID(buildingSlot)!="null"){
			UpdateBoxInfo ();
		}
	}
	public void UpdateBoxInfo(){
		needed1.text = "";
		needed2.text = "";
		needed3.text = "";
		needed4.text = "";
		needed5.text = "";
		needed6.text = "";
		name.text = "";
		buildingDisplay.sprite = null;
		neededItems = BuildingCatalog.getNeededItems (buildingSlot);
		neededAmounts = BuildingCatalog.getNeededAmounts (buildingSlot);
		if (neededAmounts [0] != 0) {
			needed1.text = ItemCatalog.getItemName (neededItems [0]) + " x " + neededAmounts [0]; 
		}
		if (neededAmounts [1] != 0) {
			needed2.text = ItemCatalog.getItemName (neededItems [1]) + " x " + neededAmounts [1]; 
		}
		if (neededAmounts [2] != 0) {
			needed3.text = ItemCatalog.getItemName (neededItems [2]) + " x " + neededAmounts [2]; 
		}
		if (neededAmounts [3] != 0) {
			needed4.text = ItemCatalog.getItemName (neededItems [3]) + " x " + neededAmounts [3]; 
		}
		if (neededAmounts [4] != 0) {
			needed5.text = ItemCatalog.getItemName (neededItems [4]) + " x " + neededAmounts [4]; 
		}
		if (neededAmounts [5] != 0) {
			needed6.text = ItemCatalog.getItemName (neededItems [5]) + " x " + neededAmounts [5]; 
		}
		name.text = BuildingCatalog.getBuildingName (BuildingCatalog.getBuildingID (buildingSlot));
		buildingDisplay.sprite = ItemCatalog.itemSpriteSheet [ItemCatalog.getItemImage (BuildingCatalog.getBuildingItemID (BuildingCatalog.getBuildingID (buildingSlot)))];
	}
}
