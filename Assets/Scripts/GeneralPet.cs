using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPet : MonoBehaviour {

	public GameObject player;
	public string petID = "";
	public int costume;
	public SpriteRenderer renderer;
	public float animate = 0.0f;
	public bool secondWalk = false;
	public bool travel = false;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		Vector3 tempPos = transform.position;
		tempPos.z = tempPos.y;
		transform.position = tempPos;
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector2.Distance(transform.position,player.transform.position) > .6f){
			if(player.transform.position.x > 1.9f){
				Vector3 tempPos = transform.position;
				tempPos.x = player.transform.position.x + .1f;
				tempPos.y = player.transform.position.y;
				transform.position = tempPos;
			}
			if(player.transform.position.x < .1f){
				Vector3 tempPos = transform.position;
				tempPos.x = player.transform.position.x - .1f;
				tempPos.y = player.transform.position.y;
				transform.position = tempPos;
			}
			if(player.transform.position.y > 1.9f){
				Vector3 tempPos = transform.position;
				tempPos.x = player.transform.position.x;
				tempPos.y = player.transform.position.y +.1f;
				transform.position = tempPos;
			}
			if(player.transform.position.y < .1f){
				Vector3 tempPos = transform.position;
				tempPos.x = player.transform.position.x;
				tempPos.y = player.transform.position.y-.1f;
				transform.position = tempPos;
			}
			travel = false;
		}
		if (player.transform.position.x > transform.position.x) {
			renderer.flipX = true;
		} else {
			renderer.flipX = false;
		}
		animate += 10.0f * Time.deltaTime;
		if(animate > 5.0f){
			secondWalk = !secondWalk;
			animate = 0.0f;
		}
		if(Vector2.Distance(player.transform.position,transform.position)>.2f && !travel){
			travel = true;
			if(PetInfo.GetPetFitness(petID)<86000){
				Instantiate (Resources.Load ("Effects/WalkHeartEffect"), new Vector3 (transform.position.x,transform.position.y+.05f, -20), Quaternion.identity);
				PetInfo.WalkPet (petID,1000);
			}
		}
		if(Vector2.Distance(player.transform.position,transform.position)<.1f){
			travel = false;
		}
		if(travel){
			if (secondWalk) {
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
			Vector3 tempPos = transform.position;
			tempPos.x += (player.transform.position.x - tempPos.x)*.5f*Time.deltaTime;
			tempPos.y += (player.transform.position.y - tempPos.y)*.5f*Time.deltaTime;
			tempPos.z = tempPos.y;
			transform.position = tempPos;
		}
		if (!travel) {
			renderer.sprite = PetCatalog.petSpriteSheet [PetCatalog.getPetImage (petID, 0, costume)];
		} else {
			if (!secondWalk) {
				renderer.sprite = PetCatalog.petSpriteSheet [PetCatalog.getPetImage (petID, 1, costume)];
			} else {
				renderer.sprite = PetCatalog.petSpriteSheet [PetCatalog.getPetImage (petID, 2, costume)];
			}
		}
	}
}
