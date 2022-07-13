using UnityEngine;
using System.Collections;

public class InstantiateAmountEnemy : MonoBehaviour {

	public GameObject[] enemys;
	public Transform[] tagEnemyInstanteate;

	public bool moveOnRight;

	public EnemyController enemyController;

	public int amountEnemyTotal;
	public int amountEnemyInScene;

	public bool restoreCamera;

	private int amountEnemyInPlay;
	private int amountEnemyDie;
	private int amountEnemyCreateTotal;

	private int idEnemy;

	public float waitingTimeInstanteateEnemy;

	private bool canCreate;
	private bool activeLogicalScript;
	private CameraFollow cameraF;

	void Start() 
	{
		this.cameraF = GameObject.Find("Main Camera").GetComponent<CameraFollow>();

		this.canCreate = true;

		this.amountEnemyInPlay = 0;

		this.amountEnemyDie = 0;

		this.amountEnemyCreateTotal = 0;

		this.idEnemy = 0;

		this.activeLogicalScript = false;
	}

	void Update() 
	{
		if(this.activeLogicalScript)
		{
			if(this.amountEnemyDie >= this.amountEnemyTotal) 
			{
				cameraF.setLockCamera(false);

				MoveOn.search().startMoveOn(2, moveOnRight ? MOVEON.LEFT : MOVEON.RIGHT);

				gameObject.SetActive(false);

			}
			else if(this.amountEnemyInPlay < this.amountEnemyInScene && this.amountEnemyCreateTotal < this.amountEnemyTotal && this.canCreate)
			{
				StartCoroutine(DrawEnemy( waitingTimeInstanteateEnemy ));
			}
		}
	}

	
	public void UpdateState ( ) 
	{
		this.amountEnemyDie++;
		this.amountEnemyInPlay--;
	}

	IEnumerator DrawEnemy ( float time )
	{
		this.canCreate = false;

		this.createEnemy();
		this.idEnemy++;
		this.amountEnemyInPlay++;

		yield return new WaitForSeconds( time );

		this.activeLogicalScript = true;
		this.canCreate = true;
	}

	void createEnemy ( ) 
	{
		//Metodo que vai instanciar o inimigo , sentando sua posiçao e rotaçao		
		GameObject current = Instantiate( enemys[idEnemy], tagEnemyInstanteate[Random.Range(0, tagEnemyInstanteate.Length)].position, Quaternion.identity) as GameObject;

		current.transform.GetComponentInChildren<EnemyAIBehaviour>().father = gameObject;

		amountEnemyCreateTotal++;

		if(current.transform.GetComponentInChildren<EnemyCanAttack>())
			enemyController.enemys.Add(current);

	}

	void OnTriggerEnter( Collider other) 
	{
		if(other.tag.Equals("Player")) 
		{

			tagEnemyInstanteate[0].position = new Vector3 ( GameObject.Find("Main Camera").camera.ScreenToWorldPoint(new Vector3(-60f,0,0)).x, tagEnemyInstanteate[0].position.y, tagEnemyInstanteate[0].position.z);
			tagEnemyInstanteate[1].position = new Vector3 ( GameObject.Find("Main Camera").camera.ScreenToWorldPoint(new Vector3(Screen.width + 60f, 0, 0)).x,tagEnemyInstanteate[1].position.y, tagEnemyInstanteate[1].position.z);

			tagEnemyInstanteate[0].localPosition = new Vector3 ( tagEnemyInstanteate[0].localPosition.x, tagEnemyInstanteate[0].localPosition.y, 0);
			tagEnemyInstanteate[1].localPosition = new Vector3 ( tagEnemyInstanteate[1].localPosition.x, tagEnemyInstanteate[1].localPosition.y, 0);

			cameraF.setLockCamera(true);

			this.activeLogicalScript = true;
			
			this.canCreate = true;

			Destroy(GetComponent<BoxCollider>());
		}
	}
}
