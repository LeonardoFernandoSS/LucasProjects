using UnityEngine;
using System.Collections;
/*
 * Script de teleporte entre dois pontos
 * Reponsavel por teletraspotar objetos de um ponto para um outro.
 * */

public class TeleportBehaviour : MonoBehaviour {

	public ParticleSystem particle;

	public TouchLeft touchLeftMove;
	public TouchRight touchRightMove;

	/*
	 * doorTeleport primeira porta onde ira detectar colisão
	 * 
	 * */
	public GameObject doorTeleport;
	
	/**
	 * jumpTeleport variavel que verifica se pulou e se está dentro do objeto linkado!
	 * */
	
	private bool jumpTeleport;
	private Transform player;
	
	public void setJumpTeleport(bool jump) { this.jumpTeleport = jump; }

	public IEnumerator start() {

		yield return new WaitForSeconds(0.5f);

		this.doorTeleport.GetComponent<TeleportBehaviour>().setJumpTeleport(true);

		this.player.transform.position = doorTeleport.transform.position;

		this.StartCoroutine(finallyCoroutine());

	}
	public IEnumerator finallyCoroutine() {

		this.doorTeleport.GetComponent<TeleportBehaviour>().particle.Play();

		yield return new WaitForSeconds(0.5f);

		this.doorTeleport.GetComponent<TeleportBehaviour>().setJumpTeleport(true);
		this.touchLeftMove.CanMove = true;
		this.touchRightMove.CanTouch = true;
		this.touchRightMove.CanAttack = true;

	}

	void OnTriggerEnter(Collider collider)
	{
		
		if(!this.jumpTeleport) {

			if(collider.transform.tag == "Player") {

				this.particle.Play();
				this.touchLeftMove.CanMove = false;
				this.touchRightMove.CanTouch = false;
				this.touchLeftMove.setMoveleft(false);
				this.touchLeftMove.setMoveRight(false);
				this.touchRightMove.CanAttack = false;
				this.touchRightMove.setJumpTouch(false);
				player = collider.transform;
				StartCoroutine(start());

			}
		}
	}
	
	void OnTriggerExit(Collider collider)
	{
		if(collider.transform.tag == "Player") 
		{
				
			this.setJumpTeleport(false);
			player = collider.transform;

		}
	}
}
