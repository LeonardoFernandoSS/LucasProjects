using UnityEngine;
using System.Collections;

public class StartedWoodenBoat : MonoBehaviour {

	public WoodenBoat boat;

	void OnTriggerEnter( Collider other ) {
		if(other.name.Equals("Jumba")) {

			boat.activeDriveBoat();

			Destroy( gameObject );

		}
	}

}
