using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

	public SpriteRenderer renderer;
	public GameObject noMap;
	public GameObject mapHelp;
	public GameObject mapNote;
	public GameObject mapInternet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (QuestInfo.map != null) {
			Texture2D map = QuestInfo.map;
			renderer.sprite = Sprite.Create (map, new Rect (0, 0, map.width, map.height), new Vector2 (0.5f, 0.5f));
			noMap.SetActive (false);
			mapHelp.SetActive (true);
			mapNote.SetActive (true);
			mapInternet.SetActive (false);
		} else {
			mapInternet.SetActive (false);
			renderer.sprite = null;
			if (Application.internetReachability == NetworkReachability.NotReachable) {
				noMap.SetActive (false);
				mapHelp.SetActive (false);
				mapNote.SetActive (false);
				mapInternet.SetActive (true);
			}
		}
	}
}
