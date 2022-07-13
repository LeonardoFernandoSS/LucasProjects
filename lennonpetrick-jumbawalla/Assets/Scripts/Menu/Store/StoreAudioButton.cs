using UnityEngine;
using System.Collections;

public class StoreAudioButton : MonoBehaviour {

	public AudioClip button;

	private static GameObject myGameObject;

	void Start()
	{
		myGameObject = this.gameObject;
	}

	public void playAudio()
	{
		Director.sharedDirector().playEffect(button);
	}
	public static StoreAudioButton shared()
	{
		return GameObject.Find(myGameObject.name).GetComponent<StoreAudioButton>();
	}
}
