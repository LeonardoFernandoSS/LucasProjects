using UnityEngine;
using System.Collections;

public class TouchButtonStore : MonoBehaviour {

	private StoreMenuManager storeMenuManager;
	public AudioClip button;

	void Start () {

		this.storeMenuManager = GetComponent<StoreMenuManager>();
	
	}

	void Update () {

		if(Input.GetMouseButtonDown(0)){
			
			RaycastHit _hit;
			
			Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(_ray, out _hit, 100.0f)){
				
				if(_hit.collider.name == "MoreTeethButton"){

					Director.sharedDirector().playEffect(button);
					this.storeMenuManager.moreTeethButtonClicked();

				}
				else if(_hit.collider.name == "BackStoreButton"){
					Director.sharedDirector().playEffect(button);
					this.storeMenuManager.backStoreButtonClicked();

				}
				else if(_hit.collider.name == "BackBuyTeethButton"){
					Director.sharedDirector().playEffect(button);
					this.storeMenuManager.backBuyTeethButtonClicked();
					
				}

			}

		}

		if(Input.GetMouseButtonUp(0)){
			
			RaycastHit _hit;
			
			Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(_ray, out _hit, 100.0f)){
				
				if(_hit.collider.name == "BackStoreButton"){
					
					this.storeMenuManager.backStoreButtonClickedEnd();
					
				}
				
			}
			
		}
	
	}

}