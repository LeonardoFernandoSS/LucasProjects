using UnityEngine;
using System.Collections;

public class Fountain : MonoBehaviour {

	public GameObject player;
	
	public float distanceToActive = 50.0f,
				 timeEruption = 5.0f,
				 distanceToPush,
				 damageToPush,
				 time,
				 speedX,
				 speedY;

	public AudioClip chafariz;

	private float lastTimeEruption;
	
	private bool 	eruptionActived = false,
					activeToPush;

	void Awake() {

		activeToPush = true;
		player = GameObject.Find("Jumba");

	}

	void Update () {

		if (Vector3.Distance(player.transform.position, transform.position) <= distanceToActive){
					
			if(eruptionActived){


				if(activeToPush) {
					checkCollision();
					activeToPush = false;
				}

				if(GetComponent<ParticleSystem>().time >= 0.5){
					
					this.deactivateEruption();

				}
			}
			else{
				
				if(lastTimeEruption + timeEruption <= Time.time){

					Director.sharedDirector().playEffect(chafariz);

					this.activeEruption();
				}
			}
		}
	}

	private void checkCollision() {

		if(Vector3.Distance(player.transform.position, transform.position) <= distanceToPush ) {

			GettingAttacks.checkGettingAttacks().setProperties( player.transform.position.x < transform.position.x ? true : false, 
			                                                   	time, 
			                                                   	speedX, 
			                                                   	speedY);

			PlayerStateControl.sharePlayer().setState(PlayerState.LeadingAttack);

			ControllerLifePlayer.sharedLife().RemoveLifePlayer(damageToPush);
		}
	}

	private void activeEruption(){

		this.activeToPush = true;

		this.eruptionActived = true;
		
		GetComponent<ParticleSystem>().Play();
				
	}
	
	private void deactivateEruption(){
		
		this.eruptionActived = false;
		
		this.lastTimeEruption = Time.time;
		
	}
}