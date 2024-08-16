using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetSummonScript : MonoBehaviour {

	public Transform topCapsule;
	public Transform bottomCapsule;
	public SpriteRenderer petRender;
	public TextMesh petName;
	public Inventory inventoryScript;
	private bool summoned;
	private float timeSummoned = 0.0f;
	public GameObject summonButton;
	public string petID;

	public static string[] pets = new string[]{
		"0002",
		"0003",
		"0004",
		"0005",
		"0006",
		"0007",
		"0008",
		"0009",
		"0010",
		"0011"
	};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(summoned){
			inventoryScript.blockExit = true;
			petRender.sprite = PetCatalog.petSpriteSheet[PetCatalog.getPetImage(petID,0,0)];
			petName.text = PetCatalog.getPetName (petID);
			timeSummoned += 1.0f * Time.deltaTime;
			Vector3 tempPos = topCapsule.transform.localPosition;
			tempPos.y = Mathf.Lerp (tempPos.y,0.8f,5.0f*Time.deltaTime);
			topCapsule.transform.localPosition = tempPos;
			Vector3 tempPos2 = bottomCapsule.transform.localPosition;
			tempPos2.y = Mathf.Lerp (tempPos2.y,-0.8f,5.0f*Time.deltaTime);
			bottomCapsule.transform.localPosition = tempPos2;
		}
		if(timeSummoned > 5.0f){
			summonButton.SetActive (true);
			summoned = false;
			inventoryScript.blockExit = false;
			timeSummoned = 0.0f;
			Vector3 tempPos = topCapsule.transform.localPosition;
			tempPos.y = 0;
			topCapsule.transform.localPosition = tempPos;
			Vector3 tempPos2 = bottomCapsule.transform.localPosition;
			tempPos2.y = 0;
			bottomCapsule.transform.localPosition = tempPos2;
			inventoryScript.closeWindows ();
			inventoryScript.needRelease = false;
		}
	}
	public void SummonScript(){
		if (!hasAllPets ()) {
			if (AccountInfo.accountCoins >= 100) {
				AccountInfo.spendCoins (100);
				Achievements.IncrementAchievement (GPGSIds.achievement_super_summoner,1);
				Random.InitState ((int)System.DateTime.Now.Ticks);
				petID = getNewPet ();
				PetInfo.UnlockPet (petID);
				summonButton.SetActive (false);
				summoned = true;
			} else {
				GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0,0, -20), Quaternion.identity);
				TextMesh errorText = error.GetComponent<TextMesh> ();
				errorText.text = "NOT ENOUGH\nMONEY";
				inventoryScript.closeWindows ();
			}
		} else {
			GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0,0, -20), Quaternion.identity);
			TextMesh errorText = error.GetComponent<TextMesh> ();
			errorText.text = "NO MORE PETS\nTO OBTAIN";
			inventoryScript.closeWindows ();
		}
	}
	public bool hasAllPets(){
		for(int i = 0;i<pets.Length;i++){
			if(!PetInfo.HasPet(pets[i])){
				return false;
			}
		}
		return true;
	}
	public string getNewPet(){
		string[] newPets = reshuffle (pets);
		for(int i = 0;i<newPets.Length;i++){
			if(!PetInfo.HasPet(newPets[i])){
				return newPets[i].ToString();
			}
		}
		return "0000";
	}
	public static string[] reshuffle(string[] pets)
	{
		// Knuth shuffle algorithm :: courtesy of Wikipedia :)
		for (int t = 0; t < pets.Length; t++ )
		{
			string tmp = pets[t];
			int r = UnityEngine.Random.Range(t, pets.Length);
			pets[t] = pets[r];
			pets[r] = tmp;
		}
		return pets;
	}
}
