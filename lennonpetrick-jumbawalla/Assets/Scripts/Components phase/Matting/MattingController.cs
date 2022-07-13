using UnityEngine;
using System.Collections;

public class MattingController : MonoBehaviour {

	public float speed;
	public Matting mattingControl;

	void Start() {
		this.mattingControl = new Matting( speed , true );
	}

	void Update() {

		if(Input.GetKeyDown(KeyCode.L)) {
			mattingControl.flipMat();
		}

		if(Input.GetKeyDown(KeyCode.K)) {
			mattingControl.offMat();
		}

	}
	
}
