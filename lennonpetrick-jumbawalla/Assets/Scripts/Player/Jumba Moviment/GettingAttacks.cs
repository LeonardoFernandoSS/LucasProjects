/* 
 * Versao 1.0
 * 
 * None: GettingAttack
 * 
 * Descriçao: Controla o impulso que o Player vai levar dependendo do Golpe do inimigo
 * 				
 * Autor: Mateus R.Culpo
 * 
 * Date: 12/12/2013
 * 
 */
using UnityEngine;
using System.Collections;

public class GettingAttacks : MonoBehaviour {

	private bool sideMove,
				 checkPush;

	private float time;

	private float timeCount;

	private float speedMoveX;

	private CharacterController controler;

	private float speedX, speedY;
	private ControllerHitSequence controllerHitSequence;
	private RayCastDistance raycastDistance;

	public void setProperties ( bool side, float time, float speedX, float speedY ) {

		this.sideMove = side;
		this.time = time;
		this.speedMoveX = speedX;
		this.speedY = speedY;
	}


	void Start() {

		this.controler = GetComponent<CharacterController>();
		this.checkPush = false;
		this.controllerHitSequence = GameObject.Find("Jumba").GetComponent<ControllerHitSequence>();
		this.raycastDistance = GameObject.Find("Jumba").GetComponent<RayCastDistance>();
	}

	void Update () {

		if(checkPush) {

			controllerHitSequence.setLeadingHit(true);

			controler.Move( new Vector3( speedX * Time.deltaTime, 
			                             speedY * Time.deltaTime, 
			                           	 0) );

			if( timeCount <= time ) 
				timeCount += Time.deltaTime;

			else {


				controllerHitSequence.setLeadingHit(false);

				controllerHitSequence.ClearList();

				checkPush = false;

				timeCount = 0;

			}
		}

	}

	public void addPush () {

		if(!checkPush)  {

			speedX = sideMove ? (speedMoveX * 1) : (speedMoveX * -1);

			speedY = raycastDistance.getHitFloor() ? speedY : 0;

			checkPush = true;

		}

	}

	public static GettingAttacks checkGettingAttacks () {

		return GameObject.Find("Jumba").GetComponent<GettingAttacks>();

	}
}
