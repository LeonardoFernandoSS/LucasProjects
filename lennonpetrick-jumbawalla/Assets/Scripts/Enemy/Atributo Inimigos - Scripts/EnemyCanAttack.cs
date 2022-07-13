using UnityEngine;
using System.Collections;

public class EnemyCanAttack : MonoBehaviour {

	private bool canAttack;

	public bool CanAttack 
	{
		get 
		{
			return this.canAttack;
		}
		set 
		{
			canAttack = value;
		}
	}
}
