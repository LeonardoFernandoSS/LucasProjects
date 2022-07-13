using UnityEngine;
using System.Collections;

public class EnemyTypeC : EnemyAIBehaviour {

	#region Variable de Attack normal
	[SerializeField] 
	private float timeForAttack;

	private float currentTime;
	private bool freeAttack = true; 

	#endregion

	#region Special Attack

	[SerializeField] 
	private float runTimeSpecial;

	private float timeForAttackSpecial, currentTimeSpecialAttack;
	private bool currentSpecialAttack;

	#endregion


	void Start(){   
		
		base.Start();

		this.freeAttack = false;

		this.timeForAttackSpecial = Random.Range(10, 15);

		this.timeForAttack = Random.Range ( 1.5f , 3f);
		
		enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
		
	}
	
	void Update () {

		moveDirection = Vector3.zero;
		// Contador dos Golpes ( Especial / Normal)
		this.countTimeForAttacks();
		// Verifica se poder ativar o golpe epecial
		this.isReadySpecial();

		this.enemyLife.CheckDied();

		if(!characterController.isGrounded && enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.Death)
			enemyAnimation.setState( stateAnim.idle );

		if(characterController.isGrounded)
		{
			if(enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Death) 
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
					StartCoroutine(startFSM ( stateAnim.run , Random.Range(2.0f, 4.0f) ) );
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
					enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Knock);
					enableCoroutine = false;

				}
				else if( !freeAttack )
				{
					StopAllCoroutines();
					enableCoroutine = true;
					randomNumberChooseState = Random.Range (1, 101);
					
					if(randomNumberChooseState >= 1 && randomNumberChooseState <= 40)
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Retreat);
					else
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
					
				}
				Move(); //
			}

			else if (enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Knock ) 
			{
				StartCoroutine(attackEnemy());
			}

			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Stop ) 
			{
				if( enableCoroutine ) 
				{ 
					rotationEnemy();
					StopAllCoroutines();
					StartCoroutine(startFSM(stateAnim.idle  ,Random.Range(1.0f, 2.0f)));
					enableCoroutine = false;
				}

				if(this.canAttack.CanAttack == true && this.freeAttack) 
				{
					StopAllCoroutines();
					enableCoroutine = true;
					enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);
				}

				Stop();
			}
			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Retreat )
			{
				if( enableCoroutine )
				{
					StopAllCoroutines();
					StartCoroutine(startFSM( stateAnim.walkingBackwards, Random.Range(1.0f,2.0f) ));
					enableCoroutine = false;
				}

				if(this.canAttack.CanAttack == true && this.freeAttack) 
				{
					StopAllCoroutines();
					enableCoroutine = true;
					enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);
				}

				Retreat();
			}
			else if (enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.SpecialAttack) 
			{
				if(enableCoroutine) 
				{
					rotationEnemy();
					StartCoroutine(attackSpecial(runTimeSpecial));
					enableCoroutine = false;
				}
				moveDirection = transform.rotation.y == 0 ? new Vector3(-30,0,0) : new Vector3(30,0,0); // Movimentaçao do Inimigo ( Especial )
			}
		}
		controlerMove();
	}

	IEnumerator startFSM ( stateAnim typeState ,float time )
	{
		enemyAnimation.setState(typeState);

		yield return new WaitForSeconds(time);

		enableCoroutine = true;
		randomNumberChooseState = Random.Range(1,101); 

		if( randomNumberChooseState >= 1 && randomNumberChooseState <= 75 )

			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);

		else if ( randomNumberChooseState > 75 )

			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);
	}

	IEnumerator death( ) 
	{
		enemyAnimation.setState(stateAnim.dead);
		yield return null;
	}

	IEnumerator attackEnemy ( ) 
	{
		enemyAnimation.setState(stateAnim.knock);
		yield return null;
	}

	IEnumerator attackSpecial ( float time ) 
	{
		enemyAnimation.setState(stateAnim.knock_two);

		yield return new WaitForSeconds( time );

		enableCoroutine = true;

		currentSpecialAttack = false;
	
		enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);

	}

	void isReadySpecial ()
	{
		if(currentSpecialAttack)
		{
			StopAllCoroutines();

			enableCoroutine = true;

			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.SpecialAttack);

			this.timeForAttackSpecial = Random.Range(5, 15);

			currentSpecialAttack = false;
		}
	}

	void countTimeForAttacks () 
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

		if(!currentSpecialAttack) 
		{
			if( currentTimeSpecialAttack <= timeForAttackSpecial)
				currentTimeSpecialAttack += Time.deltaTime;
		}
		if( currentTimeSpecialAttack >= timeForAttackSpecial ) 
		{
			currentSpecialAttack = true;
			currentTimeSpecialAttack = 0;
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if(enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.Death)
		{
			if(collider.tag == "AttackJumba")
			{
				this.controllerHitSequence.setCollisionEnemy(true);

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
}
