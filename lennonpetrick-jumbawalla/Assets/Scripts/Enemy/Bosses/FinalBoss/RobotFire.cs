using UnityEngine;
using System.Collections;

public class RobotFire : MonoBehaviour {

	public Animator anim;
	public RobotControl RobotControl;

	private float currentTime,
				  waitingTime;

	public ParticleSystem fire;
	public BoxCollider box;

	void Start() {

		this.currentTime = 0;
		this.waitingTime = 1f;
		this.fire.enableEmission = false;

	}

	void OnEnable () {

		RobotControl.fireHeadMove += fireHeadMove;
		RobotControl.fireHead += fireHead;

	}

	void OnDisable () {

		RobotControl.fireHeadMove -= fireHeadMove;
		RobotControl.fireHead -= fireHead;

	}
	
	void fireHead (){

		anim.SetInteger("Arms", 3);

		this.currentTime = 0;

		this.waitingTime = 1f;

		this.fire.enableEmission = true;

		this.box.enabled = true;

		StartCoroutine(startFireBurn( 4 ));

	}

	void fireHeadMove () {

		anim.SetInteger("Arms", 3);

		this.currentTime = 0;
		
		this.waitingTime = 1;

		this.fire.enableEmission = true;

		this.box.enabled = true;

		RobotControl.setOnFire(true);

		StartCoroutine(RobotControl.moveRobotBody(RobotControl.time));

	}

	public IEnumerator startFireBurn ( float time ) {

		GetComponent<AudioSource>().Play();

		RobotControl.setOnFire(true);
		
		yield return new WaitForSeconds( time );

		GetComponent<AudioSource>().Stop();

		RobotControl.setOnFire(false);
		
		anim.SetInteger("Arms", 0);
		
		yield return StartCoroutine(RobotControl.idleRobot());
		
	}

	void Update() {

		if(RobotControl.getOnFire()) {

			if(this.currentTime >= this.waitingTime) {

				if(this.fire.enableEmission)  {

					GetComponent<AudioSource>().Stop();

					this.box.enabled = false;

					this.fire.enableEmission = false;

					this.waitingTime = 1.5f;

				}
				else {

					GetComponent<AudioSource>().Play();

					this.box.enabled = true;

					this.fire.enableEmission = true;

					this.waitingTime = 1.5f;

				}

				this.currentTime = 0;

			}

			if(this.currentTime < this.waitingTime) 
				this.currentTime += Time.deltaTime;

		}
		else if (!RobotControl.getOnFire()) {

			this.fire.enableEmission = false;

			this.box.enabled = false;

		}
	}
}
