using UnityEngine;
using System.Collections;

public class AssignTag : MonoBehaviour {

	private GameObject[] tags;


	void Start() {

		tags = new GameObject[2];

		tags[0] = GameObject.Find("purreteJumba");
		tags[1] = GameObject.Find("peEsqJumba");

		for ( int i = 0; i < tags.Length; i++ ) {

			tags[i].transform.tag = "AttackJumba";

		}

	}
}
