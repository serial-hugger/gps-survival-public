using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {

	public string button;
	public TextMesh text;
	public Collider collider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(button=="slot2"){
			if (Controller.extraSavesPurchased) {
				text.text = "slot 2";
				collider.enabled = true;
			} else {
				text.text = "locked";
				collider.enabled = false;
			}
		}
		if(button=="slot3"){
			if (Controller.extraSavesPurchased) {
				text.text = "slot 3";
				collider.enabled = true;
			} else {
				text.text = "locked";
				collider.enabled = false;
			}
		}
		if(button=="toyboxsetup"){
			if (Controller.toyBoxPurchased) {
				text.text = "toybox";
				collider.enabled = true;
			} else {
				text.text = "locked";
				collider.enabled = false;
			}
		}
		if(button == "seed"){
			text.text = "seed:" + Controller.mainSeed;
		}
		if(button == "joystick"){
			text.text = "joypad:" + Controller.joyStick;
		}
		if(button == "chunkborders"){
			text.text = "border chunks:" + !Controller.disableBorderChunks;
		}
	}
}
