using UnityEngine;
using System.Collections;

public class FocusPrefab : MonoBehaviour {

	[SerializeField] private Animation anim;
	
	void Update () {

		if(!anim.isPlaying) {
			Destroy(gameObject);
		}

	}
}
