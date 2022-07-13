using UnityEngine;
using System.Collections;

public class DestroyFlyingDinosaur : MonoBehaviour {

	void OnTriggerEnter ( Collider other ) { 

		if(other.name.Equals("PassaroPlataforma") || other.name.Equals("PassaroPlataforma(Clone)")) {

			Destroy( other.gameObject );

		}
	}
}
