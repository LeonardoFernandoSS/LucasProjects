using UnityEngine;
using System.Collections;

public class DestroyParticle : MonoBehaviour {

	private float currentTime = 0;

	public float CurrentTime {
		get {
			return this.currentTime;
		}
		set {
			currentTime = value;
		}
	}
	// Update is called once per frame
	void Update () {

		this.currentTime += Time.deltaTime;

		if(this.currentTime >= particleSystem.duration + 0.5f) {
			this.currentTime = 0;
			this.gameObject.SetActive(false);
		}
		
	}
}
