using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tempRot = transform.localEulerAngles;
		tempRot.y += 5.0f * Time.deltaTime;
		transform.localEulerAngles = tempRot;
	}
}
