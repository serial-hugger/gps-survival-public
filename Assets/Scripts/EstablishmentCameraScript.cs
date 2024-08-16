using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EstablishmentCameraScript : MonoBehaviour {

	bool held;
	float prevTouchY;
	float startTouchY;
	float endTouchY;
	public GameObject headers;
	public SpriteRenderer pageLayer1;
	public SpriteRenderer pageLayer2;
	public SpriteRenderer pageLayer3;
	public SpriteRenderer pageLayer4;
	public SpriteRenderer pageLayer5;
	public SpriteRenderer pageLayer6;
	public SpriteRenderer pageLayer7;
	public SpriteRenderer pageLayer8;
	public SpriteRenderer pageLayer9;
	public TextMesh saying;
	public bool shopsCreated;

	public TextMesh cityText;

	public bool released;

	// Use this for initialization
	void Start () {
	}
	void LateUpdate(){
		Vector3 tempHeadPos = headers.transform.position;
		tempHeadPos.y = transform.position.y;
		headers.transform.position = tempHeadPos;
	}
	// Update is called once per frame
	void Update () {
		if(CameraController.camera != 1){
			Vector3 tempPos = transform.position;
			tempPos.y = 0.0f;
			tempPos.x = 30.0f;
			transform.position = tempPos;
			released = false;
			shopsCreated = false;
		}
		if (CameraController.camera == 1) {
			if (!shopsCreated) {
				createButtons (3, 0);
				transform.position = new Vector3 (30, 0, -30);
				shopsCreated = true;
				released = false;
				held = false;
			}
			if (CameraController.camera != 1) {
				shopsCreated = false;
			}
			cityText.text = FirstLetterToUpper (Controller.currentPlaceName) + "\nestablishments:";
			if (released) {
				for (int i = 0; i < Input.touchCount; i++) {
					if (held) {
						Vector3 tempPos = transform.position;
						tempPos.y += (prevTouchY - Input.GetTouch (i).position.y) * 10 / Screen.height;
						transform.position = tempPos;
					} else {
						startTouchY = Input.GetTouch (i).position.y;
					}
					prevTouchY = Input.GetTouch (i).position.y;
					held = true;
					if (Input.GetTouch (i).phase == TouchPhase.Ended) {
						endTouchY = Input.GetTouch (i).position.y;
						if (Mathf.Abs (startTouchY - endTouchY) < Screen.height/10) {
							Vector3 test = Camera.main.ScreenToWorldPoint (Input.GetTouch (i).position);
							RaycastHit hit; 
							Physics.Raycast (test, Vector3.forward, out hit);
							if (hit.collider != null) {
								EstablishmentButton buttonScript = hit.collider.gameObject.GetComponent<EstablishmentButton> ();
								if (buttonScript.buttonType == "shop") {
									print ("press");
									if (buttonScript.shopType == "food") {
										createShopItemButtons (EstablishmentCatalog.foodItems.Length/4, buttonScript.shopType, buttonScript.shopSlot);
									}
									if (buttonScript.shopType == "construction") {
										createShopItemButtons (EstablishmentCatalog.constructionItems.Length/4, buttonScript.shopType, buttonScript.shopSlot);
									}
									if (buttonScript.shopType == "farming") {
										createShopItemButtons (EstablishmentCatalog.farmingItems.Length/4, buttonScript.shopType, buttonScript.shopSlot);
									}
									if (buttonScript.shopType == "jeremy") {
										createShopItemButtons (EstablishmentCatalog.jeremyItems.Length/4, buttonScript.shopType, buttonScript.exclusiveShopSlot);
									}
								}
								if (buttonScript.buttonType == "exittown") {
									CameraController.camera = 0;
								}
								if (buttonScript.buttonType == "exitshop") {
									released = false;
									GameObject[] itemButtons = GameObject.FindGameObjectsWithTag ("BuyItemButton");
									for (int i2 = 0; i2 < itemButtons.Length; i2++) {
										Destroy (itemButtons [i2]);
									}
									createButtons (3, 0);
								}
								if (buttonScript.buttonType == "buyitem") {
									if (AccountInfo.accountCoins >= ItemCatalog.getItemPrice (buttonScript.itemID)) {
										if (!buttonScript.itemPrice.text.Contains ("SOLD OUT")) {
											if (Inventory.canHoldItems (buttonScript.itemID, 1)) {
												AccountInfo.spendCoins (ItemCatalog.getItemPrice (buttonScript.itemID));
												Inventory.addItem (buttonScript.itemID, 1);
												DailyInfo.addData ("itemBuy", Controller.currentPlaceID + ":" + buttonScript.buttonNumber + ":" + buttonScript.shopType);
											} else {
												GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0, 0, -20), Quaternion.identity);
												TextMesh errorText = error.GetComponent<TextMesh> ();
												errorText.text = "ONE EMPTY SPACE\nIS REQUIRED FOR\nPURCHASE";
												CameraController.camera = 0;
											}
										}
									} else {
										GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0, 0, -20), Quaternion.identity);
										TextMesh errorText = error.GetComponent<TextMesh> ();
										errorText.text = "NOT ENOUGH\nMONEY";
										CameraController.camera = 0;
									}
									released = false;
								}
								if (buttonScript.buttonType == "buycargo") {
									if (Inventory.checkForItemAmount("0129")<1 || (Inventory.checkForItemAmount("0129")<2&&PetInfo.IsPetHappy("0003"))) {
										if (AccountInfo.accountCoins >= buttonScript.price) {
											if (Inventory.canHoldItems ("0129", 1)) {
												AccountInfo.spendCoins (buttonScript.price);
												Inventory.addItem ("0129", 1);
												CameraController.camera = 0;
											} else {
												GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0, 0, -20), Quaternion.identity);
												TextMesh errorText = error.GetComponent<TextMesh> ();
												errorText.text = "ONE EMPTY SPACE\nIS REQUIRED FOR\nPURCHASE";
												CameraController.camera = 0;
											}
										} else {
											GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0, 0, -20), Quaternion.identity);
											TextMesh errorText = error.GetComponent<TextMesh> ();
											errorText.text = "NOT ENOUGH\nMONEY";
											CameraController.camera = 0;
										}
										released = false;
										CameraLocation.needToRelease = true;
									} else {
										GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0, 0, -20), Quaternion.identity);
										TextMesh errorText = error.GetComponent<TextMesh> ();
										errorText.text = "YOU ALREADY\nHAVE ONE";
										CameraController.camera = 0;
									}
								}
								if (buttonScript.buttonType == "sellcargo") {
									if (Inventory.checkForItemAmount("0129")>0) {
										Inventory.removeItem ("0129", 1);
										AccountInfo.accountCoins += buttonScript.price;
										Achievements.IncrementAchievement (GPGSIds.achievement_bringing_the_goods, buttonScript.price);
										AccountInfo.saveInfo ();
										CameraController.camera = 0;
										released = false;
										CameraLocation.needToRelease = true;
									} else {
										GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0, 0, -20), Quaternion.identity);
										TextMesh errorText = error.GetComponent<TextMesh> ();
										errorText.text = "YOU DON'T\nHAVE ONE";
										CameraController.camera = 0;
									}
								}
								buttonScript.buttonPress ();
							}
						}
						held = false;
					}
				}
				if (transform.position.y > 0) {
					Vector3 tempPos = transform.position;
					tempPos.y -= (transform.position.y) / 20;
					transform.position = tempPos;
				}
			}
			for (int i = 0; i < Input.touchCount; i++) {
				if (Input.GetTouch (i).phase == TouchPhase.Ended) {
					released = true;
				}
			}
		}
	}
	public void createButtons(int shopAmounts,int serviceAmounts){
		GameObject[] itemButtons = GameObject.FindGameObjectsWithTag("BuyItemButton");
		for(int i2 = 0;i2 < itemButtons.Length;i2++){
			Destroy(itemButtons[i2]);
		}
		float currentPlacePosition = 1.5f;
		int currentButton = 0;
		GameObject shopList = (GameObject)Instantiate (Resources.Load ("EstablishmentScreen/establishmentCatagory"), new Vector3 (30, currentPlacePosition, 0), Quaternion.identity);
		shopList.transform.GetChild (1).GetComponent<TextMesh> ().text = "SHOPS";
		currentPlacePosition -= 1.5f;
		System.Collections.Generic.List<string> items = new System.Collections.Generic.List<string>();
		for(int i = 0; i < shopAmounts;i++){
			currentButton += 1;
			Random.InitState ((int)(long.Parse(Controller.currentPlaceID)/((currentButton+1)*100)));
			Random.InitState (Random.Range(currentButton,1000));
			int slot = Random.Range (0, EstablishmentCatalog.shops.Length);
			if (!items.Contains (EstablishmentCatalog.getShopkeeperType (slot))) {
				GameObject shopButton = (GameObject)Instantiate (Resources.Load ("EstablishmentScreen/establishmentButton"), new Vector3 (30, currentPlacePosition, 0), Quaternion.identity);
				EstablishmentButton buttonScript = shopButton.GetComponent<EstablishmentButton> ();
				buttonScript.buttonNumber = currentButton;
				buttonScript.shopSlot = slot;
				items.Add (EstablishmentCatalog.getShopkeeperType (slot));
				currentPlacePosition -= 1.5f;
			}
		}
		List<int> exclusiveSlots = EstablishmentCatalog.getExclusiveShopSlots (Controller.currentPlaceID);
		if(exclusiveSlots.Count>0){
			currentButton += 1;
			Random.InitState ((int)(long.Parse(Controller.currentPlaceID)/((currentButton+1)*100)));
			Random.InitState (Random.Range(currentButton,1000));
			for(int i = 0;i<exclusiveSlots.Count;i++){
				GameObject shopButton = (GameObject)Instantiate (Resources.Load ("EstablishmentScreen/establishmentButton"), new Vector3 (30, currentPlacePosition, 0), Quaternion.identity);
				EstablishmentButton buttonScript = shopButton.GetComponent<EstablishmentButton> ();
				buttonScript.buttonNumber = currentButton;
				buttonScript.exclusiveShopSlot = exclusiveSlots [i];
				currentPlacePosition -= 1.5f;
			}
		}
		if(true){
			GameObject serviceList = (GameObject)Instantiate (Resources.Load ("EstablishmentScreen/establishmentCatagory"), new Vector3 (30, currentPlacePosition, 0), Quaternion.identity);
			serviceList.transform.GetChild (1).GetComponent<TextMesh> ().text = "DELIVERY";
			currentPlacePosition -= 1.5f;
			Instantiate (Resources.Load ("EstablishmentScreen/cargoBuyButton"), new Vector3 (30, currentPlacePosition, 0), Quaternion.identity);
			currentButton += 1;
			currentPlacePosition -= 1.5f;
			Instantiate (Resources.Load ("EstablishmentScreen/cargoSellButton"), new Vector3 (30, currentPlacePosition, 0), Quaternion.identity);
			currentButton += 1;
			currentPlacePosition -= 1.5f;
		}
	}
	void createShopItemButtons(int amounts,string shopType, int shopNumber){
		float currentPlacePositionY = 1.5f;
		float currentPlacePositionX = 57f;
		int currentButton = 0;
		currentPlacePositionY -= 1.5f;
		System.Collections.Generic.List<string> items = new System.Collections.Generic.List<string>();
		Random.InitState ((int)long.Parse(Controller.currentPlaceID) + shopNumber + System.DateTime.Now.DayOfYear);
		for(int i = 0; i < amounts;i++){
			string itemID = "";
			float itemRarity = 100.0f;
			if(shopType == "food"){
				saying.text = "\""+EstablishmentCatalog.getShopkeeperSaying (shopNumber)+"\"";
				itemID = EstablishmentCatalog.foodItems[Random.Range (0,EstablishmentCatalog.foodItems.Length)].Split(':')[0];
				itemRarity = float.Parse(EstablishmentCatalog.foodItems[Random.Range (0,EstablishmentCatalog.foodItems.Length)].Split(':')[1]);
			}
			if(shopType == "construction"){
				saying.text = "\""+EstablishmentCatalog.getShopkeeperSaying (shopNumber)+"\"";
				itemID = EstablishmentCatalog.constructionItems[Random.Range (0,EstablishmentCatalog.constructionItems.Length)].Split(':')[0];
				itemRarity = float.Parse(EstablishmentCatalog.constructionItems[Random.Range (0,EstablishmentCatalog.constructionItems.Length)].Split(':')[1]);
			}
			if(shopType == "farming"){
				saying.text = "\""+EstablishmentCatalog.getShopkeeperSaying (shopNumber)+"\"";
				itemID = EstablishmentCatalog.farmingItems[Random.Range (0,EstablishmentCatalog.farmingItems.Length)].Split(':')[0];
				itemRarity = float.Parse(EstablishmentCatalog.farmingItems[Random.Range (0,EstablishmentCatalog.farmingItems.Length)].Split(':')[1]);
			}
			if(shopType == "jeremy"){
				saying.text = "\""+EstablishmentCatalog.getExclusiveShopkeeperSaying (shopNumber)+"\"";
				itemID = EstablishmentCatalog.jeremyItems[Random.Range (0,EstablishmentCatalog.jeremyItems.Length)].Split(':')[0];
				itemRarity = float.Parse(EstablishmentCatalog.jeremyItems[Random.Range (0,EstablishmentCatalog.jeremyItems.Length)].Split(':')[1]);
			}
			if(!items.Contains(itemID) && Random.Range(0,100)<=itemRarity){
				items.Add (itemID);
				currentPlacePositionX += 1.5f;
				if(currentPlacePositionX>62){
					currentPlacePositionX = 58.5f;
					currentPlacePositionY -= 1.5f;
				}
				GameObject shopButton = (GameObject)Instantiate (Resources.Load ("EstablishmentScreen/shopItemButton"), new Vector3 (currentPlacePositionX, currentPlacePositionY, 0), Quaternion.identity);
				EstablishmentButton buttonScript = shopButton.GetComponent<EstablishmentButton> ();
				buttonScript.buttonNumber = currentButton;
				buttonScript.itemID = itemID;
				buttonScript.shopType = shopType;
			}
			currentButton += 1;
		}
	}
	public bool hasItem(System.Collections.Generic.List<string> items,string item){
		for(int i = 0;i<items.Count;i++){
			if(items[i]==item){
				return true;
			}
		}
		return false;
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
