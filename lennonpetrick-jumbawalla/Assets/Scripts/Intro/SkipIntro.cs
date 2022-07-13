using UnityEngine;
using System.Collections;

public class SkipIntro : MonoBehaviour {

	public Animator anim;

	void Update () {

		if(SaveSystem.hasObject("Intro")) {

			if(Input.touchCount > 0) 
				Director.sharedDirector().LoadLevelWithFade(Level.menu.GetHashCode());
			
			else if(!anim.GetBool("animation")) 
				Director.sharedDirector().LoadLevelWithFade(Level.menu.GetHashCode());


		}
		else {

			if(!anim.GetBool("animation")) {

				SaveSystem.saveString("Intro", "intro");
				Director.sharedDirector().LoadLevelWithFade(Level.menu.GetHashCode());

			}

		}
	
	}

	void animationOver()
	{
		anim.SetBool("animation", false);
	}
}
