using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCatalog : MonoBehaviour {
	
	public static string[] scenarios = new string[]{
		"id:0000;text:A skeleton appears from the shadows.;img:0",
		"id:0001;text:A zombie is blocking your path.;img:1",
		"id:0002;text:You are startled by a ghost\nthat emerged from the ground.;img:2",
		"id:0003;text:Slime begins to cover the walls\nand floor.;img:3",
		"id:0004;text:You wander upon a mysterious hatch.;img:4",
		"id:0005;text:The walls and floor begin shaking.;img:5",
		"id:0006;text:The tunnel branches into two\nseperate pathways.;img:6",
		"id:0007;text:You find a mysterious button\non the wall.;img:7",
		"id:0008;text:You hear the sound of footsteps.;img:8",
		"id:0009;text:You smell a sweet scent\nbehind you.;img:9",
		"id:0010;text:There is a lone apple in the pathway.;img:10",
		"id:0011;text:There is water flooding the\ntunnel behind you.;img:11",
		"id:0012;text:There is lava flooding the\ntunnel behind you.;img:12",
		"id:0013;text:The floor has a decent\nsized chasm across.;img:13"
	};
	public static string[] options = new string[]{
		"id:0000;text:Attack him.;chance:50;successtext:You defeated him!;failtext:He was too strong...;successevent:null;failevent:damage",
		"id:0000;text:Outrun him.;chance:50;successtext:You're too fast for him!;failtext:You're too slow...;successevent:null;failevent:damage",
		"id:0001;text:Attack him.;chance:75;successtext:The rotten flesh goes flying!;failtext:You was bitten...;successevent:null;failevent:damage",
		"id:0001;text:Outrun him.;chance:25;successtext:Luckily the risen arent\ntoo fast!;failtext:Somehow he is\nsuper fast...;successevent:null;failevent:damage",
		"id:0002;text:Attack it.;chance:25;successtext:It didn't like that!;failtext:You went through and slammed\ninto the floor...;successevent:null;failevent:damage",
		"id:0002;text:Evade it.;chance:50;successtext:You're good at hiding!;failtext:It found you...;successevent:null;failevent:damage",
		"id:0002;text:Keep facing it.;chance:25;successtext:It disappeared!;failtext:This isn't a game...;successevent:null;failevent:damage",
		"id:0003;text:Go through it.;chance:40;successtext:You move slowly,\nbut safely!;failtext:You get a bad rash from\nwading in it...;successevent:null;failevent:damage",
		"id:0003;text:Eat it.;chance:60;successtext:Tastes like strawberry!;failtext:You bite the slime\nthe slime bites back...;successevent:null;failevent:damage",
		"id:0004;text:Open it.;chance:25;successtext:You found a shortcut!;failtext:There's a monster waiting\nfor you...;successevent:null;failevent:damage",
		"id:0004;text:Avoid it.;chance:75;successtext:You feel confident about\nyour decision!;failtext:Better safe than sorry...;successevent:doubleadvance;failevent:null",
		"id:0005;text:Run.;chance:50;successtext:You outran the rubble!;failtext:You got bopped by a brick...;successevent:null;failevent:damage",
		"id:0005;text:Get under something.;chance:50;successtext:You're safe!;failtext:You couldn't find\nanything...;successevent:null;failevent:damage",
		"id:0006;text:Left path.;chance:50;successtext:You're safe!;failtext:Tripped a trap...;successevent:null;failevent:damage",
		"id:0006;text:Right path.;chance:50;successtext:You're safe!;failtext:Tripped a trap...;successevent:null;failevent:damage",
		"id:0007;text:Press it.;chance:40;successtext:It revealed a\nshortcut!;failtext:Spikes trigger beneath you...;successevent:doubleadvance;failevent:damage",
		"id:0007;text:Ignore it.;chance:60;successtext:That could have been bad!;failtext:You wonder what it did...;successevent:null;failevent:null",
		"id:0008;text:Run away.;chance:50;successtext:Good thing you didn't\nstick around!;failtext:You run right into\na zombie...;successevent:null;failevent:damage",
		"id:0008;text:Prepare yourself.;chance:50;successtext:It was a zombie,\ngood thing you was ready!;failtext:It was a creature\nway too strong...;successevent:null;failevent:damage",
		"id:0009;text:Go investigate.;chance:50;successtext:It was a tasty pie\nleft all alone!;failtext:It's a monster dinner\nparty, I don't think\nyou was invited...;successevent:heal;failevent:damage",
		"id:0009;text:Keep walking.;chance:50;successtext:Sweet scents in a\ndungeon are nothing\nbut trouble!;failtext:Could have been\nsomething tasty...;successevent:null;failevent:null",
		"id:0010;text:Eat it.;chance:50;successtext:It doesn't taste too\ngood, but is filling!;failtext:You don't feel\nso good...;successevent:heal;failevent:damage",
		"id:0010;text:Ignore it.;chance:50;successtext:A rat eats it\nand dies!;failtext:A rat eats it\nand is fine...;successevent:null;failevent:null",
		"id:0011;text:Outrun it.;chance:20;successtext:You're tired,\nbut you outran it!;failtext:You ran off an edge...;successevent:null;failevent:damage",
		"id:0011;text:Higher ground.;chance:75;successtext:You should be safe\nup here!;failtext:You're not the best\nclimber and fall...;successevent:null;failevent:damage",
		"id:0011;text:Nothing.;chance:5;successtext:Luckily the water doesn't\nreach you!;failtext:Somehow it didn't work...;successevent:null;failevent:damage",
		"id:0012;text:Outrun it.;chance:75;successtext:Wow it's so slow!;failtext:You trip...;successevent:null;failevent:damage",
		"id:0012;text:Higher ground.;chance:20;successtext:The heat is intense,\nbut you're safe!;failtext:You fell in...;successevent:null;failevent:damage",
		"id:0012;text:Nothing.;chance:5;successtext:Luckily the lava doesn't\nreach you!;failtext:That great plan failed...;successevent:null;failevent:damage",
		"id:0013;text:Jump over.;chance:60;successtext:You leap to safety!;failtext:You stumble and fall...;successevent:null;failevent:damage",
		"id:0013;text:Go down.;chance:40;successtext:Seems to be another\nroute!;failtext:It's a pit of snakes...;successevent:advance;failevent:damage"
	};

	public static string[] gems = new string[]{
		"0126",
		"0127",
		"0128"
	};
	public static Sprite[] scenarioSpriteSheet;
	public Sprite[] scenarioSpriteSheetTemp;

	void Start () {
		scenarioSpriteSheet = scenarioSpriteSheetTemp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//USED TO FIND ITEMS INDEX BY ID
	public static int getScenarioSlot(string id){
		for(int i = 0;i<scenarios.Length;i++){
			if(scenarios[i].Contains("id:"+id)){
				return i;
			}
		}
		return -1;
	}
	public static int getOptionSlot(int optionNumber,string id){
		var index = 0;
		for(int i = 0;i<options.Length;i++){
			if(options[i].Contains("id:"+id)){
				if (index == optionNumber) {
					return i;
				} else {
					index += 1;
				}
			}
		}
		return -1;
	}
	//USED TO FIND ITEMS ID BY SLOT
	public static string getScenarioID(int slot){
		string fishs = scenarios [slot];
		string[] attributeList = fishs.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="id"){
				return attribute[1];
			}
		}
		return "null";
	}
	public static string getScenarioText(string id){
		string fishs = scenarios [getScenarioSlot(id)];
		string[] attributeList = fishs.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="text"){
				return attribute[1];
			}
		}
		return "";
	}
	public static int getScenarioImage(string id){
		string fishs = scenarios [getScenarioSlot(id)];
		string[] attributeList = fishs.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="img"){
				return int.Parse(attribute[1]);
			}
		}
		return 0;
	}
	public static string getOptionText(int slot){
		string fishs = options [slot];
		string[] attributeList = fishs.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="text"){
				return attribute[1];
			}
		}
		return "";
	}
	public static int getOptionChance(int slot){
		string fishs = options [slot];
		string[] attributeList = fishs.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="chance"){
				return int.Parse(attribute[1]);
			}
		}
		return 0;
	}
	public static string getOptionFailText(int slot){
		string fishs = options [slot];
		string[] attributeList = fishs.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="failtext"){
				return attribute[1];
			}
		}
		return "";
	}
	public static string getOptionSuccessText(int slot){
		string fishs = options [slot];
		string[] attributeList = fishs.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="successtext"){
				return attribute[1];
			}
		}
		return "";
	}
	public static string getOptionFailEvent(int slot){
		string fishs = options [slot];
		string[] attributeList = fishs.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="failevent"){
				return attribute[1];
			}
		}
		return "null";
	}
	public static string getOptionSuccessEvent(int slot){
		string fishs = options [slot];
		string[] attributeList = fishs.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="successevent"){
				return attribute[1];
			}
		}
		return "null";
	}
}
