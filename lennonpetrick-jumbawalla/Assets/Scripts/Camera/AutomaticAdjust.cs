using UnityEngine;
using System.Collections;

public class AutomaticAdjust : MonoBehaviour {

	void Start () 
	{
		GameObject.Find("paredeEsq").transform.position = new Vector3 ( camera.ScreenToWorldPoint(new Vector3(0,0,0)).x, 0, 0);
		GameObject.Find("paredeDir").transform.position = new Vector3 ( camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x, 0, 0); 
		GameObject.Find("Barra").transform.position = camera.ScreenToWorldPoint ( new Vector3 ( (Screen.width * 20) / 100, Screen.height * 90 / 100 , 10 ) );
		GameObject.Find("ButtonPause").transform.position = camera.ScreenToWorldPoint ( new Vector3 ( (Screen.width * 90) / 100, Screen.height * 90 / 100 , 10 )  );
		GameObject.Find("fundoARMA").transform.position = camera.ScreenToWorldPoint ( new Vector3 ( (Screen.width * 78) / 100, Screen.height * 90 / 100 , 10 )  );
	}
}
