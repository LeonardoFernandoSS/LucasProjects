using UnityEngine;
using System.Collections;

public class EnemyTypeE_flamethrower : EnemyAIBehaviour {

	public float waitTimeFireNear;

	private float currentTimeFireNear;

	public ParticleSystem particle;

	// Use this for initialization
	void Start () 
	{
		base.Start();
		particle.enableEmission = false;
		this.currentTimeFireNear = waitTimeFireNear;
		enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
	}
	
	// Update is called once per frame
	void Update () 
	{ 
		this.enemyLife.CheckDied();
		this.moveDirection = Vector3.zero;
		this.checkCounter();

		if(!characterController.isGrounded && enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.Death)
			enemyAnimation.setState( stateAnim.idle );

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
					enableCoroutine = false;
					StartCoroutine(startFSM(stateAnim.run, Random.Range(2.0f, 3.0f)));
				}

				if(this.distanceToPlayer() <= 4 && this.canAttack.CanAttack == true) 
				{
					if(currentTimeFireNear >= waitTimeFireNear)
					{
						currentTimeFireNear = 0;
						enableCoroutine = true;
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Knock);
					}
					else 
					{
						enableCoroutine = true;
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Retreat);
					}
				}
				else if(this.distanceToPlayer() <= 4) 
				{
					enableCoroutine = true;
					enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
				}

				Move(); // Movimenta em direçao ao Player
				
			}
			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Knock ) 
			{
				if(enableCoroutine)
				{
					StopAllCoroutines();
					StartCoroutine(fire(stateAnim.knock, 2.5f));
					enableCoroutine = false;
				}
			}
			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Stop )
			{

				if(enableCoroutine)
				{
					StopAllCoroutines();
					StartCoroutine(startFSM(stateAnim.idle, Random.Range(1.0f, 2.0f)));
					enableCoroutine = false;
				}

				if(this.distanceToPlayer() <= 4 && this.canAttack.CanAttack == true) 
				{
					if(currentTimeFireNear >= waitTimeFireNear)
					{
						currentTimeFireNear = 0;
						enableCoroutine = true;
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Knock);
					}
					else 
					{
						enableCoroutine = true;
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
					}
				}

				Stop();

			}
			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Retreat )
			{

				if(enableCoroutine)
				{
					StopAllCoroutines();
					StartCoroutine(startFSM(stateAnim.walkingBackwards, Random.Range(1.0f, 2.0f)));
					enableCoroutine = false;
				}
				if(this.distanceToPlayer() <= 4 && this.canAttack.CanAttack == true) 
				{
					if(currentTimeFireNear >= waitTimeFireNear)
					{
						currentTimeFireNear = 0;
						enableCoroutine = true;
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Knock);
					}
					else 
					{
						enableCoroutine = true;
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
					}
				}
				Retreat();
			}
		}
		controlerMove();
	}

#region Coroutine
	IEnumerator startFSM ( stateAnim typeState , float time )
	{

		enemyAnimation.setState(typeState); // Seta a animaçao de Correr 
		
		yield return new WaitForSeconds( time );

		enableCoroutine = true;

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
		else if(this.distanceToPlayer() < 4 && this.canAttack.CanAttack == false && !this.raycastEnemy.getIAmCloseFront())
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

	IEnumerator fire(stateAnim typeState , float time)
	{
		enemyAnimation.setState(stateAnim.knock);

		activeParticle();

		yield return new WaitForSeconds(time);

		desactiveParticle();

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
			this.particle.enableEmission = false;

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
				   enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.SpecialAttack2)
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
	public void activeParticle()
	{
		particle.enableEmission = true;
	}

	public void desactiveParticle()
	{
		particle.enableEmission = false;

		enableCoroutine = true;

		enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);

	}
	
	void checkCounter() 
	{
		if(currentTimeFireNear <= waitTimeFireNear)
			currentTimeFireNear += Time.deltaTime;
	}
#endregion
}
