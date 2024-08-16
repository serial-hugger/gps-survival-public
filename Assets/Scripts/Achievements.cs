using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class Achievements : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ().Build();
		PlayGamesPlatform.InitializeInstance (config);
		PlayGamesPlatform.Activate ();
		SignIn ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public static void SignIn(){
		UnityEngine.Social.localUser.Authenticate (success => {if(success){print("success");}});
	}

	#region Achievements
	public static void UnlockAchievement(string id){
		if (Controller.slot != "/toybox") {
			UnityEngine.Social.ReportProgress (id, 100, success => {});
		}
	}

	public static void IncrementAchievement(string id,int amount){
		if (Controller.slot != "/toybox") {
			PlayGamesPlatform.Instance.IncrementAchievement (id, amount, success => {});
		}
	}

	public static void ShowAchievementsUI(){
		UnityEngine.Social.ShowAchievementsUI ();
	}
	#endregion /Achievements

	#region Leaderboards
	public static void AddScoreToLeaderboard(string id,long score){
		if (Controller.slot != "/toybox") {
			UnityEngine.Social.ReportScore (score, id, success => {});
		}
	}
	public static void ShowLeaderboardUI(){
		UnityEngine.Social.ShowLeaderboardUI ();
	}
	#endregion /Leaderboards
}
