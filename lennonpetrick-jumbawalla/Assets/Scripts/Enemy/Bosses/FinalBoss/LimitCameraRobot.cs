using UnityEngine;
using System.Collections;

public class LimitCameraRobot : MonoBehaviour {

	public Transform initLimitCamera;
	public Transform finallyLimitCamera;

	private CameraFollow cameraFollow;

	void Start() {

		this.cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();

	}

	void LateUpdate () {

		cameraFollow.initialLimit = initLimitCamera.position.x;
		cameraFollow.finalLimit = finallyLimitCamera.position.x;

	}
}
