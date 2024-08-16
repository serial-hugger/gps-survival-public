using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;
using System.Text;
using System.Linq;

public class ExtraButton : MonoBehaviour {

	public string buttonType;
	public Inventory inventoryScript;
	public SpriteRenderer buttonImage;
	public Sprite townTrue;
	public Sprite townFalse;
	public bool pressed;
	public float timeTillUnpress;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(buttonType == "town"){
			if (Controller.currentPlaceID != "none" && Controller.currentPlaceID != "nointernetplaceid") {
				buttonImage.sprite = townTrue;
			} else {
				buttonImage.sprite = townFalse;
			}
		}
	}
	public void ButtonPress(){
		pressed = true;
		if (timeTillUnpress > 0) {
			timeTillUnpress -= 1.0f * Time.deltaTime;
		} else {
			pressed = false;
		}
		if(buttonType != "soundoption" && buttonType != "internetoption"){
			inventoryScript.closeWindows ();
		}
		if(buttonType == "pet"){
			inventoryScript.petScreen.SetActive (true);
			inventoryScript.window = true;
			inventoryScript.needRelease = true;
			CameraLocation.needToRelease = true;
		}
		if(buttonType == "dailyshop"){
			inventoryScript.dailyShopScreen.SetActive (true);
			inventoryScript.window = true;
			inventoryScript.needRelease = true;
			CameraLocation.needToRelease = true;
		}
		if(buttonType == "coinshop"){
			inventoryScript.coinShopScreen.SetActive (true);
			inventoryScript.window = true;
			inventoryScript.needRelease = true;
			CameraLocation.needToRelease = true;
		}
		if(buttonType == "achievements"){
			Achievements.ShowAchievementsUI ();
			inventoryScript.closeWindows ();
			inventoryScript.needRelease = false;
			CameraLocation.needToRelease = false;
		}
		if(buttonType == "leaderboards"){
			Achievements.ShowLeaderboardUI ();
			inventoryScript.closeWindows ();
			inventoryScript.needRelease = false;
			CameraLocation.needToRelease = false;
		}
		if(buttonType == "gift"){
			inventoryScript.giftScreen.SetActive (true);
			inventoryScript.window = true;
			inventoryScript.needRelease = true;
			CameraLocation.needToRelease = true;
		}
		if(buttonType == "charactercustomize"){
			inventoryScript.customizeScreen.SetActive (true);
			inventoryScript.window = true;
			inventoryScript.needRelease = true;
			CameraLocation.needToRelease = true;
		}
		if(buttonType == "map"){
			inventoryScript.mapScreen.SetActive (true);
			inventoryScript.window = true;
			inventoryScript.needRelease = true;
			CameraLocation.needToRelease = true;
		}
		if(buttonType == "soundoption"){
			inventoryScript.needRelease = true;
			CameraLocation.needToRelease = true;
			AccountInfo.soundOption = !AccountInfo.soundOption;
			AccountInfo.saveInfo ();
		}
		if(buttonType == "internetoption"){
			inventoryScript.needRelease = true;
			CameraLocation.needToRelease = true;
			AccountInfo.internetOption = !AccountInfo.internetOption;
			AccountInfo.saveInfo ();
		}
		if(buttonType == "exitgame"){
			Application.Quit ();
		}
		if (buttonType == "restore") {
			if (pressed) {
				if (Controller.slot == "/slot1") {
					if (File.Exists (Application.persistentDataPath + "/backups/backup1/accountinfo")) {
						Directory.Delete (Application.persistentDataPath + "/slot1");
						Directory.Move (Application.persistentDataPath + "/backups/backup1", Application.persistentDataPath + "/slot1");
						SceneManager.LoadScene ("land");
					} else {
						GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0, 0, -20), Quaternion.identity);
						TextMesh errorText = error.GetComponent<TextMesh> ();
						errorText.text = "NO BACKUP FOR\nTHIS SLOT";
					}
				}
				if (Controller.slot == "/slot2") {
					if (File.Exists (Application.persistentDataPath + "/backups/backup2/accountinfo")) {
						Directory.Delete (Application.persistentDataPath + "/slot2");
						Directory.Move (Application.persistentDataPath + "/backups/backup2", Application.persistentDataPath + "/slot2");
						SceneManager.LoadScene ("land");
					} else {
						GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0, 0, -20), Quaternion.identity);
						TextMesh errorText = error.GetComponent<TextMesh> ();
						errorText.text = "NO BACKUP FOR\nTHIS SLOT";
					}
				}
				if (Controller.slot == "/slot3") {
					if (File.Exists (Application.persistentDataPath + "/backups/backup3/accountinfo")) {
						Directory.Delete (Application.persistentDataPath + "/slot3");
						Directory.Move (Application.persistentDataPath + "/backups/backup3", Application.persistentDataPath + "/slot3");
						SceneManager.LoadScene ("land");
					} else {
						GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0, 0, -20), Quaternion.identity);
						TextMesh errorText = error.GetComponent<TextMesh> ();
						errorText.text = "NO BACKUP FOR\nTHIS SLOT";
					}
				}
				if (Controller.slot == "/toybox") {
					Directory.Delete(Application.persistentDataPath + "/toybox");
					Directory.Move (Application.persistentDataPath + "/backups/backupt", Application.persistentDataPath + "/toybox");
				}
			} else {
				timeTillUnpress = 5.0f;
				pressed = true;
				GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0, 0, -20), Quaternion.identity);
				TextMesh errorText = error.GetComponent<TextMesh> ();
				errorText.text = "PRESS AGAIN\nIF YOU'RE SURE\nYOU WANT TO\nOVERWRITE THE\nCURRENT SLOT";
			}
		}
		if (buttonType == "backup") {
			if(Controller.slot == "/slot1"){
				Directory.Delete(Application.persistentDataPath + "/backups/backup1");
				Directory.Move (Application.persistentDataPath + "/slot1",Application.persistentDataPath + "/backups/backup1");
			}
			if(Controller.slot == "/slot2"){
				Directory.Delete(Application.persistentDataPath + "/backups/backup2");
				Directory.Move (Application.persistentDataPath + "/slot2",Application.persistentDataPath + "/backups/backup2");
			}
			if(Controller.slot == "/slot3"){
				Directory.Delete(Application.persistentDataPath + "/backups/backup3");
				Directory.Move(Application.persistentDataPath + "/slot3",Application.persistentDataPath + "/backups/backup3");
			}
			if(Controller.slot == "/toybox"){
				Directory.Delete(Application.persistentDataPath + "/backups/backupt");
				Directory.Move (Application.persistentDataPath + "/toybox",Application.persistentDataPath + "/backups/backupt");
			}
			GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0, 0, -20), Quaternion.identity);
			TextMesh errorText = error.GetComponent<TextMesh> ();
			errorText.text = "GAME HAS BEEN\nBACKED UP";
		}
		if (buttonType == "town") {
			if (Controller.currentPlaceID != "none" && Controller.currentPlaceID != "nointernetplaceid" && Controller.currentPlaceID != "null") {
				inventoryScript.needRelease = true;
				CameraLocation.needToRelease = true;
				inventoryScript.closeWindows ();
				inventoryScript.window = true;
				inventoryScript.placeScreen.SetActive (true);
			} else {
				GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0, 0, -20), Quaternion.identity);
				TextMesh errorText = error.GetComponent<TextMesh> ();
				errorText.text = "YOU ARE NOT\nIN A CITY,\nTOWN, OR\nVILLAGE";
			}
		}
	}
}
