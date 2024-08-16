using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAnimal : MonoBehaviour {

	Vector3 startingPosition;
	public float xMove;
	public float yMove;
	public float timeTillChange;
	public SpriteRenderer spriteRenderer;
	public GeneralPlacedItem ownedTile;
	public int ownedTileX;
	public int ownedTileY;
	public string animal;
	public Sprite chicken;
	public Sprite cow;
	public Sprite pig;
	public float speed = 1.0f;
	public float range = 0.1f;
	public int id;
	public GameObject readyBubble;

	// Use this for initialization
	void Start () {
		startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(ownedTile!=null){
			bool used = false;
			string lastDataChunk;
			string lastDataX;
			string lastDataY;
			for(int i2 = 0; i2 < DailyInfo.dailyActions.Count;i2++){
				lastDataChunk = DailyInfo.grabData ("animalUsed", i2, 0);
				lastDataX = DailyInfo.grabData ("animalUsed", i2, 1);
				lastDataY = DailyInfo.grabData ("animalUsed", i2, 2);
				if(lastDataChunk == "END"){
					break;
				}
				if(lastDataChunk == CameraLocation.currentChunkRandom.ToString() && lastDataX == ownedTileX.ToString() && lastDataY == ownedTileY.ToString()){
					used = true;
				}
			}
			if (!used) {
				readyBubble.SetActive (true);
			} else {
				readyBubble.SetActive (false);
			}
		}
		if (ownedTile == null) {
			speed = 1.0f;
			range = 0.1f;
		} else {
			speed = .2f;
			range = .005f;
		}
		if(animal == "chicken"){
			spriteRenderer.sprite = chicken;
		}
		if(animal == "cow"){
			spriteRenderer.sprite = cow;
		}
		if(animal == "pig"){
			spriteRenderer.sprite = pig;
		}
		timeTillChange -= 1.0f * Time.deltaTime;
		if(timeTillChange < 0){
			Random.InitState ((int)System.DateTime.Now.Ticks + gameObject.GetInstanceID());
			timeTillChange = 5.0f;
			xMove = 0.0f + Random.Range (-speed*2,speed*2);
			if(Mathf.Abs(transform.position.x - startingPosition.x)>range){
				if(transform.position.x > startingPosition.x){
					xMove = -speed;
				}
				if(transform.position.x < startingPosition.x){
					xMove = speed;
				}
			}
			if(xMove > 0){
				spriteRenderer.flipX = true;
			}
			if(xMove < 0){
				spriteRenderer.flipX = false;
			}
			yMove = 0.0f + Random.Range (-speed*2,speed*2);
			if(Mathf.Abs(transform.position.y - startingPosition.y)>range){
				if(transform.position.y > startingPosition.y){
					yMove = -speed;
				}
				if(transform.position.y < startingPosition.y){
					yMove = speed;
				}
			}
		}
		if (startingPosition.x > 0 && startingPosition.x < 2 && startingPosition.y > 0 && startingPosition.y < 2) {
			Vector3 tempPos = transform.position;
			tempPos.x += xMove / 50 * Time.deltaTime;
			tempPos.y += yMove / 50 * Time.deltaTime;
			tempPos.z = tempPos.y;
			transform.position = tempPos;
		} else {
			spriteRenderer.color = Color.gray;
		}
	}
}
