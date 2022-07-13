using System;
using UnityEngine;
using System.Collections;


public class RobotControl : MonoBehaviour {

	public static event Action frontArms;
	public static event Action downArms;
	public static event Action fireHead;
	public static event Action fireHeadMove;
	public static event Action missilesHead;
	public static event Action laserHead;

	public BoxCollider[] onColliders;
	public StageBoss stageBoss;

	public float speedBody;
	public float time;
	public float timeToBeat;

	private bool moveBody;
	private bool onFire;
	private int currentID;
	private float timeIdle;

	public RobotSubLife braco;
	public RobotSubLife missiles;
	public RobotSubLife brain;
	public RobotSwapTexture explosion;
	
	void Start() {

		this.currentID = UnityEngine.Random.Range(0,2);

		this.stageBoss = StageBoss.APRESENTATION;

		this.timeIdle = 3;

		this.chargeRoutine();

	}

	void chargeEvent () {


		StartCoroutine( startToBeat( timeToBeat));
	
	}

	void chargeRoutine () {

		if(stageBoss != StageBoss.NULL) {

			if(stageBoss == StageBoss.APRESENTATION) {

				GetComponent<Animator>().SetInteger("Arms", 7);

			}
			else if(stageBoss == StageBoss.STAGE_ONE) {

				if(currentID == 0) {

					fireHead();

					this.currentID = 1;

				}
				else if (currentID == 1) {

					downArms();

					this.currentID = UnityEngine.Random.Range(0,2);

				}

			}else if (stageBoss == StageBoss.STAGE_TWO) {

				if(this.currentID == 0) {

					if(this.braco.getSubLife() > 0) {

						frontArms();

						this.currentID = UnityEngine.Random.Range(0,3);
					}
					else {

						this.currentID = UnityEngine.Random.Range(0,3);

						this.chargeRoutine ();

					}
				
				}
				else if (this.currentID == 1) {

					if(this.missiles.getSubLife() > 0) {

						missilesHead();

						this.currentID = UnityEngine.Random.Range(0,3);
					}
					else {

						this.currentID = UnityEngine.Random.Range(0,3);

						this.chargeRoutine ();
					}
					
				}else if( this.currentID == 2){

					fireHead();

					this.currentID = UnityEngine.Random.Range(0,3);
				}

			}
			else if (stageBoss == StageBoss.STAGE_TREE) {

				if(this.currentID == 0) {
					
					laserHead();
					
				}
	
			}
			else if (stageBoss == StageBoss.FINAL_DESTRUCTION) {

				StartCoroutine(finalStage());

			}
		}
	}

#region Threads 

	public IEnumerator moveRobotBody( float time ) {
		
		yield return new WaitForSeconds( time );

		GetComponent<Animator>().SetInteger("Arms", 0);

		this.onFire = false;

		yield return StartCoroutine(startToBeat(timeToBeat));

	}

	public IEnumerator idleRobot ( ) {

		GetComponent<Animator>().SetInteger("Arms", 0);

		yield return new WaitForSeconds( this.timeIdle );

		this.chargeRoutine();

	}

	public IEnumerator startToBeat ( float time ) {

		GetComponent<AudioSource>().Play();

		this.GetComponent<Animator>().SetInteger("Arms", 6);

		yield return new WaitForSeconds( time );

		GetComponent<AudioSource>().Stop();

		this.GetComponent<Animator>().SetInteger("Arms", 0);
	
		yield return StartCoroutine(idleRobot());

	}

	IEnumerator finalStage () {

		Destroy(GameObject.Find("EnemyLife").gameObject);

		explosion.start_explosion();

		yield return new WaitForSeconds( 5.0f );

		Director.sharedDirector().LoadLevel((int)Level.finalScene); 

	}


#endregion

#region  @@@@@@@@@@@@@@@@@@@@@@ GET SETTING @@@@@@@@@@@@@@@@@@@@@
	
	public void setMoveBody( bool value ) {
		
		this.moveBody = value;
		
	}
	public bool getMoveBody() {
		
		return this.moveBody;
		
	}
	
	public void setOnFire( bool value ) {
		
		this.onFire = value;
		
	}
	public bool getOnFire() {
		
		return this.onFire;
		
	}
	
#endregion 

#region Events Boss

	void playAudio( AudioClip audio)
	{
		Director.sharedDirector().playEffect( audio );
	}

	void posApresentation() {
		
		StartCoroutine(idleRobot());
		
		stageBoss = StageBoss.STAGE_ONE;
		
	}

	void destructionOne( ) {

		if(stageBoss == StageBoss.STAGE_ONE) {

			GetComponent<RobotLife>().secondsStage();

			GameObject.Find("EnemyLife").GetComponent<InterfaceLifeBoss>().onStartAnimation(GetComponent<RobotLife>().getLife());

			StartCoroutine(idleRobot());

			this.currentID = UnityEngine.Random.Range(0,3);

			stageBoss = StageBoss.STAGE_TWO;

		}
		else if (stageBoss == StageBoss.STAGE_TWO) {

			GetComponent<RobotLife>().treeStage();

			GameObject.Find("EnemyLife").GetComponent<InterfaceLifeBoss>().onStartAnimation(GetComponent<RobotLife>().getLife());

			this.timeIdle = 3.5f;

			StartCoroutine(idleRobot());

			this.currentID = 0;
			
			stageBoss = StageBoss.STAGE_TREE;

		}
	}

#endregion
}

public enum StageBoss {

	APRESENTATION,
	STAGE_ONE,
	STAGE_TWO,
	STAGE_TREE,
	FINAL_DESTRUCTION,
	NULL

}

