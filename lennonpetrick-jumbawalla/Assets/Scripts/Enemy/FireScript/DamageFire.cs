using UnityEngine;
using System.Collections;

public class DamageFire : MonoBehaviour {

	public float damage;

	private ControllerLifePlayer life;
	private ControllerSpecial special;
	private PlayerStateControl stateControl;

	void Start()
	{
		this.life = GameObject.Find("Jumba").GetComponent<ControllerLifePlayer>();
		this.special = GameObject.Find("Jumba").GetComponent<ControllerSpecial>();
		stateControl = GameObject.Find("Jumba").GetComponent<PlayerStateControl>();
	}
	
	void OnTriggerStay ( Collider other )
	{
		if(other.name.Equals("Jumba"))
		{
			if(stateControl.getCurrentState() != PlayerState.Dead && stateControl.getCurrentState() != PlayerState.DeadFloor)
			{
				if(!special.getJumbaSpecial())
					life.RemoveLifePlayer( damage );
			}
		}
	}
}
