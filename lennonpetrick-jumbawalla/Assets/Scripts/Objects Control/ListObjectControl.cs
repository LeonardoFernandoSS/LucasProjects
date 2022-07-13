
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListObjectControl : MonoBehaviour {

	public int[] amountTypeObjects;
	public Transform[] prefabsObjects;

	private static GameObject myGameObject;
	private List<GameObject> listObjects;
	private TypeObjectControl[] listTypeObjects;

	void Start() {

		myGameObject = this.gameObject;

		this.init();

	}

	public void init() {

		this.createObject();
		this.createListObject();
		this.desactiveList();

	}

	void createObject() {

		for (int i = 0 ; i < prefabsObjects.Length; i++) {

			for(int j = 0; j < amountTypeObjects[i] ; j++) {

				Instantiate (prefabsObjects[i], transform.position, Quaternion.identity );

			}
		}

	}

	void createListObject() {

		GameObject[] _objects = GameObject.FindGameObjectsWithTag("Objects"); // Searches all objects with "Object" tag

		this.listTypeObjects = new TypeObjectControl[_objects.Length]; // Create list size

		this.listObjects = new List<GameObject>( _objects.Length); // Create list size

		for (int i = 0; i < _objects.Length; i++) {

			this.listTypeObjects[i] = _objects[i].GetComponent<TypeObjectControl>();

		}

		this.listObjects.AddRange( _objects ); // add Objects in list

	}

	public void activeList() {

		for(int i = 0; i < listObjects.Count; i++)

			listObjects[i].SetActive(true);

	}

	public void desactiveList() {

		for(int i = 0; i < listObjects.Count; i++)

			listObjects[i].SetActive(false);

	}

	public IEnumerator createObjects( TypeObject type, Vector3 position  ) {

		for(int i = 0;  i < listObjects.Count; i++) {
			
			if(listTypeObjects[i].typeObject == type && !listObjects[i].activeSelf) {
				
				listObjects[i].SetActive(true);
				
				listObjects[i].transform.position = position;
				
				checkObject( listTypeObjects[i], listObjects[i] );

				break;

			}
		}

		yield return null;
	}

	void checkObject(TypeObjectControl _object, GameObject _gameObject) {

		if(_object.typeObject == TypeObject.particlePoints) {

			_gameObject.GetComponent<PointBehaviour>().CurrentTime = 0;

		}

		else if(_object.typeObject == TypeObject.explosionEnemyRobot || _object.typeObject == TypeObject.batidaEnemyRobot) {

			_gameObject.GetComponent<DestroyParticle>().CurrentTime = 0;

		}

	}

	public static ListObjectControl sharedList() {

		return myGameObject.GetComponent<ListObjectControl>();

	}

}
