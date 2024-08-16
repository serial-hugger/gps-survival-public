using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTransferBox : MonoBehaviour {

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
	public bool selected;
	public SpriteRenderer box;
	public bool chest;
	public InventoryTransferScreen transferScript;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		UpdateBoxInfo (backpackItem);

	}
	public void SendToChest(int slot){
		if (ItemCatalog.getItemMax (Inventory.getSlotItemID (slot)) > 1) {
			transferScript.addItem (Inventory.getSlotItemID (slot), 1);
			transferScript.shedScript.storageSlots = transferScript.storageSlots;
			Inventory.removeSlotItem (slot,Inventory.getSlotItemID (slot), 1);
		} else {
			transferScript.storageSlots [transferScript.getEmptySlot ()] = Inventory.backpack [slot];
			transferScript.shedScript.storageSlots = transferScript.storageSlots;
			Inventory.backpack [slot] = "";
		}
		transferScript.UpdateInfoInChunk ();
	}
	public void SendToInventory(int slot){
		if (ItemCatalog.getItemMax (transferScript.getSlotItemID (slot)) > 1) {
			Inventory.addItem (transferScript.getSlotItemID (slot), 1);
			transferScript.removeSlotItem (slot,transferScript.getSlotItemID (slot), 1);
			transferScript.shedScript.storageSlots = transferScript.storageSlots;
		} else {
			Inventory.backpack[Inventory.getEmptySlot()] = transferScript.storageSlots [slot];
			transferScript.storageSlots [slot] = "null";
			transferScript.shedScript.storageSlots = transferScript.storageSlots;
		}
		transferScript.UpdateInfoInChunk ();
	}
	void UpdateBoxInfo(int slot){
		name = "";
		if (!chest) {
			if (Inventory.getSlotItemID (slot) != "null") {
				amount = Inventory.getItemQuantity (slot);
				itemSprite = ItemCatalog.itemSpriteSheet [ItemCatalog.getItemImage (Inventory.getSlotItemID (slot))];
				name = ItemCatalog.getItemName (Inventory.getSlotItemID (slot));
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
				box.color = new Color (.9f, .9f, .9f);
				if (durMaxTemp > 0f) {	
					if (durTemp == durMaxTemp) {
						box.color = Color.cyan;
					}
					if (durTemp < durMaxTemp) {
						box.color = Color.green;
					}
					if ((durTemp / durMaxTemp) < .75f) {
						box.color = Color.yellow;
					}
					if ((durTemp / durMaxTemp) < .25f) {
						box.color = Color.red;
					}
				}
			} else {
				amountCircle.enabled = false;
				itemSprite = null;
				amount = 0;
				durMax = 0;
				dur = 0;
				text.text = "";
				renderer.sprite = null;
				box.color = new Color (.9f, .9f, .9f);
			}
		} else {
			if (transferScript.getSlotItemID(slot) != "null") {
				amount = transferScript.getItemQuantity (slot);
				itemSprite = ItemCatalog.itemSpriteSheet [ItemCatalog.getItemImage (transferScript.getSlotItemID (slot))];
				name = ItemCatalog.getItemName (transferScript.getSlotItemID (slot));
				renderer.sprite = itemSprite;
				if (amount > 1) {
					text.text = amount.ToString ();
					amountCircle.enabled = true;
				} else {
					text.text = "";
					amountCircle.enabled = false;
				}
				durMax = ItemCatalog.getItemDurability (transferScript.getSlotItemID (backpackItem));
				dur = transferScript.getItemDurability (slot);
				float durTemp = (float)dur;
				float durMaxTemp = (float)durMax;
				box.color = new Color (.9f, .9f, .9f);
				if (durMaxTemp > 0f) {	
					if (durTemp == durMaxTemp) {
						box.color = Color.cyan;
					}
					if (durTemp < durMaxTemp) {
						box.color = Color.green;
					}
					if ((durTemp / durMaxTemp) < .75f) {
						box.color = Color.yellow;
					}
					if ((durTemp / durMaxTemp) < .25f) {
						box.color = Color.red;
					}
				}
			} else {
				amountCircle.enabled = false;
				itemSprite = null;
				amount = 0;
				durMax = 0;
				dur = 0;
				text.text = "";
				renderer.sprite = null;
				box.color = new Color (.9f, .9f, .9f);
			}
		}
	}
}
