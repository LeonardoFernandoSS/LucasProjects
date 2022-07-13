using UnityEngine;
using System.Collections;

public class GatesCollision : MonoBehaviour {

	public Gates gates;

	public bool openDoor;
	public bool reverseDoor;

	void OnTriggerEnter ( Collider other ) {

		if(this.reverseDoor) {
			if( other.name.Equals( "Jumba" ) ) {

				gates.reverseDoor();
			}
		}

		else if (!this.reverseDoor){
			 if( other.name.Equals( "Jumba" ) ) {
				
				if(this.openDoor) 
					gates.openDoor();
				
				else 
					gates.lockDoor();
			}
		}
	}
}
