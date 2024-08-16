using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPlacedItem : MonoBehaviour {

	public int c;
	public int r;
	public string buildingID;
	public string destroyer;
	public int axeTaps;
	public int pickTaps;
	public int shovelTaps;
	public string item;
	public SpriteRenderer renderer;
	public GameObject sprite;
	public string special;
	public float yOffset;
	public long lastUsed;
	public bool complete;
	public string random;
	public Sprite houseReady;
	public Sprite dungeonReady;
	public Sprite animalPlot;
	public string[] storageSlots = new string[]{"null","null","null","null","null","null","null","null","null"};
	public CameraLocation cameraScript;

	// Use this for initialization
	void Start () {
		cameraScript = GameObject.Find ("ViewPoint").GetComponent<CameraLocation> ();
		destroyer = ItemCatalog.getItemDestroyer (item);
		special = ItemCatalog.getItemSpecial (item);
		yOffset = ItemCatalog.getItemYOffset (item);
		renderer.sprite = ItemCatalog.itemSpriteSheet[ItemCatalog.getItemImage(item)];
		if(transform.position.x+.05f < 0 || transform.position.x+.05f >= 2 || transform.position.y-.05f < 0 || transform.position.y-.05f >= 2){
			renderer.color = Color.gray;
		}
		//spawn animal plot entities
		if(item == "0160"){
			var animal = (GameObject)Instantiate (Resources.Load ("Animal"), new Vector3 (transform.position.x + .05f, transform.position.y  - .05f, -10), Quaternion.identity);
			WanderingAnimal animalScript = animal.GetComponent<WanderingAnimal> ();
			animalScript.ownedTile = this;
			animalScript.animal = "chicken";
			animalScript.ownedTileX = c;
			animalScript.ownedTileY = r;
		}
		if(item == "0161"){
			var animal = (GameObject)Instantiate (Resources.Load ("Animal"), new Vector3 (transform.position.x + .05f, transform.position.y  - .05f, -10), Quaternion.identity);
			WanderingAnimal animalScript = animal.GetComponent<WanderingAnimal> ();
			animalScript.ownedTile = this;
			animalScript.animal = "cow";
			animalScript.ownedTileX = c;
			animalScript.ownedTileY = r;
		}
		if(item == "0162"){
			var animal = (GameObject)Instantiate (Resources.Load ("Animal"), new Vector3 (transform.position.x + .05f, transform.position.y  - .05f, -10), Quaternion.identity);
			WanderingAnimal animalScript = animal.GetComponent<WanderingAnimal> ();
			animalScript.ownedTile = this;
			animalScript.animal = "pig";
			animalScript.ownedTileX = c;
			animalScript.ownedTileY = r;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(buildingID == "0109"){
			if((System.DateTime.Now.Year + (System.DateTime.Now.Month * 40) + System.DateTime.Now.Day)>lastUsed){
				renderer.sprite = houseReady;
			}else{
				renderer.sprite = ItemCatalog.itemSpriteSheet [ItemCatalog.getItemImage (item)];
			}
		}
		if(item == "0160" || item == "0161" || item == "0162"){
			renderer.sprite = animalPlot;
		}
		if(item == "0125"){
			if(complete){
				renderer.sprite = dungeonReady;
			}
			if(!complete){
				renderer.sprite = ItemCatalog.itemSpriteSheet[ItemCatalog.getItemImage("0125")];
			}
			if(complete && (int)(lastUsed/(System.TimeSpan.TicksPerDay*7)) != (int)(System.DateTime.Now.Ticks/(System.TimeSpan.TicksPerDay*7))){
				cameraScript.removeFromChunk ("placeable",c,r);
				cameraScript.addToChunk (Application.persistentDataPath + Controller.slot + "/chunks/" + CameraLocation.chunkLat + " " + CameraLocation.chunkLon,"type:placeable;x:" + c + ";y:" + r + ";item:0125;completed:false;lastused:"+0);
				complete = false;
				lastUsed = 0;
			}
		}
		Vector3 tempPos = sprite.transform.localPosition;
		tempPos.y = yOffset;
		sprite.transform.localPosition = tempPos;
		Vector3 tempZPos = transform.position;
		tempZPos.z = transform.position.y+.5f;
		transform.position = tempZPos;
	}

	public void UpdateItemInfo(){
		destroyer = ItemCatalog.getItemDestroyer (item);
		special = ItemCatalog.getItemSpecial (item);
		yOffset = ItemCatalog.getItemYOffset (item);
		renderer.sprite = ItemCatalog.itemSpriteSheet [ItemCatalog.getItemImage (item)];
		if(transform.position.x+.05f < 0 || transform.position.x+.05f >= 2 || transform.position.y-.05f < 0 || transform.position.y-.05f >= 2){
			renderer.color = Color.gray;
		}
	}
}
