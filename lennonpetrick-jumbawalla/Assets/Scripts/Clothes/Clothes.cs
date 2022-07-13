using UnityEngine;
using System.Collections;

public class Clothes : MonoBehaviour {

	public Item clothesItem;

	public void init(Item item) {
		
		this.clothesItem = item;

		this.name = this.clothesItem.Id; // to change the identification of item in the scene

		transform.FindChild("Image").GetComponent<SpriteRenderer>().sprite = 
			Resources.Load("Roupas/" + this.clothesItem.Image, typeof(Sprite)) as Sprite;
						
	}

	public void chooseClothes(){
		
		Inventory.changeClothes(this);

		GameObject.Find("ClothesChosen").GetComponent<SpriteRenderer>().sprite = 
			Resources.Load("Roupas/" + this.clothesItem.Image, typeof(Sprite)) as Sprite;
		
	}

}