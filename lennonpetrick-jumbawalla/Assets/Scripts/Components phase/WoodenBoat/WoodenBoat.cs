using UnityEngine;
using System.Collections;

public class WoodenBoat : MonoBehaviour {

	public float speedBoatX;
	public float speedBoatY;

	private bool driveBoat;
	private bool fallingBoat;
	private bool rotateBoat;

	public Transform child;

	public void activeDriveBoat() {

		this.driveBoat = true;

	}

	void Start() {

		this.speedBoatX = 10;
		this.speedBoatY = 50;
		this.driveBoat = false;
		this.fallingBoat = false;
		this.rotateBoat = false;

	}

	void Update() {

		if( this.driveBoat ) {

			transform.Translate( new Vector3 ( this.speedBoatX, 0 , 0 ) * Time.deltaTime );

		}
		else if ( this.fallingBoat ) {

			transform.Translate( new Vector3 ( this.speedBoatX , -this.speedBoatY, 0) * Time.deltaTime);

			child.Rotate( new Vector3( 0 ,0 , -17) * Time.deltaTime);

		}

		if( this.rotateBoat) {

			transform.Translate( new Vector3 ( this.speedBoatX /2 , 0 , 0 ) * Time.deltaTime );

			child.Rotate( new Vector3( 0 ,0 , -14) * Time.deltaTime);

		}
	}

	void OnTriggerEnter( Collider other) {

		if(other.name.Equals("FallingBoat")) {

			this.driveBoat = false;

			this.fallingBoat = true;

			Destroy(other.gameObject);

		}
		else if(other.name.Equals("StopBoat")) {

			this.driveBoat = false;

			this.rotateBoat = false;

			Destroy(other.gameObject);

		}
		else if(other.name.Equals("RotateBoat") ) {

			this.rotateBoat = true;

			this.driveBoat = false;

			Destroy(other.gameObject);

		}
	}
}
