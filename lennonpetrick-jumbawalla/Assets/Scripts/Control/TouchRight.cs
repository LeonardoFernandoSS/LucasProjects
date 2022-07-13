/* 
 * Versao 1.0
 * 
 * None: TouchRight
 * 
 * Descriçao: Responsavel pela controle do touch do lado direito ( Pulo, e attack)
 * 				
 * Autor: Mateus R.Culpo 
 * 
 * Date: 03/01/2014
 *
 * ########			############	########
 */
using UnityEngine;
using System.Collections;

public class TouchRight : MonoBehaviour {
	
	public float minMovement = 1.0f;
	
	private Vector2 StartPos;
	private int SwipeID = -1;
	
	private bool canAttack;
	private bool canTouch;
	
	private Vector2 delta;

	private bool jumpTouch, attackTouch, activeAttack;

	public void setJumpTouch(  bool value ) { this.jumpTouch = value; }
	public bool isJumpTouch() { return this.jumpTouch; }
	public void setAttackTouch(bool value) { this.attackTouch = value; }
	public bool isAttackTouch() { return this.attackTouch; }

	public bool CanAttack {

		get {
			return this.canAttack;
		}
		set {
			canAttack = value;
		}
	}

	public bool CanTouch {
		get {
			return this.canTouch;
		}
		set {
			canTouch = value;
		}
	}

	void Start() {
		this.canAttack = true;
		this.canTouch = true;
	}

	void Update () {

		if(this.canTouch) {
			foreach (var touch in Input.touches) {

				if( touch.position.x >= Screen.width / 2 && touch.position.y <= Screen.height - ( Screen.height / 5 ) ){ 

					var _p = touch.position;
						
					if (touch.phase == TouchPhase.Began && SwipeID == -1) {

						activeAttack = true;
						SwipeID = touch.fingerId;
						StartPos = _p;

					}
						
					else if (touch.fingerId == SwipeID) {

						delta = _p - StartPos;
							
						if (touch.phase == TouchPhase.Moved && delta.magnitude > minMovement) {

							SwipeID = -1;

							if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x)) {
									
								if (delta.y > 0 ) {

									jumpTouch = true;

								}
							}
						}	
					}

					if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended) {

						if(canAttack) {

							if( delta.magnitude < minMovement && activeAttack) {
								attack();
							}
						}

						this.SwipeID = -1;
						this.StartPos = Vector2.zero;

					}
				}

				if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended) {
					jumpTouch = false;
				}
			}
		}

	}

	void attack() {

		activeAttack = false;

		attackTouch = true;

	}	
}

