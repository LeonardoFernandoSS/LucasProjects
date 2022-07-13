using UnityEngine;
using System.Collections;

public class ControlMissiles : MonoBehaviour {

	public float speedMove,
				 speedRotation,
				 currentSpeed;

	private Transform player;
	public Transform movePosition;

	public bool move;

	private float currentTime;

	void Start () {

		this.player = GameObject.Find("Jumba").transform;
		this.move = true;

	}

	void Update () {

		currentSpeed = speedMove * Time.deltaTime;

		if(this.move) {

			this.currentTime += Time.deltaTime;

			transform.position = Vector3.MoveTowards(transform.position, movePosition.position,currentSpeed/3);

			if(this.currentTime > 3.0f)
				this.move = false;

		}
		else {

			GetComponent<SpriteRenderer>().sortingLayerName = "Frente";

			transform.position = Vector3.MoveTowards(transform.position,player.position,currentSpeed);

			Vector3 lookPos = player.position - transform.position;

			float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;

			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

			transform.eulerAngles = new Vector3( 0, 0, transform.eulerAngles.z);

		}
	}

	void OnTriggerEnter ( Collider other ) {

		if(other.name.Equals("Jumba")) {

			other.GetComponent<ControllerLifePlayer>().RemoveLifePlayer(15);

			Destroy(gameObject);

		}
		else if(other.tag.Equals("AttackJumba")){

			Destroy(gameObject);

		}

	}
}
