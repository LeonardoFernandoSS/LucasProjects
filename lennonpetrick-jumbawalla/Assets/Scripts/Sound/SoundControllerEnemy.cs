using UnityEngine;
using System.Collections;

public class SoundControllerEnemy : MonoBehaviour {
	
	private bool audioControler;

	void Start(){

		this.audioControler = true;

	}

	public void startAudio( AudioClip _sound ) {

		if(this.audioControler) {

			StartCoroutine(playSound(_sound));
			this.audioControler = false;

		}

	}

	IEnumerator playSound(AudioClip _sound) {

		Director.sharedDirector().playEffect( _sound );
		
		yield return new WaitForSeconds(0.01f);
		
		this.audioControler = true;

	}

}
