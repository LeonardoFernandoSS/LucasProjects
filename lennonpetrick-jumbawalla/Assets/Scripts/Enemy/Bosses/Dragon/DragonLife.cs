using UnityEngine;
using System.Collections;

public class DragonLife : MonoBehaviour {

	public float life = 1000.0f;
	public Animator animator;

	private ControllerHitSequence controllerHitSequence; // sequence control of Hit
	private ControllerHitPlayer controllerHit; // Hit Control of player
	private InterfaceHitCombo interfaceHitCombo; // Interface control of hitCombo
	private InterfaceLifeBoss interfaceLifeBoss; // to manager the life interface

	private Transform player;

#region event

	public static event System.Action explodeDragonEvent;

#endregion

	void Start () {
	
		player = transform.parent.GetComponent<DragonHead>().player;

		this.controllerHitSequence = player.GetComponent<ControllerHitSequence>();

		this.controllerHit = player.GetComponent<ControllerHitPlayer>();

		this.interfaceHitCombo = player.GetComponent<InterfaceHitCombo>();

		interfaceLifeBoss = GameObject.Find("EnemyLife").GetComponent<InterfaceLifeBoss>();

	}

	void OnTriggerEnter(Collider collider){

		if(collider.tag == "AttackJumba"){

			this.controllerHitSequence.setCollisionEnemy(true);

			animator.SetTrigger("apanhando");
						
			this.controllerHit.AddHitCombo();
			
			this.interfaceHitCombo.AddHitAnimation();
						
			// remove some life accordig the powerAttack
			this.removeLife(player.GetComponent<StrikeForce>().getPowerAttack());

		}
		
	}

	private void removeLife(float damage){

		if(life > 0.0f){

			life -= damage;

			interfaceLifeBoss.checkLifeBar(life);

			if (life <= 0.0f){

				explodeDragonEvent();

			}

		}

	}

}