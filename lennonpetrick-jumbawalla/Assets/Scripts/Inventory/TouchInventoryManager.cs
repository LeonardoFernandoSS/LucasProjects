using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInventoryManager : MonoBehaviour {

	public GameObject playButton, 
					  backButton, 
					  shopButton, 
					  weaponChosen1, 
					  weaponChosen2;
	
	public Transform  set;

	public  void weaponChosen1ClickBegin() {
		
		
		set.position = weaponChosen1.transform.position;
		weaponChosen1.transform.localScale =  new Vector3(1.1f, 1.1f, 1.0f);
		
	}
	
	public void weaponChosen1ClickEnd() {
		
		weaponChosen1.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		
		Inventory.indexWeapon = 0;
		
	}

	public  void weaponChosen2ClickBegin() {
		
		set.position = weaponChosen2.transform.position;
		weaponChosen2.transform.localScale =  new Vector3(1.1f, 1.1f, 1.0f);
		
	}
	
	public void weaponChosen2ClickEnd() {
		
		weaponChosen2.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		
		Inventory.indexWeapon = 1;
		
	}

	public  void playButtonClickBegin() {

		playButton.transform.localScale =  new Vector3(1.1f, 1.1f, 1.0f);

	}

	public void playButtonClickEnd() {

		playButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

		if(Inventory.getWeapons()[0] == null || 
		   Inventory.getWeapons()[1] == null || 
		   Inventory.getClothes() == null){

			Message.sharedMessage().setText("E necessario escolher as armas e o avatar").show();

		}
		else{

			ArrayList _weapon = new ArrayList(Inventory.getWeapons());
			SaveSystem.save("armas_salvas", _weapon);
			SaveSystem.save("roupa_salvas", Inventory.getClothes());

			Director.sharedDirector().LoadLevelWithFade(LevelManager.idSceneToLoad);

		}

	}

	public void backButtonClickBegin() {

		backButton.transform.localScale = new Vector3(1.1f,1.1f,1.0f);

	}

	public void backButtonClickEnd() {

		backButton.transform.localScale = new Vector3(1,1,1);

		Director.sharedDirector().LoadLevelWithFade(Level.menu.GetHashCode());

	}

	public void shopButtonClickBegin() {

		shopButton.transform.localScale = new Vector3(1.1f,1.1f,1.0f);

	}

	public void shopButtonClickEnd() {

		shopButton.transform.localScale = new Vector3 ( 1 , 1, 1);

		LevelManager.lastIdSceneShop = Level.inventory.GetHashCode();

		Director.sharedDirector().LoadLevelWithFade(Level.shop.GetHashCode());
	}

}
