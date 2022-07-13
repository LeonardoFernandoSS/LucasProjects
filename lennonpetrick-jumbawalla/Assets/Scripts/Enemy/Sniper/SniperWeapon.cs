using UnityEngine;
using System.Collections;

public class SniperWeapon : MonoBehaviour {

#region publics variables

	public LayerMask layerMask; // Used to raycast only the layer required

	public Transform projectile,
						brilho;

	public AudioClip tiro;

	public float shootTime = 2.0f, // Interval of shooting
				 shootForce = 200.0f, // Force of shooting
				 laserDistance = 200.0f; // Distance to see the player

#endregion publics variables

#region privates variables

	private float lastShootTime; // Used to mark the last time that shot

	private bool sawPlayer; // Used to check if enemy saw player

	private LineRenderer laser;

#endregion privates variables

	void Start () {

		this.laser = GetComponent<LineRenderer>();

		this.sawPlayer = false;

	}
	
	void Update () {

		RaycastHit _hit;
				
		// Fires raycast forward of weapon
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out _hit, laserDistance, layerMask)){

			// if hit the player
			if (_hit.collider.name.Equals("Jumba")) {

				this.sawPlayer = true; // Now he saw the player

				// create a point with the distance of object that ray hit + 65% of it
				Vector3 _point = new Vector3( -(_hit.distance + this.getPercent(_hit.distance, 65.0f)), 0, 0);

				// create a laser ray with this point
				this.laser.SetPosition(1, _point);

				// if is time to shoot then
				if (lastShootTime + shootTime <= Time.time){

					// mark the last time
					this.lastShootTime = Time.time;

					// and shoot passing the point like direction transformed by transform
					this.shoot(transform.TransformDirection(_point));
					
				}

			}
			else{ // didn't hit the player

				this.sawPlayer = false;

				// create the laser ray with laserDistance + 65% of it
				this.laser.SetPosition(1, new Vector3(-(laserDistance + this.getPercent(laserDistance, 65.0f)), 0, 0));

			}
			
		}
		else{ // didn't hit nothing

			this.sawPlayer = false;

			// create the laser ray with laserDistance + 65% of it
			this.laser.SetPosition(1, new Vector3(-(laserDistance + this.getPercent(laserDistance, 65.0f)), 0, 0));

		}

	}
	
	private void shoot(Vector3 direction){

		// create a rotation according direction
		Quaternion _rotation = Quaternion.LookRotation(direction);

		// Instantiate the projectile with this rotation
		Transform _bullet = Instantiate(projectile, transform.position, new Quaternion(0, 0, _rotation.z, _rotation.w)) as Transform;

		brilho.gameObject.SetActive(true);

		Director.sharedDirector().playEffect(tiro);


		// Turn image of projectile according side of enemy
		if (transform.parent.parent.rotation.y == 1f){

			_bullet.GetComponent<tk2dSprite>().FlipX = true;

		}

		// create a velocity like direction * force;
		Vector3 _velocity = direction.normalized * shootForce;

		_bullet.GetComponent<EnemylsCollision>().father = transform.parent.parent.gameObject;

		_bullet.rigidbody.AddForce(_velocity, ForceMode.Impulse);
		
		_bullet.rigidbody.velocity = _velocity;

		Invoke("waitToDeactive", 0.5f);

	}

	private void waitToDeactive(){

		brilho.gameObject.SetActive(false);

	}

	// Method to return a percent
	private float getPercent(float value, float percent){
		
		return (percent / 100.0f) * value;
		
	}

	// Property of sawPlayer
	public bool SawPlayer {
		get {
			return this.sawPlayer;
		}
		set {
			sawPlayer = value;
		}
	}

}
