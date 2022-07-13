using UnityEngine;
using System.Collections;

public class EnemyTypeB : EnemyAIBehaviour {

	public GameObject bullet;
	public Transform positionBullet;
	#region Variable

	[SerializeField]  private float timeForAttack;

	private float currentTime;
	private bool freeAttack = true; 

	#endregion
	private Vector3 _direction;
	private RaycastHit hit;

	void Start()
	{   
		base.Start();
		enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);

	}

	void Update()
	{
		_direction = transform.TransformDirection( Vector3.left );

		this.enemyLife.CheckDied();
		this.moveDirection = Vector3.zero;
		this.countTimeForAttack();

		if(!characterController.isGrounded && enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.Death)
		{
			enemyAnimation.setState(stateAnim.idle);
		}

		if(characterController.isGrounded)
		{

			if( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Death )
			{
				if(enableCoroutine) 
				{
					StopAllCoroutines();
					StartCoroutine(death());
					enableCoroutine = false;
				}
			}
			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Move )
			{

				if(enableCoroutine)
				{
					StopAllCoroutines();
					StartCoroutine(startFSM(stateAnim.run ,Random.Range(2.0f,4.0f)));
					enableCoroutine = false;
				}
				if( this.distanceToPlayer() >= 5)
				{
					if(this.distanceToPlayer() <= 20 && this.freeAttack) 
					{
						StopAllCoroutines();
						enableCoroutine = true;
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Knock);
					}
					else if(this.distanceToPlayer() <= 8) 
					{
						enableCoroutine = true;
						randomNumberChooseState = Random.Range(1, 101);
						if(randomNumberChooseState >= 75)
							enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Retreat);
						else
							enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
					}
				}
				
				else 
				{
					if(this.distanceToPlayer() <= 5) 
					{
						StopAllCoroutines();
						enableCoroutine = true;
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.SpecialAttack);
					}

				}

				Move(); // Movimenta em direçao ao Player
			}
			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Knock ) 
			{
				if(enableCoroutine)
				{
					StopAllCoroutines();
					StartCoroutine(attackEnemy());
					enableCoroutine = false;
				}
			}
			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Stop ) 
			{
				if(enableCoroutine)
				{
					StopAllCoroutines();
					StartCoroutine(startFSM( stateAnim.idle , Random.Range(1.0f, 3.0f)));
					enableCoroutine = false;
				}

				Stop();
			}
			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Retreat ) 
			{
				if(enableCoroutine)
				{
					StopAllCoroutines();
					StartCoroutine(startFSM(stateAnim.walkingBackwards ,Random.Range(1.0f, 2.0f)));
					enableCoroutine = false;
				}

				Retreat();
			}
			else if( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.SpecialAttack )
			{
				if(enableCoroutine)
				{
					StopAllCoroutines();
					StartCoroutine(specialAttack());
					enableCoroutine = false;
				}
			}
		}

		controlerMove();
	}

#region Coroutine
	IEnumerator specialAttack()
	{
		enemyAnimation.setState(stateAnim.knock_two);
		yield return null;
	}

	IEnumerator death( ) 
	{
		enemyAnimation.setState(stateAnim.dead);
		yield return null;
	}

	IEnumerator startFSM ( stateAnim typeState , float time )
	{
		enemyAnimation.setState(typeState); // Seta a animaçao de Correr 

		yield return new WaitForSeconds( time );

		randomNumberChooseState = Random.Range(1,101);
		enableCoroutine = true;

		if(randomNumberChooseState >= 1 && randomNumberChooseState <= 70) 

			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);

		else if(randomNumberChooseState > 70 )

			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);

	}
	IEnumerator attackEnemy() 
	{
		enemyAnimation.setState(stateAnim.knock);
		yield return null;
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag == "AttackJumba")
		{
			this.controllerHitSequence.setCollisionEnemy(true);
			/*
			 * O inimigo so vai executar sua rotina de colisao se nao estiver morto
			 * */
			if(enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.Death)
			{
			
				this.controllerHit.AddHitCombo();
				this.interfaceHitCombo.AddHitAnimation();
				this.enemyLife.RemoveLife(strikeForce.getPowerAttack());

				this.enemyAnimation.receiveAttack();

				randomNumberChooseState = Random.Range(1, 101);
				
				if(enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.Knock &&
				   enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.SpecialAttack &&
				   enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.SpecialAttack2 )
				{
					if(randomNumberChooseState < 85)
					{
						StopAllCoroutines();
						enableCoroutine = true;
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
					}
					else
					{
						StopAllCoroutines();
						enableCoroutine = true;
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Retreat);
					}
				}
			}		
		}	
	}
#endregion

#region Methods
	void countTimeForAttack() 
	{
		if(!freeAttack)
		{
			if(currentTime <= timeForAttack)
				currentTime += Time.deltaTime;
		}
		if ( currentTime >= timeForAttack )
		{
			freeAttack = true;
			currentTime = 0;
		}
	}
	public void createBullet() 
	{

		GameObject _bullet = Instantiate(bullet, positionBullet.transform.position, Quaternion.identity) as GameObject;
		_bullet.gameObject.transform.GetComponent<EnemylsCollision>().father = this.gameObject;
		
		//Verifica a posiçao do inimigo para rotacionar a bala
		if(transform.rotation.y == 0)
			_bullet.transform.rotation = new Quaternion ( myTransform.rotation.x, 0, myTransform.rotation.z, myTransform.rotation.w );
		else
			_bullet.transform.rotation = new Quaternion ( myTransform.rotation.x, 180, myTransform.rotation.z, myTransform.rotation.w );
		_bullet.GetComponent<MoveBullet>().setSpeed(-40);
	}
	
	public void resetBullet()
	{
		this.freeAttack = false;
		this.enableCoroutine = true;
		this.randomNumberChooseState = Random.Range(1,101);
		
		if(randomNumberChooseState >= 1 && randomNumberChooseState <= 50)
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);
		else if (randomNumberChooseState > 50)
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
	}
#endregion
}
