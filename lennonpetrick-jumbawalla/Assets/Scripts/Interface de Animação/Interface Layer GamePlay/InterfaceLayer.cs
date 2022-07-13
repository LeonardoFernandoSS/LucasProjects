using UnityEngine;
using System.Collections;

public class InterfaceLayer : MonoBehaviour {

	public AudioClip audio,
					 button;

	void Update () {

		#region TOUCH INTERFACE_LAYER
		if(Input.touches.Length > 0){

			foreach (Touch touch in Input.touches){

				RaycastHit _hit;
				
				Ray _ray = Camera.main.ScreenPointToRay(touch.position);
				
				if (Physics.Raycast(_ray, out _hit, 100.0f)){
					
					if(touch.phase == TouchPhase.Began){
						
						if (_hit.collider.name == "ButtonPause"){

							Director.sharedDirector().Pause();
							Director.sharedDirector().playEffect(audio);

						}
						
						if (_hit.collider.name == "arma01"){

							Director.sharedDirector().playEffect(button);
							Inventory.changeWeapon();
							SwapTextureInventory.checkSwap().swapWeaponTexture ( Inventory.getWeapon().weaponItem.Image );
							SwapWeaponTextureInterface.checkTexture().SwapTexture ( Inventory.getWeapon().weaponItem.Image );
							
							if(TutorialBehaviour.IndexTutorial == 4)
								TutorialBehaviour.ActiveTutorial = true;
						}
						
					}

				}

			}

		}
		#endregion
		/*
		#region MOUSE CONTROL INTERFACE_LAYER
		if(Input.GetMouseButtonDown(0)){

			Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit _hit;

			if(Physics.Raycast(_ray, out _hit, Mathf.Infinity)){

				if(_hit.collider.name == "ButtonPause"){

					Director.sharedDirector().Pause();
					Director.sharedDirector().playEffect(audio);

				}
				if(_hit.collider.name == "arma01"){

					Director.sharedDirector().playEffect(button);
					Inventory.changeWeapon();
					SwapTextureInventory.checkSwap().swapWeaponTexture ( Inventory.getWeapon().weaponItem.Image );
					SwapWeaponTextureInterface.checkTexture().SwapTexture ( Inventory.getWeapon().weaponItem.Image );
					
					if(TutorialBehaviour.IndexTutorial == 4)
						TutorialBehaviour.ActiveTutorial = true;
	
				}
			}
		}
		#endregion*/

	}
}
