using UnityEngine;
using System.Collections;

public class LoadWinTexts : MonoBehaviour {

	public TextMesh menu,
					next,
					enemys,
					points;

	public tk2dTextMesh stage,
						complete,
						congratulations;


	// Use this for initialization
	void Start () {

		menu.text = CurrentLanguage.texts.get_Menu();
		next.text = CurrentLanguage.texts.getWin_Next();
		enemys.text = CurrentLanguage.texts.getWin_Enemys();
		points.text = CurrentLanguage.texts.getWin_Points();

		stage.text = CurrentLanguage.texts.getWin_Texts_Level();
		complete.text = CurrentLanguage.texts.getWin_Texts_Complet();
		congratulations.text = CurrentLanguage.texts.getWin_Texts_Congratulations();

	}

}
