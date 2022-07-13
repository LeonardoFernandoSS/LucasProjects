using UnityEngine;
using System.Collections;

public class IceBlock : MonoBehaviour {

	void OnTriggerStay ( Collider other ) {

		if(other.name.Equals("Jumba")) {

			if(other.GetComponent<MovimentControl>())

				other.GetComponent<MovimentControl>().OnIce = true;

			if (other.GetComponent<MovimentControlKeyboard>())

				other.GetComponent<MovimentControlKeyboard>().OnIce = true;

		}
	}

	void OnTriggerExit ( Collider other ) {

		if(other.name.Equals("Jumba")) {

			if(other.GetComponent<MovimentControl>())

				other.GetComponent<MovimentControl>().OnIce = false;

			if (other.GetComponent<MovimentControlKeyboard>())

				other.GetComponent<MovimentControlKeyboard>().OnIce = false;

		}
	}
}
