using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstablishmentCatalog : MonoBehaviour {
	
	public static string[] shops = new string[]{
		//layer1 = hat    layer4 = object
		"type:food;name:Mushroom Chef;layer1:0;layer2:1;layer3:2;layer4:3;layer5:4;layer6:null;layer7:null;layer8:null;layer9:null;randomlayer:2;saying:I'm not for sale!",
		"type:construction;name:Mushroom Builder;layer1:5;layer2:1;layer3:2;layer4:6;layer5:4;layer6:null;layer7:null;layer8:null;layer9:null;randomlayer:2;saying:Building without arms is\nvery difficult.",
		"type:farming;name:Mushroom Farmer;layer1:20;layer2:1;layer3:2;layer4:21;layer5:4;layer6:null;layer7:null;layer8:null;layer9:null;randomlayer:2;saying:I'm growing a family.",
		//layer1 = hat     layer3 = object
		"type:food;name:Robot Chef;layer1:7;layer2:8;layer3:9;layer4:10;layer5:null;layer6:null;layer7:null;layer8:null;layer9:null;randomlayer:2;saying:I welcome you.",
		"type:construction;name:Robot Builder;layer1:12;layer2:8;layer3:11;layer4:10;layer5:null;layer6:null;layer7:null;layer8:null;layer9:null;randomlayer:2;saying:Orange cone identified.",
		"type:farming;name:Robot Farmer;layer1:24;layer2:8;layer3:25;layer4:10;layer5:null;layer6:null;layer7:null;layer8:null;layer9:null;randomlayer:2;Enabling green thumb.",
		//layer2 = hat     layer3 = object
		"type:food;name:Rabbit Chef;layer1:13;layer2:16;layer3:17;layer4:14;layer5:15;layer6:null;layer7:null;layer8:null;layer9:null;randomlayer:5;saying:Hey friend!",
		"type:construction;name:Rabbit Builder;layer1:13;layer2:18;layer3:19;layer4:14;layer5:15;layer6:null;layer7:null;layer8:null;layer9:null;randomlayer:5;saying:Thanks for\nstopping by!",
		"type:farming;name:Rabbit Farmer;layer1:13;layer2:22;layer3:23;layer4:14;layer5:15;layer6:null;layer7:null;layer8:null;layer9:null;randomlayer:5;saying:Howdy."
	};
	public static string[] exclusiveShops = new string[]{
		"type:jeremy;name:Jeremy's\nFun Box;layer1:null;layer2:null;layer3:null;layer4:26;layer5:null;layer6:null;layer7:null;layer8:null;layer9:null;cities:3600130593;saying:The things in this box wasn't\nas fortunate..."
	};
	public static Sprite[] shopkeeperSpriteSheet;
	public Sprite[] shopkeeperSpriteSheetTemp;

	public static string[] foodItems = new string[]{
		"0031:100.0",
		"0058:100.0",
		"0083:100.0",
		"0084:100.0",
		"0085:100.0",
		"0086:100.0",
		"0087:100.0",
		"0090:100.0",
		"0093:100.0",
		"0094:100.0",
		"0100:100.0",
		"0110:100.0",
		"0111:100.0",
		"0112:100.0",
		"0113:100.0",
		"0114:100.0",
		"0130:100.0"
	};
	public static string[] constructionItems = new string[]{
		"0000:100.0",
		"0003:100.0",
		"0005:100.0",
		"0008:100.0",
		"0010:100.0",
		"0013:100.0",
		"0015:100.0",
		"0018:100.0",
		"0020:100.0",
		"0023:100.0",
		"0025:100.0",
		"0028:100.0",
		"0033:100.0",
		"0048:100.0",
		"0102:100.0",
		"0103:100.0",
		"0106:100.0",
		"0107:100.0",
		"0108:100.0"
	};
	public static string[] farmingItems = new string[]{
		"0002:100.0",
		"0003:100.0",
		"0007:100.0",
		"0008:100.0",
		"0012:100.0",
		"0013:100.0",
		"0017:100.0",
		"0018:100.0",
		"0022:100.0",
		"0023:100.0",
		"0027:100.0",
		"0028:100.0",
		"0030:100.0",
		"0049:100.0",
		"0057:100.0",
		"0064:100.0",
		"0065:100.0",
		"0088:100.0",
		"0089:100.0",
		"0102:100.0",
		"0131:100.0"
	};
	public static string[] jeremyItems = new string[]{
		"0176:100.0",
		"0177:100.0",
		"0178:100.0",
		"0179:75.0",
		"0180:50.0",
		"0181:100.0",
		"0182:100.0",
		"0183:50.0",
		"0184:50.0"
	};
	public static string[] shopkeeperNames = new string[]{
		"Toby",
		"Gilbert",
		"Herbert",
		"Max",
		"Maxwell",
		"Joey",
		"Buddy",
		"Jacob",
		"Noah",
		"Liam",
		"Logan",
		"Oliver",
		"Dylan",
		"Luke",
		"Nathan",
		"Jaxon",
		"Jack",
		"Lincoln",
		"Gavin",
		"Xavier",
		"Leo",
		"Bentley",
		"Kayden",
		"Sawyer"
	};
	public static string[] foodEstablishmentNames = new string[] {
		"Kitchen",
		"Diner",
		"Eatery",
		"Foods",
		"Cafe"
	};
	public static string[] constructionEstablishmentNames = new string[] {
		"Depot",
		"Garage",
		"Wares",
		"Supplies",
		"Hardware"
	};
	public static string[] farmingEstablishmentNames = new string[] {
		"Barn",
		"Shed",
		"Ranch",
		"Farm",
		"Acres"
	};

	// Use this for initialization
	void Start () {
		shopkeeperSpriteSheet = shopkeeperSpriteSheetTemp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//NORMAL SHOP FUNCTIONS
	public static int getShopkeeperImage(int slot, int layer){
		string entity = shops [slot];
		string[] attributeList = entity.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="layer"+layer.ToString()){
				if(attribute[1]!="null"){
					return int.Parse(attribute[1]);
				}else{
					return -1;
				}
			}
		}
		return -1;
	}
	public static string getShopkeeperType(int slot){
		string entity = shops [slot];
		string[] attributeList = entity.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="type"){
				return attribute[1];
			}
		}
		return "null";
	}
	public static string getShopkeeperSaying(int slot){
		string entity = shops [slot];
		string[] attributeList = entity.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="saying"){
				return attribute[1];
			}
		}
		return "null";
	}
	public static string getShopkeeperName(int slot){
		string entity = shops [slot];
		string[] attributeList = entity.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="name"){
				return attribute[1];
			}
		}
		return "null";
	}
	public static int getShopkeeperRandomLayer(int slot){
		string entity = shops [slot];
		string[] attributeList = entity.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="randomlayer"){
				return int.Parse(attribute[1]);
			}
		}
		return -1;
	}

	//EXCLUSIVE SHOP FUNCTIONS
	public static List<int> getExclusiveShopSlots(string cityID){
		List<int> slotsWithCity = new List<int>();
		for(int i = 0;i < exclusiveShops.Length;i++){
			string[] cities = getExclusiveShopCityID (i);
			for(int i2 = 0;i2 < cities.Length;i2++){
				if(cities[i2] == Controller.currentPlaceID){
					slotsWithCity.Add(i2);
				}
			}
		}
		return slotsWithCity;
	}
	public static string[] getExclusiveShopCityID(int slot){
		string entity = exclusiveShops [slot];
		string[] attributeList = entity.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="cities"){
				if (attribute [1].Contains ("x")) {
					return attribute [1].Split ('x');
				} else {
					return new string[]{attribute [1]};
				}
			}
		}
		return null;
	}
	public static int getExclusiveShopkeeperImage(int slot, int layer){
		string entity = exclusiveShops [slot];
		string[] attributeList = entity.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="layer"+layer.ToString()){
				if(attribute[1]!="null"){
					return int.Parse(attribute[1]);
				}else{
					return -1;
				}
			}
		}
		return -1;
	}
	public static string getExclusiveShopkeeperType(int slot){
		string entity = exclusiveShops [slot];
		string[] attributeList = entity.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="type"){
				return attribute[1];
			}
		}
		return "null";
	}
	public static string getExclusiveShopkeeperSaying(int slot){
		string entity = exclusiveShops [slot];
		string[] attributeList = entity.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="saying"){
				return attribute[1];
			}
		}
		return "null";
	}
	public static string getExclusiveShopkeeperName(int slot){
		string entity = exclusiveShops [slot];
		string[] attributeList = entity.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="name"){
				return attribute[1];
			}
		}
		return "null";
	}
	public static int getExclusiveShopkeeperRandomLayer(int slot){
		string entity = exclusiveShops [slot];
		string[] attributeList = entity.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="randomlayer"){
				return int.Parse(attribute[1]);
			}
		}
		return -1;
	}
}
