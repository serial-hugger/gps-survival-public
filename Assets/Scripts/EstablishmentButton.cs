using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstablishmentButton : MonoBehaviour {

	public SpriteRenderer layer1;
	public SpriteRenderer layer2;
	public SpriteRenderer layer3;
	public SpriteRenderer layer4;
	public SpriteRenderer layer5;
	public SpriteRenderer layer6;
	public SpriteRenderer layer7;
	public SpriteRenderer layer8;
	public SpriteRenderer layer9;

	public TextMesh shopInfo;

	public SpriteRenderer itemImage;
	public TextMesh itemName;
	public TextMesh itemPrice;

	public string shopType;

	public int exclusiveShopSlot = -1;
	public int shopSlot;

	public string itemID;

	public string buttonType;

	public Transform establishmentCamera;

	public int price;

	public int buttonNumber;

	void Awake(){
		establishmentCamera = GameObject.Find ("ShopCamera").transform;
	}
	// Use this for initialization
	void Start() {
		if (exclusiveShopSlot < 0) {
			if (buttonType == "shop") {
				Random.InitState ((int)(long.Parse (Controller.currentPlaceID) / ((buttonNumber + 1) * 100)));
				Random.InitState (Random.Range (buttonNumber, 1000));
				int slot = Random.Range (0, EstablishmentCatalog.shops.Length);
				int colorZero = 0;
				colorZero = Random.Range (1, 3);
				if (buttonType == "shop") {
					shopType = EstablishmentCatalog.getShopkeeperType (slot);
					if (EstablishmentCatalog.getShopkeeperType (slot) == "food") {
						shopInfo.text = EstablishmentCatalog.shopkeeperNames [Random.Range (0, EstablishmentCatalog.shopkeeperNames.Length)] + "'s\n" + EstablishmentCatalog.foodEstablishmentNames [Random.Range (0, EstablishmentCatalog.foodEstablishmentNames.Length)];
					}
					if (EstablishmentCatalog.getShopkeeperType (slot) == "construction") {
						shopInfo.text = EstablishmentCatalog.shopkeeperNames [Random.Range (0, EstablishmentCatalog.shopkeeperNames.Length)] + "'s\n" + EstablishmentCatalog.constructionEstablishmentNames [Random.Range (0, EstablishmentCatalog.constructionEstablishmentNames.Length)];
					}
					if (EstablishmentCatalog.getShopkeeperType (slot) == "farming") {
						shopInfo.text = EstablishmentCatalog.shopkeeperNames [Random.Range (0, EstablishmentCatalog.shopkeeperNames.Length)] + "'s\n" + EstablishmentCatalog.farmingEstablishmentNames [Random.Range (0, EstablishmentCatalog.farmingEstablishmentNames.Length)];
					}
					if (EstablishmentCatalog.getShopkeeperImage (slot, 1) > -1) {
						layer1.sprite = EstablishmentCatalog.shopkeeperSpriteSheet [EstablishmentCatalog.getShopkeeperImage (slot, 1)];
						if (EstablishmentCatalog.getShopkeeperRandomLayer (slot) == 1) {
							Color tempColor = layer1.color;
							if (colorZero != 1) {
								tempColor.r = Random.Range (0.5f, 0.8f);
							} else {
								tempColor.r = 0;
							}
							if (colorZero != 2) {
								tempColor.g = Random.Range (0.55f, 0.85f);
							} else {
								tempColor.g = 0;
							}
							if (colorZero != 3) {
								tempColor.b = Random.Range (0.45f, 0.75f);
							} else {
								tempColor.b = 0;
							}
							layer1.color = tempColor;
						}
					}
					if (EstablishmentCatalog.getShopkeeperImage (slot, 2) > -1) {
						layer2.sprite = EstablishmentCatalog.shopkeeperSpriteSheet [EstablishmentCatalog.getShopkeeperImage (slot, 2)];
						if (EstablishmentCatalog.getShopkeeperRandomLayer (slot) == 2) {
							Color tempColor = layer2.color;
							if (colorZero != 1) {
								tempColor.r = Random.Range (0.5f, 0.8f);
							} else {
								tempColor.r = 0;
							}
							if (colorZero != 2) {
								tempColor.g = Random.Range (0.55f, 0.85f);
							} else {
								tempColor.g = 0;
							}
							if (colorZero != 3) {
								tempColor.b = Random.Range (0.45f, 0.75f);
							} else {
								tempColor.b = 0;
							}
							layer2.color = tempColor;
						}
					}
					if (EstablishmentCatalog.getShopkeeperImage (slot, 3) > -1) {
						layer3.sprite = EstablishmentCatalog.shopkeeperSpriteSheet [EstablishmentCatalog.getShopkeeperImage (slot, 3)];
						if (EstablishmentCatalog.getShopkeeperRandomLayer (slot) == 3) {
							Color tempColor = layer3.color;
							if (colorZero != 1) {
								tempColor.r = Random.Range (0.5f, 0.8f);
							} else {
								tempColor.r = 0;
							}
							if (colorZero != 2) {
								tempColor.g = Random.Range (0.55f, 0.85f);
							} else {
								tempColor.g = 0;
							}
							if (colorZero != 3) {
								tempColor.b = Random.Range (0.45f, 0.75f);
							} else {
								tempColor.b = 0;
							}
							layer3.color = tempColor;
						}
					}
					if (EstablishmentCatalog.getShopkeeperImage (slot, 4) > -1) {
						layer4.sprite = EstablishmentCatalog.shopkeeperSpriteSheet [EstablishmentCatalog.getShopkeeperImage (slot, 4)];
						if (EstablishmentCatalog.getShopkeeperRandomLayer (slot) == 4) {
							Color tempColor = layer4.color;
							if (colorZero != 1) {
								tempColor.r = Random.Range (0.5f, 0.8f);
							} else {
								tempColor.r = 0;
							}
							if (colorZero != 2) {
								tempColor.g = Random.Range (0.55f, 0.85f);
							} else {
								tempColor.g = 0;
							}
							if (colorZero != 3) {
								tempColor.b = Random.Range (0.45f, 0.75f);
							} else {
								tempColor.b = 0;
							}
							layer4.color = tempColor;
						}
					}
					if (EstablishmentCatalog.getShopkeeperImage (slot, 5) > -1) {
						layer5.sprite = EstablishmentCatalog.shopkeeperSpriteSheet [EstablishmentCatalog.getShopkeeperImage (slot, 5)];
						if (EstablishmentCatalog.getShopkeeperRandomLayer (slot) == 5) {
							Color tempColor = layer5.color;
							if (colorZero != 1) {
								tempColor.r = Random.Range (0.5f, 0.8f);
							} else {
								tempColor.r = 0;
							}
							if (colorZero != 2) {
								tempColor.g = Random.Range (0.55f, 0.85f);
							} else {
								tempColor.g = 0;
							}
							if (colorZero != 3) {
								tempColor.b = Random.Range (0.45f, 0.75f);
							} else {
								tempColor.b = 0;
							}
							layer5.color = tempColor;
						}
					}
					if (EstablishmentCatalog.getShopkeeperImage (slot, 6) > -1) {
						layer6.sprite = EstablishmentCatalog.shopkeeperSpriteSheet [EstablishmentCatalog.getShopkeeperImage (slot, 6)];
						if (EstablishmentCatalog.getShopkeeperRandomLayer (slot) == 6) {
							Color tempColor = layer6.color;
							if (colorZero != 1) {
								tempColor.r = Random.Range (0.5f, 0.8f);
							} else {
								tempColor.r = 0;
							}
							if (colorZero != 2) {
								tempColor.g = Random.Range (0.55f, 0.85f);
							} else {
								tempColor.g = 0;
							}
							if (colorZero != 3) {
								tempColor.b = Random.Range (0.45f, 0.75f);
							} else {
								tempColor.b = 0;
							}
							layer6.color = tempColor;
						}
					}
					if (EstablishmentCatalog.getShopkeeperImage (slot, 7) > -1) {
						layer7.sprite = EstablishmentCatalog.shopkeeperSpriteSheet [EstablishmentCatalog.getShopkeeperImage (slot, 7)];
						if (EstablishmentCatalog.getShopkeeperRandomLayer (slot) == 7) {
							Color tempColor = layer7.color;
							if (colorZero != 1) {
								tempColor.r = Random.Range (0.5f, 0.8f);
							} else {
								tempColor.r = 0;
							}
							if (colorZero != 2) {
								tempColor.g = Random.Range (0.55f, 0.85f);
							} else {
								tempColor.g = 0;
							}
							if (colorZero != 3) {
								tempColor.b = Random.Range (0.45f, 0.75f);
							} else {
								tempColor.b = 0;
							}
							layer7.color = tempColor;
						}
					}
					if (EstablishmentCatalog.getShopkeeperImage (slot, 8) > -1) {
						layer8.sprite = EstablishmentCatalog.shopkeeperSpriteSheet [EstablishmentCatalog.getShopkeeperImage (slot, 8)];
						if (EstablishmentCatalog.getShopkeeperRandomLayer (slot) == 8) {
							Color tempColor = layer7.color;
							if (colorZero != 1) {
								tempColor.r = Random.Range (0.5f, 0.8f);
							} else {
								tempColor.r = 0;
							}
							if (colorZero != 2) {
								tempColor.g = Random.Range (0.55f, 0.85f);
							} else {
								tempColor.g = 0;
							}
							if (colorZero != 3) {
								tempColor.b = Random.Range (0.45f, 0.75f);
							} else {
								tempColor.b = 0;
							}
							layer8.color = tempColor;
						}
					}
					if (EstablishmentCatalog.getShopkeeperImage (slot, 9) > -1) {
						layer9.sprite = EstablishmentCatalog.shopkeeperSpriteSheet [EstablishmentCatalog.getShopkeeperImage (slot, 9)];
						if (EstablishmentCatalog.getShopkeeperRandomLayer (slot) == 9) {
							Color tempColor = layer9.color;
							if (colorZero != 1) {
								tempColor.r = Random.Range (0.5f, 0.8f);
							} else {
								tempColor.r = 0;
							}
							if (colorZero != 2) {
								tempColor.g = Random.Range (0.55f, 0.85f);
							} else {
								tempColor.g = 0;
							}
							if (colorZero != 3) {
								tempColor.b = Random.Range (0.45f, 0.75f);
							} else {
								tempColor.b = 0;
							}
							layer9.color = tempColor;
						}
					}
				}
			}
			if (buttonType == "buycargo") {
				if (!PetInfo.IsPetHappy ("0003")) {
					if (Inventory.getCrateAmount () <= 0) {
						Random.InitState ((int)(long.Parse (Controller.currentPlaceID) / ((buttonNumber + 1) * 100)));
						Random.InitState (Random.Range (buttonNumber, 1000));
						price = Random.Range (30, 60);
						shopInfo.text = "Buy: " + price;
					} else {
						shopInfo.text = "Buy: ---";
					}
				} else {
					if (Inventory.getCrateAmount () <= 1) {
						Random.InitState ((int)(long.Parse (Controller.currentPlaceID) / ((buttonNumber + 1) * 100)));
						Random.InitState (Random.Range (buttonNumber, 1000));
						price = Random.Range (30, 60);
						shopInfo.text = "Buy: " + price;
					} else {
						shopInfo.text = "Buy: ---";
					}
				}
			}
			if (buttonType == "sellcargo") {
				if (Inventory.getCrateSlot () != -1) {
					Random.InitState ((int)(long.Parse (Controller.currentPlaceID) / ((buttonNumber + 1) * 100)));
					Random.InitState (Random.Range (buttonNumber, 1000));
					price = (int)(3 * Controller.getDistanceFromLatLonInKm (Input.location.lastData.latitude, Input.location.lastData.longitude, Inventory.getItemLat (Inventory.getCrateSlot ()), Inventory.getItemLon (Inventory.getCrateSlot ())));
					if (price > 1000) {
						price = 1000;
					}
					shopInfo.text = "Sell: " + price;
				} else {
					shopInfo.text = "Sell: ---";
				}
			}
		} else {
			layer4.sprite = EstablishmentCatalog.shopkeeperSpriteSheet[EstablishmentCatalog.getExclusiveShopkeeperImage (exclusiveShopSlot, 4)];
			shopInfo.text = EstablishmentCatalog.getExclusiveShopkeeperName (exclusiveShopSlot);
			shopType = EstablishmentCatalog.getExclusiveShopkeeperType (exclusiveShopSlot);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (exclusiveShopSlot < 0) {
			if (buttonType == "buyitem") {
				itemImage.sprite = ItemCatalog.itemSpriteSheet [ItemCatalog.getItemImage (itemID)];
				itemName.text = ItemCatalog.getItemName (itemID);
				itemPrice.text = "Cost: " + ItemCatalog.getItemPrice (itemID);
				string lastData;
				for (int i = 0; i < 999999; i++) {
					if (DailyInfo.grabData ("itemBuy", i, 0) == "END") {
						return;
					}
					if (DailyInfo.grabData ("itemBuy", i, 0) == Controller.currentPlaceID && DailyInfo.grabData ("itemBuy", i, 1) == buttonNumber.ToString () && DailyInfo.grabData ("itemBuy", i, 2) == shopType) {
						itemImage.sprite = ItemCatalog.itemSpriteSheet [ItemCatalog.getItemImage (itemID)];
						itemName.text = ItemCatalog.getItemName (itemID);
						itemPrice.text = "SOLD OUT";
					}
				}
			}
		} else {
		}
	}
	public void buttonPress(){
		EstablishmentCameraScript cameraScript = establishmentCamera.GetComponent<EstablishmentCameraScript> ();
		if (exclusiveShopSlot < 0) {
			if (buttonType == "shop") {
				cameraScript.pageLayer1.sprite = layer1.sprite;
				cameraScript.pageLayer1.color = layer1.color;
				cameraScript.pageLayer2.sprite = layer2.sprite;
				cameraScript.pageLayer2.color = layer2.color;
				cameraScript.pageLayer3.sprite = layer3.sprite;
				cameraScript.pageLayer3.color = layer3.color;
				cameraScript.pageLayer4.sprite = layer4.sprite;
				cameraScript.pageLayer4.color = layer4.color;
				cameraScript.pageLayer5.sprite = layer5.sprite;
				cameraScript.pageLayer5.color = layer5.color;
				cameraScript.pageLayer6.sprite = layer6.sprite;
				cameraScript.pageLayer6.color = layer6.color;
				cameraScript.pageLayer7.sprite = layer7.sprite;
				cameraScript.pageLayer7.color = layer7.color;
				cameraScript.pageLayer8.sprite = layer8.sprite;
				cameraScript.pageLayer8.color = layer8.color;
				cameraScript.pageLayer9.sprite = layer9.sprite;
				cameraScript.pageLayer9.color = layer9.color;
				Vector3 tempPos = establishmentCamera.position;
				tempPos.y = 0.0f;
				tempPos.x = 60.0f;
				establishmentCamera.position = tempPos;
			}
			if (buttonType == "exitshop") {
				Vector3 tempPos = establishmentCamera.position;
				tempPos.y = 0.0f;
				tempPos.x = 30.0f;
				establishmentCamera.position = tempPos;
			}
		} else {
			cameraScript.pageLayer1.sprite = null;
			cameraScript.pageLayer2.sprite = null;
			cameraScript.pageLayer3.sprite = null;
			cameraScript.pageLayer4.sprite = layer4.sprite;
			cameraScript.pageLayer5.sprite = null;
			cameraScript.pageLayer6.sprite = null;
			cameraScript.pageLayer7.sprite = null;
			cameraScript.pageLayer8.sprite = null;
			cameraScript.pageLayer9.sprite = null;
			Vector3 tempPos = establishmentCamera.position;
			tempPos.y = 0.0f;
			tempPos.x = 60.0f;
			establishmentCamera.position = tempPos;
		}
	}
}
