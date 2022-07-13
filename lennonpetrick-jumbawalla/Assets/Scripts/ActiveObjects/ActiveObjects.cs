using UnityEngine;
using System.Collections;

public class ActiveObjects : MonoBehaviour {

	public GameObject[] objects;

	void OnTriggerEnter( Collider other )
	{
		if(other.name.Equals("Jumba"))
		{
			for(int i = 0; i < objects.Length; i++)
			{
				objects[i].SetActive(true);
			}
			Destroy(this.gameObject);
		}
	}
}
