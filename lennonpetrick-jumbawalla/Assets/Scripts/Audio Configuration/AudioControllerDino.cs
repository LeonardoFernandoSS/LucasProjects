using UnityEngine;
using System.Collections;

public class AudioControllerDino : MonoBehaviour {

	private AudioSource audio;

	void Start()
	{
		audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
	}

	void Update() 
	{
		if(!audio.isPlaying)
		{
			audio.Play();
		}
	}
}
