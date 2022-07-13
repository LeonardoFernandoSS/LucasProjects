using UnityEngine;
using System.Collections;

public class RobotArms : MonoBehaviour {

	public Animator anim;
	public RobotControl RobotControl;


	void OnEnable () {

		RobotControl.downArms += downArms;
		RobotControl.frontArms += frontArms;

	}

	void OnDisable () {

		RobotControl.downArms -= downArms;
		RobotControl.frontArms -= frontArms;

	}
	
	void frontArms () {

		anim.SetInteger("Arms", 2);
		
	}
	
	void downArms () {
		
		anim.SetInteger("Arms", 1);
		
		StartCoroutine(RobotControl.moveRobotBody(RobotControl.time));
		
	}
}

