using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLogScreen : MonoBehaviour {

	public TextMesh noQuests; 
	public SpriteRenderer questBox1;
	public SpriteRenderer questBox2;
	public SpriteRenderer questBox3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!questBox1.enabled && !questBox2.enabled && !questBox3.enabled) {
			noQuests.text = "No Quests";
		} else {
			noQuests.text = "";
		}
	}
}
