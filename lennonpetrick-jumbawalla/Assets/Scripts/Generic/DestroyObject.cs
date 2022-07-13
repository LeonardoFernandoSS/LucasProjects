using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour {

	public float time;

	void Update () {
		Destroy(this.gameObject, time);
	}
}
