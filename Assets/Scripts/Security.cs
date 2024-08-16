using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System;

public class Security : MonoBehaviour {
	
	
	public string inventoryPath;
	public string skillPath;
	public string accountPath;
	public string questInfoPath;
	public string npcInfoPath;
	public string petInfoPath;
	public string dailyInfoPath;
	public string badPath;
	public static bool validFiles;
	public long nextSecure;

	// Use this for initialization
	void Start () {
		if(Controller.slot != "/toybox"){
			inventoryPath = Application.persistentDataPath + Controller.slot + "/backpack";
			skillPath = Application.persistentDataPath + Controller.slot + "/stats";
			accountPath = Application.persistentDataPath + Controller.slot + "/accountinfo";
			questInfoPath = Application.persistentDataPath + Controller.slot + "/questinfo";
			npcInfoPath = Application.persistentDataPath + Controller.slot + "/npcinfo";
			petInfoPath = Application.persistentDataPath + Controller.slot + "/petinfo";
			dailyInfoPath = Application.persistentDataPath + Controller.slot + "/dailyinfo";
			badPath = Application.persistentDataPath + Controller.slot + "/FailedFiles/";
			if (false/*!VerifyIntegrity()*/) {
				if(Directory.Exists(badPath)){
					File.Delete (badPath+"backpack");
					File.Delete (badPath+"stats");
					File.Delete (badPath+"accountinfo");
					File.Delete (badPath+"npcinfo");
					File.Delete (badPath+"questinfo");
					File.Delete (badPath+"petinfo");
					File.Delete (badPath+"dailyinfo");
					File.Delete (badPath+"README.txt");
					Directory.Delete(badPath);
				}
				Directory.CreateDirectory (badPath);
				File.Move (inventoryPath,badPath+"backpack");
				File.Move (skillPath,badPath+"stats");
				File.Move (accountPath,badPath+"accountinfo");
				File.Move (questInfoPath,badPath+"questinfo");
				File.Move (npcInfoPath,badPath+"npcinfo");
				File.Move (petInfoPath,badPath+"petinfo");
				File.Move (dailyInfoPath,badPath+"dailyinfo");
				var read = File.CreateText (badPath+"README.txt");
				read.WriteLine ("These files failed to verify their integrity.");
				read.WriteLine ("");
				read.WriteLine ("If you believe this to be an error please send all files in this folder to (serialhugger13@gmail.com).");
				read.Close ();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
	public bool VerifyIntegrity(){
		DateTime inventoryWrite = File.GetLastWriteTime (inventoryPath);
		DateTime skillWrite = File.GetLastWriteTime (skillPath);
		DateTime accountWrite = File.GetLastWriteTime (accountPath);
		DateTime questWrite = File.GetLastWriteTime (questInfoPath);
		DateTime npcWrite = File.GetLastWriteTime (npcInfoPath);
		DateTime petWrite = File.GetLastWriteTime (petInfoPath);
		DateTime dailyWrite = File.GetLastWriteTime (dailyInfoPath);
		long highTick = inventoryWrite.Ticks;
		long lowTick = inventoryWrite.Ticks;
		if(skillWrite.Ticks > highTick){
			highTick = skillWrite.Ticks;
		}
		if(skillWrite.Ticks < lowTick){
			lowTick = skillWrite.Ticks;
		}
		if(accountWrite.Ticks > highTick){
			highTick = accountWrite.Ticks;
		}
		if(accountWrite.Ticks < lowTick){
			lowTick = accountWrite.Ticks;
		}
		if(questWrite.Ticks > highTick){
			highTick = questWrite.Ticks;
		}
		if(questWrite.Ticks < lowTick){
			lowTick = questWrite.Ticks;
		}
		if(npcWrite.Ticks > highTick){
			highTick = npcWrite.Ticks;
		}
		if(npcWrite.Ticks < lowTick){
			lowTick = npcWrite.Ticks;
		}
		if(petWrite.Ticks > highTick){
			highTick = petWrite.Ticks;
		}
		if(petWrite.Ticks < lowTick){
			lowTick = petWrite.Ticks;
		}
		if(dailyWrite.Ticks > highTick){
			highTick = dailyWrite.Ticks;
		}
		if(dailyWrite.Ticks < lowTick){
			lowTick = dailyWrite.Ticks;
		}
		if ((highTick-lowTick)<=System.TimeSpan.TicksPerSecond) {
			return true;
		} else {
			return false;
		}

	}
	public static string Rot39(string input,int mixer)
	{
		// This string contains 78 different characters in random order.
		var mix = "REDACTED";
		char[] mixArray = mix.ToCharArray ();
		UnityEngine.Random.InitState (mixer);
		mixArray = reshuffle (mixArray,mixer);
		mix = charArrayToString (mixArray);
		var result = (input ?? "").ToCharArray();
		for (int i = 0; i < result.Length; ++i)
		{
			int j = mix.IndexOf(result[i]);
			result[i] = (j < 0) ? result[i] : mix[(j + 40) % 80];
		}
		return new string(result);
	}
	public static string Rot39Static(string input)
	{
		// This string contains 78 different characters in random order.
		var mix = "REDACTED";
		var result = (input ?? "").ToCharArray();
		for (int i = 0; i < result.Length; ++i)
		{
			int j = mix.IndexOf(result[i]);
			result[i] = (j < 0) ? result[i] : mix[(j + 40) % 80];
		}
		return new string(result);
	}
	public static char[] reshuffle(char[] texts,int mixer)
	{
		// Knuth shuffle algorithm :: courtesy of Wikipedia :)
		for (int t = 0; t < texts.Length; t++ )
		{
			char tmp = texts[t];
			int r = UnityEngine.Random.Range(t, texts.Length);
			texts[t] = texts[r];
			texts[r] = tmp;
		}
		return texts;
	}
	public static string charArrayToString(char[] array){
		string tempString = "";
		for(int i = 0;i<array.Length;i++){
			tempString += array [i].ToString ();
		}
		return tempString;
	}
}
