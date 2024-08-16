using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Linq;

public class Skills : MonoBehaviour {

	public static int cuttingExp;
	public static int miningExp;
	public static int diggingExp;
	public static int farmingExp;
	public static int slayingExp;
	public static int fishingExp;
	public static int craftingExp;
	public static int smeltingExp;
	public static int smithingExp;
	public static int cookingExp;
	public static int questingExp;
	public static int maxHealth = 100;
	public static int currentHealth = 100;
	public GameObject levelUp;
	public TextMesh levelUpText;
	public static string levelUpString;
	public static float levelUpDisplay;
	public float healTime;
	public Inventory inventoryScript;

	// Use this for initialization
	void Start () {
		if (File.Exists (Application.persistentDataPath + Controller.slot + "/stats")) {
			loadStats ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		healTime -= 1f * Time.deltaTime;
		if(healTime<0){
			if(PlayerScript.spellUsed == "0127" && !inventoryScript.window){
				changeCurrentHealth (1);
			}
			healTime = 1f;
		}
		if (levelUpDisplay > 0) {
			if(!levelUp.activeSelf){
				Instantiate (Resources.Load ("Effects/LevelUpSound"), new Vector3 (transform.position.x, transform.position.y, 0), Quaternion.identity);
			}
			levelUp.SetActive (true);
			levelUpText.text = levelUpString;
		} else {
			levelUp.SetActive (false);
		}
		levelUpDisplay -= 1.0f * Time.deltaTime;
	}
	public static void changeMaxHealth(int newHealth){
		maxHealth = newHealth;
		saveStats ();
	}
	public static void changeCurrentHealth(int amount){
		currentHealth += amount;
		if(currentHealth > maxHealth){
			currentHealth = maxHealth;
		}
		if(currentHealth < 0){
			currentHealth = 0;
		}
		saveStats ();
	}
	public static void addExp(string skill,int exp){
		exp *= Controller.experienceMultiplier ();
		int startLevel = getLevel (skill);
		if(skill == "cutting"){
			cuttingExp += exp;
			if(Controller.slot != "/toybox"){
				if(getLevel("cutting")>=10){
					Achievements.UnlockAchievement (GPGSIds.achievement_skilled_cutter);
				}
				if(getLevel("cutting")>=20){
					Achievements.UnlockAchievement (GPGSIds.achievement_professional_cutter);
				}
				if(getLevel("cutting")>=30){
					Achievements.UnlockAchievement (GPGSIds.achievement_master_cutter);
				}
				Achievements.AddScoreToLeaderboard (GPGSIds.leaderboard_cutting_level,getLevel("cutting"));
			}
		}
		if(skill == "mining"){
			miningExp += exp;
			if (Controller.slot != "/toybox") {
				if (getLevel ("mining") >= 10) {
					Achievements.UnlockAchievement (GPGSIds.achievement_skilled_miner);
				}
				if (getLevel ("mining") >= 20) {
					Achievements.UnlockAchievement (GPGSIds.achievement_professional_miner);
				}
				if (getLevel ("mining") >= 30) {
					Achievements.UnlockAchievement (GPGSIds.achievement_master_miner);
				}
				Achievements.AddScoreToLeaderboard (GPGSIds.leaderboard_mining_level, getLevel ("mining"));
			}
		}
		if(skill == "digging"){
			diggingExp += exp/2;
			if (Controller.slot != "/toybox") {
				if (getLevel ("digging") >= 10) {
					Achievements.UnlockAchievement (GPGSIds.achievement_skilled_digger);
				}
				if (getLevel ("digging") >= 20) {
					Achievements.UnlockAchievement (GPGSIds.achievement_professional_digger);
				}
				if (getLevel ("digging") >= 30) {
					Achievements.UnlockAchievement (GPGSIds.achievement_master_digger);
				}
				Achievements.AddScoreToLeaderboard (GPGSIds.leaderboard_digging_level, getLevel ("digging"));
			}
		}
		if(skill == "farming"){
			farmingExp += exp;
			if (Controller.slot != "/toybox") {
				if (getLevel ("farming") >= 10) {
					Achievements.UnlockAchievement (GPGSIds.achievement_skilled_farmer);
				}
				if (getLevel ("farming") >= 20) {
					Achievements.UnlockAchievement (GPGSIds.achievement_professional_farmer);
				}
				if (getLevel ("farming") >= 30) {
					Achievements.UnlockAchievement (GPGSIds.achievement_master_farmer);
				}
				Achievements.AddScoreToLeaderboard (GPGSIds.leaderboard_farming_level, getLevel ("farming"));
			}
		}
		if(skill == "slaying"){
			slayingExp += exp;
			if (Controller.slot != "/toybox") {
				if (getLevel ("slaying") >= 10) {
					Achievements.UnlockAchievement (GPGSIds.achievement_skilled_slayer);
				}
				if (getLevel ("slaying") >= 20) {
					Achievements.UnlockAchievement (GPGSIds.achievement_professional_slayer);
				}
				if (getLevel ("slaying") >= 30) {
					Achievements.UnlockAchievement (GPGSIds.achievement_master_slayer);
				}
				Achievements.AddScoreToLeaderboard (GPGSIds.leaderboard_slaying_level, getLevel ("slaying"));
			}
		}
		if(skill == "fishing"){
			fishingExp += exp;
			if (Controller.slot != "/toybox") {
				if (getLevel ("fishing") >= 10) {
					Achievements.UnlockAchievement (GPGSIds.achievement_skilled_fisher);
				}
				if (getLevel ("fishing") >= 20) {
					Achievements.UnlockAchievement (GPGSIds.achievement_professional_fisher);
				}
				if (getLevel ("fishing") >= 30) {
					Achievements.UnlockAchievement (GPGSIds.achievement_master_fisher);
				}
				Achievements.AddScoreToLeaderboard (GPGSIds.leaderboard_fishing_level, getLevel ("fishing"));
			}
		}
		if(skill == "crafting"){
			craftingExp += exp;
		}
		if(skill == "smelting"){
			if (PetInfo.IsPetHappy ("0008")) {
				smeltingExp += exp*2;
			} else {
				smeltingExp += exp;
			}

		}
		if(skill == "smithing"){
			smithingExp += exp;
		}
		if(skill == "cooking"){
			cookingExp += exp;
		}
		if(skill == "questing"){
			questingExp += exp;
		}
		if (getLevel (skill)>startLevel) {
			levelUpDisplay = 5.0f;
			levelUpString = ((skill + " " + getLevel (skill).ToString()).ToUpper());
		}
		saveStats ();
	}
	public static int getLevel(string skill){
		int additionalExp = 0;
		int currentExp = 0;
		int nextLvl = 100;
		int level = 0;
		int totalExp = 0;
		if(skill == "cutting"){
			totalExp = cuttingExp;
		}
		if(skill == "mining"){
			totalExp = miningExp;
		}
		if(skill == "digging"){
			totalExp = diggingExp;
		}
		if(skill == "farming"){
			totalExp = farmingExp;
		}
		if(skill == "slaying"){
			totalExp = slayingExp;
		}
		if(skill == "fishing"){
			totalExp = fishingExp;
		}
		if(skill == "crafting"){
			totalExp = craftingExp;
		}
		if(skill == "smelting"){
			totalExp = smeltingExp;
		}
		if(skill == "smithing"){
			totalExp = smithingExp;
		}
		if(skill == "cooking"){
			totalExp = cookingExp;
		}
		if(skill == "questing"){
			totalExp = questingExp;
		}
		while (totalExp > 0) {
			currentExp += 1;
			totalExp -= 1;
			if(currentExp == nextLvl){
				additionalExp += 25;
				nextLvl += (100 + additionalExp);
				level += 1;
			}
		}
		return level;
	}
	public static float getPercentage(string skill){
		int additionalExp = 0;
		int currentExp = 0;
		int nextLvl = 100;
		int prevNextLvl = 0;
		int totalExp = 0;
		if(skill == "cutting"){
			totalExp = cuttingExp;
		}
		if(skill == "mining"){
			totalExp = miningExp;
		}
		if(skill == "digging"){
			totalExp = diggingExp;
		}
		if(skill == "farming"){
			totalExp = farmingExp;
		}
		if(skill == "slaying"){
			totalExp = slayingExp;
		}
		if(skill == "fishing"){
			totalExp = fishingExp;
		}
		if(skill == "crafting"){
			totalExp = craftingExp;
		}
		if(skill == "smelting"){
			totalExp = smeltingExp;
		}
		if(skill == "smithing"){
			totalExp = smithingExp;
		}
		if(skill == "cooking"){
			totalExp = cookingExp;
		}
		if(skill == "questing"){
			totalExp = questingExp;
		}
		while (totalExp > 0) {
			currentExp += 1;
			totalExp -= 1;
			if(currentExp == nextLvl){
				additionalExp += 25;
				prevNextLvl = nextLvl;
				nextLvl += (100 + additionalExp);
			}
		}
		return ((float)(currentExp-prevNextLvl) / (float)(nextLvl-prevNextLvl));
	}
	public static void saveStats(){
		File.Delete (Application.persistentDataPath + Controller.slot + "/stats");
		var file = File.CreateText (Application.persistentDataPath + Controller.slot + "/stats");
		file.WriteLine (Security.Rot39(cuttingExp.ToString(),0));
		file.WriteLine (Security.Rot39(miningExp.ToString(),1));
		file.WriteLine (Security.Rot39(diggingExp.ToString(),2));
		file.WriteLine (Security.Rot39(farmingExp.ToString(),3));
		file.WriteLine (Security.Rot39(slayingExp.ToString(),4));
		file.WriteLine (Security.Rot39(fishingExp.ToString(),5));
		file.WriteLine (Security.Rot39(craftingExp.ToString(),6));
		file.WriteLine (Security.Rot39(smeltingExp.ToString(),7));
		file.WriteLine (Security.Rot39(smithingExp.ToString(),8));
		file.WriteLine (Security.Rot39(cookingExp.ToString(),9));
		file.WriteLine (Security.Rot39(questingExp.ToString(),10));
		file.WriteLine (Security.Rot39(maxHealth.ToString(),11));
		file.WriteLine (Security.Rot39(currentHealth.ToString(),12));
		file.Close ();
	}
	public static void loadStats(){
		string line;
		int index = 0;
		StreamReader theReader = new StreamReader(Application.persistentDataPath + Controller.slot + "/stats", Encoding.Default);
		using(theReader){
			do{
				line = theReader.ReadLine();
				if(line != null){
					if(index == 0){
						cuttingExp = int.Parse(Security.Rot39(line,0));
					}
					if(index == 1){
						miningExp = int.Parse(Security.Rot39(line,1));
					}
					if(index == 2){
						diggingExp = int.Parse(Security.Rot39(line,2));
					}
					if(index == 3){
						farmingExp = int.Parse(Security.Rot39(line,3));
					}
					if(index == 4){
						slayingExp = int.Parse(Security.Rot39(line,4));
					}
					if(index == 5){
						fishingExp = int.Parse(Security.Rot39(line,5));
					}
					if(index == 6){
						craftingExp = int.Parse(Security.Rot39(line,6));
					}
					if(index == 7){
						smeltingExp = int.Parse(Security.Rot39(line,7));
					}
					if(index == 8){
						smithingExp = int.Parse(Security.Rot39(line,8));
					}
					if(index == 9){
						cookingExp = int.Parse(Security.Rot39(line,9));
					}
					if(index == 10){
						questingExp = int.Parse(Security.Rot39(line,10));
					}
					if(index == 11){
						maxHealth = int.Parse(Security.Rot39(line,11));
					}
					if(index == 12){
						currentHealth = int.Parse(Security.Rot39(line,12));
					}
				}
				index += 1;
			}while (line != null);
			theReader.Close ();
		}
	}
}
