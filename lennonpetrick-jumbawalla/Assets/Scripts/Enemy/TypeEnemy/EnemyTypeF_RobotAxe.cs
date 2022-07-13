using UnityEngine;
using System.Collections;

public class EnemyTypeF_RobotAxe : EnemyAIBehaviour {

	public EnemylsCollision damageCollision;

	public float waitTimeAttack,
				 waitTimeSpecialAttack;

	private float currentTimeAttack,
				  currentTimeAttackSpecial;
	
	void Start() 
	{
		base.Start();
		this.speedEnemy = 7;
		enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
	}

	void Update() 
	{
		this.moveDirection = Vector3.zero;

		this.enemyLife.CheckDied();

		if(currentTimeAttack <= waitTimeAttack)
			currentTimeAttack += Time.deltaTime;
		if(currentTimeAttackSpecial <= waitTimeSpecialAttack)
			currentTimeAttackSpecial += Time.deltaTime;

		if(!characterController.isGrounded && enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.Death)
			enemyAnimation.setState( stateAnim.idle );
		
		if(characterController.isGrounded)
		{

			if( enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.Move && this.distanceToPlayer() > 30)
			{
				StopAllCoroutines();
				enableCoroutine = true;
				enemyStateMachine.setStateMachineEnemy (StateMachineEnemy.Move);
			}

			if (this.distanceToPlayer() < 6 && currentTimeAttack >= waitTimeAttack && enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.SpecialAttack
			    && this.canAttack.CanAttack == true)
			{
				enableCoroutine = true;
				enemyStateMachine.setStateMachineEnemy( StateMachineEnemy.Knock );
				currentTimeAttack = 0;
			}

			else if(this.distanceToPlayer() < 6 && currentTimeAttackSpecial >= waitTimeSpecialAttack && enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.Knock
			        && this.canAttack.CanAttack == true) 
			{
				enableCoroutine = true;
				enemyStateMachine.setStateMachineEnemy( StateMachineEnemy.SpecialAttack );
				currentTimeAttackSpecial = 0;
			}

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
					StartCoroutine(startFSM(stateAnim.run, Random.Range ( 2.0f ,5.0f ) ));
					enableCoroutine = false;
				}

				if(this.distanceToPlayer() <= 6 && currentTimeAttack <= waitTimeAttack && currentTimeAttackSpecial <= waitTimeSpecialAttack )
				{
					StopAllCoroutines();
					enableCoroutine = true;

					randomNumberChooseState = Random.Range(1, 100);
					if( randomNumberChooseState <= 60 )
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
					else 
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Retreat);
				}

				Move(); // Movimenta em direçao ao Player
			}
			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Knock ) 
			{
				if(enableCoroutine)
				{
					StopAllCoroutines();
					StartCoroutine(knock (stateAnim.knock_two) );
					enableCoroutine = false;

					damageCollision.timeForcePush = 0.2f;
					damageCollision.speedPush = 5;
					damageCollision.speedPushY = 0;
					damageCollision.damagePercent = 3;
				}	
			}
			else if (enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.SpecialAttack)
			{
				if(enableCoroutine)
				{
					StopAllCoroutines();
					StartCoroutine(knock (stateAnim.knock) );

					enableCoroutine = false;

					damageCollision.timeForcePush = 0.5f;
					damageCollision.speedPush = 20;
					damageCollision.speedPushY = 40;
					damageCollision.damagePercent = 7;	
				}
			}
			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Stop ) 
			{
				Stop();
				if(enableCoroutine)
				{
					StopAllCoroutines();
					StartCoroutine(startFSM(stateAnim.idle, Random.Range ( 1.0f ,2.0f ) ) );
					enableCoroutine = false;
				}
				
			}
			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Retreat ) 
			{
				Retreat();

				if(enableCoroutine)
				{
					StopAllCoroutines();
					StartCoroutine(startFSM(stateAnim.walkingBackwards, Random.Range ( 1.0f ,2.0f ) ) );
					enableCoroutine = false;
				}

				if(this.canAttack.CanAttack == true) 
				{
					StopAllCoroutines();
					enableCoroutine = true;
					enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);
				}
			}
		}
		controlerMove();
	}

#region Coroutine
	IEnumerator startFSM ( stateAnim typeState ,float time ) 
	{
		enemyAnimation.setState(typeState);

		yield return new WaitForSeconds(time);
		
		enableCoroutine = true;
		randomNumberChooseState = Random.Range(1,101); 

		if(this.canAttack.CanAttack == true) 
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);
		else
		{
			if( randomNumberChooseState <= 60 )
				enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);
			else if ( randomNumberChooseState > 60 && randomNumberChooseState < 70)
				enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Retreat);
			else 
				enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
		}
	}

	IEnumerator knock ( stateAnim typeState ) 
	{
		enemyAnimation.setState( typeState );

		yield return null;
	}

	IEnumerator death( ) 
	{
		enemyAnimation.setState(stateAnim.dead);
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
}
