using UnityEngine;
using System.Collections;

public class RobotLife : MonoBehaviour {

	private float currentRobotLife;
	public RobotControl robotControl;

	void Start() {

		firstStage();

	}

	public void updateLife( float life) {

		this.currentRobotLife -= life;

		this.checkControl();

	}

	public float getLife() {

		return this.currentRobotLife;

	}

	void Update() {

		this.checkControl();

	}

	public void firstStage() {

		this.robotControl.braco.addLife(2500);

		this.currentRobotLife = this.robotControl.braco.getSubLife();

	}

	public void secondsStage() {

		this.robotControl.braco.addLife(2000);

		this.robotControl.missiles.addLife(1000);

		this.currentRobotLife = this.robotControl.braco.getSubLife() + this.robotControl.missiles.getSubLife();

	}

	public void treeStage() {

		this.robotControl.brain.addLife(1500);

		this.currentRobotLife = this.robotControl.brain.getSubLife();

	}
	
	void checkControl() {

		if( this.currentRobotLife <= 0 ) {

			robotControl.StopAllCoroutines();

			if(robotControl.stageBoss == StageBoss.STAGE_ONE) {

				GetComponent<Animator>().SetInteger("Arms", 8);

				robotControl.onColliders[0].enabled = true;
				
				robotControl.onColliders[1].enabled = true;

			}
			else if(robotControl.stageBoss == StageBoss.STAGE_TWO) {
		
				GetComponent<Animator>().SetInteger("Arms", 10);
				
				robotControl.onColliders[0].enabled = false;
				
				robotControl.onColliders[1].enabled = false;
				
				robotControl.onColliders[2].enabled = true;

			}

			else if(robotControl.stageBoss == StageBoss.STAGE_TREE) {

				robotControl.stageBoss = StageBoss.FINAL_DESTRUCTION;

			}

			this.currentRobotLife = -1;
		}
	}
}
