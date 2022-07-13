using UnityEngine;
using System.Collections;

public abstract class StaticItemsList {

	public static ArrayList getDefault(){

		ArrayList _list = new ArrayList();

		// id, nome, descricao em portugues, descricao em ingles, nomeNaColecao, damage, preco, tipo
		
		#region weapon
		
		Item _item = new Item("id_arma01", "Club", "description in portuguese",  "description in english", "arma01", 0.07f, 0, ItemType.Weapon);
			_list.Add(_item);
			
			_item = new Item("id_arma02", "Bone", "description in portuguese",  "description in english", "arma02", 0.09f, 0, ItemType.Weapon);
			_list.Add(_item);

		#endregion
		
		#region clothes
		
			_item = new Item("id_roupa01", "Roupa01", "description in portuguese",  "description in english", "roupa01", 40, 0, ItemType.Clothes);
			_list.Add(_item);

		#endregion

		return _list;
	}
				
	public static ArrayList get(){

		ArrayList _list = new ArrayList();

		// id, nome, descricao em portugues, descricao em ingles, nomeNaColecao, damage, preco, tipo

		#region weapon

		Item _item = new Item("id_arma03", "Spiky Bone", "description in portuguese",  "description in english", "arma03", 0.1f, 200, ItemType.Weapon);
			_list.Add(_item);
			
			_item = new Item("id_arma04", "Guitar", "description in portuguese",  "description in english", "arma04", 0.2f, 500, ItemType.Weapon);
			_list.Add(_item);

			_item = new Item("id_arma05", "Basebal Bat", "description in portuguese",  "description in english", "arma05", 0.2f, 1000, ItemType.Weapon);
			_list.Add(_item);

			_item = new Item("id_arma07", "Mace", "description in portuguese",  "description in english", "arma07", 0.4f, 1500, ItemType.Weapon);
			_list.Add(_item);

			_item = new Item("id_arma08", "Shotgun", "description in portuguese",  "description in english", "arma08", 0.5f, 2500, ItemType.Weapon);
			_list.Add(_item);

			_item = new Item("id_arma09", "Skillet", "description in portuguese",  "description in english", "arma09", 0.6f, 3000, ItemType.Weapon);
			_list.Add(_item);

			_item = new Item("id_arma10", "Chicken", "description in portuguese",  "description in english", "arma10", 0.7f, 3400, ItemType.Weapon);
			_list.Add(_item);

			_item = new Item("id_arma11", "Machete", "description in portuguese",  "description in english", "arma11", 0.8f, 4200, ItemType.Weapon);
			_list.Add(_item);

			_item = new Item("id_arma12", "Harchet", "description in portuguese",  "description in english", "arma12", 0.85f, 5000, ItemType.Weapon);
			_list.Add(_item);

			_item = new Item("id_arma13", "Zombie", "description in portuguese",  "description in english", "arma13", 0.95f, 10000, ItemType.Weapon);
			_list.Add(_item);

			_item = new Item("id_arma06", "SledgeHammer", "description in portuguese",  "description in english", "arma06", 1f, 25000, ItemType.Weapon);
			_list.Add(_item);

		#endregion

		#region clothes
					
			_item = new Item("id_roupa02", "Roupa02", "description in portuguese",  "description in english", "roupa02", 0, 1000, ItemType.Clothes);
			_list.Add(_item);

			_item = new Item("id_roupa03", "Roupa03", "description in portuguese",  "description in english", "roupa03", 0, 2500, ItemType.Clothes);
			_list.Add(_item);

			_item = new Item("id_roupa04", "Roupa04", "description in portuguese",  "description in english", "roupa04", 0, 10000, ItemType.Clothes);
			_list.Add(_item);

			_item = new Item("id_roupa05", "Roupa05", "description in portuguese",  "description in english", "roupa05", 0, 10000, ItemType.Clothes);
			_list.Add(_item);

			_item = new Item("id_roupa06", "Roupa06", "description in portuguese",  "description in english", "roupa06", 0, 15000, ItemType.Clothes);
			_list.Add(_item);

			_item = new Item("id_roupa07", "Roupa07", "description in portuguese",  "description in english", "roupa07", 0, 40000, ItemType.Clothes);
			_list.Add(_item);

			_item = new Item("id_roupa08", "Roupa08", "description in portuguese",  "description in english", "roupa08", 0, 50000, ItemType.Clothes);
			_list.Add(_item);

		#endregion

		return _list;
	}

}