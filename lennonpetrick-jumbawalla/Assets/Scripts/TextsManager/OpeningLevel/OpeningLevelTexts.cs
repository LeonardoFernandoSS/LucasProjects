using UnityEngine;
using System.Collections;

public class OpeningLevelTexts : MonoBehaviour {

	public tk2dTextMesh level;

	void Update() {
		level.text = CurrentLanguage.texts.getWin_Texts_Level();
	}

}
