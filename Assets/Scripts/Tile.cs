using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public int shovelTaps;
	public string type;
	public int c;
	public int r;
	public int treeStage;
	public int treeTimePlanted;
	public int treeSize;
	public int ore;
	public Sprite grassTile;
	public Sprite hoedTile;
	public bool hoed;
	public string biome;
	public bool sandy;
	public bool snowy;
	SpriteRenderer renderer;
	public Sprite[] landImages;
	public ParticleSystem bubbles;
	public bool fishingSpot;

	// Use this for initialization
	void Awake(){
		renderer = transform.GetChild (0).GetComponent <SpriteRenderer>();
	}
	void Start () {
		if(transform.position.x+.05f < 0 || transform.position.x+.05f >= 2 || transform.position.y-.05f < 0 || transform.position.y-.05f >= 2){
			renderer.color = Color.gray;
		}
		if(biome == "beach"){
			sandy = true;
		}
		if(biome == "snowyforest" || biome == "snowyplains"){
			snowy = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(type == "water"){
			Random.InitState (c*r);
			Random.InitState (Random.Range(0,c+r+System.DateTime.Now.Hour+System.DateTime.Now.Day*24+System.DateTime.Now.Month*30));
			if(Random.Range(0,100)>95){
				fishingSpot = true;
			}
			if (fishingSpot) {
				if(!bubbles.isPlaying){
					bubbles.Play ();
				}
			} else {
				if (!bubbles.isStopped) {
					bubbles.Stop ();
				}
			}
		}
		if(type == "land" && hoed == false){
			renderer.sprite = landImages[0];
			if(biome == "swamp"){
				renderer.sprite = landImages[1];
			}
			if(biome == "beach"){
				renderer.sprite = landImages[2];
			}
			if(biome == "snowyforest" || biome == "snowyplains"){
				renderer.sprite = landImages[3];
			}
		}
		if(type == "land" && hoed == true){
			renderer.sprite = hoedTile;
		}
	}
}
