using UnityEngine;
using System.Collections;

public class RobotLaser : MonoBehaviour {

	private Transform player;
	private Transform initTransform;
	private bool onLaser;
	private float speedLaser;

	public float currentTime,	
				 waitingTime;

	public Animator anim;
	public RobotControl robotControl;
	public float speed;
	public ParticleSystem laser;


	void Start() {

		this.speedLaser = speed;

		this.player = GameObject.Find("Jumba").transform;

		this.initTransform = gameObject.transform;

		this.onLaser = false;

		this.laser.enableEmission = false;

	}

	void OnEnable() {
		
		RobotControl.laserHead += laserHead;
		
	}
	
	void OnDisable() {
		
		RobotControl.laserHead -= laserHead;
		
	}
	
	void laserHead () {

		GetComponent<AudioSource>().Play();

		if(player.transform.position.x < transform.position.x)
			this.speedLaser = this.speed;

		else if(player.transform.position.x >= transform.position.x)
			this.speedLaser = -this.speed;

		this.currentTime = 0;

		this.anim.SetInteger("Arms", 9);

		this.onLaser = true;

		this.laser.enableEmission = true;

		GetComponent<BoxCollider>().enabled = true;

	}

	void Update() {

		if(this.onLaser) {

			transform.Rotate (0, 0 ,speedLaser * Time.deltaTime);

			if(currentTime >= waitingTime) {
				resetLaser();
			}

			this.currentTime += Time.deltaTime;

		}
	}

	void resetLaser() {

		GetComponent<AudioSource>().Stop();

		this.onLaser = false;
		
		this.laser.enableEmission = false;

		this.speedLaser = 0.0f;

		this.transform.localRotation = new Quaternion(0,0,0,0);

		this.currentTime = 0.0f;

		GetComponent<BoxCollider>().enabled = false;

		StartCoroutine(robotControl.idleRobot());

	}
}
