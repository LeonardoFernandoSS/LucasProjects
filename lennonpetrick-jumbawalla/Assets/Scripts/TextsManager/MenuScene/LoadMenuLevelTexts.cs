using UnityEngine;
using System.Collections;

public class LoadMenuLevelTexts : MonoBehaviour {

	public TextMesh txt_One,
					txt_Two;

	// Use this for initialization
	void Start () {
		txt_One.text = CurrentLanguage.texts.getMenu_Callout_Levels_textOne();
		txt_Two.text = CurrentLanguage.texts.getMenu_Callout_Levels_textTwo();
	}

}
