using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class EnemyTypeDino : MonoBehaviour {
	
	private CharacterController character;
	
	public int speedEnemy;
	
	public float gravity;

	private Animation swingingCamera; 
	
	private Vector3 moveDirection;

	void Start () {

		character = GetComponent<CharacterController>();

		swingingCamera = GameObject.Find("Main Camera").GetComponent<Animation>();

	}
	
	
	void Update () {
		
	
		if(character.isGrounded){

			if(!swingingCamera.IsPlaying("tremeCamera")){
				swingingCamera.Play("tremeCamera");
			}

			this.moveDirection = new Vector3 (-speedEnemy, 0 ,0 );	
			
		}
		
		moveDirection.y -= gravity;
		
		character.Move(moveDirection * Time.deltaTime);
		
	}
}
