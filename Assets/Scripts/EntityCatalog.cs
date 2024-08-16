using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCatalog : MonoBehaviour {

	public static string[] entities = new string[]{
		"id:0000;name:Skeleton;img:0;atk:5;hlth:10;drop:0082%75;drop:0082%50",
		"id:0001;name:Zombie;img:1;atk:5;hlth:20;drop:0010%1;drop:0011%1;drop:0012%1;drop:0013%1;drop:0014%1;drop:0053%1",
		"id:0002;name:Ghost;img:2;atk:5;hlth:30;drop:0133%50;drop:0133%25",
		"id:0003;name:Purple Slime;img:3;atk:5;hlth:10;drop:0080%75;drop:0080%50",
		"id:0004;name:Red Slime;img:4;atk:5;hlth:10;drop:0081%75;drop:0081%50",
		"id:0005;name:Green Slime;img:5;atk:5;hlth:10;drop:0078%75;drop:0078%50",
		"id:0006;name:Blue Slime;img:6;atk:5;hlth:10;drop:0079%75;drop:0079%50",
		"id:0007;name:Cactus;img:7;atk:15;hlth:25;drop:0134%75;drop:0134%50",
		"id:0008;name:Crab;img:8;atk:30;hlth:5;drop:0154%25",
		"id:0009;name:Flower;img:9;atk:5;hlth:10;drop:0061%50;drop:0062%50;drop:0063%50",
		"id:0010;name:Snake;img:10;atk:20;hlth:20;drop:0145%75;drop:0145%50",
		"id:0011;name:Goblin;img:11;atk:20;hlth:50;drop:0135%10",
		"id:0012;name:Mine;img:12;atk:10;hlth:15;drop:0139%50;drop:0139%10",
		"id:0013;name:Fyreball;img:13;atk:10;hlth:30;drop:0141%50",
		"id:0014;name:Overbyte;img:14;atk:10;hlth:30;drop:0142%50",
		"id:0015;name:Sproot;img:15;atk:10;hlth:30;drop:0143%50",
		"id:0016;name:Virol;img:16;atk:10;hlth:30;drop:0140%50"
	};
	public static int[]  normalEntityIndexes = new int[]{0,1,2,3,4,5,6};
	public static int[]  rareEntityIndexes = new int[]{7,8,9,10,11,12,13,14,15,16};
	public static Sprite[] entitySpriteSheet;
	public Sprite[] entitySpriteSheetTemp;

	// Use this for initialization
	void Start () {
		entitySpriteSheet = entitySpriteSheetTemp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//USED TO FIND PLANTS INDEX BY ID
	public static int getEntitySlot(string id){
		for(int i = 0;i<entities.Length;i++){
			if(entities[i].Contains("id:"+id)){
				return i;
			}
		}
		return 0;
	}

	public static string getEntityName(string id){
		string entity = entities [getEntitySlot (id)];
		string[] attributeList = entity.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="name"){
				return attribute[1];
			}
		}
		return "null";
	}
	public static int getEntityImage(string id){
		string entity = entities [getEntitySlot (id)];
		string[] attributeList = entity.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="img"){
				return int.Parse(attribute[1]);
			}
		}
		return 0;
	}
	public static int getEntityAttack(string id){
		string entity = entities [getEntitySlot (id)];
		string[] attributeList = entity.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="atk"){
				return int.Parse(attribute[1]);
			}
		}
		return 0;
	}
	public static int getEntityHealth(string id){
		string entity = entities [getEntitySlot (id)];
		string[] attributeList = entity.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="hlth"){
				return int.Parse(attribute[1]);
			}
		}
		return 0;
	}
	public static string getEntityID(int slot){
		string entity = entities [slot];
		string[] attributeList = entity.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="id"){
				return attribute[1];
			}
		}
		return "";
	}
	public static string[] getEntityDrops(string id){
		List<string> drops = new List<string>();
		string entity = entities [getEntitySlot (id)];
		string[] attributeList = entity.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="drop"){
				drops.Add(attribute[1]);
			}
		}
		string[] finalDrops = drops.ToArray();
		return finalDrops;
	}
}
