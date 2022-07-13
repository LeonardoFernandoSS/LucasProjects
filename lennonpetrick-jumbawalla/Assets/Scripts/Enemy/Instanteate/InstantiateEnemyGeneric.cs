using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 * Classe que processa o instanciamento dos inimigos
 * selecionando qual tipo de inimigo vai ser instanciado
 * selecionando quantidade de inimigo
 * selecionando quais as posiçoes vao ser  instanciados os inimigos
 * 
 * 
 * */
public class InstantiateEnemyGeneric : MonoBehaviour {

	public EnemyController enemyController;

	public bool moveOnRight;	

	public GameObject[] enemys;				// inimigos que vao ser instanciados

	public Transform[] tags;				// tag onde vai ser instanciados os inimigos

	public Animation airCraft;				//Controlador das Animaçoes
	
	private int idEnemy;					// id do inimigo que vai ser instanciado ( todos vao ser instaciado em Ordem )

	private int amountEnemyDie; 			// inimigo mortos
	private int amountEnemyInScene;			// inimigos atuais que estao na cena
	private int amountEnemyTotal;			// inimigos total que foram criados

	private bool onTrigger;					//utilizar o Trigger apenas uma vez

	public int enemyInScene; 				//total de inimigos que pode ficar na scena
	public int enemiesTotal;				//total de inimigo que vai ser criados
	public int enemyInitInScene;			//total de inimigo que vai começar quando for criado 

	public float timeCreate;				//Intervalo de tempo para criar os inimigos

	void Start()
	{
		this.amountEnemyDie = 0;
		this.amountEnemyInScene = 0;
		this.amountEnemyTotal = 0;
		this.idEnemy = 0;
		this.onTrigger = true;
	}


	public void UpdateState() 
	{

		this.amountEnemyDie++;
		this.amountEnemyInScene--;
		this.checkEnemy();

	}

	void checkEnemy() 
	{
		if(this.amountEnemyDie >= this.enemiesTotal)
		{
			GameObject.Find("Main Camera").GetComponent<CameraFollow>().setLockCamera(false);
			StartCoroutine(destroy());
		}
		else if(this.amountEnemyInScene < this.enemyInScene && this.amountEnemyTotal < this.enemiesTotal )
		{
			if(!airCraft.isPlaying)
				StartCoroutine(createEnemy( 1  , timeCreate ));
		}
	}

	IEnumerator destroy()
	{
		bool _exit = true;

		while(_exit)
		{
			if(!airCraft.isPlaying)
			{
				MoveOn.search().startMoveOn(3, moveOnRight ? MOVEON.LEFT : MOVEON.RIGHT);
				Destroy(this.gameObject);
				_exit = !_exit;
			}
		}

		yield return null;
	}

	IEnumerator createEnemy(int amount, float time) 
	{
		if(!airCraft.IsPlaying("naveDesce"))
			airCraft.Play("naveDesce");

		yield return new WaitForSeconds(2.0f);

		for(int i = 0; i < amount; i++ )
		{
			GameObject _enemy = Instantiate ( enemys[idEnemy], tags[Random.Range(0, tags.Length)].position, Quaternion.identity ) as GameObject;
			_enemy.GetComponentInChildren<EnemyAIBehaviour>().father = this.gameObject;

			enemyController.enemys.Add(_enemy);
			this.idEnemy++;
			this.amountEnemyTotal++;
			this.amountEnemyInScene++;
			yield return new WaitForSeconds(time);
		}

		airCraft.Play("naveSobe");

		yield return new WaitForSeconds(1.3f);

		if(!airCraft.isPlaying)
			checkEnemy();
	}

	void OnTriggerEnter(Collider other) 
	{
		if(this.onTrigger)
		{
			if(other.name.Equals("Jumba"))
			{
				GameObject.Find("Main Camera").GetComponent<CameraFollow>().setLockCamera(true);
				StartCoroutine(createEnemy( enemyInitInScene, 2f ));
				this.onTrigger = !this.onTrigger;
			}
		}
	}

}
