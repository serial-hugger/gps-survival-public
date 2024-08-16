using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour {

	public Sprite itemSprite;
	public string name;
	public int backpackItem;
	public int amount;
	public bool prevEnabled;
	public SpriteRenderer renderer;
	public SpriteRenderer amountCircle;
	public TextMesh text;
	public int durMax;
	public int dur;
	public int chargeMax;
	public int charge;
	public bool selected;
	public SpriteRenderer box;
	public GameObject infoBox;
	public TextMesh infoBoxText;
	public SpriteRenderer infoBoxSprite;

	// Use this for initialization
	void Start () {
		infoBox = GameObject.Find ("ItemInfoCard");
		infoBoxText = infoBox.transform.GetChild (0).GetComponent<TextMesh> ();
		infoBoxSprite = infoBox.transform.GetChild (1).GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.parent.gameObject.activeSelf != prevEnabled){
			prevEnabled = transform.parent.gameObject.activeSelf;
			UpdateBoxInfo (backpackItem);
		}
		if (selected) {
			transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
			infoBoxText.text = name;
			infoBoxSprite.sprite = itemSprite;
			if (name == "") {
				infoBox.SetActive (false);
			} else {
				infoBox.SetActive (true);
			}
			UpdateBoxInfo (backpackItem);
		} else {
			transform.localScale = new Vector3 (1f, 1f, 1f);
			UpdateBoxInfo (backpackItem);
		}
		if(CameraLocation.selectedSlot == -1){
			selected = false;
			UpdateBoxInfo (backpackItem);
		}

	}
	void UpdateBoxInfo(int slot){
		name = "";
		if (Inventory.getSlotItemID (slot) != "null") {
			amount = Inventory.getItemQuantity (slot);
			itemSprite = ItemCatalog.itemSpriteSheet [ItemCatalog.getItemImage (Inventory.getSlotItemID (slot))];
			name = ItemCatalog.getItemName (Inventory.getSlotItemID (slot));
			if(Inventory.getSlotItemID(backpackItem)=="0129"){
				name = Inventory.getItemCrateName (backpackItem);
			}
			renderer.sprite = itemSprite;
			if (amount > 1) {
				text.text = amount.ToString ();
				amountCircle.enabled = true;
			} else {
				text.text = "";
				amountCircle.enabled = false;
			}
			durMax = ItemCatalog.getItemDurability (Inventory.getSlotItemID (backpackItem));
			dur = Inventory.getItemDurability (slot);
			float durTemp = (float)dur;
			float durMaxTemp = (float)durMax;
			box.color = new Color(.9f,.9f,.9f);
			if(durMaxTemp > 0f){	
				if(durTemp == durMaxTemp){
					box.color = Color.cyan;
				}
				if(durTemp < durMaxTemp){
					box.color = Color.green;
				}
				if((durTemp/durMaxTemp) < .75f){
					box.color = Color.yellow;
				}
				if((durTemp/durMaxTemp) < .25f){
					box.color = Color.red;
				}
			}
			chargeMax = ItemCatalog.getItemCharges (Inventory.getSlotItemID (backpackItem));
			charge = Inventory.getItemCharges (slot);
			float chargeTemp = (float)charge;
			float chargeMaxTemp = (float)chargeMax;
			if(chargeMaxTemp > 0f){	
				if(chargeTemp == 0){
					box.color = Color.cyan;
				}
				if(chargeTemp > 0){
					box.color = Color.green;
				}
				if((chargeTemp/chargeMaxTemp) > .25f){
					box.color = Color.yellow;
				}
				if((chargeTemp/chargeMaxTemp) > .75f){
					box.color = Color.red;
				}
			}
		} else {
			amountCircle.enabled = false;
			infoBox.name = "";
			itemSprite = null;
			amount = 0;
			durMax = 0;
			dur = 0;
			text.text = "";
			renderer.sprite = null;
			box.color = new Color(.9f,.9f,.9f);
		}
	}
}
