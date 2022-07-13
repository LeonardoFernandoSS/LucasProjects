using UnityEngine;
using System.Collections;

public class EnemyEvents : MonoBehaviour {

	public EnemyStateMachine state;

	public GameObject destroy;

	public CreatePoints create;

	public AudioClip[] pickAudio;

	public Transform explosion;

	void chargeStateStop() 
	{
		transform.parent.GetComponent<EnemyAIBehaviour>().enableCoroutineTrue();
		state.setStateMachineEnemy(StateMachineEnemy.Stop);
	}

	void stateCatch()
	{
		transform.parent.GetComponent<EnemyAIBehaviour>().enableCoroutineTrue();
		state.setStateMachineEnemy(StateMachineEnemy.Catch);
	}

	void chargeStateRetreatEndStop() 
	{
		transform.parent.GetComponent<EnemyAIBehaviour>().enableCoroutineTrue();

		float _random = Random.Range(1,100);
		if(_random < 60)
			state.setStateMachineEnemy(StateMachineEnemy.Retreat);
		else 
			state.setStateMachineEnemy(StateMachineEnemy.Stop);
	}

	void chargeStateMoveEndStop() 
	{

		
		float _random = Random.Range(1,100);

		if(_random < 60)
			state.setStateMachineEnemy(StateMachineEnemy.Move);
		else 
			state.setStateMachineEnemy(StateMachineEnemy.Stop);

		transform.parent.GetComponent<EnemyAIBehaviour>().enableCoroutineTrue();
	}

	void playAudio( AudioClip other)
	{
		Director.sharedDirector().playEffect(other);
	}

	public void playAudioRandom()
	{
		Director.sharedDirector().playEffect(pickAudio[ Random.Range(0, pickAudio.Length)]);
	}

	void createGrenade() 
	{
		transform.parent.GetComponent<EnemyTypeD>().createGrenade();
	}

	void createBullet()
	{
		transform.parent.GetComponent<EnemyTypeB>().createBullet();
	}

	void createBulletAir()
	{
		transform.parent.GetComponent<EnemyTypeH_Fly>().createBullet();
	}
	void resetBulletAir()
	{
		transform.parent.GetComponent<EnemyTypeH_Fly>().resetBullet();
	}

	void endLevel() 
	{
		GameObject.Find("EndLevel").GetComponent<EndLevel>().activeEnter();
	}

	void DestroyRemoveList()
	{
		if(transform.parent.GetComponent<EnemyAIBehaviour>().father != null)
		{
			if(transform.parent.GetComponent<EnemyAIBehaviour>().father.GetComponent<InstantiateAmountEnemy>() != null)
			{
				InstantiateAmountEnemy _script = transform.parent.GetComponent<EnemyAIBehaviour>().father.GetComponent<InstantiateAmountEnemy>();
				_script.UpdateState();
			}
			else if(transform.parent.GetComponent<EnemyAIBehaviour>().father.GetComponent<InstantiateEnemyGeneric>() != null) 
			{
				InstantiateEnemyGeneric _script = transform.parent.GetComponent<EnemyAIBehaviour>().father.GetComponent<InstantiateEnemyGeneric>();
				_script.UpdateState();
			}
		}

		EnemyController _scriptController = GameObject.Find("EnemyController").GetComponent<EnemyController>();

		if(_scriptController.enemys.Count >= 1)
			GameObject.Find("EnemyController").GetComponent<EnemyController>().enemys.Remove (destroy);

		Inventory.addEnemyDead(1);
		create.create();
		Destroy(destroy);
	}
	void createExplosion()
	{
		Instantiate ( explosion, transform.position, transform.rotation );
	}
	void chargeRotation()
	{
		float transformFloor = transform.parent.GetComponent<RaycastEnemy>().myFloor.localRotation.z;

		if(transformFloor * 100 >= 10 && transformFloor * 100 <= 180)
		{
			if(transform.parent.localRotation.y == 0)
				transform.parent.Rotate( transform.rotation.x, transform.rotation.y, 22);
			else
				transform.parent.Rotate( transform.rotation.x, transform.rotation.y, -22);
		}
		else if(transformFloor * 100 < -10 && transformFloor * 100 >= -180)
		{
			if(transform.parent.localRotation.y == 0)
				transform.parent.Rotate( transform.rotation.x, transform.rotation.y, -22);
			else
				transform.parent.Rotate( transform.rotation.x, transform.rotation.y, 22);
		}
	}

}
