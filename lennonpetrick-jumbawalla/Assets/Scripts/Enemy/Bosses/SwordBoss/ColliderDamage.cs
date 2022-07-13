using UnityEngine;
using System.Collections;

public class ColliderDamage : MonoBehaviour {

	public SwordBossLife life;
	public SwordBossEvents enemyAnimation;

	private ControllerHitPlayer controllerHit;
	private InterfaceHitCombo interfaceHitCombo;
	private ControllerHitSequence controllerHitSequence; // controle da sequencia de Hit

	void Start() 
	{
		var jumba = GameObject.Find("Jumba");
		controllerHit = jumba.GetComponent<ControllerHitPlayer>();
		interfaceHitCombo = jumba.GetComponent<InterfaceHitCombo>();
		controllerHitSequence = jumba.GetComponent<ControllerHitSequence>();
	}

	void OnTriggerEnter ( Collider other ) {
		
		if(other.tag.Equals("AttackJumba")) {

			if(life.getCurrentLife() >= 0) {

				this.controllerHitSequence.setCollisionEnemy(true);

				this.controllerHit.AddHitCombo();

				this.interfaceHitCombo.AddHitAnimation();
				
				// Remove uma quantidade de vida equivalente
				//this.enemyAnimation.receiveAttack();

				life.removeLife(StrikeForce.checkStrikeForce().getPowerAttack());

				GameObject.Find("EnemyLife").GetComponent<InterfaceLifeBoss>().checkLifeBar(life.getCurrentLife());
				
			}
			else {

				SwordBoss sword = GetComponent<SwordBoss>();
				sword.IsAnimation = true;
				sword.setStateSwordBoss (stateSwordBoss.DEAD);
				Destroy(GetComponent<ColliderDamage>());

			}
		}
	}
}
