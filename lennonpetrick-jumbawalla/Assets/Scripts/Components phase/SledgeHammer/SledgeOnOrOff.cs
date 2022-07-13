using UnityEngine;
using System.Collections;

public class SledgeOnOrOff : MonoBehaviour {

	public SledgeHammerControl hammerControl;

	public bool turnOn;

	void OnTriggerEnter ( Collider other ) {

		if( other.name.Equals("Jumba") ) {

			if(this.turnOn)
				hammerControl.onHammers();
			else
				hammerControl.offHammers();

		}
	}
}
