using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class EnemyTypeDinoRun : MonoBehaviour {

	public EnemyAnimator animation;

	private CharacterController character;
	private Transform player;
	private Vector3 moveDirection;
	private bool jumpAttack,
				 move;

	public int speedEnemy;
	public float gravity;

	void Start () {

		this.character = GetComponent<CharacterController>();
		this.player = GameObject.Find("Jumba").transform;
		this.jumpAttack = true;
		this.move = true;
		// muda velocidade de acordo com o lado do inimigo
		this.speedEnemy = this.speedSide() == true ? this.speedEnemy : this.speedEnemy * -1;
		// rotaciona o inimigo de acordo com o lado que esta
		this.transform.rotation = this.speedSide() == true ? new Quaternion(transform.rotation.x, 0,
		                                                                    transform.rotation.z,
		                                                                   	transform.rotation.w) :
															 new Quaternion(transform.rotation.x, 180,
															                transform.rotation.z,
			               												    transform.rotation.w) ;
	}

	void Update () {
		
		if(this.character.isGrounded){

			float _distance = Mathf.FloorToInt( Vector3.Distance(player.transform.position, transform.position)/transform.lossyScale.magnitude);

			if(_distance >= 2 && _distance <= 10 && this.jumpAttack) {

				this.move = false;
				this.jumpAttack = false;
				StartCoroutine(jumpDino());

			}

			this.moveDirection = new Vector3 (-this.speedEnemy, 0 ,0 );

			if(animation.getState() != stateAnim.run && this.move)
				animation.setState(stateAnim.run);
		}

		this.moveDirection.y -= gravity;

		this.character.Move(moveDirection * Time.deltaTime);
		
	}

	public void chargeAnimator()
	{
		animation.setState(stateAnim.run);
	}

	IEnumerator jumpDino ( ) {

		animation.setState(stateAnim.knock);

		yield return new WaitForSeconds( 1 );

		this.move = true;

	}

	bool speedSide() {

		return (transform.position.x - player.position.x) < 0 ? false : true;

	}

}
