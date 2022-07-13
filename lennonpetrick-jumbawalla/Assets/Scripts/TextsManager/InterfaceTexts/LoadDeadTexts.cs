using UnityEngine;
using System.Collections;

public class LoadDeadTexts : MonoBehaviour {

	public TextMesh menu,
					restarted,
					text_died;

	void Start () {

		menu.text = CurrentLanguage.texts.get_Menu();
		restarted.text = CurrentLanguage.texts.get_Restart();
		text_died.text = CurrentLanguage.texts.getDead_Texts_Dead();
	}

}
