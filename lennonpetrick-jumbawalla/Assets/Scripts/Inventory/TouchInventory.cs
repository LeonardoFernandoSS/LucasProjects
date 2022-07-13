using UnityEngine;
using System.Collections;

public class TouchInventory : MonoBehaviour {

	public TouchInventoryManager manager;
	public AudioClip audio;

	void Update () {

		if (Input.GetMouseButtonDown(0)){

			RaycastHit _hit;
			
			Ray _ray =  Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(_ray, out _hit, 100.0f)){
			
				if(_hit.transform.name.Equals("WeaponChosen1")) {

					Director.sharedDirector().playEffect(audio);
					manager.weaponChosen1ClickBegin();
					
				}
				else if(_hit.transform.name.Equals("WeaponChosen2")) {

					Director.sharedDirector().playEffect(audio);
					manager.weaponChosen2ClickBegin();
					
				}
				else if(_hit.transform.name.Equals("voltar")) {

					Director.sharedDirector().playEffect(audio);
					manager.backButtonClickBegin();
					
				}
				else if(_hit.transform.name.Equals("playButton")) {

					Director.sharedDirector().playEffect(audio);
					manager.playButtonClickBegin();
					
				}
				else if(_hit.transform.name.Equals("ShopButton")) {

					Director.sharedDirector().playEffect(audio);
					manager.shopButtonClickBegin();
					
				}

			}

		}

		if (Input.GetMouseButtonUp(0)){
			
			RaycastHit _hit;
			
			Ray _ray =  Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(_ray, out _hit, 100.0f)){
				
				if(_hit.transform.name.Equals("WeaponChosen1")) {
					
					manager.weaponChosen1ClickEnd();
					
				}
				else if(_hit.transform.name.Equals("WeaponChosen2")) {
					
					manager.weaponChosen2ClickEnd();
					
				}
				else if(_hit.transform.name.Equals("voltar")) {
					
					manager.backButtonClickEnd();
					
				}
				else if(_hit.transform.name.Equals("playButton")) {
					
					manager.playButtonClickEnd();
					
				}
				else if(_hit.transform.name.Equals("ShopButton")) {
					
					manager.shopButtonClickEnd();
					
				}
				
			}
			
		}

	}

}