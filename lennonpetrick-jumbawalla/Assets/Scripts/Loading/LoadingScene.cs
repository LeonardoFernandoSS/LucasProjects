using UnityEngine;
using System.Collections;

public class LoadingScene : MonoBehaviour {
	
	AsyncOperation async;
    
	
	IEnumerator Start () {
		
		async = Application.LoadLevelAsync(Director.sceneToLoad);
		
		yield return async;
	
	}
		
}