/* 
 * 
 * 
 * Versao 1.0
 * 
 * None: SubLevelInformation
 * 
 * Descriçao: 	Detector do torque de tela (Android/IOS), tendo funcionalidade para chamar metodo onde cria objetos (subNiveis) e verifica colisoes,
 * 				Detector do mouse em Desktop # # #
 * 				
 * Autor: Mateus R.Culpo
 * 
 * Date: 05/12/2013
 * 
 * Modificado por  	(DD/MM/YYYY)
 * ########			############	#########
 */
using UnityEngine;
using System.Collections;

public class TouchPhases : MonoBehaviour {

	private bool playing;

	private AudioClip audio;

	void Start()
	{
		audio = GameObject.Find("MenuController").GetComponent<MenuManager>().soundButton;
	}

	void Update () {

		#region Torque Control

		// Se nenhum toque for detectado
		if(Input.touches.Length > 0){

			// cria um grupo de instruçoes
			foreach(Touch touch in Input.touches){
				
				RaycastHit _hit; // cria um objeto raycast
				
				Ray _ray = GameObject.Find("Main Camera").camera.ScreenPointToRay(touch.position); // Cria um objeto Ray
				
				if (Physics.Raycast(_ray, out _hit, 100.0f)){ // Quando ouver colisao pelo raycast

					//A finger touched the screen.
					if(touch.phase == TouchPhase.Began){ // se for o toque inicial fazer os demais procedimentos de verificaçao
						
						if(_hit.collider.name == "fase01"){
							StartCoroutine(MenuManager.playLevel(_hit, audio));
						}
						else if(_hit.collider.name == "fase02"){

							StartCoroutine(MenuManager.playLevel(_hit, audio));

						}
						else if(_hit.collider.name == "fase03"){

							StartCoroutine(MenuManager.playLevel(_hit, audio));

						}
						else if(_hit.collider.name == "fase04"){

							StartCoroutine(MenuManager.playLevel(_hit, audio));

						}
						else if(_hit.collider.name == "fase05"){

							StartCoroutine(MenuManager.playLevel(_hit, audio));

						}
						else if(_hit.collider.name == "fase06"){

							StartCoroutine(MenuManager.playLevel(_hit, audio));

						}
						else if(_hit.collider.name == "fase07"){

							StartCoroutine(MenuManager.playLevel(_hit, audio));

						}

						else if(_hit.collider.name == "fase08"){

							StartCoroutine(MenuManager.playLevel(_hit, audio));

						}
						else if(_hit.collider.name == "fase09"){

							StartCoroutine(MenuManager.playLevel(_hit, audio));

						}
						else if(_hit.collider.name == "fase10"){

							StartCoroutine(MenuManager.playLevel(_hit, audio));
									
						}
						else if(_hit.collider.name == "fase11"){

							StartCoroutine(MenuManager.playLevel(_hit, audio));

						}
						else if (_hit.collider.name == "SubFase1"){

							StartCoroutine(MenuManager.playSubLevel(_hit, audio));

						}
						else if (_hit.collider.name == "SubFase2"){

							StartCoroutine(MenuManager.playSubLevel(_hit, audio));

						}
						else if (_hit.collider.name == "SubFase3"){

							StartCoroutine(MenuManager.playSubLevel(_hit, audio));

						}
						else if(_hit.collider.name == "backButtonLevel"){

							Director.sharedDirector().playEffect( audio );
							GameObject.Find("stage").SetActive(false); 
							
						}
						
					}
		
				}
				
			}
			
		}

		#endregion
		/*
		#region MOUSECONTROL CONTROL IN PAUSE
		if(Input.GetMouseButtonDown(0)){
			
			RaycastHit _hit;
			
			Ray _ray = GameObject.Find("Main Camera").camera.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(_ray, out _hit, 100.0f)){
				
				if(_hit.collider.name == "fase01"){
					LevelInformation _level = _hit.collider.GetComponent<LevelInformation>();
					_level.buttonClickEnter(_level);
				}
				else if(_hit.collider.name == "fase02"){
					LevelInformation _level = _hit.collider.GetComponent<LevelInformation>();
					_level.buttonClickEnter(_level);
				}
				else if(_hit.collider.name == "fase03"){
					LevelInformation _level = _hit.collider.GetComponent<LevelInformation>();
					_level.buttonClickEnter(_level);
				}
				else if(_hit.collider.name == "fase04"){
					LevelInformation _level = _hit.collider.GetComponent<LevelInformation>();
					_level.buttonClickEnter(_level);
				}
				else if(_hit.collider.name == "fase05"){
					LevelInformation _level = _hit.collider.GetComponent<LevelInformation>();
					_level.buttonClickEnter(_level);
				}
				else if(_hit.collider.name == "fase06"){
					LevelInformation _level = _hit.collider.GetComponent<LevelInformation>();
					_level.buttonClickEnter(_level);
				}
				else if(_hit.collider.name == "fase07"){
					LevelInformation _level = _hit.collider.GetComponent<LevelInformation>();
					_level.buttonClickEnter(_level);
				}
				else if(_hit.collider.name == "fase08"){
					LevelInformation _level = _hit.collider.GetComponent<LevelInformation>();
					_level.buttonClickEnter(_level);
				}
				else if(_hit.collider.name == "fase09"){
					LevelInformation _level = _hit.collider.GetComponent<LevelInformation>();
					_level.buttonClickEnter(_level);
				}
				else if(_hit.collider.name == "fase10"){
					LevelInformation _level = _hit.collider.GetComponent<LevelInformation>();
					_level.buttonClickEnter(_level);		
				}
				else if(_hit.collider.name == "fase11"){
					LevelInformation _level = _hit.collider.GetComponent<LevelInformation>();
					_level.buttonClickEnter(_level);
				}
				else if(_hit.collider.name == "backButtonLevel"){

					GameObject _scene = GameObject.Find("sceneSubLevel"); // Procura o Objeto criado 
					Destroy(_scene); // destroy aquele objeto

				}
			}
		}	
		#endregion*/
	}
}
