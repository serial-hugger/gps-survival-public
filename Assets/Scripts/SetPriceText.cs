using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPriceText : MonoBehaviour {

	public string textBefore;
	public string productID;
	public TextMesh text;
	public Purchaser purchaseScript;

	// Use this for initialization
	void Start () {
		purchaseScript = GameObject.Find ("_Controller").GetComponent<Purchaser>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = textBefore + purchaseScript.getPrice (productID);
	}
}
