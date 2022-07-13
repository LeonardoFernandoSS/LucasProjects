using UnityEngine;
using System.Collections;

public class AircraftDeathCollision : MonoBehaviour {

	public float damage = 30.0f, // damage of aircraft explosion
				 explosionForce = 2.0f, // force of aircraft explosion
				 recoilSpeed = 10.0f; // velocity that the player will recoil when him is hit

	// prefabs
	public Transform explosionEffect;
	public AudioClip explosionAudio;

	private ControllerHitSequence controllerHitSequence; // sequence control of Hit	
	private ControllerHitPlayer controllerHit; // Hit Control of player	
	private InterfaceHitCombo interfaceHitCombo; // Interface control of hitCombo
	private Transform player;
			
	void Start () {
		
		player = GameObject.Find("Jumba").transform;

		// gets this scripts from player
		this.controllerHitSequence = player.GetComponent<ControllerHitSequence>();		
		this.controllerHit = player.GetComponent<ControllerHitPlayer>();		
		this.interfaceHitCombo = player.GetComponent<InterfaceHitCombo>();
				
	}

	void OnTriggerEnter(Collider collider){

		// when it is hit by AttackJumba
		if(collider.tag == "AttackJumba"){
			
			this.controllerHitSequence.setCollisionEnemy(true);

			this.controllerHit.AddHitCombo();
			
			this.interfaceHitCombo.AddHitAnimation();
						
			// play sound of attack jumba
			
			/*// Instanciate some particle of collision
			Instantiate(particleCollision, new Vector3(	collider.transform.position.x, 
			                                           collider.transform.position.y, 
			                                           collider.transform.position.z - 0.2f), Quaternion.identity);*/

			// warns the AircraftMovement to fall down
			GetComponent<AircraftMovement>().CanFallDown = true;
							
		}

		// when it is hit by floor, in other words, it hit the ground
		if(collider.tag == "Floor"){

			this.explode();

		}
		
	}

	// method used to explode the aircraft
	private void explode(){

		// creates 3 explosions with different positions
		this.createExplosion(transform.GetChild(0).GetChild(0).GetChild(0).position);
		this.createExplosion(transform.GetChild(0).GetChild(0).GetChild(1).position);
		this.createExplosion(transform.position);

		// after that, destroy the object
		Destroy(this.transform.parent.gameObject);
		
	}

	// method to create the explosion
	private void createExplosion(Vector3 position){

		Transform _explosion = Instantiate(explosionEffect, position, Quaternion.identity) as Transform;
		
		Destroy(_explosion.gameObject, 1.0f);
		
		//Director.sharedDirector().playEffect(explosionAudio);

		// check the distance of player to hit him according this
		float _distance = Vector3.Distance(player.transform.position, position);
		
		if(_distance <= 20.0f){
			
			player.GetComponent<ControllerLifePlayer>().RemoveLifePlayer(damage / _distance);
			
			player.GetComponent<CharacterController>().Move(new Vector3(explosionForce / _distance, 
			                                                            (explosionForce / _distance), 0.0f) * recoilSpeed * Time.deltaTime);
			
		}

	}

}