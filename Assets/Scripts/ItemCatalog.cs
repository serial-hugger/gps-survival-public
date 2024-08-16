using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCatalog : MonoBehaviour {

	public static string[] items = new string[]{
		"id:0000;name:Wood Shovel;img:0;max:1;dur:10;type:shovel;pwr:0;price:3",
		"id:0001;name:Wood Pick;img:1;max:1;dur:10;type:pick;pwr:0;price:3",
		"id:0002;name:Wood Hoe;img:2;max:1;dur:10;type:hoe;pwr:0;price:3",
		"id:0003;name:Wood Axe;img:3;max:1;dur:10;type:axe;pwr:0;price:3",
		"id:0004;name:Wood Sword;img:4;max:1;dur:10;type:sword;pwr:0;price:3",
		"id:0005;name:Aluminum Shovel;img:5;max:1;dur:25;type:shovel;pwr:1;price:30",
		"id:0006;name:Aluminum Pick;img:6;max:1;dur:25;type:pick;pwr:1;price:30",
		"id:0007;name:Aluminum Hoe;img:7;max:1;dur:25;type:hoe;pwr:1;price:30",
		"id:0008;name:Aluminum Axe;img:8;max:1;dur:25;type:axe;pwr:1;price:30",
		"id:0009;name:Aluminum Sword;img:9;max:1;dur:25;type:sword;pwr:1;price:30",
		"id:0010;name:Iron Shovel;img:10;max:1;dur:50;type:shovel;pwr:2;price:40",
		"id:0011;name:Iron Pick;img:11;max:1;dur:50;type:pick;pwr:2;price:40",
		"id:0012;name:Iron Hoe;img:12;max:1;dur:50;type:hoe;pwr:2;price:40",
		"id:0013;name:Iron Axe;img:13;max:1;dur:50;type:axe;pwr:2;price:40",
		"id:0014;name:Iron Sword;img:14;max:1;dur:50;type:sword;pwr:2;price:40",
		"id:0015;name:Copper Shovel;img:15;max:1;dur:75;type:shovel;pwr:3;price:60",
		"id:0016;name:Copper Pick;img:16;max:1;dur:75;type:pick;pwr:3;price:60",
		"id:0017;name:Copper Hoe;img:17;max:1;dur:75;type:hoe;pwr:3;price:60",
		"id:0018;name:Copper Axe;img:18;max:1;dur:75;type:axe;pwr:3;price:60",
		"id:0019;name:Copper Sword;img:19;max:1;dur:75;type:sword;pwr:3;price:60",
		"id:0020;name:Gold Shovel;img:20;max:1;dur:100;type:shovel;pwr:4;price:80",
		"id:0021;name:Gold Pick;img:21;max:1;dur:100;type:pick;pwr:4;price:80",
		"id:0022;name:Gold Hoe;img:22;max:1;dur:100;type:hoe;pwr:4;price:80",
		"id:0023;name:Gold Axe;img:23;max:1;dur:100;type:axe;pwr:4;price:80",
		"id:0024;name:Gold Sword;img:24;max:1;dur:100;type:sword;pwr:4;price:80",
		"id:0025;name:Diamond Shovel;img:25;max:1;dur:200;type:shovel;pwr:5;price:100",
		"id:0026;name:Diamond Pick;img:26;max:1;dur:200;type:pick;pwr:5;price:100",
		"id:0027;name:Diamond Hoe;img:27;max:1;dur:200;type:hoe;pwr:5;price:100",
		"id:0028;name:Diamond Axe;img:28;max:1;dur:200;type:axe;pwr:5;price:100",
		"id:0029;name:Diamond Sword;img:29;max:1;dur:200;type:sword;pwr:5;price:100",
		"id:0030;name:Tree Start;img:30;max:16;type:treestart;pwr:0;price:5",
		"id:0031;name:Apple;img:31;max:16;type:food;price:10",
		"id:0032;name:Log;img:32;max:16;price:10",
		"id:0033;name:Plank;img:33;max:16;price:1",
		"id:0034;name:Bark;img:34;max:16;price:0",
		"id:0035;name:Rock;img:35;max:16;price:1",
		"id:0036;name:Coal;img:36;max:16;price:10",
		"id:0037;name:Aluminum;img:37;max:16;price:10",
		"id:0038;name:Iron;img:38;max:16;price:15",
		"id:0039;name:Copper;img:39;max:16;price:20",
		"id:0040;name:Gold;img:40;max:16;price:25",
		"id:0041;name:Diamond;img:41;max:16;price:30",
		"id:0042;name:Work Bench;type:placeable;img:42;yoffset:-.15;max:1;special:workbench;destroyer:axe;pwrmin:0;price:5",
		"id:0043;name:Furnace;type:placeable;img:43;yoffset:-.10;max:1;special:furnace;destroyer:pick;pwrmin:0;price:15",
		"id:0044;name:Aluminum Bar;img:44;max:16;price:15",
		"id:0045;name:Iron Bar;img:45;max:16;price:20",
		"id:0046;name:Copper Bar;img:46;max:16;price:25",
		"id:0047;name:Gold Bar;img:47;max:16;price:30",
		"id:0048;name:Anvil;type:placeable;img:48;yoffset:-.10;max:1;special:anvil;destroyer:pick;pwrmin:0;price:60",
		"id:0049;name:Cotton Seeds;type:seed;img:49;max:16;special:cotton;plantid:0000;price:5",
		"id:0050;name:Cotton;img:50;max:16;price:10",
		"id:0051;name:Wood Scythe;img:51;max:1;dur:100;type:scythe;pwr:0;price:3",
		"id:0052;name:Aluminum Scythe;img:52;max:1;dur:150;type:scythe;pwr:1;price:30",
		"id:0053;name:Iron Scythe;img:53;max:1;dur:200;type:scythe;pwr:2;price:40",
		"id:0054;name:Copper Scythe;img:54;max:1;dur:250;type:scythe;pwr:3;price:60",
		"id:0055;name:Gold Scythe;img:55;max:1;dur:300;type:scythe;pwr:4;price:80",
		"id:0056;name:Diamond Scythe;img:56;max:1;dur:350;type:scythe;pwr:5;price:100",
		"id:0057;name:Cabbage Seeds;type:seed;img:57;max:16;special:cabbage;plantid:0001;price:5",
		"id:0058;name:Cabbage;img:58;max:16;price:10",
		"id:0059;name:String;img:59;max:16;price:1",
		"id:0060;name:Wood Rod;img:60;max:1;dur:10;type:fishingpole;pwr:0;price:10",
		"id:0061;name:Red Flower;type:seed;img:61;max:16;special:flower;plantid:0002;price:5",
		"id:0062;name:Yellow Flower;type:seed;img:62;max:16;special:flower;plantid:0003;price:5",
		"id:0063;name:Purple Flower;type:seed;img:63;max:16;special:flower;plantid:0004;price:5",
		"id:0064;name:Cattail;type:seed;img:64;max:16;plantid:0005;price:5",
		"id:0065;name:Flytrap Seeds;type:seed;img:65;max:16;plantid:0006;price:5",
		"id:0066;name:Cooking Pot;type:placeable;img:66;yoffset:-.10;max:1;special:cookingpot;destroyer:pick;pwrmin:0;price:60",
		"id:0067;name:Bass;type:fish;img:67;max:16;price:20",
		"id:0068;name:Catfish;type:fish;img:68;max:16;price:20",
		"id:0069;name:Carp;type:fish;img:69;max:16;price:20",
		"id:0070;name:Salmon;type:fish;img:70;max:16;price:20",
		"id:0071;name:Koi;type:fish;img:71;max:16;price:20",
		"id:0072;name:Bluegill;type:fish;img:72;max:16;price:20",
		"id:0073;name:Sweetfish;type:fish;img:73;max:16;price:20",
		"id:0074;name:Freshwater Drum;type:fish;img:74;max:16;price:20",
		"id:0075;name:White Bass;type:fish;img:75;max:16;price:20",
		"id:0076;name:Yellow Bass;type:fish;img:76;max:16;price:20",
		"id:0077;name:Striped Bass;type:fish;img:77;max:16;price:20",
		"id:0078;name:Green Slime;img:78;max:16;price:5",
		"id:0079;name:Blue Slime;img:79;max:16;price:5",
		"id:0080;name:Purple Slime;img:80;max:16;price:5",
		"id:0081;name:Red Slime;img:81;max:16;price:5",
		"id:0082;name:Bone;img:82;max:16;price:2",
		"id:0083;name:Green Jello;type:food;img:83;max:16;price:20",
		"id:0084;name:Blue Jello;type:food;img:84;max:16;price:20",
		"id:0085;name:Purple Jello;type:food;img:85;max:16;price:20",
		"id:0086;name:Red Jello;type:food;img:86;max:16;price:20",
		"id:0087;name:Cooked Fish;type:food;img:87;max:16;price:20",
		"id:0088;name:Wheat Seeds;type:seed;plantid:0008;img:88;max:16;price:5",
		"id:0089;name:Tomato Seeds;type:seed;plantid:0007;img:89;max:16;price:2",
		"id:0090;name:Tomato;type:food;img:90;max:16;price:10",
		"id:0091;name:Wheat;img:91;max:16;price:10",
		"id:0092;name:Dough;img:92;max:16;price:15",
		"id:0093;name:Bread;type:food;img:93;max:16;price:20",
		"id:0094;name:Pizza;type:food;img:94;max:16;price:30",
		"id:0095;name:Aluminum Rod;img:95;max:1;dur:25;type:fishingpole;pwr:1;price:30",
		"id:0096;name:Iron Rod;img:96;max:1;dur:50;type:fishingpole;pwr:2;price:40",
		"id:0097;name:Copper Rod;img:97;max:1;dur:75;type:fishingpole;pwr:3;price:60",
		"id:0098;name:Gold Rod;img:98;max:1;dur:100;type:fishingpole;pwr:4;price:80",
		"id:0099;name:Diamond Rod;img:99;max:1;dur:200;type:fishingpole;pwr:5;price:100",
		"id:0100;name:Coconut;img:100;max:16;type:food;price:10",
		"id:0101;name:Stone Path;type:placeable;img:101;yoffset:-.16;max:16;destroyer:pick;pwrmin:0;price:15",
		"id:0102;name:Wood Fence;type:placeable;img:102;yoffset:-.10;max:16;destroyer:axe;pwrmin:0;price:15",
		"id:0103;name:Structure Base;type:placeable;img:103;yoffset:-.10;max:1;destroyer:axe;pwrmin:0;price:20",
		"id:0104;name:Structure Collection;type:buildingsite;img:104;yoffset:-.10;max:1;destroyer:axe;pwrmin:0;price:0",
		"id:0105;name:Storage Shed;type:building;img:105;yoffset:-.10;max:1;destroyer:axe;pwrmin:0;price:0",
		"id:0106;name:Nail;img:106;max:16;price:5",
		"id:0107;name:Saw;img:107;max:16;price:5",
		"id:0108;name:Hammer;img:108;max:16;price:5",
		"id:0109;name:House;type:building;img:109;yoffset:-.10;max:1;destroyer:axe;pwrmin:0;price:0",
		"id:0110;name:Cheese Burger;type:food;img:110;max:16;price:30",
		"id:0111;name:Chocolate Bar;type:food;img:111;max:16;price:20",
		"id:0112;name:Carrot;type:foodseed;plantid:0111;img:112;max:16;price:5",
		"id:0113;name:Onion;type:foodseed;plantid:0112;img:113;max:16;price:10",
		"id:0114;name:Potato;type:foodseed;plantid:0010;img:114;max:16;price:5",
		"id:0115;name:Soul Bass;type:fish;img:115;max:16;price:40",
		"id:0116;name:Soul Carp;type:fish;img:116;max:16;price:40",
		"id:0117;name:Soul Koi;type:fish;img:117;max:16;price:40",
		"id:0118;name:Soul Bluegill;type:fish;img:118;max:16;price:40",
		"id:0119;name:Soul Sweetfish;type:fish;img:119;max:16;price:40",
		"id:0120;name:Makky;type:fish;img:120;max:16;price:40",
		"id:0121;name:Goldfish;type:fish;img:121;max:16;price:40",
		"id:0122;name:Guppy;type:fish;img:122;max:16;price:40",
		"id:0123;name:Jellyfish;type:fish;img:123;max:16;price:40",
		"id:0124;name:Squid;type:fish;img:124;max:16;price:40",
		"id:0125;name:Dungeon;type:placeable;img:125;yoffset:-.10;max:1;special:dungeon;price:15",
		"id:0126;name:Damage Gem;type:gem;special:doubledamage;charges:50;img:126;max:1;price:200",
		"id:0127;name:Heal Gem;type:gem;special:healing;charges:50;img:127;max:1;price:200",
		"id:0128;name:Experience Gem;type:gem;special:doubleexperience;charges:50;img:128;max:1;price:200",
		"id:0129;name:CRTENME;type:crate;lat:NA;lon:NA;cratename:NA;img:129;max:1;price:1",
		"id:0130;name:Pumpkin;type:food;img:130;max:16;price:15",
		"id:0131;name:Pumpkin Seeds;type:seed;plantid:0009;img:131;max:16;price:2",
		"id:0132;name:Salad;type:food;img:132;max:16;price:40",
		"id:0133;name:Plasma;img:133;max:16;price:2",
		"id:0134;name:Spike;img:134;max:16;price:2",
		"id:0135;name:Busted Club;img:135;max:16;price:5",
		"id:0136;name:Club;img:136;max:1;dur:25;type:sword;pwr:1;price:30",
		"id:0137;name:Spiked Club;img:137;max:1;dur:25;type:sword;pwr:2;price:40",
		"id:0138;name:Bone Sword;img:138;max:1;dur:15;type:sword;pwr:1;price:25",
		"id:0139;name:Iron Piece;img:139;max:16;price:2",
		"id:0140;name:Red Bit;img:140;max:16;price:2",
		"id:0141;name:Orange Bit;img:141;max:16;price:2",
		"id:0142;name:Green Bit;img:142;max:16;price:2",
		"id:0143;name:Purple Bit;img:143;max:16;price:2",
		"id:0144;name:Peer Seeds;type:seed;img:144;max:16;special:peer;plantid:0013;price:5",
		"id:0145;name:Poison;img:145;max:16;price:2",
		"id:0146;name:Toxic Bone Sword;img:146;max:1;dur:15;type:sword;pwr:2;price:35",
		"id:0147;name:Plasma Beam;img:147;max:1;dur:30;type:sword;pwr:1;price:20",
		"id:0148;name:Viral Shovel;img:148;max:1;dur:30;type:shovel;pwr:1;price:35",
		"id:0149;name:Viral Pick;img:149;max:1;dur:30;type:pick;pwr:1;price:35",
		"id:0150;name:Viral Hoe;img:150;max:1;dur:30;type:hoe;pwr:1;price:35",
		"id:0151;name:Viral Axe;img:151;max:1;dur:30;type:axe;pwr:1;price:35",
		"id:0152;name:Viral Sword;img:152;max:1;dur:30;type:sword;pwr:1;price:35",
		"id:0153;name:Viral Scyth;img:153;max:1;dur:30;type:scythe;pwr:1;price:35",
		"id:0154;name:Crab Claw;img:154;max:16;type:meat;price:2",
		"id:0155;name:Crab Beater;img:155;max:1;dur:20;type:sword;pwr:1;price:25",
		"id:0156;name:Raw Meat;type:food;img:156;max:16;price:10",
		"id:0157;name:Cooked Meat;type:food;img:157;max:16;price:20",
		"id:0158;name:Fly;type:bait;img:158;max:16;price:2",
		"id:0159;name:Worm;type:bait;img:159;max:16;price:2",
		"id:0160;name:Chicken;type:placeable;yoffset:-.11;img:160;max:1;price:2",
		"id:0161;name:Cow;type:placeable;yoffset:-.11;img:161;max:1;price:2",
		"id:0162;name:Pig;type:placeable;yoffset:-.11;img:162;max:1;price:2",
		"id:0163;name:Lasso;type:catcher;pwr:0;img:163;max:16;price:8",
		"id:0164;name:Egg;img:164;max:16;price:5",
		"id:0165;name:Milk;img:165;max:16;price:5",
		"id:0166;name:Bacon;type:meat;img:166;max:16;price:5",
		"id:0167;name:Mashed Potatoes;type:food;img:167;max:16;price:40",
		"id:0168;name:Potato Soup;type:food;img:168;max:16;price:40",
		"id:0169;name:Binoculars;img:169;max:1;type:binocular;pwr:1;price:40",
		"id:0170;name:Cloth;img:170;max:16;price:5",
		"id:0171;name:Umbrella;img:171;max:1;dur:25;type:sword;pwr:2;rainpwr:3;price:40",
		"id:0172;name:Pail;type:bucket;pwr:0;img:172;max:1;price:5",
		"id:0173;name:Water Pail;img:173;max:1;price:5",
		"id:0174;name:Ice;img:174;max:16;price:3",
		"id:0175;name:Icycle;img:175;max:1;dur:25;type:sword;pwr:2;snowpwr:3;price:40",
		"id:0176;name:Foxxy Plush;img:176;max:16;price:50",
		"id:0177;name:Dino Plush;img:177;max:16;price:50",
		"id:0178;name:Snubble Plush;img:178;max:16;price:50",
		"id:0179;name:Phoebe Plush;img:179;max:16;price:50",
		"id:0180;name:Flappy Plush;img:180;max:16;price:50",
		"id:0181;name:Brainbow Puzzle;img:181;max:16;price:50",
		"id:0182;name:Brainbow Device;img:182;max:16;price:50",
		"id:0183;name:Volcano Escape;img:183;max:16;price:50",
		"id:0184;name:Krawk Plush;img:184;max:16;price:50"
	};
	public static Sprite[] itemSpriteSheet;
	public Sprite[] itemSpriteSheetTemp;

	void Start () {
		itemSpriteSheet = itemSpriteSheetTemp;
	}
	//USED TO FIND ITEMS INDEX BY ID
	public static int getItemSlot(string id){
		for(int i = 0;i<items.Length;i++){
			if(items[i].Contains("id:"+id)){
				return i;
			}
		}
		return -1;
	}
	//USED TO GENERATE A ITEM COMPATIBLE WITH BACKPACK
	public static string constructItem(string id){
		string item = items [getItemSlot (id)];
		string constructedItem = "";
		constructedItem += "id:" + id;
		string[] attributeList = item.Split (';');
		for(int i = 0; i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split (':');
			if(attribute[0]=="dur"){
				constructedItem += ";" + attribute [0] + ":" + attribute [1];
			}
			if(attribute[0]=="charges"){
				constructedItem += ";" + attribute [0] + ":" + attribute [1];
			}
			if(attribute[0]=="lat"){
				constructedItem += ";" + attribute [0] + ":" + Input.location.lastData.latitude;
			}
			if(attribute[0]=="lon"){
				constructedItem += ";" + attribute [0] + ":" + Input.location.lastData.longitude;
			}
			if(attribute[0]=="cratename"){
				constructedItem += ";" + attribute [0] + ":" + Controller.currentPlaceName+" Crate";
			}
		}
		return constructedItem + ";qty:1";
	}
	public static int getItemImage(string id){
		if(id == "null"){
			return 0;
		}
		string item = items [getItemSlot (id)];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="img"){
				return int.Parse(attribute[1]);
			}
		}
		return 0;
	}
	public static string getItemType(string id){
		if(id != "null"){
		string item = items [getItemSlot (id)];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="type"){
				return attribute[1];
			}
		}
		}
		return "null";
	}
	public static string getItemName(string id){
		string item = items [getItemSlot (id)];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="name"){
				return attribute[1];
			}
		}
		return "null";
	}
	public static int getItemPower(string id){
		if(id != "null"){
			string item = items [getItemSlot (id)];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="pwr"){
					return int.Parse(attribute[1]);
				}
			}
		}
		return 0;
	}
	public static int getItemCharges(string id){
		if(id != "null"){
			string item = items [getItemSlot (id)];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="charges"){
					return int.Parse(attribute[1]);
				}
			}
		}
		return 0;
	}
	public static int getItemPowerMin(string id){
		string item = items [getItemSlot (id)];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="pwrmin"){
				return int.Parse(attribute[1]);
			}
		}
		return 0;
	}
	public static int getItemMax(string id){
		string item = items [getItemSlot (id)];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="max"){
				return int.Parse(attribute[1]);
			}
		}
		return -1;
	}
	public static int getItemDurability(string id){
		string item = items [getItemSlot (id)];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="dur"){
				return int.Parse(attribute[1]);
			}
		}
		return -1;
	}
	public static int getItemPrice(string id){
		string item = items [getItemSlot (id)];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="price"){
				return int.Parse(attribute[1]);
			}
		}
		return -1;
	}
	public static float getItemYOffset(string id){
		string item = items [getItemSlot (id)];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="yoffset"){
				return float.Parse(attribute[1]);
			}
		}
		return 0.0f;
	}
	public static string getItemSpecial(string id){
		string item = items [getItemSlot (id)];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="special"){
				return attribute[1];
			}
		}
		return "";
	}
	public static string getItemDestroyer(string id){
		string item = items [getItemSlot (id)];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="destroyer"){
				return attribute[1];
			}
		}
		return "";
	}
	public static string getItemPlantID(string id){
		print (id);
		string item = items [getItemSlot (id)];
		string[] attributeList = item.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="plantid"){
				return attribute[1];
			}
		}
		return "";
	}
}
