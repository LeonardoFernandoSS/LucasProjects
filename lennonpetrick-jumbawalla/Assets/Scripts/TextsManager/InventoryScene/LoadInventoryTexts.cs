using UnityEngine;
using System.Collections;

public class LoadInventoryTexts : MonoBehaviour {

	public TextMesh chooseYourWeapons;

	void Start () {
	
		chooseYourWeapons.text = CurrentLanguage.texts.getInventory_ChooseYourWeapons();

	}

}