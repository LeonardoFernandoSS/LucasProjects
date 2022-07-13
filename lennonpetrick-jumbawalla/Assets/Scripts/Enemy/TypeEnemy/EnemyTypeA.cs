using UnityEngine;
using System.Collections;

public class EnemyTypeA : EnemyAIBehaviour {
	
	private float currentTime;

	[SerializeField]  private float timeForAttack;

	public bool freeAttack; 

	void Start()
	{   
		base.Start();
		this.freeAttack = true;
		this.timeForAttack = 1.5f;
		this.speedEnemy = 12;
	}

	void Update () {

		this.moveDirection = Vector3.zero;
		this.countTimeForAttack();
		this.enemyLife.CheckDied();

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

				if(transform.rotation.y == 0)
					this.moveDirection = new Vector3(3, 0, 0);
				else
					this.moveDirection = new Vector3(-3, 0, 0);
			}
			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Move )
			{

				if(enableCoroutine) 
				{
					StopAllCoroutines();
					StartCoroutine(startFSM(stateAnim.run ,Random.Range(2.0f,4.0f)));
					enableCoroutine = false;
				}

				if(this.distanceToPlayer() < 6 && this.canAttack.CanAttack == false) 
				{
					StopAllCoroutines();

					enableCoroutine = true;

					randomNumberChooseState = Random.Range (1, 101);

					if(randomNumberChooseState >= 1 && randomNumberChooseState <= 40)
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Retreat);
					else
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);

				}
				else if(this.distanceToPlayer() < 4 && this.freeAttack && this.canAttack.CanAttack == true) 
				{
					StopAllCoroutines();
					enableCoroutine = true;
					enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Knock);

				}
				else if(!freeAttack ) 
				{
					StopAllCoroutines();
					enableCoroutine = true;
					randomNumberChooseState = Random.Range (1, 101);
					if(randomNumberChooseState >= 1 && randomNumberChooseState <= 30)
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Retreat);
					else
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
				}

				Move(); // Movimenta em direçao ao Player

			}

			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Knock ) 
			{
				if(this.enableCoroutine) 
				{
					StopAllCoroutines();
					randomNumberChooseState = Random.Range(1,100);
					StartCoroutine(startKnock( randomNumberChooseState < 70 ? stateAnim.knock : stateAnim.knock_two ));
					this.enableCoroutine = false;
				}
			}

			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Stop ) 
			{
				if(enableCoroutine)
				{
					StopAllCoroutines();
					StartCoroutine(startFSM(stateAnim.idle ,Random.Range(1, 2)));
					enableCoroutine = false;
				}

				if(this.distanceToPlayer() < 4 && this.freeAttack && this.canAttack.CanAttack == true) 
				{
					StopAllCoroutines();
					enableCoroutine = true;
					enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Knock);
					
				}

				Stop();	
			}

			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Retreat ) 
			{
				if(enableCoroutine)
				{
					StopAllCoroutines();
					StartCoroutine(startFSM(stateAnim.walkingBackwards , Random.Range(1.5f, 2) ) );
					enableCoroutine = false;
				}

				if(this.distanceToPlayer() < 4 && this.freeAttack && this.canAttack.CanAttack == true) 
				{
					StopAllCoroutines();
					enableCoroutine = true;
					enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Knock);
					
				}

				Retreat();
			}
		}
		controlerMove();
	}

	IEnumerator startFSM (stateAnim typeState,  float time)
	{

		enemyAnimation.setState(typeState);

		yield return new WaitForSeconds(time);

		enableCoroutine = true;
		randomNumberChooseState = Random.Range(1,101);

		if(this.canAttack.CanAttack == true ) 
		{
			if(this.distanceToPlayer() < 4)
				enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
			else
				enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);
		}
		else if( this.canAttack.CanAttack == false && this.raycastEnemy.getIAmCloseFront())
		{
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
		}
		else if(this.distanceToPlayer() < 6 && this.canAttack.CanAttack == false && !this.raycastEnemy.getIAmCloseFront())
		{
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
		}
		else if( this.raycastEnemy.getIAmCloseFront() && this.raycastEnemy.getIAmCloseBack() )
		{
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
		}
		else if(!this.raycastEnemy.getIAmCloseFront() && this.canAttack.CanAttack == false)
		{
			if(randomNumberChooseState < 50)
				enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);
			else
				enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
		}

		else if (!this.raycastEnemy.getIAmCloseFront() && !this.raycastEnemy.getIAmCloseBack())
		{
			if(randomNumberChooseState < 50)
				enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);
			else
				enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
		}
	}

	IEnumerator startKnock( stateAnim type ) 
	{
		enemyAnimation.setState(type);
		freeAttack = false;
		yield return null;
	}

	IEnumerator death( ) 
	{
		enemyAnimation.setState(stateAnim.dead);
		yield return null;
	}

	void countTimeForAttack () 
	{
		if( !freeAttack ) 
		{
			if( currentTime <= timeForAttack )
				currentTime += Time.deltaTime;
		}
		if ( currentTime >= timeForAttack )
		{
			freeAttack = true;
			currentTime = 0;
		}
	}

	void OnTriggerEnter (Collider collider)
	{

		if(collider.tag == "AttackJumba")
		{

			this.controllerHitSequence.setCollisionEnemy(true);
			 //O inimigo so vai executar sua rotina de colisao se nao estiver morto
			if(enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.Death)
			{
				this.controllerHit.AddHitCombo();
				this.interfaceHitCombo.AddHitAnimation();

				// Remove uma quantidade de vida equivalente

				this.enemyLife.RemoveLife(strikeForce.getPowerAttack());
				this.enemyAnimation.receiveAttack();

				randomNumberChooseState = Random.Range(1,100);

				if(enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.Knock &&
				   enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.SpecialAttack &&
				   enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.SpecialAttack2 && this.freeAttack)
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
}

