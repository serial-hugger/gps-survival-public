using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCatalog : MonoBehaviour {

	public static string[] plants = new string[]{
		"id:0000;name:Cotton;img1:0;img2:1;img3:2;img4:3;special:cotton;growtime:6.0;drop:0049%100;drop:0049%50;drop:0050%100;drop:0050%75;drop:0050%50",
		"id:0001;name:Cabbage;img1:4;img2:5;img3:6;img4:7;special:cabbage;growtime:12.0;drop:0057%100;drop:0057%50;drop:0058%100;drop:0058%75;drop:0058%50",
		"id:0002;name:Red Flower;img1:8;img2:9;img3:10;img4:11;special:flower;growtime:0.50;drop:0061%100",
		"id:0003;name:Yellow Flower;img1:12;img2:13;img3:14;img4:15;special:flower;growtime:0.50;drop:0062%100",
		"id:0004;name:Purple Flower;img1:16;img2:17;img3:18;img4:19;special:flower;growtime:0.50;drop:0063%100",
		"id:0005;name:Cattail;img1:20;img2:21;img3:22;img4:23;special:cattail;growtime:24.0;drop:0064%100;drop:0064%75;drop:0064%50",
		"id:0006;name:Flytrap;img1:24;img2:25;img3:26;img4:27;special:flytrap;growtime:48.0;drop:0065%100;drop:0065%75;drop:0065%50;drop:0158%10",
		"id:0007;name:Tomato;img1:28;img2:29;img3:30;img4:31;special:tomato;growtime:24.0;drop:0090%100;drop:0090%75;drop:0090%50",
		"id:0008;name:Wheat;img1:32;img2:33;img3:34;img4:35;special:wheat;growtime:24.0;drop:0091%100;drop:0091%75;drop:0091%50;drop:0088%100;drop:0088%50",
		"id:0009;name:Pumpkin;img1:36;img2:37;img3:38;img4:39;special:pumpkin;growtime:48.0;drop:0130%100",
		"id:0010;name:Potato;img1:40;img2:41;img3:42;img4:43;special:potato;growtime:48.0;drop:0114%100;drop:0114%75;drop:0114%50",
		"id:0011;name:Carrot;img1:44;img2:45;img3:46;img4:47;special:carrot;growtime:48.0;drop:0112%100;drop:0112%75;drop:0112%50",
		"id:0012;name:Onion;img1:48;img2:49;img3:50;img4:51;special:onion;growtime:48.0;drop:0113%100;drop:0113%25",
		"id:0013;name:Peer;img1:52;img2:53;img3:54;img4:55;special:peer;growtime:72.0;drop:0140%100;drop:0141%75;drop:0142%50;drop:0143%25;drop:0148%7;drop:0149%6;drop:0150%5;drop:0151%4;drop:0152%3;drop:0153%2"
	};
	public static int[] normalPlants = new int[]{0,1,2,3,4,7,8,9,10,11,12};
	public static int[] flowerPlants = new int[]{2,3,4};
	public static int[] swampPlants = new int[]{5,6,9};
	public static int[] farmPlants = new int[]{0,1,7,8,9,10,11,12};
	public static Sprite[] plantSpriteSheet;
	public Sprite[] plantSpriteSheetTemp;

	// Use this for initialization
	void Start () {
		plantSpriteSheet = plantSpriteSheetTemp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//USED TO FIND PLANTS INDEX BY ID
	public static int getPlantSlot(string id){
		for(int i = 0;i<plants.Length;i++){
			if(plants[i].Contains("id:"+id)){
				return i;
			}
		}
		return -1;
	}
	public static int getPlantImage(string id,int stage){
		if(id == "null"){
			return 0;
		}
		string plant = plants [getPlantSlot (id)];
		string[] attributeList = plant.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="img1" && stage == 0){
				return int.Parse(attribute[1]);
			}
			if(attribute[0]=="img2" && stage == 1){
				return int.Parse(attribute[1]);
			}
			if(attribute[0]=="img3" && stage == 2){
				return int.Parse(attribute[1]);
			}
			if(attribute[0]=="img4" && stage >= 3){
				return int.Parse(attribute[1]);
			}
		}
		return 0;
	}
	public static string getPlantType(string id){
		string plant = plants [getPlantSlot (id)];
		string[] attributeList = plant.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="type"){
				return attribute[1];
			}
		}
		return "null";
	}
	public static string getPlantName(string id){
		string plant = plants [getPlantSlot (id)];
		string[] attributeList = plant.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="name"){
				return attribute[1];
			}
		}
		return "null";
	}
	public static int getPlantMax(string id){
		string plant = plants [getPlantSlot (id)];
		string[] attributeList = plant.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="max"){
				return int.Parse(attribute[1]);
			}
		}
		return -1;
	}
	public static float getPlantYOffset(string id){
		string plant = plants [getPlantSlot (id)];
		string[] attributeList = plant.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="yoffset"){
				return float.Parse(attribute[1]);
			}
		}
		return 0.0f;
	}
	public static float getPlantGrowTime(string id){
		string plant = plants [getPlantSlot (id)];
		string[] attributeList = plant.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="growtime"){
				return float.Parse(attribute[1]);
			}
		}
		return 0.0f;
	}
	public static string getPlantSpecial(string id){
		string plant = plants [getPlantSlot (id)];
		string[] attributeList = plant.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="special"){
				return attribute[1];
			}
		}
		return "";
	}
	public static string getPlantID(int slot){
		string plant = plants [slot];
		string[] attributeList = plant.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="id"){
				return attribute[1];
			}
		}
		return "";
	}
	public static string[] getPlantDrops(string id){
		List<string> drops = new List<string>();
		string plant = plants [getPlantSlot (id)];
		string[] attributeList = plant.Split (';');
		for(int i = 0;i < attributeList.Length;i++){
			string[] attribute = attributeList [i].Split(':');
			if(attribute[0]=="drop"){
				drops.Add(attribute[1]);
			}
		}
		string[] finalDrops = drops.ToArray();
		return finalDrops;
	}
	public static string constructRandomPlant(int randomizer,string biome,int x, int y){
		Random.InitState (randomizer+x+y);
		int slot = normalPlants[Random.Range (0,normalPlants.Length)];
		if(biome == "flowergarden"){
			slot = flowerPlants[Random.Range (0,flowerPlants.Length)];
		}
		if(biome == "swamp"){
			slot = swampPlants[Random.Range (0,swampPlants.Length)];
		}
		if(biome == "farmland"){
			slot = farmPlants[Random.Range (0,farmPlants.Length)];
		}
		string id = getPlantID (slot);
		return ("type:plant;stage:3;plantid:"+id+";special:"+getPlantSpecial(id)+";x:"+x+";y:"+y);
	}
}
