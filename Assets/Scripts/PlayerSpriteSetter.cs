using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteSetter : MonoBehaviour {

	public SpriteRenderer head;
	public SpriteRenderer hands;
	public bool animatedHands;
	public SpriteRenderer hair;
	public SpriteRenderer shirt;
	public SpriteRenderer pants;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		head.sprite = AccountInfo.headSpriteSheet[AccountInfo.skin];
		if(!animatedHands){
			hands.sprite = AccountInfo.handSpriteSheet[(AccountInfo.skin*3)+1];
		}
		if (AccountInfo.gender == 0) {
			hair.sprite = AccountInfo.maleHairSpriteSheet [AccountInfo.hair];
		} else {
			hair.sprite = AccountInfo.femaleHairSpriteSheet [AccountInfo.hair];
		}
		shirt.sprite = AccountInfo.shirtSpriteSheet [AccountInfo.shirt];
		pants.sprite = AccountInfo.pantsSpriteSheet [AccountInfo.pants];
	}
}
