using UnityEngine;
using System.Collections;

public class Item {

	private string id, 
				   name, 
				   descriptionPT, 
				   descriptionEN,
				   image;

	private float damage,
 	 			  price;

	private ItemType type;

	public Item (){
	}

	public Item (string id, string name, 
	             string descriptionPT, string descriptionEN, string image, 
	             float damage, float price, ItemType type){

		this.id = id;
		this.name = name;
		this.descriptionPT = descriptionPT;
		this.descriptionEN = descriptionEN;
		this.image = image;
		this.damage = damage;
		this.price = price;
		this.type = type;

	}
	

	public string Id {
		get {
			return this.id;
		}
		set {
			id = value;
		}
	}

	public string Name {
		get {
			return this.name;
		}
		set {
			name = value;
		}
	}

	public string DescriptionPT {
		get {
			return this.descriptionPT;
		}
		set {
			descriptionPT = value;
		}
	}

	public string DescriptionEN {
		get {
			return this.descriptionEN;
		}
		set {
			descriptionEN = value;
		}
	}

	public string Image {
		get {
			return this.image;
		}
		set {
			image = value;
		}
	}

	public float Damage {
		get {
			return this.damage;
		}
		set {
			damage = value;
		}
	}

	public float Price {
		get {
			return this.price;
		}
		set {
			price = value;
		}
	}

	public ItemType Type {
		get {
			return this.type;
		}
		set {
			type = value;
		}
	}

}