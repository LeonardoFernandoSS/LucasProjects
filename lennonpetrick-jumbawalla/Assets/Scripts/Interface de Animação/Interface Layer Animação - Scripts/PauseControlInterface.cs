using UnityEngine;
using System.Collections;

public class PauseControlInterface : MonoBehaviour {

	public Animation pause; // Controle de animaçoes da interfaceLayerPause

	public AudioClip audio;

	private bool animationRunningOut; // Verifica se a animaçao ja começou a executar, para verificar se pode destruir quando terminar a animaçao
	private bool pauseAnimationOutActive; // Ativa a o metodo para executar a animaçao de subir e detroir o objeto


	void Start(){

		ButtonSoundController.checkButtons();

		this.animationRunningOut = false;
		this.pauseAnimationOutActive = false;

	}

	void Update(){

		if(Input.GetMouseButtonDown(0)){

			RaycastHit _hit;
				
			Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				
			if (Physics.Raycast(_ray, out _hit, 100.0f)){

				if(_hit.collider.name == "MENU"){

					CheckPoint.reset();

					Director.sharedDirector().LoadLevelWithFade(LevelManager.getLevelID(Level.menu));

				}
				else if(_hit.collider.name == "RESTART"){

					Director.sharedDirector().restartScene();

				}
				else if(_hit.collider.name == "RESUME"){

					Director.sharedDirector().playEffect(audio);
					Director.sharedDirector().Pause();
					this.pauseAnimationOutActive = true;

				}
				else if(_hit.collider.name == "SOM"){

					Director.sharedDirector().muteAudioBackground();

				}
				else if(_hit.collider.name == "SOMEFEITOS"){

					Director.sharedDirector().muteAudioEffects();

				}

			}

		}	

		this.activeAnimationOut();

	}

	#region ACTIVE ANIMATION OUT - reponsavel por executar a animaçao de subir
	/*
	 * Metodo resposavel por controlar a animaçao de subir e destroir a mesma
	 */
	public void activeAnimationOut(){

		if(pauseAnimationOutActive){

			if(!pause.IsPlaying("pausaOut") && this.animationRunningOut) { 

				Destroy(gameObject);

			}

			this.animationRunningOut = true;
			pause.Play("pausaOut");

		}

	}
	#endregion

	public static bool checkAnimationPause(){

		GameObject _layer0 = GameObject.Find("Layer0");

		if(_layer0)  {

			return _layer0.GetComponent<PauseControlInterface>().pause.IsPlaying("pausaIn");

		}
		else {

			return true;

		}

	}

}
