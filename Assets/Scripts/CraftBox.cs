using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftBox : MonoBehaviour {

	public Sprite itemSprite;
	public string[] neededItems = new string[]{"","","","","",""};
	public string[] neededTypes = new string[]{"","","","","",""};
	public int[] neededAmounts = new int[]{0,0,0,0,0,0};
	public string[] neededSkills = new string[]{"","","","","",""};
	public int[] neededSkillLevels = new int[]{0,0,0,0,0,0};
	public float tempBelow = 1234.0f;
	public float tempAbove = 1234.0f;
	public bool hasSkills;
	public int amount;
	public string section;
	public int recipeItem;
	public bool prevEnabled;
	public SpriteRenderer renderer;
	public TextMesh text;
	public SpriteRenderer box;
	public Sprite craftSprite;
	public Sprite cantCraftSprite;
	public TextMesh need1;
	public TextMesh need2;
	public TextMesh need3;
	public TextMesh need4;
	public TextMesh need5;
	public TextMesh need6;
	public string name;
	public TextMesh nameText;
	public bool inactive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(section != Inventory.craftingSection){
			recipeItem = -1;
			section = Inventory.craftingSection;
			UpdateBoxInfo ();
		}
		if (inactive) {
			box.sprite = cantCraftSprite;
		} else {
			box.sprite = craftSprite;
		}
	}
	public void UpdateBoxInfo(){
		inactive = false;
		if (hasSkills) {
			for (int i = 0; i < neededItems.Length; i++) {
				if (neededItems [i] != "") {
					if (!Inventory.checkForItem (neededItems [i], neededAmounts [i])) {
						inactive = true;
					}
				}
			}
			for (int i = 0; i < neededTypes.Length; i++) {
				if (neededTypes [i] != "") {
					if (!Inventory.checkForItemType (neededTypes [i], neededAmounts [i])) {
						inactive = true;
					}
				}
			}
		} else {
			inactive = true;
		}
		nameText.text = "";
		renderer.sprite = null;
		need1.text = "";
		need2.text = "";
		need3.text = "";
		need4.text = "";
		need5.text = "";
		need6.text = "";
		nameText.text = "";
		if(recipeItem != -1){
			if (((tempAbove == 1234 || CameraLocation.currentTemperature > tempAbove) && (tempBelow == 1234 || CameraLocation.currentTemperature < tempBelow))) {
				if (hasSkills) {
					if (neededAmounts [0] != 0) {
						if (neededTypes [0] == "") {
							need1.text = ItemCatalog.getItemName (neededItems [0]) + " x " + neededAmounts [0]; 
						} else {
							need1.text = neededTypes [0] + " x " + neededAmounts [0]; 
						}
					}
					if (neededAmounts [1] != 0) {
						if (neededTypes [1] == "") {
							need2.text = ItemCatalog.getItemName (neededItems [1]) + " x " + neededAmounts [1]; 
						} else {
							need2.text = neededTypes [1] + " x " + neededAmounts [1];
						}
					}
					if (neededAmounts [2] != 0) {
						if (neededTypes [2] == "") {
							need3.text = ItemCatalog.getItemName (neededItems [2]) + " x " + neededAmounts [2]; 
						} else {
							need3.text = neededTypes [2] + " x " + neededAmounts [2];
						}
					}
					if (neededAmounts [3] != 0) {
						if (neededTypes [3] == "") {
							need4.text = ItemCatalog.getItemName (neededItems [3]) + " x " + neededAmounts [3]; 
						} else {
							need4.text = neededTypes [3] + " x " + neededAmounts [3];
						}
					}
					if (neededAmounts [4] != 0) {
						if (neededTypes [4] == "") {
							need5.text = ItemCatalog.getItemName (neededItems [4]) + " x " + neededAmounts [4]; 
						} else {
							need5.text = neededTypes [4] + " x " + neededAmounts [4];
						}
					}
					if (neededAmounts [5] != 0) {
						if (neededTypes [5] == "") {
							need6.text = ItemCatalog.getItemName (neededItems [5]) + " x " + neededAmounts [5]; 
						} else {
							need6.text = neededTypes [5] + " x " + neededAmounts [5]; 
						}
					}
				} else {
					if (neededSkillLevels [0] != 0) {
						need1.text = neededSkills [0] + " lv." + neededSkillLevels [0]; 
					}
					if (neededSkillLevels [1] != 0) {
						need2.text = neededSkills [1] + " lv." + neededSkillLevels [1];
					}
					if (neededSkillLevels [2] != 0) {
						need3.text = neededSkills [2] + " lv." + neededSkillLevels [2];
					}
					if (neededSkillLevels [3] != 0) {
						need4.text = neededSkills [3] + " lv." + neededSkillLevels [3];
					}
					if (neededSkillLevels [4] != 0) {
						need5.text = neededSkills [4] + " lv." + neededSkillLevels [4];
					}
					if (neededSkillLevels [5] != 0) {
						need6.text = neededSkills [5] + " lv." + neededSkillLevels [5]; 
					}
				}
			} else {
				if(CameraLocation.currentTemperature <= tempAbove && tempAbove != 1234){
					need1.text = "TOO COLD"; 
				}
				if(CameraLocation.currentTemperature >= tempBelow && tempBelow != 1234){
					need1.text = "TOO WARM"; 
				}
			}
			nameText.text = name;
			renderer.sprite = itemSprite;
			if (amount > 1) {
				text.text = amount.ToString ();
			} else {
				text.text = "";
			}
		}
	}
	public void craft(){
		string craftID = Recipes.getCraftItemID (section,recipeItem);
		int craftAmount = amount;
		string skill = "";
		if (Inventory.canHoldItems (craftID, craftAmount)) {
			if (hasSkills) {
				if ((CameraLocation.currentTemperature >= tempAbove || tempAbove == 1234) && (CameraLocation.currentTemperature <= tempBelow || tempBelow == 1234)) {
					for (int i = 0; i < neededItems.Length; i++) {
						if (neededItems [i] != "") {
							if (!Inventory.checkForItem (neededItems [i], neededAmounts [i])) {
								GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0, 0, -20), Quaternion.identity);
								TextMesh errorText = error.GetComponent<TextMesh> ();
								errorText.text = "NOT ENOUGH\nMATERIALS";
								print ("not enough materials");
								return;
							}
						} else {
							if (!Inventory.checkForItemType (neededTypes [i], neededAmounts [i])) {
								GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0, 0, -20), Quaternion.identity);
								TextMesh errorText = error.GetComponent<TextMesh> ();
								errorText.text = "NOT ENOUGH\nMATERIALS";
								print ("not enough materials");
								return;
							}
						}
					}
					for (int i = 0; i < neededItems.Length; i++) {
						if (neededItems [i] != "") {
							Inventory.removeItem (neededItems [i], neededAmounts [i]);
						}
					}
					for (int i = 0; i < neededTypes.Length; i++) {
						if (neededTypes [i] != "") {
							Inventory.removeItemType (neededTypes [i], neededAmounts [i]);
						}
					}
					if (section == "workbench") {
						skill = "crafting";
					}
					if (section == "furnace") {
						skill = "smelting";
					}
					if (section == "anvil") {
						skill = "smithing";
					}
					if (section == "cookingpot") {
						skill = "cooking";
					}
					if (Recipes.getCraftItemID (section, recipeItem) == "0087"
					    || Recipes.getCraftItemID (section, recipeItem) == "0093"
					    || Recipes.getCraftItemID (section, recipeItem) == "0094") {
						skill = "cooking";
					}
					if (skill != "") {
						Skills.addExp (skill, (ItemCatalog.getItemPrice (Recipes.getCraftItemID (section, recipeItem)) * Recipes.getCraftItemAmount (section, recipeItem)) * 4);
					}
					Inventory.addItem (Recipes.getCraftItemID (section, recipeItem), Recipes.getCraftItemAmount (section, recipeItem));
					Instantiate (Resources.Load ("Effects/BookCloseSound"), new Vector3 (transform.position.x + .05f, transform.position.y - .05f, 0), Quaternion.identity);
				} else {
					GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0,0, -20), Quaternion.identity);
					TextMesh errorText = error.GetComponent<TextMesh> ();
					errorText.text = "THE TEMPERATURE IS\nNOT RIGHT";
				}
			} else {
				GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0,0, -20), Quaternion.identity);
				TextMesh errorText = error.GetComponent<TextMesh> ();
				errorText.text = "SKILL REQUIREMENTS\nNOT REACHED";
			}
		} else {
			print ("no space");
			GameObject error = (GameObject)Instantiate (Resources.Load ("ErrorText"), new Vector3 (0,0, -20), Quaternion.identity);
			TextMesh errorText = error.GetComponent<TextMesh> ();
			errorText.text = "ONE EMPTY SPACE\nIS REQUIRED FOR\nCRAFTING";
		}
	}
}
