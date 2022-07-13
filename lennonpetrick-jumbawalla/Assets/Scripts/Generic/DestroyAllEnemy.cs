using UnityEngine;
using System.Collections;

public class DestroyAllEnemy : MonoBehaviour {

	void OnTriggerEnter ( Collider other ) {

		if(other.collider.name == "Jumba"){

			foreach( GameObject units in GameObject.FindGameObjectsWithTag("Enemy") ) {


				if(units.transform.name == "Enemy - Fight" || units.transform.name == "Enemy - Collision"){
					Destroy ( units.gameObject );
				}

			}
		}
	}
}
