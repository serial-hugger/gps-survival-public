using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Linq;

public class DeviceInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(File.Exists(Application.persistentDataPath + "/deviceinfo")){
			loadInfo ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public static void saveInfo(){
		File.Delete (Application.persistentDataPath + "/deviceinfo");
		var file = File.CreateText (Application.persistentDataPath + "/deviceinfo");
		if (Controller.extraSavesPurchased) {
			file.WriteLine (Rot39 (SystemInfo.deviceUniqueIdentifier +"extrasaves:true"));
		} else {
			file.WriteLine (Rot39 (SystemInfo.deviceUniqueIdentifier + "extrasaves:false"));
		}
		if (Controller.toyBoxPurchased) {
			file.WriteLine (Rot39 (SystemInfo.deviceUniqueIdentifier +"toybox:true"));
		} else {
			file.WriteLine (Rot39 (SystemInfo.deviceUniqueIdentifier + "toybox:false"));
		}
		file.Close ();
	}
	public static void loadInfo(){
		string line;
		int index = 0;
		StreamReader theReader = new StreamReader(Application.persistentDataPath + "/deviceinfo", Encoding.Default);
		using(theReader){
			do{
				line = theReader.ReadLine();
				if(line != null){
					if(index == 0){
						if(Rot39(line) == (SystemInfo.deviceUniqueIdentifier + "extrasaves:true")){
							Controller.extraSavesPurchased = true;
						}else{
							Controller.extraSavesPurchased = false;
						}
					}
					if(index == 1){
						if(Rot39(line) == (SystemInfo.deviceUniqueIdentifier + "toybox:true")){
							Controller.toyBoxPurchased = true;
						}else{
							Controller.toyBoxPurchased = false;
						}
					}
				}
				index += 1;
			}while (line != null);
			theReader.Close ();
		}
	}
	public static string Rot39(string input)
	{
		// This string contains 78 different characters in random order.
		var mix = "QDXkW<_(V?cqK.lJ>-*y&zv9prf8biYCFeMxBm6ZnG3H4OuS1UaI5TwtoA#Rs!,7d2@L^gNhj)EP$0";
		var result = (input ?? "").ToCharArray();
		for (int i = 0; i < result.Length; ++i)
		{
			int j = mix.IndexOf(result[i]);
			result[i] = (j < 0) ? result[i] : mix[(j + 39) % 78];
		}
		return new string(result);
	}
}
