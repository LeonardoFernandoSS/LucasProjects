using UnityEngine;
using System.Collections;

public class DeadControlInterface : MonoBehaviour {

	public Animation animation;

	void Update(){
								
		if(Input.GetMouseButtonDown(0)){
			
			RaycastHit _hit;
			
			Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(_ray, out _hit, 100.0f)){
				
				if(_hit.collider.name == "MENU"){

					//ADSController.instance.StartCoroutine(ADSController.hideBanner());

					CheckPoint.reset();

					Director.sharedDirector().LoadLevelWithFade(LevelManager.getLevelID(Level.menu));

				}
				else if(_hit.collider.name == "CONTINUE"){

					//ADSController.instance.StartCoroutine(ADSController.hideBanner());

					Director.sharedDirector().restartScene();

				}
			}
		}	

	}

	public static bool checkAnimationDie(){

		GameObject _layer1 = GameObject.Find("Layer1");

		if(_layer1){

			return _layer1.GetComponent<DeadControlInterface>().animation.isPlaying;
		}
		else{
		
			return true;

		}

	}

}
