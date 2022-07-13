using UnityEngine;
using System.Collections;

public class LoadMenuCollectionTexts : MonoBehaviour {

	public TextMesh collections;
	public TextMesh calloutText;
	// Use this for initialization
	void Start () {
		collections.text = CurrentLanguage.texts.getMenu_Collections();
		calloutText.text = CurrentLanguage.texts.getMenu_Callout_Collection();
	}

}
