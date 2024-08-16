using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class NPCInfo : MonoBehaviour {

	public static string npcInfoPath = "";

	// Use this for initialization
	void Start() {
		npcInfoPath = (Application.persistentDataPath + Controller.slot + "/npcinfo");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public static bool HasQuestOfNPC(string npcId){
		string line = null;
		string npcID = "";
		StreamReader theReader = new StreamReader(npcInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),682236)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "npc") {
						if (attribute [1] == npcId) {
							theReader.Close ();
							return true;
						};
					}
				}
			}
		}
		theReader.Close ();
		return false;
	}
	public static int GetHighestCompleteOfType(string type){
		string line = null;
		string npcID = "";
		bool rightType = false;
		int highestCompleted = 0;
		int completed = 0;
		StreamReader theReader = new StreamReader(npcInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),682236)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "completed") {
						completed = int.Parse (attribute[1]);
					}
					if (attribute [0] == "type") {
						if (attribute [1] == type) {
							rightType = true;
						};
					}
				}
				if(rightType && completed > highestCompleted){
					highestCompleted = completed;
				}
				rightType = false;
				completed = 0;
			}
		}
		theReader.Close ();
		return highestCompleted;
	}
	public static void AddNpc(string npcID, string npcName,int npcSprite, string type){
		StreamWriter file = new StreamWriter (npcInfoPath,true);
		file.WriteLine (Security.Rot39("npc:"+npcID+";name:"+npcName+";npcimage:" + npcSprite + ";type:" + type + ";completed:0",682236));
		file.Close ();
	}
	public static void AddNpcCompletion(string npcID){
		string line = null;
		bool npc = false;
		int oldComplete = 0;
		List<string> newText = new List<string>();
		StreamReader theReader = new StreamReader(npcInfoPath, Encoding.Default);
		using (theReader) {
			while ((line = Security.Rot39(theReader.ReadLine (),682236)).Contains(":")) {
				var attributeGroups = line.Split (new char[]{ ';' });
				for (int i = 0; i < attributeGroups.Length; i++) {
					var attribute = attributeGroups [i].Split (new char[]{ ':' });
					if (attribute [0] == "npc") {
						if (attribute[1] == npcID) {
							npc = true;
						} 
					}
					if(attribute[0] == "completed"){
						oldComplete = int.Parse(attribute [1]);
					}
				}
				if (npc) {
					line = line.Replace ("completed:"+oldComplete, "completed:"+(oldComplete+1));
				}
				newText.Add(Security.Rot39(line,682236));
				npc = false;
				oldComplete = 0;
			}
		}
		theReader.Close ();
		StreamWriter theWriter = new StreamWriter(npcInfoPath);
		for(int i = 0;i < newText.Count; i++){
			theWriter.WriteLine (newText[i]);
		}
		theWriter.Close ();
	}
}
