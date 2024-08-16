using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Linq;

public class Inventory : MonoBehaviour {

	public static string[] backpack = new string[]{"","","","","","","","","","","","","","","","","","","",""};
	private string[] backpackPrev = new string[]{"0","","","","","","","","","","","","","","","","","","",""};
	public static int hotbarItem;
	public static int tool;
	public Texture2D hotbarBox;
	public bool needRelease = true;
	public Texture2D inventoryButton;
	public Texture2D craftButton;
	public Texture2D skillButton;
	public Texture2D questButton;
	public Texture2D petButton;
	public Texture2D plusButton;
	public Texture2D exitButton;
	private float UISize = .5f;
	private float UILandscapeSize = .5f;
	private float UIPortraitSize = .5f;
	public GameObject inventoryScreen;
	public GameObject craftingScreen;
	public GameObject questScreen;
	public GameObject questLogScreen;
	public GameObject skillScreen;
	public GameObject petScreen;
	public GameObject mapScreen;
	public GameObject extraScreen;
	public GameObject giftScreen;
	public GameObject petViewScreen;
	public GameObject buildingScreen;
	public GameObject buildingMaterialScreen;
	public GameObject shedScreen;
	public GameObject placeScreen;
	public GameObject dailyShopScreen;
	public GameObject coinShopScreen;
	public GameObject petSummonScreen;
	public FishingScript fishingScript;
	public BattleScreen battleScript;
	public GameObject fishingScreen;
	public GameObject battleScreen;
	public GameObject customizeScreen;
	public GameObject joyPad;
	public GameObject joyPadLandscape;
	public Texture2D hotbarSelect;
	public static Texture2D hotbarImage1;
	public static Texture2D hotbarImage2;
	public static Texture2D hotbarImage3;
	public static Texture2D hotbarImage4;
	public static Texture2D hotbarImage5;
	public Texture2D coinDisplay;
	public Texture2D healthDisplay;
	public Font hudFont;
	public GUIStyle hudStyle;
	public GUIStyle itemAmountStyle;
	public static string craftingSection;
	public bool window;
	public bool blockExit;

	void Start(){
		needRelease = true;
		//UISize = UISize / 2;
		hudStyle.fontSize = Mathf.RoundToInt(40.0f * UISize);
		itemAmountStyle.fontSize = Mathf.RoundToInt(50.0f * UISize);
		if (File.Exists (Application.persistentDataPath + Controller.slot + "/backpack")) {
			loadBackpack ();
		}
		//addItem ("0181",1);
		//addItem ("0182",1);
		//addItem ("0183",1);
		//addItem ("0184",1);
	}
	
	// Update is called once per frame
	void Update () {
		if(AccountInfo.firstCustomize == false && CameraLocation.loading<=0 && (Input.location.status == LocationServiceStatus.Running)){
			window = true;
			customizeScreen.SetActive (true);
		}
		if ((Controller.joyStick && (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown))&&!window) {
			joyPad.SetActive (true);
		} else {
			joyPad.SetActive (false);
		}
		if ((Controller.joyStick && (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight))&&!window) {
			joyPadLandscape.SetActive (true);
		} else {
			joyPadLandscape.SetActive (false);
		}
		hudStyle.fontSize = Mathf.RoundToInt(40.0f * UISize);
		itemAmountStyle.fontSize = Mathf.RoundToInt(50.0f * UISize);
		loadHotbarImages ();
		UIPortraitSize = Screen.height / 1200.0f;
		UILandscapeSize = Screen.width / 1200.0f;
		if(Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown){
			UISize = UIPortraitSize;
		}
		if(Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight){
			UISize = UIPortraitSize;
		}
		if(backpackChanged()){
			saveBackpack ();
		}
		for (int i = 0; i < Input.touchCount; i++) {
			if(!needRelease  && !(Skills.levelUpDisplay>0) &&CameraController.camera==0){
				if(Input.touches[i].position.x > Screen.width-5-48*3*UISize){
					if((Input.touches[i].position.y < Screen.height/2-90*4*UISize) && (Input.touches[i].position.y > Screen.height/2-90*4*UISize-48*3*UISize)){
						hotbarItem = 4;
						needRelease = true;
					}
					if((Input.touches[i].position.y < Screen.height/2-90*2*UISize) && (Input.touches[i].position.y > Screen.height/2-90*2*UISize-48*3*UISize)){
						hotbarItem = 3;
						needRelease = true;
					}
					if((Input.touches[i].position.y < Screen.height/2) && (Input.touches[i].position.y > Screen.height/2-48*3*UISize)){
						hotbarItem = 2;
						needRelease = true;
					}
					if((Input.touches[i].position.y < Screen.height/2+90*2*UISize) && (Input.touches[i].position.y > Screen.height/2+90*2*UISize-48*3*UISize)){
						hotbarItem = 1;
						needRelease = true;
					}
					if((Input.touches[i].position.y < Screen.height/2+90*4*UISize) && (Input.touches[i].position.y > Screen.height/2+90*4*UISize-48*3*UISize)){
						hotbarItem = 0;
						needRelease = true;
					}
				}
				if(!window && !needRelease){
					if (Input.touches [i].position.x < 5 + 48 * 3*UISize) {
						if((Input.touches[i].position.y < Screen.height/2+90*4*UISize) && (Input.touches[i].position.y > Screen.height/2+90*4*UISize-48*3*UISize)){
							inventoryScreen.SetActive (true);
							Instantiate (Resources.Load ("Effects/BookCloseSound"), new Vector3 (Input.touches[i].position.x + .05f, Input.touches[i].position.y - .05f, 0), Quaternion.identity);
							window = true;
							needRelease = true;
							CameraLocation.needToRelease = true;
						}
						if((Input.touches[i].position.y < Screen.height/2+90*2*UISize) && (Input.touches[i].position.y > Screen.height/2+90*2*UISize-48*3*UISize)){
							openCraftingWindow ("bareHand");
							Instantiate (Resources.Load ("Effects/BookOpenSound"), new Vector3 (Input.touches[i].position.x + .05f, Input.touches[i].position.y - .05f, 0), Quaternion.identity);
						}
						if((Input.touches[i].position.y < Screen.height/2) && (Input.touches[i].position.y > Screen.height/2-48*3*UISize)){
							skillScreen.SetActive (true);
							Instantiate (Resources.Load ("Effects/BookOpenSound"), new Vector3 (Input.touches[i].position.x + .05f, Input.touches[i].position.y - .05f, 0), Quaternion.identity);
							window = true;
							needRelease = true;
							CameraLocation.needToRelease = true;
						}
						if((Input.touches[i].position.y < Screen.height/2-90*2*UISize) && (Input.touches[i].position.y > Screen.height/2-90*2*UISize-48*3*UISize)){
							questLogScreen.SetActive (true);
							Instantiate (Resources.Load ("Effects/PageSound"), new Vector3 (Input.touches[i].position.x + .05f, Input.touches[i].position.y - .05f, 0), Quaternion.identity);
							window = true;
							needRelease = true;
							CameraLocation.needToRelease = true;
						}
						if((Input.touches[i].position.y < Screen.height/2-90*4*UISize) && (Input.touches[i].position.y > Screen.height/2-90*4*UISize-48*3*UISize)){
							extraScreen.SetActive (true);
							Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (Input.touches[i].position.x + .05f, Input.touches[i].position.y - .05f, 0), Quaternion.identity);
							window = true;
							needRelease = true;
							CameraLocation.needToRelease = true;
						}
					}
				}
				if((window) && !needRelease && !petViewScreen.activeSelf && !blockExit){
					if (Input.touches [i].position.x < 5 + 48 * 3*UISize) {
						if (Controller.isPortrait ()) {
							if ((Input.touches [i].position.y < Screen.height / 2 + 90 * 6 * UISize) && (Input.touches [i].position.y > Screen.height / 2 + 90 * 6 * UISize - 48 * 3 * UISize)) {
								closeWindows ();
								CameraLocation.mapZoomedOut = false;
								Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (Input.touches[i].position.x + .05f, Input.touches[i].position.y - .05f, 0), Quaternion.identity);
							}
						}
						if (Controller.isLandscape ()) {
							if ((Input.touches [i].position.y < Screen.height / 2 + 90 * 4 * UISize) && (Input.touches [i].position.y > Screen.height / 2 + 90 * 4 * UISize - 48 * 3 * UISize)) {
								closeWindows ();
								CameraLocation.mapZoomedOut = false;
								Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (Input.touches[i].position.x + .05f, Input.touches[i].position.y - .05f, 0), Quaternion.identity);
							}
						}
					}
				}
			}
			if(Input.GetTouch(i).phase == TouchPhase.Ended){
				needRelease = false;
			}
		}
	}
	void OnGUI(){
		if((Input.location.status == LocationServiceStatus.Running||Controller.joyStick) && !window && CameraLocation.loading<=0 && !(Skills.levelUpDisplay>0)&&CameraController.camera==0){
			GUI.DrawTexture (new Rect((Screen.width)-5,Screen.height/2-90*4*UISize,-48*3*UISize,48*3*UISize),hotbarBox);
				if(Inventory.hotbarItem == 0){
				GUI.DrawTexture (new Rect((Screen.width)-5,Screen.height/2-90*4*UISize,-48*3*UISize,48*3*UISize),hotbarSelect);
				}
				if(backpack[0]!=""){
				GUI.DrawTexture (new Rect((Screen.width)-5,Screen.height/2-90*4*UISize,-48*3*UISize,48*3*UISize),hotbarImage1);
				GUI.Label (new Rect ((Screen.width)-5+(16*3*UISize),Screen.height/2-90*4*UISize+(16*3*UISize),-48*3*UISize,48*3*UISize), Inventory.getItemQuantity(0).ToString(),itemAmountStyle);
				}
			GUI.DrawTexture (new Rect((Screen.width)-5,Screen.height/2-90*2*UISize,-48*3*UISize,48*3*UISize),hotbarBox);
				if(Inventory.hotbarItem == 1){
				GUI.DrawTexture (new Rect((Screen.width)-5,Screen.height/2-90*2*UISize,-48*3*UISize,48*3*UISize),hotbarSelect);
				}
				if(backpack[1]!=""){
				GUI.DrawTexture (new Rect((Screen.width)-5,Screen.height/2-90*2*UISize,-48*3*UISize,48*3*UISize),hotbarImage2);
				GUI.Label (new Rect ((Screen.width)-5+(16*3*UISize),Screen.height/2-90*2*UISize+(16*3*UISize),-48*3*UISize,48*3*UISize), Inventory.getItemQuantity(1).ToString(),itemAmountStyle);
				}
			GUI.DrawTexture (new Rect((Screen.width)-5,Screen.height/2,-48*3*UISize,48*3*UISize),hotbarBox);
				if(Inventory.hotbarItem == 2){
				GUI.DrawTexture (new Rect((Screen.width)-5,Screen.height/2,-48*3*UISize,48*3*UISize),hotbarSelect);
				}
				if(backpack[2]!=""){
				GUI.DrawTexture (new Rect((Screen.width)-5,Screen.height/2,-48*3*UISize,48*3*UISize),hotbarImage3);
				GUI.Label (new Rect ((Screen.width)-5+(16*3*UISize),Screen.height/2+(16*3*UISize),-48*3*UISize,48*3*UISize), Inventory.getItemQuantity(2).ToString(),itemAmountStyle);
				}
			GUI.DrawTexture (new Rect((Screen.width)-5,Screen.height/2+90*2*UISize,-48*3*UISize,48*3*UISize),hotbarBox);
				if(Inventory.hotbarItem == 3){
				GUI.DrawTexture (new Rect((Screen.width)-5,Screen.height/2+90*2*UISize,-48*3*UISize,48*3*UISize),hotbarSelect);
				}
				if(backpack[3]!=""){
				GUI.DrawTexture (new Rect((Screen.width)-5,Screen.height/2+90*2*UISize,-48*3*UISize,48*3*UISize),hotbarImage4);
				GUI.Label (new Rect ((Screen.width)-5+(16*3*UISize),Screen.height/2+90*2*UISize+(16*3*UISize),-48*3*UISize,48*3*UISize), Inventory.getItemQuantity(3).ToString(),itemAmountStyle);
				}
			GUI.DrawTexture (new Rect((Screen.width)-5,Screen.height/2+90*4*UISize,-48*3*UISize,48*3*UISize),hotbarBox);
				if(Inventory.hotbarItem == 4){
				GUI.DrawTexture (new Rect((Screen.width)-5,Screen.height/2+90*4*UISize,-48*3*UISize,48*3*UISize),hotbarSelect);
				}
				if(backpack[4]!=""){
				GUI.DrawTexture (new Rect((Screen.width)-5,Screen.height/2+90*4*UISize,-48*3*UISize,48*3*UISize),hotbarImage5);
				GUI.Label (new Rect ((Screen.width)-5+(16*3*UISize),Screen.height/2+90*4*UISize+(16*3*UISize),-48*3*UISize,48*3*UISize), Inventory.getItemQuantity(4).ToString(),itemAmountStyle);
				}
		}
		if(!window && (Input.location.status == LocationServiceStatus.Running || Controller.joyStick) && CameraLocation.loading<=0){
			if(CameraController.camera!=2){
				GUI.DrawTexture (new Rect(5 * UISize,10*UISize,128*2f*UISize,56*2f*UISize),coinDisplay);
				GUI.Label (new Rect (110 * UISize, 10 * UISize, 128 * 2f * UISize, 56 * 2f * UISize), AccountInfo.accountCoins.ToString(),hudStyle);
			}
			if(CameraController.camera==0){
				GUI.DrawTexture (new Rect(305 * UISize,10*UISize,128*2f*UISize,56*2f*UISize),healthDisplay);
				GUI.Label (new Rect (410 * UISize, 10 * UISize, 128 * 2f * UISize, 56 * 2f * UISize), Skills.currentHealth.ToString()+"/"+Skills.maxHealth.ToString(),hudStyle);
				if(!(Skills.levelUpDisplay>0)){
					GUI.DrawTexture (new Rect(5,Screen.height/2-90*4*UISize,48*3*UISize,48*3*UISize),inventoryButton);
					GUI.DrawTexture (new Rect(5,Screen.height/2-90*2*UISize,48*3*UISize,48*3*UISize),craftButton);
					GUI.DrawTexture (new Rect(5,Screen.height/2,48*3*UISize,48*3*UISize),skillButton);
					GUI.DrawTexture (new Rect(5,Screen.height/2+90*2*UISize,48*3*UISize,48*3*UISize),questButton);
					GUI.DrawTexture (new Rect(5,Screen.height/2+90*4*UISize,48*3*UISize,48*3*UISize),plusButton);
				}
			}
		}
		if(window && !petViewScreen.activeSelf && !(Skills.levelUpDisplay>0) && !blockExit){
			if(Controller.isPortrait()){
				GUI.DrawTexture (new Rect(5,Screen.height/2-90*6*UISize,48*3*UISize,48*3*UISize),exitButton);
			}
			if(Controller.isLandscape()){
				GUI.DrawTexture (new Rect(5,Screen.height/2-90*4*UISize,48*3*UISize,48*3*UISize),exitButton);
			}
		}
	}
	public static int checkHeldForTypeAndPwr(string type,int pwr){
		string id = getSlotItemID(hotbarItem);
		string itemType = ItemCatalog.getItemType(id);
		int itemPwr = ItemCatalog.getItemPower(id);
		if(itemType == type && itemPwr >= pwr && id != "null"){
			return itemPwr;
		}
		return -1;
	}
	public static bool checkHeldEmpty(){
		if(backpack[hotbarItem] == ""){
			return true;
		}
		return false;
	}
	public static void decreaseDurForSlot(int slot){
		bool nothing = false;
		string item = backpack[slot];
		string[] attributeList = item.Split (';');
		string newItem = "";
		for(int i = 0;i<attributeList.Length;i++){
			string[] attribute = attributeList [i].Split (':');
			if (attribute [0] == "dur") {
				newItem += "dur:" + (int.Parse (attribute [1]) - 1).ToString ();
				if(((int.Parse (attribute [1]))-1)<=0){
					nothing = true;
				}
			} else {
				newItem += attribute [0] + ":" + attribute [1];
			}
			if(i<attributeList.Length-1){
				newItem += ";";
			}
		}
		if (!nothing) {
			backpack [slot] = newItem;
		} else {
			backpack [slot] = "";
		}
	}
	public static void changeQtyForSlot(int slot,int amount){
		bool nothing = false;
		string item = backpack[slot];
		string[] attributeList = item.Split (';');
		string newItem = "";
		for(int i = 0;i<attributeList.Length;i++){
			string[] attribute = attributeList [i].Split (':');
			if (attribute [0] == "qty") {
				newItem += "qty:" + (int.Parse (attribute [1]) + amount).ToString ();
				if((int.Parse (attribute [1])+amount)<=0){
					nothing = true;
				}
			} else {
				newItem += attribute [0] + ":" + attribute [1];
			}
			if(i<attributeList.Length-1){
				newItem += ";";
			}
		}
		if (!nothing) {
			backpack [slot] = newItem;
		} else {
			backpack [slot] = "";
		}
	}
	public static void changeChargesForSlot(int slot,int amount){
		bool nothing = false;
		string item = backpack[slot];
		string[] attributeList = item.Split (';');
		string newItem = "";
		for(int i = 0;i<attributeList.Length;i++){
			string[] attribute = attributeList [i].Split (':');
			if (attribute [0] == "charges") {
				if ((int.Parse (attribute [1]) - amount) >= 0) {
					newItem += "charges:" + (int.Parse (attribute [1]) - amount).ToString ();
				} else {
					newItem += "charges:" + (int.Parse (attribute [1])).ToString ();
				}
			} else {
				newItem += attribute [0] + ":" + attribute [1];
			}
			if(i<attributeList.Length-1){
				newItem += ";";
			}
		}
		backpack [slot] = newItem;
	}
	public static int getEmptySlot(){
		for(int i = 0;i<backpack.Length;i++){
			if(!backpack[i].Contains("id")){
				return i;
			}
		}
		print ("Couldn't find empty slot...");
		return -1;
	}
	public static int getCrateSlot(){
		for(int i = 0;i<backpack.Length;i++){
			if(backpack[i].Contains("id:0129")){
				return i;
			}
		}
		print ("Couldn't find empty slot...");
		return -1;
	}
	public static int getCrateAmount(){
		int amount = 0;
		for(int i = 0;i<backpack.Length;i++){
			if(backpack[i].Contains("id:0129")){
				amount+= 1;
			}
		}
		print ("Couldn't find empty slot...");
		return amount;
	}
	public static void addItem(string id,int amount){
		for(int a = 0; a < amount; a++){
			bool finished = false;
			for(int i = 0;i<backpack.Length;i++){
					if (backpack [i].Contains ("id:" + id) && !finished) {
						string[] attributeList = backpack [i].Split (';');
						for (int i2 = 0; i2 < attributeList.Length; i2++) {
							string[] attribute = attributeList [i2].Split (':');
							if (attribute [0] == "qty") {
								if (int.Parse (attribute [1]) < ItemCatalog.getItemMax (id)) {
									changeQtyForSlot (i, 1);
								finished = true;
								}
							}
						}
					}
			}
			if(!finished){
				backpack [getEmptySlot ()] = ItemCatalog.constructItem (id);
			}
		}
	}
	public static bool canHoldItems(string id,int amount){
		if (getEmptySlot () > -1) {
			return true;
		} else {
			return false;
		}
	}
	public static void removeItem(string id,int amount){
		for(int a = 0; a < amount; a++){
			changeQtyForSlot (getSlotWithLeastAmount(id), -1);
		}
	}
	public static void removeSlotItem(int slot,string id,int amount){
		for(int a = 0; a < amount; a++){
			if (getSlotItemID (slot) != "null") {
				changeQtyForSlot (slot, -1);
			} else {
				changeQtyForSlot (getSlotWithLeastAmount(id), -1);
			}
		}
	}
	public static void removeItemType(string type,int amount){
		for(int a = 0; a < amount; a++){
			changeQtyForSlot (getSlotWithLeastType(type), -1);
		}
	}
	public static int getSlotWithLeastAmount(string id){
		int lowestAmount = 1000;
		int slot = 0;
		for(int i = 0;i<backpack.Length;i++){
			if(backpack[i].Contains("id:"+id)){
				bool idMatch = false;
				int amountTemp = 0;
				string[] attributeList = backpack [i].Split (';');
				for(int i2 = 0;i2<attributeList.Length;i2++){
					string[] attribute = attributeList [i2].Split (':');
					if(attribute[0]=="id"){
						if(attribute[1]==id){
							idMatch = true;
						}
					}
					if(attribute[0]=="qty"){
						amountTemp = int.Parse(attribute[1]);
					}
				}
				if(idMatch){
					if(amountTemp < lowestAmount){
						lowestAmount = amountTemp;
						slot = i;
					};
				}
			}
		}
		return slot;
	}
	public static int getSlotWithLeastType(string type){
		int lowestAmount = 1000;
		int slot = 0;
		for(int i = 0;i<backpack.Length;i++){
			if(backpack[i].Contains("id:")){
				bool idMatch = false;
				int amountTemp = 0;
				string[] attributeList = backpack [i].Split (';');
				for(int i2 = 0;i2<attributeList.Length;i2++){
					string[] attribute = attributeList [i2].Split (':');
					if(attribute[0]=="id"){
						string tempID = attribute [1];
						if(ItemCatalog.getItemType(tempID)==type){
							idMatch = true;
						}
					}
					if(attribute[0]=="qty"){
						amountTemp = int.Parse(attribute[1]);
					}
				}
				if(idMatch){
					if(amountTemp < lowestAmount){
						lowestAmount = amountTemp;
						slot = i;
					};
				}
			}
		}
		return slot;
	}
	public static bool checkForItem(string id,int amount){
		int totalAmount = 0;
		for(int i = 0;i<backpack.Length;i++){
			if(backpack[i].Contains("id:"+id)){
				bool idMatch = false;
				int amountTemp = 0;
				string[] attributeList = backpack [i].Split (';');
				for(int i2 = 0;i2<attributeList.Length;i2++){
					string[] attribute = attributeList [i2].Split (':');
					if(attribute[0]=="id"){
						if(attribute[1]==id){
							idMatch = true;
						}
					}
					if(attribute[0]=="qty"){
						amountTemp = int.Parse(attribute[1]);
					}
				}
				if(idMatch){
					totalAmount += amountTemp;
				}
			}
		}
		if (totalAmount >= amount) {
			return true;
		} else {
			return false;
		}
	}
	public static int checkForItemAmount(string id){
		int totalAmount = 0;
		for(int i = 0;i<backpack.Length;i++){
			if(backpack[i].Contains("id:"+id)){
				bool idMatch = false;
				int amountTemp = 0;
				string[] attributeList = backpack [i].Split (';');
				for(int i2 = 0;i2<attributeList.Length;i2++){
					string[] attribute = attributeList [i2].Split (':');
					if(attribute[0]=="id"){
						if(attribute[1]==id){
							idMatch = true;
						}
					}
					if(attribute[0]=="qty"){
						amountTemp = int.Parse(attribute[1]);
					}
				}
				if(idMatch){
					totalAmount += amountTemp;
				}
			}
		}
		return totalAmount;
	}
	public static bool checkForItemType(string type,int amount){
		int totalAmount = 0;
		for(int i = 0;i<backpack.Length;i++){
			if(backpack[i].Contains("id:")){
				bool typeMatch = false;
				int amountTemp = 0;
				string[] attributeList = backpack [i].Split (';');
				for(int i2 = 0;i2<attributeList.Length;i2++){
					string[] attribute = attributeList [i2].Split (':');
					if(attribute[0]=="id"){
						string tempID = attribute [1];
						if(ItemCatalog.getItemType(tempID)==type){
							typeMatch = true;
						}
					}
					if(attribute[0]=="qty"){
						amountTemp = int.Parse(attribute[1]);
					}
				}
				if(typeMatch){
					totalAmount += amountTemp;
				}
			}
		}
		if (totalAmount >= amount) {
			return true;
		} else {
			return false;
		}
	}
	public static string getSlotItemID(int slot){
		string item = backpack[slot];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="id"){
				return attribute[1];
			}
		}
		return "null";
	}
	public void initializeBackpack(){
		for(int i = 0;i < backpack.Length;i++){
			backpack [i] = "";
		}
	}
	public static void loadHotbarImages(){
		if(getSlotItemID (0)!="null"){
			hotbarImage1 = ItemCatalog.itemSpriteSheet [ItemCatalog.getItemImage (getSlotItemID (0))].texture;
		}
		if (getSlotItemID (1) != "null") {
			hotbarImage2 = ItemCatalog.itemSpriteSheet [ItemCatalog.getItemImage (getSlotItemID (1))].texture;
		}
		if (getSlotItemID (2) != "null") {
			hotbarImage3 = ItemCatalog.itemSpriteSheet [ItemCatalog.getItemImage (getSlotItemID (2))].texture;
		}
		if (getSlotItemID (3) != "null") {
			hotbarImage4 = ItemCatalog.itemSpriteSheet [ItemCatalog.getItemImage (getSlotItemID (3))].texture;
		}
		if (getSlotItemID (4) != "null") {
			hotbarImage5 = ItemCatalog.itemSpriteSheet [ItemCatalog.getItemImage (getSlotItemID (4))].texture;
		}
	}
	public static int getItemQuantity(int slot){
		string item = backpack [slot];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="qty"){
				return int.Parse(attribute[1]);
			}
		}
		print ("Coudn't get item quantity...");
		return -1;
	}
	public static string getItemCrateName(int slot){
		string item = backpack [slot];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="cratename"){
				return attribute[1];
			}
		}
		print ("Coudn't get item quantity...");
		return "null";
	}
	public static float getItemLat(int slot){
		string item = backpack [slot];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="lat"){
				return float.Parse(attribute[1]);
			}
		}
		return -1;
	}
	public static float getItemLon(int slot){
		string item = backpack [slot];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="lon"){
				return float.Parse(attribute[1]);
			}
		}
		return -1;
	}
	public static int getItemCharges(int slot){
		string item = backpack [slot];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="charges"){
				return int.Parse(attribute[1]);
			}
		}
		print ("Coudn't get item quantity...");
		return -1;
	}
	public static int getItemDurability(int slot){
		string item = backpack [slot];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="dur"){
				return int.Parse(attribute[1]);
			}
		}
		return -1;
		print ("Coudn't get item durability...");
	}
	public static void swapBackpackSlots(int slot1, int slot2){
		string item1 = backpack[slot1];
		string item2 = backpack[slot2];
		backpack [slot1] = item2;
		backpack [slot2] = item1;
	}
	public static void saveBackpack(){
		File.Delete (Application.persistentDataPath + Controller.slot + "/backpack");
		var file = File.CreateText (Application.persistentDataPath + Controller.slot + "/backpack");
		for(int i = 0;i < backpack.Length;i++){
			file.WriteLine (Security.Rot39(backpack[i],131+i));
		}
		file.Close ();

	}
	public static void loadBackpack(){
		string line;
		int index = 0;
		StreamReader theReader = new StreamReader(Application.persistentDataPath + Controller.slot + "/backpack", Encoding.Default);
		using(theReader){
			do{
				line = theReader.ReadLine();
				if(line != null){
					backpack[index] = Security.Rot39(line,131+index);
				}
				index += 1;
			}while (line != null);
			theReader.Close ();
		}
	}
	public bool backpackChanged(){
		bool changed = false;
		for(int i = 0;i<backpack.Length;i++){
			if(backpack[i]!=backpackPrev[i]){
				backpackPrev [i] = backpack [i];
				changed = true;
			}
		}
		return changed;
	}
	public void openCraftingWindow(string section){
		CameraLocation.selectedRecipe = 0;
		craftingScreen.SetActive (true);
		craftingSection = section;
		window = true;
		needRelease = true;
		CameraLocation.needToRelease = true;
	}
	public void openFishingWindow(){
		fishingScreen.SetActive (true);
		fishingScript.Reset ();
		window = true;
		needRelease = true;
	}
	public void openBattleWindow(string enemyID, string weaponID){
		battleScreen.SetActive (true);
		battleScript.enemyID = enemyID;
		battleScript.playerWeaponID = weaponID;
		battleScript.Reset ();
		window = true;
		needRelease = true;
	}
	public void closeWindows(){
		CameraLocation.selectedRecipe = 0;
		CameraLocation.selectedSlot = -1;
		inventoryScreen.SetActive (false);
		craftingScreen.SetActive (false);
		skillScreen.SetActive (false);
		questScreen.SetActive (false);
		questLogScreen.SetActive (false);
		fishingScript.Reset ();
		fishingScreen.SetActive (false);
		battleScript.Reset ();
		battleScreen.SetActive (false);
		petScreen.SetActive (false);
		extraScreen.SetActive (false);
		giftScreen.SetActive (false);
		mapScreen.SetActive (false);
		buildingScreen.SetActive (false);
		buildingMaterialScreen.SetActive (false);
		shedScreen.SetActive (false);
		placeScreen.SetActive (false);
		dailyShopScreen.SetActive (false);
		coinShopScreen.SetActive (false);
		petSummonScreen.SetActive (false);
		customizeScreen.SetActive (false);
		window = false;
		CameraLocation.craftingPage = 0;
		needRelease = true;
	}
}
