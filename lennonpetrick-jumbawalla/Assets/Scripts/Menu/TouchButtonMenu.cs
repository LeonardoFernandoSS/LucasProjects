/* 
 * Versao 1.0
 * 
 * None: MenuController
 * 
 * Descriçao: 	Responsavel por todo o Torque no menu, e tambem responsavel pela detecçao do mouse
 * 				
 * Autor: Mateus R.Culpo
 * 
 * Date: 02/12/2013
 * 
 * Modificado por  	(DD/MM/YYYY)
 * 
 * ########			############	#########
 * 
 */
using UnityEngine;
using System.Collections;

public class TouchButtonMenu : MonoBehaviour {

	MenuManager menu;
	
	void Start () {
		
		this.menu = GetComponent<MenuManager>();

	}
	
	void Update () {
		
		if(Input.touches.Length > 0){
	
			foreach(Touch touch in Input.touches){
					
				RaycastHit _hit;
				
				Ray _ray = GameObject.Find("Main Camera").camera.ScreenPointToRay(touch.position);
		
        		if (Physics.Raycast(_ray, out _hit, 100.0f)){
				
					if(touch.phase == TouchPhase.Began){
						
						if (_hit.collider.name == "PlayButton"){

							StartCoroutine (this.menu.playButtonClicked());
						}
						
						if (_hit.collider.name == "StoreButton"){
							StartCoroutine (this.menu.storeButtonClicked());
						}
						
						if (_hit.collider.name == "OptionsButton"){
							StartCoroutine (this.menu.optionButtonClicked());
						}
						
						if (_hit.collider.name == "CreditsButton"){
							StartCoroutine (this.menu.creditsButtonClicked());
						}

						if(_hit.collider.name == "BackButton"){
							StartCoroutine (this.menu.backButtonClicked(_hit));
						}

						if(_hit.collider.name == "SoundButton"){
							StartCoroutine (this.menu.audioButtonClicked());
						}

						if(_hit.collider.name == "SoundEffectsButton"){
							StartCoroutine (this.menu.effectButtonClicked());
						}

						if(_hit.collider.name == "TutorialButton"){
							StartCoroutine (this.menu.tutorialButtonClicked());
						}
						
						if(_hit.collider.name == "CharacterButton"){
							StartCoroutine (this.menu.characterButtonClicked());
						}

						if(_hit.collider.name == "FaceBookButton"){
							Application.OpenURL("https://www.facebook.com/pages/Neocubo-Design/449435465144148?ref=ts&fref=ts");
						}

						if(_hit.collider.name == "NeocuboButton"){
							Application.OpenURL("http://www.neocubogames.com/");
						}

						if(_hit.collider.name == "CollectionButton") {
							StartCoroutine (this.menu.collectionButtonClicked());
						}
						if(_hit.collider.name == "EndButton"){
							Application.Quit();
						}
					}
				}
			}
		}
		/*
		#region MOUSECONTROL CONTROL IN PAUSE
		if(Input.GetMouseButtonDown(0)){
			
			RaycastHit _hit;
			
			Ray _ray = GameObject.Find("Main Camera").camera.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(_ray, out _hit, 100.0f)){
				
				if(_hit.collider.name == "PlayButton"){
					this.menu.playButtonClickedBegin();
					this.menu.playButtonClickedEnd();
					this.menu.playButtonClicked();
				}
				else if(_hit.collider.name == "StoreButton"){
					this.menu.storeButtonClickedBegin();
					this.menu.storeButtonClickedEnd();
					this.menu.storeButtonClicked();
				}
				else if(_hit.collider.name == "OptionsButton"){
					this.menu.optionButtonClickedBegin();
					this.menu.optionButtonClickedEnd();
					this.menu.optionButtonClicked();
				}
				else if(_hit.collider.name == "CreditsButton"){
					this.menu.optionButtonClickedBegin();
					this.menu.optionButtonClickedEnd();
					this.menu.creditsButtonClicked();
				}
				else if(_hit.collider.name == "BackButton"){
					this.menu.backButtonClickedBegin();
					this.menu.backButtonClickedEnd();
					this.menu.backButtonClicked();
				}
				else if(_hit.collider.name == "TutorialButton"){
					this.menu.tutorialButtonClickedBegin();
					this.menu.tutorialButtonClickedEnd();
					this.menu.tutorialButtonClicked();
				}
				else if(_hit.collider.name == "CharacterButton"){
					this.menu.characterButtonClickedBegin();
					this.menu.characterButtonClickedEnd();
					this.menu.characterButtonClicked();
				}
				else if(_hit.collider.name == "CollectionButton"){
					this.menu.collectionButtonClickedBegin();
					this.menu.collectionButtonClickedEnd();
					this.menu.collectionButtonClicked();
				}
				else if(_hit.collider.name == "FaceBookButton"){
					Application.OpenURL("https://www.facebook.com/pages/Neocubo-Design/449435465144148?ref=ts&fref=ts");
				}
				else if(_hit.collider.name == "NeocuboButton"){
					Application.OpenURL("http://www.neocubo.com.br/");

				}
				else if(_hit.collider.name == "EndButton"){
					this.menu.backButtonClickedBegin();
					this.menu.backButtonClicked();
					// Chama funçao para sair do game 
				}
			}
		}	
		#endregion
		*/
	}
	
}