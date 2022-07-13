using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {

	void Start () {

		Director.sharedDirector().LoadLevelWithFade(Level.intro.GetHashCode());

	}

}
