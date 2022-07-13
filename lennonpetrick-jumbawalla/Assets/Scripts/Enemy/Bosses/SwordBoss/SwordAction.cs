using UnityEngine;
using System.Collections;

public class SwordAction : MonoBehaviour {

	private float speedSword;

	private bool activeSwordMove;

	public SwordBoss boss;

	public GameObject swordPrefabs;
	public Transform transformOriginalSword;

	private GameObject swordObject;

	void Start() {

		this.activeSwordMove = false;

		this.speedSword = 40;

	}

	void OnEnable() {

		SwordBoss.sword += sword; 

	}

	void OnDisable() {

		SwordBoss.sword -= sword;

	}

	void sword () {

		this.boss.chargeRotate();

		this.swordObject = Instantiate( swordPrefabs, transformOriginalSword.position, swordPrefabs.transform.rotation) as GameObject;

		this.transformOriginalSword.gameObject.SetActive( false );

		StartCoroutine( activeSword() );

	}

	IEnumerator activeSword() {

		this.swordObject.GetComponent<moveSword>().speed = transform.rotation.y == 0? this.speedSword : -this.speedSword;

		yield return new WaitForSeconds(2);

		this.swordObject.GetComponent<moveSword>().speed = transform.rotation.y != 0? this.speedSword : -this.speedSword;

		yield return new WaitForSeconds(2);

		Destroy ( swordObject );

		this.transformOriginalSword.gameObject.SetActive( true );

		boss.resetState();

	}

}
