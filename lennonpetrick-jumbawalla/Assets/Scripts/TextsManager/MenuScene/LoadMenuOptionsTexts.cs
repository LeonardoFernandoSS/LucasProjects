using UnityEngine;
using System.Collections;

public class LoadMenuOptionsTexts : MonoBehaviour {

	public TextMesh options,
					tutorial,
					credits,
					character;

	void Start () {
		options.text = CurrentLanguage.texts.getMenu_Options();
		tutorial.text = CurrentLanguage.texts.getMenu_Options_Tutorial();
		credits.text = CurrentLanguage.texts.getMenu_Options_Credits();
		character.text = CurrentLanguage.texts.getMenu_Options_Character();
	}

}
