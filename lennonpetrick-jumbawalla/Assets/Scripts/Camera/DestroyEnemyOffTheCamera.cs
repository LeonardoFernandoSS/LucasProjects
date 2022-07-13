using UnityEngine;
using System.Collections;

public class DestroyEnemyOffTheCamera : MonoBehaviour {

	void OnTriggerEnter( Collider other ) 
	{
		if(other.transform.parent != null) {
			if(other.transform.parent.tag == "Enemy"){

				if(other.GetComponent<EnemyLife>())
					other.GetComponent<EnemyLife>().life = -1;
				else 
					Destroy( other.gameObject.transform.parent.gameObject );
			}
		}
		else {

			if(other.transform.tag == "Enemy"){
				
				if(other.GetComponent<EnemyLife>())
					other.GetComponent<EnemyLife>().life = -1;
				else 
					Destroy( other.gameObject );
				
			}
		}
	}

}
