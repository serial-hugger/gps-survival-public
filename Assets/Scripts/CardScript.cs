using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour {

	public SpriteRenderer renderer;
	public TextMesh nameRenderer;
	public TextMesh attackRenderer;
	public BattleScreen battleScript;
	public Sprite cardFront;
	public Sprite cardBack;
	public string text;
	public int damage;
	public int type;
	public bool flipped;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (flipped) {
			Vector3 tempRot = transform.localEulerAngles;
			tempRot.y = Mathf.Lerp (tempRot.y, 0,10.0f * Time.deltaTime);
			transform.localEulerAngles = tempRot;
		} else {
			Vector3 tempRot = transform.localEulerAngles;
			tempRot.y = Mathf.Lerp (tempRot.y, 180,10.0f * Time.deltaTime);
			transform.localEulerAngles = tempRot;
		}
		if (transform.localEulerAngles.y > 90) {
			renderer.sprite = cardBack;
			nameRenderer.text = "";
			attackRenderer.text = "";
		} else {
			renderer.sprite = cardFront;
			nameRenderer.text = text;
			attackRenderer.text = damage.ToString();
		}
	}

	public void SelectCard(){
		if(!flipped){
			Instantiate (Resources.Load ("Effects/CardSound"), new Vector3 (transform.position.x + .05f, transform.position.y - .05f, 0), Quaternion.identity);
		flipped = true;
			if(type == 1){
				Random.InitState ((int)System.DateTime.Now.Ticks);
				string cardID;
				if (Random.Range (0, 100) > 10) {
					cardID = CardCatalog.getCardID(CardCatalog.getSlotWithWeaponID (battleScript.playerWeaponID),"weapon");
					cardFront = CardCatalog.cardSpriteSheet [CardCatalog.getCardImage(cardID,"weapon")];
					text = CardCatalog.getCardName (cardID,"weapon");
					damage = CardCatalog.getCardAttack (cardID,"weapon");
				} else {
					cardID = CardCatalog.getCardID(CardCatalog.getSlotWithWeaponID (battleScript.playerWeaponID)+1,"weapon");
					cardFront = CardCatalog.cardSpriteSheet [CardCatalog.getCardImage (cardID,"weapon")];
					text = CardCatalog.getCardName (cardID,"weapon");
					damage = CardCatalog.getCardAttack (cardID,"weapon");
				}
			}
			if(type == 2){
				Random.InitState ((int)System.DateTime.Now.Ticks);
				string cardID;
				cardID = CardCatalog.getCardID(Random.Range(0,CardCatalog.bondCards.Length),"bond");
				print (cardID);
				cardFront = CardCatalog.cardSpriteSheet [CardCatalog.getCardImage(cardID,"bond")];
				text = CardCatalog.getCardName (cardID,"bond");
				damage = NPCInfo.GetHighestCompleteOfType(CardCatalog.getCardNpc(cardID));
			}
			if(type == 3){
				Random.InitState ((int)System.DateTime.Now.Ticks);
				string cardID;
				cardID = CardCatalog.getCardID(Random.Range(0,CardCatalog.bondCards.Length),"skill");
				print (cardID);
				cardFront = CardCatalog.cardSpriteSheet [CardCatalog.getCardImage(cardID,"skill")];
				text = CardCatalog.getCardName (cardID,"skill");
				damage = Skills.getLevel(CardCatalog.getCardSkill(cardID));
			}
	}
	}
}
