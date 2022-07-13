using UnityEngine;
using System.Collections;

public class AircraftMovement : MonoBehaviour {
	
	public Transform bomb, // bomb prefab
					 projectile; // bullet prefab
	
	public bool isThrowBomb = true; // used to check if the aircraft will throw bombs or shoot
	
	public float speed = 10.0f, // velocity of aircraft
				 maxDistance = 50.0f, // the maximum distance that the aircraft will start follow and shoot
				 shootTime = 1.0f, // wait time to shoot
				 throwForce = 10.0f, // force of bomb thrown
				 shootForce = 200.0f, // Force of shooting
				 delayRotation = 4.0f, // delay of rotation
				 highToFlyDown = 1.0f; // the high that aircraft will fly down
	
	private Transform player, // used to get the distance between thw player and the aircraft
					  carcass; // used to get the child that is called by carcass
	
	private Vector3 direction, // used to define the direction of aircraft
					lastDirection,// used to save the last direction to keep the rotation on shooting
					lastDistance; // used to save the last distance to calculate when the airplane will turn to Vector.left

	private bool canFollow = true, // used to allow following the player
				 canShoot = true, // used to allow shooting
				 canFallDown = false; //used to allow the aircraft fall down when the jumba attack it

	public AudioClip atira;
	
	void Start(){
		
		player= GameObject.Find("Jumba").transform;
		
		carcass = transform.GetChild(0);

		// check if the player is right or left from aircraft to turn it according this
		if((player.position - transform.position).x < 0){
			
			carcass.localRotation = new Quaternion(carcass.localRotation.x, 0f, carcass.localRotation.z, carcass.localRotation.w);
			
		}
		else{
			
			carcass.localRotation = new Quaternion(carcass.localRotation.x, 180.0f, carcass.localRotation.z, carcass.localRotation.w);
			
		}

		// set the direction according the carcass
		direction = carcass.TransformDirection(Vector3.left);

	}
	
	void Update () {

		// gets the distance
		Vector3 _distance = new Vector3(player.position.x, player.position.y, 0) - new Vector3(transform.position.x, transform.position.y, 0);

		// if itsn't falling down
		if(!canFallDown){

			// and checks if is already in maximum
			if (_distance.magnitude <= maxDistance){

				// call the method to do the aircraft fly down
				this.flyDown();

				// check if is already time to shoot
				if(canShoot){
					
					StartCoroutine(this.shootOnPlayer());
					
					this.canShoot = false;
					
				}

				// here we can regule what the high the aircraft will turn and stop following
				if (Mathf.Abs(_distance.y) <= Mathf.Abs(lastDistance.y/highToFlyDown)){

					// set again the direction according the carcass
					direction = carcass.TransformDirection(Vector3.left);

					// and look at the position
					this.lookAt(Vector3.left);

					// if it is not to shoot does this
					if (!isThrowBomb){

						// gets the carcass's child
						Transform _carcassChild = carcass.GetChild(0);

						// used to continue looking to the last direction
						Quaternion _auxRotation = Quaternion.LookRotation(carcass.TransformDirection(lastDirection));
						
						_auxRotation = Quaternion.Slerp(_carcassChild.localRotation, _auxRotation, delayRotation * Time.deltaTime);
						
						_carcassChild.localRotation = new Quaternion(_carcassChild.localRotation.x, _carcassChild.localRotation.y, _auxRotation.z, _auxRotation.w);
						
					}
					
					canFollow = false;
					
				}
				
			}
			else{

				// if not is in the maximum then save the actualy distance
				lastDistance = _distance;
				
			}
			
		}
		else{// if it's falling down

			// gets the direction and starts to subtract the Y to the aircraft falls down
			direction = new Vector3(direction.x, direction.y - 0.01f, direction.z);

			// do the aircraft looks at the direction
			this.lookAt(direction);

			// increase the speed because the more the object falls, more increase its mass, so the speed increases too
			this.speed += 0.1f;
			
		}

		// do the aircraft run to this direction
		transform.Translate(this.direction * this.speed * Time.deltaTime);

		if(_distance.magnitude > 150.0f)
		{
			print(_distance.magnitude);
			Destroy(gameObject.transform.parent.gameObject);
		}

	}

	// method to do the aircraft fly down
	private void flyDown(){
		
		if(canFollow){
			
			Vector3 _distance = (player.position - transform.position).normalized;
			
			direction = _distance;
			
			this.lookAt(_distance);
			
			lastDirection = direction;
			
		}
		
	}

	// this method will check if is to shoot or throw the bombs
	private IEnumerator shootOnPlayer(){
		
		yield return new WaitForSeconds(shootTime);
		
		if (isThrowBomb){
			
			this.throwBomb();
			
		}
		else{
			
			StartCoroutine(this.shootWithGun());
			
		}
		
		this.canShoot = true;
		
	}

	// method to throw the bombs
	private void throwBomb(){
		
		Transform _bomb = Instantiate(bomb, transform.position, Quaternion.identity) as Transform;
		
		_bomb.rigidbody.AddForce(-_bomb.transform.up * throwForce * throwForce);
		
	}

	// method to shoot
	private IEnumerator shootWithGun(){

		// this FOR is used to fill the information for each bullet
		for(int i = 0; i < 8; i++) {
			
			Transform _shootBase = carcass.GetChild(0);

			// this is to create the bullet each time in diferent position
			if (i % 2 == 0){
				
				_shootBase = _shootBase.GetChild(0);
				
			}
			else{
				
				_shootBase = _shootBase.GetChild(1);
				
			}

			// gets the direction of shoot
			Vector3 _bulletDirection = _shootBase.TransformDirection(Vector3.left);
			
			// create a rotation according this direction
			Quaternion _rotation = Quaternion.LookRotation(_bulletDirection);
			
			// Instantiate the projectile with this rotation
			Transform _bullet = Instantiate(projectile, _shootBase.position, new Quaternion(0, 0, _rotation.z, _rotation.w)) as Transform;
			
			// turn image of projectile according side of aircraft
			if (carcass.rotation.y == 1f){
				
				_bullet.GetComponent<tk2dSprite>().FlipX = true;
				
			}
			
			// create a velocity like direction * force;
			Vector3 _velocity = _bulletDirection.normalized * shootForce;
			
			_bullet.gameObject.GetComponent<EnemylsCollision>().father = carcass.gameObject;
			
			_bullet.rigidbody.AddForce(_velocity, ForceMode.Impulse);
			
			_bullet.rigidbody.velocity = _velocity;

			Director.sharedDirector().playEffect(atira);
			
			yield return new WaitForSeconds(0.01f); // wait a time to create each bullet
			
		}
		
	}

	// method used to look at position
	private void lookAt(Vector3 target){
		
		Quaternion _auxRotation = Quaternion.LookRotation(target);
		
		_auxRotation = Quaternion.Slerp(transform.rotation, _auxRotation, delayRotation * Time.deltaTime);
		
		transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, _auxRotation.z, _auxRotation.w);
		
	}
	
	public bool CanFallDown {
		
		get {
			
			return this.canFallDown;
			
		}
		set {
			
			canFallDown = value;
			
		}
		
	}
	
}