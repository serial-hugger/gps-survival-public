using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	public string npcClass;
	public int chunkRandom;
	public string npcQuests;
	public int npcImage;
	public string item = "0000";
	public string itemName = "";
	public int amount = 0;
	public string text = "";
	public SpriteRenderer renderer;
	public bool inactive;
	public string id;
	public string name;

	// Use this for initialization
	void Start () {
		item = Quests.getQuest ((System.DateTime.Now.Year+(System.DateTime.Now.Month*40)+System.DateTime.Now.Day)+chunkRandom,npcQuests)[0];
		amount = int.Parse(Quests.getQuest ((System.DateTime.Now.Year+(System.DateTime.Now.Month*40)+System.DateTime.Now.Day)+chunkRandom,npcQuests)[1]);
		itemName = ItemCatalog.getItemName (item);
	}
	
	// Update is called once per frame
	void Update () {
		if((transform.position.x < 0 || transform.position.x > 2 || transform.position.y < 0 || transform.position.y > 2)){
			inactive = true;
		}
		if(inactive){
			renderer.color = Color.gray;
		}
		Vector3 tempPos = transform.position;
		tempPos.z = tempPos.y;
		transform.position = tempPos;
		renderer.sprite = QuestInfo.npcSpritesGlobal[npcImage];
	}
	public void UpdateInfo(){
		if(!inactive){
			if ((!QuestInfo.HoldingQuestOfNPC (id,-1)) || QuestInfo.HoldingQuestOfNPC (id,0)) {
				text = Quests.getQuest ((System.DateTime.Now.Year + (System.DateTime.Now.Month * 40) + System.DateTime.Now.Day)+chunkRandom, npcQuests) [2];
			}
			if(QuestInfo.HoldingQuestOfNPC (id,1)){
				text = "Have you completed\nmy request?";
			}
			if(QuestInfo.HoldingQuestOfNPC (id,2)){
				text = "Thanks!";
			}
		}
	}
}
