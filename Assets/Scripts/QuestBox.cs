using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBox : MonoBehaviour {

	public int questNumber;
	public SpriteRenderer box;
	public SpriteRenderer npcRender;
	public SpriteRenderer itemRender;
	public TextMesh npcName;
	public TextMesh itemAmount;
	public TextMesh reward;
	public TextMesh rewardText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateBoxInfo ();
	}
	public void UpdateBoxInfo(){
		npcRender.sprite = null;
		npcName.text = "";
		itemRender.sprite = null;
		itemAmount.text = "";
		reward.text = "";
		rewardText.text = "";
		box.enabled = false;
		//-----
		if(QuestInfo.LoadHeldQuest(questNumber)!=""){
		npcRender.sprite = QuestInfo.npcSpritesGlobal[QuestInfo.GetNpcImage(questNumber)];
		npcName.text = QuestInfo.GetNpcName (questNumber);
		itemRender.sprite = ItemCatalog.itemSpriteSheet[ItemCatalog.getItemImage(QuestInfo.GetItem(questNumber))];
		itemAmount.text = "x"+QuestInfo.GetItemQty(questNumber);
		reward.text = (ItemCatalog.getItemPrice (QuestInfo.GetItem(questNumber)) * QuestInfo.GetItemQty(questNumber)) + " Coins";
		rewardText.text = "Reward";
		box.enabled = true;
		}
	}
}
