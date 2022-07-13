using UnityEngine;
using System.Collections;

public class LoadIntroTexts : MonoBehaviour {

	public TextMesh one,
					two,
					tree,
					four,
					five,
					six,
					seven;

	void Start () {
		one.text = CurrentLanguage.texts.getIntro_One();
		two.text = CurrentLanguage.texts.getIntro_Two();
		tree.text = CurrentLanguage.texts.getIntro_Tree();
		four.text = CurrentLanguage.texts.getIntro_Four();
		five.text = CurrentLanguage.texts.getIntro_Five();
		six.text = CurrentLanguage.texts.getIntro_Six();
		seven.text = CurrentLanguage.texts.getIntro_Seven();
	}

}
