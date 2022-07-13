using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	void OnTriggerEnter( Collider other ) {

		if( other.name.Equals( "Jumba" ) ) {

			other.GetComponent<ControllerLifePlayer>().setLifePlayer(0);

			Director.sharedDirector().endScene(Director.SceneEndedStatus.lost);

		}
	}
}
