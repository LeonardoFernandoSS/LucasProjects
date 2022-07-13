using UnityEngine;
using System.Collections;

public class MovingBackGround : MonoBehaviour {
	
	public float speedLayerDown;
	
	public Transform layerDown;

	private bool revert;

	private float currentCount;
	public float waitingCount;

	void Start() {

		this.currentCount = 0.0f;

		this.waitingCount = 0.5f;

		this.revert = true;

	}

	void Update() {


		layerDown.position += new Vector3( 0, this.revert ? -this.speedLayerDown * Time.deltaTime : this.speedLayerDown * Time.deltaTime, 0);

		if(this.currentCount > this.waitingCount) {
			this.revert = !this.revert;
			this.currentCount = 0.0f;
		}
		else {
			this.currentCount += Time.deltaTime;
		}
	}
	
}
