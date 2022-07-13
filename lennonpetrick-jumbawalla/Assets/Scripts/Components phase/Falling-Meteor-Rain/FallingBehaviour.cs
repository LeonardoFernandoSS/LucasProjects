using UnityEngine;
using System.Collections;

public class FallingBehaviour : MonoBehaviour {

	private Transform player;

	public float distanceToPush,
				 damageToPush,
				 time,
				 speedX,
				 speedY;

	private bool activeToPush;	

	void Awake() {

		this.activeToPush = true;

		player = GameObject.Find("Jumba").transform;

	}

	void Update () {

		if(GetComponent<ParticleSystem>().time >= 3) {

			if(this.activeToPush) {

				toPush();

				this.activeToPush = false;

			}
		}

		Destroy( gameObject, 4f );

	}

	private void toPush() {

		if(Vector3.Distance(player.transform.position, transform.position) <= distanceToPush) {

			GettingAttacks.checkGettingAttacks().setProperties( player.transform.position.x < transform.position.x ? true : false, 
			                                                   time, 
			                                                   speedX, 
			                                                   speedY);
			
			PlayerStateControl.sharePlayer().setState(PlayerState.LeadingAttack);
			
			ControllerLifePlayer.sharedLife().RemoveLifePlayer(damageToPush);
		}
	}
}
