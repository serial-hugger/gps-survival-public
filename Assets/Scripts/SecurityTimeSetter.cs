using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Security.AccessControl;
using System.Security.Principal;

public class SecurityTimeSetter : MonoBehaviour {

	public long nextSecure;

	// Use this for initialization
	void Start () {
		nextSecure = System.DateTime.Now.Ticks;
	}
	
	// Update is called once per frame
	void Update () {
		if(Controller.slot != "/toybox"){
			if(System.DateTime.Now.Ticks >= nextSecure){
				nextSecure = System.DateTime.Now.Ticks + (System.TimeSpan.TicksPerSecond/2);
				Secure();
			}
		}
	}
	void OnApplicationQuit(){
		Secure();
	}

	void Secure(){
		System.DateTime time = System.DateTime.Now;
		try{
			File.SetLastWriteTime (Application.persistentDataPath + Controller.slot + "/backpack",time);
		}catch(IOException e){
			File.Copy (Application.persistentDataPath + Controller.slot + "/backpack",Application.persistentDataPath + Controller.slot + "/backpack1");
			File.Delete (Application.persistentDataPath + Controller.slot + "/backpack");
			File.Move (Application.persistentDataPath + Controller.slot + "/backpack1",Application.persistentDataPath + Controller.slot + "/backpack");
		}
		try{
		File.SetLastWriteTime (Application.persistentDataPath + Controller.slot + "/accountinfo", time);
		}catch{
			File.Copy (Application.persistentDataPath + Controller.slot + "/accountinfo",Application.persistentDataPath + Controller.slot + "/accountinfo1");
			File.Delete (Application.persistentDataPath + Controller.slot + "/accountinfo");
			File.Move (Application.persistentDataPath + Controller.slot + "/accountinfo1",Application.persistentDataPath + Controller.slot + "/accountinfo");
		}
		try{
		File.SetLastWriteTime (Application.persistentDataPath + Controller.slot + "/npcinfo", time);
		}catch{
			File.Copy (Application.persistentDataPath + Controller.slot + "/npcinfo",Application.persistentDataPath + Controller.slot + "/npcinfo1");
			File.Delete (Application.persistentDataPath + Controller.slot + "/npcinfo");
			File.Move (Application.persistentDataPath + Controller.slot + "/npcinfo1",Application.persistentDataPath + Controller.slot + "/npcinfo");
		}
		try{
		File.SetLastWriteTime (Application.persistentDataPath + Controller.slot + "/petinfo", time);
		}catch{
			File.Copy (Application.persistentDataPath + Controller.slot + "/petinfo",Application.persistentDataPath + Controller.slot + "/petinfo1");
			File.Delete (Application.persistentDataPath + Controller.slot + "/petinfo");
			File.Move (Application.persistentDataPath + Controller.slot + "/petinfo1",Application.persistentDataPath + Controller.slot + "/petinfo");
		}
		try{
			File.SetLastWriteTime (Application.persistentDataPath + Controller.slot + "/dailyinfo", time);
		}catch{
			File.Copy (Application.persistentDataPath + Controller.slot + "/dailyinfo",Application.persistentDataPath + Controller.slot + "/dailyinfo1");
			File.Delete (Application.persistentDataPath + Controller.slot + "/dailyinfo");
			File.Move (Application.persistentDataPath + Controller.slot + "/dailyinfo1",Application.persistentDataPath + Controller.slot + "/dailyinfo");
		}
		try{
		File.SetLastWriteTime (Application.persistentDataPath + Controller.slot + "/questinfo", time);
		}catch{
			File.Copy (Application.persistentDataPath + Controller.slot + "/questinfo",Application.persistentDataPath + Controller.slot + "/questinfo1");
			File.Delete (Application.persistentDataPath + Controller.slot + "/questinfo");
			File.Move (Application.persistentDataPath + Controller.slot + "/questinfo1",Application.persistentDataPath + Controller.slot + "/questinfo");
		}
		try{
		File.SetLastWriteTime (Application.persistentDataPath + Controller.slot + "/stats", time);
		}catch{
			File.Copy (Application.persistentDataPath + Controller.slot + "/stats",Application.persistentDataPath + Controller.slot + "/stats1");
			File.Delete (Application.persistentDataPath + Controller.slot + "/stats");
			File.Move (Application.persistentDataPath + Controller.slot + "/stats1",Application.persistentDataPath + Controller.slot + "/stats");
		}
	}
}
