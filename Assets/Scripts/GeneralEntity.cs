using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEntity : MonoBehaviour {

	public float gotoX;
	public float gotoY;
	public float leaveX;
	public float leaveY;
	public string entityID;
	public long timeToDespawn;
	public bool squished;
	public float animateTime;
	public float currentAnimateTime;
	public SpriteRenderer renderer;


	// Use this for initialization
	void Start () {
		bool spawnLeftRight;
		if (Random.Range (0, 1000)>500) {
			spawnLeftRight = true;
		} else {
			spawnLeftRight = false;
		}
		if (spawnLeftRight) {
			if (Random.Range (0, 10000) > 5000) {
				leaveX = -1;
			} else {
				leaveX = 3;
			}
			leaveY = Random.Range (-2.0f, 4.0f);
		} else {
			if (Random.Range (0, 10000) > 5000) {
				leaveY = -1;
			} else {
				leaveY = 3;
			}
			leaveX = Random.Range (-2.0f, 4.0f);
		}
		print ("entity spawned");
		renderer.sprite = EntityCatalog.entitySpriteSheet [EntityCatalog.getEntityImage (entityID)];
		Random.InitState ((int)leaveX * (int)leaveY * gameObject.GetInstanceID());
		animateTime = (float)Random.Range (3,6);
		currentAnimateTime = animateTime;
	}
	
	// Update is called once per frame
	void Update () {
		currentAnimateTime -= 10.0f * Time.deltaTime;
		if(currentAnimateTime < 0){
			currentAnimateTime = animateTime;
			squished = !squished;
		}
		if (squished) {
			Vector3 tempScale = transform.localScale;
			tempScale.y = .47f;
			transform.localScale = tempScale;
			Vector3 tempY = transform.GetChild(0).localPosition;
			tempY.y = 0.1f;
			transform.GetChild (0).localPosition = tempY;
		} else {
			Vector3 tempScale = transform.localScale;
			tempScale.y = .5f;
			transform.localScale = tempScale;
			Vector3 tempY = transform.GetChild(0).localPosition;
			tempY.y = 0.11f;
			transform.GetChild (0).localPosition = tempY;
		}
		if (!(((System.DateTime.Now.Ticks / 3000000000) - 1) > timeToDespawn)) {
			Vector3 tempPos = transform.position;
			tempPos.x = Mathf.Lerp (transform.position.x, gotoX, .05f * Time.deltaTime);
			tempPos.y = Mathf.Lerp (transform.position.y, gotoY, .05f * Time.deltaTime);
			tempPos.z = tempPos.y;
			transform.position = tempPos;
		} else {
			if(transform.position.x <= -.99f || transform.position.x >= 2.99f || transform.position.y <= -.99f || transform.position.y >= 2.99f){
				GameObject.Destroy (gameObject);
			}
			Vector3 tempPos = transform.position;
			tempPos.x = Mathf.Lerp (transform.position.x, leaveX, .05f * Time.deltaTime);
			tempPos.y = Mathf.Lerp (transform.position.y, leaveY, .05f * Time.deltaTime);
			tempPos.z = tempPos.y;
			transform.position = tempPos;
		}
	}
}
