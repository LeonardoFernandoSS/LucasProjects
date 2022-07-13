using UnityEngine;
using System.Collections;

public class DestroyPlataformFalling : MonoBehaviour {

	void OnTriggerEnter(Collider other) {

		Destroy( other.gameObject );

	}
}
