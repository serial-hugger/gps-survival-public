using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	int walkSide;
	bool spriteMoveRight;
	float spriteTime;
	public Transform camera;

	public SpriteRenderer shoes;
	public SpriteRenderer hands;

	public Sprite shoesDown1;
	public Sprite shoesDown2;
	public Sprite shoesDown3;

	public Sprite handsDown1;
	public Sprite handsDown2;
	public Sprite handsDown3;
	public bool startSet;
	public float lastLon;
	public float lastLat;

	public Inventory inventoryScript;

	public static string spellUsed = "null";
	public static float secondsOfSpellLeft;

	public SpriteRenderer gem;
	public TextMesh gemTime;

	// Use this for initialization
	void Start () {
		Random.InitState (257);
	}
	
	// Update is called once per frame
	void Update () {
		if (spellUsed != "null") {
			gem.sprite = ItemCatalog.itemSpriteSheet[ItemCatalog.getItemImage (spellUsed)];
			if ((int)(secondsOfSpellLeft % 60) >= 10) {
				gemTime.text = (int)(secondsOfSpellLeft / 60) + ":" + (int)(secondsOfSpellLeft % 60);
			} else {
				gemTime.text = (int)(secondsOfSpellLeft / 60) + ":0" + (int)(secondsOfSpellLeft % 60);
			}
		} else {
			gem.sprite = null;
			gemTime.text = "";
		}
		if(inventoryScript.window){
			gem.sprite = null;
			gemTime.text = "";
		}
		if(secondsOfSpellLeft < 0){
			spellUsed = "null";
		}
		secondsOfSpellLeft -= 1f * Time.deltaTime;
		if (lastLon != CameraLocation.lastLon || lastLat != CameraLocation.lastLat) {
			lastLon = CameraLocation.lastLon;
			lastLat = CameraLocation.lastLat;
			if(startSet){
				for(int i = 0;i<5;i++){
					Random.InitState ((354*i+1)+(int)System.DateTime.Now.Ticks);
					if(Random.Range(0,100)<5){
						Inventory.changeChargesForSlot (i,1);
					}
				}
			}
			startSet = true;
		}
		Vector3 tempPos = transform.position;
		tempPos.x = Mathf.Lerp(transform.position.x,camera.position.x,5f * Time.deltaTime);
		tempPos.y = Mathf.Lerp(transform.position.y,camera.position.y,5f * Time.deltaTime);
		tempPos.z = tempPos.y;
		transform.position = tempPos;
		if(Mathf.Abs(transform.position.x - camera.position.x)>.2 || Mathf.Abs(transform.position.y - camera.position.y)>.2){
			tempPos.x = camera.position.x;
			tempPos.y = camera.position.y;
			transform.position = tempPos;
		}
		if(spriteTime <= 0.0f){
			spriteTime = 1.0f;
			if(spriteMoveRight){
				walkSide += 1;
			}
			if(!spriteMoveRight){
				walkSide -= 1;
			}
			if(walkSide==-1){
				shoes.sprite = shoesDown1;
				hands.sprite = AccountInfo.handSpriteSheet[(AccountInfo.skin*3)];
			}
			if(walkSide==0){
				shoes.sprite = shoesDown2;
				hands.sprite = AccountInfo.handSpriteSheet[(AccountInfo.skin*3)+1];
			}
			if(walkSide==1){
				shoes.sprite = shoesDown3;
				hands.sprite = AccountInfo.handSpriteSheet[(AccountInfo.skin*3)+2];
			}
			Vector3 tempY = transform.GetChild(0).localPosition;
			if (walkSide != 0) {
				spriteMoveRight = !spriteMoveRight;
				tempY.y = 0.1f;
			} else {
				tempY.y = 0.108f;
			}
			transform.GetChild (0).localPosition = tempY;
		}
		if ((Vector2.Distance (transform.position, camera.position) <= .0001f)) {
			shoes.sprite = shoesDown2;
			hands.sprite = AccountInfo.handSpriteSheet[(AccountInfo.skin*3)+1];
		} else {
			spriteTime -= 6f * Time.deltaTime;
		}
	}
}
