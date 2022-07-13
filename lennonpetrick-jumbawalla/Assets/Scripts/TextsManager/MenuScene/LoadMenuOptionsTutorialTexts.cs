using UnityEngine;
using System.Collections;

public class LoadMenuOptionsTutorialTexts : MonoBehaviour {

	public TextMesh first_text;
	
	void Start () {
		first_text.text = CurrentLanguage.texts.getMenu_Options_Tutorial();
	}

}
