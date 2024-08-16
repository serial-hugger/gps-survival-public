using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignArrow : MonoBehaviour {

	public int designScroll;
	public BuildingScreen designScript;

	public void Pressed(){
		if((designScript.buildingSlot + designScroll)>=0&&(designScript.buildingSlot + designScroll)<BuildingCatalog.buildings.Length){
			designScript.buildingSlot += designScroll;
		}
	}
}
