using UnityEngine;
using System.Collections;

public class ColliderBosses : MonoBehaviour {

	void OnTriggerEnter ( Collider other )
	{
		if( other.name.Equals("Boss-HULK(Clone)"))
		{
			other.transform.GetComponent<EnemyAnimator>().setState(stateAnim.stun);
			other.transform.GetComponent<EnemyStateMachine>().setStateMachineEnemy(StateMachineEnemy.Catch);
		}
	}
}
