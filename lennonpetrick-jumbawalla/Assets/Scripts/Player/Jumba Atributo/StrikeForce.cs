using UnityEngine;
using System.Collections;

public class StrikeForce : MonoBehaviour {
	/*
	 * Classe recebe a força do golpe do Player e passa para o personagem tirando sua vida dessa maneira.
	 * */
	[SerializeField]private float blowDamage;
	
	public void setBlowDamage(float blowDamage) {this.blowDamage = blowDamage;}

	public float getPowerAttack() {

		float _damage;

		if(GetComponent<ControllerSpecial>().getJumbaSpecial()) 
			_damage = blowDamage + (blowDamage * Inventory.getWeapon().weaponItem.Damage/100) * 3;
		else 
			_damage = blowDamage + (blowDamage * Inventory.getWeapon().weaponItem.Damage/100);

		return _damage ;
	}

	public static StrikeForce checkStrikeForce () {
		return GameObject.Find("Jumba").GetComponent<StrikeForce>();
	}

}
