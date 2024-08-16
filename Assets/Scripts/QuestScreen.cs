using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestScreen : MonoBehaviour {

	public TextMesh textRenderer;
	public TextMesh nameRenderer;
	public SpriteRenderer npcRender;
	public SpriteRenderer itemRender;
	public TextMesh amountRender;
	public string name;
	public string npcID;
	public string text = "";
	public Sprite npcImage;
	public string item = "0000";
	public string itemName = "";
	public int amount = 0;
	public int status;
	public SpriteRenderer bubbleRender;
	public Sprite bubbleQuest;
	public Sprite bubbleText;
	public GameObject button;
	public SpriteRenderer buttonRender;
	public Sprite buttonAccept;
	public Sprite buttonComplete;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		textRenderer.text = text;
		nameRenderer.text = name.ToUpper() + " says...";
		npcRender.sprite = npcImage;
		itemRender.sprite = ItemCatalog.itemSpriteSheet[ItemCatalog.getItemImage (item)];
		amountRender.text = "x" + amount;
		if (status == 0) {
			buttonRender.sprite = buttonAccept;
		}
		if(status == 1){
			buttonRender.sprite = buttonComplete;
		}
		if (status == 2) {
			button.SetActive (false);
			bubbleRender.sprite = bubbleText;
			itemRender.sprite = null;
			amountRender.text = "";
		} else {
			button.SetActive (true);
			bubbleRender.sprite = bubbleQuest;
		}
	}
}
