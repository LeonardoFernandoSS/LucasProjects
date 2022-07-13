using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PterodactylsCollision : MonoBehaviour {
	
	public float damage = 20f;
	
	private void OnTriggerEnter(Collider collider) {
		
		if (collider.name == "Jumba"){
	
			ControllerLifePlayer _lifePlayer = collider.GetComponent<ControllerLifePlayer>();
			
			_lifePlayer.RemoveLifePlayer(damage);
			
		}
		
	}
	
}