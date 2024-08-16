using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour {

	public static string[] foodQuests = new string[] {
		"item:0031;max:10;text:I need apples for a\npie I am making.|I need apples for a\nnew recipe.|Do you think you could\nhelp me find\nsome apples?",
		"item:0058;max:10;text:I need cabbage for a\nsalad I am making.|I need cabbage for a\nnew recipe.|Do you think you could\nhelp me find\nsome cabbage?",
		"item:0083;max:10;text:I need green jello\nfor the dessert of\na meal I just made.",
		"item:0084;max:10;text:I need blue jello\nfor the dessert of\na meal I just made.",
		"item:0085;max:10;text:I need purple jello\nfor the dessert of\na meal I just made.",
		"item:0086;max:10;text:I need red jello\nfor the dessert of\na meal I just made.",
		"item:0066;max:1;text:My cooking pot busted.\ncan I buy one off of you?",
		"item:0112;max:20;text:Do you happen to\nhave some carrots?",
		"item:0113;max:20;text:Do you happen to\nhave some onions?",
		"item:0114;max:20;text:Do you happen to\nhave some potatos?"
	};
	public static string[] battleQuests = new string[] {
		"item:0004;max:10;text:I need some wooden swords\nfor a journey\nI will be going on.|I need some\nwooden swords.|Do you have any\nwooden swords I\ncan have?",
		"item:0009;max:10;text:I need some aluminum swords\nfor a journey I will be going on.|I need some\naluminum swords.|Do you have any\naluminum swords I\ncan have?",
		"item:0014;max:10;text:I need some iron swords\nfor a journey I will be going on.|I need some\niron swords.|Do you have any\niron swords I\ncan have?",
		"item:0019;max:10;text:I need some copper swords\nfor a journey I will be going on.|I need some\ncopper swords.|Do you have any\ncopper swords I\ncan have?",
		"item:0078;max:10;text:I hate green slimes,\ncan you defeat them\nand bring me the\ngreen slime?",
		"item:0079;max:10;text:I hate blue slimes,\ncan you defeat them\nand bring me the\nblue slime?",
		"item:0080;max:10;text:I hate purple slimes,\ncan you defeat them\nand bring me the\npurple slime?",
		"item:0081;max:10;text:I hate red slimes,\ncan you defeat them\nand bring me the\nred slime?",
		"item:0082;max:10;text:Can you slay some\nskeletons for me\nand bring me the\nbones?"
	};
	public static string[] farmingQuests = new string[] {
		"item:0002;max:10;text:I need some wooden hoes\nto plow the field\ntoday.|I need some\nwooden hoes.|Do you have any\nwooden hoes I\ncan have?",
		"item:0007;max:10;text:I need some aluminum hoes\nto plow the field\ntoday.|I need some\naluminum hoes.|Do you have any\naluminum hoes I\ncan have?",
		"item:0012;max:10;text:I need some iron hoes\nto plow the field\ntoday.|I need some\niron hoes.|Do you have any\niron hoes I\ncan have?",
		"item:0017;max:10;text:I need some copper hoes\nto plow the field\ntoday.|I need some\ncopper hoes.|Do you have any\ncopper hoes I\ncan have?",
		"item:0049;max:20;text:I need some cotton\nseeds to plant.",
		"item:0057;max:20;text:I need some cabbage\nseeds to plant.",
		"item:0088;max:20;text:I need some wheat\nseeds to plant.",
		"item:0089;max:20;text:I need some tomato\nseeds to plant.",
		"item:0131;max:20;text:I need some pumpkin\nseeds to plant.",
		"item:0112;max:20;text:Do you happen to\nhave some carrots?",
		"item:0113;max:20;text:Do you happen to\nhave some onions?",
		"item:0114;max:20;text:Do you happen to\nhave some potatos?"
	};
	public static string[] lumberQuests = new string[] {
		"item:0032;max:10;text:I havn't been running\ninto many trees lately,\ncan I buy some logs\noff of you?|Can you give me a hand?\nI'll pay you\nfor your work.",
		"item:0033;max:10;text:Choping logs is a pain,\ncan I buy some\nplanks off of you?|These logs are too heavy,\ncan I buy some lighter\nplanks off of you?",
		"item:0003;max:10;text:Can I buy some\nwooden axes off\nof you?",
		"item:0008;max:10;text:Can I buy some\naluminum axes off\nof you?",
		"item:0013;max:10;text:Can I buy some\niron axes off\nof you?",
		"item:0018;max:10;text:Can I buy some\ncopper axes off\nof you?",
		"item:0023;max:5;text:Can I buy some\ngold axes off\nof you?",
		"item:0028;max:1;text:Can I buy a\ndiamond axe off\nof you?"
	};
	public static string[] buildingQuests = new string[] {
		"item:0033;max:10;text:I'm a bit short on planks,\ncan I buy some\noff of you?",
		"item:0045;max:10;text:I need more iron bars for\na structure,\ncan I buy some\noff of you?",
		"item:0000;max:10;text:I need more wooden shovels\nfor an excavation,\ncan I buy some\noff of you?",
		"item:0005;max:10;text:I need more aluminum shovels\nfor an excavation,\ncan I buy some\noff of you?",
		"item:0010;max:10;text:I need more iron shovels\nfor an excavation,\ncan I buy some\noff of you?",
		"item:0106;max:20;text:I ran out of nails!\nCan you help me?",
		"item:0107;max:20;text:This job will take\nmore saws than\nI have...",
		"item:0108;max:20;text:I forgot to bring\nsome hammers with me!",
		"item:0102;max:20;text:I need some fence\nposts... can you\nsell me some?"
	};
	public static string[] fishingQuests = new string[] {
		"item:0067;max:10;text:I've been trying to\ncatch some bass all\nday... Can you\ncatch some for me?",
		"item:0068;max:10;text:Can you catch\nsome catfish for me?",
		"item:0069;max:10;text:I've been trying to\ncatch some carp all\nday... Can you\ncatch some for me?",
		"item:0070;max:10;text:Can you catch\nsome salmon for me?",
		"item:0071;max:10;text:I've been trying to\ncatch some koi all\nday... Can you\ncatch some for me?",
		"item:0072;max:10;text:Can you catch\nsome bluegill for me?",
		"item:0073;max:10;text:I've been trying to\ncatch some sweetfish\nall day... Can you\ncatch some for me?",
		"item:0074;max:10;text:Can you catch\nsome freshwater\ndrum for me?",
		"item:0075;max:10;text:I've been trying to\ncatch some white bass\nall day... Can you\ncatch some for me?",
		"item:0076;max:10;text:Can you catch\nsome yellow bass\n for me?",
		"item:0077;max:10;text:I've been trying to\ncatch some striped bass\nall day... Can you\ncatch some for me?"
	};
	public static string[] maleNames = new string[] {
		"Bill",
		"John",
		"Jack",
		"Johny",
		"Bob",
		"Kenny",
		"Keith",
		"Oswald",
		"Anthony",
		"Link",
		"Nathan",
		"Nate",
		"Adam",
		"Ian",
		"George",
		"Tim",
		"Stan",
		"Kyle",
		"Paul",
		"Steve",
		"Jim",
		"Jeremy",
		"Jacob",
		"Cody",
		"Josh",
		"Sebastian",
		"Noah",
		"Liam",
		"Mason",
		"William",
		"Ethan",
		"James",
		"Alexander",
		"Michael",
		"Daniel",
		"Logan"
	};
	public static string heldQuest1;
	public static string heldQuest2;
	public static string heldQuest3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public static string[] getQuest(int randomizer,string type){
		string fullLine;
		string item = "";
		string amount = "0";
		string text = "";
		string[] quests = new string[0];
		if(type == "food"){
			quests = foodQuests;
		}
		if(type == "battle"){
			quests = battleQuests;
		}
		if(type == "lumber"){
			quests = lumberQuests;
		}
		if(type == "building"){
			quests = buildingQuests;
		}
		if(type == "fishing"){
			quests = fishingQuests;
		}
		if(type == "farming"){
			quests = farmingQuests;
		}
		Random.InitState (randomizer);
		fullLine = quests [Random.Range (0, quests.Length-1)];
		string[] attributeList = fullLine.Split (';');
		for(int i = 0; i<attributeList.Length;i++){
			string[] attribute = attributeList [i].Split (':');
			if(attribute[0]=="item"){
				item = attribute [1];
			}
			if(attribute[0]=="text"){
				string[] texts = attribute [1].Split ('|');
				text = texts[Random.Range(0,texts.Length-1)];
			}
			if(attribute[0]=="max"){
				Random.InitState (randomizer+int.Parse(item));
				amount = Random.Range(1,int.Parse(attribute[1])).ToString();
			}
		}
		string[] final = new string[]{item,amount,text};
		return final;
	}
}
