using UnityEngine;
using System.Collections;

public class LoadTutorialTexts : MonoBehaviour {

	public tk2dTextMesh dicaOne,
						dicaTwo,
						dicaTree,
						dicaFour,
						dicaFive,
						dicaSix;
	void Start () {

		dicaOne.text = CurrentLanguage.texts.getTutorial_TipOne();
		dicaTwo.text = CurrentLanguage.texts.getTutorial_TipTwo();
		dicaTree.text = CurrentLanguage.texts.getTutorial_TipTree();
		dicaFour.text = CurrentLanguage.texts.getTutorial_TipFour();
		dicaFive.text = CurrentLanguage.texts.getTutorial_TipFive();
		dicaSix.text = CurrentLanguage.texts.getTutorial_TipSix();

	}

}
