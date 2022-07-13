using UnityEngine;
using System.Collections;

public enum stateAnim 
{
	dead,
	idle,
	run,
	knock,
	knock_two,
	walkingBackwards,
	knock_up,
	knock_down,
	stun
}

public class EnemyAnimator : MonoBehaviour {

	public Animator anim;

	private stateAnim stateEnemyControl;

	public void setState ( stateAnim value  ) 
	{
		this.stateEnemyControl = value;
		this.chargeAnim();
	}

	public stateAnim getState() 
	{ 
		return this.stateEnemyControl; 
	}

	public void receiveAttack() 
	{
		this.anim.SetTrigger("apanhando");
	}

	void chargeAnim() 
	{
		if(this.stateEnemyControl == stateAnim.idle)
		{
			this.anim.SetInteger("animation", 0);
		}
		else if(this.stateEnemyControl == stateAnim.run) 
		{
			this.anim.SetInteger("animation", 1);
		}
		else if(this.stateEnemyControl == stateAnim.walkingBackwards) 
		{
			this.anim.SetInteger("animation", 2);
		}
		else if(this.stateEnemyControl == stateAnim.knock) 
		{
			this.anim.SetInteger("animation", 3);
		}
		if(this.stateEnemyControl == stateAnim.knock_two)
		{
			this.anim.SetInteger("animation", 4);
		}
		else if(this.stateEnemyControl == stateAnim.dead) 
		{
			this.anim.SetInteger("animation", 5);
		}
		else if(this.stateEnemyControl == stateAnim.knock_up)
		{
			this.anim.SetInteger("animation", 6);
		}
		else if(this.stateEnemyControl == stateAnim.knock_down)
		{
			this.anim.SetInteger("animation", 7);
		}
		else if(this.stateEnemyControl == stateAnim.stun)
		{
			this.anim.SetInteger("animation", 8);
		}
	}
}
