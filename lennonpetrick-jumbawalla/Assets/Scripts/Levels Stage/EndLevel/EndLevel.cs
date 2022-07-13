using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {

	public GameObject focusAnimation; // prefab que tem animaçao foco

	private bool callDirector = false, 
				 aFocus = false; // Verifica se pode ser instanciado o prefabs do foco

	private CharacterController player; // Charachter controler do player

	void Update(){
		// caso o player nao seja nulo ele entra dentro dessa condiçao
		if(player != null){

			// se o player estiver no chao
			if(player.isGrounded){

				//Pega o componente MovimentoControlKeyboard e desabilita a Script
				MovimentControl _player = GameObject.Find("Jumba").GetComponent<MovimentControl>();

				_player.enabled = false;

				if(!callDirector){

					Director.sharedDirector().endScene(Director.SceneEndedStatus.won);

					this.callDirector = true;

				}

				// se animaçao de Focus for igual a "true" instancia o prefab da animaçao
				if(this.aFocus){

					GameObject _focus = Instantiate(focusAnimation,focusAnimation.transform.position, Quaternion.identity) as GameObject;
					
					_focus.transform.parent = GameObject.Find("Main Camera").transform;
					
					_focus.transform.localPosition = focusAnimation.transform.position;

					this.aFocus = false;

				}
			}
		}
	}

	void OnTriggerEnter(Collider other){

		if(other.transform.name == "Jumba"){

			this.activeEnter();

		}
	}

	public void activeEnter() {

		this.player = GameObject.Find("Jumba").GetComponent<CharacterController>();
		
		this.aFocus = true;

	}
}
