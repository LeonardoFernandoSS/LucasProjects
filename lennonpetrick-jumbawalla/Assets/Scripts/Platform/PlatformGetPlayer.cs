using UnityEngine;
using System.Collections;

public class PlatformGetPlayer : MonoBehaviour {
		
	private bool collisionExited = false;	
	private float platformDistance = 0.45f;	
	private Transform player, 
					  lastParent;
	private PlayerStateControl statePlayer;
	public Transform parent;
		
	protected void Start () {
		
		player = GameObject.Find("Jumba").transform;

		statePlayer = player.GetComponent<PlayerStateControl>();
		
		this.lastParent = player.parent;
	
	}
	
	protected void Update () {
		
		this.checkDistance();
			
	}
	
	protected void checkDistance(){

		if(collisionExited){
					
			if(Vector3.Distance(new Vector3(player.position.x, 0, 0), new Vector3(transform.position.x, 0, 0)) > platformDistance){
					
				player.parent = lastParent;

				player.transform.Rotate( player.transform.rotation.x, player.transform.rotation.y, 0 );

				this.collisionExited = false;
				
			}			
				
		}
		
	}
			
	private void OnTriggerEnter(Collider collider){

		if(collider.name == "Jumba"){

			player.parent = parent;

			player.transform.Rotate( player.transform.rotation.x, player.transform.rotation.y, 0 );

			this.collisionExited = false;
									
		}
										
	}
	
	private void OnTriggerExit(Collider collider){
	
		if(collider.name == "Jumba"){
			
			if(statePlayer.getCurrentState() == PlayerState.Falling){
					
				player.parent = lastParent;

				player.transform.Rotate( player.transform.rotation.x, player.transform.rotation.y, 0 );

				player.transform.localScale = new Vector3( 1, 1, 1);

			}	
			else{
			
				this.collisionExited = true;
			
			}
									
		}
								
	}
		
}