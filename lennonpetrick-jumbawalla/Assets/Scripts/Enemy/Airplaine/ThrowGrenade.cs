using UnityEngine;
using System.Collections;

public class ThrowGrenade : MonoBehaviour {

	private float 	forceX, 
					forceY,
					distance,
					gravityModify;

	void Start () {

		this.gravityModify = 80;
		rigidbody.AddForce(new Vector3(this.forceX * this.distance , this.forceY * this.distance * 2, 0), ForceMode.Impulse);

	}
	void FixedUpdate() {

		rigidbody.AddForce(-Vector3.up * gravityModify, ForceMode.Acceleration);

	}
	public void addForceGrenade( float force_x, float force_y, float distance ) {
		this.forceX = force_x;
		this.forceY = force_y;
		this.distance = distance;
	}
}
