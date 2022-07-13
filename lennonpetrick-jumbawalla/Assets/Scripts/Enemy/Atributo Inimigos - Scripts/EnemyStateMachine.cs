using UnityEngine;
using System.Collections;

public enum StateMachineEnemy
{
	
	Move,
	Retreat,
	Knock,
	Stop,
	Catch,
	Death,
	SpecialAttack,
	SpecialAttack2,
	Null
	
}

public class EnemyStateMachine : MonoBehaviour {
	
	[SerializeField]private StateMachineEnemy stateMachineEnemy;
	
	
	public void setStateMachineEnemy( StateMachineEnemy stateMachineEnemy) {
		
		this.stateMachineEnemy = stateMachineEnemy;
		
	}
	
	public StateMachineEnemy getStateMachineEnemy (){
		
		return this.stateMachineEnemy;
		
	}
	
	
}
