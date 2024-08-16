using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;
using System.Linq;

public class MenuCameraScript : MonoBehaviour {

	public string inventoryPath;
	public string skillPath;
	public string accountPath;
	public string questInfoPath;
	public string npcInfoPath;
	public string petInfoPath;
	public string customInfoPath;
	bool needToRelease = true;
	public GameObject pressStart;
	public GameObject slotMenu;
	public GameObject mainMenu;
	public GameObject shopMenu;
	public GameObject graphicMenu;
	public GameObject gameModeMenu;
	public GameObject toyBoxMenu;
	public GameObject deleteSlot1;
	public GameObject deleteSlot2;
	public GameObject deleteSlot3;
	public GameObject deleteSlotToybox;
	public GameObject policy;
	public Purchaser purchaseScript;
	public Texture2D loadingText;
	public TouchScreenKeyboard keyboardSeed;
	public TextMesh versionDisplay;
	public bool loadGame;
	public float timeTillGameLoad = 5.0f;

	// Use this for initialization
	void Start () {
		versionDisplay.text = "V"+Application.version;
		purchaseScript = GameObject.Find ("_Controller").GetComponent<Purchaser>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Controller.lastPolicyAccept < Controller.currentPolicyNum){
			pressStart.SetActive (false);
			policy.SetActive (true);
		}
		if(!loadGame){
		if(Input.touchCount==0){
			needToRelease = false;
		}
		//TOUCHES
		for (int i = 0; i < Input.touchCount; i++) {
			Vector3 test = Camera.main.ScreenToWorldPoint (Input.GetTouch(i).position);
			RaycastHit hit; 
			Physics.Raycast (test, Vector3.forward,out hit);
			if(hit.collider!=null){
				MenuButton buttonScript = hit.transform.GetComponent<MenuButton> ();
				if(!needToRelease){
					needToRelease = true;
					if(buttonScript.button == "buyextraslots"){
						print ("should buy");
						purchaseScript.BuyExtraSaves ();
						return;
					}
					if(buttonScript.button == "buytoybox"){
						print ("should buy");
						purchaseScript.BuyToyBox ();
						return;
					}
					if(buttonScript.button == "start"){
						pressStart.SetActive (false);
						shopMenu.SetActive (false);
						mainMenu.SetActive (true);
						gameModeMenu.SetActive (false);
						toyBoxMenu.SetActive (false);
						graphicMenu.SetActive (false);
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
						return;
					}
					if(buttonScript.button == "gamemode"){
						pressStart.SetActive (false);
						shopMenu.SetActive (false);
						mainMenu.SetActive (false);
						slotMenu.SetActive (false);
						gameModeMenu.SetActive (true);
						toyBoxMenu.SetActive (false);
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
						return;
					}
					if(buttonScript.button == "delete1ask"){
						slotMenu.SetActive (false);
						deleteSlot1.SetActive (true);
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
					}
					if(buttonScript.button == "delete2ask"){
						slotMenu.SetActive (false);
						deleteSlot2.SetActive (true);
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
					}
					if(buttonScript.button == "delete3ask"){
						slotMenu.SetActive (false);
						deleteSlot3.SetActive (true);
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
					}
					if(buttonScript.button == "deletetask"){
						toyBoxMenu.SetActive (false);
						deleteSlotToybox.SetActive (true);
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
					}
					if(buttonScript.button == "shop"){
						mainMenu.SetActive (false);
						shopMenu.SetActive (true);
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
						return;
					}
					if(buttonScript.button == "accept"){
							Controller.lastPolicyAccept = Controller.currentPolicyNum;
							Controller.saveInfo ();
							pressStart.SetActive (true);
							policy.SetActive (false);
							Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
							return;
					}
					if(buttonScript.button == "graphic"){
						mainMenu.SetActive (false);
						graphicMenu.SetActive (true);
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
						return;
					}
					if(buttonScript.button == "saves"){
						Controller.mainSeed = 15487;
						Controller.joyStick = false;
						mainMenu.SetActive (false);
						slotMenu.SetActive (true);
						gameModeMenu.SetActive (false);
						toyBoxMenu.SetActive (false);
						deleteSlot1.SetActive (false);
						deleteSlot2.SetActive (false);
						deleteSlot3.SetActive (false);
						deleteSlotToybox.SetActive (false);
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
						return;
					}
					if(buttonScript.button == "slot1"){
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
						Controller.slot = "/slot1";
						loadGame = true;
						return;
					}
					if(buttonScript.button == "slot2"){
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
						Controller.slot = "/slot2";
						loadGame = true;
						return;
					}
					if(buttonScript.button == "slot3"){
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
						Controller.slot = "/slot3";
						loadGame = true;
						return;
					}
					if(buttonScript.button == "privacypolicy"){
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
							Application.OpenURL("https://app.termly.io/document/privacy-policy/5d8b2cdd-0864-4956-bb1e-5f713263e1a6");
						return;
					}
					if(buttonScript.button == "tutorial"){
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
						SceneManager.LoadScene ("miniguide");
						return;
					}
					if(buttonScript.button == "discord"){
						Application.OpenURL("https://discord.gg/fDGwXyR");
						return;
					}
					if(buttonScript.button == "website"){
						Application.OpenURL("http://gpssurvivalgame.bitballoon.com/");
						return;
					}
					if(buttonScript.button == "toyboxsetup"){
						Controller.slot = "/toybox";
						Controller.mainSeed = 25565;
						loadStats ();
						gameModeMenu.SetActive (false);
						toyBoxMenu.SetActive (true);
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
						return;
					}
					if(buttonScript.button == "toybox"){
						saveStats ();
						gameModeMenu.SetActive (false);
						toyBoxMenu.SetActive (true);
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
						loadGame = true;
						return;
					}
					if (buttonScript.button == "delete1") {
						inventoryPath = Application.persistentDataPath + "/slot1" + "/backpack";
						skillPath = Application.persistentDataPath + "/slot1" + "/stats";
						accountPath = Application.persistentDataPath + "/slot1" + "/accountinfo";
						questInfoPath = Application.persistentDataPath + "/slot1" + "/questinfo";
						npcInfoPath = Application.persistentDataPath + "/slot1" + "/npcinfo";
						petInfoPath = Application.persistentDataPath + "/slot1" + "/petinfo";
						File.Delete (inventoryPath);
						File.CreateText (inventoryPath);
						File.Delete (skillPath);
						File.CreateText (skillPath);
						File.Delete (accountPath);
						File.CreateText (accountPath);
						File.Delete (questInfoPath);
						File.CreateText (questInfoPath);
						File.Delete (npcInfoPath);
						File.CreateText (npcInfoPath);
						File.Delete (petInfoPath);
						File.CreateText (petInfoPath);
						if(Directory.Exists(Application.persistentDataPath + "/slot1" + "/chunks")){
							Directory.Delete(Application.persistentDataPath + "/slot1" + "/chunks",true);
						}
						deleteSlot1.SetActive (false);
						mainMenu.SetActive (true);
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
					}
					if (buttonScript.button == "delete2") {
						inventoryPath = Application.persistentDataPath + "/slot2" + "/backpack";
						skillPath = Application.persistentDataPath + "/slot2" + "/stats";
						accountPath = Application.persistentDataPath + "/slot2" + "/accountinfo";
						questInfoPath = Application.persistentDataPath + "/slot2" + "/questinfo";
						npcInfoPath = Application.persistentDataPath + "/slot2" + "/npcinfo";
						petInfoPath = Application.persistentDataPath + "/slot2" + "/petinfo";
						File.Delete (inventoryPath);
						File.CreateText (inventoryPath);
						File.Delete (skillPath);
						File.CreateText (skillPath);
						File.Delete (accountPath);
						File.CreateText (accountPath);
						File.Delete (questInfoPath);
						File.CreateText (questInfoPath);
						File.Delete (npcInfoPath);
						File.CreateText (npcInfoPath);
						File.Delete (petInfoPath);
						File.CreateText (petInfoPath);
						if(Directory.Exists(Application.persistentDataPath + "/slot2" + "/chunks")){
							Directory.Delete(Application.persistentDataPath + "/slot2" + "/chunks",true);
						}
						deleteSlot2.SetActive (false);
						mainMenu.SetActive (true);
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
					}
					if (buttonScript.button == "delete3") {
						inventoryPath = Application.persistentDataPath + "/slot3" + "/backpack";
						skillPath = Application.persistentDataPath + "/slot3" + "/stats";
						accountPath = Application.persistentDataPath + "/slot3" + "/accountinfo";
						questInfoPath = Application.persistentDataPath + "/slot3" + "/questinfo";
						npcInfoPath = Application.persistentDataPath + "/slot3" + "/npcinfo";
						petInfoPath = Application.persistentDataPath + "/slot3" + "/petinfo";
						File.Delete (inventoryPath);
						File.CreateText (inventoryPath);
						File.Delete (skillPath);
						File.CreateText (skillPath);
						File.Delete (accountPath);
						File.CreateText (accountPath);
						File.Delete (questInfoPath);
						File.CreateText (questInfoPath);
						File.Delete (npcInfoPath);
						File.CreateText (npcInfoPath);
						File.Delete (petInfoPath);
						File.CreateText (petInfoPath);
						if(Directory.Exists(Application.persistentDataPath + "/slot3" + "/chunks")){
							Directory.Delete(Application.persistentDataPath + "/slot3" + "/chunks",true);
						}
						deleteSlot3.SetActive (false);
						mainMenu.SetActive (true);
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
					}
					if (buttonScript.button == "deletet") {
						inventoryPath = Application.persistentDataPath + "/toybox" + "/backpack";
						skillPath = Application.persistentDataPath + "/toybox" + "/stats";
						accountPath = Application.persistentDataPath + "/toybox" + "/accountinfo";
						questInfoPath = Application.persistentDataPath + "/toybox" + "/questinfo";
						npcInfoPath = Application.persistentDataPath + "/toybox" + "/npcinfo";
						petInfoPath = Application.persistentDataPath + "/toybox" + "/petinfo";
						File.Delete (inventoryPath);
						File.CreateText (inventoryPath);
						File.Delete (skillPath);
						File.CreateText (skillPath);
						File.Delete (accountPath);
						File.CreateText (accountPath);
						File.Delete (questInfoPath);
						File.CreateText (questInfoPath);
						File.Delete (npcInfoPath);
						File.CreateText (npcInfoPath);
						File.Delete (petInfoPath);
						File.CreateText (petInfoPath);
						File.Delete (Application.persistentDataPath + "/toybox" + "/custominfo");
						File.CreateText (Application.persistentDataPath + "/toybox" + "/custominfo");
						if(Directory.Exists(Application.persistentDataPath + "/toybox" + "/chunks")){
							Directory.Delete(Application.persistentDataPath + "/toybox" + "/chunks",true);
						}
						deleteSlotToybox.SetActive (false);
						mainMenu.SetActive (true);
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
					}
					if(buttonScript.button == "seed"){
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
						keyboardSeed = TouchScreenKeyboard.Open (Controller.mainSeed.ToString(),TouchScreenKeyboardType.NumberPad,false,false,false);
						return;
					}
					if(buttonScript.button == "joystick"){
						Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
						Controller.joyStick = !Controller.joyStick;
						return;
					}
						if(buttonScript.button == "chunkborders"){
							Instantiate (Resources.Load ("Effects/ButtonSound"), new Vector3 (hit.transform.position.x + .05f, hit.transform.position.y - .05f, 0), Quaternion.identity);
							Controller.disableBorderChunks = !Controller.disableBorderChunks;
							Controller.saveInfo ();
							return;
						}
				}
			}
		}
		if(keyboardSeed!=null){
		if (keyboardSeed.text.Length >= 5) {
			Controller.mainSeed = int.Parse (keyboardSeed.text.Substring (0, 5));
		} else if (keyboardSeed.text.Length != 0) {
			Controller.mainSeed = int.Parse (keyboardSeed.text);
		} else {
			Controller.mainSeed = 25565;
		}
		}
		}
	}
	void OnGUI(){
		if(timeTillGameLoad < 0){
			SceneManager.LoadScene ("land");
		}
		if(loadGame){
			DrawQuad (new Rect(0,0,Screen.width,Screen.height),Color.black);
			GUI.DrawTexture (new Rect((Screen.width/2)-21*5,Screen.height/2,42*5,12*5),loadingText);
			timeTillGameLoad -= 0.1f;
		}
	}
	void DrawQuad(Rect position,Color color){
		Texture2D texture = new Texture2D (1,1);
		texture.SetPixel (0,0,color);
		texture.Apply ();
		GUI.skin.box.normal.background = texture;
		GUI.Box (position,GUIContent.none);
	}
	public static void saveStats(){
		File.Delete (Application.persistentDataPath + "/toybox" + "/custominfo");
		var file = File.CreateText (Application.persistentDataPath + "/toybox" + "/custominfo");
		file.WriteLine (Controller.mainSeed);
		file.WriteLine (Controller.joyStick);
		file.WriteLine (Controller.joyLat);
		file.WriteLine (Controller.joyLon);
		file.Close ();
	}
	public static void loadStats(){
		string line;
		int index = 0;
		StreamReader theReader = new StreamReader(Application.persistentDataPath + "/toybox" + "/custominfo", Encoding.Default);
		using(theReader){
			do{
				line = theReader.ReadLine();
				if(line != null){
					if(index == 0){
						Controller.mainSeed = int.Parse(line);
					}
					if(index == 1){
						Controller.joyStick = bool.Parse(line);
					}
					if(index == 2){
						Controller.joyLat = float.Parse(line);
					}
					if(index == 3){
						Controller.joyLon = float.Parse(line);
					}
				}
				index += 1;
			}while (line != null);
			theReader.Close ();
		}
	}
}
