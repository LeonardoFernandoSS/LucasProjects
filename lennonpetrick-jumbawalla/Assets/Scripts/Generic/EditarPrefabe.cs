using UnityEngine;
using System.Collections;

public class EditarPrefabe : MonoBehaviour {
	
	public GameObject objects;
	
	void OnTriggerEnter( Collider other)
	{
		if(other.name.Equals("Jumba"))
		{
			if(objects.activeSelf == true)
				objects.SetActive(false);
			else
				objects.SetActive(true);
		}
	}
}
