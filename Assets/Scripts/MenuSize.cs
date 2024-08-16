using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSize : MonoBehaviour {

	public float portraitSizeX;
	public float portraitSizeY;
	public float landscapeSizeX;
	public float landscapeSizeY;
	public float portraitPositionY;
	public float landscapePositionY;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Controller.isPortrait()){
			Vector3 tempSize = transform.localScale;
			tempSize.x = portraitSizeX;
			tempSize.y = portraitSizeY;
			transform.localScale = tempSize;
			Vector3 tempPos = transform.localPosition;
			tempPos.y = portraitPositionY;
			transform.localPosition = tempPos;
		}
		if(Controller.isLandscape()){
			Vector3 tempSize = transform.localScale;
			tempSize.x = landscapeSizeX;
			tempSize.y = landscapeSizeY;
			transform.localScale = tempSize;
			Vector3 tempPos = transform.localPosition;
			tempPos.y = landscapePositionY;
			transform.localPosition = tempPos;
		}
	}
}
