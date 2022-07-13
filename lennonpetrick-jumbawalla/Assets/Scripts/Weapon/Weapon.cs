using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Item weaponItem;

	public void init(Item item) {

		this.weaponItem = item;


		this.name = this.weaponItem.Id; // to change the identification of item in the scene

		Transform _transformName = transform.FindChild("Name");
		Transform _transformDamage = transform.FindChild("Damage");

		_transformName.gameObject.SetActive(true);
		_transformDamage.gameObject.SetActive(true);

		transform.FindChild("DamageFront").gameObject.SetActive(true);
		transform.FindChild("DamageText").gameObject.SetActive(true);

		_transformName.GetComponent<tk2dTextMesh>().text = this.weaponItem.Name;

		_transformDamage.localScale = new Vector3(this.weaponItem.Damage, _transformDamage.localScale.y, _transformDamage.localScale.z);

		transform.FindChild("Image").GetComponent<SpriteRenderer>()
			.sprite = Resources.Load("Armas/" + this.weaponItem.Image, typeof(Sprite)) as Sprite;

	}

	public float getDamagePercent(){

		return this.weaponItem.Damage;
		
	}

	public void chooseWeapon(){

		bool _hasEqual = false;
		foreach(Weapon weapon in Inventory.getWeapons()){

			if(weapon == this){

				_hasEqual = true;

			}

		}

		if(!_hasEqual){

			Inventory.addWeapon(this);

			GameObject.Find("WeaponChosen" + (Inventory.indexWeapon + 1).ToString()).transform.GetChild(0).GetComponent<SpriteRenderer>()
			.sprite = Resources.Load("Armas/" + this.weaponItem.Image, typeof(Sprite)) as Sprite;

		}

	}

}