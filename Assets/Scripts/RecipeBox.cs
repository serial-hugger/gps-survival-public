using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBox : MonoBehaviour {

	public Sprite itemSprite;
	public string section;
	public string[] neededItems = new string[]{"","","","","",""};
	public string[] neededTypes = new string[]{"","","","","",""};
	public string[] neededSkills = new string[]{"","","","","",""};
	public float tempBelow = 1234.0f;
	public float tempAbove = 1234.0f;
	public int[] neededSkillLevels = new int[]{0,0,0,0,0,0};
	public GameObject craftBox;
	public int[] neededAmounts = new int[]{0,0,0,0,0,0};
	public int amount;
	public SpriteRenderer amountCircle;
	public int recipeItem;
	private int actualRecipeItem;
	public bool prevEnabled;
	public int prevCraftingPage = 0;
	public SpriteRenderer renderer;
	public TextMesh text;
	public bool selected;
	public SpriteRenderer box;
	public bool inactive;
	public GameObject lockDisplay;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		actualRecipeItem = recipeItem + (15 * CameraLocation.craftingPage);
		if (CameraLocation.selectedRecipe != recipeItem) {
			selected = false;
		} else {
			selected = true;
		}
		if (inactive) {
			box.material.color = Color.gray;
		} else {
			box.material.color = Color.white;
		}
		UpdateBoxInfo (actualRecipeItem);
		inactive = false;
		CraftBox craftScript = craftBox.GetComponent<CraftBox> ();
		if (transform.parent.gameObject.activeSelf != prevEnabled) {
			prevEnabled = transform.parent.gameObject.activeSelf;
		}
		if (selected) {
			transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
			neededItems = Recipes.getNeededItems (section, actualRecipeItem);
			neededTypes = Recipes.getNeededTypes (section, actualRecipeItem);
			tempAbove = Recipes.getNeededTempAbove (section, actualRecipeItem);
			tempBelow = Recipes.getNeededTempBelow (section, actualRecipeItem);
			neededAmounts = Recipes.getNeededAmounts (section, actualRecipeItem);
			craftScript.amount = amount;
			craftScript.recipeItem = actualRecipeItem;
			craftScript.itemSprite = itemSprite;
			craftScript.neededItems = neededItems;
			craftScript.neededTypes = neededTypes;
			craftScript.neededAmounts = neededAmounts;
			craftScript.neededSkills = neededSkills;
			craftScript.tempAbove = tempAbove;
			craftScript.tempBelow = tempBelow;
			if (lockDisplay.activeSelf) {
				craftScript.hasSkills = false;
			} else {
				craftScript.hasSkills = true;
			}
			craftScript.neededSkillLevels = neededSkillLevels;
			craftScript.name = ItemCatalog.getItemName (Recipes.getCraftItemID (section, actualRecipeItem));
			craftScript.UpdateBoxInfo ();
		} else {
			transform.localScale = new Vector3 (1f, 1f, 1f);
		}
		if (Recipes.getCraftItemID (section, actualRecipeItem) == "null") {
			inactive = true;
		}
		if(section != Inventory.craftingSection){
			section = Inventory.craftingSection;
			UpdateBoxInfo (recipeItem);
		}
		if(recipeItem == 14){
			if (Recipes.getCraftItemID (section, actualRecipeItem) == "null") {
				CameraLocation.lastCraftingPage = true;
			} else {
				CameraLocation.lastCraftingPage = false;
			}
		}
		if(!inactive){
			neededSkills = Recipes.getNeededSkills (section, actualRecipeItem);
			neededSkillLevels = Recipes.getNeededLevels (section, actualRecipeItem);
			tempAbove = Recipes.getNeededTempAbove (section, actualRecipeItem);
			tempBelow = Recipes.getNeededTempBelow (section, actualRecipeItem);
		}
	}
	void UpdateBoxInfo(int slot){
		if (!inactive && !HasSkillLevels()) {
			lockDisplay.SetActive (true);
		} else {
			lockDisplay.SetActive (false);
		}
		amountCircle.enabled = false;
		itemSprite = null;
		amount = 0;
		text.text = "";
		renderer.sprite = null;
		box.color = Color.white;
		if (!inactive) {
			if (Recipes.getCraftItemID (section, slot) != "null") {
				if (Recipes.getCraftItemID (section, slot) != "null") {
					itemSprite = ItemCatalog.itemSpriteSheet [ItemCatalog.getItemImage (Recipes.getCraftItemID (section, slot))];
					amount = Recipes.getCraftItemAmount (section, slot);
					renderer.sprite = itemSprite;
				}
				if (amount > 1) {
					text.text = amount.ToString ();
					amountCircle.enabled = true;
				} else {
					text.text = "";
					amountCircle.enabled = false;
				}
			}
		} else {
			amountCircle.enabled = false;
		}
	}
	public bool HasSkillLevels(){
		bool status = true;
		for(int i = 0;i<neededSkills.Length;i++){
			if(neededSkills[i]!=""){
				if (!(Skills.getLevel (neededSkills [i]) >= neededSkillLevels [i])) {
					status = false;
				}
			}
		}
		return status;
	}
}
