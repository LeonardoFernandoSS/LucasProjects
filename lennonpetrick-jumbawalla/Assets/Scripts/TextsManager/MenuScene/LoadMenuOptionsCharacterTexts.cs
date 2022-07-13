using UnityEngine;
using System.Collections;

public class LoadMenuOptionsCharacterTexts : MonoBehaviour {

	public TextMesh first_text;

	void Start () {
		first_text.text = CurrentLanguage.texts.getMenu_Options_Character();
	}

}
