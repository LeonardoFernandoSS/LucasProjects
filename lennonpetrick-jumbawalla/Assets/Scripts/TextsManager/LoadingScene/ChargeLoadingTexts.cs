using UnityEngine;
using System.Collections;

public class ChargeLoadingTexts : MonoBehaviour {

	public TextMesh loading;

	void Start () {
		loading.text = CurrentLanguage.texts.getLoading();
	}

}
