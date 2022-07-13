using UnityEngine;
using System.Collections;

public class StoreToothItem : MonoBehaviour {

	public string id;
	
	public void init(string itemId, string price) {
		
		this.id = itemId;
				
		transform.FindChild("TeethValue").GetComponent<TextMesh>().text = price;
				
	}

	// method to buy money in game
	public IEnumerator buyItem(){

		Transform _button = transform.FindChild("BuyTeethButton");

		StoreAudioButton.shared().playAudio();

		_button.localScale = new Vector3(1.05f, 1.05f, 1.0f);
				
		yield return new WaitForSeconds(0.1f);
		
		_button.localScale = new Vector3(1.0f, 1.0f, 1.0f);
						
		//BillingManager.buyItem(this.id);
		
	}

}