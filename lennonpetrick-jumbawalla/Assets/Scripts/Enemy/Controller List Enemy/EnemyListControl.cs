using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyListControl : MonoBehaviour {

	public int[] amountTypeObjects;
	public Transform[] prefabsObjects;

	//list enemy in scene
	private List<GameObject> enemyControl;
	private EnumEnemyList[] enemyList;

	void Start() {

		this.init();

	}

	public void init() {

		this.createEnemy();

		this.addEnemyList();
		
		this.disableAllEnemys();

	}

	//Disable all enemies of the scene
	public void disableAllEnemys ( ) {

		for(int i = 0; i < enemyControl.Count; i++)
			enemyControl[i].SetActive(false);

	}
	// Active all enemies of the scene
	public void activeAllEnemy ( ) {

		for(int i = 0; i < enemyControl.Count; i++)
			enemyControl[i].SetActive(true);

	}
	//returns enemy who is disabled
	public GameObject addDisabledEnemy (EnemyType type, Vector3 position) {

		for(int i = 0; i < enemyControl.Count; i ++) {

			if(enemyList[i].EnemyType == type && !enemyControl[i].activeSelf) {

				enemyControl[i].SetActive(true);
				enemyControl[i].transform.GetChild(0).localPosition = Vector3.zero;
				enemyControl[i].transform.position = position;
				EnemyStateMachine _state = enemyControl[i].GetComponentInChildren<EnemyStateMachine>();

				if( _state != null ) {
					enemyControl[i].GetComponentInChildren<EnemyStateMachine>().setStateMachineEnemy(StateMachineEnemy.Null);
				}

				return enemyControl[i];
			}
	
		}
		return null;
	}
	//Create list the enemy of the scene
	void addEnemyList () {
		
		// pega todos os objetos que tenha a tag de Enemy
		GameObject[] _enemys = GameObject.FindGameObjectsWithTag("Enemy"); 
		
		//Cria as listas com os tamanhos dos objetos
		enemyList = new EnumEnemyList[_enemys.Length];
		enemyControl = new List<GameObject>(_enemys.Length);
		
		for(int i = 0; i < _enemys.Length; i++) {
			enemyList[i] = _enemys[i].GetComponentInChildren<EnumEnemyList>();
		}
		
		this.enemyControl.AddRange( _enemys );
		
	}

	void createEnemy () {

		for (int i = 0 ; i < prefabsObjects.Length; i++) {
			for(int j = 0; j < amountTypeObjects[i] ; j++) {
				Instantiate (prefabsObjects[i], transform.position, Quaternion.identity );
			}
		}
	}
}
