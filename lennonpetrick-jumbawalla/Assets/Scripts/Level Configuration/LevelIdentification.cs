using UnityEngine;
using System.Collections;

public class LevelIdentification : MonoBehaviour {
	
	public Level thisLevel;

	void Start(){

		LevelManager.currentLevel = LevelManager.getLevelID(thisLevel);

	}
	
	public static LevelIdentification sharedLevelIdentification(){
		
		return Camera.main.GetComponent<LevelIdentification>();
		
	}
		
}