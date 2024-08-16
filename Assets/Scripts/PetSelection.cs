using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetSelection : MonoBehaviour {

	public SpriteRenderer renderer;
	public Sprite petSprite;
	public string petID;
	public int pet;
	public string name;
	public TextMesh nameRender;
	public bool prevEnabled;
	public string[] myPets = new string[1000];
	public GameObject petViewScreen;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		UpdateBoxInfo ();
	}
	void UpdateBoxInfo(){
		myPets = PetInfo.GetPets ();
		petSprite = null;
		petID = "";
		name = "";
		renderer.sprite = null;
		petID = getID(myPets[pet+(9*CameraLocation.petPage)]);
		petSprite = PetCatalog.petSpriteSheet[PetCatalog.getPetImage(petID,0,0)];
		name = PetCatalog.getPetName (petID);
		if (petID != "") {
			renderer.sprite = petSprite;
			nameRender.text = name;
		} else {
			renderer.sprite = null;
			nameRender.text = "";
			if(pet == 0){
				if(CameraLocation.petPage>0){
					CameraLocation.petPage -= 1;
				}
			}
		}
	}
	public string getID(string line){
		if(line == null){
			return "";
		}
		string[] attributeList = line.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="pet"){
				return attribute[1];
			}
		}
		return "";
	}
}
