using UnityEngine;
using System.Collections;

public class TutorialBehaviour : MonoBehaviour {

	public GameObject lateTutorial;

	private GameObject[] arrayTutorial;
	private GameObject currenteTutorial, lastTutorial;
	private bool activeCoroutine;

	private static int indexTutorial;
	private static bool activeTutorial;

	private CameraFollow  camera;
	private int countTouch;

	public static bool ActiveTutorial {

		get {
			return activeTutorial;
		}
		set {
			activeTutorial = value;
		}

	}

	public static int IndexTutorial {

		get {
			return indexTutorial;
		}
		set {
			indexTutorial = value;
		}

	}

	void Start () {

	//	PlayerPrefs.DeleteKey("tutorial");
		createTutorial();

	}
	

	void Update () {

		if(currenteTutorial != null) {

			currenteTutorial.SetActive(true);

			if(currenteTutorial == arrayTutorial[0]) {

				if(activeTutorial) {
					createTutorial( 3 , 3f);
				}
			}

			else if(currenteTutorial == arrayTutorial[1]) {
				
				if(activeTutorial) {
					createTutorial( 2, 1f);
				}
			}

			else if(currenteTutorial == arrayTutorial[2]) {
				
				if(activeTutorial) {
					createTutorial( 4, 1f);
				}
			}

			else if(currenteTutorial == arrayTutorial[3]) {

				if(activeTutorial) {
					createTutorial( 2, 1f);
				}

			}
			else if(currenteTutorial == arrayTutorial[4]) {

				if(activeTutorial) 
					createTutorial( 1, 1f );

			}
		}
	}

	void createTutorial() {

		activeCoroutine = true;
		
		arrayTutorial = new GameObject[5];
		
		if(SaveSystem.loadString("tutorial") == "tutorial") {


			for(int i = 0; i < arrayTutorial.Length; i++) {
				arrayTutorial[i] = GameObject.Find("DICA0"+ (i+1));
				Destroy(arrayTutorial[i]);
			}

			Destroy( gameObject );
			
		}
		else {

			Destroy( lateTutorial );
			
			for(int i = 0; i < 5; i++) {
				arrayTutorial[i] = GameObject.Find("DICA0"+ (i+1));
				arrayTutorial[i].SetActive(false);
			}

			currenteTutorial = arrayTutorial[0];
			indexTutorial = 0;
			camera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
			camera.setLockCamera(true);
		}

	}

	void createTutorial (int count, float time) {
		countTouch++;
		if(countTouch >= count) {

			if(activeCoroutine) {

				StartCoroutine( destroyTutorial(time) );
				activeCoroutine = false;
			}
		}
		activeTutorial = false;
	}

	IEnumerator destroyTutorial (float time) {

		yield return new WaitForSeconds(time);

		indexTutorial++;

		lastTutorial = currenteTutorial;
		Destroy ( lastTutorial );

		yield return new WaitForSeconds(2.0f);

		countTouch = 0;
		if(indexTutorial < arrayTutorial.Length)
			currenteTutorial = arrayTutorial[indexTutorial];
		activeCoroutine = true;
	
		if(indexTutorial >= arrayTutorial.Length) {
			camera.setLockCamera(false);
			MoveOn.search().startMoveOn(3,MOVEON.RIGHT);
			Destroy ( gameObject );
		}

	}
}
