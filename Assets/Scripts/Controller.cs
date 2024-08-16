using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Text;

public class Controller : MonoBehaviour {

	long timeToSpawn;

	public int tempBundleCode;
	public static int bundleCode;

	public static int currentPolicyNum = 1;

	public static string globalOptionsPath = "";

	public string inventoryPath;
	public string skillPath;
	public string accountPath;
	public string questInfoPath;
	public string npcInfoPath;
	public string petInfoPath;
	public string dailyInfoPath;
	public string customInfoPath;
	public static string slot = "/slot1";
	public static bool extraSavesPurchased = false;
	public static bool toyBoxPurchased = false;
	public static bool joyStick;
	public static float joyLat = 45.0005f;
	public static float joyLon = 90.0005f;
	public static int mainSeed = 15487;
	public bool test;

	public static bool disableBorderChunks;
	public static int lastPolicyAccept;

	public static string currentPet = "";
	public static int currentPetCostume = 0;
	public static string currentPlaceName = "";
	public static string currentPlaceID = "";
	public static string currentPlaceType = "";
	public static string currentPlaceWiki = "";

	public static string cityEntityID = "";
	public static string cityFishID = "";

	public static string lastWeaponUsed = "";

	void Awake(){
		DontDestroyOnLoad (gameObject);
		SceneManager.LoadScene ("Menu");
		bundleCode = tempBundleCode;
	}
	// Use this for initialization
	void Start () {
		globalOptionsPath = (Application.persistentDataPath + "/globaloptions");
		if (File.Exists (globalOptionsPath)) {
			loadInfo ();
		}
		saveInfo ();
		if(!File.Exists(Application.persistentDataPath + "/deviceinfo")){
			File.CreateText (Application.persistentDataPath + "/deviceinfo");
		}
		if(!Directory.Exists(Application.persistentDataPath + "/backups")){
			Directory.CreateDirectory (Application.persistentDataPath + "/backups");
		}
		if(!Directory.Exists(Application.persistentDataPath + "/backups/backup1")){
			Directory.CreateDirectory (Application.persistentDataPath + "/backups/backup1");
		}
		if(!Directory.Exists(Application.persistentDataPath + "/backups/backup2")){
			Directory.CreateDirectory (Application.persistentDataPath + "/backups/backup2");
		}
		if(!Directory.Exists(Application.persistentDataPath + "/backups/backup3")){
			Directory.CreateDirectory (Application.persistentDataPath + "/backups/backup3");
		}
		if(!Directory.Exists(Application.persistentDataPath + "/backups/backupt")){
			Directory.CreateDirectory (Application.persistentDataPath + "/backups/backupt");
		}
		if(!Directory.Exists(Application.persistentDataPath + "/slot1")){
			Directory.CreateDirectory (Application.persistentDataPath + "/slot1");
		}
		if(!Directory.Exists(Application.persistentDataPath + "/slot2")){
			Directory.CreateDirectory (Application.persistentDataPath + "/slot2");
		}
		if(!Directory.Exists(Application.persistentDataPath + "/slot3")){
			Directory.CreateDirectory (Application.persistentDataPath + "/slot3");
		}
		if(!Directory.Exists(Application.persistentDataPath + "/toybox")){
			Directory.CreateDirectory (Application.persistentDataPath + "/toybox");
		}
		inventoryPath = Application.persistentDataPath + "/slot1" + "/backpack";
		skillPath = Application.persistentDataPath + "/slot1" + "/stats";
		accountPath = Application.persistentDataPath + "/slot1" + "/accountinfo";
		questInfoPath = Application.persistentDataPath + "/slot1" + "/questinfo";
		npcInfoPath = Application.persistentDataPath + "/slot1" + "/npcinfo";
		petInfoPath = Application.persistentDataPath + "/slot1" + "/petinfo";
		dailyInfoPath = Application.persistentDataPath + "/slot1" + "/dailyinfo";
		if(!File.Exists(inventoryPath)){
			File.CreateText (inventoryPath);
		}
		if(!File.Exists(skillPath)){
			File.CreateText (skillPath);
		}
		if(!File.Exists(accountPath)){
			File.CreateText (accountPath);
		}
		if(!File.Exists(questInfoPath)){
			File.CreateText (questInfoPath);
		}
		if(!File.Exists(npcInfoPath)){
			File.CreateText (npcInfoPath);
		}
		if(!File.Exists(petInfoPath)){
			File.CreateText (petInfoPath);
		}
		if(!File.Exists(dailyInfoPath)){
			File.CreateText (dailyInfoPath);
		}
		inventoryPath = Application.persistentDataPath + "/slot2" + "/backpack";
		skillPath = Application.persistentDataPath + "/slot2" + "/stats";
		accountPath = Application.persistentDataPath + "/slot2" + "/accountinfo";
		questInfoPath = Application.persistentDataPath + "/slot2" + "/questinfo";
		npcInfoPath = Application.persistentDataPath + "/slot2" + "/npcinfo";
		petInfoPath = Application.persistentDataPath + "/slot2" + "/petinfo";
		dailyInfoPath = Application.persistentDataPath + "/slot2" + "/dailyinfo";
		if(!File.Exists(inventoryPath)){
			File.CreateText (inventoryPath);
		}
		if(!File.Exists(skillPath)){
			File.CreateText (skillPath);
		}
		if(!File.Exists(accountPath)){
			File.CreateText (accountPath);
		}
		if(!File.Exists(questInfoPath)){
			File.CreateText (questInfoPath);
		}
		if(!File.Exists(npcInfoPath)){
			File.CreateText (npcInfoPath);
		}
		if(!File.Exists(petInfoPath)){
			File.CreateText (petInfoPath);
		}
		if(!File.Exists(dailyInfoPath)){
			File.CreateText (dailyInfoPath);
		}
		inventoryPath = Application.persistentDataPath + "/slot3" + "/backpack";
		skillPath = Application.persistentDataPath + "/slot3" + "/stats";
		accountPath = Application.persistentDataPath + "/slot3" + "/accountinfo";
		questInfoPath = Application.persistentDataPath + "/slot3" + "/questinfo";
		npcInfoPath = Application.persistentDataPath + "/slot3" + "/npcinfo";
		petInfoPath = Application.persistentDataPath + "/slot3" + "/petinfo";
		dailyInfoPath = Application.persistentDataPath + "/slot3" + "/dailyinfo";
		if(!File.Exists(inventoryPath)){
			File.CreateText (inventoryPath);
		}
		if(!File.Exists(skillPath)){
			File.CreateText (skillPath);
		}
		if(!File.Exists(accountPath)){
			File.CreateText (accountPath);
		}
		if(!File.Exists(questInfoPath)){
			File.CreateText (questInfoPath);
		}
		if(!File.Exists(npcInfoPath)){
			File.CreateText (npcInfoPath);
		}
		if(!File.Exists(petInfoPath)){
			File.CreateText (petInfoPath);
		}
		if(!File.Exists(dailyInfoPath)){
			File.CreateText (dailyInfoPath);
		}
		inventoryPath = Application.persistentDataPath + "/toybox" + "/backpack";
		skillPath = Application.persistentDataPath + "/toybox" + "/stats";
		accountPath = Application.persistentDataPath + "/toybox" + "/accountinfo";
		questInfoPath = Application.persistentDataPath + "/toybox" + "/questinfo";
		npcInfoPath = Application.persistentDataPath + "/toybox" + "/npcinfo";
		petInfoPath = Application.persistentDataPath + "/toybox" + "/petinfo";
		customInfoPath = Application.persistentDataPath + "/toybox" + "/custominfo";
		dailyInfoPath = Application.persistentDataPath + "/toybox" + "/dailyinfo";
		if(!File.Exists(inventoryPath)){
			File.CreateText (inventoryPath);
		}
		if(!File.Exists(skillPath)){
			File.CreateText (skillPath);
		}
		if(!File.Exists(accountPath)){
			File.CreateText (accountPath);
		}
		if(!File.Exists(questInfoPath)){
			File.CreateText (questInfoPath);
		}
		if(!File.Exists(npcInfoPath)){
			File.CreateText (npcInfoPath);
		}
		if(!File.Exists(petInfoPath)){
			File.CreateText (petInfoPath);
		}
		if(!File.Exists(dailyInfoPath)){
			File.CreateText (dailyInfoPath);
		}
		if(!File.Exists(customInfoPath)){
			File.CreateText (customInfoPath);
		}
		//6000000000
		timeToSpawn = (System.DateTime.Now.Ticks/3000000000)+1;
	}
	
	// Update is called once per frame
	void Update () {
		print (currentPlaceID);
		if(test){
			currentPlaceID = "1394323";
		}
		if(SceneManager.GetActiveScene().name == "land"){
			if((System.DateTime.Now.Ticks/3000000000) >= timeToSpawn){
				timeToSpawn = (System.DateTime.Now.Ticks/3000000000)+1;
				Instantiate (Resources.Load ("Effects/BirdSound"), new Vector3 (0 + .05f, 0 - .05f, 0), Quaternion.identity);
				spawnEntity (138593,timeToSpawn);
				spawnEntity (654345,timeToSpawn);
				if(PetInfo.IsPetHappy("0000")){
					spawnEntity (395873,timeToSpawn);
					spawnEntity (999999,timeToSpawn);
				}
			}
		}
		if (SceneManager.GetActiveScene ().name == "privacypolicy" || SceneManager.GetActiveScene ().name == "miniguide") {
			Screen.orientation = ScreenOrientation.Portrait;
			for (int i = 0; i < Input.touchCount; i++) {
				if (Input.GetTouch (i).position.y > 25) {
					if (Input.GetTouch (i).phase == TouchPhase.Began) {
						SceneManager.LoadScene ("Menu");
					}
				}
			}
		} else {
			Screen.orientation = ScreenOrientation.AutoRotation;
		}
	}
	void spawnEntity(int randomizer, long timeToDespawn){
		bool spawnLeftRight;
		float outsideXSpawn;
		float outsideYSpawn;
		Random.InitState ((int)timeToSpawn + randomizer);
		string entityID = EntityCatalog.getEntityID (EntityCatalog.normalEntityIndexes [Random.Range (0, EntityCatalog.normalEntityIndexes.Length)]);
		if ((Controller.currentPlaceID != "nointernetid" && Controller.currentPlaceID != "null" && Controller.currentPlaceID != "none")) {
			Random.InitState ((int)long.Parse (Controller.currentPlaceID));
			entityID = EntityCatalog.getEntityID (EntityCatalog.rareEntityIndexes [Random.Range (0, EntityCatalog.rareEntityIndexes.Length)]);
			print (entityID);
		} else if (Random.Range (0, 5000) > 4500) {
			entityID = EntityCatalog.getEntityID (EntityCatalog.rareEntityIndexes [Random.Range (0, EntityCatalog.rareEntityIndexes.Length)]);
		}
		Random.InitState ((int)timeToSpawn + randomizer);
		//-2,4
		float xGoTo;
		float yGoTo;
		if (Random.Range (0, 1000)>500) {
			spawnLeftRight = true;
		} else {
			spawnLeftRight = false;
		}
		if (spawnLeftRight) {
			if (Random.Range (0, 10000) > 5000) {
				outsideXSpawn = -1;
			} else {
				outsideXSpawn = 3;
			}
			outsideYSpawn = Random.Range (-2.0f, 4.0f);
		} else {
			if (Random.Range (0, 10000) > 5000) {
				outsideYSpawn = -1;
			} else {
				outsideYSpawn = 3;
			}
			outsideXSpawn = Random.Range (-2.0f, 4.0f);
		}
		xGoTo = Random.Range (0.2f,1.8f);
		yGoTo = Mathf.Abs(Random.Range (0.2f,-1.8f));
		GameObject entity = (GameObject)Instantiate (Resources.Load ("GeneralEntity"), new Vector3 (outsideXSpawn, outsideYSpawn, 0), Quaternion.identity);
		GeneralEntity entityScript = entity.GetComponent<GeneralEntity> ();
		entityScript.gotoX = xGoTo;
		entityScript.gotoY = yGoTo;
		entityScript.entityID = entityID;
		entityScript.timeToDespawn = timeToDespawn;
	}
	public static bool isPortrait(){
		if(Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown){
			return true;
		}
		return false;
	}
	public static bool isLandscape(){
		if(Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight){
			return true;
		}
		return false;
	}
	public static int experienceMultiplier(){
		if(PlayerScript.spellUsed == "0128"){
			return 2;
		}
		return 1;
	}
	public static int damageMultiplier(){
		if(PlayerScript.spellUsed == "0126"){
			return 2;
		}
		return 1;
	}
	public static float getDistanceFromLatLonInKm(float lat1,float lon1,float lat2,float lon2){
		var R = 6371;
		var dLat = deg2rad (lat2-lat1);
		var dLon = deg2rad (lon2-lon1);
		var a =
			Mathf.Sin (dLat / 2) * Mathf.Sin (dLat / 2) +
			Mathf.Cos (deg2rad (lat1)) * Mathf.Cos (deg2rad (lat2)) *
			Mathf.Sin (dLon / 2) * Mathf.Sin (dLon / 2)
			;
		var c = 2 * Mathf.Atan2 (Mathf.Sqrt(a),Mathf.Sqrt(1-a));
		var d = R * c;
		return d;
	}
	public static float deg2rad(float deg){
		return deg * (Mathf.PI/180);
	}
	public static void saveInfo(){
		File.Delete (globalOptionsPath);
		var file = File.CreateText (globalOptionsPath);
		file.WriteLine (disableBorderChunks);
		file.WriteLine (lastPolicyAccept);
		file.Close();
	}
	public static void loadInfo(){
		string line;
		int index = 0;
		StreamReader theReader = new StreamReader(globalOptionsPath, Encoding.Default);
		using(theReader){
			do{
				line = theReader.ReadLine();
				if(line != null){
					if(index == 0){
						disableBorderChunks = bool.Parse(line);
					}
					if(index == 1){
						lastPolicyAccept = int.Parse(line);
					}
				}
				index += 1;
			}while (line != null);
			theReader.Close ();
		}
	}
}
