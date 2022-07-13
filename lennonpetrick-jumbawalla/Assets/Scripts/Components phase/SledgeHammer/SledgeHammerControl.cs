using UnityEngine;
using System.Collections;

public class SledgeHammerControl : MonoBehaviour {

	private bool activeHammers;

	void Start() {

		this.onHammers();

	}

	public bool getStateControl() {
		return this.activeHammers;
	}

	public void reverseStateHammers() {

		this.activeHammers = !this.activeHammers;

	}

	public void onHammers () {

		this.activeHammers = true;

	}
	public void offHammers () {

		this.activeHammers = false;

	}
}
