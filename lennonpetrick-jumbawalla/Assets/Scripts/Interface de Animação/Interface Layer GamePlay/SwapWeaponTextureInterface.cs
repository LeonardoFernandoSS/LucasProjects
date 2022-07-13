using UnityEngine;
using System.Collections;

public class SwapWeaponTextureInterface : MonoBehaviour {

	public void SwapTexture( string swapWeapon) {

		this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Armas/"+swapWeapon);

	}

	public static SwapWeaponTextureInterface checkTexture() {

		return GameObject.Find("arma01").GetComponent<SwapWeaponTextureInterface>();

	}
}
