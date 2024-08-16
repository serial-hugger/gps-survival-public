using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftButton : MonoBehaviour {

	public string buttonType;
	public Inventory inventoryScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ButtonPress(){
		if(buttonType == "gms"){
			Application.OpenURL ("https://play.google.com/store/apps/details?id=com.tankenka.gms");
			PetInfo.UnlockPet ("0000");
			PetInfo.UnlockCostume ("0000",1);
			PetInfo.UnlockCostume ("0000",2);
			PetInfo.UnlockCostume ("0000",3);
			PetInfo.UnlockCostume ("0000",4);
		}
		if(buttonType == "roly"){
			Application.OpenURL ("https://play.google.com/store/apps/details?id=com.spaddlewit.rolypolyputt");
			PetInfo.UnlockPet ("0001");
			PetInfo.UnlockCostume ("0001",1);
			PetInfo.UnlockCostume ("0001",2);
			PetInfo.UnlockCostume ("0001",3);
			PetInfo.UnlockCostume ("0001",4);
		}
		inventoryScript.closeWindows ();
		inventoryScript.needRelease = false;
		CameraLocation.needToRelease = false;
	}
}
