using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLimits : MonoBehaviour {

	public int latAlign;
	public int lonAlign;
	public GameObject leftBorder;
	public GameObject rightBorder;
	public GameObject nearBorder;
	public CameraLocation cameraScript;
	public Sprite cityGrid;
	public Sprite townGrid;
	public Sprite villageGrid;
	public SpriteRenderer topGrid;
	public SpriteRenderer bottomGrid;
	public SpriteRenderer leftGrid;
	public SpriteRenderer rightGrid;
	public SpriteRenderer topLeftGrid;
	public SpriteRenderer topRightGrid;
	public SpriteRenderer bottomLeftGrid;
	public SpriteRenderer bottomRightGrid;
	public float lastChunkLat;
	public float lastChunkLon;
	public bool firstGenerate;
	public float nextUpdate = 10.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(!firstGenerate && (CameraLocation.loading<0)){
			UpdateTile ();
			firstGenerate = true;
		}
		if ((CameraLocation.chunkLat != lastChunkLat || CameraLocation.chunkLon != lastChunkLon)) {
			lastChunkLat = CameraLocation.chunkLat;
			lastChunkLon = CameraLocation.chunkLon;
			firstGenerate = false;
			UpdateTile ();
		}
	}
	void UpdateTile(){
		if (cameraScript.getChunkPlaceID (latAlign, lonAlign) == cameraScript.getChunkPlaceID (0, 0)) {
			nearBorder.SetActive (false);
		} else {
			nearBorder.SetActive (true);
		}
		if(latAlign==1 && lonAlign==0){
			if (cameraScript.getChunkPlaceType (0, 0 + 1) == "city") {
				rightGrid.enabled = true;
				rightGrid.sprite = cityGrid;
			} else if (cameraScript.getChunkPlaceType (0, 0 + 1) == "town") {
				rightGrid.enabled = true;
				rightGrid.sprite = townGrid;
			} else if (cameraScript.getChunkPlaceType (0, 0 + 1) == "village") {
				rightGrid.enabled = true;
				rightGrid.sprite = villageGrid;
			} else {
				rightGrid.enabled = false;
			}
			if (cameraScript.getChunkPlaceType (0, 0 - 1) == "city") {
				leftGrid.enabled = true;
				leftGrid.sprite = cityGrid;
			} else if (cameraScript.getChunkPlaceType (0, 0 - 1) == "town") {
				leftGrid.enabled = true;
				leftGrid.sprite = townGrid;
			} else if (cameraScript.getChunkPlaceType (0, 0 - 1) == "village") {
				leftGrid.enabled = true;
				leftGrid.sprite = villageGrid;
			} else {
				leftGrid.enabled = false;
			}
			if (cameraScript.getChunkPlaceType (0+1, 0) == "city") {
				topGrid.enabled = true;
				topGrid.sprite = cityGrid;
			} else if (cameraScript.getChunkPlaceType (0+1, 0) == "town") {
				topGrid.enabled = true;
				topGrid.sprite = townGrid;
			} else if (cameraScript.getChunkPlaceType (0+1, 0) == "village") {
				topGrid.enabled = true;
				topGrid.sprite = villageGrid;
			} else {
				topGrid.enabled = false;
			}
			if (cameraScript.getChunkPlaceType (0-1, 0) == "city") {
				bottomGrid.enabled = true;
				bottomGrid.sprite = cityGrid;
			} else if (cameraScript.getChunkPlaceType (0-1, 0) == "town") {
				bottomGrid.enabled = true;
				bottomGrid.sprite = townGrid;
			} else if (cameraScript.getChunkPlaceType (0-1, 0) == "village") {
				bottomGrid.enabled = true;
				bottomGrid.sprite = villageGrid;
			} else {
				bottomGrid.enabled = false;
			}
			//corners
			if (cameraScript.getChunkPlaceType (0+1, 0 + 1) == "city") {
				topRightGrid.enabled = true;
				topRightGrid.sprite = cityGrid;
			} else if (cameraScript.getChunkPlaceType (0+1, 0 + 1) == "town") {
				topRightGrid.enabled = true;
				topRightGrid.sprite = townGrid;
			} else if (cameraScript.getChunkPlaceType (0+1, 0 + 1) == "village") {
				topRightGrid.enabled = true;
				topRightGrid.sprite = villageGrid;
			} else {
				topRightGrid.enabled = false;
			}
			if (cameraScript.getChunkPlaceType (0+1, 0 - 1) == "city") {
				topLeftGrid.enabled = true;
				topLeftGrid.sprite = cityGrid;
			} else if (cameraScript.getChunkPlaceType (0+1, 0 - 1) == "town") {
				topLeftGrid.enabled = true;
				topLeftGrid.sprite = townGrid;
			} else if (cameraScript.getChunkPlaceType (0+1, 0 - 1) == "village") {
				topLeftGrid.enabled = true;
				topLeftGrid.sprite = villageGrid;
			} else {
				topLeftGrid.enabled = false;
			}
			if (cameraScript.getChunkPlaceType (0-1, 0+1) == "city") {
				bottomRightGrid.enabled = true;
				bottomRightGrid.sprite = cityGrid;
			} else if (cameraScript.getChunkPlaceType (0-1, 0+1) == "town") {
				bottomRightGrid.enabled = true;
				bottomRightGrid.sprite = townGrid;
			} else if (cameraScript.getChunkPlaceType (0-1, 0+1) == "village") {
				bottomRightGrid.enabled = true;
				bottomRightGrid.sprite = villageGrid;
			} else {
				bottomRightGrid.enabled = false;
			}
			if (cameraScript.getChunkPlaceType (0-1, 0-1) == "city") {
				bottomLeftGrid.enabled = true;
				bottomLeftGrid.sprite = cityGrid;
			} else if (cameraScript.getChunkPlaceType (0-1, 0-1) == "town") {
				bottomLeftGrid.enabled = true;
				bottomLeftGrid.sprite = townGrid;
			} else if (cameraScript.getChunkPlaceType (0-1, 0-1) == "village") {
				bottomLeftGrid.enabled = true;
				bottomLeftGrid.sprite = villageGrid;
			} else {
				bottomLeftGrid.enabled = false;
			}
		}
		if(latAlign!=0){
			if (cameraScript.getChunkPlaceID (latAlign, 0 + 1) == cameraScript.getChunkPlaceID (latAlign, lonAlign)) {
				rightBorder.SetActive(false);
			} else {
				rightBorder.SetActive(true);
			}
			if (cameraScript.getChunkPlaceID (latAlign, lonAlign - 1) == cameraScript.getChunkPlaceID (latAlign, lonAlign)) {
				leftBorder.SetActive(false);
			} else {
				leftBorder.SetActive(true);
			}
		}
		if(lonAlign!=0){
			if (cameraScript.getChunkPlaceID (latAlign+1, lonAlign) == cameraScript.getChunkPlaceID (latAlign, lonAlign)) {
				rightBorder.SetActive(false);
			} else {
				rightBorder.SetActive(true);
			}
			if (cameraScript.getChunkPlaceID (latAlign-1, lonAlign) == cameraScript.getChunkPlaceID (latAlign, lonAlign)) {
				leftBorder.SetActive(false);
			} else {
				leftBorder.SetActive(true);
			}
		}
	}
}
