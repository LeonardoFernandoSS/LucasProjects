using UnityEngine;
using System.Collections;

public class ExterminatorLife : MonoBehaviour {
	
	public float maxLife = 1000.0f;
	
	private ControllerHitSequence controllerHitSequence; // sequence control of Hit
	
	private ControllerHitPlayer controllerHit; // Hit Control of player
	
	private InterfaceHitCombo interfaceHitCombo; // Interface control of hitCombo
	
	private Transform player;

	private ExterminatorMovement exterminatorMovement;
	
	private InterfaceLifeBoss interfaceLifeBoss; // to manager the life interface

	private int angryDegree = 0, // the degree of angry of the Exterminator
				maxAngryDegree; // the maximum of degree of angry  that the Exterminator can be

	private float life; // used to work with the life
	
	void Start () {
		
		player = GameObject.Find("Jumba").transform;
		
		this.controllerHitSequence = player.GetComponent<ControllerHitSequence>();
		
		this.controllerHit = player.GetComponent<ControllerHitPlayer>();
		
		this.interfaceHitCombo = player.GetComponent<InterfaceHitCombo>();
		
		interfaceLifeBoss = GameObject.Find("EnemyLife").GetComponent<InterfaceLifeBoss>();

		exterminatorMovement = transform.parent.GetComponent<ExterminatorMovement>();

		life = maxLife;

		// define the maximum angry degree of the boss
		maxAngryDegree = Random.Range(5, 10);

		// sends the difficulty factor
		exterminatorMovement.setDegreeOfDifficulty(life / maxLife);

	}

	void OnTriggerEnter(Collider collider){

		if(collider.tag == "AttackJumba"){

			if(exterminatorMovement.getCurrentState() != MovementState.dead){
			
				this.controllerHitSequence.setCollisionEnemy(true);
				
				exterminatorMovement.setCurrentState(MovementState.leadingAttack, 0.0f, 0.0f);
									
				this.controllerHit.AddHitCombo();
				
				this.interfaceHitCombo.AddHitAnimation();
				
				// remove some life accordig the powerAttack
				this.removeLife(player.GetComponent<StrikeForce>().getPowerAttack());

				// add one degree for boss's angry
				angryDegree++;

				// checks if is in the maximum
				if(angryDegree >= maxAngryDegree){

					exterminatorMovement.setCurrentState(MovementState.angry, 2f, 4f);

					// starts again
					angryDegree = 0;

					// and set the new maximum degree of angry
					maxAngryDegree = Random.Range(5, 10);

				}

			}

		}
		
	}
	
	private void removeLife(float damage){
		
		life -= damage;

		// update the life bar in the interface
		interfaceLifeBoss.checkLifeBar(life);

		// sends the current difficulty factor
		exterminatorMovement.setDegreeOfDifficulty(life / maxLife);

		if (life <= 0.0f){
			
			exterminatorMovement.setCurrentState(MovementState.dead, 0, 0);
			
		}
		
	}
	
}