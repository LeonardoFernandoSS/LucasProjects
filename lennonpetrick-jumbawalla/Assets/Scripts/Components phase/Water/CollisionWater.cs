using UnityEngine;
using System.Collections;

public class CollisionWater : MonoBehaviour {

	[SerializeField] private GameObject prefabWater;

	private GameObject prefab;

	void Update() {
		if(prefab != null) {
			Destroy(prefab, 2.0f);
		}
	}

	void OnTriggerEnter ( Collider other ) {

		if(other.tag.Equals("Player") || other.tag.Equals("Enemy")) {
			Vector3 _position = other.transform.position;

			prefab = Instantiate(prefabWater, _position, Quaternion.identity) as GameObject;

			prefab.name = "CollisionWater";
		}
	}
}
