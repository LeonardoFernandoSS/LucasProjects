using UnityEngine;
using System.Collections;

public class moveSword : MonoBehaviour {

	public float speed;

	public Transform SwordRotation;


	// Update is called once per frame
	void Update () {

		SwordRotation.Rotate( new Vector3 ( 0, 0, 1000) * Time.deltaTime );

		transform.Translate ( new Vector3( this.speed , 0  , 0 ) * Time.deltaTime );

	}
}
