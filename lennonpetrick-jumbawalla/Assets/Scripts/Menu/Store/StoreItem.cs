using UnityEngine;
using System.Collections;

public class StoreItem : MonoBehaviour {
	
	public Item storeItem;
	
	public void init(Item item) {
		
		this.storeItem = item;
		
		this.name = this.storeItem.Id; // to change the identification of store item in the scene

		//transform.FindChild("BlackSquare").FindChild("BuyButton").GetChild(0).GetComponent<tk2dTextMesh>().text = method;

		transform.FindChild("WhiteSquare").FindChild("Name").GetComponent<tk2dTextMesh>().text = this.storeItem.Name;
		transform.FindChild("WhiteSquare").FindChild("Description").GetComponent<tk2dTextMesh>().text = 
			CurrentLanguage.language == LanguageType.Portuguese ? this.storeItem.DescriptionPT : this.storeItem.DescriptionEN;

		string _resourcesPath;

		// check what is the type, if it is weapon then active the damageBar 
		// and set the resources path, else only set the resources path to clothes path
		if(storeItem.Type == ItemType.Weapon){
		
			transform.FindChild("WhiteSquare").FindChild("Damage").gameObject.SetActive(true);

			Transform _transformDamage = transform.FindChild("WhiteSquare").FindChild("Damage").FindChild("DamageBar");
			_transformDamage.localScale = new Vector3(this.storeItem.Damage, _transformDamage.localScale.y, _transformDamage.localScale.z);

			_resourcesPath = "Armas/";
 	
		}
		else{

			_resourcesPath = "Roupas/";

		}

		transform.FindChild("WhiteSquare").FindChild("Image").GetComponent<SpriteRenderer>()
			.sprite = Resources.Load(_resourcesPath + this.storeItem.Image, typeof(Sprite)) as Sprite;

		transform.FindChild("BlackSquare").FindChild("Price").GetComponent<tk2dTextMesh>().text = "$ " + this.storeItem.Price;
		
	}
		
	// method to buy item of the game
	public IEnumerator buyItem(){
	
		// find buyButton gameObject to change the scale
		Transform _button = transform.FindChild("BlackSquare").FindChild("BuyButton");

		StoreAudioButton.shared().playAudio();

		_button.localScale = new Vector3(1.05f, 1.05f, 1.0f);

		// wait some seconds to change back the scale
		yield return new WaitForSeconds(0.1f);
		
		_button.localScale = new Vector3(1.0f, 1.0f, 1.0f);

		// removes the price from money manager if the money is enough
		if(MoneyTeeth.removeTeeth(storeItem.Price)){

			// load new teeth amount
			LoadStore.shareLoadStore().loadTeethAmount();

			// to receive the items bought
			ArrayList _cloakRoomList = new ArrayList();

			// check if there are
			if(SaveSystem.hasObject("CloakRoom_List")){
				
				_cloakRoomList = (ArrayList) SaveSystem.load("CloakRoom_List", typeof(ArrayList));
				
			}

			// add the new item bought
			_cloakRoomList.Add(storeItem);

			// save the new list
			SaveSystem.save("CloakRoom_List", _cloakRoomList);

			// remove all objects in the scene
			LoadStore.shareLoadStore().clearStoreItems();

			// and load again
			LoadStore.shareLoadStore().loadStoreItems();

		}
		else{ // if the money isn't enough, show message

			Message.sharedMessage().setText("Dente insuficiente").show();

		}

	}
	
}