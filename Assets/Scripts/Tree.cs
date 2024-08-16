using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

	public int c;
	public int r;
	public int size;
	public int stage;
	public string biome;
	public Sprite sapling1;
	public Sprite sapling2;
	public Sprite sapling3;
	public Sprite tree1;
	public Sprite tree2;
	public Sprite tree3;
	public Sprite palmTree;
	public int axeTaps;
	public long tickPlanted;

	float startScaleX;

	public SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
		startScaleX = transform.GetChild (0).localScale.x;
		if(transform.position.x+.05f < 0 || transform.position.x+.05f >= 2 || transform.position.y-.05f < 0 || transform.position.y-.05f >= 2){
			SpriteRenderer renderer = transform.GetChild (0).GetComponent <SpriteRenderer>();
			renderer.color = Color.gray;
		}
		Random.InitState (c*r*size*stage);
		if ((int)Random.Range(0,1000)>500) {
			Vector3 tempScale = transform.GetChild (0).localScale;
			tempScale.x = 0 - startScaleX;
			transform.GetChild (0).localScale = tempScale;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(tickPlanted > 0){
			stage = (int)((System.DateTime.Now.Ticks - tickPlanted)/((36000000000*24)/3));
			print (stage + " " + size);
		}
		if (stage >= 3) {
			transform.GetChild (0).localScale = new Vector3 (2f, 2f, 2f);
		}
		Vector3 tempPos = transform.position;
		tempPos.z = transform.position.y - .060f;
		transform.position = tempPos;
		if(stage == 0){
			renderer.sprite = sapling1;
		}
		if(stage == 1){
			renderer.sprite = sapling2;
		}
		if(stage == 2){
			renderer.sprite = sapling3;
		}
		if(stage >= 3){
			transform.GetChild (0).localScale = new Vector3(transform.GetChild (0).localScale.x,2f,2f);
			if(size == 1){
				renderer.sprite = tree1;
			}
			if(size == 2){
				renderer.sprite = tree2;
			}
			if(size == 3){
				renderer.sprite = tree3;
			}
			if(biome == "beach"){
				renderer.sprite = palmTree;
			}
		}
	}
}
