using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceScript : MonoBehaviour {

	public SpriteRenderer fishRenderer;
	public SpriteRenderer entityRenderer;
	public int fishSlot;
	public string entitySlot;
	public TextMesh placeText;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Controller.currentPlaceID!="nointernetid"&&Controller.currentPlaceID!="null"&&Controller.currentPlaceID!="none"){
			Random.InitState ((int)long.Parse (Controller.currentPlaceID));
			print (entitySlot);
			fishRenderer.sprite = FishCatalog.fishSpriteSheet[FishCatalog.getFishImage(Controller.cityFishID)];
			entityRenderer.sprite = EntityCatalog.entitySpriteSheet[EntityCatalog.getEntityImage(Controller.cityEntityID)];
			placeText.text = Controller.currentPlaceName + "\noption panel";
		}
	}
	public string FirstLetterToUpper(string str)
	{
		if (str == null)
			return null;

		if (str.Length > 1)
			return char.ToUpper(str[0]) + str.Substring(1);

		return str.ToUpper();
	}
}
