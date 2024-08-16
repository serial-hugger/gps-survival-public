using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class AccountInfo : MonoBehaviour {

	//bundle code is the one that the gift will be awarded in

	public static string[] giftBox = new string[]{
		"id:4E49A37F-B8BF-44A4-87BA-ED1ACBOA0151;bundle:65;coins:540",
		"id:7BD2C7CA-9A01-4D7D-9F90-DDF221099850;bundle:66;coins:1000"
	};

	public static string accountInfoPath = "";
	public static long tickStarted = 0;
	public static int accountCoins = 0;
	public static long lastVisit = 0;
	public static long lastDailyBuy = 0;
	public static long firstPlay = 0;
	public static bool soundOption = true;
	public static bool internetOption = true;
	public static bool firstCustomize = false;

	public static int locationMode = 0;

	public static int gender = 0;
	public static int skin = 0;
	public static int hair = 0;
	public static int shirt = 0;
	public static int pants = 0;

	public static string playerID = "";
	public static int lastBundle = 0;

	public static Sprite[] headSpriteSheet;
	public Sprite[] headSpriteSheetTemp;

	public static Sprite[] handSpriteSheet;
	public Sprite[] handSpriteSheetTemp;

	public static Sprite[] maleHairSpriteSheet;
	public Sprite[] maleHairSpriteSheetTemp;

	public static Sprite[] femaleHairSpriteSheet;
	public Sprite[] femaleHairSpriteSheetTemp;

	public static Sprite[] shirtSpriteSheet;
	public Sprite[] shirtSpriteSheetTemp;

	public static Sprite[] pantsSpriteSheet;
	public Sprite[] pantsSpriteSheetTemp;

	// Use this for initialization
	void Start () {
		headSpriteSheet = headSpriteSheetTemp;
		handSpriteSheet = handSpriteSheetTemp;
		maleHairSpriteSheet = maleHairSpriteSheetTemp;
		femaleHairSpriteSheet = femaleHairSpriteSheetTemp;
		shirtSpriteSheet = shirtSpriteSheetTemp;
		pantsSpriteSheet = pantsSpriteSheetTemp;

		accountInfoPath = (Application.persistentDataPath + Controller.slot + "/accountinfo");
		if (File.Exists (accountInfoPath)) {
			loadInfo ();
			print (playerID.ToUpper());
			if(lastVisit<1000){
				tickStarted = System.DateTime.Now.Ticks;
				locationMode = 1;
			}
			PetInfo.StartUpdatePetInfo (lastVisit);
			saveInfo ();
		}
		if(playerID == ""){
			playerID = System.Guid.NewGuid ().ToString();
			print ("GUID CREATED:" + playerID);
		}
		if(lastBundle < Controller.bundleCode){
			for(int i = 0;i<giftBox.Length;i++){
				if(getGiftID(i).Replace("0","").Replace("O","")==playerID.Replace("0","").Replace("O","")){
					if(getGiftBundle(i)>lastBundle){
						lastBundle = Controller.bundleCode;
						addCoins (getGiftCoins(i));
					}
				}
			}
			lastBundle = Controller.bundleCode;
		}
		//accountCoins = 10000;
	}
	
	// Update is called once per frame
	void Update () {
		lastVisit = System.DateTime.Now.Ticks;
	}
	public static void saveInfo(){
		File.Delete (accountInfoPath);
		var file = File.CreateText (accountInfoPath);
		file.WriteLine (Security.Rot39(tickStarted.ToString(),39));
		file.WriteLine (Security.Rot39(accountCoins.ToString(),556));
		file.WriteLine (Security.Rot39(lastVisit.ToString(),7435));
		file.WriteLine (Security.Rot39(lastDailyBuy.ToString(),65));
		if (firstPlay > 1000) {
			file.WriteLine (Security.Rot39 (firstPlay.ToString (), 678));
		} else {
			file.WriteLine (Security.Rot39 (System.DateTime.Now.Ticks.ToString(), 678));
		}
		file.WriteLine (Security.Rot39(soundOption.ToString(),232));
		file.WriteLine (Security.Rot39(internetOption.ToString(),555));
		file.WriteLine (Security.Rot39(gender.ToString(),23));
		file.WriteLine (Security.Rot39(skin.ToString(),23));
		file.WriteLine (Security.Rot39(hair.ToString(),23));
		file.WriteLine (Security.Rot39(shirt.ToString(),23));
		file.WriteLine (Security.Rot39(pants.ToString(),23));
		file.WriteLine (Security.Rot39(firstCustomize.ToString(),23));
		file.WriteLine (Security.Rot39(playerID.ToString(),567));
		file.WriteLine (Security.Rot39(lastBundle.ToString(),34));
		file.Close();
	}
	public static void loadInfo(){
		string line;
		int index = 0;
		StreamReader theReader = new StreamReader(accountInfoPath, Encoding.Default);
		using(theReader){
			do{
				line = theReader.ReadLine();
				if(line != null){
					if(index == 0){
						line = Security.Rot39(line,39);
						tickStarted = long.Parse(line);
					}
					if(index == 1){
						line = Security.Rot39(line,556);
						accountCoins = int.Parse(line);
					}
					if(index == 2){
						line = Security.Rot39(line,7435);
						lastVisit = long.Parse(line);
					}
					if(index == 3){
						line = Security.Rot39(line,65);
						lastDailyBuy = long.Parse(line);
					}
					if(index == 4){
						line = Security.Rot39(line,678);
						firstPlay = long.Parse(line);
						if (!(firstPlay > 1000)) {
							firstPlay = System.DateTime.Now.Ticks;
						}
					}
					if(index == 5){
						line = Security.Rot39(line,232);
						soundOption = bool.Parse(line);
					}
					if(index == 6){
						line = Security.Rot39(line,555);
						internetOption = bool.Parse(line);
					}
					if(index == 7){
						line = Security.Rot39(line,23);
						gender = int.Parse(line);
					}
					if(index == 8){
						line = Security.Rot39(line,23);
						skin = int.Parse(line);
					}
					if(index == 9){
						line = Security.Rot39(line,23);
						hair = int.Parse(line);
					}
					if(index == 10){
						line = Security.Rot39(line,23);
						shirt = int.Parse(line);
					}
					if(index == 11){
						line = Security.Rot39(line,23);
						pants = int.Parse(line);
					}
					if(index == 12){
						line = Security.Rot39(line,23);
						firstCustomize = bool.Parse(line);
					}
					if(index == 13){
						line = Security.Rot39(line,567);
						playerID = line;
					}
					if(index == 14){
						line = Security.Rot39(line,34);
						lastBundle = int.Parse(line);
					}
				}
				index += 1;
			}while (line != null);
			theReader.Close ();
		}
	}
	public static void addCoins(int amount){
		accountCoins += amount;
		saveInfo ();
		Instantiate (Resources.Load ("Effects/CoinSound"), new Vector3 (0, 0, 0), Quaternion.identity);
	}
	public static void spendCoins(int amount){
		accountCoins -= amount;
		saveInfo ();
		Achievements.IncrementAchievement (GPGSIds.achievement_patron,amount);
		Achievements.IncrementAchievement (GPGSIds.achievement_consumer,amount);
		Achievements.IncrementAchievement (GPGSIds.achievement_mega_buyer,amount);
		Instantiate (Resources.Load ("Effects/CoinSound"), new Vector3 (0, 0, 0), Quaternion.identity);
	}
	public static string getGiftID(int slot){
		string item = giftBox [slot];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="id"){
				return attribute[1].ToLower();
			}
		}
		return "null";
	}
	public static int getGiftCoins(int slot){
		string item = giftBox [slot];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="coins"){
				return int.Parse(attribute[1]);
			}
		}
		return 0;
	}
	public static int getGiftBundle(int slot){
		string item = giftBox [slot];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="bundle"){
				return int.Parse(attribute[1]);
			}
		}
		return 0;
	}
}
