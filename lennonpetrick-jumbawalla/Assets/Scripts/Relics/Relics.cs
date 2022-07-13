using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Relics : MonoBehaviour {

	public static string relicsIdentifies = "relics";

	private GameObject[] collections;

	void Awake() {

		getColectionGameObjects();

		unlockIconRelics();

	}

	// Adiciona uma reliquia na coleçao
	public static void addRelicCollections (int id) {

		ArrayList _arrayRelicsIndex;

		if(!SaveSystem.hasObject(relicsIdentifies)) {

			_arrayRelicsIndex = new ArrayList();

			_arrayRelicsIndex.Add( id );

		}
		else  {

			_arrayRelicsIndex = (ArrayList)SaveSystem.load(relicsIdentifies,  typeof(ArrayList));

			PlayerPrefs.DeleteKey ( relicsIdentifies );

			_arrayRelicsIndex.Add( id );

		}

		SaveSystem.save(relicsIdentifies, _arrayRelicsIndex);

	}
	// Salva os gameobjects do itens da coleçao
	void getColectionGameObjects() {

		collections = new GameObject[10];
		
		for(int i = 0; i < 10; i++) {

			if(i == 9) 
				collections[i] = GameObject.Find("colecao10");
			else
				collections[i] = GameObject.Find("colecao" + (i+1));

		}

	}

	// Desbloqueia os icones
	void unlockIconRelics () {

		ArrayList number = (ArrayList)SaveSystem.load(relicsIdentifies,  typeof(ArrayList));

		if(number != null) {
			for (int i = 0; i < collections.Length; i ++) {
				for(int j = 0; j < number.Count; j++ ) {
					if(collections[i].GetComponent<tk2dSprite>().spriteId == (int)number[j]) {
						collections[i].SetActive(true);
						break;
					}
					else
						collections[i].SetActive(false);
				}
			}
		}
		else  {
			for (int i = 0; i < collections.Length; i ++) {
				collections[i].SetActive(false);
			}
		}
	}

}
