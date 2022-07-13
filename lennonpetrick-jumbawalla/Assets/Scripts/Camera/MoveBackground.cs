using UnityEngine;
using System.Collections;

public class MoveBackground : MonoBehaviour {

	public float scrollSpeed = 0.5F;
	public Transform target;


	void Update() {

		//transform.position = new Vector3 ( target.position.x, transform.position.y, transform.position.y );

		float offset = Time.time * scrollSpeed;
		renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));

	}


}
