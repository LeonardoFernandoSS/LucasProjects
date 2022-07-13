using UnityEngine;
using System.Collections;

public class PlatformSoftMoviment : MonoBehaviour {
	
	public float maxDown = 3.0f,
				 speed = 1.5f;
	
	private float inicialPositionY;
	private bool playerUpPlatform = false;

	public bool voltar;

	public GameObject[]	fire;

	public bool downPlatform;
	
	void Start () {
		
		this.inicialPositionY = gameObject.transform.position.y;

	}
	
	void Update () {
		// Caso a plataforma esteja marcada pra descer
		if(downPlatform) {

			if(playerUpPlatform && gameObject.transform.position.y > (inicialPositionY - maxDown)) {
				
				transform.position -= new Vector3(0, this.speed, 0) * Time.deltaTime;

				activeFire(true);

			}
			
			else if( !playerUpPlatform && gameObject.transform.position.y < inicialPositionY && voltar) {
				
				transform.position += new Vector3(0, this.speed, 0) * Time.deltaTime;

				activeFire(true);

			}

			else if (playerUpPlatform) {

				activeFire ( true );

			}
			else {

				activeFire (false);

			}
		}
		else {

			if(playerUpPlatform && gameObject.transform.position.y < (inicialPositionY + maxDown)) {
				
				transform.position += new Vector3(0, this.speed, 0) * Time.deltaTime;
				
				activeFire(true);
				
			}
			
			else if( !playerUpPlatform && gameObject.transform.position.y > inicialPositionY && voltar) {
				
				transform.position -= new Vector3(0, this.speed, 0) * Time.deltaTime;
				
				activeFire(false);
				
			}
			else if (playerUpPlatform) {
				
				activeFire ( true );
				
			}
			else {

				activeFire (false);
			}
		}
	}

	private void activeFire(bool value) {
		for (int i = 0; i < fire.Length; i++) 
			fire[i].SetActive(value);
	}

	private void OnTriggerEnter(Collider collider){
				
		if(collider.name == "Jumba"){
			
			this.playerUpPlatform = true;
											
		}
		
    }
	
	private void OnTriggerExit(Collider collider){
        
		if(collider.name == "Jumba"){
			
			this.playerUpPlatform = false;
											
		}
		
    }
				
}