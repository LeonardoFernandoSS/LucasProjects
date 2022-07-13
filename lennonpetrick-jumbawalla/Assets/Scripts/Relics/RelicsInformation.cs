using UnityEngine;
using System.Collections;

public class RelicsInformation : MonoBehaviour {

	private Animation animCamera;

	public AudioClip audioColecao;

	void Start() {

		animCamera = GameObject.Find("efeitoColecao").animation;

		thisExistsSave();

	}

	void thisExistsSave () {

		ArrayList _relicsIndex = ( ArrayList )SaveSystem.load( Relics.relicsIdentifies, typeof(ArrayList));
		
		if(_relicsIndex != null) {
			for(int i = 0; i < _relicsIndex.Count; i++) {
				if(transform.GetComponent<tk2dSprite>().spriteId == (int)_relicsIndex[i]) {
					Destroy( gameObject );
				}
			}
		}
	}

	void OnTriggerEnter( Collider other) {
		
		if(other.transform.name.Equals("Jumba")) {
			
			Relics.addRelicCollections(transform.GetComponent<tk2dSprite>().spriteId);
			animCamera.Play("rodandoEstrela");

			Director.sharedDirector().playEffect(audioColecao);

			Destroy( gameObject );
		}
	}

}
