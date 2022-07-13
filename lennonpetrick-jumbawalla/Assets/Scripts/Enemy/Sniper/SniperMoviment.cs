using UnityEngine;
using System.Collections;

public class SniperMoviment : MonoBehaviour {

#region publics variables
		
	public Transform player;

	public float lookingForTime = 5.0f, // time that the enemy is seeking
				 delay = 1.25f, // delay of sight
				 speedLookFor = 0.1f, // velocity of looking for
				 maxAngleZ = 0.4f, // angle maximum of looking for
				 minAngleZ = 0.0f, // angle minimum of looking for
				 maxDistance = 50.0f; // distance maximum that the enemy can hear the player
	
#endregion publics variables
	
#region privates variables

	private SniperWeapon sniperWeapon;
	
	private Transform childTransform; // Top of enemy
	
	private float positionZ, // used to change Z position according side of enemy
				  angleZ = 0.0f, // used to work with Z angle of looking for
				  lastTimeViewed; // last time that the enemy saw the player
	
	private Quaternion newRotation, // Used to work with rotation of all object
					   newChildLocalRotation; // Used to work with rotation of top only
	
#endregion privates variables

	void Start () {
		
		this.childTransform = transform.FindChild("SniperUp");
		this.positionZ = this.childTransform.localPosition.z;
		this.angleZ = this.speedLookFor;

		this.newRotation = transform.localRotation;
		this.newChildLocalRotation = this.childTransform.localRotation;

		this.sniperWeapon = this.childTransform.GetChild(0).GetComponent<SniperWeapon>();
				
	}

	void LateUpdate () {

		// check if enemy listened the player
		bool _listenedPlayer = Vector3.Distance(player.position, transform.position) <= maxDistance;

		// if saw or listened then
		if (sniperWeapon.SawPlayer || _listenedPlayer){

			this.followVictim();
			
			this.lastTimeViewed = Time.time; // mark the last time viewed

		}
		else{ // neither saw nor heard

			// if the time passed then start to look for, else continues follow
			if((lastTimeViewed + lookingForTime) <= Time.time){

				this.lookingFor();

			}
			else{

				this.followVictim();

			}

		}

		// change the rotation of object
		transform.rotation = newRotation;

		// change the rotation of top object
		this.childTransform.localRotation = Quaternion.Slerp(this.childTransform.localRotation, newChildLocalRotation, this.delay * Time.deltaTime);
		
	}

	// Method to follow the victim
	private void followVictim(){

		// get distance
		Vector3 _distance = (player.position - this.childTransform.position).normalized;

		// if the player aren't under the enemy
		if(_distance.x <= -0.2f || _distance.x >= 0.2f){

			// look to the distance
			newChildLocalRotation = Quaternion.LookRotation(_distance);

			// if enemy are to left side
			if(_distance.x < 0){

				// turn to the left side
				newRotation = new Quaternion(transform.rotation.x, 0.0f, transform.rotation.z, transform.rotation.w);

				// turn Z rotation of top object according the side
				newChildLocalRotation = new Quaternion(this.childTransform.localRotation.x, this.childTransform.localRotation.y, 
				                                        newChildLocalRotation.z, newChildLocalRotation.w);

				// fix the Z position
				this.childTransform.localPosition = new Vector3(this.childTransform.localPosition.x, this.childTransform.localPosition.y, this.positionZ - 1);
				
			}
			else{ // enemy are to right side

				// turn to the right side
				newRotation = new Quaternion(transform.rotation.x, 180.0f, transform.rotation.z, transform.rotation.w);

				// turn Z rotation of top object according the side
				newChildLocalRotation = new Quaternion(this.childTransform.localRotation.x, this.childTransform.localRotation.y, 
				                                        -newChildLocalRotation.z, newChildLocalRotation.w);

				// fix the Z position
				this.childTransform.localPosition = new Vector3(this.childTransform.localPosition.x, this.childTransform.localPosition.y, this.positionZ + 1);
				
			}
			
		}
		
	}

	// Method to looking for the victim
	private void lookingFor(){

		// check if sigth to top or bottom
		if (this.childTransform.localRotation.z <= minAngleZ){
			
			this.angleZ = this.speedLookFor;
			
		}
		else if (this.childTransform.localRotation.z >= maxAngleZ){
			
			this.angleZ = -this.speedLookFor;
			
		}

		// continues with the same rotation
		newRotation = transform.rotation;

		// only change the rotation of top
		newChildLocalRotation = new Quaternion(this.childTransform.localRotation.x, this.childTransform.localRotation.y, 
		                                        this.childTransform.localRotation.z + this.angleZ , this.childTransform.localRotation.w);

	}

}