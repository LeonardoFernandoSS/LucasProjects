using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	public float timeToExplode = 2.0f, 
				 damage = 30.0f,
				 explosionForce = 2.0f,
				 recoilSpeed = 10.0f;

	public Transform explosionEffect;
	public AudioClip explosionAudio;

	private GameObject player;
	
	void Start(){

		player = GameObject.Find("Jumba");

		StartCoroutine(waitToExplode());
		
	}
	
	private IEnumerator waitToExplode(){
		
		yield return new WaitForSeconds(this.timeToExplode);

		this.explode();

	}

	private void explode(){

		Transform _explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity) as Transform;

		Destroy(_explosion.gameObject, 1.0f);

		Director.sharedDirector().playEffect(explosionAudio);

		float _distance = Vector3.Distance(player.transform.position, transform.position);
		
		if(_distance <= 20.0f){

			player.GetComponent<ControllerLifePlayer>().RemoveLifePlayer(damage / _distance);

			player.GetComponent<CharacterController>().Move(new Vector3(explosionForce / _distance, 
			                                                            (explosionForce / _distance), 0.0f) * recoilSpeed * Time.deltaTime);

		}

		Destroy(this.gameObject);

	}

}