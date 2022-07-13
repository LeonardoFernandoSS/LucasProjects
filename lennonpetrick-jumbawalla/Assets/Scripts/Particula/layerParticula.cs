using UnityEngine;
using System.Collections;

public class layerParticula : MonoBehaviour {

	public string nome;
	public int sortLayer;

	void Start ()
	{
		// Set the sorting layer of the particle system.
		particleSystem.renderer.sortingLayerName = nome;
		particleSystem.renderer.sortingOrder = sortLayer;
	}
}
