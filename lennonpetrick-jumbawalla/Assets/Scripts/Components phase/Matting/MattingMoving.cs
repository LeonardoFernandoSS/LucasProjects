using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MattingMoving : MonoBehaviour {

	private float modify = 11.2f;

	public MattingController m_control;
	
	void OnTriggerStay(Collider obj) {

		if(obj.tag.Equals("Player")) {

			obj.GetComponent<CharacterController>().Move( new Vector3 ( ((modify * m_control.mattingControl.Speed)/0.5f ) * (m_control.mattingControl.Direction * - 1), 0, 0 ) * Time.deltaTime);

		}
		else if (obj.name.Equals("InimigoRoboBate")) {
			obj.gameObject.transform.position += new Vector3 (((modify * m_control.mattingControl.Speed)/0.5f ) * (m_control.mattingControl.Direction * - 1), 0, 0 ) * Time.deltaTime;
		}
	}
}
