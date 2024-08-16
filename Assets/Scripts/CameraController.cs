using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject landCamera;
	public GameObject shopCamera;
	public GameObject dungeonCamera;

	public static int camera;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(camera==0){
			landCamera.GetComponent<Camera> ().enabled = true;
			shopCamera.GetComponent<Camera> ().enabled = false;
			dungeonCamera.GetComponent<Camera> ().enabled = false;
		}
		if(camera==1){
			landCamera.GetComponent<Camera> ().enabled = false;
			shopCamera.GetComponent<Camera> ().enabled = true;
			dungeonCamera.GetComponent<Camera> ().enabled = false;
		}
		if(camera==2){
			landCamera.GetComponent<Camera> ().enabled = false;
			shopCamera.GetComponent<Camera> ().enabled = false;
			dungeonCamera.GetComponent<Camera> ().enabled = true;
		}
	}
}
