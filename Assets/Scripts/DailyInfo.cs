using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class DailyInfo : MonoBehaviour {

	public static string dailyInfoPath = "";
	public static List<string> dailyActions = new List<string>();
	public Inventory inventoryScript;

	// Use this for initialization
	void Start () {
		dailyInfoPath = (Application.persistentDataPath + Controller.slot + "/dailyinfo");
		if (File.Exists (dailyInfoPath)) {
			loadInfo ();
		}
		if(dailyActions.Count==0){
			dailyActions.Add(System.DateTime.Now.Ticks.ToString());
			saveInfo ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if((int)(long.Parse(dailyActions[0])/System.TimeSpan.TicksPerDay)!=(int)(System.DateTime.Now.Ticks/System.TimeSpan.TicksPerDay)){
			newDayReset ();
		}
	}
	public void newDayReset(){
		if(CameraController.camera == 1){
			CameraController.camera = 0;
			GameObject[] itemButtons = GameObject.FindGameObjectsWithTag ("BuyItemButton");
			for (int i2 = 0; i2 < itemButtons.Length; i2++) {
				Destroy (itemButtons [i2]);
			}
		}
		dailyActions.Clear ();
		dailyActions.Add(System.DateTime.Now.Ticks.ToString ());
		saveInfo ();
	}
	public static string grabData(string header,int indexFind, int split){
		int index = 0;
		for(int i = 0; i < dailyActions.Count;i++){
			if (dailyActions [i].Split ('=')[0]==header) {
				if(index == indexFind){
					return dailyActions [i].Split ('=')[1].Split(':')[split];
				}
				index ++;
			}
		}
		return "END";
	}
	public static void saveInfo(){
		File.Delete (dailyInfoPath);
		var file = File.CreateText (dailyInfoPath);
		for(int i = 0; i < dailyActions.Count; i++){
			file.WriteLine (Security.Rot39(dailyActions[i],154+i));
		}
		file.Close();
	}
	public static void loadInfo(){
		dailyActions.Clear ();
		string line;
		int index = 0;
		StreamReader theReader = new StreamReader (dailyInfoPath, Encoding.Default);
		using (theReader) {
			do {
				line = theReader.ReadLine ();
				if (line != null) {
					dailyActions.Add (Security.Rot39 (line, 154 + index));
				}
				index += 1;
			} while (line != null);
			theReader.Close ();
		}
	}
	public static void addData(string header,string data){
		dailyActions.Add (header+"="+data);
		saveInfo ();
	}
}
