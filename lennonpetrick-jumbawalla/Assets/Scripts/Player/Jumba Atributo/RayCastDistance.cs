using UnityEngine;
using System.Collections;

public class RayCastDistance : MonoBehaviour {
	
	private RaycastHit hit;
	
	private bool hitFloor;

	public bool getHitFloor () { return this.hitFloor; }

	private int layerGround;

	void Start() {

		layerGround = LayerMask.NameToLayer("Floor");

	}

	void Update () {
	
		#region RayCast Floor
		Vector3 _directionDown = transform.TransformDirection(Vector3.down);

		if(Physics.Raycast(transform.position, _directionDown, out hit, 7)) {

			if( hit.transform.gameObject.layer == layerGround ) 
				hitFloor = true;

		}
		else 
			hitFloor = false;
		#endregion 

	}
}
