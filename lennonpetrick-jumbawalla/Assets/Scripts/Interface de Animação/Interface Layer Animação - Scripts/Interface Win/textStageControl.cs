using UnityEngine;
using System.Collections;

public class textStageControl : MonoBehaviour {


	void Start() {
		GetComponent<tk2dTextMesh>().text = loadText();
	}

	string loadText () {

	 	switch(LevelManager.currentLevel) {
			case 7 : {
				return "stage 1-1";
				break;
			}
			case 8 : {
				return "stage 1-2";
				break;
			}case 9 : {
				return "stage 1-3";
				break;
			}case 10 : {
				return "stage 2-1";
				break;
			}case 11 : {
				return "stage 2-2";
				break;
			}case 12 : {
				return "stage 2-3";
				break;
			}case 13 : {
				return "stage 3-1";
				break;
			}case 14 : {
				return "stage 3-2";
				break;
			}case 15 : {
				return "stage 3-3";
				break;
			}case 16 : {
				return "stage 4-1";
				break;
			}case 17 : {
				return "stage 4-2";
				break;
			}case 18 : {
				return "stage 4-3";
				break;
			}case 19 : {
				return "stage 5-1";
				break;
			}case 20 : {
				return "stage 5-2";
				break;
			}case 21 : {
				return "stage 5-3";
				break;
			}case 22 : {
				return "stage 6-1";
				break;
			}case 23 : {
				return "stage 6-2";
				break;
			}case 24 : {
				return "stage 6-3";
				break;
			}case 25 : {
				return "stage 7-1";
				break;
			}case 26 : {
				return "stage 7-2";
				break;
			}case 27 : {
				return "stage 7-3";
				break;
			}default: {
				return "vazio";
				break;
			}
		}

	}

}
