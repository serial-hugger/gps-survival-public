using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyShopItem : MonoBehaviour {

	public string itemID;
	public int buttonNumber;

	public SpriteRenderer itemRender;
	public TextMesh itemName;
	public TextMesh itemPrice;

	public static string[] items = new string[]{
		"0005",
		"0006",
		"0007",
		"0008",
		"0009",
		"0010",
		"0011",
		"0012",
		"0013",
		"0014",
		"0015",
		"0016",
		"0017",
		"0018",
		"0019",
		"0020",
		"0021",
		"0022",
		"0023",
		"0024",
		"0025",
		"0026",
		"0027",
		"0028",
		"0029",
		"0041",
		"0044",
		"0045",
		"0046",
		"0047",
		"0049",
		"0052",
		"0053",
		"0054",
		"0055",
		"0056",
		"0057",
		"0065",
		"0083",
		"0084",
		"0085",
		"0086",
		"0088",
		"0089",
		"0093",
		"0094",
		"0110",
		"0126",
		"0127",
		"0128"
	};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Refresh ();
	}

	public void Refresh(){
		Random.InitState ((int)(System.DateTime.Now.Ticks/System.TimeSpan.TicksPerDay)+buttonNumber);
		itemID = items[Random.Range(0,items.Length)];
		itemRender.sprite = ItemCatalog.itemSpriteSheet[ItemCatalog.getItemImage(itemID)];
		itemName.text = ItemCatalog.getItemName (itemID);
		itemPrice.text = "Cost: " + (ItemCatalog.getItemPrice (itemID)/2).ToString();
		string lastData;
		for(int i = 0; i < DailyInfo.dailyActions.Count;i++){
			lastData = DailyInfo.grabData ("dailyItemBuy", i, 0);
			if(lastData == "END"){
				return;
			}
			if(lastData == itemID){
				itemRender.sprite = ItemCatalog.itemSpriteSheet[ItemCatalog.getItemImage(itemID)];
				itemName.text = ItemCatalog.getItemName (itemID);
				itemPrice.text = "SOLD OUT";
			}
		}
	}
	public void BuyItem(){
		if(!itemPrice.text.Contains("SOLD OUT")){
			if (AccountInfo.accountCoins >= ItemCatalog.getItemPrice (itemID)/2) {
				if (Inventory.canHoldItems (itemID, 1)) {
					AccountInfo.lastDailyBuy = System.DateTime.Now.Ticks;
					AccountInfo.spendCoins (ItemCatalog.getItemPrice (itemID)/2);
					Inventory.addItem (itemID, 1);
					DailyInfo.addData ("dailyItemBuy",itemID);
				} else {
					GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0, 0, -20), Quaternion.identity);
					TextMesh errorText = error.GetComponent<TextMesh> ();
					errorText.text = "ONE EMPTY SPACE\nIS REQUIRED FOR\nPURCHASE";
				}
			} else {
				GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0, 0, -20), Quaternion.identity);
				TextMesh errorText = error.GetComponent<TextMesh> ();
				errorText.text = "NOT ENOUGH\nMONEY";
			}
		}
	}
}
