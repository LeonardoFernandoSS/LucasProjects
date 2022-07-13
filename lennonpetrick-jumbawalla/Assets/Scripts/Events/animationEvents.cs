using UnityEngine;
using System.Collections;

public class animationEvents : MonoBehaviour {

	public void displayAudio( AudioClip _audio) {

		Director.sharedDirector().playEffect(_audio);
	
	}
	void Destroy()
	{
		Destroy( this.gameObject );
	}

}
