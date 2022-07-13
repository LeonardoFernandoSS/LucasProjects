using UnityEngine;
using System.Collections;

public class MattingInformation : MonoBehaviour {

	public MattingController m_controller;

	void Update () {

		m_controller.mattingControl.treadmillRun( this.gameObject );
	
	}
}
