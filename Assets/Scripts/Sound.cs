using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

	public AudioClip[] sounds;
	public AudioSource audio;
	int randomSound;
	public bool randomPitch;

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}
	// Use this for initialization
	void Start () {
		randomSound = (int)(Time.time*100)%(sounds.Length);
		if(randomPitch){
			Random.InitState ((int)System.DateTime.Now.Ticks);
			audio.pitch = Random.Range (0.8f,1.2f);
		}
		audio.clip = sounds [randomSound];
		if(AccountInfo.soundOption){
			audio.Play();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!audio.isPlaying || !AccountInfo.soundOption){
			Destroy (gameObject);
		}
	}
}
