using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPlant : MonoBehaviour {

	public string plantID;
	public int c;
	public int r;
	public int stage;
	public Sprite stage1;
	public Sprite stage2;
	public Sprite stage3;
	public Sprite stage4;
	public long tickPlanted;
	public string special;
	public float timeMultiplier;
	public int scytheTaps;

	float startScaleX;

	public SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
		startScaleX = transform.GetChild (0).localScale.x;
		if(transform.position.x+.05f < 0 || transform.position.x+.05f >= 2 || transform.position.y-.05f < 0 || transform.position.y-.05f >= 2){
			renderer.color = Color.gray;
		}
		Random.InitState (c*r*stage);
		if ((int)Random.Range(0,1000)>500) {
			Vector3 tempScale = transform.GetChild (0).localScale;
			tempScale.x = 0 - startScaleX;
			transform.GetChild (0).localScale = tempScale;
		}
		stage1 = PlantCatalog.plantSpriteSheet[PlantCatalog.getPlantImage (plantID, 0)];
		stage2 = PlantCatalog.plantSpriteSheet[PlantCatalog.getPlantImage (plantID, 1)];
		stage3 = PlantCatalog.plantSpriteSheet[PlantCatalog.getPlantImage (plantID, 2)];
		stage4 = PlantCatalog.plantSpriteSheet[PlantCatalog.getPlantImage (plantID, 3)];
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tempZPos = transform.position;
		tempZPos.z = transform.position.y+.5f;
		transform.position = tempZPos;
		if(tickPlanted > 0){
			stage = (int)((System.DateTime.Now.Ticks - tickPlanted)/((36000000000*timeMultiplier)/3));
		}
		renderer.sprite = PlantCatalog.plantSpriteSheet[PlantCatalog.getPlantImage(plantID,stage)];
		if(stage == 0){
			renderer.sprite = stage1;
		}
		if(stage == 1){
			renderer.sprite = stage2;
		}
		if(stage == 2){
			renderer.sprite = stage3;
		}
		if(stage >= 3){
			renderer.sprite = stage4;
		}
	}
}
