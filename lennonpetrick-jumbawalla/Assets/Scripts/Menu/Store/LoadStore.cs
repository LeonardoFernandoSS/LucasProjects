using UnityEngine;
using System.Collections;

public class LoadStore : MonoBehaviour {

	public tk2dUILayout prefabItem;
	public TextMesh teethAmountText;		
	public tk2dUIScrollableArea scrollableArea;

	void Start () {

		this.loadStoreItems();
		
		this.loadTeethAmount(); // load money in the game

		//BillingManager.getItems();

	}

	public static LoadStore shareLoadStore(){

		return Camera.main.GetComponent<LoadStore>();

	}

	public void loadTeethAmount() {
					
		// gets money saved
		teethAmountText.text = MoneyTeeth.getAmount().ToString();

	}

	public void loadStoreItems() {

		// get items list to buy
		ArrayList _staticItemsList = StaticItemsList.get();

		// used to load the items list bought
		ArrayList _cloakRoomList = new ArrayList();

		// check if there are objects
		if(SaveSystem.hasObject("CloakRoom_List")){

			// if there are, load them
			_cloakRoomList = (ArrayList) SaveSystem.load("CloakRoom_List", typeof(ArrayList));

		}

		float _y = 0.0f; // position Y that the item will instantiate
		float _h = (prefabItem.GetMaxBounds() - prefabItem.GetMinBounds()).y; // height of the item object

		// pass every item of list
		foreach(Item item in _staticItemsList) {

			// it compares if this item was already bought
			if(!compareList(_cloakRoomList, item)){

				// instantiates the prefabItem
				tk2dUILayout _layout = Instantiate(prefabItem) as tk2dUILayout;

				// changes the parent
				_layout.transform.parent = scrollableArea.contentContainer.transform;

				// and changes the position
				_layout.transform.localPosition = new Vector3(0, _y, 0);

				// fills the store item informations
				_layout.gameObject.AddComponent<StoreItem>().init(item);

				// add and get the tk2dUIItem Component to set some informations
				tk2dUIItem _uiItem = _layout.transform.FindChild("BlackSquare").FindChild("BuyButton").gameObject.AddComponent<tk2dUIItem>();

				// set message target
				_uiItem.sendMessageTarget = _layout.gameObject;		

				// set the method that will call
				_uiItem.SendMessageOnClickMethodName = "buyItem";

				// set this informations
				_uiItem.InternalSetIsChildOfAnotherUIItem(true);
				_uiItem.isHoverEnabled = true;

				// sum the position with the heigth to other item
				_y -= _h;
			
			}

		}

		// the ContentLength receive the heigth of list
		scrollableArea.ContentLength = Mathf.Abs(_y);

	}

	// method to destroy all objects
	public void clearStoreItems(){

		foreach(StoreItem child in scrollableArea.contentContainer.GetComponentsInChildren<StoreItem>()) {

			Destroy(child.gameObject);
			
		}

	}

	// method to compare the item with items of list
	private bool compareList(ArrayList list, Item item){

		foreach(Item it in list) {

			if (it.Id == item.Id)
				return true;
		
		}

		return false;
	}

}