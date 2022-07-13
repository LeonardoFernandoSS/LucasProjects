using UnityEngine;
using System.Collections;

public enum EnemyType { 

	InimigoA_RoboStrike, 
	InimigoB_RoboAtira, 
	InimigoC_Ogro,  
	InimigoD_RoboScudo,
	InimigoE_RoboFOGO,
	InimigoF_Martelinho,
	InimigoG_Sniper,
	InimigoH_Voador,
	Chefao1,
	Chefao2,
	Chefao3,
	Chefao4,
	Chefao5,
	Dino01,
	Dino02,
	Dino03,
	Dino04MoveFly,
	AirCraftBomb,
	AirCraftFire

}

public class EnumEnemyList : MonoBehaviour {

	[SerializeField]private EnemyType  enemyType;

	public EnemyType EnemyType {

		get {
			return this.enemyType;
		}
		set {
			this.enemyType = value;
		}
	}

}