using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScript : MonoBehaviour {

	public Sprite tunnel1;
	public Sprite tunnel2;
	public Sprite tunnel3;
	public Sprite tunnel4;
	public Sprite tunnel5;
	public Transform tunnelGate;
	public TextMesh enterDungeon;
	public SpriteRenderer renderer;
	public int tunnelImage;
	public CameraLocation cameraScript;
	public static GeneralPlacedItem dungeonScript;
	public static float animateNext = 10.0f;
	public static bool animate = false;
	public static bool started;
	public static bool firstTouched;
	public static int currentPosition;
	public static int dungeonLength;
	public TextMesh positionText;
	public SpriteRenderer heartContainer;
	public Sprite heart0;
	public Sprite heart1;
	public Sprite heart2;
	public Sprite heart3;
	public static float timeTillNext;
	public static int randomizer;
	public int scenarioSlot;
	public GameObject scenarioBubble;
	public static int health = 3;
	public TextMesh scenario;
	public TextMesh outcome;
	public int option1Slot;
	public GameObject option1Bubble;
	public TextMesh option1Text;
	public int option2Slot;
	public GameObject option2Bubble;
	public TextMesh option2Text;
	public int option3Slot;
	public GameObject option3Bubble;
	public TextMesh option3Text;
	public int rightOption;
	public static bool finished;
	public SpriteRenderer scenarioImage;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(finished){
			cameraScript.removeFromChunk ("placeable",dungeonScript.c,dungeonScript.r);
			cameraScript.addToChunk (cameraScript.MainChunkPath + CameraLocation.chunkLat + " " + CameraLocation.chunkLon,"type:placeable;x:" + dungeonScript.c + ";y:" + dungeonScript.r + ";item:0125;completed:true;lastused:"+System.DateTime.Now.Ticks);
			Random.InitState (randomizer);
			cameraScript.spawnRandomItem (dungeonScript.gameObject.transform.position.x + .05f, dungeonScript.gameObject.transform.position.y - .05f, DungeonCatalog.gems[Random.Range(0,DungeonCatalog.gems.Length)], 100);
			Achievements.IncrementAchievement (GPGSIds.achievement_dungeon_raider,1);
			Reset (randomizer+(int)(System.DateTime.Now.Ticks/(System.TimeSpan.TicksPerDay*7)));
		}
		if(!started){
			positionText.text = "---";
		}
		if(health > 3){
			health = 3;
		}
		if(health == 3){
			heartContainer.sprite = heart3;
		}
		if(health == 2){
			heartContainer.sprite = heart2;
		}
		if(health == 1){
			heartContainer.sprite = heart1;
		}
		if(health == 0){
			heartContainer.sprite = heart0;
		}
		if(CameraController.camera == 2 && health <= 0 && timeTillNext < 10){
			CameraController.camera = 0;
			Reset (randomizer+(int)(System.DateTime.Now.Ticks/(System.TimeSpan.TicksPerDay*7)));
		}
		if(Input.touchCount>0&&!CameraLocation.needToRelease){
			firstTouched = true;
		}
		if(CameraController.camera == 2){
			//TOUCHES
			for (int i = 0; i < Input.touchCount; i++) {
				Vector3 test = Camera.main.ScreenToWorldPoint (Input.GetTouch(i).position);
				RaycastHit hit; 
				Physics.Raycast (test, Vector3.forward,out hit);
				if(!CameraLocation.needToRelease && timeTillNext<0 && started){
					if(hit.collider != null && hit.collider.tag == "DungeonOption"){
						currentPosition += 1;
						if(hit.transform.name == "Option1"){
							if(option1Slot>-1){
								timeTillNext = 20;
								if (rightOption == 1) {
									outcome.text = DungeonCatalog.getOptionSuccessText (option1Slot);
									if(DungeonCatalog.getOptionSuccessEvent(option1Slot)=="heal"){
										health += 1;
									}

									if(DungeonCatalog.getOptionSuccessEvent(option1Slot)=="advance"){
										currentPosition += 1;
									}
									if(DungeonCatalog.getOptionSuccessEvent(option1Slot)=="doubleadvance"){
										currentPosition += 2;
									}
									if(DungeonCatalog.getOptionSuccessEvent(option1Slot)=="tripleadvance"){
										currentPosition += 3;
									}
								} else {
									outcome.text = DungeonCatalog.getOptionFailText (option1Slot);
									if(DungeonCatalog.getOptionFailEvent(option1Slot)=="damage"){
										health -= 1;
									}
									if(DungeonCatalog.getOptionFailEvent(option1Slot)=="doubledamage"){
										health -= 2;
									}
									if(DungeonCatalog.getOptionFailEvent(option1Slot)=="tripledamage"){
										health -= 3;
									}
								}
							}
						}
						if(hit.transform.name == "Option2"){
							if(option2Slot>-1){
								timeTillNext = 20;
								if (rightOption == 2) {
									outcome.text = DungeonCatalog.getOptionSuccessText (option2Slot);
									if(DungeonCatalog.getOptionSuccessEvent(option2Slot)=="heal"){
										health += 1;
									}
									if(DungeonCatalog.getOptionSuccessEvent(option2Slot)=="advance"){
										currentPosition += 1;
									}
									if(DungeonCatalog.getOptionSuccessEvent(option2Slot)=="doubleadvance"){
										currentPosition += 2;
									}
									if(DungeonCatalog.getOptionSuccessEvent(option2Slot)=="tripleadvance"){
										currentPosition += 3;
									}
								} else {
									outcome.text = DungeonCatalog.getOptionFailText (option2Slot);
									if(DungeonCatalog.getOptionFailEvent(option2Slot)=="damage"){
										health -= 1;
									}
									if(DungeonCatalog.getOptionFailEvent(option2Slot)=="doubledamage"){
										health -= 2;
									}
									if(DungeonCatalog.getOptionFailEvent(option2Slot)=="tripledamage"){
										health -= 3;
									}
								}
							}
						}
						if(hit.transform.name == "Option3"){
							if(option3Slot>-1){
								timeTillNext = 20;
								if (rightOption == 3) {
									outcome.text = DungeonCatalog.getOptionSuccessText (option3Slot);
									if(DungeonCatalog.getOptionSuccessEvent(option3Slot)=="heal"){
										health += 1;
									}
									if(DungeonCatalog.getOptionSuccessEvent(option3Slot)=="advance"){
										currentPosition += 1;
									}
									if(DungeonCatalog.getOptionSuccessEvent(option3Slot)=="doubleadvance"){
										currentPosition += 2;
									}
									if(DungeonCatalog.getOptionSuccessEvent(option3Slot)=="tripleadvance"){
										currentPosition += 3;
									}
								} else {
									outcome.text = DungeonCatalog.getOptionFailText (option3Slot);
									if(DungeonCatalog.getOptionFailEvent(option3Slot)=="damage"){
										health -= 1;
									}
									if(DungeonCatalog.getOptionFailEvent(option3Slot)=="doubledamage"){
										health -= 2;
									}
									if(DungeonCatalog.getOptionFailEvent(option3Slot)=="tripledamage"){
										health -= 3;
									}
								}
							}
						}
					}
				}
			}
		}
		if(animate){
			animateNext -= 50f * Time.deltaTime;
		}
		if (firstTouched) {
			timeTillNext -= 3f * Time.deltaTime;
			Color tempColor = enterDungeon.color;
			tempColor.a = 0;
			enterDungeon.color = tempColor;
		} else {
			Color tempColor = enterDungeon.color;
			tempColor.a = 1;
			enterDungeon.color = tempColor;
		}
		if(!started){
			if (timeTillNext < 0) {
				animate = true;
				started = true;
				timeTillNext = 10.0f;
			}
			hideOptions ();
		}
		if(started){
			if (timeTillNext < 0) {
				animate = false;
				showOptions ();
			} else if (timeTillNext < 10) {
				animate = true;
				outcome.text = "";
			} else {
				hideOptions ();
			}
		}
		if(animateNext<0){
			animateNext = 10.0f;
			tunnelImage += 1;
		}
		if(tunnelImage>4){
			tunnelImage = 0;
		}
		if(tunnelImage == 0){
			renderer.sprite = tunnel1;
		}
		if(tunnelImage == 1){
			renderer.sprite = tunnel2;
		}
		if(tunnelImage == 2){
			renderer.sprite = tunnel3;
		}
		if(tunnelImage == 3){
			renderer.sprite = tunnel4;
		}
		if(tunnelImage == 4){
			renderer.sprite = tunnel5;
		}
		if (firstTouched) {
			Vector3 tempPos = tunnelGate.position;
			tempPos.y += 5.0f * Time.deltaTime;
			tunnelGate.position = tempPos;
		} else {
			Vector3 tempPos = tunnelGate.position;
			tempPos.y = -0.6f;
			tunnelGate.position = tempPos;
		}
	}
	public static void Reset(int random){
		randomizer = random;
		animate = false;
		started = false;
		timeTillNext = 10.0f;
		firstTouched = false;
		currentPosition = 1;
		health = 3;
		finished = false;
	}
	public void showOptions(){
		Random.InitState (randomizer);
		dungeonLength = Random.Range (8,16);
		if(currentPosition > dungeonLength && CameraController.camera == 2){
			finished = true;
			dungeonScript.complete = true;
			CameraController.camera = 0;
		}
		positionText.text = currentPosition + " of " + dungeonLength;
		Random.InitState (randomizer+currentPosition+343);
		var randomChance = Random.Range (0,100);
		scenarioSlot = Random.Range (0,DungeonCatalog.scenarios.Length);
		var scenarioID = DungeonCatalog.getScenarioID (scenarioSlot);
		option1Slot = DungeonCatalog.getOptionSlot (0,scenarioID);
		option2Slot = DungeonCatalog.getOptionSlot (1,scenarioID);
		option3Slot = DungeonCatalog.getOptionSlot (2,scenarioID);
		var last = 0;
		var option1Low = 0;
		var option1High = 0;
		var option2Low = 0;
		var option2High = 0;
		var option3Low = 0;
		var option3High = 0;
		scenarioBubble.SetActive (true);
		scenario.text = DungeonCatalog.getScenarioText (scenarioID);
		scenarioImage.sprite = DungeonCatalog.scenarioSpriteSheet[DungeonCatalog.getScenarioImage(scenarioID)];
		if (option1Slot > -1) {
			option1Text.text = DungeonCatalog.getOptionText (option1Slot);
			option1Low = last;
			option1High = last + DungeonCatalog.getOptionChance (option1Slot);
			last = option1High;
			option1Bubble.SetActive (true);
		} else {
			option1Bubble.SetActive (false);
			option1Text.text = "";
		}
		if(option2Slot>-1){
			option2Text.text = DungeonCatalog.getOptionText (option2Slot);
			option2Low = last;
			option2High = last + DungeonCatalog.getOptionChance (option2Slot);
			last = option2High;
			option2Bubble.SetActive (true);
		} else {
			option2Bubble.SetActive (false);
			option2Text.text = "";
		}
		if(option3Slot>-1){
			option3Text.text = DungeonCatalog.getOptionText (option3Slot);
			option3Low = last;
			option3High = last + DungeonCatalog.getOptionChance (option3Slot);
			last = option3High;
			option3Bubble.SetActive (true);
		} else {
			option3Bubble.SetActive (false);
			option3Text.text = "";
		}
		if(randomChance >= option1Low && randomChance <= option1High){
			rightOption = 1;
		}
		if(randomChance > option2Low && randomChance <= option2High){
			rightOption = 2;
		}
		if(randomChance > option3Low && randomChance <= option3High){
			rightOption = 3;
		}
		if (PetInfo.IsPetHappy ("0011")) {
			if(rightOption != 1){
				if(Random.Range(0,1000)>750){
					option1Bubble.SetActive (false);
				}
			}
			if(rightOption != 2){
				if(Random.Range(0,1000)>750){
					option2Bubble.SetActive (false);
				}
			}
			if(rightOption != 3){
				if(Random.Range(0,1000)>750){
					option3Bubble.SetActive (false);
				}
			}
		}
	}
	public void hideOptions(){
		scenario.text = "";
		scenarioImage.sprite = null;
		scenarioBubble.SetActive (false);
		option1Text.text = "";
		option1Bubble.SetActive (false);
		option2Text.text = "";
		option2Bubble.SetActive (false);
		option3Text.text = "";
		option3Bubble.SetActive (false);
	}
}
