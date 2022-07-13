using UnityEngine;
using System.Collections;
public enum MOVEON
{
	LEFT,
	RIGHT
}
public class MoveOn : MonoBehaviour {

	public GameObject prefabMoveRight;
	public GameObject prefabMoveLeft;
	private static GameObject myGameObject;

	void Start() {

		myGameObject = this.gameObject;
		//prefabMoveLeft.SetActive(false);
		prefabMoveRight.SetActive(false);

	}

	public void startMoveOn( float _time , MOVEON move) {
		StartCoroutine(moveOn ( _time , move));
	}

	IEnumerator moveOn(float _time, MOVEON move) {

		if(move == MOVEON.LEFT)
			prefabMoveLeft.SetActive(true);
		else 
			prefabMoveRight.SetActive(true);

		yield return new WaitForSeconds(_time);

		if(move == MOVEON.LEFT)
			prefabMoveLeft.SetActive(false);
		else 
			prefabMoveRight.SetActive(false);

	}

	public static MoveOn search() {
		return myGameObject.GetComponent<MoveOn>();
	}

}
