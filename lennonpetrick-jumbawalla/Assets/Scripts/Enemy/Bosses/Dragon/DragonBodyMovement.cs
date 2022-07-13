using UnityEngine;
using System.Collections;

public class DragonBodyMovement : MonoBehaviour {

	public Transform explosionPrefab;

	public GameObject platform;

	public AudioClip explosionAudio;

	public float distance = 9.0f; // The distance between the bodies

	private Vector3 lastPosition; // Used to save the last position from last child

	private Transform head, // The body will movement according this, may be the dragon's head or dragon's tail
					  dragonHead; // The transform of dragon's head

	private float variationZ;

	public static float constantZPosition; // used to fix the head Z position

	// turn on every events
	void OnEnable(){
				
		DragonLife.explodeDragonEvent += explodeDragon;
		
	}
	
	// turn off every events
	void OnDisable(){
				
		DragonLife.explodeDragonEvent -= explodeDragon;
		
	}

	void Start () {

		this.dragonHead = transform.FindChild("Head");

		constantZPosition = this.dragonHead.localPosition.z;

		this.setHead(this.dragonHead);

	}

	void Update () {

		this.variationZ = 0.1f;

		// check the direction
		if(head == dragonHead){// here is the head

			for(int i = 0; i < transform.childCount; i++){ // pass through all body
				
				this.moveChild(transform.GetChild(i)); // get the child and move it
				
			}

		}
		else{// here is the tail

			for(int i = transform.childCount - 2; i >= 0 ; i--){ // pass through all body
				
				this.moveChild(transform.GetChild(i)); // get the child and move it
				
			}

			this.moveChild(this.dragonHead);

		}

		// After change all body, save last position and last rotation from dragon's head
		// to change the body again
		this.lastPosition = head.localPosition;

	}

	private void moveChild(Transform child){

		// compare with the head, because can't be the same object
		if(child != head){
			
			Vector3 _auxPosition = child.localPosition; // save the position its
			
			// calc the distance from this position and lastPosition and normalize
			Vector3 _auxDistance = (child.localPosition - this.lastPosition).normalized;

			// look to this distance before multiply
			if(_auxDistance != Vector3.zero){

				Quaternion _rotation = Quaternion.LookRotation(_auxDistance);

				_rotation = new Quaternion(0, 0, _rotation.z, _rotation.w); // fix the rotation
				
				child.localRotation = _rotation; // change to new rotation

			}
			
			Transform _grandson = child.GetChild(0);	
			
			// turn Y rotation of object child according the side
			// If forward the head variable is the dragon's head, else it's dragon's tail
			if(head == dragonHead){// here is the head
				
				if(_auxDistance.x < 0){
					
					_grandson.localRotation = Quaternion.identity;
					
				}
				else{
					
					_grandson.localRotation = new Quaternion(0, 180, 0, 0);
					
				}
				
			}
			else{// here is the tail
				
				if(_auxDistance.x < 0){
					
					_grandson.localRotation = new Quaternion(0, 180, 0, 0);
					
				}
				else{
					
					_grandson.localRotation = Quaternion.identity;
					
				}
				
			}

			_auxDistance *= this.distance; // now, multiply by distance to change the position

			// the position receives the sum between de distance and the lastPosition on X and Y coordinates
			// The Z coordinate receives the Z constante plus the variationZ
			Vector3 _newPosition = this.lastPosition + _auxDistance;

			child.localPosition = new Vector3(_newPosition.x, _newPosition.y, constantZPosition + this.variationZ);

			// increases the variation
			this.variationZ += 0.1f;

			this.lastPosition = _auxPosition; // the lastPosition receives the position saved
						
		}

	}

	public void setHead(Transform newHead){

		this.head = newHead;

		this.lastPosition = this.head.localPosition; // change to the head position

	}

	private void explodeDragon(){

		Vector3 _position = transform.GetChild(0).position;

		if(explosionPrefab != null){

			if(explosionAudio != null){

				Director.sharedDirector().playEffect(explosionAudio);

			}

			Instantiate(explosionPrefab, new Vector3(_position.x, _position.y, _position.z - 1.0f), Quaternion.identity);

		}

		transform.gameObject.SetActive(false);

		platform.SetActive(true);

		Destroy(GameObject.Find("EnemyLife"));
		
		CameraFollow _camera = Camera.main.GetComponent<CameraFollow>();
		
		_camera.restoreInitialLimit();
		_camera.restoreFinalLimit();
		
		Director.sharedDirector().restoreLastBackgroundAudio();
			
	}

}