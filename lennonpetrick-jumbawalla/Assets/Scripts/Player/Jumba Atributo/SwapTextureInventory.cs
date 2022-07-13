using UnityEngine;
using System.Collections;
using SmoothMoves;
public class SwapTextureInventory : MonoBehaviour {

	[ SerializeField ]private BoneAnimation boneAnimation;
	[ SerializeField ]private Texture[] arraySetTexture;
	[ SerializeField ]private Material currentMaterial;

	private string currentWeapon , loadingWeapon;

	void Start() {

		currentWeapon = "purrete";

		/*
		////////////////////////////
		Inventory.init();

		Item novaRopa = new Item("1", "nome", "ad", "ds", "roupa01", 100, 123, ItemType.Clothes);
		Item arma1 = new Item("1", "nome", "ad", "ds", "arma04", 3, 123, ItemType.Weapon);
		Item arma2 = new Item("2", "nome", "ad", "de", "arma05", 3, 123, ItemType.Weapon);


		Clothes c = new Clothes();
		Weapon a1 = new Weapon();
		Weapon a2 = new Weapon();

		a1.init( arma1 );
		a2.init( arma2 );
		c.init ( novaRopa );

		Inventory.changeClothes ( c );
		Inventory.addWeapon( a1 );
		Inventory.addWeapon( a2 );

		////////////////////////////
		*/
		swapSetTexture ( Inventory.getClothes().clothesItem.Image);
		swapWeaponTexture (Inventory.getWeapon().weaponItem.Image);
		SwapWeaponTextureInterface.checkTexture().SwapTexture(Inventory.getWeapon().weaponItem.Image);


	}

	public void swapWeaponTexture( string swapWeapon)
	{

		boneAnimation.SwapTexture("armas", currentWeapon , "armas", swapWeapon);

	}

	public void swapSetTexture ( string nameTexture ) {

		switch ( nameTexture) {

			case "roupa01": {
				
				currentMaterial.mainTexture = arraySetTexture[0];
				break;
			}
			case "roupa02": {

				currentMaterial.mainTexture = arraySetTexture[1];
				break;
			}
			case "roupa03": {
				
				currentMaterial.mainTexture = arraySetTexture[2];
				break;
			}
			case "roupa04": {
				
				currentMaterial.mainTexture = arraySetTexture[3];
				break;
			}
			case "roupa05": {
				
				currentMaterial.mainTexture = arraySetTexture[4];
				break;
			}
			case "roupa06": {
				
				currentMaterial.mainTexture = arraySetTexture[5];
				break;
			}
			case "roupa07": {
				
				currentMaterial.mainTexture = arraySetTexture[6];
				break;
			}
			case "roupa08": {
				
				currentMaterial.mainTexture = arraySetTexture[7];
				break;
			}
			case "roupa09": {
				
				currentMaterial.mainTexture = arraySetTexture[8];
				break;
			}

		}
	}

	public static SwapTextureInventory checkSwap() {
		return GameObject.Find("jumbaAnimate").GetComponent<SwapTextureInventory>();
	}

}
