using UnityEngine;
using System.Collections;

public class PterodactylsMovement : MonoBehaviour {
		
	public float maxDistanceToFollow = 50.0f,
				 maxDistanceToStopFollow = 10.0f;
	private Transform player;	
	public float followEnemySpeed = 10.5f, 
					normalEnemySpeed = 8.5f;
		
	private Vector3 direction = new Vector3(-1, 0, 0);
	private float enemySpeed;
	private bool canFollow = true;
	
	void Start(){
		player= GameObject.Find("Jumba").transform;
		
	}
			
	void Update () {
		
		if (Vector3.Distance(new Vector3(player.position.x, 0, 0), new Vector3(transform.position.x, 0, 0)) <= maxDistanceToFollow){
			
			this.enemySpeed = this.followEnemySpeed;
			
			this.followPlayer();
						
			if (Vector3.Distance(new Vector3(player.position.x, 0, 0), new Vector3(transform.position.x, 0, 0)) <= maxDistanceToStopFollow){
				
				this.enemySpeed = this.normalEnemySpeed;
				
				this.direction = new Vector3(-1, 0, 0);
								
				transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w);
				
				canFollow = false;
			
			}
									
		}
		else{
			
			this.enemySpeed = this.normalEnemySpeed;
			
		}
		
		transform.Translate(this.direction * this.enemySpeed * Time.deltaTime);
		
	}
		
	private void followPlayer(){
		
		if(canFollow){
				
			Vector3 _distance = (player.position - transform.position).normalized;
			
			this.direction = _distance;
							
			transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, Quaternion.LookRotation(_distance).z, transform.rotation.w);
			
		}
		
	}
	
}