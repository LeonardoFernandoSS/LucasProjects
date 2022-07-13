using UnityEngine;
using System.Collections;

public class JumpDinoTutorial : MonoBehaviour {

	private bool slowJump;

	private GameObject 	player,
						dino,
						jump;

	public GameObject prefabsJump;


	private TouchRight touch;
	private TouchLeft touchLeft;

	void Start() {
	//	PlayerPrefs.DeleteKey("tutorial");

		this.slowJump = true;
		// Se o tutorial ja estiver completo 
		if(SaveSystem.loadString("tutorial") == "tutorial") {

			jump = GameObject.Find("JumpTutorial");
			Destroy ( jump );
			Destroy (prefabsJump.GetComponent<ActiveJumpDinoTutorial>());
			Destroy ( gameObject );

		}

		else {
			// Get Components
			touch = GameObject.Find("DirTouch").GetComponent<TouchRight>();
			touchLeft = GameObject.Find("EsqTouch").GetComponent<TouchLeft>();
			jump = GameObject.Find("JumpTutorial");
			jump.SetActive(false);
		}
	}

	public void init( Collider other ) {

		player = other.gameObject;
		dino = GameObject.Find("DinoCorre-01(Clone)");

	}

	void Update() {

		if(dino != null ){

			if(slowJump && Vector3.Distance(player.transform.position, dino.transform.position) <= 30) {

				Time.timeScale = 0f;

				jump.SetActive(true); // visible plaque

				touch.CanAttack = false; // Desactive the Attack Touch

				touchLeft.CanMove = false; // Desactive the Moviment Touch

				slowJump = false; 
			}

			else if(Time.timeScale == 0) {

				print("Estou aqui");

				if( touch.isJumpTouch() )  {

					Time.timeScale = 0.2f;
				}

			}
			else if(Time.timeScale == 0.2f) {

				touch.CanAttack = true; // Active the attack Touch
				touchLeft.CanMove = true; // active the moviment touch

				if(player.GetComponent<CharacterController>().isGrounded) { // player is on the ground

					Time.timeScale = 1.0f; // timeScale game returned to normal

					SaveSystem.saveString("tutorial", "tutorial"); // tutorial complete

					Destroy (jump); // Destroy gameobject ( jump )

					Destroy (GetComponent<JumpDinoTutorial>()); // destory myself<Component>

				}
			}
		}
	}
}
