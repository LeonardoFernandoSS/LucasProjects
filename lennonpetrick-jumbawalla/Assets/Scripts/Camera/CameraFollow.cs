using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public float delay = 4f,
				 initialLimit, 
				 finalLimit,
				 cameraHigh; // used to define the high of camera relative to the player 

	public bool deactivateFollowMovementY; // option to follow or not follow the player in Y
	public Transform[] layers; // layers that will movement according the camera
	public Vector2[] layersVelocities; // velocity of these layers
	
	private Transform targetTransform; // player's transform component
	private PlayerStateControl targetState; // player's state
	private Vector2 lastPosition;
	private Vector3 newCameraPosition;
	
	private bool lockCamera = false; // used to lock the camera in X position

	private float initialLimitBkp,
				  finalLimitBkp;
		
	void Start () {
		
		GameObject _jumba = GameObject.Find("Jumba"); // find the jumba
		
		targetTransform = _jumba.transform; // gets his transform
		
		targetState = _jumba.GetComponent<PlayerStateControl>(); // gets his state
		
	}
	 
	void LateUpdate() {

		bool _isOutLimit = true;

		// check if it is between the positions
		if (targetTransform.position.x >= initialLimit && targetTransform.position.x <= finalLimit){

			_isOutLimit = false;

		}

		// create the new position using the slerp and adding the cameraHigh
		newCameraPosition = Vector3.Slerp(new Vector3(transform.position.x, transform.position.y, 0), 
		                                  new Vector3(targetTransform.position.x, targetTransform.position.y + cameraHigh, 0), delay * Time.deltaTime);
		
		// now change the position of camera, check some things like the lockCamera, _isOutLimit, deactivateFollowMovementY and the jumba's state
		transform.position = new Vector3(lockCamera || _isOutLimit ? transform.position.x : newCameraPosition.x, 
		                                ((targetState.getCurrentState() == PlayerState.Jumping) || 
	                                    (targetState.getCurrentState() == PlayerState.Falling) || 
		 								(targetState.getCurrentState() == PlayerState.DobleJumping) ||
	                                    (targetState.getCurrentState() == PlayerState.JumpAttacking)) && deactivateFollowMovementY ? 
	                                   transform.position.y : newCameraPosition.y, transform.position.z);
		
		// gets the velocity of the camera
		Vector2 _velocity = this.lastPosition - new Vector2(transform.position.x, transform.position.y);
		
		this.lastPosition = new Vector2(transform.position.x, transform.position.y);
		
		int _index = 0;
		
		// pass through each layer changing the positions
		foreach(Transform layer in layers){
			
			// create the new position according the velocities respective
			Vector2 _newPosition = new Vector2(((_velocity.x * layersVelocities[_index].x)/100.0f), ((_velocity.y * layersVelocities[_index].y)/100.0f));
			
			// changes the position by adding one's own position and the new position
			layer.position = new Vector3(layer.position.x + _newPosition.x, layer.position.y + _newPosition.y, layer.position.z);
			
			_index++;
			
		}

	}

	public void changeInitialLimit(float newLimit){

		initialLimitBkp = initialLimit;

		initialLimit = newLimit;

	}

	public void changeFinalLimit(float newLimit){
		
		finalLimitBkp = finalLimit;
		
		finalLimit = newLimit;
		
	}

	public void restoreInitialLimit(){
				
		initialLimit = initialLimitBkp;
		
	}
	
	public void restoreFinalLimit(){
						
		finalLimit = finalLimitBkp;
		
	}
	
	public void setLockCamera(bool camera) {

		this.lockCamera = camera;
	
	}
	
	public bool getLockCamera() {

		return this.lockCamera;
	
	}
	
}