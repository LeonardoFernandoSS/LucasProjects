using UnityEngine;
using System.Collections;

public class EnemyLife : MonoBehaviour {

	public float life;

	private EnemyStateMachine enemyMachine;

	void Start() 
	{
		this.enemyMachine = GetComponent<EnemyStateMachine>();
	}

	public void RemoveLife(float damage)
	{
		this.life -= damage;
	}

	public void CheckDied()
	{

		if(this.life <= 0 ) {

			transform.GetComponent<EnemyAIBehaviour>().enableCoroutineTrue();
			enemyMachine.setStateMachineEnemy(StateMachineEnemy.Death);

		}
	}
}
