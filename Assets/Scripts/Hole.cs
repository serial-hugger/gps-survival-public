using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour {

	public int ore;
	public int c;
	public int r;
	public int shovelTaps;
	public int pickTaps;
	public GameObject parentTile;

	public Sprite coalOre;
	public Sprite aluminumOre;
	public Sprite copperOre;
	public Sprite ironOre;
	public Sprite goldOre;
	public Sprite diamondOre;

	public SpriteRenderer renderer;
	public SpriteRenderer oreRenderer;

	// Use this for initialization
	void Start () {
		if(transform.position.x+.05f < 0 || transform.position.x+.05f >= 2 || transform.position.y-.05f < 0 || transform.position.y-.05f >= 2){
			renderer.color = Color.gray;
			oreRenderer.color = Color.gray;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(ore==0){
			oreRenderer.sprite = null;
		}
		if(ore==1){
			oreRenderer.sprite = coalOre;
		}
		if(ore==2){
			oreRenderer.sprite = aluminumOre;
		}
		if(ore==3){
			oreRenderer.sprite = ironOre;
		}
		if(ore==4){
			oreRenderer.sprite = copperOre;
		}
		if(ore==5){
			oreRenderer.sprite = goldOre;
		}
		if(ore==6){
			oreRenderer.sprite = diamondOre;
		}
	}
}
