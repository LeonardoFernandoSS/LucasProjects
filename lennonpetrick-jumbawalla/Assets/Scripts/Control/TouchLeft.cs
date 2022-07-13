/* 
 * Versao 1.0
 * 
 * None: TouchLeft
 * 
 * Descriçao: Responsavel pela controle do touch do lado esquerdo ( movimentaçao )
 * 				
 * Autor: Mateus R.Culpo 
 * 
 * Date: 03/01/2014
 *
 * ########			############	########
 */

using UnityEngine;
using System.Collections;

public class TouchLeft : MonoBehaviour {

	public float modifyDistance;

	private Vector2 startPos, positionMove, lastPositionMove;

	private int idTouch;

	private bool moveTouchPadLeft, moveTouchPadRight, canMove = true;

	public void setMoveleft ( bool value ) { this.moveTouchPadLeft = value; }
	public void setMoveRight ( bool value ) {this.moveTouchPadRight = value;}

	public bool isMoveTouchPadLeft () {  return this.moveTouchPadLeft; }

	public bool isMoveTouchPadRight () { return this.moveTouchPadRight; }

	public bool CanMove {
		get {
			return this.canMove;
		}
		set {
			canMove = value;
		}
	}

	void Update ()
	{
		if(canMove) {
			foreach (var touch in Input.touches) {

				if( touch.position.x <= Screen.width / 2 ) { 

					Vector2 _p = touch.position;
					
					if ( touch.phase == TouchPhase.Began ) {

						startPos = _p;
						idTouch = touch.fingerId;
					}
					else if ( touch.phase == TouchPhase.Moved ) {

						lastPositionMove = positionMove;
						positionMove = touch.position;

						checkMove();

						if(positionMove.x < lastPositionMove.x && !moveTouchPadRight) {
							startPos.x = lastPositionMove.x + modifyDistance;

						}
						else if (positionMove.x > lastPositionMove.x && !moveTouchPadLeft) {
							startPos.x = lastPositionMove.x - modifyDistance;
						}
					}
				}

				if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended) {

					if(touch.fingerId == idTouch) {

						this.startPos = Vector2.zero;
						this.positionMove = Vector2.zero;
						this.lastPositionMove = Vector2.zero;
						this.moveTouchPadRight = false;
						this.moveTouchPadLeft = false;
					}
				}
			}
		}
	}

	void checkMove() {

		if( positionMove.x > startPos.x ) {

			this.moveTouchPadRight = true;
			this.moveTouchPadLeft = false;

		}
		else if ( positionMove.x < startPos.x ) {

			this.moveTouchPadLeft = true;
			this.moveTouchPadRight = false;

		}
	}

}
