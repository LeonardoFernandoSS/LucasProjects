using UnityEngine;
using System.Collections;

public class MenuEvents : MonoBehaviour {

	public void load_shop_menus () {
		LevelManager.lastIdSceneShop = Level.menu.GetHashCode();
		Director.sharedDirector().LoadLevel(Level.shop.GetHashCode());
	}

}
