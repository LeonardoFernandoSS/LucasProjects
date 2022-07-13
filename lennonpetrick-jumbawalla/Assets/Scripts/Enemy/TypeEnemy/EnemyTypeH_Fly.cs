using UnityEngine;
using System.Collections;

public class EnemyTypeH_Fly : EnemyAIBehaviour {

	public GameObject bullet;
	public Transform positionBullet;

	public float adjustedDis;

	private float maxDistanceFly,
				  minDistaceFly;

	private float speedY;

	private float countTimeShoot,
				  waitTimeShoot;

	public Transform weapon;
	public Transform explosion;

	private Quaternion weaponRotate;

	void Start(  ) 
	{
		base.Start();

		this.countTimeShoot = 0;
		this.waitTimeShoot = 5;
		this.speedEnemy = 25;
		this.speedY = 18;
		this.gravity = 0;

		this.maxDistanceFly = transform.localPosition.x + adjustedDis;
		this.minDistaceFly = transform.localPosition.y - adjustedDis;
		enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);
	}

	void Update()
	{
		this.moveDirection = Vector3.zero;

		enemyLife.CheckDied();

		if(countTimeShoot >= waitTimeShoot && this.distanceToPlayer() <= 30) 
		{
			enableCoroutine = true;
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Knock);
			countTimeShoot = 0;
		}


		if(countTimeShoot <= waitTimeShoot)
			countTimeShoot += Time.deltaTime;


		if( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Death ) 
		{
			if(enableCoroutine) 
			{
				StopAllCoroutines();
				StartCoroutine(death());
				enableCoroutine = false;
			}

			this.gravity = 1600;

			if(characterController.isGrounded)
			{
				enemyAnimation.anim.SetTrigger("morreu");
			}
		}

		else if(enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Move)
		{
			this.speedY = 0;

			if ( enableCoroutine )
			{
				rotationEnemy();
				StopAllCoroutines();
				StartCoroutine(startFSM(stateAnim.run, Random.Range(2, 3)));
				this.enableCoroutine = false;
			}

			if(transform.position.y <= target.transform.position.y + 15) {
				this.speedY = 10;
			}
			else if(transform.position.y >= target.transform.position.y + 30){
				this.speedY = -10;
			}
			else
				this.speedY = 0;

			this.moveDirection = transform.rotation.y == 0 ? new Vector3(-speedEnemy, speedY,0) : new Vector3(speedEnemy, speedY,0);

		}
		else if (enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Stop)
		{
			if( enableCoroutine )
			{
				this.speedY = 10;
				rotationEnemy();
				StopAllCoroutines();
				StartCoroutine(startFSM(stateAnim.idle, 2));
				this.enableCoroutine = false;
			}

			if(transform.position.x < target.transform.position.x - 0.5f)

				transform.rotation = new Quaternion(transform.rotation.x,180,transform.rotation.z,transform.rotation.w);

			else if(transform.position.x > target.transform.position.x + 0.5f)

				transform.rotation = new Quaternion(transform.rotation.x,0,transform.rotation.z,transform.rotation.w);
			else 
				this.enableCoroutine = true;

			if(transform.localPosition.y >= maxDistanceFly) 
				speedY = -speedY;

			else if(transform.localPosition.y <= minDistaceFly)
				speedY = speedY;
			
			this.moveDirection = transform.rotation.y == 0 ? new Vector3(0,speedY,0) : new Vector3(0,speedY,0);

		}
		else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Knock ) 
		{
			if ( enableCoroutine )
			{
				rotationEnemy();
				StopAllCoroutines();
				StartCoroutine(attackEnemy());
				this.enableCoroutine = false;
			}
		}

		Vector3 _distace = (positionBullet.position - target.position).normalized;

		weaponRotate = Quaternion.LookRotation(_distace);

		if( transform.localRotation.y == 0 )
		{
			if(weapon.position.x > target.position.x)
				weapon.localRotation = new Quaternion( 0, 0, weaponRotate.z , weaponRotate.w );
			else
				weapon.localRotation = Quaternion.Slerp( this.weapon.localRotation, new Quaternion( 0, 0, 0, weaponRotate.w ) , 3f * Time.deltaTime);

		}
		else if( transform.localRotation.y != 0 )
		{
			if(weapon.position.x < target.position.x)
				weapon.localRotation = new Quaternion( 0, 0, -weaponRotate.z , weaponRotate.w );
			else
				weapon.localRotation = Quaternion.Slerp(this.weapon.localRotation, new Quaternion( 0, 0, 0, weaponRotate.w ) , 3f * Time.deltaTime);
				//weapon.localRotation = new Quaternion( 0, 0, 0, weaponRotate.w );
		}
		controlerMove();
	}

#region Methods
	public void createBullet()
	{
		GameObject _bullet = Instantiate(bullet, positionBullet.transform.position, Quaternion.identity) as GameObject;
		//Verifica a posiçao do inimigo para rotacionar a bala
		if(transform.rotation.y == 0)
			_bullet.transform.Rotate(new Vector3(0, 0, weaponRotate.z * 165));
			//_bullet.transform.rotation = new Quaternion ( 0, 0, positionBullet.rotation.z * 100, transform.rotation.w );
		else
			_bullet.transform.Rotate(new Vector3(0, 180, weaponRotate.z * -165));
			//_bullet.transform.rotation = new Quaternion ( 0, 180, positionBullet.rotation.z * 100, transform.rotation.w );

		_bullet.GetComponent<EnemylsCollision>().father = this.gameObject;
		_bullet.GetComponent<MoveBullet>().setSpeed(-50);
	}
	
	public void resetBullet() 
	{
		enableCoroutine = true;
		
		this.countTimeShoot = 0;
		this.waitTimeShoot = Random.Range( 3, 7);
		
		randomNumberChooseState = Random.Range(1,101);
		
		if(randomNumberChooseState >= 1 && randomNumberChooseState <= 50)
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);
		else if (randomNumberChooseState > 50)
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
	}
#endregion

#region Methods Coroutines
	IEnumerator startFSM ( stateAnim typeState , float time )
	{
		enemyAnimation.setState(typeState); // Seta a animaçao de Correr 
		
		yield return new WaitForSeconds( time );
		
		enableCoroutine = true;
		randomNumberChooseState = Random.Range( 1, 101);

		if(randomNumberChooseState < 50)
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);
		else 
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


				if(enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.Knock &&
				   enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.SpecialAttack &&
				   enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.SpecialAttack2 )
				{

					StopAllCoroutines();
					enableCoroutine = true;
					enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);
					
				}
			}		
		}	
	}

#endregion
}
