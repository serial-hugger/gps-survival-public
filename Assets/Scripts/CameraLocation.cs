using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

public class CameraLocation : MonoBehaviour {

	public static string[] chunkBiomes = new string[]{"wood","plain","forest","swamp","flowergarden","farmland"};
	public static string[] rareChunkBiomes = new string[]{"beach","snowyforest","snowyplains","tundra"};
	public static string[] villagers = new string[]{"fisherman:fishing:3","lumberjack:lumber:6","farmer:farming:2","knight:battle:5","chef:food:0","construction:building:1","boy:default:4"};
	// Use this for initialization
	public Transform interactionArea;
	public static int chunkLat = 10;
	public static float actualChunkLat;
	public float lastChunkLat;
	public static int chunkLon = 10;
	public static float actualChunkLon;
	public float lastChunkLon;
	public string MainChunkPath;
	string ChunkDirectoryPath;
	public Transform player;
	float noLerpTime;
	float restTime;
	public static bool needToRelease = true;
	public Texture2D loadingText;
	public Texture2D gettingLocation;
	public Inventory inventoryScript;
	public QuestInfo questInfoScript;
	public static float loading;
	public static int selectedSlot = -1;
	public static int selectedRecipe = 0;
	public static int craftingPage = 0;
	public static int petPage = 0;
	public static bool lastCraftingPage;
	bool borderChunks = true;
	public static int currentChunkRandom = 0;
	public static string currentChunkBiome = "";
	public static float longSize = .001f;
	public static float longMultiple = 2000f;
	private float[] longSizes = new float[]{0.0045f,0.0025f,.0022f,0.0022f,0.0018f,0.0018f,0.0014f,0.0014f,0.001f};
	public static float lastLon;
	public static float lastLat;
	public bool regened;
	public static bool mapZoomedOut;
	public bool mapScrollable;

	public float timeTillFirstWeather = 1f;
	public float timeTillWeather = 0.0f;
	public static string currentWeatherCode = "000";
	public static float currentTemperature = 70.0f;

	public Transform lightRainEmitter;
	public Transform mediumRainEmitter;
	public Transform heavyRainEmitter;
	public Transform lightSnowEmitter;
	public Transform mediumSnowEmitter;
	public Transform heavySnowEmitter;
	public Transform clouds1;
	public Transform clouds2;
	public Transform clouds3;
	public static string mainWeather = "clear";

	public long lastWeatherUpdate = System.DateTime.Now.Ticks;

	public Camera mainCamera;

	public float nextJoy;

	void Awake(){
		needToRelease = true;
		inventoryScript.needRelease = true;
	}
	void Start () {
		Debug.Log(Application.persistentDataPath);
		needToRelease = true;
		SetLocation ();
		ChunkDirectoryPath = (Application.persistentDataPath + Controller.slot + "/chunks");
		MainChunkPath = (ChunkDirectoryPath + "/");
		if(!Controller.joyStick){
			StartCoroutine(StartLocationService(2f,2f));
		}
		if(!Directory.Exists(MainChunkPath)){
			Directory.CreateDirectory(MainChunkPath);
		}
		chunkSetup ();
	}
	
	// Update is called once per frame
	void Update () {
		if(loading > 0){
			mapZoomedOut = false;
		}
		if(nextJoy > 0){
			nextJoy -= 2f * Time.deltaTime;
		}
		if(Input.touchCount < 1){
			needToRelease = false;
		}
		if(restTime > 0){
			restTime -= 10f * Time.deltaTime;
		}
		if(loading > 0){
			loading -= 10f * Time.deltaTime;
			needToRelease = true;
		}
		//TOUCHES
		for (int i = 0; i < Input.touchCount; i++) {
			Debug.Log("TOUCH");
			Vector3 test = Camera.main.ScreenToWorldPoint (Input.GetTouch(i).position);
			Debug.Log(test);
			RaycastHit hit; 
			Physics.Raycast (test, Vector3.forward,out hit);
			Debug.Log(hit.transform.gameObject.tag);
			if(loading <= 0){
			if(hit.point.x<transform.position.x+.1f&&hit.point.x>transform.position.x-.1f&&hit.point.y<transform.position.y+.1f&&hit.point.y>transform.position.y-.1f && !needToRelease){
				if (!inventoryScript.window && (Inventory.checkHeldForTypeAndPwr ("food", 0)!=-1||Inventory.checkHeldForTypeAndPwr ("foodseed", 0)!=-1) && Skills.currentHealth<Skills.maxHealth) {
					needToRelease = true;
					Skills.currentHealth += ItemCatalog.getItemPrice (Inventory.getSlotItemID (Inventory.hotbarItem));
					if(Skills.currentHealth>Skills.maxHealth){
						Skills.currentHealth = Skills.maxHealth;
					}
					Inventory.removeItem(Inventory.getSlotItemID (Inventory.hotbarItem),1);
					Skills.saveStats ();
				}
				if (!inventoryScript.window && Inventory.checkHeldForTypeAndPwr ("gem", 0)!=-1 && Inventory.getItemCharges (Inventory.hotbarItem)<=0) {
					needToRelease = true;
					PlayerScript.spellUsed = Inventory.getSlotItemID (Inventory.hotbarItem);
					if (PetInfo.IsPetHappy ("0007")) {
						PlayerScript.secondsOfSpellLeft = 1800;
					} else {
						PlayerScript.secondsOfSpellLeft = 1800*1.5f;
					}
					Inventory.removeItem(Inventory.getSlotItemID (Inventory.hotbarItem),1);
				}
				if (!inventoryScript.window && Inventory.checkHeldForTypeAndPwr ("binocular", 0)!=-1) {
					needToRelease = true;
						mapZoomedOut = true;
						inventoryScript.window = true;
				}
			}
			if (hit.collider != null && hit.collider.tag == "Entity" && !needToRelease) {
				needToRelease = true;
				if(!inventoryScript.window && Inventory.checkHeldForTypeAndPwr ("sword", 0)!=-1 && Input.GetTouch(i).phase == TouchPhase.Began && Input.GetTouch(i).position.x > 150 && Input.GetTouch(i).position.x < Screen.width -150 && restTime <=0){
					Controller.lastWeaponUsed = Inventory.getSlotItemID (Inventory.hotbarItem);
					GameObject slash = (GameObject)Instantiate (Resources.Load ("Slash"), new Vector3 (player.position.x, player.position.y+.05f, 0), Quaternion.identity);
					SlashScript slashScript = slash.GetComponent<SlashScript> ();
					slashScript.inventoryScript = inventoryScript;
					Inventory.decreaseDurForSlot (Inventory.hotbarItem);
					restTime = 6.0f;
				}
			}
			if (hit.collider != null && hit.collider.tag == "InventoryBox" && !needToRelease) {
				needToRelease = true;
				ItemBox boxScript = hit.collider.GetComponent<ItemBox> ();
				if (boxScript.selected) {
					selectedSlot = -1;
				} else {
					if (selectedSlot != -1) {
						int slotTemp = selectedSlot;
						Inventory.swapBackpackSlots (slotTemp, boxScript.backpackItem);
						selectedSlot = -1;
					} else {
						selectedSlot = boxScript.backpackItem;
						boxScript.selected = true;
					}
				}
			}
			if (hit.collider != null && hit.collider.tag == "ItemTransferInventory" && !needToRelease) {
				needToRelease = true;
				ItemTransferBox boxScript = hit.collider.GetComponent<ItemTransferBox> ();
				if(Inventory.getSlotItemID(boxScript.backpackItem)!="0129"){
					boxScript.SendToChest (boxScript.backpackItem);
				}
			}
			if (hit.collider != null && hit.collider.tag == "ItemTransferChest" && !needToRelease) {
				needToRelease = true;
				ItemTransferBox boxScript = hit.collider.GetComponent<ItemTransferBox> ();
				boxScript.SendToInventory (boxScript.backpackItem);
			}
			if (hit.collider != null && hit.collider.transform.tag == "ExtraButton" && !needToRelease) {
				ExtraButton buttonScript = hit.collider.transform.GetComponent<ExtraButton> ();
					if(buttonScript.buttonType != "achievements" && buttonScript.buttonType != "leaderboards"){
					needToRelease = true;
				}
				buttonScript.ButtonPress ();
			}
			if (hit.collider != null && hit.collider.transform.tag == "CharacterCustomize" && !needToRelease) {
				CustomizeButton buttonScript = hit.collider.transform.GetComponent<CustomizeButton> ();
				needToRelease = true;
				buttonScript.ButtonPress ();
				}
			if (hit.collider != null && hit.collider.transform.tag == "SummonPetScreen" && !needToRelease) {
				inventoryScript.closeWindows ();
				needToRelease = true;
				inventoryScript.petSummonScreen.SetActive (true);
				inventoryScript.window = true;
				inventoryScript.needRelease = true;
			}
			if (hit.collider != null && hit.collider.transform.tag == "SummonPet" && !needToRelease) {
				needToRelease = true;
				inventoryScript.needRelease = true;
				PetSummonScript summonScript = hit.transform.parent.GetComponent<PetSummonScript> ();
				summonScript.SummonScript ();
			}
			if (hit.collider != null && hit.collider.transform.tag == "BuyProduct" && !needToRelease) {
				BuyProduct buttonScript = hit.collider.transform.GetComponent<BuyProduct> ();
				buttonScript.Purchase ();
				inventoryScript.closeWindows ();
				needToRelease = false;
				inventoryScript.needRelease = false;
			}
			if (hit.collider != null && hit.collider.transform.tag == "ShopsButton" && !needToRelease) {
				needToRelease = true;
				inventoryScript.needRelease = true;
				inventoryScript.closeWindows ();
				CameraController.camera = 1;
			}
			if (hit.collider != null && hit.collider.transform.tag == "DailyShopItem" && !needToRelease) {
				needToRelease = true;
				inventoryScript.needRelease = true;
				DailyShopItem buyScript = hit.transform.GetComponent<DailyShopItem> ();
				buyScript.BuyItem ();
			}
			if (hit.collider != null && hit.collider.transform.tag == "InformationButton" && !needToRelease) {
				needToRelease = true;
				inventoryScript.needRelease = true;
				Application.OpenURL ("https://www.wikidata.org/wiki/"+Controller.currentPlaceWiki);
				inventoryScript.closeWindows ();
			}
			if (hit.collider != null && hit.collider.transform.tag == "JoyStick"&&nextJoy<=0) {
				nextJoy = 1f;
				var multiple = Time.deltaTime;
				if(hit.point.x > hit.collider.transform.position.x+.05f){
					Controller.joyLon += 0.00002f;
				}
				if(hit.point.x < hit.collider.transform.position.x-.05f){
					Controller.joyLon -= 0.00002f;
				}
				if(hit.point.y > hit.collider.transform.position.y+.05f){
					Controller.joyLat += 0.00002f;
				}
				if(hit.point.y < hit.collider.transform.position.y-.05f){
					Controller.joyLat -= 0.00002f;
				}
			}
			if (hit.collider != null && hit.collider.transform.tag == "Trash" && !needToRelease) {
				needToRelease = true;
				if(selectedSlot!=-1){
					Inventory.changeQtyForSlot(selectedSlot,-1);
					if (Inventory.getItemQuantity (selectedSlot) <= 0) {
						selectedSlot = -1;
					}
				}
			}
			if (hit.collider != null && hit.collider.tag == "DesignArrow" && !needToRelease) {
				needToRelease = true;
				DesignArrow arrowScript = hit.collider.GetComponent<DesignArrow> ();
				arrowScript.Pressed ();
			}
			if (hit.collider != null && hit.collider.transform.tag == "SelectDesign" && !needToRelease) {
				needToRelease = true;
				BuildingScreen buildingScript = hit.collider.GetComponent<BuildingScreen> ();
				GeneralPlacedItem itemScript = buildingScript.baseObject.GetComponent<GeneralPlacedItem> ();
				itemScript.item = "0104";
				itemScript.buildingID = BuildingCatalog.getBuildingID(buildingScript.buildingSlot);
				itemScript.UpdateItemInfo ();
				inventoryScript.closeWindows ();
				removeFromChunk ("placeable",buildingScript.baseX,buildingScript.baseY);
				string[] neededItems = new string[]{"null","null","null","null","null","null"};
				int[] neededAmounts = new int[]{0,0,0,0,0,0};
				neededItems = BuildingCatalog.getNeededItems (buildingScript.buildingSlot);
				neededAmounts = BuildingCatalog.getNeededAmounts (buildingScript.buildingSlot);
				addToChunk (MainChunkPath + chunkLat + " " + chunkLon, "type:buildingsite;buildingid:"+BuildingCatalog.getBuildingID(buildingScript.buildingSlot)+";x:" + buildingScript.baseX + ";y:" + buildingScript.baseY + ";item:0104;part1:"+neededItems[0]+"x0;part2:"+neededItems[1]+"x0;part3:"+neededItems[2]+"x0;part4:"+neededItems[3]+"x0;part5:"+neededItems[4]+"x0;part6:"+neededItems[5]+"x0");
			}
			if (hit.collider != null && hit.collider.transform.tag == "MaterialAdd" && !needToRelease) {
				needToRelease = true;
				BuildingScreenAddMaterial materialAddScript = hit.transform.GetComponent<BuildingScreenAddMaterial> ();
				if(materialAddScript.materialScript.aquiredAmounts[materialAddScript.material]<materialAddScript.materialScript.neededAmounts[materialAddScript.material]){
					if(Inventory.checkForItem(materialAddScript.materialScript.neededItems[materialAddScript.material],1)){
						Inventory.removeItem (materialAddScript.materialScript.neededItems[materialAddScript.material],1);
						materialAddScript.materialScript.aquiredAmounts[materialAddScript.material]+=1;
						materialAddScript.materialScript.UpdateMaterials ();
						materialAddScript.materialScript.UpdateBoxInfo ();
					}
				}
			}
			if (hit.collider != null && hit.collider.transform.tag == "GiftButton" && !needToRelease) {
				GiftButton buttonScript = hit.collider.transform.GetComponent<GiftButton> ();
				buttonScript.ButtonPress ();
			}
			if (hit.collider != null && hit.collider.tag == "RecipeBox" && !needToRelease) {
				needToRelease = true;
				RecipeBox recipeScript = hit.collider.GetComponent<RecipeBox> ();
				if (!recipeScript.inactive) {
					selectedRecipe = recipeScript.recipeItem;
					recipeScript.selected = true;
				}
			}
			if (hit.collider != null && hit.collider.tag == "PetBanner" && !needToRelease) {
				needToRelease = true;
				PetScreenView viewScript = hit.collider.transform.parent.GetComponent<PetScreenView> ();
				if(viewScript.petID!=""){
					if (!PetInfo.HasPetCostume (viewScript.petID, viewScript.costume)) {
						if (AccountInfo.accountCoins >= 500) {
							AccountInfo.spendCoins (500);
							PetInfo.UnlockCostume (viewScript.petID, viewScript.costume);
							viewScript.UpdateInfo ();
						}
					} else {
						GameObject[] pets = GameObject.FindGameObjectsWithTag("Pet");
						for(int p = 0;p < pets.Length;p++){
							Destroy(pets[p]);
						}
						GameObject pet = (GameObject)Instantiate (Resources.Load ("GeneralPet"), new Vector3 (player.position.x + .1f, player.position.y, 0), Quaternion.identity);
						GeneralPet petScript = pet.GetComponent<GeneralPet> ();
						petScript.petID = viewScript.petID;
						petScript.costume = viewScript.costume;
						Controller.currentPet = viewScript.petID;
						Controller.currentPetCostume = viewScript.costume;
						hit.collider.transform.parent.gameObject.SetActive (false);
						inventoryScript.closeWindows ();
					}
				}
			}
			if (hit.collider != null && hit.collider.tag == "Pet" && !needToRelease) {
				needToRelease = true;
				GeneralPet petScript = hit.collider.GetComponent<GeneralPet> ();
				if((Inventory.checkHeldForTypeAndPwr("food",0)!=-1 || Inventory.checkHeldForTypeAndPwr ("foodseed", 0)!=-1) && PetInfo.GetPetFullness(petScript.petID) < 86400){
					PetInfo.FeedPet (petScript.petID,ItemCatalog.getItemPrice(Inventory.getSlotItemID (Inventory.hotbarItem))*1080);
					Instantiate (Resources.Load ("Effects/FeedHeartEffect"), new Vector3 (hit.collider.transform.position.x,hit.collider.transform.position.y+.05f, -20), Quaternion.identity);
					Inventory.removeItem(Inventory.getSlotItemID (Inventory.hotbarItem),1);
				}
			}
			if (hit.collider != null && hit.collider.tag == "PetSelect" && !needToRelease) {
				needToRelease = true;
				PetSelection selectionScript = hit.collider.GetComponent<PetSelection> ();
				if(selectionScript.petID!=""){
					GameObject petScreen = selectionScript.petViewScreen;
					petScreen.SetActive (true);
					PetScreenView viewScript = petScreen.GetComponent<PetScreenView> ();
					viewScript.Reset ();
					viewScript.petID = selectionScript.petID;
					viewScript.UpdateInfo ();
				}
			}
			if (hit.collider != null && hit.collider.tag == "CostumeArrow" && !needToRelease) {
				needToRelease = true;
				CostumeArrow arrowScript = hit.collider.GetComponent<CostumeArrow> ();
				if(arrowScript.left && arrowScript.viewScript.costume>0){
					arrowScript.viewScript.costume -= 1;
				}
				if(!arrowScript.left && arrowScript.viewScript.costume<4){
					arrowScript.viewScript.costume += 1;
				}
				arrowScript.viewScript.UpdateInfo ();
			}
			if (hit.collider != null && hit.collider.tag == "ClosePetWindow" && !needToRelease) {
				needToRelease = true;
				GameObject.Find("PetsViewScreen").SetActive (false);
			}
			if (hit.collider != null && hit.collider.tag == "FishingSpot" && !needToRelease) {
				needToRelease = true;
				WaterSpotScript spotScript = hit.collider.GetComponent<WaterSpotScript> ();
				spotScript.Select ();
				Instantiate (Resources.Load ("Effects/BobberSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
			}
			if (hit.collider != null && hit.collider.tag == "BattleCard" && !needToRelease) {
				needToRelease = true;
				CardScript cardScript = hit.collider.GetComponent<CardScript> ();
				cardScript.SelectCard ();
			}
			if (hit.collider != null && hit.collider.tag == "Reel" && !needToRelease) {
				needToRelease = true;
				FishingScript fishingScript = hit.collider.GetComponent<FishingScript> ();
				if(fishingScript.catchable){
					Instantiate (Resources.Load ("Effects/SplashSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
					if (!fishingScript.trash) {
						spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f, FishCatalog.getFishItemID (fishingScript.localFish), 100);
						Skills.addExp ("fishing",100);
							if (PetInfo.IsPetHappy ("0006")) {
								Skills.addExp ("fishing", 100);
							} else {
								Skills.addExp ("fishing", 50);
							}
					} else {
						spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f, "0032", 100);
						Skills.addExp ("fishing",50);
							if (PetInfo.IsPetHappy ("0006")) {
								Skills.addExp ("fishing", 50);
							} else {
								Skills.addExp ("fishing", 25);
							}
					}
				}
				inventoryScript.closeWindows ();
			}
			if (hit.collider != null && hit.collider.tag == "PageTurn" && !needToRelease) {
				needToRelease = true;
				PageTurnArrow pageTurnScript = hit.collider.GetComponent<PageTurnArrow> ();
				if (pageTurnScript.turnRight && !lastCraftingPage) {
					craftingPage += 1;
					selectedRecipe = 0;
					Instantiate (Resources.Load ("Effects/PageSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
				}
				if(!pageTurnScript.turnRight && craftingPage > 0){
					lastCraftingPage = false;
					craftingPage -= 1;
					selectedRecipe = 0;
					Instantiate (Resources.Load ("Effects/PageSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
				}
			}
			if (hit.collider != null && hit.collider.tag == "PetTurnLeft" && !needToRelease) {
				needToRelease = true;
				if(petPage > 0){
					petPage -= 1;
				}
			}
			if (hit.collider != null && hit.collider.tag == "PetTurnRight" && !needToRelease) {
				needToRelease = true;
				petPage += 1;
			}
			if (hit.collider != null && hit.collider.tag == "CraftButton" && !needToRelease) {
				needToRelease = true;
				CraftBox craftScript = hit.collider.GetComponent<CraftBox> ();
				craftScript.craft ();
			}
			if (hit.collider != null && hit.collider.tag == "QuestBox" && !needToRelease) {
				needToRelease = true;
				QuestBox questScript = hit.collider.GetComponent<QuestBox> ();
				//this doubled works for some reason
				QuestInfo.RemoveQuest (questScript.questNumber);
				QuestInfo.RemoveQuest (questScript.questNumber);
				questScript.UpdateBoxInfo ();
			}
			if (hit.collider != null && hit.collider.tag == "ItemDrop" && !needToRelease) {
				ItemDrop dropScript = hit.transform.GetComponent<ItemDrop> ();
				Inventory.addItem (dropScript.itemID,1);
				Destroy (hit.transform.gameObject);
				Instantiate (Resources.Load ("Effects/ItemPickupSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
				needToRelease = true;
			}
			if (hit.collider != null && hit.collider.tag == "Map" && !needToRelease && Controller.slot != "/toybox") {
				print ("touched");
				if((hit.transform.position.x < (interactionArea.position.x+.15f)) && (hit.transform.position.x > (interactionArea.position.x-.15f)) && (hit.transform.position.y < (interactionArea.position.y+.15f)) && (hit.transform.position.y > (interactionArea.position.y-.15f))){
				Instantiate (Resources.Load ("Effects/ItemPickupSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
				print ("touched interaction");
				Random.InitState ((int)(System.DateTime.Now.Year+System.DateTime.Now.Month+System.DateTime.Now.Day+chunkLon+chunkLat));
				questInfoScript.AddMapQuest (Random.Range(-25,25),Random.Range(25,-25),longSize,System.DateTime.Now.Year + (System.DateTime.Now.Month * 40) + System.DateTime.Now.Day);
				Destroy (hit.transform.gameObject);
				needToRelease = true;
				}
			}
			if (hit.collider != null && hit.collider.tag == "QuestButton" && !needToRelease) {
				if(!QuestInfo.IsHeldQuestsFull()){
						print ("QUEST ADDED");
					QuestScreen questScreenScript = inventoryScript.questScreen.GetComponent<QuestScreen> ();
					if(questScreenScript.status < 1){
						QuestInfo.ChangeStatusOfQuest (questScreenScript.npcID, 1);
							print ("STATUS CHANGED");
						needToRelease = true;
						inventoryScript.window = false;
						inventoryScript.questScreen.SetActive (false);
					}
					if(questScreenScript.status == 1){
						if(Inventory.checkForItem(questScreenScript.item,questScreenScript.amount)){
							Inventory.removeItem (questScreenScript.item,questScreenScript.amount);
							AccountInfo.addCoins (ItemCatalog.getItemPrice(questScreenScript.item) * questScreenScript.amount);
							Skills.addExp ("questing",((ItemCatalog.getItemPrice(questScreenScript.item) * questScreenScript.amount)/3));
							QuestInfo.ChangeStatusOfQuest (questScreenScript.npcID, 2);
							NPCInfo.AddNpcCompletion (questScreenScript.npcID);
							needToRelease = true;
							inventoryScript.window= false;
							inventoryScript.questScreen.SetActive (false);
						}
					}
				}
			}
			if (hit.collider != null && hit.collider.tag == "NPC" && !needToRelease) {
				if(!inventoryScript.window && (hit.transform.position.x < (interactionArea.position.x+.15f)) && (hit.transform.position.x > (interactionArea.position.x-.15f)) && (hit.transform.position.y < (interactionArea.position.y+.15f)) && (hit.transform.position.y > (interactionArea.position.y-.15f))){
					NPC npcScript = hit.transform.GetComponent<NPC> ();
					QuestInfo.RemoveOldQuests ();
					if(true){
						QuestScreen questScreenScript = inventoryScript.questScreen.GetComponent<QuestScreen> ();
						Inventory.loadHotbarImages ();
						inventoryScript.window = true;
						inventoryScript.needRelease = true;
						needToRelease = true;
						int status = -1;
						if(QuestInfo.HoldingQuestOfNPC (npcScript.id,0)){
							status = 0;
						}
						if(QuestInfo.HoldingQuestOfNPC (npcScript.id,1)){
							status = 1;
						}
						if(QuestInfo.HoldingQuestOfNPC (npcScript.id,2)){
							status = 2;
						}
						if (status < 0) {
							questScreenScript.status = 0;
							status = 0;
							QuestInfo.AddHeldQuest (npcScript.id,npcScript.name, npcScript.npcImage, System.DateTime.Now.Year + (System.DateTime.Now.Month * 40) + System.DateTime.Now.Day, npcScript.item, npcScript.amount);
						}
						questScreenScript.status = status;
						if(!NPCInfo.HasQuestOfNPC(npcScript.id)){
							NPCInfo.AddNpc (npcScript.id,npcScript.name,npcScript.npcImage,npcScript.npcClass);
						}
						npcScript.UpdateInfo ();
						inventoryScript.questScreen.SetActive (true);
						questScreenScript.item = npcScript.item;
						questScreenScript.itemName = npcScript.itemName;
						questScreenScript.amount = npcScript.amount;
						questScreenScript.npcImage = npcScript.renderer.sprite;
						questScreenScript.npcID = npcScript.id;
						questScreenScript.name = npcScript.name;
						questScreenScript.text = npcScript.text;
					}
				}
			}
				if (!inventoryScript.window && restTime <= 0 && !needToRelease && (hit.transform.position.x < (interactionArea.position.x + .15f)) && (hit.transform.position.x > (interactionArea.position.x - .15f)) && (hit.transform.position.y < (interactionArea.position.y + .15f)) && (hit.transform.position.y > (interactionArea.position.y - .15f))) {
					
					if (hit.collider != null && hit.collider.tag == "Animal") {
						WanderingAnimal animalScript = hit.transform.GetComponent<WanderingAnimal> ();
						if (Inventory.checkHeldForTypeAndPwr ("catcher", 0) != -1) {
							Inventory.removeItem (Inventory.getSlotItemID (Inventory.hotbarItem), 1);
							if (animalScript.animal == "chicken") {
								Inventory.addItem ("0160", 1);
							}
							if (animalScript.animal == "cow") {
								Inventory.addItem ("0161", 1);
							}
							if (animalScript.animal == "pig") {
								Inventory.addItem ("0162", 1);
							}
							if (animalScript.ownedTile == null) {
								Destroy (hit.transform.gameObject);
								DailyInfo.addData ("wildAnimalGone", animalScript.id.ToString ());
							} else {
								removeFromChunk ("placeable", animalScript.ownedTileX, animalScript.ownedTileY);
								Destroy (animalScript.ownedTile.gameObject);
								Destroy (hit.transform.gameObject);

							}
						} else {
							if(animalScript.ownedTile!=null){
								bool used = false;
								string lastDataChunk;
								string lastDataX;
								string lastDataY;
								for(int i2 = 0; i2 < DailyInfo.dailyActions.Count;i2++){
									lastDataChunk = DailyInfo.grabData ("animalUsed", i2, 0);
									lastDataX = DailyInfo.grabData ("animalUsed", i2, 1);
									lastDataY = DailyInfo.grabData ("animalUsed", i2, 2);
									if(lastDataChunk == "END"){
										break;
									}
									if(lastDataChunk == currentChunkRandom.ToString() && lastDataX == animalScript.ownedTileX.ToString() && lastDataY == animalScript.ownedTileY.ToString()){
										used = true;
									}
								}
								if(!used){
									if(animalScript.animal == "chicken"){
										spawnRandomItem (hit.transform.position.x,hit.transform.position.y,"0164",100);
									}
									if(animalScript.animal == "cow"){
										spawnRandomItem (hit.transform.position.x,hit.transform.position.y,"0165",100);
									}
									if(animalScript.animal == "pig"){
										spawnRandomItem (hit.transform.position.x,hit.transform.position.y,"0166",100);
									}
									DailyInfo.addData("animalUsed",currentChunkRandom.ToString()+":"+animalScript.ownedTileX+":"+animalScript.ownedTileY);
								}
							}
						}
					}
				}
			if(!inventoryScript.window && restTime <= 0 && !needToRelease && (hit.transform.position.x+.05f < (interactionArea.position.x+.15f)) && (hit.transform.position.x+.05f > (interactionArea.position.x-.15f)) && (hit.transform.position.y-.05f < (interactionArea.position.y+.15f)) && (hit.transform.position.y-.05f > (interactionArea.position.y-.15f))){
				restTime = 6.0f;

				//HITS FOR GENERAL PLACED ITEMS
				if (hit.collider != null && hit.collider.tag == "GeneralPlaced") {
					GeneralPlacedItem itemScript = hit.transform.GetComponent<GeneralPlacedItem> ();
					if(itemScript.item == "0103" && Inventory.checkHeldForTypeAndPwr ("axe", 0)==-1 && getBuildingsAmount()<5){
						needToRelease = true;
						inventoryScript.buildingScreen.SetActive (true);
						BuildingScreen buildingScript = inventoryScript.buildingScreen.GetComponent<BuildingScreen> ();
						buildingScript.buildingSlot = 0;
						buildingScript.baseX = itemScript.c;
						buildingScript.baseY = itemScript.r;
						buildingScript.baseObject = hit.transform.gameObject;
						inventoryScript.window = true;
						inventoryScript.needRelease = true;
					}
					if(itemScript.item == "0104" && Inventory.checkHeldForTypeAndPwr ("axe", 0)==-1){
						needToRelease = true;
						inventoryScript.buildingMaterialScreen.SetActive (true);
						BuildingMaterialScreen buildingScript = inventoryScript.buildingMaterialScreen.GetComponent<BuildingMaterialScreen> ();
						buildingScript.Reset ();
						buildingScript.buildingID = itemScript.buildingID;
						buildingScript.baseX = itemScript.c;
						buildingScript.baseY = itemScript.r;
						buildingScript.baseObject = hit.transform.gameObject;
						buildingScript.UpdateBoxInfo ();
						inventoryScript.window = true;
						inventoryScript.needRelease = true;
					}
					if(itemScript.item == "0105" && Inventory.checkHeldForTypeAndPwr ("axe", 0)==-1){
						needToRelease = true;
						inventoryScript.shedScreen.SetActive (true);
						InventoryTransferScreen transferScript = inventoryScript.shedScreen.GetComponent <InventoryTransferScreen>();
						transferScript.shedX = itemScript.c;
						transferScript.shedY = itemScript.r;
						transferScript.buildingID = itemScript.buildingID;
						transferScript.shedScript = itemScript;
						transferScript.storageSlots = itemScript.storageSlots;
						inventoryScript.window = true;
						inventoryScript.needRelease = true;
					}
					if(itemScript.item == "0109" && Inventory.checkHeldForTypeAndPwr ("axe", 0)==-1){
						needToRelease = true;
						if((System.DateTime.Now.Year + (System.DateTime.Now.Month * 40) + System.DateTime.Now.Day)>itemScript.lastUsed){
							itemScript.lastUsed = System.DateTime.Now.Year + (System.DateTime.Now.Month * 40) + System.DateTime.Now.Day;
							removeFromChunk ("building",itemScript.c,itemScript.r);
							addToChunk (MainChunkPath + CameraLocation.chunkLat + " " + CameraLocation.chunkLon, "type:building;buildingid:" + BuildingCatalog.getBuildingItemID(itemScript.buildingID) + ";x:" + itemScript.c+ ";y:" + itemScript.r + ";item:"+BuildingCatalog.getBuildingItemID(itemScript.buildingID)+";lastused:"+System.DateTime.Now.Year + (System.DateTime.Now.Month * 40) + System.DateTime.Now.Day);
						}
						inventoryScript.needRelease = true;
					}
					if (itemScript.item == "0125" && itemScript.lastUsed+(System.TimeSpan.TicksPerMinute*60)<=System.DateTime.Now.Ticks && !itemScript.complete) {
						needToRelease = true;
						inventoryScript.needRelease = true;
						itemScript.lastUsed = System.DateTime.Now.Ticks;
						removeFromChunk ("placeable",itemScript.c,itemScript.r);
						addToChunk (MainChunkPath + CameraLocation.chunkLat + " " + CameraLocation.chunkLon,"type:placeable;x:" + itemScript.c + ";y:" + itemScript.r + ";item:0125;completed:false;lastused:"+System.DateTime.Now.Ticks);
						CameraController.camera = 2;
						DungeonScript.Reset (int.Parse(itemScript.random)+(int)(System.DateTime.Now.Ticks/(System.TimeSpan.TicksPerDay*7)));
						DungeonScript.dungeonScript = itemScript;
					}
					if(Inventory.checkHeldForTypeAndPwr ("axe", 0)==-1 && itemScript.special == "workbench"){
						needToRelease = true;
						inventoryScript.openCraftingWindow ("workbench");
					}
					if(Inventory.checkHeldForTypeAndPwr ("pick", 0)==-1 && itemScript.special == "furnace"){
						needToRelease = true;
						inventoryScript.openCraftingWindow ("furnace");
					}
					if(Inventory.checkHeldForTypeAndPwr ("pick", 0)==-1 && itemScript.special == "anvil"){
						needToRelease = true;
						inventoryScript.openCraftingWindow ("anvil");
					}
					if(Inventory.checkHeldForTypeAndPwr ("pick", 0)==-1 && itemScript.special == "cookingpot"){
						needToRelease = true;
						inventoryScript.openCraftingWindow ("cookingPot");
					}
					if (ItemCatalog.getItemDestroyer(itemScript.item)=="shovel" && Inventory.checkHeldForTypeAndPwr ("shovel", ItemCatalog.getItemPowerMin(itemScript.item))!=-1) {
						if (itemScript.shovelTaps > 10) {
							//Handheld.Vibrate ();
							needToRelease = true;
							Inventory.decreaseDurForSlot (Inventory.hotbarItem);
							Instantiate (Resources.Load ("Effects/PatSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
							Instantiate (Resources.Load ("Effects/HitCircleDone"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
							spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,itemScript.item,100);
							itemScript.shovelTaps = 0;
							removeFromChunk ("placeable", itemScript.c, itemScript.r);
							Destroy (hit.transform.gameObject);
						} else {
							Instantiate (Resources.Load ("Effects/PatSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
							Instantiate (Resources.Load ("Effects/HitCircle"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
							itemScript.shovelTaps += 1+Inventory.checkHeldForTypeAndPwr ("shovel", ItemCatalog.getItemPowerMin(itemScript.item));
						}
					}
					if (ItemCatalog.getItemDestroyer(itemScript.item)=="axe" && Inventory.checkHeldForTypeAndPwr ("axe", ItemCatalog.getItemPowerMin(itemScript.item))!=-1) {
						if (itemScript.axeTaps > 10) {
							//Handheld.Vibrate ();
							needToRelease = true;
							Inventory.decreaseDurForSlot (Inventory.hotbarItem);
							Instantiate (Resources.Load ("Effects/ChopSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
							Instantiate (Resources.Load ("Effects/HitCircleDone"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
							if(itemScript.item == "0104"){
								spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f, "0103", 100);
								removeFromChunk ("buildingsite", itemScript.c, itemScript.r);
							}else if (itemScript.item == "0109" || itemScript.item == "0105") {
								spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f, "0103", 100);
								removeFromChunk ("building", itemScript.c, itemScript.r);
							} else{
								spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f, itemScript.item, 100);
								removeFromChunk ("placeable", itemScript.c, itemScript.r);
							}
							itemScript.axeTaps = 0;
							Destroy (hit.transform.gameObject);
						} else {
							Instantiate (Resources.Load ("Effects/ChopSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
							Instantiate (Resources.Load ("Effects/HitCircle"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
							itemScript.axeTaps += 1+Inventory.checkHeldForTypeAndPwr ("axe", ItemCatalog.getItemPowerMin(itemScript.item));
						}
					}
					if (ItemCatalog.getItemDestroyer(itemScript.item)=="pick" && Inventory.checkHeldForTypeAndPwr ("pick", ItemCatalog.getItemPowerMin(itemScript.item))!=-1) {
						if (itemScript.pickTaps > 10) {
							//Handheld.Vibrate ();
							needToRelease = true;
							Inventory.decreaseDurForSlot (Inventory.hotbarItem);
							Instantiate (Resources.Load ("Effects/PickSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
							Instantiate (Resources.Load ("Effects/HitCircleDone"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
							spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,itemScript.item,100);
							itemScript.pickTaps = 0;
							removeFromChunk ("placeable", itemScript.c, itemScript.r);
							Destroy (hit.transform.gameObject);
						} else {
							Instantiate (Resources.Load ("Effects/PickSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
							Instantiate (Resources.Load ("Effects/HitCircle"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
							itemScript.pickTaps += 1+Inventory.checkHeldForTypeAndPwr ("pick", ItemCatalog.getItemPowerMin(itemScript.item));
						}
					}
				}
				if (hit.collider != null && hit.collider.tag == "Tile") {
					Tile tileScript = hit.transform.GetComponent<Tile> ();
					if (tileScript.type == "water" && tileScript.fishingSpot) {
						if (Inventory.checkHeldForTypeAndPwr ("fishingpole", 0)!=-1) {
							Inventory.decreaseDurForSlot (Inventory.hotbarItem);
							inventoryScript.openFishingWindow ();
							needToRelease = true;
						}
					}
					if (tileScript.type == "water") {
						if (Inventory.checkHeldForTypeAndPwr ("bucket", 0)!=-1) {
							Inventory.removeItem (Inventory.getSlotItemID(Inventory.hotbarItem),1);
							Inventory.addItem ("0173",1);
							needToRelease = true;
						}
					}
					if (tileScript.type == "land") {
						if(Inventory.checkHeldForTypeAndPwr("hoe",0)!=-1 && !tileScript.sandy){
							needToRelease = true;
							tileScript.hoed = true;
							Inventory.decreaseDurForSlot (Inventory.hotbarItem);
						}
						if (Inventory.checkHeldForTypeAndPwr ("treestart", 0)!=-1 && tileScript.hoed == true) {
							needToRelease = true;
							Instantiate (Resources.Load ("Effects/PatSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
							Instantiate (Resources.Load ("Effects/HitCircleDone"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
							tileScript.hoed = false;
							GameObject tree = (GameObject)Instantiate (Resources.Load ("Placeable/Tree"), new Vector3 (hit.transform.position.x, hit.transform.position.y, 9), Quaternion.identity);
							Random.InitState ((int)System.DateTime.Now.Ticks);
							int random = Random.Range (1,3);
							addToChunk (MainChunkPath + chunkLat + " " + chunkLon, "type:tree;stage:0;size:" + random + ";x:" + tileScript.c + ";y:" + tileScript.r + ";time:" + System.DateTime.Now.Ticks);
							Tree treeScript = tree.GetComponent<Tree> ();
							treeScript.tickPlanted = System.DateTime.Now.Ticks;
							Inventory.changeQtyForSlot (Inventory.hotbarItem,-1);
						}
						if ((Inventory.checkHeldForTypeAndPwr ("seed", 0)!=-1||Inventory.checkHeldForTypeAndPwr ("foodseed", 0)!=-1) && tileScript.hoed == true) {
							needToRelease = true;
							Instantiate (Resources.Load ("Effects/HitCircleDone"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
							Instantiate (Resources.Load ("Effects/PatSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
							tileScript.hoed = false;
							GameObject plant = (GameObject)Instantiate (Resources.Load ("Placeable/GeneralPlant"), new Vector3 (hit.transform.position.x, hit.transform.position.y, 9), Quaternion.identity);
							Random.InitState ((int)System.DateTime.Now.Ticks);
							int random = Random.Range (1,3);
							addToChunk (MainChunkPath + chunkLat + " " + chunkLon, "type:plant;plantid:"+ItemCatalog.getItemPlantID(Inventory.getSlotItemID(Inventory.hotbarItem))+";special:"+ItemCatalog.getItemSpecial(Inventory.getSlotItemID(Inventory.hotbarItem))+";x:" + tileScript.c + ";y:" + tileScript.r + ";time:" + System.DateTime.Now.Ticks+";growtime:"+PlantCatalog.getPlantGrowTime(ItemCatalog.getItemPlantID(Inventory.getSlotItemID(Inventory.hotbarItem))));
							GeneralPlant plantScript = plant.GetComponent<GeneralPlant> ();
							plantScript.tickPlanted = System.DateTime.Now.Ticks;
							plantScript.plantID = ItemCatalog.getItemPlantID (Inventory.getSlotItemID (Inventory.hotbarItem));
							plantScript.timeMultiplier = PlantCatalog.getPlantGrowTime (ItemCatalog.getItemPlantID (Inventory.getSlotItemID (Inventory.hotbarItem)));
							Inventory.changeQtyForSlot (Inventory.hotbarItem,-1);
						}
						if (Inventory.checkHeldForTypeAndPwr ("placeable", 0)!=-1) {
							needToRelease = true;
							tileScript.hoed = false;
							GameObject item = (GameObject)Instantiate (Resources.Load ("Placeable/GeneralPlaced"), new Vector3 (hit.transform.position.x, hit.transform.position.y, 9), Quaternion.identity);
							string itemID = Inventory.getSlotItemID (Inventory.hotbarItem);
							addToChunk (MainChunkPath + chunkLat + " " + chunkLon, "type:placeable;x:" + tileScript.c + ";y:" + tileScript.r + ";item:"+itemID);
							GeneralPlacedItem itemScript = item.GetComponent<GeneralPlacedItem> ();
							itemScript.item = itemID;
							itemScript.r = tileScript.r;
							itemScript.c = tileScript.c;
							if(Inventory.getSlotItemID(Inventory.hotbarItem) == "0160" || Inventory.getSlotItemID(Inventory.hotbarItem) == "0161" || Inventory.getSlotItemID(Inventory.hotbarItem) == "0162"){
									bool used = false;
									string lastDataChunk;
									string lastDataX;
									string lastDataY;
									for(int i2 = 0; i2 < DailyInfo.dailyActions.Count;i2++){
										lastDataChunk = DailyInfo.grabData ("animalUsed", i2, 0);
										lastDataX = DailyInfo.grabData ("animalUsed", i2, 1);
										lastDataY = DailyInfo.grabData ("animalUsed", i2, 2);
										if(lastDataChunk == "END"){
											break;
										}
										if(lastDataChunk == CameraLocation.currentChunkRandom.ToString() && lastDataX == itemScript.c.ToString() && lastDataY == itemScript.r.ToString()){
											used = true;
										}
									}
								if(!used){
									DailyInfo.addData("animalUsed",currentChunkRandom.ToString()+":"+itemScript.c.ToString()+":"+itemScript.r.ToString());
								}
							}
							Inventory.changeQtyForSlot (Inventory.hotbarItem,-1);
						}
						if(Inventory.checkHeldForTypeAndPwr("shovel",0)!=-1 && tileScript.hoed == false){
							if (tileScript.shovelTaps > 10) {
								//Handheld.Vibrate ();
								needToRelease = true;
								if(Mathf.Abs(QuestInfo.GetMapLon() - actualChunkLon)<=longSize && Mathf.Abs(QuestInfo.GetMapLat() - actualChunkLat)<=.001f){
									if(QuestInfo.GetMapStatus()==1){
										QuestInfo.ChangeStatusOfMapQuest (2);
										spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0041",100);
										spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0041",75);
										spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0041",50);
										spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0041",25);
										spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0025",5);
										spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0026",5);
										spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0027",5);
										spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0028",5);
										spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0029",5);
									}
								}
								if (PetInfo.IsPetHappy ("0004")) {
									Skills.addExp ("digging", 50);
								} else {
									Skills.addExp ("digging", 25);
								}
								spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f, "0159", 10);
								Inventory.decreaseDurForSlot (Inventory.hotbarItem);
								tileScript.shovelTaps = 0;
								Instantiate (Resources.Load ("Effects/DigSound"), new Vector3 (hit.transform.position.x+.05f, hit.transform.position.y-.05f, 0), Quaternion.identity);
								Instantiate (Resources.Load ("Effects/HitCircleDone"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
								GameObject hole = (GameObject)Instantiate (Resources.Load ("DugHole"), new Vector3 (hit.transform.position.x, hit.transform.position.y, 9), Quaternion.identity);
								addToChunk (MainChunkPath + chunkLat + " " + chunkLon,"type:hole;x:"+tileScript.c+";y:"+tileScript.r+";ore:"+tileScript.ore);
								Hole holeScript = hole.GetComponent<Hole> ();
								holeScript.c = tileScript.c;
								holeScript.r = tileScript.r;
								holeScript.ore = tileScript.ore;
								holeScript.parentTile = hit.transform.gameObject;
							} else {
								Instantiate (Resources.Load ("Effects/DigSound"), new Vector3 (hit.transform.position.x+.05f, hit.transform.position.y-.05f, 0), Quaternion.identity);
								Instantiate (Resources.Load ("Effects/HitCircle"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
								tileScript.shovelTaps += 1+Inventory.checkHeldForTypeAndPwr("shovel",0);
							}
						}
					}
				}
			if (hit.collider != null && hit.collider.tag == "Hole") {
				Hole holeScript = hit.transform.GetComponent<Hole> ();
					if (Inventory.checkHeldForTypeAndPwr ("shovel", 0)!=-1) {
						if (holeScript.shovelTaps > 2) {
							needToRelease = true;
							Inventory.decreaseDurForSlot (Inventory.hotbarItem);
							Instantiate (Resources.Load ("Effects/PatSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
							Instantiate (Resources.Load ("Effects/HitCircleDone"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
							holeScript.shovelTaps = 0;
							Destroy (hit.transform.gameObject);
							removeFromChunk ("hole",holeScript.c,holeScript.r);
						} else {
							Instantiate (Resources.Load ("Effects/PatSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
							Instantiate (Resources.Load ("Effects/HitCircle"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
							holeScript.shovelTaps += 1+Inventory.checkHeldForTypeAndPwr ("shovel", 0);
						}
					}
					if (Inventory.checkHeldForTypeAndPwr ("pick", 0)!=-1) {
						if(holeScript.ore != 0){
							if (holeScript.pickTaps > 10) {
								//Handheld.Vibrate ();
								needToRelease = true;
								removeFromChunk ("hole",holeScript.c,holeScript.r);
								addToChunk (MainChunkPath + chunkLat + " " + chunkLon,"type:hole;x:"+holeScript.c+";y:"+holeScript.r+";ore:0");
									if (PetInfo.IsPetHappy ("0009")) {
										Skills.addExp ("mining", 50);
									} else {
										Skills.addExp ("mining", 25);
									}
								Inventory.decreaseDurForSlot (Inventory.hotbarItem);
								Instantiate (Resources.Load ("Effects/PickSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
								Instantiate (Resources.Load ("Effects/HitCircleDone"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
								if(holeScript.ore == 1){
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0035",100);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0035",50);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0036",100);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0036",50);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0036",10);
								}
								if(holeScript.ore == 2){
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0035",100);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0035",50);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0037",100);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0037",50);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0037",10);
								}
								if(holeScript.ore == 3){
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0035",100);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0035",50);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0038",100);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0038",50);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0038",10);
								}
								if(holeScript.ore == 4){
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0035",100);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0035",50);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0039",100);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0039",50);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0039",10);
								}
								if(holeScript.ore == 5){
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0035",100);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0035",50);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0040",100);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0040",50);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0040",10);
								}
								if(holeScript.ore == 6){
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0035",100);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0035",50);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0041",100);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0041",50);
									spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0041",10);
								}
								holeScript.pickTaps = 0;
								holeScript.ore = 0;
								Tile tileScript = holeScript.parentTile.GetComponent <Tile>();
								tileScript.ore = 0;
								removeFromChunk ("ore", holeScript.c, holeScript.r);
							} else {
								Instantiate (Resources.Load ("Effects/PickSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
								Instantiate (Resources.Load ("Effects/HitCircle"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
								holeScript.pickTaps += 1+Inventory.checkHeldForTypeAndPwr ("pick", 0);
							}
						}
					}
			}
				if (hit.collider != null && hit.collider.tag == "Tree") {
					Tree treeScript = hit.transform.GetComponent<Tree> ();
					if (Inventory.checkHeldForTypeAndPwr ("axe", 0)==-1) {
						spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0034",100);
						//Handheld.Vibrate ();
						needToRelease = true;
						Instantiate (Resources.Load ("Effects/BarkSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
						Instantiate (Resources.Load ("Effects/HitCircleDone"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
					}
					if (Inventory.checkHeldForTypeAndPwr ("axe", 0)!=-1 && treeScript.stage < 3) {
						Inventory.decreaseDurForSlot (Inventory.hotbarItem);
						Instantiate (Resources.Load ("Effects/ChopSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
						Instantiate (Resources.Load ("Effects/HitCircleDone"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
						removeFromChunk ("tree", treeScript.c, treeScript.r);
						Destroy (hit.transform.gameObject);
					}
					if (Inventory.checkHeldForTypeAndPwr ("axe", 0)!=-1 && treeScript.stage >= 3) {
						if (treeScript.axeTaps > 10) {
							//Handheld.Vibrate ();
							needToRelease = true;
							if (!(treeScript.tickPlanted > 0)) {
								if (PetInfo.IsPetHappy ("0002")) {
									Skills.addExp ("cutting", 50);
								} else {
									Skills.addExp ("cutting", 25);
								}
							} else {
								Skills.addExp ("farming", 25);
								if (PetInfo.IsPetHappy ("0002")) {
									Skills.addExp ("cutting", 50);
								} else {
									Skills.addExp ("cutting", 25);
								}
							}
							Inventory.decreaseDurForSlot (Inventory.hotbarItem);
							Instantiate (Resources.Load ("Effects/ChopSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
							Instantiate (Resources.Load ("Effects/HitCircleDone"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
							spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0030",100);
							spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0030",20);
							if(treeScript.biome != "beach"){
								spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0031",50);
							}
							if(treeScript.biome == "beach"){
								spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0100",50);
							}
							spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0032",100);
							spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0032",75);
							spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,"0032",10);
							treeScript.axeTaps = 0;
							removeFromChunk ("tree", treeScript.c, treeScript.r);
							Destroy (hit.transform.gameObject);
						} else {
							Instantiate (Resources.Load ("Effects/ChopSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
							Instantiate (Resources.Load ("Effects/HitCircle"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
							treeScript.axeTaps += 1+Inventory.checkHeldForTypeAndPwr ("axe", 0);
						}
					}
				}
				if (hit.collider != null && hit.collider.tag == "GeneralPlant") {
					GeneralPlant plantScript = hit.transform.GetComponent<GeneralPlant> ();
					string[] drops = PlantCatalog.getPlantDrops (plantScript.plantID);
					if (Inventory.checkHeldForTypeAndPwr ("scythe", 0)!=-1 && plantScript.stage < 3) {
						needToRelease = true;
						Inventory.decreaseDurForSlot (Inventory.hotbarItem);
						Instantiate (Resources.Load ("Effects/SlashSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
						Instantiate (Resources.Load ("Effects/HitCircleDone"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
						removeFromChunk ("plant", plantScript.c, plantScript.r);
						Destroy (hit.transform.gameObject);
					}
					if (Inventory.checkHeldForTypeAndPwr ("scythe", 0)!=-1 && plantScript.stage >= 3) {
						if (plantScript.scytheTaps > 10) {
							//Handheld.Vibrate ();
							needToRelease = true;
							if(plantScript.tickPlanted > 100){
								Skills.addExp ("farming", 25);
							}
							Inventory.decreaseDurForSlot (Inventory.hotbarItem);
							Instantiate (Resources.Load ("Effects/SlashSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
							Instantiate (Resources.Load ("Effects/HitCircleDone"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
							for(int i2 = 0;i2 < drops.Length;i2++){
								string[] dropSplit = drops[i2].Split('%');
								spawnRandomItem (hit.transform.position.x + .05f, hit.transform.position.y - .05f,dropSplit[0],int.Parse(dropSplit[1]));
							}
							plantScript.scytheTaps = 0;
							removeFromChunk ("plant", plantScript.c, plantScript.r);
							Destroy (hit.transform.gameObject);
						} else {
							Instantiate (Resources.Load ("Effects/SlashSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
							Instantiate (Resources.Load ("Effects/HitCircle"), new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
							plantScript.scytheTaps += 1+Inventory.checkHeldForTypeAndPwr ("scythe", 0);
						}
					}
				}
				}
			}
			if(Input.GetTouch(i).phase == TouchPhase.Ended){
				needToRelease = false;
			}
		}
		Vector3 tempPos = transform.position;
		if(!Controller.joyStick){
			if((chunkLat != lastChunkLat || chunkLon != lastChunkLon) && loading>0){
				inventoryScript.closeWindows ();
				CameraController.camera = 0;
				lastChunkLat = chunkLat;
				lastChunkLon = chunkLon;
				chunkSetup ();
				tempPos = transform.position;
				if (Input.location.lastData.longitude > 0) {
					tempPos.x = (Mathf.Abs (Input.location.lastData.longitude % longSize) * longMultiple);
				} else {
					tempPos.x = 2-(Mathf.Abs (Input.location.lastData.longitude % longSize) * longMultiple);
				}
				if (Input.location.lastData.latitude > 0) {
					tempPos.y = (Mathf.Abs (Input.location.lastData.latitude % .001f) * 2000f);
				} else {
					tempPos.y = 2-(Mathf.Abs (Input.location.lastData.latitude % .001f) * 2000f);
				}
				transform.position = tempPos;
			}
			if (Input.location.status == LocationServiceStatus.Running) {
				lastLon = Input.location.lastData.longitude;
				lastLat = Input.location.lastData.latitude;
				if(Input.location.lastData.longitude<0){
					chunkLon = (int)((Input.location.lastData.longitude)/longSize);
					actualChunkLon = (Input.location.lastData.longitude);
					actualChunkLon = float.Parse((actualChunkLon+.0005f).ToString ("F3"));
				}
				if(Input.location.lastData.longitude>0){
					chunkLon = (int)((Input.location.lastData.longitude)/longSize);
					actualChunkLon = (Input.location.lastData.longitude);
					actualChunkLon = float.Parse((actualChunkLon+.0005f).ToString ("F3"));
				}
				if (Input.location.lastData.latitude < 0) {
					chunkLat = (int)((Input.location.lastData.latitude) / .001f);
					actualChunkLat = (Input.location.lastData.latitude);
					actualChunkLat = float.Parse((actualChunkLat-.0005f).ToString ("F3"));
				}
				if (Input.location.lastData.latitude > 0) {
					chunkLat = (int)((Input.location.lastData.latitude) / .001f);
					actualChunkLat = (Input.location.lastData.latitude);
					actualChunkLat = float.Parse((actualChunkLat-.0005f).ToString ("F3"));
				}
				tempPos = transform.position;
			}
			if (Input.location.lastData.longitude > 0) {
				tempPos.x = Mathf.Lerp(transform.position.x,(Mathf.Abs (Input.location.lastData.longitude) % longSize * longMultiple),10f * Time.deltaTime);
			} else {
				tempPos.x = Mathf.Lerp(transform.position.x,2-(Mathf.Abs (Input.location.lastData.longitude) % longSize * longMultiple),10f * Time.deltaTime);
			}
			if (Input.location.lastData.latitude > 0) {
				tempPos.y = Mathf.Lerp(transform.position.y,(Mathf.Abs (Input.location.lastData.latitude) % (.001f) * 2000f),10f * Time.deltaTime);
			} else {
				tempPos.y = Mathf.Lerp(transform.position.y,2-(Mathf.Abs (Input.location.lastData.latitude) % (.001f) * 2000f),10f * Time.deltaTime);
			}
			transform.position = tempPos;
			if (chunkLat != lastChunkLat || chunkLon != lastChunkLon) {
				loading = 1.0f;
			}
		}
		if(Controller.joyStick){
			if((chunkLat != lastChunkLat || chunkLon != lastChunkLon) && loading>0){
				inventoryScript.closeWindows ();
				CameraController.camera = 0;
				lastChunkLat = chunkLat;
				lastChunkLon = chunkLon;
				chunkSetup ();
				tempPos = transform.position;
				if (Controller.joyLon > 0) {
					tempPos.x = (Mathf.Abs (Controller.joyLon % longSize) * longMultiple);
				} else {
					tempPos.x = 2-(Mathf.Abs (Controller.joyLon % longSize) * longMultiple);
				}
				if (Controller.joyLat > 0) {
					tempPos.y = (Mathf.Abs (Controller.joyLat % .001f) * 2000f);
				} else {
					tempPos.y = 2-(Mathf.Abs (Controller.joyLat % .001f) * 2000f);
				}
				transform.position = tempPos;
			}
			if (true) {
				lastLon = Controller.joyLon;
				lastLat = Controller.joyLat;
				if(Controller.joyLon<0){
					chunkLon = (int)((Controller.joyLon)/longSize);
					actualChunkLon = (Controller.joyLon);
					actualChunkLon = float.Parse((actualChunkLon+.0005f).ToString ("F3"));
				}
				if(Controller.joyLon>0){
					chunkLon = (int)((Controller.joyLon)/longSize);
					actualChunkLon = (Controller.joyLon);
					actualChunkLon = float.Parse((actualChunkLon+.0005f).ToString ("F3"));
				}
				if (Controller.joyLat < 0) {
					chunkLat = (int)((Controller.joyLat) / .001f);
					actualChunkLat = (Controller.joyLat);
					actualChunkLat = float.Parse((actualChunkLat-.0005f).ToString ("F3"));
				}
				if (Controller.joyLat > 0) {
					chunkLat = (int)((Controller.joyLat) / .001f);
					actualChunkLat = (Controller.joyLat);
					actualChunkLat = float.Parse((actualChunkLat-.0005f).ToString ("F3"));
				}
				tempPos = transform.position;
			}
			if (Controller.joyLon > 0) {
				tempPos.x = Mathf.Lerp(transform.position.x,(Mathf.Abs (Controller.joyLon) % longSize * longMultiple),10f * Time.deltaTime);
			} else {
				tempPos.x = Mathf.Lerp(transform.position.x,2-(Mathf.Abs (Controller.joyLon) % longSize * longMultiple),10f * Time.deltaTime);
			}
			if (Controller.joyLat > 0) {
				tempPos.y = Mathf.Lerp(transform.position.y,(Mathf.Abs (Controller.joyLat) % (.001f) * 2000f),10f * Time.deltaTime);
			} else {
				tempPos.y = Mathf.Lerp(transform.position.y,2-(Mathf.Abs (Controller.joyLat) % (.001f) * 2000f),10f * Time.deltaTime);
			}
			transform.position = tempPos;
			if (chunkLat != lastChunkLat || chunkLon != lastChunkLon) {
				loading = 1.0f;
				MenuCameraScript.saveStats ();
			}
		}
		//SETS WEATHER
		borderChunks = !Controller.disableBorderChunks;
		if(System.DateTime.Now.Ticks > (lastWeatherUpdate + (System.TimeSpan.TicksPerMinute*5))){
			string weatherReturn = GetChunkWeather (actualChunkLat, actualChunkLon);
			currentWeatherCode = GetChunkMainWeather(weatherReturn);
			currentTemperature = GetChunkTemperature(weatherReturn);
			lastWeatherUpdate = System.DateTime.Now.Ticks;
		}
		mainWeather = "clear";
		if (currentWeatherCode [0].ToString ().Contains ("8")) {
			if (currentWeatherCode [2].ToString ().Contains ("0")) {
				clouds1.gameObject.SetActive (false);
				clouds2.gameObject.SetActive (false);
				clouds3.gameObject.SetActive (false);
				mainWeather = "clear";
			}
			if (currentWeatherCode [2].ToString ().Contains ("1")) {
				clouds1.gameObject.SetActive (true);
				clouds2.gameObject.SetActive (false);
				clouds3.gameObject.SetActive (false);
				mainWeather = "clouds";
			}
			if (currentWeatherCode [2].ToString ().Contains ("2")) {
				clouds1.gameObject.SetActive (true);
				clouds2.gameObject.SetActive (true);
				clouds3.gameObject.SetActive (false);
				mainWeather = "clouds";
			}
			if (currentWeatherCode [2].ToString ().Contains ("3")) {
				clouds1.gameObject.SetActive (true);
				clouds2.gameObject.SetActive (true);
				clouds3.gameObject.SetActive (false);
				mainWeather = "clouds";
			}
			if (currentWeatherCode [2].ToString ().Contains ("4")) {
				clouds1.gameObject.SetActive (true);
				clouds2.gameObject.SetActive (true);
				clouds3.gameObject.SetActive (true);
				mainWeather = "clouds";
			}
		} else {
			clouds1.gameObject.SetActive (false);
			clouds2.gameObject.SetActive (false);
			clouds3.gameObject.SetActive (false);
		}
		if (currentWeatherCode.ToString ().Contains ("200") || currentWeatherCode.ToString ().Contains ("210") || currentWeatherCode.ToString ().Contains ("230") || currentWeatherCode.ToString ().Contains ("300") || currentWeatherCode.ToString ().Contains ("310") || currentWeatherCode.ToString ().Contains ("321") || currentWeatherCode.ToString ().Contains ("500") || currentWeatherCode.ToString ().Contains ("520") || currentWeatherCode.ToString ().Contains ("612") || currentWeatherCode.ToString ().Contains ("615") || currentWeatherCode.ToString ().Contains ("620")) {
			lightRainEmitter.gameObject.SetActive (true);
			mainWeather = "rain";
		} else {
			lightRainEmitter.gameObject.SetActive (false);
		}
		if (currentWeatherCode.ToString ().Contains ("201") || currentWeatherCode.ToString ().Contains ("211") || currentWeatherCode.ToString ().Contains ("231") || currentWeatherCode.ToString ().Contains ("301") || currentWeatherCode.ToString ().Contains ("311") || currentWeatherCode.ToString ().Contains ("313") || currentWeatherCode.ToString ().Contains ("501") || currentWeatherCode.ToString ().Contains ("511") || currentWeatherCode.ToString ().Contains ("521") || currentWeatherCode.ToString ().Contains ("616") || currentWeatherCode.ToString ().Contains ("621")) {
			mediumRainEmitter.gameObject.SetActive (true);
			mainWeather = "rain";
		} else {
			mediumRainEmitter.gameObject.SetActive (false);
		}
		if (currentWeatherCode.ToString ().Contains ("202") || currentWeatherCode.ToString ().Contains ("212") || currentWeatherCode.ToString ().Contains ("221") || currentWeatherCode.ToString ().Contains ("232") || currentWeatherCode.ToString ().Contains ("302") || currentWeatherCode.ToString ().Contains ("312") || currentWeatherCode.ToString ().Contains ("314") || currentWeatherCode.ToString ().Contains ("502") || currentWeatherCode.ToString ().Contains ("503") || currentWeatherCode.ToString ().Contains ("504") || currentWeatherCode.ToString ().Contains ("522") || currentWeatherCode.ToString ().Contains ("531") || currentWeatherCode.ToString ().Contains ("622")) {
			heavyRainEmitter.gameObject.SetActive (true);
			mainWeather = "rain";
		} else {
			heavyRainEmitter.gameObject.SetActive (false);
		}
		if (currentWeatherCode.ToString ().Contains ("600") || currentWeatherCode.ToString ().Contains ("611") || currentWeatherCode.ToString ().Contains ("615") || currentWeatherCode.ToString ().Contains ("620")) {
			lightSnowEmitter.gameObject.SetActive (true);
			mainWeather = "snow";
		} else {
			lightSnowEmitter.gameObject.SetActive (false);
		}
		if (currentWeatherCode.ToString ().Contains ("601") || currentWeatherCode.ToString ().Contains ("612") || currentWeatherCode.ToString ().Contains ("616") || currentWeatherCode.ToString ().Contains ("621")) {
			mediumSnowEmitter.gameObject.SetActive (true);
			mainWeather = "snow";
		} else {
			mediumSnowEmitter.gameObject.SetActive (false);
		}
		if (currentWeatherCode.ToString ().Contains ("602") || currentWeatherCode.ToString ().Contains ("622")) {
			heavySnowEmitter.gameObject.SetActive (true);
			mainWeather = "snow";
		} else {
			heavySnowEmitter.gameObject.SetActive (false);
		}
		timeTillFirstWeather -= 1f * Time.deltaTime;
		timeTillWeather -= 1f * Time.deltaTime;
		if (Controller.isPortrait ()) {
			if (mapZoomedOut) {
				mainCamera.orthographicSize = Mathf.Lerp (mainCamera.orthographicSize, 2.8f, 5f * Time.deltaTime);
			} else {
				mainCamera.orthographicSize = Mathf.Lerp (mainCamera.orthographicSize, .7f, 5f * Time.deltaTime);
			}
		}
		if (Controller.isLandscape ()) {
			if (mapZoomedOut) {
				mainCamera.orthographicSize = Mathf.Lerp (mainCamera.orthographicSize, 1.6f, 5f * Time.deltaTime);
			} else {
				mainCamera.orthographicSize = Mathf.Lerp (mainCamera.orthographicSize, .4f, 5f * Time.deltaTime);
			}
		}
	}
	void FixedUpdate(){
		if(false){
			Vector2 screenBounds = Camera.main.ScreenToWorldPoint (new Vector2(Screen.width,Screen.height));
			Vector3 tempPos = transform.position;
			if(transform.position.x < (0 + (screenBounds.x/2))){
				tempPos.x = (0 + (screenBounds.x / 2));
			}
			if(transform.position.x > (2 - (screenBounds.x/2))){
				tempPos.x = (2 - (screenBounds.x / 2));
			}
			if(transform.position.y < (0 + (screenBounds.y/2))){
				tempPos.y = (0 + (screenBounds.y / 2));
			}
			if(transform.position.y > (2 - (screenBounds.y/2))){
				tempPos.y = (2 - (screenBounds.y / 2));
			}
			transform.position = tempPos;
		}
	}
	void OnGUI(){
		if(!(Input.location.status == LocationServiceStatus.Running) && !Controller.joyStick){
			DrawQuad (new Rect(0,0,Screen.width,Screen.height),Color.black);
			GUI.DrawTexture (new Rect((Screen.width/2)-45*5,Screen.height/2,90*5,12*5),gettingLocation);
		}
		if(loading>0){
			DrawQuad (new Rect(0,0,Screen.width,Screen.height),Color.black);
			GUI.DrawTexture (new Rect((Screen.width/2)-21*5,Screen.height/2,42*5,12*5),loadingText);
		}
		//GUI.Label(new Rect(10, 10, 500, 20), "CurrentChunkRandom:" + currentChunkRandom);
		//GUI.Label(new Rect(10, 190, 500, 20), "Longitude:" + (Input.location.lastData.longitude));
		//GUI.Label(new Rect(10, 220, 500, 20), "Latitude:" + (Input.location.lastData.latitude));
		//GUI.Label(new Rect(10, 70, 500, 20), "Chunk Long:" + (chunkLon));
		//GUI.Label(new Rect(10, 100, 500, 20), "Chunk Lat:" + (chunkLat));
		//GUI.Label(new Rect(10, 130, 500, 20), "Actual Chunk Long:" + actualChunkLon);
		//GUI.Label(new Rect(10, 160, 500, 20), "Actual Chunk Lat:" + actualChunkLat);
		//GUI.Label(new Rect(10, 190, 500, 20), "Path:" + MainChunkPath);
	}
	void GenerateChunkInfo(string path,int lat,int lon,float actualChunkLat,float actualChunkLon){
		var file = File.CreateText (path);
		var place = "nointernetcity";
		var placeID = "nointernetplaceid";
		var placeType = "nointernetplacetype";
		var placeWikiData = "null";
		if(Application.internetReachability != NetworkReachability.NotReachable && AccountInfo.internetOption){
			string[] osmData = GetChunkBoundaries (actualChunkLat,actualChunkLon).Split("\n"[0]); 
			place = GetPlaceFromOSM (osmData,"name");
			placeID = GetPlaceFromOSM (osmData,"id");
			placeType = GetPlaceFromOSM (osmData,"type");
			placeWikiData = GetPlaceFromOSM (osmData,"wiki");
		}
		string tempString = "";// = (Random.Range((int)lat + (int)lon,(int)lat - (int)lon) + (int)(((lon%10)-(lon%1))/1)).ToString();
		//tempString += Mathf.Abs(((lat%10)-(lat%1))/1).ToString();
		//tempString += Mathf.Abs(((lon%10)-(lon%1))/1).ToString();
		string latString = "000"+Mathf.Abs(lat).ToString();
		string lonString = "000"+Mathf.Abs (lon).ToString ();
		tempString += latString.Substring(latString.Length-3);
		tempString += lonString.Substring(lonString.Length-3);
		int chunkRandom = int.Parse(tempString)+Controller.mainSeed;
		Random.InitState (chunkRandom);
		string villager = "none";
		string biome = "";
		if (Random.Range (0, 100) < 10) {
			villager = villagers [Random.Range (0, villagers.Length - 1)];
		}
		if (Random.Range (0, 1000) > 50) {
			biome = chunkBiomes [Random.Range (0, chunkBiomes.Length - 1)];
			file.WriteLine (Security.Rot39Static("biome:" + biome + ";chunkRandom:" + chunkRandom + ";place:"+place.Replace("\"",string.Empty) + ";placeID:"+placeID.Replace("\"",string.Empty)+";placeType:"+placeType+";placeWikiData:"+placeWikiData+";lastLoad:"+System.DateTime.Now.Ticks));
		} else {
			biome = rareChunkBiomes [Random.Range (0, rareChunkBiomes.Length - 1)];
			file.WriteLine (Security.Rot39Static("biome:" + biome + ";chunkRandom:" + chunkRandom + ";place:"+place.Replace("\"",string.Empty) + ";placeID:"+placeID.Replace("\"",string.Empty)+";placeType:"+placeType+";placeWikiData:"+placeWikiData+";lastLoad:"+System.DateTime.Now.Ticks));
		}
		for(int r = 0;r < 20;r++){
			for(int c = 0;c < 20;c++){
				Random.InitState (Random.Range(0,((123+c)*r)*100+(int)lat+((157+r)*c)*100+(int)lon + chunkRandom));
				int random = Random.Range (0, 1000);
				Random.InitState (chunkRandom*((123+c)*r)*100+(int)lat+((157+r)*c)*100+(int)lon+(System.DateTime.Now.Month)+System.DateTime.Now.Year*12);
				int randomOre = Random.Range (0,10000);
				Random.InitState (Random.Range(0,((123+c)*r)*100+(int)lat+((157+r)*c)*100+(int)lon + chunkRandom));
				if(biome == "wood"){
					if(random>=200){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
					}
					if(random<200&&random>=100){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:tree;stage:" + 3 + ";size:" + (int)Random.Range(1,3) + ";x:" + c + ";y:" + r));
					}
					if(random<100&&random>=10){
						file.WriteLine (Security.Rot39Static("type:water;x:"+c+";y:"+r));
					}
					if(random<10&&random>=1){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static(PlantCatalog.constructRandomPlant(chunkRandom,biome,c,r)));
					}
					if(random<1){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:placeable;x:" + c + ";y:" + r + ";item:0125;completed:false;lastused:0"));
					}
				}
				if(biome == "forest"){
					if(random>=300){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
					}
					if(random<300&&random>=50){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:tree;stage:" + 3 + ";size:" + (int)Random.Range(1,3) + ";x:" + c + ";y:" + r));
					}
					if(random<50&&random>=10){
						file.WriteLine (Security.Rot39Static("type:water;x:"+c+";y:"+r));
					}
					if(random<10&&random>=1){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static(PlantCatalog.constructRandomPlant(chunkRandom,biome,c,r)));
					}
					if(random<1){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:placeable;x:" + c + ";y:" + r + ";item:0125;completed:false;lastused:0"));
					}
				}
				if(biome == "snowyforest"){
					if(random>=300){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
					}
					if(random<300&&random>=50){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:tree;stage:" + 3 + ";size:" + (int)Random.Range(1,3) + ";x:" + c + ";y:" + r));
					}
					if(random<50&&random>=1){
						file.WriteLine (Security.Rot39Static("type:water;x:"+c+";y:"+r));
					}
					if(random<1){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:placeable;x:" + c + ";y:" + r + ";item:0125;completed:false;lastused:0"));
					}
				}
				if(biome == "plain"){
					if(random>=150){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
					}
					if(random<150&&random>=100){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:tree;stage:" + 3 + ";size:" + (int)Random.Range(1,3) + ";x:" + c + ";y:" + r));
					}
					if(random<100&&random>=10){
						file.WriteLine (Security.Rot39Static("type:water;x:"+c+";y:"+r));
					}
					if(random<10&&random>=1){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static(PlantCatalog.constructRandomPlant(chunkRandom,biome,c,r)));
					}
					if(random<1){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:placeable;x:" + c + ";y:" + r + ";item:0125;completed:false;lastused:0"));
					}
				}
				if(biome == "flowergarden"){
					if(random>=150){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
					}
					if(random<150&&random>=100){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:tree;stage:" + 3 + ";size:" + (int)Random.Range(1,3) + ";x:" + c + ";y:" + r));
					}
					if(random<100&&random>=50){
						file.WriteLine (Security.Rot39Static("type:water;x:"+c+";y:"+r));
					}
					if(random<50&&random>=1){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static(PlantCatalog.constructRandomPlant(chunkRandom,biome,c,r)));
					}
					if(random<1){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:placeable;x:" + c + ";y:" + r + ";item:0125;completed:false;lastused:0"));
					}
				}
				if(biome == "farmland"){
					if(random>=150){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
					}
					if(random<150&&random>=100){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:tree;stage:" + 3 + ";size:" + (int)Random.Range(1,3) + ";x:" + c + ";y:" + r));
					}
					if(random<100&&random>=50){
						file.WriteLine (Security.Rot39Static("type:water;x:"+c+";y:"+r));
					}
					if(random<50&&random>=1){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static(PlantCatalog.constructRandomPlant(chunkRandom,biome,c,r)));
					}
					if(random<1){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:placeable;x:" + c + ";y:" + r + ";item:0125;completed:false;lastused:0"));
					}
				}
				if(biome == "snowyplains"){
					if(random>=150){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
					}
					if(random<150&&random>=100){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:tree;stage:" + 3 + ";size:" + (int)Random.Range(1,3) + ";x:" + c + ";y:" + r));
					}
					if(random<100&&random>=1){
						file.WriteLine (Security.Rot39Static("type:water;x:"+c+";y:"+r));
					}
					if(random<1){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:placeable;x:" + c + ";y:" + r + ";item:0125;completed:false;lastused:0"));
					}
				}
				if(biome == "swamp"){
					if(random>=400){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
					}
					if(random<400&&random>=300){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:tree;stage:" + 3 + ";size:" + (int)Random.Range(1,3) + ";x:" + c + ";y:" + r));
					}
					if(random<300&&random>=25){
						file.WriteLine (Security.Rot39Static("type:water;x:"+c+";y:"+r));
					}
					if(random<25&&random>=1){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static(PlantCatalog.constructRandomPlant(chunkRandom,biome,c,r)));
					}
					if(random<1){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:placeable;x:" + c + ";y:" + r + ";item:0125;completed:false;lastused:0"));
					}
				}
				if(biome == "beach"){
					if(random>=400){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
					}
					if(random<400&&random>=380){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:tree;stage:" + 3 + ";size:" + (int)Random.Range(1,3) + ";x:" + c + ";y:" + r));
					}
					if(random<380&&random>=1){
						file.WriteLine (Security.Rot39Static("type:water;x:"+c+";y:"+r));
					}
					if(random<1){
						file.WriteLine (Security.Rot39Static("type:land;biome:"+biome+";x:" + c + ";y:" + r + GetOre(randomOre)));
						file.WriteLine (Security.Rot39Static("type:placeable;x:" + c + ";y:" + r + ";item:0125;completed:false;lastused:0"));
					}
				}
			}
		}
		file.Close ();
	}
	void LoadChunkInfo(string path,int latAlign,int lonAlign){
		string line;
		string biome = "";
		int chunkRandom = 0;
		if(Application.internetReachability != NetworkReachability.NotReachable && latAlign == 0 && lonAlign == 0 && Controller.slot != "/toybox" && AccountInfo.internetOption){
			try{
			if(timeTillFirstWeather<=0&&timeTillWeather<=0){
				string weatherReturn = GetChunkWeather (actualChunkLat, actualChunkLon);
				currentWeatherCode = GetChunkMainWeather(weatherReturn);
				currentTemperature = GetChunkTemperature(weatherReturn);
				timeTillWeather = 5f;
				lastWeatherUpdate = System.DateTime.Now.Ticks;
			}
			}catch(System.Exception e){
			}
			if(getChunkPlaceID(0,0)=="nointernetplaceid"){
				string[] osmData = GetChunkBoundaries (actualChunkLat,actualChunkLon).Split("\n"[0]); 
				changeChunkPlaceInfo(GetPlaceFromOSM (osmData,"name"),GetPlaceFromOSM (osmData,"id"),GetPlaceFromOSM (osmData,"type"),GetPlaceFromOSM (osmData,"wiki"));
			}
			if(getChunkPlaceType(0,0)!="place"&&getChunkPlaceType(0,0)!="none"){
				string[] osmData = GetChunkBoundaries (actualChunkLat,actualChunkLon).Split("\n"[0]); 
				changeChunkPlaceInfo(GetPlaceFromOSM (osmData,"name"),GetPlaceFromOSM (osmData,"id"),GetPlaceFromOSM (osmData,"type"),GetPlaceFromOSM (osmData,"wiki"));
			}
		}
		Controller.currentPlaceName = getChunkPlace (0, 0);
		Controller.currentPlaceID = getChunkPlaceID (0, 0);
		Controller.currentPlaceType = getChunkPlaceType (0, 0);
		Controller.currentPlaceWiki = getChunkPlaceWiki (0, 0);
		if(latAlign==0&&lonAlign==0){
			System.DateTime lastLoaded = new System.DateTime (getChunkLastLoad(latAlign,lonAlign));
			if((lastLoaded.Month*12)+lastLoaded.Year < (System.DateTime.Now.Month*12)+System.DateTime.Now.Year){
				replaceOre (getChunkRandom(latAlign,lonAlign));
			}
			changeChunkTick (System.DateTime.Now.Ticks);
			if (Controller.currentPlaceID != "nointernetplaceid" && Controller.currentPlaceID != "null" && Controller.currentPlaceID != "none") {
				Random.InitState ((int)long.Parse (Controller.currentPlaceID));
				Controller.cityEntityID = EntityCatalog.getEntityID (EntityCatalog.rareEntityIndexes [Random.Range (0, EntityCatalog.rareEntityIndexes.Length)]);
				Controller.cityFishID = FishCatalog.getFishID (FishCatalog.rareFishIndexes [Random.Range (0, FishCatalog.rareFishIndexes.Length)]);
			}
		}
		StreamReader theReader = new StreamReader(path, Encoding.Default);
		using(theReader){
			do{
				line = theReader.ReadLine();

				var type = "";
				var tstage = 0;
				var tsize = 0;
				var x = 0;
				var y = 0;
				var ore = 0;
				long timePlanted = 0;
				float growTime = 0;
				var special = "";
				var plantid = "";
				var item = "";
				var buildingID = "";
				var complete = false;
				long lastUsed = 0;
				string[] storageSlots = new string[]{"nullx0","nullx0","nullx0","nullx0","nullx0","nullx0","nullx0","nullx0","nullx0"};

				if(line != null){
					var attributeGroups = Security.Rot39Static(line).Split(new char[]{';'});
					for(int i = 0;i < attributeGroups.Length; i++){
						var attribute = attributeGroups[i].Split(new char[]{':'});
						if(attribute[0] == "biome"){
							biome = attribute[1];
						}
						if(attribute[0] == "chunkRandom"){
							chunkRandom = int.Parse(attribute[1]);
						}
						if(attribute[0] == "type"){
							type = attribute[1];
						}
						if(attribute[0] == "item"){
							item = attribute[1];
						}
						if(attribute[0] == "stage"){
							tstage = int.Parse(attribute[1]);
						}
						if(attribute[0] == "size"){
							tsize = int.Parse(attribute[1]);
						}
						if(attribute[0] == "x"){
							x = int.Parse(attribute[1]);
						}
						if(attribute[0] == "y"){
							y = int.Parse(attribute[1]);
						}
						if(attribute[0] == "ore"){
							ore = int.Parse(attribute[1]);
						}
						if(attribute[0] == "time"){
							timePlanted = long.Parse(attribute[1]);
						}
						if(attribute[0] == "growtime"){
							growTime = float.Parse(attribute[1]);
						}
						if(attribute[0] == "plantid"){
							plantid = attribute[1];
						}
						if(attribute[0] == "special"){
							special = attribute[1];
						}
						if(attribute[0] == "buildingid"){
							buildingID = attribute[1];
						}
						if(attribute[0] == "slot1"){
							storageSlots[0] = attribute[1];
						}
						if(attribute[0] == "slot2"){
							storageSlots[1] = attribute[1];
						}
						if(attribute[0] == "slot3"){
							storageSlots[2] = attribute[1];
						}
						if(attribute[0] == "slot4"){
							storageSlots[3] = attribute[1];
						}
						if(attribute[0] == "slot5"){
							storageSlots[4] = attribute[1];
						}
						if(attribute[0] == "slot6"){
							storageSlots[5] = attribute[1];
						}
						if(attribute[0] == "slot7"){
							storageSlots[6] = attribute[1];
						}
						if(attribute[0] == "slot8"){
							storageSlots[7] = attribute[1];
						}
						if(attribute[0] == "slot9"){
							storageSlots[8] = attribute[1];
						}
						if(attribute[0] == "completed"){
							complete = bool.Parse(attribute[1]);
						}
						if(attribute[0] == "lastused"){
							lastUsed = long.Parse(attribute[1]);
						}
					}
					GameObject recentTile;
					Tile tileScript;
					Tree treeScript;
					Hole holeScript;
					GeneralPlant plantScript;
					if(type == "land"){
						recentTile = (GameObject)Instantiate(Resources.Load("Tiles/GrassTile"),new Vector3(lonAlign + (1.9f-(x * .1f)),latAlign + (2f-(y * .1f)),10),Quaternion.identity);
						tileScript = recentTile.GetComponent<Tile>();
						tileScript.c = x;
						tileScript.r = y;
						tileScript.ore = ore;
						tileScript.biome = biome;
					}
					if(type == "water"){
						recentTile = (GameObject)Instantiate(Resources.Load("Tiles/WaterTile"),new Vector3(lonAlign + (1.9f-(x * .1f)),latAlign + (2f-(y * .1f)),10),Quaternion.identity);
						tileScript = recentTile.GetComponent<Tile>();
						tileScript.c = x;
						tileScript.r = y;
						tileScript.biome = biome;
					}
					if(type == "hole"){
						recentTile = (GameObject)Instantiate(Resources.Load("DugHole"),new Vector3(lonAlign + (1.9f-(x * .1f)),latAlign + (2f-(y * .1f)),10),Quaternion.identity);
						holeScript = recentTile.GetComponent<Hole>();
						holeScript.ore = ore;
						holeScript.c = x;
						holeScript.r = y;
					}
					if(type == "placeable"){
						recentTile = (GameObject)Instantiate(Resources.Load("Placeable/GeneralPlaced"),new Vector3(lonAlign + (1.9f-(x * .1f)),latAlign + (2f-(y * .1f)),10),Quaternion.identity);
						GeneralPlacedItem itemScript = recentTile.GetComponent<GeneralPlacedItem>();
						itemScript.c = x;
						itemScript.r = y;
						itemScript.item = item;
						itemScript.random = chunkRandom + x.ToString() + y.ToString();
						itemScript.lastUsed = lastUsed;
						itemScript.complete = complete;
					}
					if(type == "building"){
						recentTile = (GameObject)Instantiate(Resources.Load("Placeable/GeneralPlaced"),new Vector3(lonAlign + (1.9f-(x * .1f)),latAlign + (2f-(y * .1f)),10),Quaternion.identity);
						GeneralPlacedItem itemScript = recentTile.GetComponent<GeneralPlacedItem>();
						itemScript.c = x;
						itemScript.r = y;
						itemScript.item = item;
						itemScript.buildingID = buildingID;
						itemScript.storageSlots = storageSlots;
						itemScript.lastUsed = lastUsed;
					}
					if(type == "buildingsite"){
						recentTile = (GameObject)Instantiate(Resources.Load("Placeable/GeneralPlaced"),new Vector3(lonAlign + (1.9f-(x * .1f)),latAlign + (2f-(y * .1f)),10),Quaternion.identity);
						GeneralPlacedItem itemScript = recentTile.GetComponent<GeneralPlacedItem>();
						itemScript.c = x;
						itemScript.r = y;
						itemScript.item = item;
						itemScript.buildingID = buildingID;
					}
					if(type == "tree"){
						recentTile = (GameObject)Instantiate(Resources.Load("Placeable/Tree"),new Vector3(lonAlign + (1.9f-(x * .1f)),latAlign + (2f-(y * .1f)),9),Quaternion.identity);
						treeScript = recentTile.GetComponent<Tree>();
						treeScript.c = x;
						treeScript.r = y;
						treeScript.size = tsize;
						treeScript.stage = tstage;
						treeScript.tickPlanted = timePlanted;
						treeScript.biome = biome;
					}
					if(type == "plant"){
						recentTile = (GameObject)Instantiate(Resources.Load("Placeable/GeneralPlant"),new Vector3(lonAlign + (1.9f-(x * .1f)),latAlign + (2f-(y * .1f)),9),Quaternion.identity);
						plantScript = recentTile.GetComponent<GeneralPlant>();
						plantScript.c = x;
						plantScript.r = y;
						plantScript.stage = tstage;
						plantScript.tickPlanted = timePlanted;
						plantScript.special = special;
						plantScript.plantID = plantid;
						plantScript.timeMultiplier = growTime;
					}
					type = "land";
					tstage = 0;
					tsize = 0;
					x = 0;
					y = 0;
					ore = 0;
					timePlanted = 0;
					item = "";

				}
			}while (line != null);
			theReader.Close ();
		}
		if(latAlign == 0 && lonAlign == 0){
			currentChunkRandom = chunkRandom;
			currentChunkBiome = biome;
		}
		Random.InitState (chunkRandom);
		string npcName = Quests.maleNames[Random.Range (0, Quests.maleNames.Length)];
		string[] npcInfo = villagers[Random.Range(0,villagers.Length-1)].Split(':');
		float posX = Random.Range (0.1f, 1.9f);
		Random.InitState (chunkRandom*System.DateTime.Now.Year+(System.DateTime.Now.Month*40)+System.DateTime.Now.Day);
		float posY = Random.Range (0.1f, 1.9f);
		Random.InitState (chunkRandom);
		if(Random.Range(0,100)>70){
			GameObject recentNPC = (GameObject)Instantiate(Resources.Load("NPC"),new Vector3(lonAlign+posX,latAlign+posY,9),Quaternion.identity);
			NPC npcScript = recentNPC.GetComponent<NPC> ();
			npcScript.chunkRandom = chunkRandom;
			npcScript.npcClass = npcInfo [0];
			npcScript.npcQuests = npcInfo [1];
			npcScript.npcImage = int.Parse(npcInfo [2]);
			npcScript.name = npcName;
			if(latAlign == 0 && lonAlign == 0){
				npcScript.id = chunkLon + " " + chunkLat + " " + "NPC";
			}
		}
		bool regen = false;
		if(latAlign == 0 && lonAlign == 0){
			GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
			for(int i = 0;i < tiles.Length;i++){
				if(tiles[i].transform.position.x >= 0 && tiles[i].transform.position.x <= 1.9f && tiles[i].transform.position.y >= 0.1f && tiles[i].transform.position.y <= 2){
					regen = false;
				}
			}
		}
		if(regen && !regened){
			File.Delete ((MainChunkPath + chunkLat + " " + chunkLon));
			regened = true;
			chunkSetup ();
		}
	}
	void chunkSetup(){
		if (!Controller.joyStick) {
			//print (GetChunkBoundaries(actualChunkLat,actualChunkLon));
			if (chunkLat > 0 || chunkLat < 0) {
				longSize = longSizes [8];
			}
			if (chunkLat > 10000 || chunkLat < -10000) {
				longSize = longSizes [7];
			}
			if (chunkLat > 20000 || chunkLat < -20000) {
				longSize = longSizes [6];
			}
			if (chunkLat > 30000 || chunkLat < -30000) {
				longSize = longSizes [5];
			}
			if (chunkLat > 40000 || chunkLat < -40000) {
				longSize = longSizes [4];
			}
			if (chunkLat > 50000 || chunkLat < -50000) {
				longSize = longSizes [3];
			}
			if (chunkLat > 60000 || chunkLat < -60000) {
				longSize = longSizes [2];
			}
			if (chunkLat > 70000 || chunkLat < -70000) {
				longSize = longSizes [1];
			}
			if (chunkLat > 80000 || chunkLat < -80000) {
				longSize = longSizes [0];
			}
			longMultiple = 2 / longSize;
		} else {
			longSize = .001f;
			longMultiple = 2 / longSize;
		}
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
		for(int i = 0;i < tiles.Length;i++){
			Destroy(tiles[i]);
		}
		GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
		for(int i = 0;i < trees.Length;i++){
			Destroy(trees[i]);
		}
		GameObject[] holes = GameObject.FindGameObjectsWithTag("Hole");
		for(int i = 0;i < holes.Length;i++){
			Destroy(holes[i]);
		}
		GameObject[] npc = GameObject.FindGameObjectsWithTag("NPC");
		for(int i = 0;i < npc.Length;i++){
			Destroy(npc[i]);
		}
		GameObject[] items = GameObject.FindGameObjectsWithTag("ItemDrop");
		for(int i = 0;i < items.Length;i++){
			Destroy(items[i]);
		}
		GameObject[] placed = GameObject.FindGameObjectsWithTag("GeneralPlaced");
		for(int i = 0;i < placed.Length;i++){
			Destroy(placed[i]);
		}
		GameObject[] plant = GameObject.FindGameObjectsWithTag("GeneralPlant");
		for(int i = 0;i < plant.Length;i++){
			Destroy(plant[i]);
		}
		GameObject[] entity = GameObject.FindGameObjectsWithTag("Entity");
		for(int i = 0;i < entity.Length;i++){
			Destroy(entity[i]);
		}
		GameObject[] animal = GameObject.FindGameObjectsWithTag("Animal");
		for(int i = 0;i < animal.Length;i++){
			Destroy(animal[i]);
		}
		GameObject[] map = GameObject.FindGameObjectsWithTag("Map");
		for(int i = 0;i < map.Length;i++){
			Destroy(map[i]);
		}
		string tempString = "";
		string latString = "000"+Mathf.Abs(chunkLat).ToString();
		string lonString = "000"+Mathf.Abs (chunkLon).ToString ();
		tempString += latString.Substring(latString.Length-3);
		tempString += lonString.Substring(lonString.Length-3);
		int chunkRandom = int.Parse(tempString)+Controller.mainSeed;
		Random.InitState (int.Parse(chunkRandom.ToString() + System.DateTime.Now.DayOfYear.ToString()));
		var animalAmount = Random.Range (0,5);
		int animalID = 0;
		string[] animals = new string[]{
			"chicken",
			"cow",
			"pig"
		};
		for(int i = 0;i<animalAmount;i++){
			Random.InitState (i+chunkRandom + System.DateTime.Now.DayOfYear);
			animalID = (int)long.Parse(i + chunkRandom.ToString() + System.DateTime.Now.DayOfYear.ToString());
			bool gone = false;
			string lastData;
			for(int i2 = 0; i2 < DailyInfo.dailyActions.Count;i2++){
				lastData = DailyInfo.grabData ("wildAnimalGone", i2, 0);
				if(lastData == "END"){
					break;
				}
				if(lastData == animalID.ToString()){
					gone = true;
				}
			}
			if(!gone){
				var x = 0.1f * Random.Range (1,19);
				var y = 0.1f * Random.Range (2,18);
				var animalSpawn = (GameObject)Instantiate (Resources.Load ("Animal"), new Vector3 (x, y, -10), Quaternion.identity);
				Random.InitState (i+chunkRandom + System.DateTime.Now.DayOfYear);
				WanderingAnimal animalScript = animalSpawn.GetComponent<WanderingAnimal> ();
				animalScript.animal = animals[Random.Range(0,animals.Length)];
				animalScript.id = animalID;
			}
		}
		Random.InitState ((int)((System.DateTime.Now.Year+360)+(System.DateTime.Now.Month+31)+System.DateTime.Now.Day+chunkRandom));
		if(Random.Range(0,100)>80 && AccountInfo.internetOption){
			if(!QuestInfo.HasMapQuest() && Controller.slot != "/toybox"){
				Instantiate (Resources.Load ("Map"), new Vector3 (Random.Range(.5f,1.5f),Mathf.Abs(Random.Range(-.5f,-1.5f)), -20), Quaternion.identity);
			}
		}
		//CREATE MIDDLE CHUNK
		if(File.Exists(MainChunkPath + chunkLat + " " + chunkLon)){
			LoadChunkInfo((MainChunkPath + chunkLat + " " + chunkLon),0,0);
		}else{
			GenerateChunkInfo((MainChunkPath + chunkLat + " " + chunkLon),chunkLat,chunkLon,actualChunkLat,actualChunkLon);
			LoadChunkInfo((MainChunkPath + chunkLat + " " + chunkLon),0,0);
		}
		if(borderChunks){
		//CREATE LEFT CHUNK
		if(File.Exists(MainChunkPath + chunkLat + " " + (chunkLon-1))){
			LoadChunkInfo((MainChunkPath + chunkLat + " " + (chunkLon-1)),0,-2);
		}
		//CREATE RIGHT CHUNK
		if(File.Exists(MainChunkPath + chunkLat + " " + (chunkLon+1))){
			LoadChunkInfo((MainChunkPath + chunkLat + " " + (chunkLon+1)),0,2);
		}
		//CHUNK LONS NEED TO BE MODIFIED
			if((!(chunkLat < 10000 && chunkLat+1 >= 10000))&&(!(chunkLat < 20000 && chunkLat+1 >= 20000))&&(!(chunkLat < 30000 && chunkLat+1 >= 30000))&&(!(chunkLat < 40000 && chunkLat+1 >= 40000))&&(!(chunkLat < 50000 && chunkLat+1 >= 50000))&&(!(chunkLat < 60000 && chunkLat+1 >= 60000))&&(!(chunkLat < 70000 && chunkLat+1 >= 70000))&&(!(chunkLat < 80000 && chunkLat+1 >= 80000))){
				//CREATE TOP CHUNK
				if(File.Exists(MainChunkPath + (chunkLat+1) + " " + chunkLon)){
					LoadChunkInfo((MainChunkPath + (chunkLat+1) + " " + chunkLon),2,0);
				}
				//CREATE TOP LEFT CHUNK
				if(File.Exists(MainChunkPath + (chunkLat+1) + " " + (chunkLon-1))){
					LoadChunkInfo((MainChunkPath + (chunkLat+1) + " " + (chunkLon-1)),2,-2);
				}
				//CREATE TOP RIGHT CHUNK
				if(File.Exists(MainChunkPath + (chunkLat+1) + " " + (chunkLon+1))){
					LoadChunkInfo((MainChunkPath + (chunkLat+1) + " " + (chunkLon+1)),2,2);
				}
			}
			if ((!(chunkLat < -10000 && chunkLat - 1 >= -10000)) && (!(chunkLat < -20000 && chunkLat - 1 >= -20000)) && (!(chunkLat < -30000 && chunkLat - 1 >= -30000)) && (!(chunkLat < -40000 && chunkLat - 1 >= -40000)) &&(!(chunkLat < -50000 && chunkLat - 1 >= -50000)) && (!(chunkLat < -60000 && chunkLat - 1 >= -60000)) && (!(chunkLat < -70000 && chunkLat - 1 >= -70000)) && (!(chunkLat < -80000 && chunkLat - 1 >= -80000))) {
				//CREATE BOTTOM CHUNK
				if (File.Exists (MainChunkPath + (chunkLat - 1) + " " + chunkLon)) {
					LoadChunkInfo ((MainChunkPath + (chunkLat - 1) + " " + chunkLon), -2, 0);
				}
				//CREATE BOTTOM LEFT CHUNK
				if (File.Exists (MainChunkPath + (chunkLat - 1) + " " + (chunkLon - 1))) {
					LoadChunkInfo ((MainChunkPath + (chunkLat - 1) + " " + (chunkLon - 1)), -2, -2);
				}
				//CREATE BOTTOM RIGHT CHUNK
				if (File.Exists (MainChunkPath + (chunkLat - 1) + " " + (chunkLon + 1))) {
					LoadChunkInfo ((MainChunkPath + (chunkLat - 1) + " " + (chunkLon + 1)), -2, 2);
				}
			}
		}
		needToRelease = false;
		inventoryScript.needRelease = false;
		regened = false;
	}
	private IEnumerator  StartLocationService (float desiredAccuracyInMeters,float updateDistanceInMeters) {
		yield return new WaitForSeconds (5);
		if (!Input.location.isEnabledByUser) {
			yield break;
		}
		Input.location.Start(desiredAccuracyInMeters,updateDistanceInMeters);
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0){
			yield return new WaitForSeconds(1);
			maxWait--;
		}
		if (maxWait < 1){
			print("Timed out");
			yield break;
		}
		if (Input.location.status == LocationServiceStatus.Failed){
			print("Unable to determine device location");
			yield break;
		}
	}
	void DrawQuad(Rect position,Color color){
		Texture2D texture = new Texture2D (1,1);
		texture.SetPixel (0,0,color);
		texture.Apply ();
		GUI.skin.box.normal.background = texture;
		GUI.Box (position,GUIContent.none);
	}
	string GetOre(float random){
		if(random >7000 && random <= 8000){
			return ";ore:1";
		}
		if(random >8000 && random <= 9000){
			return ";ore:2";
		}
		if(random >9000 && random <= 9500){
			return ";ore:3";
		}
		if(random >9500 && random <= 9850){
			return ";ore:4";
		}
		if(random >9850 && random <= 9950){
			return ";ore:5";
		}
		if(random >9950){
			return ";ore:6";
		}
		return ";ore:0";
	}
	public void spawnRandomItem(float x, float y,string id,int odds){
		GameObject item;
		ItemDrop dropScript;
		Random.InitState (System.Environment.TickCount);
		if(Random.Range(0,100)<odds){
			item = (GameObject)Instantiate (Resources.Load ("DroppedItem"), new Vector3 (x, y, -10), Quaternion.identity);
			dropScript = item.GetComponent <ItemDrop>();
			dropScript.itemID = id;
		}
	}
	public void addToChunk(string path, string line){
		StreamWriter file = new StreamWriter (path,true);
		file.WriteLine (Security.Rot39Static(line));
		file.Close ();
		compressChunk ();
	}
	public int getBuildingsAmount(){
		int amount = 0;
		string line;
		StreamReader theReader = new StreamReader(MainChunkPath + chunkLat + " " + chunkLon, Encoding.Default);
		using (theReader) {
			while ((line = theReader.ReadLine ()) != null) {
				if(Security.Rot39Static(line).Contains("type:building")){
					amount += 1;
				}
			}
		}
		theReader.Close ();
		return amount;
	}
	public string getMaterialsAmount(int part, int xFind, int yFind){
		bool x = false;
		bool y = false;
		bool site = false;
		string partID = "";
		string partAmount = "0";
		string line;
		StreamReader theReader = new StreamReader(MainChunkPath + chunkLat + " " + chunkLon, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39Static(theReader.ReadLine ())).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "type") {
						if(attribute[1]=="buildingsite"){
							site = true;
						}
					}
					if (attribute [0] == "part"+part.ToString()) {
						var partSplit = attribute [1].Split ('x');
						partID = partSplit [0];
						partAmount = partSplit[1];
					}
					if (attribute [0] == "x") {
						if (int.Parse (attribute [1]) == xFind) {
							x = true;
						}
					}
					if (attribute [0] == "y") {
						if (int.Parse (attribute [1]) == yFind) {
							y = true;
						}
					}
				}
				if (site == true && x == true && y == true) {
					if (partID != "") {
						return partID + "x" + partAmount;
					} else {
						return "null";
					}
				}
				site = false;
				partID = "";
				partAmount = "0";
				x = false;
				y = false;
			}
		}
		theReader.Close ();
		return "null";
	}
	public void removeFromChunk(string typeFind,int xFind,int yFind){
		string line = null;
		bool type = false;
		bool x = false;
		bool y = false;
		int ore = 0;
		List<string> newText = new List<string>();
		StreamReader theReader = new StreamReader(MainChunkPath + chunkLat + " " + chunkLon, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39Static(theReader.ReadLine ())).Contains(":")) {
					var attributeGroups = line.Split (new char[]{ ';' });
					for (int i = 0; i < attributeGroups.Length; i++) {
						var attribute = attributeGroups [i].Split (new char[]{ ':' });
						if (attribute [0] == "type") {
							if (typeFind != "ore") {
								if (attribute [1] == typeFind) {
									type = true;
								}
							} else {
								if (attribute [1] == "land") {
									type = true;
								}
							}
						}
						if (attribute [0] == "x") {
							if (int.Parse (attribute [1]) == xFind) {
								x = true;
							}
						}
						if (attribute [0] == "y") {
							if (int.Parse (attribute [1]) == yFind) {
								y = true;
							}
						}
						if (attribute [0] == "ore") {
							ore = int.Parse (attribute [1]);
						}
					}
					if (type == true && x == true && y == true) {
						if (typeFind != "ore") {
						line = null;
						} else {
							line = line.Replace ("ore:" + ore, "ore:0");
						}
					}
				newText.Add(Security.Rot39Static(line));
				type = false;
				x = false;
				y = false;
				ore = 0;
				}
		}
		theReader.Close ();
		StreamWriter theWriter = new StreamWriter(MainChunkPath + chunkLat + " " + chunkLon);
		for(int i = 0;i < newText.Count; i++){
			theWriter.WriteLine (newText[i]);
		}
		theWriter.Close ();
		compressChunk ();
	}
	public void compressChunk(){
		string line = null;
		List<string> newText = new List<string>();
		StreamReader theReader = new StreamReader(MainChunkPath + chunkLat + " " + chunkLon, Encoding.Default);
		using (theReader) {
			while ((line = theReader.ReadLine ())!=null) {
				line = Security.Rot39Static(line);
				if(line.Contains(":")){
					newText.Add(Security.Rot39Static(line));
				}
			}
		}
		theReader.Close ();
		StreamWriter theWriter = new StreamWriter(MainChunkPath + chunkLat + " " + chunkLon);
		for(int i = 0;i < newText.Count; i++){
			theWriter.WriteLine (newText[i]);
		}
		theWriter.Close ();
	}
	public long getChunkLastLoad(int latAlign,int lonAlign){
		string line;
		StreamReader theReader = new StreamReader(MainChunkPath + (chunkLat+latAlign).ToString() + " " + (chunkLon+lonAlign).ToString(), Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39Static(theReader.ReadLine ())).Contains(":")) {
				if(line.Contains("lastLoad:")){
					string[] attributes = line.Split (';');
					for(int i = 0;i<attributes.Length;i++){
						string[] attribute = attributes [i].Split (':');
						if(attribute[0] == "lastLoad"){
							return long.Parse(attribute [1]);
						}
					}
				}
			}
		}
		theReader.Close ();
		return System.DateTime.Now.Ticks + 9999999;
	}
	public int getChunkRandom(int latAlign,int lonAlign){
		string line;
		StreamReader theReader = new StreamReader(MainChunkPath + (chunkLat+latAlign).ToString() + " " + (chunkLon+lonAlign).ToString(), Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39Static(theReader.ReadLine ())).Contains(":")) {
				if(line.Contains("chunkRandom:")){
					string[] attributes = line.Split (';');
					for(int i = 0;i<attributes.Length;i++){
						string[] attribute = attributes [i].Split (':');
						if(attribute[0] == "chunkRandom"){
							return int.Parse(attribute [1]);
						}
					}
				}
			}
		}
		return 0;
		theReader.Close ();
	}
	public string getChunkPlace(int latAlign,int lonAlign){
		string line;
		if(!File.Exists(MainChunkPath + (chunkLat+latAlign).ToString() + " " + (chunkLon+lonAlign).ToString())){
			return("null");
		}
		StreamReader theReader = new StreamReader(MainChunkPath + (chunkLat+latAlign).ToString() + " " + (chunkLon+lonAlign).ToString(), Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39Static(theReader.ReadLine ())).Contains(":")) {
				if(line.Contains("place:")){
					string[] attributes = line.Split (';');
					for(int i = 0;i<attributes.Length;i++){
						string[] attribute = attributes [i].Split (':');
						if(attribute[0] == "place"){
							return attribute [1];
						}
					}
				}
			}
		}
		return "null";
		theReader.Close ();
	}
	public string getChunkPlaceType(int latAlign,int lonAlign){
		string line;
		if(!File.Exists(MainChunkPath + (chunkLat+latAlign).ToString() + " " + (chunkLon+lonAlign).ToString())){
			return("null");
		}
		StreamReader theReader = new StreamReader(MainChunkPath + (chunkLat+latAlign).ToString() + " " + (chunkLon+lonAlign).ToString(), Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39Static(theReader.ReadLine ())).Contains(":")) {
				if(line.Contains("placeType:")){
					string[] attributes = line.Split (';');
					for(int i = 0;i<attributes.Length;i++){
						string[] attribute = attributes [i].Split (':');
						if(attribute[0] == "placeType"){
							return attribute [1];
						}
					}
				}
			}
		}
		theReader.Close ();
		return "null";
	}
	public string getChunkPlaceID(int latAlign,int lonAlign){
		string line;
		if(!File.Exists(MainChunkPath + (chunkLat+latAlign).ToString() + " " + (chunkLon+lonAlign).ToString())){
			return("null");
		}
		StreamReader theReader = new StreamReader(MainChunkPath + (chunkLat+latAlign).ToString() + " " + (chunkLon+lonAlign).ToString(), Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39Static(theReader.ReadLine ())).Contains(":")) {
				if(line.Contains("placeID:")){
					string[] attributes = line.Split (';');
					for(int i = 0;i<attributes.Length;i++){
						string[] attribute = attributes [i].Split (':');
						if(attribute[0] == "placeID"){
							return attribute [1];
						}
					}
				}
			}
		}
		theReader.Close ();
		return "null";
	}
	public string getChunkPlaceWiki(int latAlign,int lonAlign){
		string line;
		if(!File.Exists(MainChunkPath + (chunkLat+latAlign).ToString() + " " + (chunkLon+lonAlign).ToString())){
			return("null");
		}
		StreamReader theReader = new StreamReader(MainChunkPath + (chunkLat+latAlign).ToString() + " " + (chunkLon+lonAlign).ToString(), Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39Static(theReader.ReadLine ())).Contains(":")) {
				if(line.Contains("placeWikiData:")){
					string[] attributes = line.Split (';');
					for(int i = 0;i<attributes.Length;i++){
						string[] attribute = attributes [i].Split (':');
						if(attribute[0] == "placeWikiData"){
							return attribute [1];
						}
					}
				}
			}
		}
		theReader.Close ();
		return "null";
	}
	public void changeChunkPlaceInfo(string city,string placeID,string placeType,string placeWiki){
		string line;
		List<string> newText = new List<string>();
		StreamReader theReader = new StreamReader(MainChunkPath + chunkLat + " " + chunkLon, Encoding.Default);
		using (theReader) {
			while ((line = theReader.ReadLine ()) != null) {
				if (line.Contains (Security.Rot39Static("placeID"))) {
					line.Replace (Security.Rot39Static("nointernetcity"), Security.Rot39Static(city));
					line.Replace (Security.Rot39Static("nointernetplaceid"), Security.Rot39Static(placeID));
					line.Replace (Security.Rot39Static("nointernetplacetype"), Security.Rot39Static(placeType));
					line.Replace (Security.Rot39Static("null"), Security.Rot39Static(placeWiki));
					newText.Add (line);
				}
				newText.Add (line);
			}
		}
		theReader.Close ();
		StreamWriter theWriter = new StreamWriter(MainChunkPath + chunkLat + " " + chunkLon);
		for(int i = 0;i < newText.Count; i++){
			theWriter.WriteLine (newText[i]);
		}
		theWriter.Close ();
	}
	public void changeChunkTick(long tick){
		string line;
		string newLine = "";
		List<string> newText = new List<string>();
		StreamReader theReader = new StreamReader(MainChunkPath + chunkLat + " " + chunkLon, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39Static(theReader.ReadLine ())).Contains(":")) {
				newLine = "";
				string[] attributes = line.Split (';');
				for(int i = 0;i<attributes.Length;i++){
					string[] attribute = attributes [i].Split (':');
					newLine += attribute[0];
					newLine += ":";
					if (attribute [0] == "lastLoad") {
						newLine += tick;
					} else {
						newLine += attribute[1];
					}
					if(i<attributes.Length-1){
						newLine += ";";
					}
				}
				newText.Add (Security.Rot39Static(newLine));
			}
		}
		theReader.Close ();
		StreamWriter theWriter = new StreamWriter(MainChunkPath + chunkLat + " " + chunkLon);
		for(int i = 0;i < newText.Count; i++){
			theWriter.WriteLine (newText[i]);
		}
		theWriter.Close ();
	}
	public void replaceOre(int chunkRandom){
		string line;
		string newLine = "";
		bool land = false;
		bool hole = false;
		int x = 0;
		int y = 0;
		List<string> newText = new List<string>();
		StreamReader theReader = new StreamReader(MainChunkPath + chunkLat + " " + chunkLon, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39Static(theReader.ReadLine ())).Contains(":")) {
				newLine = "";
				land = false;
				hole = false;
				x = 0;
				y = 0;
				string[] attributes = line.Split (';');
				for(int i = 0;i<attributes.Length;i++){
					string[] attribute = attributes [i].Split (':');
					newLine += attribute[0];
					newLine += ":";
					if (attribute [0] == "type") {
						if(attribute[1] == "land"){
							land = true;
						}
						if(attribute[1] == "hole"){
							hole = true;
						}
					}
					if (attribute [0] == "x") {
						x = int.Parse (attribute[1]);
					}
					if (attribute [0] == "y") {
						y = int.Parse (attribute[1]);
					}
					if (attribute [0] == "ore") {
						Random.InitState (chunkRandom*((123 + x) * y) * 100 + (int)chunkLat + ((157 + y) * x) * 100 + (int)chunkLon + (System.DateTime.Now.Month) + System.DateTime.Now.Year*12);
						int randomOre = Random.Range (0, 10000);
						newLine += int.Parse(Regex.Replace(GetOre (randomOre),"[^0-9]",""));
					} else {
						newLine += attribute [1];
					}
					if(i<attributes.Length-1){
						newLine += ";";
					}
				}
				if(!hole){
					newText.Add (Security.Rot39Static(newLine));
				}
			}
		}
		theReader.Close ();
		StreamWriter theWriter = new StreamWriter(MainChunkPath + chunkLat + " " + chunkLon);
		for(int i = 0;i < newText.Count; i++){
			theWriter.WriteLine (newText[i]);
		}
		theWriter.Close ();
	}
	public string GetChunkBoundaries(float lat,float lon){
		if (Application.internetReachability != NetworkReachability.NotReachable && AccountInfo.internetOption) {
			WWW www;
			www = new WWW ("https://overpass-api.de/api/interpreter?data=is_in%28"+(lat-.0005f).ToString()+"%2C%20"+(lon+.0005f).ToString()+"%29%3B%0Aout%3B");
			StartCoroutine(WaitForWWW(www));
			while (!www.isDone) {  }
			return www.text;
		} else {
			return "nointernet";
		}
	}
	public string GetChunkWeather(float lat,float lon){
		if (Application.internetReachability != NetworkReachability.NotReachable && AccountInfo.internetOption) {
			WWW www;
			www = new WWW ("http://api.openweathermap.org/data/2.5/weather?lat="+(lat-.0005f).ToString()+"&lon="+(lon+.0005f).ToString()+"&appid=REDACTED&mode=xml&units=imperial");
			StartCoroutine(WaitForWWW(www));
			while (!www.isDone) {  }
			string[] newString = www.text.Split('>');
			string finalString = "";
			for(int i = 0;i<newString.Length;i++){
				if (i < newString.Length - 1) {
					if(!newString[i].Contains("xml version") && !newString[i].Contains("encoding")){
						finalString += newString [i].Replace ("<","").Replace ("/","").Replace (" ",":") + ":";
					}
				} else {
					finalString += newString [i].Replace ("<","").Replace ("/","").Replace (" ",":");
				}
			}
			return finalString;
		} else {
			return "999";
		}
	}
	public string GetChunkMainWeather(string chunkWeather){
		string currentSearch = "";
		string[] splitString = chunkWeather.Split (':');
		for(int i = 0;i < splitString.Length; i++){
			string[] equalSplit = splitString [i].Split ('=');
			if (equalSplit.Length == 1) {
				currentSearch = equalSplit [0];
			} else {
				if(currentSearch == "weather"){
					if(equalSplit[0] == "number"){
						return equalSplit[1].Replace("\"","");
					}
				}
			}
		}
		return "000";
	}
	public float GetChunkTemperature(string chunkWeather){
		string currentSearch = "";
		string[] splitString = chunkWeather.Split (':');
		for(int i = 0;i < splitString.Length; i++){
			string[] equalSplit = splitString [i].Split ('=');
			if (equalSplit.Length == 1) {
				currentSearch = equalSplit [0];
			} else {
				if(currentSearch == "temperature"){
					if(equalSplit[0] == "value"){
						return float.Parse(equalSplit[1].Replace("\"",""));
					}
				}
			}
		}
		return 70.0f;
	}
	IEnumerator WaitForWWW(WWW www)
	{
		yield return www;
	}
	public void SetLocation(){
		if(!Controller.joyStick){
			if (AccountInfo.locationMode == 0) {
				lastLon = Input.location.lastData.longitude;
				lastLat = Input.location.lastData.latitude;
				if (Input.location.lastData.longitude < 0) {
					chunkLon = (int)((Input.location.lastData.longitude) / longSize);
					actualChunkLon = (Input.location.lastData.longitude);
					actualChunkLon = float.Parse ((actualChunkLon + .0005f).ToString ("F3"));
				}
				if (Input.location.lastData.longitude > 0) {
					chunkLon = (int)((Input.location.lastData.longitude) / longSize);
					actualChunkLon = (Input.location.lastData.longitude);
					actualChunkLon = float.Parse ((actualChunkLon + .0005f).ToString ("F3"));
				}
				if (Input.location.lastData.latitude < 0) {
					chunkLat = (int)((Input.location.lastData.latitude) / .001f);
					actualChunkLat = (Input.location.lastData.latitude);
					actualChunkLat = float.Parse ((actualChunkLat - .0005f).ToString ("F3"));
				}
				if (Input.location.lastData.latitude > 0) {
					chunkLat = (int)((Input.location.lastData.latitude) / .001f);
					actualChunkLat = (Input.location.lastData.latitude);
					actualChunkLat = float.Parse ((actualChunkLat - .0005f).ToString ("F3"));
				}
			} else {
				lastLat = Input.location.lastData.latitude;
				lastLon = Controller.getDistanceFromLatLonInKm(lastLat,0,lastLat,Input.location.lastData.longitude);
				if (Input.location.lastData.longitude < 0) {
					chunkLon = 0-(int)lastLon;
					actualChunkLon = float.Parse ((chunkLon + .0005f).ToString ("F3"));
				}
				if (Input.location.lastData.longitude > 0) {
					chunkLon = (int)lastLon;
					actualChunkLon = float.Parse ((chunkLon + .0005f).ToString ("F3"));
				}
				if (Input.location.lastData.latitude < 0) {
					chunkLat = (int)((Input.location.lastData.latitude) / .001f);
					actualChunkLat = (Input.location.lastData.latitude);
					actualChunkLat = float.Parse ((chunkLat - .0005f).ToString ("F3"));
				}
				if (Input.location.lastData.latitude > 0) {
					chunkLat = (int)((Input.location.lastData.latitude) / .001f);
					actualChunkLat = (Input.location.lastData.latitude);
					actualChunkLat = float.Parse ((chunkLat - .0005f).ToString ("F3"));
				}
			}
		}
		if(Controller.joyStick){
			lastLon = Controller.joyLon;
			lastLat = Controller.joyLat;
			if(Controller.joyLon<0){
				chunkLon = (int)((Controller.joyLon)/longSize);
				actualChunkLon = (Controller.joyLon);
				actualChunkLon = float.Parse((actualChunkLon+.0005f).ToString ("F3"));
			}
			if(Controller.joyLon>0){
				chunkLon = (int)((Controller.joyLon)/longSize);
				actualChunkLon = (Controller.joyLon);
				actualChunkLon = float.Parse((actualChunkLon+.0005f).ToString ("F3"));
			}
			if (Controller.joyLat < 0) {
				chunkLat = (int)((Controller.joyLat) / .001f);
				actualChunkLat = (Controller.joyLat);
				actualChunkLat = float.Parse((actualChunkLat-.0005f).ToString ("F3"));
			}
			if (Controller.joyLat > 0) {
				chunkLat = (int)((Controller.joyLat) / .001f);
				actualChunkLat = (Controller.joyLat);
				actualChunkLat = float.Parse((actualChunkLat-.0005f).ToString ("F3"));
			}
		}
	}
	public string GetPlaceFromOSM(string[] osmData,string returnInfo){
		var mainPlaceType = 0;
		var mainPlaceName = "";
		var mainPlaceID = "";
		var mainPlaceWiki = "null";
		var placeType = 0;
		var placeName = "";
		var placeID = "";
		var placeWiki = "null";
		for(int i = 0;i<osmData.Length;i++){
			string line = osmData [i];
			if(line.Contains("k=\"admin_level\"")){
				string splitLine = line.Split (new string[]{"v="},System.StringSplitOptions.None)[1];
				splitLine = splitLine.Substring (0,splitLine.Length-2).Replace("\"","");
				if(splitLine == "8"){
					placeType = 4;
				}
			}
			if(line.Contains("<area id=")){
				placeID = Regex.Replace(line,"[^0-9]","");
			}
			if(line.Contains("k=\"name\"")){
				string splitLine = line.Split (new string[]{"v="},System.StringSplitOptions.None)[1];
				placeName = splitLine.Substring (0,splitLine.Length-2).Replace("\"","");
			}
			if(line.Contains("k=\"wikidata\"")){
				string splitLine = line.Split (new string[]{"v="},System.StringSplitOptions.None)[1];
				placeWiki = splitLine.Substring (0,splitLine.Length-2).Replace("\"","");
			}
			if (line.Contains ("</area>")) {
				if(placeType > mainPlaceType){
					mainPlaceType = placeType;
					mainPlaceName = placeName;
					mainPlaceID = placeID;
					mainPlaceWiki = placeWiki;
				}
				placeType = 0;
				placeName = "";
				placeID = "";
			}
		}
		if (mainPlaceType == 0) {
			return "none";
		} else {
			if (returnInfo == "name") {
				return mainPlaceName;
			} else if (returnInfo == "type") {
				string[] returnValues = new string[]{"none","village","town","city","place"};
				return returnValues[mainPlaceType];
			} else if(returnInfo == "id"){
				return mainPlaceID;
			}else{
				return mainPlaceWiki;
			}
		}
	}
}
