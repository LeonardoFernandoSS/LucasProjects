using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

	public  List<GameObject> enemys;
	private GameObject enemyNear;
	private Transform player;
	private float time;

	void Start()
	{
		this.player = GameObject.Find("Jumba").transform;
	}

	void Update()
	{
		if(enemys != null) 
		{
			foreach( var enemy in enemys ) 
			{
				if(enemy != null) 
				{
					if(this.enemyNear == null)
					{
						this.enemyNear = enemy;
						this.enemyNear.transform.GetChild(0).GetComponent<EnemyCanAttack>().CanAttack = true;
					}
					else if(Vector3.Distance(enemy.transform.GetChild(0).position, player.position) < Vector3.Distance(enemyNear.transform.GetChild(0).position, player.position))
					{
						this.enemyNear.transform.GetChild(0).GetComponent<EnemyCanAttack>().CanAttack = false;
						this.enemyNear = enemy;
						this.enemyNear.transform.GetChild(0).GetComponent<EnemyCanAttack>().CanAttack = true;
					}
				}
				else
					enemys.Remove ( enemy );
			}
		}
	}
}
