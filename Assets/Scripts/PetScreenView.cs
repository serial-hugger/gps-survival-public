using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetScreenView : MonoBehaviour {

	public string petID = "";
	public int costume = 0;
	public SpriteRenderer sprite;
	public TextMesh bannerText;
	public TextMesh abilityText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void UpdateInfo(){
		sprite.sprite = null;
		sprite.sprite = PetCatalog.petSpriteSheet[PetCatalog.getPetImage(petID,0,costume)];
		abilityText.text = "Ability:\n"+PetCatalog.getPetAbility (petID);
		if (PetInfo.HasPetCostume (petID, costume)) {
			bannerText.text = "SELECT";
		} else {
			bannerText.text = "BUY: 500";
		}
	}
	public void Reset(){
		petID = "";
		costume = 0;
	}
}
