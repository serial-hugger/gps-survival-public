using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScreen : MonoBehaviour {

	public Inventory inventoryScript;
	public SpriteRenderer enemyRenderer;
	public Transform playerHealthBar;
	public Transform enemyHealthBar;
	public Transform enemy;
	public int enemyHealth;
	public int currentEnemyHealth;
	public int enemyAttack;
	public string enemyID;
	public Transform player;
	public SpriteRenderer weaponRender;
	public string playerWeaponID;
	public CardScript card1;
	public CardScript card2;
	public CardScript card3;
	float timeTillPlayerAttack = 5.0f;
	bool playerAttacked;
	float timeTillEnemyAttack = 10.0f;
	bool enemyAttacked;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(card1.flipped && card2.flipped && card3.flipped){
			timeTillPlayerAttack -= 5.0f * Time.deltaTime;
			timeTillEnemyAttack -= 5.0f * Time.deltaTime;
		}
		//player attack
		if(timeTillPlayerAttack < 0 && timeTillPlayerAttack > -2){
			Vector3 tempPos = player.localPosition;
			tempPos.x = Mathf.Lerp (tempPos.x,-.1f,10.0f * Time.deltaTime);
			player.localPosition = tempPos;
			if(!playerAttacked){
				playerAttacked = true;
				currentEnemyHealth -= (card1.damage + card2.damage + card3.damage)*Controller.damageMultiplier();
			}
		}
		if(timeTillPlayerAttack < -2){
			Vector3 tempPos = player.localPosition;
			tempPos.x = Mathf.Lerp (tempPos.x,-.3f,10.0f * Time.deltaTime);
			player.localPosition = tempPos;
		}
		//enemy attack
		if(timeTillEnemyAttack < 0 && timeTillEnemyAttack > -2){
			Vector3 tempPos = enemy.localPosition;
			tempPos.x = Mathf.Lerp (tempPos.x,.1f,10.0f * Time.deltaTime);
			enemy.localPosition = tempPos;
			if(!enemyAttacked){
				enemyAttacked = true;
				Skills.changeCurrentHealth(-EntityCatalog.getEntityAttack(enemyID));
			}
		}
		if(timeTillEnemyAttack < -2){
			Vector3 tempPos = enemy.localPosition;
			tempPos.x = Mathf.Lerp (tempPos.x,.3f,10.0f * Time.deltaTime);
			enemy.localPosition = tempPos;
		}
		if(timeTillEnemyAttack < -5){
			FlipCards ();
		}
		Vector3 tempSize;
		tempSize = enemyHealthBar.localScale;
		tempSize.x = (float)currentEnemyHealth / (float)enemyHealth;
		if (tempSize.x <= 0) {
			tempSize.x = 0;
			if (PetInfo.IsPetHappy ("0010")) {
				Skills.addExp ("slaying",((EntityCatalog.getEntityHealth(enemyID) + EntityCatalog.getEntityAttack(enemyID))*20));
			} else {
				Skills.addExp ("slaying",((EntityCatalog.getEntityHealth(enemyID) + EntityCatalog.getEntityAttack(enemyID))*10));
			}
			string[] drops = EntityCatalog.getEntityDrops (enemyID);
			for(int i2 = 0;i2 < drops.Length;i2++){
				string[] dropSplit = drops[i2].Split('%');
				spawnRandomItem (transform.position.x, transform.position.y,dropSplit[0],int.Parse(dropSplit[1]));
			}
			Reset ();
			inventoryScript.closeWindows ();
		}
		enemyHealthBar.localScale = tempSize;
		tempSize = playerHealthBar.localScale;
		tempSize.x = (float)Skills.currentHealth / (float)Skills.maxHealth;
		if (tempSize.x <= 0) {
			tempSize.x = 0;
			Reset ();
			inventoryScript.closeWindows ();
		}
		playerHealthBar.localScale = tempSize;
	}
	public void FlipCards(){
		Instantiate (Resources.Load ("Effects/CardSound"), new Vector3 (transform.position.x + .05f, transform.position.y - .05f, 0), Quaternion.identity);
		card1.flipped = false;
		card2.flipped = false;
		card3.flipped = false;
		timeTillPlayerAttack = 5.0f;
		timeTillEnemyAttack = 10.0f;
		playerAttacked = false;
		enemyAttacked = false;
	}
	public void Reset(){
		card1.flipped = false;
		card2.flipped = false;
		card3.flipped = false;
		playerAttacked = false;
		enemyAttacked = false;
		timeTillPlayerAttack = 5.0f;
		timeTillEnemyAttack = 10.0f;
		weaponRender.sprite = ItemCatalog.itemSpriteSheet [ItemCatalog.getItemImage (playerWeaponID)];
		enemyRenderer.sprite = EntityCatalog.entitySpriteSheet[EntityCatalog.getEntityImage (enemyID)];
		enemyHealth = EntityCatalog.getEntityHealth (enemyID);
		currentEnemyHealth = EntityCatalog.getEntityHealth (enemyID); 
		enemyAttack = EntityCatalog.getEntityAttack (enemyID);
	}
	void spawnRandomItem(float x, float y,string id,int odds){
		if(PetInfo.IsPetHappy("0001")){
			odds += 25;
		}
		GameObject item;
		ItemDrop dropScript;
		Random.InitState (System.Environment.TickCount);
		if(PetInfo.IsPetHappy("0005")){
			if(id == "0078"){
				if(Random.Range(0,1000)>750){
					id = "0083";
				}
			}
			if(id == "0079"){
				if(Random.Range(0,1000)>750){
					id = "0084";
				}
			}
			if(id == "0080"){
				if(Random.Range(0,1000)>750){
					id = "0085";
				}
			}
			if(id == "0081"){
				if(Random.Range(0,1000)>750){
					id = "0086";
				}
			}
		}
		if(Random.Range(0,100)<odds){
			item = (GameObject)Instantiate (Resources.Load ("DroppedItem"), new Vector3 (x, y, -10), Quaternion.identity);
			dropScript = item.GetComponent <ItemDrop>();
			dropScript.itemID = id;
		}
	}
}
