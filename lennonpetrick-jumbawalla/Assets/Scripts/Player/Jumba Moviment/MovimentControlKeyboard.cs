using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStateControl))]
public class MovimentControlKeyboard : MonoBehaviour {
		
	public float forwardSpeed = 4.0f, 
				 backwardSpeed = 4.0f, 
				 jumpSpeed = 16.0f,
				 timeBetweenJumps = 0.2f,
				 inAirMultiplier = 0.25f,
				 downVelocity = 2.5f,
				 mass = 10f;
	
	public int jumpQuantityMaximum = 2;
	
	private ControllerHitSequence controllerHitSequence;
		
	private PlayerStateControl stateControl; 
	
	private CharacterController character;
	
	private Vector3 velocity;						// Used for continuing momentum while in air
	
	private float directionY = 0,
				  lastJumpTime,
				  rayDistance,
				  positionZ;
	
	private bool hitHead = false,
				 jumping = false,
				 jumpingAttackPending = false;

	public bool  onIce = false,
				  isMoveSlipping = false;
	
	public float speedSlip = 4.0f;
	
	private int jumpQuantity = 0;

	private RayCastDistance raycastDistance;

	void Start () {
		
		// Cache component lookup at startup instead of doing this every frame
		this.character = GetComponent<CharacterController>();
		this.stateControl = GetComponent<PlayerStateControl>();
		this.controllerHitSequence = GetComponent<ControllerHitSequence>();
		this.raycastDistance = GetComponent<RayCastDistance>();
		
		rayDistance = (this.character.height / 4.0f) + (this.character.height / 2.0f);
		
		positionZ = transform.position.z;
		
	}
	
	void Update () { 

		float _input = Input.GetAxis("Horizontal");

		Vector3 _movement = Vector3.zero;  
	
		// if the player is not dead
		if(stateControl.getCurrentState() != PlayerState.Dead && stateControl.getCurrentState() != PlayerState.DeadFloor){


			// Apply movement from move joystick
			if (_input > 0){

				if(!stateControl.isBlowRunAnimation()){
				
					_movement += Vector3.right * this.forwardSpeed * 1;

					if(raycastDistance.getHitFloor()){

						transform.rotation = new Quaternion(transform.rotation.x, 0.0f, transform.rotation.z, transform.rotation.w);

						this.stateControl.setState(PlayerState.Running);
						
					}
					
				}
				
			}
			
			if (_input < 0){
				
				if(!stateControl.isBlowRunAnimation()){
				
					_movement += Vector3.right * this.backwardSpeed * -1;

					if(raycastDistance.getHitFloor()){
						
						transform.rotation = new Quaternion(transform.rotation.x, 180.0f, transform.rotation.z, transform.rotation.w);
					
						this.stateControl.setState(PlayerState.Running);
						
					}
					
				}
				
			}
			
			if(Input.GetMouseButtonDown(0) || Input.GetKeyDown( KeyCode.Joystick1Button3 )){
				
				if (raycastDistance.getHitFloor()){

					controllerHitSequence.addComboHit();
					
				}
				else{

					this.jumpingAttackPending = true;
													
				}
				
			}
			
			if(jumpingAttackPending){
				
				if (!stateControl.isJumpingRunning()){
						
					this.stateControl.setState(PlayerState.JumpAttacking);
					
					this.jumpingAttackPending = false;
	
				}
		
			}
					
			// Check for jump
			if (character.isGrounded){
					
				// Remove any persistent velocity after landing	and back hitHead to false, stop jumping and jumpingQuantity
				this.velocity = Vector3.zero;
				this.hitHead = false;
				this.jumping = false;
				this.jumpQuantity = 0;
					
				if(_movement == Vector3.zero && !stateControl.isBlowRunning()) {

					this.stateControl.setState(PlayerState.Idle);
					
				}
										
			}
			else{
													 			
				// Apply gravity to our velocity to diminish it over time					
				this.velocity.y += (Physics.gravity.y - this.mass) * Time.deltaTime;
												
				if(directionY > 0){
					
					if(jumpQuantity > 1){
						
						this.stateControl.setState(PlayerState.DobleJumping);
						
					}
					else{
						
						this.stateControl.setState(PlayerState.Jumping);
						
					}
									
				}
				else{
					
					if(jumping){
						
						if(!stateControl.isBlowRunning()){
						
							this.stateControl.setState(PlayerState.Falling);
							
						}
												
					}
					else{
						
						RaycastHit _ray;
						
						if(Physics.Raycast(transform.position, Vector3.down, out _ray, rayDistance)) {
							
							_movement.x += _movement.x * downVelocity;
							
						}
						else{
							
							this.stateControl.setState(PlayerState.Falling);
							
						}	
					}								
				}
					
				// Adjust additional movement while in-air
				_movement.x *= this.inAirMultiplier;
				
			}
			
			if (Input.GetKeyDown(KeyCode.Space) && ((jumpQuantity < jumpQuantityMaximum) && ((Time.time - lastJumpTime) > timeBetweenJumps))  && !stateControl.isBlowRunning() ||
			    Input.GetKeyDown(KeyCode.Joystick1Button2) && ((jumpQuantity < jumpQuantityMaximum) && ((Time.time - lastJumpTime) > timeBetweenJumps))  && !stateControl.isBlowRunning()){

				this.speedSlip = 2.0f;

				this.jumping = true;
				
				this.lastJumpTime = Time.time;
																
				// Apply the current movement to launch velocity		
				this.velocity = this.character.velocity;
				this.velocity.y = this.jumpSpeed;
				
				this.jumpQuantity++;
								
			}

			
			#region Moving To Slide

			if( _input != 0 &&  this.onIce ) {
				
				this.isMoveSlipping = true;
				
				this.speedSlip = 2.0f;
				
			}
			
			if(speedSlip <= 0) {
				
				this.isMoveSlipping = false;
				
				this.onIce = false;
				
				this.speedSlip = 2.0f;
				
			}
			
			
			#endregion

		}
		//If a player is killed he had executed his move and call up the defeat
		else {

			if(stateControl.isBlowRunning()){

				velocity.y = 0; // Caso esteja pulando e morrer ele perde sua velocidade para cair

				_movement = transform.rotation.y == 0 ? _movement = new Vector3(1 * -10, 0, 0) : _movement = new Vector3(1 * 10, 0, 0);

			}
			else{

				if(stateControl.getCurrentState() == PlayerState.Dead) {

					stateControl.setState(PlayerState.DeadFloor);

				}
			}
		}

		if( _input == 0 && this.isMoveSlipping && this.onIce) {

			if(this.stateControl.getCurrentState() != PlayerState.Attacking && this.stateControl.getCurrentState() != PlayerState.Running &&
			   this.stateControl.getCurrentState() != PlayerState.Idle && this.stateControl.getCurrentState() != PlayerState.Jumping &&
			   this.stateControl.getCurrentState() != PlayerState.Falling ) {
				
				this.stateControl.setState(PlayerState.Idle);
				
			}
			
			_movement += new Vector3 ( (transform.rotation.y == 0 ? speedSlip * Time.deltaTime * 500 : -speedSlip * Time.deltaTime * 500) , 0, 0);
			
			this.speedSlip -= Time.deltaTime;
	
		}

		_movement += new Vector3 (this.velocity.x / 2, this.velocity.y, this.velocity.z);	

		_movement += Physics.gravity;
		
		_movement *= Time.deltaTime;
		
		this.directionY = _movement.y;
							
		// Actually move the character	
		CollisionFlags _collisionFlags = this.character.Move(_movement);
		
		transform.position = new Vector3(transform.position.x, transform.position.y, this.positionZ);
		
		if (_collisionFlags == CollisionFlags.CollidedAbove && !hitHead){

			this.velocity.y = -Physics.gravity.y;
			
			this.hitHead = true;
			
		}			
	}
	

	private void onEndGame(){
			
		// Don't allow any more control changes when the game ends
		this.enabled = false;
		
	}

	#region Set Getter

	public bool isJumping {
		
		get {
			return !character.isGrounded;
		}

	}
	
	public bool OnIce {
		
		get {
			return this.onIce;
		}
		set {
			onIce = value;
		}
	}
	
	public bool IsMoveSlipping {
		
		get {
			return this.isMoveSlipping;
		}
		set {
			isMoveSlipping = value;
		}
	}
	
	#endregion
}