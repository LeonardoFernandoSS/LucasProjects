using UnityEngine;
using System.Collections;

public class LoadMenuLevelNameTexts : MonoBehaviour {

	public TextMesh levelName;

	public void loadMenuLevelTexts ( int _idLevel ) {
		levelName.text = CurrentLanguage.texts.getMenu_Level_NameLevel(_idLevel);
	}

}
