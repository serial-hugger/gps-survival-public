using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipes : MonoBehaviour {

	public static string[] bareHandRecipes = new string[]{
		"id:0033x1;name:Plank;part:0034x8",
		"id:0033x4;name:Plank;part:0032x1",
		"id:0000x1;name:Wood Shovel;part:0033x8",
		"id:0001x1;name:Wood Pick;part:0033x8",
		"id:0002x1;name:Wood Hoe;part:0033x8",
		"id:0003x1;name:Wood Axe;part:0033x8",
		"id:0004x1;name:Wood Sword;part:0033x8",
		"id:0051x1;name:Wood Scythe;part:0033x8",
		"id:0042x1;name:Work Bench;part:0032x2;part:0033x10",
		"id:0174x4;name:Ice;part:0173x1;tempbelow:32",
		"id:0172x1;name:Pail;part:0033x4",
		"id:0059x1;name:String;part:0050x4",
		"id:0089x4;name:Tomato Seeds;part:0090x1",
		"id:0131x4;name:Pumpkin Seeds;part:0130x1",
		"id:0163x1;name:Lasso;part:0059x4",
		"id:0144x1;name:Peer Seeds;part:0140x4;part:0141x4;part:0142x4;part:0143x4"
	};
	public static string[] workBenchRecipes = new string[]{
		"id:0043x1;name:Furnace;part:0035x16;part:0036x1",
		"id:0060x1;name:Fishing Pole;part:0059x2;part:0033x4",
		"id:0101x1;name:Stone Path;part:0035x8",
		"id:0102x1;name:Wood Fence;part:0033x8",
		"id:0103x1;name:Structure Base;part:0033x16",
		"id:0106x32;name:Nails;part:0045x1",
		"id:0107x1;name:Saw;part:0033x1;part:0045x1",
		"id:0108x1;name:Hammer;part:0033x1;part:0045x1",
		"id:0136x1;name:Club;part:0035x1;part:0033x8;skill:craftingx5",
		"id:0137x1;name:Club;part:0035x1;part:0134x4;part:0033x8;skill:craftingx10",
		"id:0138x1;name:Bone Sword;part:0082x8;skill:craftingx5",
		"id:0146x1;name:Toxic Bone Sword;part:0082x8;part:0145x4;skill:craftingx10",
		"id:0147x1;name:Plasma Beam;part:0133x8;part:0045x1;skill:craftingx10",
		"id:0155x1;name:Crab Beater;part:0154x8;skill:craftingx5",
		"id:0169x1;name:Binocular;part:0045x3;skill:craftingx5",
		"id:0171x1;name:Umbrella;part:0170x16;skill:craftingx10",
		"id:0175x1;name:Icycle;part:0174x16;tempbelow:32"
	};
	public static string[] furnaceRecipes = new string[]{
		"id:0036x1;name:Charcoal;part:0032x4",
		"id:0044x1;name:Aluminum Bar;part:0037x1;part:0036x1",
		"id:0044x8;name:Aluminum Bar;part:0037x8;part:0036x2;skill:smeltingx5",
		"id:0045x1;name:Iron Bar;part:0038x1;part:0036x1;skill:smeltingx5",
		"id:0045x8;name:Iron Bar;part:0038x8;part:0036x2;skill:smeltingx10",
		"id:0046x1;name:Copper Bar;part:0039x1;part:0036x1;skill:smeltingx10",
		"id:0046x8;name:Copper Bar;part:0039x8;part:0036x2;skill:smeltingx15",
		"id:0047x1;name:Gold Bar;part:0040x1;part:0036x1;skill:smeltingx15",
		"id:0047x8;name:Gold Bar;part:0040x8;part:0036x2;skill:smeltingx20",
		"id:0048x1;name:Anvil;part:0045x3;part:0036x1",
		"id:0066x1;name:Cooking Pot;part:0045x3;part:0036x1",
		"id:0087x1;name:Cooked Fish;typepart:fishx1;part:0036x1",
		"id:0087x8;name:Cooked Fish;typepart:fishx8;part:0036x2;skill:cookingx5",
		"id:0157x1;name:Cooked Meat;part:0156x1;part:0036x1",
		"id:0157x8;name:Cooked Meat;part:0156x8;part:0036x2;skill:cookingx5",
		"id:0093x1;name:Bread;part:0092x4",
		"id:0094x1;name:Pizza;part:0092x4;part:0090x1"
	};
	public static string[] anvilRecipes = new string[]{
		"id:0005x1;name:Aluminum Shovel;part:0044x4;part:0033x2;skill:diggingx5",
		"id:0006x1;name:Aluminum Pick;part:0044x4;part:0033x2;skill:miningx5",
		"id:0007x1;name:Aluminum Hoe;part:0044x4;part:0033x2;skill:farmingx5",
		"id:0008x1;name:Aluminum Axe;part:0044x4;part:0033x2;skill:cuttingx5",
		"id:0009x1;name:Aluminum Sword;part:0044x4;part:0033x2;skill:slayingx5",
		"id:0052x1;name:Aluminum Scythe;part:0044x4;part:0033x2;skill:farmingx5",
		"id:0095x1;name:Aluminum Rod;part:0044x4;part:0059x2;skill:fishingx5",
		"id:0010x1;name:Iron Shovel;part:0045x4;part:0033x2;skill:diggingx10;skill:smithingx5",
		"id:0011x1;name:Iron Pick;part:0045x4;part:0033x2;skill:miningx10;skill:smithingx5",
		"id:0012x1;name:Iron Hoe;part:0045x4;part:0033x2;skill:farmingx10;skill:smithingx5",
		"id:0013x1;name:Iron Axe;part:0045x4;part:0033x2;skill:cuttingx10;skill:smithingx5",
		"id:0014x1;name:Iron Sword;part:0045x4;part:0033x2;skill:slayingx10;skill:smithingx5",
		"id:0053x1;name:Iron Scythe;part:0045x4;part:0033x2;skill:farmingx10;skill:smithingx5",
		"id:0096x1;name:Iron Rod;part:0045x4;part:0059x2;skill:fishingx10;skill:smithingx5",
		"id:0015x1;name:Copper Shovel;part:0046x4;part:0033x2;skill:diggingx15;skill:smithingx10",
		"id:0016x1;name:Copper Pick;part:0046x4;part:0033x2;skill:miningx15;skill:smithingx10",
		"id:0017x1;name:Copper Hoe;part:0046x4;part:0033x2;skill:farmingx15;skill:smithingx10",
		"id:0018x1;name:Copper Axe;part:0046x4;part:0033x2;skill:cuttingx15;skill:smithingx10",
		"id:0019x1;name:Copper Sword;part:0046x4;part:0033x2;skill:slayingx15;skill:smithingx10",
		"id:0054x1;name:Copper Scythe;part:0046x4;part:0033x2;skill:farmingx15;skill:smithingx10",
		"id:0097x1;name:Copper Rod;part:0046x4;part:0059x2;skill:fishingx15;skill:smithingx10",
		"id:0020x1;name:Gold Shovel;part:0047x4;part:0033x2;skill:diggingx20;skill:smithingx15",
		"id:0021x1;name:Gold Pick;part:0047x4;part:0033x2;skill:miningx20;skill:smithingx15",
		"id:0022x1;name:Gold Hoe;part:0047x4;part:0033x2;skill:farmingx20;skill:smithingx15",
		"id:0023x1;name:Gold Axe;part:0047x4;part:0033x2;skill:cuttingx20;skill:smithingx15",
		"id:0024x1;name:Gold Sword;part:0047x4;part:0033x2;skill:slayingx20;skill:smithingx15",
		"id:0055x1;name:Gold Scythe;part:0047x4;part:0033x2;skill:farmingx20;skill:smithingx15",
		"id:0098x1;name:Gold Rod;part:0047x4;part:0059x2;skill:fishingx20;skill:smithingx15",
		"id:0025x1;name:Diamond Shovel;part:0041x4;part:0033x2;skill:diggingx25;skill:smithingx20",
		"id:0026x1;name:Diamond Pick;part:0041x4;part:0033x2;skill:miningx25;skill:smithingx20",
		"id:0027x1;name:Diamond Hoe;part:0041x4;part:0033x2;skill:farmingx25;skill:smithingx20",
		"id:0028x1;name:Diamond Axe;part:0041x4;part:0033x2;skill:cuttingx25;skill:smithingx20",
		"id:0029x1;name:Diamond Sword;part:0041x4;part:0033x2;skill:slayingx25;skill:smithingx20",
		"id:0056x1;name:Diamond Scythe;part:0041x4;part:0033x2;skill:farmingx25;skill:smithingx20",
		"id:0099x1;name:Diamond Rod;part:0041x4;part:0059x2;skill:fishingx25;skill:smithingx20"
	};
	public static string[] cookingRecipes = new string[]{
		"id:0083x1;name:Green Jello;part:0078x4",
		"id:0084x1;name:Blue Jello;part:0079x4",
		"id:0085x1;name:Purple Jello;part:0080x4",
		"id:0086x1;name:Red Jello;part:0081x4",
		"id:0092x1;name:Dough;part:0091x4",
		"id:0132x1;name:Salad;part:0058x1;part:0090x1;part:0112x1;part:0113x1",
		"id:0156x1;name:Raw Meat;typepart:meatx1",
		"id:0167x1;name:Mashed Potatoes;part:0114x4",
		"id:0168x1;name:Potato Soup;part:0114x1;part:0156x1;part:0112x1"
	};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public static string getCraftItemID(string section,int slot){
		if(section == "bareHand"){
			if(slot >= bareHandRecipes.Length){
				return "null";
			}
			string item = bareHandRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="id"){
					string[] splitx = attribute [1].Split ('x');
					return splitx[0];
				}
			}
		}
		if(section == "workbench"){
			if(slot >= workBenchRecipes.Length){
				return "null";
			}
			string item = workBenchRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="id"){
					string[] splitx = attribute [1].Split ('x');
					return splitx[0];
				}
			}
		}
		if(section == "furnace"){
			if(slot >= furnaceRecipes.Length){
				return "null";
			}
			string item = furnaceRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="id"){
					string[] splitx = attribute [1].Split ('x');
					return splitx[0];
				}
			}
		}
		if(section == "anvil"){
			if(slot >= anvilRecipes.Length){
				return "null";
			}
			string item = anvilRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="id"){
					string[] splitx = attribute [1].Split ('x');
					return splitx[0];
				}
			}
		}
		if(section == "cookingPot"){
			if(slot >= cookingRecipes.Length){
				return "null";
			}
			string item = cookingRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="id"){
					string[] splitx = attribute [1].Split ('x');
					return splitx[0];
				}
			}
		}
		return "null";
	}
	public static int getCraftItemAmount(string section,int slot){
		if(section == "bareHand"){
			if(slot > bareHandRecipes.Length-1){
				return -1;
			}
			string item = bareHandRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="id"){
					string[] splitx = attribute [1].Split ('x');
					return int.Parse(splitx[1]);
				}
			}
		}
		if(section == "workbench"){
			if(slot > workBenchRecipes.Length-1){
				return -1;
			}
			string item = workBenchRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="id"){
					string[] splitx = attribute [1].Split ('x');
					return int.Parse(splitx[1]);
				}
			}
		}
		if(section == "furnace"){
			if(slot > furnaceRecipes.Length-1){
				return -1;
			}
			string item = furnaceRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="id"){
					string[] splitx = attribute [1].Split ('x');
					return int.Parse(splitx[1]);
				}
			}
		}
		if(section == "anvil"){
			if(slot > anvilRecipes.Length-1){
				return -1;
			}
			string item = anvilRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="id"){
					string[] splitx = attribute [1].Split ('x');
					return int.Parse(splitx[1]);
				}
			}
		}
		if(section == "cookingPot"){
			if(slot > cookingRecipes.Length-1){
				return -1;
			}
			string item = cookingRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="id"){
					string[] splitx = attribute [1].Split ('x');
					return int.Parse(splitx[1]);
				}
			}
		}
		return -1;
	}
	public static string[] getNeededItems(string section,int slot){
		string[] items = new string[6];
		int currentItem = 0;
		if(section == "bareHand"){
			items [currentItem] = "";
			string item = bareHandRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="part"){
					string[] splitx = attribute [1].Split ('x');
					items[currentItem] = splitx[0];
					currentItem += 1;
				}
				if (attribute [0] == "typepart") {
					items[currentItem] = "";
					currentItem += 1;
				}
			}
		}
		if(section == "workbench"){
			items [currentItem] = "";
			string item = workBenchRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="part"){
					string[] splitx = attribute [1].Split ('x');
					items[currentItem] = splitx[0];
					currentItem += 1;
				}
				if (attribute [0] == "typepart") {
					items[currentItem] = "";
					currentItem += 1;
				}
			}
		}
		if(section == "furnace"){
			items [currentItem] = "";
			string item = furnaceRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="part"){
					string[] splitx = attribute [1].Split ('x');
					items[currentItem] = splitx[0];
					currentItem += 1;
				}
				if (attribute [0] == "typepart") {
					items[currentItem] = "";
					currentItem += 1;
				}
			}
		}
		if(section == "anvil"){
			items [currentItem] = "";
			string item = anvilRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="part"){
					string[] splitx = attribute [1].Split ('x');
					items[currentItem] = splitx[0];
					currentItem += 1;
				}
				if (attribute [0] == "typepart") {
					items[currentItem] = "";
					currentItem += 1;
				}
			}
		}
		if(section == "cookingPot"){
			items [currentItem] = "";
			string item = cookingRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="part"){
					string[] splitx = attribute [1].Split ('x');
					items[currentItem] = splitx[0];
					currentItem += 1;
				}
				if (attribute [0] == "typepart") {
					items[currentItem] = "";
					currentItem += 1;
				}
			}
		}
		return items;
	}
	public static string[] getNeededTypes(string section,int slot){
		string[] items = new string[6];
		int currentItem = 0;
		if(section == "bareHand"){
			items [currentItem] = "";
			string item = bareHandRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if (attribute [0] == "part") {
					items[currentItem] = "";
					currentItem += 1;
				}
				if(attribute[0]=="typepart"){
					string[] splitx = attribute [1].Split ('x');
					items[currentItem] = splitx[0];
					currentItem += 1;
				}
			}
		}
		if(section == "workbench"){
			items [currentItem] = "";
			string item = workBenchRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if (attribute [0] == "part") {
					items[currentItem] = "";
					currentItem += 1;
				}
				if(attribute[0]=="typepart"){
					string[] splitx = attribute [1].Split ('x');
					items[currentItem] = splitx[0];
					currentItem += 1;
				}
			}
		}
		if(section == "furnace"){
			items [currentItem] = "";
			string item = furnaceRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if (attribute [0] == "part") {
					items[currentItem] = "";
					currentItem += 1;
				}
				if(attribute[0]=="typepart"){
					string[] splitx = attribute [1].Split ('x');
					items[currentItem] = splitx[0];
					currentItem += 1;
				}
			}
		}
		if(section == "anvil"){
			items [currentItem] = "";
			string item = anvilRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if (attribute [0] == "part") {
					items[currentItem] = "";
					currentItem += 1;
				}
				if(attribute[0]=="typepart"){
					string[] splitx = attribute [1].Split ('x');
					items[currentItem] = splitx[0];
					currentItem += 1;
				}
			}
		}
		if(section == "cookingPot"){
			items [currentItem] = "";
			string item = cookingRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if (attribute [0] == "part") {
					items[currentItem] = "";
					currentItem += 1;
				}
				if(attribute[0]=="typepart"){
					string[] splitx = attribute [1].Split ('x');
					items[currentItem] = splitx[0];
					currentItem += 1;
				}
			}
		}
		return items;
	}
	public static int[] getNeededAmounts(string section,int slot){
		int[] amounts = new int[10];
		int currentItem = 0;
		if(section == "bareHand"){
			string item = bareHandRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="part" || attribute[0]=="typepart"){
					string[] splitx = attribute [1].Split ('x');
					amounts[currentItem] = int.Parse(splitx[1]);
					currentItem += 1;
				}
			}
		}
		if(section == "workbench"){
			string item = workBenchRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="part" || attribute[0]=="typepart"){
					string[] splitx = attribute [1].Split ('x');
					amounts[currentItem] = int.Parse(splitx[1]);
					currentItem += 1;
				}
			}
		}
		if(section == "furnace"){
			string item = furnaceRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="part" || attribute[0]=="typepart"){
					string[] splitx = attribute [1].Split ('x');
					amounts[currentItem] = int.Parse(splitx[1]);
					currentItem += 1;
				}
			}
		}
		if(section == "anvil"){
			string item = anvilRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="part" || attribute[0]=="typepart"){
					string[] splitx = attribute [1].Split ('x');
					amounts[currentItem] = int.Parse(splitx[1]);
					currentItem += 1;
				}
			}
		}
		if(section == "cookingPot"){
			string item = cookingRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="part" || attribute[0]=="typepart"){
					string[] splitx = attribute [1].Split ('x');
					amounts[currentItem] = int.Parse(splitx[1]);
					currentItem += 1;
				}
			}
		}
		return amounts;
	}
	public static string[] getNeededSkills(string section,int slot){
		string[] items = new string[]{"","","","","",""};
		int currentItem = 0;
		if(section == "bareHand"){
			items [currentItem] = "";
			string item = bareHandRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="skill"){
					string[] splitx = attribute [1].Split ('x');
					items[currentItem] = splitx[0];
					currentItem += 1;
				}
			}
		}
		if(section == "workbench"){
			items [currentItem] = "";
			string item = workBenchRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="skill"){
					string[] splitx = attribute [1].Split ('x');
					items[currentItem] = splitx[0];
					currentItem += 1;
				}
			}
		}
		if(section == "furnace"){
			items [currentItem] = "";
			string item = furnaceRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="skill"){
					string[] splitx = attribute [1].Split ('x');
					items[currentItem] = splitx[0];
					currentItem += 1;
				}
			}
		}
		if(section == "anvil"){
			items [currentItem] = "";
			string item = anvilRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="skill"){
					string[] splitx = attribute [1].Split ('x');
					items[currentItem] = splitx[0];
					currentItem += 1;
				}
			}
		}
		if(section == "cookingPot"){
			items [currentItem] = "";
			string item = cookingRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="skill"){
					string[] splitx = attribute [1].Split ('x');
					items[currentItem] = splitx[0];
					currentItem += 1;
				}
			}
		}
		return items;
	}
	public static int[] getNeededLevels(string section,int slot){
		int[] amounts = new int[]{0,0,0,0,0,0};
		int currentItem = 0;
		if(section == "bareHand"){
			string item = bareHandRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="skill"){
					string[] splitx = attribute [1].Split ('x');
					amounts[currentItem] = int.Parse(splitx[1]);
					currentItem += 1;
				}
			}
		}
		if(section == "workbench"){
			string item = workBenchRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="skill"){
					string[] splitx = attribute [1].Split ('x');
					amounts[currentItem] = int.Parse(splitx[1]);
					currentItem += 1;
				}
			}
		}
		if(section == "furnace"){
			string item = furnaceRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="skill"){
					string[] splitx = attribute [1].Split ('x');
					amounts[currentItem] = int.Parse(splitx[1]);
					currentItem += 1;
				}
			}
		}
		if(section == "anvil"){
			string item = anvilRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="skill"){
					string[] splitx = attribute [1].Split ('x');
					amounts[currentItem] = int.Parse(splitx[1]);
					currentItem += 1;
				}
			}
		}
		if(section == "cookingPot"){
			string item = cookingRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="skill"){
					string[] splitx = attribute [1].Split ('x');
					amounts[currentItem] = int.Parse(splitx[1]);
					currentItem += 1;
				}
			}
		}
		return amounts;
	}
	public static float getNeededTempAbove(string section,int slot){
		int currentItem = 0;
		if(section == "bareHand"){
			string item = bareHandRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="tempabove"){
					return float.Parse(attribute [1]);
				}
			}
		}
		if(section == "workbench"){
			string item = workBenchRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="tempabove"){
					return float.Parse(attribute [1]);
				}
			}
		}
		if(section == "furnace"){
			string item = furnaceRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="tempabove"){
					return float.Parse(attribute [1]);
				}
			}
		}
		if(section == "anvil"){
			string item = anvilRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="tempabove"){
					return float.Parse(attribute [1]);
				}
			}
		}
		if(section == "cookingPot"){
			string item = cookingRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="tempabove"){
					return float.Parse(attribute [1]);
				}
			}
		}
		return 1234.0f;
	}
	public static float getNeededTempBelow(string section,int slot){
		int currentItem = 0;
		if(section == "bareHand"){
			string item = bareHandRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="tempbelow"){
					return float.Parse(attribute [1]);
				}
			}
		}
		if(section == "workbench"){
			string item = workBenchRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="tempbelow"){
					return float.Parse(attribute [1]);
				}
			}
		}
		if(section == "furnace"){
			string item = furnaceRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="tempbelow"){
					return float.Parse(attribute [1]);
				}
			}
		}
		if(section == "anvil"){
			string item = anvilRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="tempbelow"){
					return float.Parse(attribute [1]);
				}
			}
		}
		if(section == "cookingPot"){
			string item = cookingRecipes [slot];
			string[] attributeList = item.Split (';');
			for(int i = 0;i < attributeList.Length;i++){
				string[] attribute = attributeList [i].Split(':');
				if(attribute[0]=="tempbelow"){
					return float.Parse(attribute [1]);
				}
			}
		}
		return 1234.0f;
	}
}
