using UnityEngine;
using System.Collections;

public class InstantiateEnemyOther : MonoBehaviour {

	// Array de objetos, os inimigos que vai ser instanciado
	public GameObject[] enemys;
	
	//posiçao onde vai instanciar os inimigos dentro da cena
	public Transform[] instantiate;
	// Use this for initialization

	public EnemyController enemyController;

	public int amountEnemy;
	public float time;
	// Update is called once per frame

	private bool enableCoroutine = true;

	IEnumerator Create(){

		for(int i = 0; i < amountEnemy; i++){	
			
			Transform _tagPosition = instantiate[TypeTag()];
								
								//pega posiçao onde vai instaciar e randomiza a posiçao x para nao instanciar todos na mesma posiçao
			Vector3 _position = new Vector3(_tagPosition.transform.position.x,
											_tagPosition.transform.position.y,
											_tagPosition.transform.position.z);

			/*	Metodo que vai instanciar o inimigo , sentando sua posiçao e rotaçao
								 * */
							
			GameObject current = Instantiate ( enemys[i], _position, Quaternion.identity ) as GameObject;
			
			if(current.transform.GetComponentInChildren<EnemyAIBehaviour>())
				current.transform.GetComponentInChildren<EnemyAIBehaviour>().father = gameObject;
			
			//if(current.transform.GetComponentInChildren<EnemyCanAttack>())
				enemyController.enemys.Add(current);

			yield return new WaitForSeconds(time);
			
		}

		StartCoroutine(destroy(gameObject));

	}
	void OnTriggerEnter(Collider collider)
	{
		// Caso colide com  o Player
		if(collider.tag == "Player"){
			if(enableCoroutine) {
				StartCoroutine(Create());
				this.enableCoroutine = false;
			}
		}
	}

	IEnumerator destroy ( GameObject _this) {

		yield return null;
		Destroy(_this);

	}
	/*
	 * Metodo que vai retornar um numero randomico, que indica qual tag
	 * no Array setado vai ser instanciado o inimigo
	 * 
	 * Uso do metodo para instaciar um objeto "_tagPosition"
	 * 
	 * */
	
	int TypeTag() {return Random.Range(0, instantiate.Length); }
}