using UnityEngine;
using System.Collections;

public class StoreMenuManager : MonoBehaviour {

	public Animator moreTeethAnimation;

	public Transform backStoreButton;

	// About more teeth button
	public void moreTeethButtonClicked(){
	
		this.moreTeethAnimation.SetBool("AtivaCompra", true);

	}

	// About back store button
	public void backStoreButtonClicked(){

		backStoreButton.localScale = new Vector3(0.9f, 0.9f, 1.0f);

		if(LevelManager.lastIdSceneShop.Equals(Level.inventory.GetHashCode())) 
			Director.sharedDirector().LoadLevelWithFade(Level.inventory.GetHashCode());
	
		else  Director.sharedDirector().LoadLevelWithFade(Level.menu.GetHashCode());
		

	}
	
	public void backStoreButtonClickedEnd(){

		backStoreButton.localScale = new Vector3(0.8f, 0.8f, 1.0f);
		
	}

	// About back buy teeth button
	public void backBuyTeethButtonClicked(){

		this.moreTeethAnimation.SetBool("AtivaCompra", false);
		
	}

}