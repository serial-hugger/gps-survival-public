using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingScript : MonoBehaviour {

	public CameraLocation cameraScript;
	public Inventory inventoryScript;
	public GameObject player;
	public GameObject pole;
	public float playerZ;
	public float poleZ;
	public int fishing = 0;
	public WaterSpotScript waterSpot1;
	public WaterSpotScript waterSpot2;
	public WaterSpotScript waterSpot3;
	public string localFish;
	public Collider reeling;
	public bool catchable;
	public float timeTillEscape;
	public bool trash;

	// Use this for initialization
	void Start () {
		playerZ = 10.0f;
		poleZ = 40.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(catchable){
			timeTillEscape -= 1.0f * Time.deltaTime;
			if(timeTillEscape < 0.0f){
				inventoryScript.closeWindows ();
			}
		}
		Random.InitState (CameraLocation.currentChunkRandom);
		localFish = FishCatalog.getFishID(FishCatalog.normalFishIndexes[Random.Range (0,FishCatalog.normalFishIndexes.Length)]);
		if ((Controller.currentPlaceID != "nointernetid" && Controller.currentPlaceID != "null" && Controller.currentPlaceID != "none")) {
			Random.InitState ((int)long.Parse(Controller.currentPlaceID));
			localFish = FishCatalog.getFishID(FishCatalog.rareFishIndexes[Random.Range (0,FishCatalog.rareFishIndexes.Length)]);
		}else if(Random.Range(0,5000)>4500){
			localFish = FishCatalog.getFishID(FishCatalog.rareFishIndexes[Random.Range (0,FishCatalog.rareFishIndexes.Length)]);
		}
		Vector3 tempPlayer = player.transform.localEulerAngles;
		tempPlayer.z = Mathf.SmoothStep (tempPlayer.z,playerZ,10.0f * Time.deltaTime);
		player.transform.localEulerAngles = tempPlayer;
		Vector3 tempPole = pole.transform.localEulerAngles;
		tempPole.z = Mathf.SmoothStep (tempPole.z,poleZ,10.0f * Time.deltaTime);
		pole.transform.localEulerAngles = tempPole;
	}
	public void Reset(){
		trash = false;
		catchable = false;
		timeTillEscape = FishCatalog.getFishSpeed (localFish);
		reeling.enabled = false;
		playerZ = 10.0f;
		poleZ = 40.0f;
		fishing = 0;
		Vector3 tempPlayer = player.transform.localEulerAngles;
		tempPlayer.z = 0.0f;
		player.transform.localEulerAngles = tempPlayer;
		Vector3 tempPole = pole.transform.localEulerAngles;
		tempPole.z = 0.0f;
		pole.transform.localEulerAngles = tempPole;
	}
}
