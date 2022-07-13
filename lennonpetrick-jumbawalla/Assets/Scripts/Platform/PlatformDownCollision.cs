using UnityEngine;
using System.Collections;

public class PlatformDownCollision : MonoBehaviour {

	private void OnTriggerStay(Collider collider) {
        
		if(collider.name == "Jumba"){

			transform.parent.gameObject.layer = (int)TypeLayer.UpPlataform;
			//transform.parent.GetComponent<MeshCollider>().isTrigger = true;
			
		}
		
	}
	
	private void OnTriggerExit(Collider collider) {
        
		if(collider.name == "Jumba"){

			transform.parent.gameObject.layer = (int)TypeLayer.Floor;
			//transform.parent.GetComponent<MeshCollider>().isTrigger = false;
			
		}
		
	}
	
}