using UnityEngine;
using System.Collections;

public class RaycastEnemy : MonoBehaviour {

	public Transform myFloor;

	private EnemyStateMachine enemyBehaviour;
	private EnemyAIBehaviour enemyCoroutine;
	private EnemyCanAttack enemyCanAttack;

	private RaycastHit hit;

	public bool iAmCloseFront;
	public bool iAmCloseBack;

	public Transform frontEnemy;
	public Transform backEnemy;

	public float distanceFront;
	public float distanceBack;
	public float distanceJumba;
	public float closedistance;

	void Start() 
	{
		enemyBehaviour = GetComponent<EnemyStateMachine>();
		enemyCoroutine = GetComponent<EnemyAIBehaviour>();
		if(GetComponent<EnemyCanAttack>())
			enemyCanAttack = GetComponent<EnemyCanAttack>();
	}

	void Update(  ) 
	{
		Vector3 _directionFront = transform.TransformDirection(Vector3.left);
		Vector3 _directionBack = transform.TransformDirection(Vector3.right);
		Vector3 _directionDown = transform.TransformDirection(Vector3.down);

		if(Physics.Raycast( transform.position, _directionDown , out hit, 10 ))
		{
			myFloor = hit.transform;
		}

		if (enemyBehaviour.getStateMachineEnemy() != StateMachineEnemy.Stop)
		{

			if(Physics.Raycast(transform.position, _directionFront, out hit, 3)) 
			{
				if( hit.transform.name.Equals("BlockEnemy")) 
				{
					this.front();
				}
			}

			if(Physics.Raycast(transform.position, _directionBack, out hit, 3)) 
			{
				if( hit.transform.name.Equals("BlockEnemy"))
				{
					this.back();
				}
			}
		}

		if(Physics.Raycast(transform.position, _directionFront, out hit, 20)) 
		{
			if(hit.transform.parent)
			{
				if( hit.transform.parent.tag.Equals("Enemy") )
				{
					if(this.transform.name == hit.transform.name)
					{
						frontEnemy = hit.transform;

						if(Mathf.FloorToInt(Vector3.Distance( transform.position, hit.transform.position)/transform.localScale.magnitude) < distanceFront)
						{
							iAmCloseFront = true;

							if(enemyCanAttack != null)
							{
								if(enemyCanAttack.CanAttack == false)
								{
									enemyCoroutine.enableCoroutineTrue();

									this.charge();
								}
							}
						}
						else if(Mathf.FloorToInt(Vector3.Distance( transform.position, hit.transform.position)/transform.localScale.magnitude) >= distanceFront &&
						        Mathf.FloorToInt(Vector3.Distance( transform.position, hit.transform.position)/transform.localScale.magnitude) < 5)
						{	
							enemyCoroutine.enableCoroutineTrue();

							enemyBehaviour.setStateMachineEnemy(StateMachineEnemy.Stop);
						}
					}
				}
				else if(hit.transform.parent.name.Equals("Jumba"))
				{
					if(Mathf.FloorToInt(Vector3.Distance( transform.position, hit.transform.position)/transform.localScale.magnitude) > distanceJumba && enemyCanAttack.CanAttack == false 

					   && enemyBehaviour.getStateMachineEnemy() != StateMachineEnemy.Stop)
					{
						enemyCoroutine.enableCoroutineTrue();
						
						enemyBehaviour.setStateMachineEnemy(StateMachineEnemy.Stop);
					}
				}
			}
		}
		else if(frontEnemy != null)
		{
			if(Mathf.FloorToInt(Vector3.Distance( transform.position, frontEnemy.transform.position)/transform.localScale.magnitude) > distanceFront + 1)
				iAmCloseFront = false;
		}

		if(Physics.Raycast(transform.position, _directionBack, out hit, 20)) 
		{
			if(hit.transform.parent)
			{
				if( hit.transform.parent.tag.Equals("Enemy") )
				{
					if(this.transform.name == hit.transform.name)
					{
						backEnemy = hit.transform;

						if(Mathf.FloorToInt(Vector3.Distance( transform.position, hit.transform.position)/transform.localScale.magnitude) < distanceBack)
						{
							iAmCloseBack = true;
						}
						else {

						}
					}
				}
			}
		}
		else if(backEnemy != null)
		{
			if(Mathf.FloorToInt(Vector3.Distance( transform.position, backEnemy.transform.position)/transform.localScale.magnitude) > distanceBack + 1)
				iAmCloseBack = false;
		}

		if(frontEnemy != null && backEnemy != null)
		{
			if( Mathf.FloorToInt(Vector3.Distance( transform.position, frontEnemy.transform.position)/transform.localScale.magnitude) > distanceFront &&
			   Mathf.FloorToInt(Vector3.Distance( transform.position, backEnemy.transform.position)/transform.localScale.magnitude) < distanceBack &&
			   	enemyCoroutine.distanceToPlayer() > 20)
			{
				if(enemyBehaviour.getStateMachineEnemy() != StateMachineEnemy.Move)
				{	
					enemyCoroutine.enableCoroutineTrue();
					enemyBehaviour.setStateMachineEnemy(StateMachineEnemy.Move);
				}
			}
			   
			else if(Mathf.FloorToInt(Vector3.Distance( transform.position, frontEnemy.transform.position)/transform.localScale.magnitude) < closedistance && backEnemy != frontEnemy)
			{
				if(enemyBehaviour.getStateMachineEnemy() != StateMachineEnemy.Retreat)
				{	
					enemyCoroutine.enableCoroutineTrue();
					enemyBehaviour.setStateMachineEnemy(StateMachineEnemy.Retreat);
				}

			}
			else if(Mathf.FloorToInt(Vector3.Distance( transform.position, backEnemy.transform.position)/transform.localScale.magnitude) < closedistance 
			        && !iAmCloseFront && backEnemy != frontEnemy)
			{
				if(enemyBehaviour.getStateMachineEnemy() != StateMachineEnemy.Move)
				{	
					enemyCoroutine.enableCoroutineTrue();
					enemyBehaviour.setStateMachineEnemy(StateMachineEnemy.Move);
				}
			}
		}
		else if(frontEnemy == null && backEnemy != null) 
		{
			if(Mathf.FloorToInt(Vector3.Distance( transform.position, backEnemy.transform.position)/transform.localScale.magnitude ) < closedistance)
			{
				float _number = Random.Range(1,5);

				if(_number < 2.5f)
				{
					enemyCoroutine.enableCoroutineTrue();
					enemyBehaviour.setStateMachineEnemy(StateMachineEnemy.Move);
				}
				else
				{
					enemyCoroutine.enableCoroutineTrue();
					enemyBehaviour.setStateMachineEnemy(StateMachineEnemy.Stop);
				}
			}
		}
		else if(frontEnemy != null && backEnemy == null) 
		{
			if(Mathf.FloorToInt(Vector3.Distance( transform.position, frontEnemy.transform.position)/transform.localScale.magnitude ) < closedistance)
			{
				float _number = Random.Range(1,5);
				
				if(_number < 2.5f)
				{
					enemyCoroutine.enableCoroutineTrue();
					enemyBehaviour.setStateMachineEnemy(StateMachineEnemy.Retreat);
				}
				else
				{
					enemyCoroutine.enableCoroutineTrue();
					enemyBehaviour.setStateMachineEnemy(StateMachineEnemy.Stop);
				}
			}
		}
	}

	void charge()
	{
		if( iAmCloseBack && iAmCloseFront )
		{
			if(enemyBehaviour.getStateMachineEnemy() != StateMachineEnemy.Stop)
				enemyBehaviour.setStateMachineEnemy(StateMachineEnemy.Stop);
		}
		else if( !iAmCloseBack && iAmCloseFront ) 
		{
			if(enemyBehaviour.getStateMachineEnemy() != StateMachineEnemy.Retreat)
				enemyBehaviour.setStateMachineEnemy(StateMachineEnemy.Retreat);
		}
		else if( !iAmCloseBack && !iAmCloseFront )
		{
			if(enemyBehaviour.getStateMachineEnemy() != StateMachineEnemy.Move)
				enemyBehaviour.setStateMachineEnemy(StateMachineEnemy.Move);
		}
		else if( iAmCloseBack && !iAmCloseFront )
		{
			if(enemyBehaviour.getStateMachineEnemy() != StateMachineEnemy.Move)
				enemyBehaviour.setStateMachineEnemy(StateMachineEnemy.Move);
		}
	}

	void front()
	{
		enemyCoroutine.enableCoroutineTrue();
		enemyBehaviour.setStateMachineEnemy(StateMachineEnemy.Retreat);
	}

	void back()
	{
		int _random = Random.Range(1 , 101);
		enemyCoroutine.enableCoroutineTrue();
		if(_random < 50)
			enemyBehaviour.setStateMachineEnemy(StateMachineEnemy.Move);
		else
			enemyBehaviour.setStateMachineEnemy(StateMachineEnemy.Stop);
	}

	public void setIAmCloseFront( bool value )
	{
		iAmCloseFront = value;
	}

	public bool getIAmCloseFront()
	{
		return iAmCloseFront;
	}

	public void setIAmCloseBack( bool value )
	{
		iAmCloseBack = value;
	}

	public bool getIAmCloseBack ()
	{
		return iAmCloseBack;
	}
}
