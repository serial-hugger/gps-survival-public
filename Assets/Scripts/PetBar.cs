using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetBar : MonoBehaviour {

	public string stat;
	public PetScreenView viewScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(stat == "fullness"){
			transform.localScale = new Vector3 (((float)PetInfo.GetPetFullness(viewScript.petID)/86400f), 1, 1);
		}
		if(stat == "happiness"){
			transform.localScale = new Vector3 (((float)PetInfo.GetPetHappiness(viewScript.petID)/86400f), 1, 1);
		}
		if(stat == "fitness"){
			transform.localScale = new Vector3 (((float)PetInfo.GetPetFitness(viewScript.petID)/86400f), 1, 1);
		}
	}
}
