using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIDTextSetter : MonoBehaviour {

	public TextMesh id;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		id.text = AccountInfo.playerID;
	}
}
