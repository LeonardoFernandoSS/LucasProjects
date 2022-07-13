using UnityEngine;
using System.Collections;

public class InterfaceLifeBoss : MonoBehaviour {

	const float MAX_SCALE_X = 1;
	const float MAX_SCALE_Y = 1;
	const float MAX_SCALE_Z = 1;

	public Transform boss;
	public Transform myScale;

	private bool startAnimation;
	public float lifeTotal;

	public void onStartAnimation( float life) {

		this.lifeTotal = life;

		this.myScale.localScale += new Vector3 (0.1f, 0, 0);

		this.startAnimation = true;

	}

	void Start() {

		if(boss == null){

			this.boss = GameObject.Find("createBoss").GetComponent<InstantiateBoss>().prefabBoss;

		}

		EnemyLife _enemyLife = this.boss.GetComponent<EnemyLife>();

		if(_enemyLife != null){

			this.lifeTotal = _enemyLife.life;

		}
		else{

			if(boss.name.Equals("BossRobot")) {

				this.lifeTotal = GameObject.Find("BossRobot(Clone)").GetComponent<RobotLife>().getLife();

			}
			else if(boss.name.Equals("SwordBoss")) {

				this.lifeTotal = GameObject.Find("SwordBoss(Clone)").GetComponent<SwordBossLife>().getCurrentLife();

			}
			else if(boss.name.Equals("DragonScene")) {

				this.lifeTotal = this.boss.FindChild("Dragon").FindChild("Head").GetChild(0).GetComponent<DragonLife>().life;

			}
			else if(boss.name.Equals("Exterminator")) {

				this.lifeTotal = this.boss.GetChild(0).GetComponent<ExterminatorLife>().maxLife;
				
			}

		}

		this.startAnimation = true;

		myScale.localScale = new Vector3(MAX_SCALE_X - 0.9f, MAX_SCALE_Y, MAX_SCALE_Z);

	}

	void Update() {

		this.animationLifeBar();

	}

	private void animationLifeBar() {

		if(this.startAnimation) {

			if(myScale.localScale.x <= MAX_SCALE_X){

				myScale.localScale += new Vector3(myScale.localScale.x  * Time.deltaTime, 0, 0);

			}
			else {

				myScale.localScale = new Vector3(MAX_SCALE_X, MAX_SCALE_Y, MAX_SCALE_Z);

				this.startAnimation = false;

			}
		}
	}
	
	public void checkLifeBar (float life) {

		if(life >= 0){

			myScale.localScale = new Vector3 ( (life * MAX_SCALE_X) / lifeTotal, 
			                                  myScale.localScale.y, myScale.localScale.z);

		}

	}

}
