using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour {

	float gravity = 10.0f;
	float yChange = 5.0f;
	float yOffset = 0.0f;
	float yStart;
	float xChange;
	public string itemID;
	public SpriteRenderer renderer;
	public Collider collider;

	// Use this for initialization
	void Start () {
		Random.InitState (System.Environment.TickCount + int.Parse(itemID) + gameObject.GetInstanceID());
		yChange = Random.Range (.2f,.6f);
		xChange = Random.Range (-.3f,.3f);
		yStart = transform.position.y;
		renderer.sprite = ItemCatalog.itemSpriteSheet[ItemCatalog.getItemImage (itemID)];
	}
	
	// Update is called once per frame
	void Update () {
		if (yOffset > -.0001) {
			yOffset += yChange * Time.deltaTime;
			Vector3 tempPosition = transform.position; 
			tempPosition.y = yStart + yOffset;
			tempPosition.x += xChange / 2f * Time.deltaTime;
			if(tempPosition.x >= 2){
				tempPosition.x = 2;
			}
			if(tempPosition.x <= 0){
				tempPosition.x = 0;
			}
			transform.position = tempPosition;
			yChange -= gravity / 6f * Time.deltaTime;
		} else {
			collider.enabled = true;
		}
	}
}
