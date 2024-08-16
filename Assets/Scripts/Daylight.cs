using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daylight : MonoBehaviour {

	public Material daylight;
	public MeshRenderer rend;

	// Use this for initialization
	void Start () {
		rend.sortingLayerName = "Front";
	}
	
	// Update is called once per frame
	void Update () {
		if(System.DateTime.Now.Hour >= 7 && System.DateTime.Now.Hour < 10){
			Color tempColor = daylight.color;
			tempColor.a = .2f;
			daylight.color = tempColor;
		}
		if(System.DateTime.Now.Hour >= 10 && System.DateTime.Now.Hour < 16){
			Color tempColor = daylight.color;
			tempColor.a = 0;
			daylight.color = tempColor;
		}
		if(System.DateTime.Now.Hour >= 16 && System.DateTime.Now.Hour < 19){
			Color tempColor = daylight.color;
			tempColor.a = .2f;
			daylight.color = tempColor;
		}
		if(System.DateTime.Now.Hour >= 19 || System.DateTime.Now.Hour < 7){
			Color tempColor = daylight.color;
			tempColor.a = .4f;
			daylight.color = tempColor;
		}
	}
}
