using UnityEngine;
using System.Collections;

public class GSpheres : MonoBehaviour {

	void OnDrawGizmos() {

		Gizmos.color = Color.blue;

		Gizmos.DrawWireSphere(transform.position, 3);

	}

}