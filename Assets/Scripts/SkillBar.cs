using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBar : MonoBehaviour {

	public string skill;
	public TextMesh level;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3 (Skills.getPercentage(skill), 1, 1);
		level.text = (Skills.getLevel (skill)).ToString();
	}
}
