using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionText : MonoBehaviour {

	public TextMesh text;
	public string type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(type=="sound"){
			text.text = "sound:" + AccountInfo.soundOption;
		}
		if(type=="internet"){
			text.text = "internet:" + AccountInfo.internetOption;
		}
	}
}
