using UnityEngine;
using System.Collections;

public class ButtonControl : MonoBehaviour {

	public IEnumerator menu( RaycastHit _object ) {
		
		Director.sharedDirector().LoadLevelWithFade(LevelManager.getLevelID(Level.menu));

		_object.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);

		yield return new WaitForSeconds(0.3f);

		_object.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

	}

	public IEnumerator restart( RaycastHit _object) {

		Director.sharedDirector().restartScene();

		_object.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);
		
		yield return new WaitForSeconds(0.3f);
		
		_object.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

	}

	public IEnumerator resumo( RaycastHit _object ) {
		
		Director.sharedDirector().Pause();

		_object.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);
		
		yield return new WaitForSeconds(0.3f);
		
		_object.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

	}

	public IEnumerator somEfeitos( RaycastHit _object ) {

		Director.sharedDirector().muteAudioEffects();

		_object.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);
		
		yield return new WaitForSeconds(0.3f);
		
		_object.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

	}

	public IEnumerator som( RaycastHit _object ) {
		
		Director.sharedDirector().muteAudioBackground();
		
		_object.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);
		
		yield return new WaitForSeconds(0.3f);
		
		_object.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		
	}

	public IEnumerator proximo ( RaycastHit _object ) {

		Director.sharedDirector().nextLevel();
		
		_object.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);
		
		yield return new WaitForSeconds(0.3f);
		
		_object.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		
	}
}
