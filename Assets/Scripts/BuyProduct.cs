using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyProduct : MonoBehaviour {

	public string product;
	public Purchaser purchaseScript;

	// Use this for initialization
	void Start () {
		purchaseScript = GameObject.Find ("_Controller").GetComponent<Purchaser>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Purchase(){
		if(product == "100coins"){
			purchaseScript.Buy100Coins ();
			return;
		}
		if(product == "500coins"){
			purchaseScript.Buy500Coins ();
			return;
		}
		if(product == "1000coins"){
			purchaseScript.Buy1000Coins ();
			return;
		}
		if(product == "5000coins"){
			purchaseScript.Buy5000Coins ();
			return;
		}
		if(product == "10000coins"){
			purchaseScript.Buy10000Coins ();
			return;
		}
	}
}
