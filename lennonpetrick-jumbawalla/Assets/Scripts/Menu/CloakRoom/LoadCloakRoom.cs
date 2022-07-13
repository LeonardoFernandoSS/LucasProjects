using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadCloakRoom : MonoBehaviour {

	public tk2dUILayout prefabWeaponItem, 
						prefabClothesItem;

	public tk2dUIScrollableArea scrollableWeaponArea, 
								scrollableClothesArea;

	private float xWeapon,
				  xClothes,
				  wWeapon,
				  wClothes;

	private List<Weapon> _w = new List<Weapon>();
	
	void Start () 
	{
		Inventory.init();

		this.loadItems();

		if(!SaveSystem.hasObject("armas_salvas") && !SaveSystem.hasObject("roupa_salvas"))
		{
			Inventory.addWeapon(GameObject.Find("id_arma01").GetComponent<Weapon>(), 0);
			Inventory.addWeapon(GameObject.Find("id_arma02").GetComponent<Weapon>(), 1);
			Inventory.changeClothes(GameObject.Find("id_roupa01").GetComponent<Clothes>());
		}
		else
		{
			#region Load weapons and clothes
			ArrayList _weapons = (ArrayList)SaveSystem.load("armas_salvas", typeof(ArrayList));
			Clothes _clothes = (Clothes)SaveSystem.load("roupa_salvas", typeof(Clothes));
			#endregion

			#region Add Weapons e Clothes to Inventory
			foreach( Weapon weapon in _weapons)
			{
				_w.Add(GameObject.Find(weapon.weaponItem.Id).GetComponent<Weapon>());
			}

			_clothes = GameObject.Find(_clothes.clothesItem.Id).GetComponent<Clothes>();

			Inventory.addWeapon(this._w[0], 0);
			Inventory.addWeapon(this._w[1], 1);
			Inventory.changeClothes(_clothes);
			#endregion

			#region ChargeSprites
			GameObject.Find("WeaponChosen1").transform.GetChild(0).GetComponent<SpriteRenderer>()
				.sprite = Resources.Load("Armas/" + _w[0].weaponItem.Image, typeof(Sprite)) as Sprite;
			GameObject.Find("WeaponChosen2").transform.GetChild(0).GetComponent<SpriteRenderer>()
				.sprite = Resources.Load("Armas/" + _w[1].weaponItem.Image, typeof(Sprite)) as Sprite;
			GameObject.Find("ClothesChosen").GetComponent<SpriteRenderer>().sprite = 
				Resources.Load("Roupas/" + _clothes.clothesItem.Image, typeof(Sprite)) as Sprite;
			#endregion
		}
	}

	public void loadItems(){

		// start setting the beginning position of the objects and the width of them
		xWeapon = 0.85f; // position X that the weapon will instantiate
		xClothes = 0.85f; // position X that the clothe will instantiate
		wWeapon = (prefabWeaponItem.GetMaxBounds() - prefabWeaponItem.GetMinBounds()).x; // width of the weapon object
		wClothes = (prefabClothesItem.GetMaxBounds() - prefabClothesItem.GetMinBounds()).x; // width of the clothe object

		// add default items
		foreach(Item defaultItem in StaticItemsList.getDefault()){

			addItem(defaultItem);

		}

		// check if has objects saved
		if(SaveSystem.hasObject("CloakRoom_List")){

			// if yes, then gets them
			ArrayList _itemsList = (ArrayList) SaveSystem.load("CloakRoom_List", typeof(ArrayList));

			// pass through list and add which item
			foreach(Item item in _itemsList) {
				
				addItem(item);
				
			}
						
		}

		// add the empty object in the final
		this.addLastWeapon();
		this.addLastClothes();

		// the ContentLength receive the length of list
		scrollableWeaponArea.ContentLength = xWeapon;
		scrollableClothesArea.ContentLength = xClothes;
		
	}

	// method that check what is the type and add in the rigth method
	private void addItem(Item item){

		if(item.Type == ItemType.Weapon){
			
			// fills with weapon
			this.addWeapon(item);
			
		}
		else{
			
			// fills with clothe
			this.addClothes(item);
			
		}

	}

#region Weapon

	private void addWeapon(Item item){

		// instantiates the prefabWeaponItem
		tk2dUILayout _layout = Instantiate(prefabWeaponItem) as tk2dUILayout;

		// changes the parent
		_layout.transform.parent = scrollableWeaponArea.contentContainer.transform;

		// and changes the position
		_layout.transform.localPosition = new Vector3(xWeapon, 0, 0);

		// fills the weapon informations
		_layout.gameObject.AddComponent<Weapon>().init(item);

		// gets the tk2dUIItem Component to set some informations
		tk2dUIItem _uiItem = _layout.gameObject.AddComponent<tk2dUIItem>();

		// set message target
		_uiItem.sendMessageTarget = _layout.gameObject;

		// set the method that will call
		_uiItem.SendMessageOnClickMethodName = "chooseWeapon";

		// set this informations
		_uiItem.InternalSetIsChildOfAnotherUIItem(true);		
		_uiItem.isHoverEnabled = true;

		// sum the position with the width to other item
		xWeapon += wWeapon;

	}

	private void addLastWeapon(){

		// instantiates the prefabWeaponItem
		tk2dUILayout _lastLayout = Instantiate(prefabWeaponItem) as tk2dUILayout;

		// changes the parent
		_lastLayout.transform.parent = scrollableWeaponArea.contentContainer.transform;

		// and changes the position
		_lastLayout.transform.localPosition = new Vector3(xWeapon, 0, 0);

		// sum the position with the width to other item
		xWeapon += wWeapon;

	}

#endregion Weapon

#region Clothes

	private void addClothes(Item item){

		// instantiates the prefabClothesItem
		tk2dUILayout _layout = Instantiate(prefabClothesItem) as tk2dUILayout;

		// changes the parent
		_layout.transform.parent = scrollableClothesArea.contentContainer.transform;

		// and changes the position
		_layout.transform.localPosition = new Vector3(xClothes, 0, 0);

		// fills the clothe informations
		_layout.gameObject.AddComponent<Clothes>().init(item);

		// gets the tk2dUIItem Component to set some informations
		tk2dUIItem _uiItem = _layout.gameObject.AddComponent<tk2dUIItem>();

		// set message target
		_uiItem.sendMessageTarget = _layout.gameObject;

		// set the method that will call
		_uiItem.SendMessageOnClickMethodName = "chooseClothes";

		// set this informations
		_uiItem.InternalSetIsChildOfAnotherUIItem(true);		
		_uiItem.isHoverEnabled = true;

		// sum the position with the width to other item
		xClothes += wClothes;

	}
	
	private void addLastClothes(){

		// instantiates the prefabWeaponItem
		tk2dUILayout _lastLayout = Instantiate(prefabClothesItem) as tk2dUILayout;

		// changes the parent
		_lastLayout.transform.parent = scrollableClothesArea.contentContainer.transform;

		// and changes the position
		_lastLayout.transform.localPosition = new Vector3(xClothes, 0, 0);

		// sum the position with the width to other item
		xClothes += wClothes;

	}

#endregion Clothes

}
