using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPet : MonoBehaviour {

	public SpriteRenderer renderer;

	// Update is called once per frame
	void Update () {
		if (Controller.currentPet != "") {
			renderer.sprite = PetCatalog.petSpriteSheet [PetCatalog.getPetImage (Controller.currentPet, 0, Controller.currentPetCostume)];
		} else {
			renderer.sprite = null;
		}
	}
}
