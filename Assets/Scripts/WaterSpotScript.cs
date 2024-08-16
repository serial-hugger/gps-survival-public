using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpotScript : MonoBehaviour {

	public GameObject bobber;
	private float bobberMove = -0.01f;
	public int spot;
	public FishingScript mainScript;
	public SpriteRenderer spotRender;
	public SpriteRenderer bobberRender;
	public float nextFakeOut;
	public float catchTime;
	GameObject line = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 bobPos;
		if(bobber.transform.localPosition.y < -0.01f){
			bobberMove = 0.01f;
		}
		if(bobber.transform.localPosition.y > 0.0f){
			bobberMove = -0.01f;
		}
		if(mainScript.fishing == spot){
			catchTime -= 1.0f * Time.deltaTime;
			nextFakeOut -= 1.0f * Time.deltaTime;
			if (catchTime < 0) {
				mainScript.catchable = true;
				bobPos = bobber.transform.localPosition;
				bobPos.y = -0.02f;
				Random.InitState ((int)(System.DateTime.Now.Ticks / spot));
				bobPos.x = Random.Range (-0.01f, 0.01f);
				bobber.transform.localPosition = bobPos;
				if(line == null){
					line = (GameObject)Instantiate (Resources.Load ("Effects/LineSound"), new Vector3 (transform.position.x, transform.position.y, 0), Quaternion.identity);
					Instantiate (Resources.Load ("Effects/BobberSound"), new Vector3 (transform.position.x, transform.position.y, 0), Quaternion.identity);
				}
			} else {
				bobPos = bobber.transform.localPosition;
				bobPos.x = 0.0f;
				bobber.transform.localPosition = bobPos;
			}
			if(nextFakeOut < 0){
				Instantiate (Resources.Load ("Effects/BobberSound"), new Vector3 (transform.position.x, transform.position.y, 0), Quaternion.identity);
				Random.InitState ((int)(System.DateTime.Now.Ticks/spot));
				nextFakeOut = (float)Random.Range (5,25);
				bobPos = bobber.transform.localPosition;
				bobPos.y = -0.02f;
				bobber.transform.localPosition = bobPos;
			}
		}
		bobPos = bobber.transform.localPosition;
		bobPos.y += bobberMove * Time.deltaTime;
		bobber.transform.localPosition = bobPos;
		if (mainScript.fishing == spot) {
			spotRender.enabled = true;
			bobberRender.enabled = true;
		} else {
			if (mainScript.fishing == 0) {
				spotRender.enabled = true;
			} else {
				spotRender.enabled = false;
			}
			bobberRender.enabled = false;
		}
	}
	public void Select(){
		if(mainScript.fishing == 0){
			mainScript.reeling.enabled = true;
			Random.InitState ((int)(System.DateTime.Now.Ticks/spot));
			nextFakeOut = (float)Random.Range (5,25);
			catchTime = (float)Random.Range (25,200);
			if (Random.Range (0,10000) > 8000) {
				mainScript.trash = false;
			} else {
				mainScript.trash = true;
			}
			mainScript.fishing = spot;
			mainScript.playerZ = 0.0f;
			mainScript.poleZ = 0.0f;
		}
	}
}
