using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeButton : MonoBehaviour {

	public Inventory inventoryScript;

	public int skinChange;
	public int hairChange;
	public int shirtChange;
	public int pantsChange;

	public bool changeToMale;
	public bool changeToFemale;

	public bool finished;

	// Use this for initialization
	void Start () {
		
	}
	void Update(){
		inventoryScript.blockExit = true;
	}
	public void ButtonPress(){
		Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (transform.position.x,transform.position.y, 0), Quaternion.identity);
		AccountInfo.skin += skinChange;
		AccountInfo.hair += hairChange;
		AccountInfo.shirt += shirtChange;
		AccountInfo.pants += pantsChange;
		if(AccountInfo.skin<0){
			AccountInfo.skin = AccountInfo.headSpriteSheet.Length-1;
		}
		if(AccountInfo.skin>AccountInfo.headSpriteSheet.Length-1){
			AccountInfo.skin = 0;
		}
		if(AccountInfo.hair<0){
			AccountInfo.hair = AccountInfo.maleHairSpriteSheet.Length-1;
		}
		if(AccountInfo.hair>AccountInfo.maleHairSpriteSheet.Length-1){
			AccountInfo.hair = 0;
		}
		if(AccountInfo.shirt<0){
			AccountInfo.shirt = AccountInfo.shirtSpriteSheet.Length-1;
		}
		if(AccountInfo.shirt>AccountInfo.shirtSpriteSheet.Length-1){
			AccountInfo.shirt = 0;
		}
		if(AccountInfo.pants<0){
			AccountInfo.pants = AccountInfo.pantsSpriteSheet.Length-1;
		}
		if(AccountInfo.pants>AccountInfo.pantsSpriteSheet.Length-1){
			AccountInfo.pants = 0;
		}
		if(changeToMale){
			AccountInfo.gender = 0;
		}
		if(changeToFemale){
			AccountInfo.gender = 1;
		}
		if(finished){
			AccountInfo.firstCustomize = true;
		}
		AccountInfo.saveInfo ();
		if(finished){
			inventoryScript.blockExit = false;
			inventoryScript.closeWindows ();
		}
	}
}
