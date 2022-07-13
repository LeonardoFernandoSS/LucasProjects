using System;
using UnityEngine;
using System.Collections;

public class DragonHead : MonoBehaviour {

#region publics variables

	public Animator animator;

	public AudioClip rainAudio;

	public LayerMask layerMask; // Used to raycast only the layer required

	[NonSerialized]
	public Transform player;

	public float delayRotation = 2.0f,
				 attackVelocity = 200.0f,
				 maxDistanceBite = 10.0f,
				 maxTimeToComeBack = 4.0f, // wait time to come back after attack
				 rayDistance = 200.0f, // Distance of ray
				 timeDropRay = 2.0f; // dropping ray time
	
#endregion
		
#region privates variables
	
	private Quaternion rotation;

	// parameters to active actions of the dragon
	private bool canLook = false,
				 canAttack = false,
				 canDropRay = false,
				 hitRay = false;

	private Vector3 direction; 

	private Transform rayTransform;

	private ParticleSystem particle;

	private Collider collider;
	
	private EnemylsCollision enemylsCollision;

	private LineRenderer ray;

	private float startTimeDropRay; // start of the dropping ray time

	private MovimentControl playerMovement; // player's state

#endregion

#region events
	
	public static event Action<Transform> goBackEvent; // Event to do dragon goes back
	
#endregion

	void Start () {

		player = GameObject.Find("Jumba").transform;

		playerMovement = player.GetComponent<MovimentControl>();

		rayTransform = transform.GetChild(0).GetChild(1);

		particle = rayTransform.GetComponent<ParticleSystem>();

		collider = rayTransform.GetComponent<Collider>();

		enemylsCollision = rayTransform.GetComponent<EnemylsCollision>();

		ray = rayTransform.GetComponent<LineRenderer>();

	}

	void Update () {

		if(canLook){

			// get distance
			direction = (player.position - transform.position).normalized;
			
			// look to the distance
			rotation = Quaternion.LookRotation(direction);

			// change the rotation of dragon head
			Quaternion _auxRotation = Quaternion.Slerp(transform.localRotation, rotation, this.delayRotation * Time.deltaTime);

			// fix the rotation and throw to the transform.localRotation
			transform.localRotation = new Quaternion(0, 0, _auxRotation.z, _auxRotation.w);

			RaycastHit _hit;

			// Fires raycast forward of dragon's mouth
			if (Physics.Raycast(rayTransform.position, rayTransform.TransformDirection(Vector3.right), out _hit, rayDistance, layerMask)){

				// if hit the player
				if (_hit.collider.name.Equals("Jumba") && !playerMovement.isJumping) {

					this.canLook = false; // stop

					collider.enabled = true;

					// call Attack type
					if(UnityEngine.Random.Range(0, 2) == 0){

						this.attack();
					
					}
					else{

						this.dropRay();

					}

				}

			}

		}

		// here the dragon will attack the player
		if(canAttack){

			RaycastHit _hit;

			try{

				// Fires raycast forward of dragon's mouth
				if (Physics.Raycast(rayTransform.position, rayTransform.TransformDirection(Vector3.right), out _hit, rayDistance)){

					if(Vector3.Distance(_hit.point, transform.position) > maxDistanceBite){
						
						Vector3 _auxDirection = this.direction * this.attackVelocity * Time.deltaTime;
						
						transform.position += new Vector3(_auxDirection.x, _auxDirection.y, 0.0f);
						
					}
					else{
						
						throw new Exception();
						
					}

				}
				else{

					throw new Exception();

				}

			}
			catch(Exception ex){

				collider.enabled = false;

				animator.SetTrigger("CloseMouth");

				this.canAttack = false;
				
				StartCoroutine(waitToGoBack());

			}

		}

		// here the dragon will drop ray
		if(canDropRay){

			if(Time.time < (startTimeDropRay + timeDropRay)){

				try{

					RaycastHit _hit;

					// Fires raycast forward of dragon's mouth
					if (Physics.Raycast(rayTransform.position, rayTransform.TransformDirection(Vector3.right), out _hit, rayDistance, layerMask)){
					
						// if hit the player
						if (_hit.collider.name.Equals("Jumba")) {

							if(rainAudio != null){
								
								Director.sharedDirector().playEffect(rainAudio);
								
							}

							particle.enableEmission = true;

							// create a point with the distance of object that ray hit + 550% of it
							Vector3 _point = new Vector3(_hit.distance + this.getPercent(_hit.distance, 550.0f), 0, 0);

							// create a ray with this point
							ray.SetPosition(1, _point);

							if(!hitRay){

								hitRay = true;

								PlayerControl.leadAttack(enemylsCollision.damagePercent, enemylsCollision.returnRotate(), 
								                         enemylsCollision.timeForcePush, enemylsCollision.speedPush, 
								                         enemylsCollision.speedPushY);

							}

						}
						else{ // didn't hit the player
												
							throw new Exception();
							
						}
						
					}
					else{ // didn't hit nothing
										
						throw new Exception();
						
					}

				}
				catch(Exception ex){
					
					// create the ray with rayDistance + 30% of it
					ray.SetPosition(1, new Vector3(rayDistance + this.getPercent(rayDistance, 80.0f), 0, 0));
					
				}

			}
			else{

				particle.enableEmission = false;

				collider.enabled = false;

				canDropRay = false;

				// now ray can hit again
				hitRay = false;

				// reset the ray
				ray.SetPosition(1, Vector3.zero);

				animator.SetTrigger("CloseMouth");

				// call the method to go back
				StartCoroutine(waitToGoBack());

			}

		}
	
	}

	public void searchVictim(){

		this.canLook = true;

	}

	private void attack(){

		this.canAttack = true;

		animator.SetTrigger("OpenMouth");

	}

	private void dropRay(){

		// gets the current time
		startTimeDropRay = Time.time;

		animator.SetTrigger("OpenMouth");
		
		this.canDropRay = true;
		
	}

	private IEnumerator waitToGoBack(){

		// wait a time
		yield return new WaitForSeconds(UnityEngine.Random.Range(2.0f, maxTimeToComeBack));

		// becomes ready to hit
		enemylsCollision.setReadyToHit(true);

		// gets the last body
		Transform _newHead = transform.parent.FindChild("FBody");

		// pass this new head to DragonBodyMovement
		transform.parent.GetComponent<DragonBodyMovement>().setHead(_newHead);

		// and pass to the event
		goBackEvent(_newHead);

	}

	// Method to return a percent
	private float getPercent(float value, float percent){
		
		return (percent / 100.0f) * value;
		
	}

}