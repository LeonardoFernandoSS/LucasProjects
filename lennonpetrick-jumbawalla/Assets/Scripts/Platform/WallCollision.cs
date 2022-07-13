using UnityEngine;
using System.Collections;

public class WallCollision : MonoBehaviour {
	
	private GameObject player;
	
	private Transform lastParent;
			
	void Start () {
		
		player = GameObject.Find("Jumba");

		this.lastParent = player.transform.parent;
		
	}

	void OnTriggerEnter (Collider collision){	

		if(collision.name.Equals("Jumba")) {	

			collision.transform.parent = this.lastParent;

		}							
	}

	void OnTriggerStay (Collider collision){	
		
		if(collision.name.Equals("Jumba")) {	
			
			collision.transform.parent = this.lastParent;
			
		}							
	}

}