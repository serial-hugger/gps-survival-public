using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassArrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Input.compass.enabled = true;
		if(Controller.slot == "/toybox" && Controller.joyStick == true){
			gameObject.SetActive (false);
		}
	}

	// Update is called once per frame
	void Update () {
		Vector3 tempRot = transform.localEulerAngles;
		//tempRot.z = Mathf.Round(-Input.compass.trueHeading/45)*45;
		float offset = 10.0f;
		if(Input.compass.trueHeading > 337.5f+offset || Input.compass.trueHeading < 22.5f-offset){
			tempRot.z = 0;
		}
		if(Input.compass.trueHeading > 22.5f+offset && Input.compass.trueHeading < 67.5f-offset){
			tempRot.z = -45;
		}
		if(Input.compass.trueHeading > 67.5f+offset && Input.compass.trueHeading < 112.5f-offset){
			tempRot.z = -90;
		}
		if(Input.compass.trueHeading > 112.5f+offset && Input.compass.trueHeading < 157.5f-offset){
			tempRot.z = -135;
		}
		if(Input.compass.trueHeading > 157.5f+offset && Input.compass.trueHeading < 202.5f-offset){
			tempRot.z = -180;
		}
		if(Input.compass.trueHeading > 202.5f+offset && Input.compass.trueHeading < 247.5f-offset){
			tempRot.z = -225;
		}
		if(Input.compass.trueHeading > 247.5f+offset && Input.compass.trueHeading < 292.5f-offset){
			tempRot.z = -270;
		}
		if(Input.compass.trueHeading > 292.5f+offset && Input.compass.trueHeading < 337.5f-offset){
			tempRot.z = -315;
		}
		transform.localEulerAngles = tempRot;
		//print (Input.compass.trueHeading);
	}
}
