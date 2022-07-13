using UnityEngine;
using System.Collections;

public class SwordBossEvents : MonoBehaviour {

	private Animator anim;
	private SwordBoss componenteBoss;

	void Start() {

		this.anim = GetComponent<Animator>();
		this.componenteBoss = this.transform.parent.GetComponent<SwordBoss>();

	}

	void playAudio( AudioClip audio)
	{
		Director.sharedDirector().playEffect( audio );
	}

	void chargeParametersAnimationIdle() {

		this.componenteBoss.IsAnimation = true;

		anim.SetInteger("State", 1);

		this.componenteBoss.setStateSwordBoss(stateSwordBoss.MOVE);

	}

	void OnMoving() {

		this.componenteBoss.IsMove = true;

	}
	void DesableMoving() {

		this.componenteBoss.IsMove = false;

	}

	void OnAttaking() {

		this.componenteBoss.IsAttacking = true;

	}

	void ChargePositionY() {

		this.componenteBoss.chargePositionY();

	}

	void Destroy()
	{
		Destroy(GameObject.Find("EnemyLife").gameObject);

		Destroy(GameObject.Find("SwordBoss(Clone)").gameObject);

		GameObject.Find("EndLevel").GetComponent<EndLevel>().activeEnter();
	}

	public void receiveAttack() 
	{
		this.anim.SetTrigger("apanhando");
	}
}
