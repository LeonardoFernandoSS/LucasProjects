using UnityEngine;
using System.Collections;

public class LoadPausedTexts : MonoBehaviour {

	public TextMesh paused,
					continue_txt,
					menu,
					restarted;

	void Start () {
		paused.text = CurrentLanguage.texts.get_Paused();
		continue_txt.text = CurrentLanguage.texts.get_Continue();
		menu.text = CurrentLanguage.texts.get_Menu();
		restarted.text = CurrentLanguage.texts.get_Restart();
	}

}
