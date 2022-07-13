using UnityEngine;
using System.Collections;

public class DamageCollision : MonoBehaviour {

	public RobotSubLife life;

	private ControllerHitSequence controllerHitSequence;
	private ControllerHitPlayer controllerHit;
	private InterfaceHitCombo interfaceHitCombo;

	public AudioClip strike;

	void Start()
	{
		GameObject jumba = GameObject.Find("Jumba");

		controllerHit = jumba.GetComponent<ControllerHitPlayer>();
		controllerHitSequence = jumba.GetComponent<ControllerHitSequence>();
		interfaceHitCombo = jumba.GetComponent<InterfaceHitCombo>();

	}

	void OnTriggerEnter ( Collider other ) {

		if(other.tag.Equals("AttackJumba")) {

			if(life.life.getLife() >= 0) {

				Director.sharedDirector().playEffect( strike );
				this.controllerHitSequence.setCollisionEnemy(true);
				this.controllerHit.AddHitCombo();
				this.interfaceHitCombo.AddHitAnimation();

				life.removeLife(StrikeForce.checkStrikeForce().getPowerAttack());
				GameObject.Find("EnemyLife").GetComponent<InterfaceLifeBoss>().checkLifeBar(life.life.getLife());

			}

		}
	}
}
