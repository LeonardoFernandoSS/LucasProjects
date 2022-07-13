using UnityEngine;
using System.Collections;

public class LoadMenuInitTexts : MonoBehaviour {

	public TextMesh play,
					options,
					shop,
					collections;

	void Start () {

		play.text = CurrentLanguage.texts.getMenu_Play();
		options.text = CurrentLanguage.texts.getMenu_Options();
		shop.text = CurrentLanguage.texts.getMenu_Shop();
		collections.text = CurrentLanguage.texts.getMenu_Collections();

	}

}
