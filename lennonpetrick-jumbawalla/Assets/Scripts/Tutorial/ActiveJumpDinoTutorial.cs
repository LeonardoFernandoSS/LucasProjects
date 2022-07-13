using UnityEngine;
using System.Collections;

public class ActiveJumpDinoTutorial : MonoBehaviour {

	public JumpDinoTutorial tutorial;
	
	void OnTriggerEnter( Collider other ) {
		if(other.name.Equals("Jumba")) {
			tutorial.init(other);
		}
	}

}
