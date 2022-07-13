using UnityEngine;
using System.Collections;

public class EnemyMoveBird : MonoBehaviour {

	public float speed;

	private Transform myTransform;

	void Start() {

		this.myTransform = gameObject.transform;

	}

	void Update() {

		this.myTransform.position += new Vector3( -speed * Time.deltaTime, 0, 0);

	}
}
