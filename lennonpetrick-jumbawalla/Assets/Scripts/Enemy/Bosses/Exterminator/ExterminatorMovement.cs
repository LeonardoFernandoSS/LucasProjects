using UnityEngine;
using System.Collections;

public class ExterminatorMovement : MonoBehaviour {

	public LayerMask layerMask; // Used to raycast only the layer required

	public Animator animator;

	public AudioClip[] audioEffects;

	public float speed = 10.0f,
				 maxLimit, // the maximum limit that Exterminator will run
				 minLimit, // the minimum limit that Exterminator will run
				 mass = 10.0f, // used to help in gravity
				 shootForce = 200.0f,
				 damageOfExplosion = 30.0f, // damage of Exterminator explosion
				 explosionForce = 2.0f, // force of Exterminator explosion
				 recoilSpeed = 10.0f; // velocity that the player will recoil when him is hit
	
	public Transform shootBase, 
					 projectile; // collision for attack jumba

	public Collider collision;

	private float fixedPositionZ, // used to define a fixed Z position
				  lastTime, // used to mark the last time making some action
				  maxTime,  // maximum time to wait the next action
				  lastShotTime, // used to mark last time shooting
				  maxShotTime, // maximum time to wait the next shoot
				  degreeOfDifficulty; // used to define degree of defficult of boss
		
	private Transform ownTransform;

	private MovementState currentState;

	private Vector3 gravity;
	
	private CharacterController controller;

	void OnEnable () {

		ExterminatorEvents.onAttackEndsEvent += onAttackEnds;
		ExterminatorEvents.onLeadingAttackEndsEvent += onLeadingAttackEnds;
		ExterminatorEvents.onShootEvent += onShoot;
		ExterminatorEvents.onExplodeEvent += onExplode;
		ExterminatorEvents.onRunEvent += onRun;
		ExterminatorEvents.onWheelieEvent += onWheelie;
		ExterminatorEvents.onIdleEvent += onIdle;

	}

	void OnDisable () {

		ExterminatorEvents.onAttackEndsEvent -= onAttackEnds;
		ExterminatorEvents.onLeadingAttackEndsEvent -= onLeadingAttackEnds;
		ExterminatorEvents.onShootEvent -= onShoot;
		ExterminatorEvents.onExplodeEvent -= onExplode;
		ExterminatorEvents.onRunEvent -= onRun;
		ExterminatorEvents.onWheelieEvent -= onWheelie;
		ExterminatorEvents.onIdleEvent -= onIdle;

	}

	void Start () {

		controller = GetComponent<CharacterController>();

		fixedPositionZ = transform.position.z;

		ownTransform = transform;

		this.setCurrentState(MovementState.presentation, 4.0f, 8.0f);

	}

	void Update () {
	
		if(currentState != MovementState.dead){

			// The driveFast state is going to shoot and turn. The angry state and presentation state just is going to turn
			if(currentState == MovementState.driveFast || currentState == MovementState.angry || currentState == MovementState.presentation){

				if(controller.isGrounded){

					// resets any gravity existing
					gravity = Vector3.zero;

					// turns in minimum limit
					if(transform.position.x <= minLimit){

						ownTransform.rotation = new Quaternion(ownTransform.rotation.x, 180f, ownTransform.rotation.z, ownTransform.rotation.w);

						// Just wait if isn't angry state and presentation state
						if(currentState != MovementState.angry && currentState != MovementState.presentation){

							this.setCurrentState(MovementState.idleHidden, 4.0f, 5.0f);

						}

					}

					// turns in maximum limit
					if(transform.position.x >= maxLimit){

						ownTransform.rotation = new Quaternion(ownTransform.rotation.x, 0, ownTransform.rotation.z, ownTransform.rotation.w);

						// Just wait if isn't angry state and presentation state
						if(currentState != MovementState.angry && currentState != MovementState.presentation){

							this.setCurrentState(MovementState.idleHidden, 4.0f, 5.0f);
							
						}

					}

					// Just shoot if it's driveFast state
					if(currentState == MovementState.driveFast){
						
						// if spent the all time, so now it can to shoot
						if(canShoot()){

							// set the state and the time of next shoot
							this.setCurrentState(MovementState.attacking, 5.0f, 10.0f);
							
						}
						
					}

				}
				else{

					// Apply gravity to our velocity to diminish it over time
					gravity.y += (Physics.gravity.y - mass) * Time.deltaTime;

				}

			}

			Vector3 _movement = Vector3.zero;

			// here it goes driving for few time doing the presentation
			if(currentState == MovementState.presentation){

				_movement = this.driveFast();

				if((lastTime + maxTime) < Time.time){

					this.setCurrentState(MovementState.driveFast, 0.0f, 0.0f);
					
				}

			}

			// here it drives slowly for few time and stop. After that it's idle
			if(currentState == MovementState.driveSlowly){

				_movement = this.driveSlowly();

				if((lastTime + maxTime) < Time.time){

					this.setCurrentState(MovementState.idle, 0.5f, 1.0f);

				}

			}

			// here it's angry and run faster from one side to the other. After come back to driveFast
			if(currentState == MovementState.angry){
				
				_movement = this.driveAngry();

				if((lastTime + maxTime) < Time.time){
					
					this.setCurrentState(MovementState.driveFast, 0.0f, 0.0f);
					
				}
				
			}

			// here it drive fast
			if(currentState == MovementState.driveFast){

				_movement = this.driveFast();

			}

			// here it waits a time and then it drives fast
			if(currentState == MovementState.idle){

				_movement = Vector3.zero;

				if((lastTime + maxTime) < Time.time){

					this.setCurrentState(MovementState.driveFast, 0.0f, 0.0f);

				}

			}

			// here it cans attack and be idle after that
			if(currentState == MovementState.attacking){

				_movement = Vector3.zero;

			}

			// here it wait a time hidden on side of screen. After starts driving slowly
			if(currentState == MovementState.idleHidden){

				_movement = Vector3.zero;

				if((lastTime + maxTime) < Time.time){
					
					this.setCurrentState(MovementState.driveSlowly, 1.0f, 2.0f);
					
				}
				
			}

			// adds the gravity in movement
			_movement += gravity;
			_movement += Physics.gravity;

			_movement *= Time.deltaTime;

			controller.Move(_movement);

			// fixes the Z position
			transform.position = new Vector3(transform.position.x, transform.position.y, fixedPositionZ);

		}

	}

	// defines the movement angry
	private Vector3 driveAngry(){
		
		return ownTransform.TransformDirection(Vector3.left) * (speed * Random.Range(1.5f, 2.5f));
		
	}

	// define the fast movement
	private Vector3 driveFast(){

		return ownTransform.TransformDirection(Vector3.left) * speed;

	}

	// defines the slowly movement
	private Vector3 driveSlowly(){

		return ownTransform.TransformDirection(Vector3.left) * (speed / Random.Range(2.0f, 4.5f));

	}

	// check if the boss is behind of player, between the distance and at time to shoot
	private bool canShoot(){

		RaycastHit _hit;

		bool _return = false;

		if (Physics.Raycast(ownTransform.position, ownTransform.TransformDirection(Vector3.right), out _hit, 200.0f, layerMask)){

			// if hit the player
			if (_hit.collider.name.Equals("Jumba")) {

				// check if is in the distance
				if(_hit.distance >= 20.0f){

					// if spent the all time, so now it can to shoot
					if((lastShotTime + maxShotTime) < Time.time){
						
						_return = true;
						
					}

				}

			}

		}

		return _return;

	}

	private void onShoot(){

		this.playAudioEffect(audioEffects[1]);

		for(int i = 0; i < 2; i++){
			
			// gets the direction of shoot
			Vector3 _bulletDirection = shootBase.TransformDirection(Vector3.right);
			
			// defines different directions to each bullet
			if(i == 0){
				
				_bulletDirection += new Vector3(0f, -0.05f, 0f);
				
			}
			else{
				
				_bulletDirection += new Vector3(0f, 0.05f, 0f);
				
			}
			
			// create a rotation according this direction
			Quaternion _rotation = Quaternion.LookRotation(_bulletDirection);
			
			// Instantiate the projectile with this rotation
			Transform _bullet = Instantiate(projectile, shootBase.position, new Quaternion(0, 0, _rotation.z, _rotation.w)) as Transform;

			// turn image of projectile according side of Exterminator
			if (ownTransform.rotation.y == 0f){
				
				_bullet.GetComponent<tk2dSprite>().FlipX = true;
				
			}
			
			// create a velocity like direction * force;
			Vector3 _velocity = _bulletDirection.normalized * shootForce;
			
			_bullet.gameObject.GetComponent<EnemylsCollision>().father = ownTransform.gameObject;
			
			_bullet.rigidbody.AddForce(_velocity, ForceMode.Impulse);
			
			_bullet.rigidbody.velocity = _velocity;
			
		}
			
	}

	public void setCurrentState(MovementState newState, float min, float max){

		// deactivate for all animation
		this.setCollisionActivation(false);

		currentState = newState;

		switch(currentState){

			case MovementState.presentation : {

				this.setCollisionActivation(false);
				
				if(Random.Range(0, 2) == 0){
					
					animator.SetInteger("animation", 3);
					
				}
				else{
					
					animator.SetInteger("animation", 1);
					
				}
				
				// regulates the time to stop doing a presentation
				maxTime = Random.Range(min, max);
				
				lastTime = Time.time;
				
				break;
			}
			
			case MovementState.idleHidden : {

				animator.SetInteger("animation", 0);
								
				// regulates the time to start running slowly after being idleHidden
				maxTime = Random.Range(min, max) * degreeOfDifficulty;
				lastTime = Time.time;
				
				break;
			}
			
			case MovementState.idle : {
							
				animator.SetInteger("animation", 0);
				
				// regulates the time to start running fast after being idle
				maxTime = Random.Range(min, max) * degreeOfDifficulty;
				lastTime = Time.time;

				break;
			}

			case MovementState.driveFast : {

				// activate for this animation
				this.setCollisionActivation(true);

				animator.SetInteger("animation", 1);
				
				if(Random.Range(0, 2) == 0){

					animator.SetInteger("animation", 3);

				}

				break;
			}

			case MovementState.driveSlowly : {

				// activate for this animation
				this.setCollisionActivation(true);

				animator.SetInteger("animation", 1);

				// regulates the time to be idle after driving slowly
				maxTime = Random.Range(min, max) * degreeOfDifficulty;
				lastTime = Time.time;
				
				break;
			}

			case MovementState.leadingAttack : {
	
				animator.SetTrigger("apanhando");

				break;
			}

			case MovementState.angry : {
				
				// activate for this animation
				this.setCollisionActivation(true);
				
				if(Random.Range(0, 2) == 0){
					
					animator.SetInteger("animation", 3);

				}
				else{
					
					animator.SetInteger("animation", 1);
					
				}

				// regulates the time to drive fast after being angry
				maxTime = Random.Range(min, max);
				lastTime = Time.time;
			
				break;
			}

			case MovementState.attacking : {
				
				animator.SetInteger("animation", 2);

				float _animationTime = 2.34f;

				// regulates the time for the next shoot
				maxShotTime = (Random.Range(min, max) * degreeOfDifficulty) + _animationTime;
				lastShotTime = Time.time;

				this.playAudioEffect(audioEffects[0]);
			
				break;
			}

			case MovementState.dead : {

				animator.SetTrigger("morrendo");

				break;
			}

		}
		
	}

	// method used to explode the Exterminator
	private void onExplode(){

		damagePlayer();

		this.playAudioEffect(audioEffects[5]);

		Destroy(GameObject.Find("EnemyLife"));

		CameraFollow _camera = Camera.main.GetComponent<CameraFollow>();

		_camera.restoreInitialLimit();
		_camera.restoreFinalLimit();

		Director.sharedDirector().restoreLastBackgroundAudio();
		
	}

	private void damagePlayer(){

		Transform _player = GameObject.Find("Jumba").transform;
		
		// check the distance of player to hit him according this
		float _distance = Vector3.Distance(_player.position, transform.position);
		
		if(_distance <= 20.0f){
			
			_player.GetComponent<ControllerLifePlayer>().RemoveLifePlayer(damageOfExplosion / _distance);
			
			_player.GetComponent<CharacterController>().Move(new Vector3(explosionForce / _distance, 
			                                                            (explosionForce / _distance), 0.0f) * recoilSpeed);
			
		}
		
	}

	private void onAttackEnds(){

		this.setCurrentState(MovementState.idle, 1.5f, 2.5f);

	}
	
	private void onLeadingAttackEnds(){

		if(currentState != MovementState.dead && currentState != MovementState.angry && currentState != MovementState.presentation){

 			this.setCurrentState(MovementState.idle, 0.0f, 0.0f);

		}

	}

	private void onRun(){

		this.playAudioEffect(audioEffects[3]);
		
	}
	
	private void onWheelie(){

		this.playAudioEffect(audioEffects[2]);
		
	}

	private void onIdle(){

		this.playAudioEffect(audioEffects[4]);
		
	}
		
	private void playAudioEffect(AudioClip effect){

		if(!Director.sharedDirector().isPlayingEffect(effect.name)){

			Director.sharedDirector().playEffect(effect);

		}

	}

	public MovementState getCurrentState(){

		return this.currentState;

	}

	// method to activate and deactivate the collision to attack Jumba
	private void setCollisionActivation(bool parm){

		collision.enabled = parm;

	}

	public void setDegreeOfDifficulty(float value){

		this.degreeOfDifficulty = value;

	}

}

public enum MovementState {
	presentation,
	idleHidden,
	idle,
	driveFast,
	driveSlowly,
	leadingAttack,
	angry,
	dead,
	attacking
}