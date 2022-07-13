using UnityEngine;
using System.Collections;

public class ActiveWater : MonoBehaviour {

	public RasingWater water;

	void OnTriggerEnter( Collider other ) {

		if(other.name.Equals("Jumba")) {
			water.activeWater();
		}

	}

}
