using UnityEngine;
using System.Collections;

public class InstantiateBoss : MonoBehaviour {

	public bool canDestroy = false; // this is able to destroy this gameObject

	public Transform prefabBoss, // gameObject que vai ter o Boss
					 positionBoss, // posiçao onde o boss vai nascer
					 colliders, // colisores para nao o boss nao passar dos limites
					 lifeBar;

	public float initialLimit; // posiçao inicial onde a camera vai iniciar
	public float finalLimit; // posiçao final onde a camera vai parar

	public AudioClip audioBoss;

	private bool trigger = true; // para ativar apenas uma vez quando bater no colisor

	void OnTriggerEnter (Collider other) {

		if(other.name.Equals("Jumba") && this.trigger ) {

			// Cria um game Object instanciando o prefab do boss
			Instantiate(prefabBoss, positionBoss.position, Quaternion.identity);

			//Vector3 _cameraPosition = Camera.main.transform.position;

			//_cameraPosition = new Vector3(this.initialLimit, _cameraPosition.y, _cameraPosition.z);

			//Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, _cameraPosition, 5.0f);

			CameraFollow _camera = Camera.main.GetComponent<CameraFollow>(); // pega o componente camera do Main Camera
			// atribui os novos valores inicial e final
			_camera.changeInitialLimit(this.initialLimit);
			_camera.changeFinalLimit(this.finalLimit);

			if(this.colliders != null) {

				Transform _colunCollider = Instantiate (colliders, 
				                                         new Vector3(this.initialLimit - 46, positionBoss.position.y, positionBoss.position.z), 
				                                         Quaternion.identity ) as Transform;
			
				Transform _colunCollider2 = Instantiate (colliders, 
				                                         new Vector3(this.finalLimit + 46, positionBoss.position.y, positionBoss.position.z), 
				                                         Quaternion.identity ) as Transform;

				_colunCollider.name = "CollidersBox";
				_colunCollider2.name = "CollidersBox";

			}

			Transform _lifeBar = Instantiate (lifeBar, lifeBar.position, Quaternion.identity) as Transform;

			_lifeBar.parent = Camera.main.transform;
			_lifeBar.name = lifeBar.name;
			_lifeBar.localPosition = lifeBar.position;

			if(audioBoss != null){

				Director.sharedDirector().changeBackgroundAudio(audioBoss);

			}

			this.trigger = false;

			if(canDestroy){

				Destroy(this.gameObject);

			}

		}
	}
}
