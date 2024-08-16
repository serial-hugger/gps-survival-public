using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeildOfInteraction : MonoBehaviour {

	public Transform player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 tempPos = transform.position;
		tempPos.x = (float)((int)((player.position.x)/.1f)*.1f)+.05f;
		tempPos.y = (float)((int)((player.position.y+.1f)/.1f)*.1f)-.05f;
		transform.position = tempPos;
	}
}
