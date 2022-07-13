using UnityEngine;
using System.Collections;

public class SniperLife : MonoBehaviour {

	#region public variables

		public float life = 100.0f;
	
		public Transform particleCollision;

	public EnemyEvents events;
	
	#endregion

	#region private variables
	
		private ControllerHitSequence controllerHitSequence; // sequence control of Hit
		
		private ControllerHitPlayer controllerHit; // Hit Control of player
		
		private InterfaceHitCombo interfaceHitCombo; // Interface control of hitCombo
		
		private Transform player; // to get the weapon power
		
		private bool dead = false, 
					 canChangeDirection = true;
		
		private float fallSpeedFactor = 0.5f, // this is the factor that goes increasing according each frame
					  fallSpeed = 7.0f, // the fall velocity
					  fallRotationSpeed = 300.0f; // the velocity of fall rotation
		
		private Vector3 direction; // used to mark the direction

	#endregion
	
	void Start () {
		
		player = transform.parent.GetComponent<SniperMoviment>().player; // it gets the player
		
		this.controllerHitSequence = player.GetComponent<ControllerHitSequence>(); // to control the hit sequence
		
		this.controllerHit = player.GetComponent<ControllerHitPlayer>(); // to control the hit
		
		this.interfaceHitCombo = player.GetComponent<InterfaceHitCombo>(); // to control the hit interface
		
	}
	
	void Update () {

		// check if the enemy is dead
		if(dead){

			// if yes, rotates the enemy
			transform.parent.Rotate(Vector3.back * fallRotationSpeed * Time.deltaTime);
			
			// and fall
			transform.parent.parent.Translate(direction * fallSpeed * Time.deltaTime);
			
			// check if the script cans change the direction
			if (canChangeDirection){

				// then invoke this method in 0.5 seconds
				Invoke("changeDirection", 0.5f);

				// and block this way
				canChangeDirection = false;
				
			}

			// increase the fall velocity with the factor that is ranging
			fallSpeed += fallSpeedFactor;
			
		}
		
	}
	
	private void changeDirection(){

		// increase the factor to the fall velocity becomes fast
		fallSpeedFactor = 5;

		// checks the side of enemy to change the direction
		if(transform.parent.localRotation.y == 0){
			
			direction = new Vector3(0.2f, -1f, 0.0f);
			
		}
		else{
			
			direction = new Vector3(-0.2f, -1f, 0.0f);
			
		}
		
	}
	
	void OnTriggerEnter(Collider collider){
		
		if(collider.tag == "AttackJumba"){

			// activates the hit sequence
			this.controllerHitSequence.setCollisionEnemy(true);

			events.playAudioRandom();
						
			// Instanciate some particle of collision
			Instantiate(particleCollision, new Vector3(	collider.transform.position.x, 
			                                           collider.transform.position.y, 
			                                           collider.transform.position.z - 0.2f), Quaternion.identity);				
				
			// add the hit on combo	
			this.controllerHit.AddHitCombo();

			// does the hit animation
			this.interfaceHitCombo.AddHitAnimation();
			
			// remove some life accordig the powerAttack
			this.removeLife(player.GetComponent<StrikeForce>().getPowerAttack());

		}
		
	}
	
	private void removeLife(float damage){

		// removes damage from life
		life -= damage;

		// if life is 0
		if (life <= 0.0f){

			// disable some scripts to stop the moviment and the fire of weapon
			transform.parent.GetComponent<SniperMoviment>().enabled = false;
			transform.GetChild(0).GetComponent<SniperWeapon>().enabled = false;

			// checks the side of enemy to start the direction
			if(transform.parent.localRotation.y == 0){
				
				direction = new Vector3(0.8f, 0.5f, 0.0f);
				
			}
			else{
				
				direction = new Vector3(-0.8f, 0.5f, 0.0f);
				
			}

			// activates the update
			dead = true;

			// order to destroy this object in 2 seconds
			Destroy(transform.parent.parent.gameObject, 2.0f);
			
		}
		
	}
	
}