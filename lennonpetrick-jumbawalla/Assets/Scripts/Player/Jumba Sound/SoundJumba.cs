using UnityEngine;
using System.Collections;

public class SoundJumba : MonoBehaviour {

	public AudioClip[] jumpSound;
	public AudioClip[] gettingSound;
	public AudioClip[] specialJumba;
	private bool golpeJump,
				 gettingJumba,
				 specialSoundJumba;

	void Start() {
		this.gettingJumba = true;
		this.golpeJump = true;
		this.specialSoundJumba = true;
	}

	public void startSound(int _qtdJump) {

		if(_qtdJump <= 0) {

			Director.sharedDirector().playEffect( jumpSound[0] );

		}
		else if(_qtdJump > 0 && _qtdJump <=1) {

			Director.sharedDirector().playEffect( jumpSound[1] );

		}
	}

	public void startSoundJump( AudioClip _sound ) {

		if(this.golpeJump) {
			StartCoroutine(soundJumpGolpe(_sound));
			this.golpeJump = false;
		}

	}

	IEnumerator soundJumpGolpe( AudioClip _sound ) {

		Director.sharedDirector().playEffect(_sound);

		yield return new WaitForSeconds(_sound.length);

		this.golpeJump = true;

	}

	public void startSoundGetting( AudioClip _sound ) {
		
		if(this.gettingJumba) {
			StartCoroutine(soundGetting(_sound));
			this.gettingJumba = false;
		}
		
	}

	public void startSoundGetting() {
		
		if(this.gettingJumba) {
			StartCoroutine(soundGetting(gettingSound[Random.Range(0, gettingSound.Length)]));
			this.gettingJumba = false;
		}
		
	}
	
	IEnumerator soundGetting( AudioClip _sound ) {
		
		Director.sharedDirector().playEffect(_sound);
		
		yield return new WaitForSeconds(_sound.length);

		this.gettingJumba = true;
	}


	public void startSpecial() {

		if(this.specialSoundJumba) {

			StartCoroutine(soundSpecial());
			this.specialSoundJumba = false;

		}
	}

	IEnumerator soundSpecial ( ) {

		Director.sharedDirector().playEffect(specialJumba[0]);

		Director.sharedDirector().changeBackgroundAudio(specialJumba[1]);

		float _time = GetComponent<ControllerSpecial>().timeSpecial;

		yield return new WaitForSeconds(_time);

		Director.sharedDirector().playEffect(specialJumba[2]);

		Director.sharedDirector().restoreLastBackgroundAudio();

		this.specialSoundJumba = true;

	}
}
