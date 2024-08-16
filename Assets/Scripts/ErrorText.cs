using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorText : MonoBehaviour {

	public GameObject viewPoint;
	private float time = 3.0f;

	// Use this for initialization
	void Start () {
		viewPoint = GameObject.Find ("ViewPoint");
	}
	void Update(){
		time -= 1.0f * Time.deltaTime;
		if(time < 0){
			GameObject.Destroy (gameObject);
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 tempPos = transform.position;
		tempPos.x = viewPoint.transform.position.x;
		tempPos.y = viewPoint.transform.position.y+.31f;
		transform.position = tempPos;
	}
}
