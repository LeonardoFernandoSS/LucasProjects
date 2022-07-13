using UnityEngine;
using System.Collections;

public class ButtonSoundController : MonoBehaviour {

	public static void checkButtons(){

		GameObject _audioBackground = GameObject.Find("SoundButton");

		if(_audioBackground == null){

			_audioBackground = GameObject.Find("SOM");

		}

		GameObject _audioEffect = GameObject.Find("SoundEffectsButton");

		if(_audioEffect == null){
			
			_audioEffect = GameObject.Find("SOMEFEITOS");
			
		}

		if(_audioBackground != null){

			_audioBackground.GetComponent<tk2dSprite>()
				.SetSprite(Director.sharedDirector().isAudioBackgroundMute() ? "somOff" : "somOn");
			
		}

		if(_audioEffect != null){
			
			_audioEffect.GetComponent<tk2dSprite>()
				.SetSprite(Director.sharedDirector().isAudioEffectsMute() ? "somEfeitosOFF" : "somEfeitosON");
			
		}

	}

}