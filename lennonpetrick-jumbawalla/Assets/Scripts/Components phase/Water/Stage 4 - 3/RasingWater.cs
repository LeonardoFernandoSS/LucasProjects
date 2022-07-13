using UnityEngine;
using System.Collections;

public class RasingWater : MonoBehaviour {

	public float speedWater;
	public float damageWater;

	private bool moveWater;

	void Update() {

		if(this.moveWater) {
			if(transform.position.y <= 563.2028) 
				transform.Translate( new Vector3( 0, speedWater, 0 ) * Time.deltaTime);
		}

	}

	public void activeWater() {
		this.moveWater = true;
	}

	void OnTriggerStay ( Collider  other ) {

		if(other.tag.Equals("Player")) {

			other.GetComponent<ControllerLifePlayer>().RemoveLifePlayer(damageWater);

		}
		else if (other.tag.Equals("Enemy")) {

			other.GetComponent<EnemyLife>().RemoveLife( 1000 );

		}
	}

}
