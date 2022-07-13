using UnityEngine;
using System.Collections;

public class EnemyTypeD : EnemyAIBehaviour {
	
	public GameObject grenade;
	
	public Transform positionGrenade;
	
	#region Variable
	
	[SerializeField]  private float timeForAttack;
	
	private float currentTime;
	
	private bool  	freeAttack = true, 
					checkAnimation = false;

	public float forceGrenade;

	#endregion
	
	private Vector3 _direction; // Direçao RayCast
	
	private RaycastHit hit; // hit Raycast
	
	void Start()
	{   
		base.Start();
		enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);
	}
	
	void Update()
	{
		_direction = transform.TransformDirection( Vector3.left );
		
		enemyLife.CheckDied();
		
		this.moveDirection = Vector3.zero;
		
		countTimeForAttack();
		
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
			else if (enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Null)
			{
				StopAllCoroutines();
				enableCoroutine = true;
				enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
			}

			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Move )
			{
				if(enableCoroutine)
				{
					StopAllCoroutines();
					StartCoroutine(startFSM(stateAnim.run ,Random.Range(2.0f,4.0f)));
					enableCoroutine = false;
				}
				if( this.distanceToPlayer() > 7)
				{
					if(this.distanceToPlayer() >= 10 && this.distanceToPlayer() <= 20 && this.freeAttack) 
					{
						StopAllCoroutines();
						enableCoroutine = true;
						enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Knock);
					
					}
					else if(this.distanceToPlayer() < 10 )
					{
						enableCoroutine = true;

						randomNumberChooseState = Random.Range(1, 101);

						if(randomNumberChooseState >= 75)
							enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Retreat);
						else
							enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
					}
				}
				
				else {
					if(this.distanceToPlayer() <= 7) 
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
					StartCoroutine(startFSM(stateAnim.walkingBackwards ,Random.Range(1.0f, 3.0f)));
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

	IEnumerator specialAttack() 
	{
		enemyAnimation.setState(stateAnim.knock_two);

		yield return null;

	}

	IEnumerator startFSM ( stateAnim typeState , float time )
	{
		enemyAnimation.setState(typeState);// Seta a animaçao de Correr 
		yield return new WaitForSeconds( time );

		randomNumberChooseState = Random.Range(1,101);
		enableCoroutine = true;

		if(randomNumberChooseState >= 1 && randomNumberChooseState <= 70) 
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);
		else if(randomNumberChooseState > 70 )
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
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

	void countTimeForAttack () 
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

	public void createGrenade ()
	{
		GameObject _bullet = Instantiate(grenade, positionGrenade.transform.position, Quaternion.identity) as GameObject;
		_bullet.AddComponent<ThrowGrenade>();
		
		//Verifica a posiçao do inimigo para rotacionar a bala
		if(transform.rotation.y == 0)
			_bullet.GetComponent<ThrowGrenade>().addForceGrenade(-forceGrenade,forceGrenade, this.distanceToPlayer());
		else
			_bullet.GetComponent<ThrowGrenade>().addForceGrenade(forceGrenade,forceGrenade, this.distanceToPlayer());
		
		this.freeAttack = false;
		
		enableCoroutine = true;
		
		enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
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
				
				this.randomNumberChooseState = Random.Range(1, 101);
				
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
