using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizing : MonoBehaviour {

	public Camera myCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Controller.isPortrait()){
			//myCamera.orthographicSize = .7f;
		}
		if(Controller.isLandscape()){
			//myCamera.orthographicSize = .4f;
		}
	}
}
