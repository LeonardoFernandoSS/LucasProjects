using UnityEngine;
using System.Collections;

public class PlatformFalling : MonoBehaviour {

	public float speedPlatform;

	private bool movePlatform;

	void Start( ) {

		this.movePlatform = false;

	}

	void Update() {

		if( this.movePlatform ) {

			transform.position += new Vector3( 0  , -this.speedPlatform * Time.deltaTime , 0 );

			this.speedPlatform = this.speedPlatform + Time.deltaTime + 0.5f;

			Destroy( this.gameObject, 10);

		}
	}

	void OnTriggerStay( Collider other ) {

		if(other.name.Equals("Jumba") && other.GetComponent<CharacterController>().isGrounded) {

			if(!this.movePlatform)
				this.movePlatform = true;

		}
	}
}
