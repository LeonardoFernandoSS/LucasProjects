using UnityEngine;
using System.Collections;

public class RobotMissel : MonoBehaviour {

	public Transform[] positionMissiles;
	public Transform missiles;

	public int amountMissiles;
	public RobotControl RobotControl;
	public AudioClip missel;

	void OnEnable() {

		RobotControl.missilesHead += missilesHead;

	}

	void OnDisable() {

		RobotControl.missilesHead -= missilesHead;

	}

	void missilesHead (){

		StartCoroutine(startMissiles());

	}

	IEnumerator startMissiles() {

		RobotControl.GetComponent<Animator>().SetInteger("Arms", 4);

		yield return new WaitForSeconds(2);

		for (int i = 0; i < amountMissiles; i++){

			Director.sharedDirector().playEffect(missel);

			Instantiate( missiles, positionMissiles[0].position, positionMissiles[0].rotation );

			yield return new WaitForSeconds(2);

			Director.sharedDirector().playEffect(missel);

			Instantiate( missiles, positionMissiles[1].position, positionMissiles[1].rotation );


			yield return new WaitForSeconds(3);

		}

		yield return StartCoroutine(RobotControl.idleRobot());
	}
}
