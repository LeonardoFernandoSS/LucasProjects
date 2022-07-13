using UnityEngine;
using System.Collections;

public class LoadMenuOptionsCredTexts : MonoBehaviour {

	public TextMesh first_text;
	
	void Start () {
		first_text.text = CurrentLanguage.texts.getMenu_Options_Credits();
	}

}
