using UnityEngine;
using System.Collections;

public class SledgeHammer : MonoBehaviour {

	public SledgeHammerControl hammerControl;

	public float damage; // damage the hammer
	public float timeToDamage;
	public float speedX, speedY;
	public float speedHammer; // speed hammer down and up

	public float limitMin; // limit up hammer
	public float limitMax; // limit down hammer

	private float currentSledgeHammer; // time to active up or down
	private float waitingSledgeHammer; // waiting time to active

	private bool activeSledgeHammer; // active down or up Hammer
	private bool onTriggerCollision;

	void Start() {

		this.currentSledgeHammer = 0.0f;

		this.waitingSledgeHammer = Random.Range( 2, 5);

		this.activeSledgeHammer = true;

		this.onTriggerCollision = true;

	}

	void Update() {

		if( this.hammerControl.getStateControl() ) {

			if(this.currentSledgeHammer >= this.waitingSledgeHammer) {

				transform.Translate( new Vector3 (0, this.activeSledgeHammer ? -this.speedHammer : this.speedHammer/9, 0 ) * Time.deltaTime );

				if( this.activeSledgeHammer ) {
					
					if(transform.localPosition.y <= this.limitMin)  
						
						resetHammer();
						
				}
				else {
					
					if(transform.localPosition.y >= this.limitMax) 
						
						resetHammer();
				}

			}
			if(this.currentSledgeHammer <= this.waitingSledgeHammer) 

				this.currentSledgeHammer += Time.deltaTime;
		}

		else {

			if(transform.localPosition.y <= this.limitMax)
				transform.Translate( new Vector3 (0, this.speedHammer/9, 0 ) * Time.deltaTime );

		}
	}

	void resetHammer() {

		this.currentSledgeHammer = 0.0f;
		
		this.waitingSledgeHammer = Random.Range( 2, 5);
		
		this.activeSledgeHammer = !this.activeSledgeHammer;

	}
	
	void OnTriggerStay ( Collider other ) {
		
		if(this.onTriggerCollision) {
			
			if(other.name.Equals("Jumba")) {

				PlayerControl.leadAttack( damage, other.transform.rotation.y == 0 ? true : false, timeToDamage, speedX, speedY);
				
				this.onTriggerCollision = false;

			}
		}
	}

	void OnTriggerExit ( Collider other ) {

		if(other.name.Equals("Jumba")) 
			this.onTriggerCollision = true;

	}

}
