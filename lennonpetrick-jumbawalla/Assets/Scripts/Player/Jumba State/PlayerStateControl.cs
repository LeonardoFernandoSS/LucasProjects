using SmoothMoves;
using UnityEngine;
using System.Collections;

public class PlayerStateControl : MonoBehaviour {

	public AudioClip golpe01;
	public AudioClip golpe02;
	public AudioClip golpe03;

	private StrikeForce strikeForce;
	
	public BoneAnimation currentAnimation;
	
	private PlayerState currentState;
	
	private int blowID = 0;
	
	private bool sideMove;
	
	private float valueNumber;

	private GettingAttacks gettingAttack;
	
	void Start(){
		
		this.strikeForce = GetComponent<StrikeForce>();

		gettingAttack = GameObject.Find("Jumba").GetComponent<GettingAttacks>();
		
	}
	
	public void setState(PlayerState state){
		
		this.currentState = state;
				
		this.refreshState();
		
	}
	
	public void setState(PlayerState state, int blowID){
		
		this.blowID = blowID;

		this.setState(state);

	}

	public BoneAnimation getAnimation(){
		return this.currentAnimation;
	}

	public PlayerState getCurrentState(){
		
		return this.currentState;
		
	}
	
	public bool isJumpingRunning(){
		
		if(currentAnimation.IsPlaying("pulando") || currentAnimation.IsPlaying("pulandoduplo")){
			
			return true;
			
		}
		else {
			
			return false;
			
		}
		
	}
	
	public bool isBlowRunning(){
		
		if(	currentAnimation.IsPlaying("golpePulo") || currentAnimation.IsPlaying("golpe01retorno") || currentAnimation.IsPlaying("golpe02retorno") || currentAnimation.IsPlaying("golpe03retorno")  
		    ||currentAnimation.IsPlaying("golpe01") || currentAnimation.IsPlaying("golpe02") || currentAnimation.IsPlaying("golpe03")  
		   || currentAnimation.IsPlaying("golpePulo") ||  currentAnimation.IsPlaying("apanhando") ||  currentAnimation.IsPlaying("morrendo")) {
			
			return true;
			
		}
		else {
			
			return false;
			
		}
		
	}

	public bool isBlowRunAnimation () {

		if(	currentAnimation.IsPlaying("golpe03") || currentAnimation.IsPlaying("golpe01")  || currentAnimation.IsPlaying("golpe02") 
		   || currentAnimation.IsPlaying("golpePulo") ||  currentAnimation.IsPlaying("apanhando") ||  currentAnimation.IsPlaying("morrendo")){

			return true;
			
		}
		else {
			
			return false;
			
		}
	}
	
	private void refreshState() {
		
		switch(currentState){

			case PlayerState.Idle : {
				
				currentAnimation.Play("parado");
				break;

			}

			case PlayerState.Winner : {

				currentAnimation.Play("ganhando");

				

				break;

			}
			case PlayerState.Running : {
						
				currentAnimation.Play("andando");
								
				//ANIMAÇAO CORRENDO
		
				break;
			}
		
			case PlayerState.Jumping : {
		
				currentAnimation.Play("pulando");
				//ANIMAÇAO PULANDO
		
				break;
			}
			
			case PlayerState.DobleJumping : {
		
				currentAnimation.Play("pulandoduplo");
				//ANIMAÇAO PULANDO
		
				break;
			}
					
			case PlayerState.Falling : {			
				
				currentAnimation.Play("caindoLoop");
														
				//ANIMAÇAO CAINDO
		
				break;
			}
		
			case PlayerState.Dead : {

				currentAnimation.Play("morrendo");
				//ANIMAÇAO MORRENDO
		
				break;
			}

			case PlayerState.DeadFloor : {

				
				currentAnimation.Play("morreu");
				//ANIMAÇAO MORRENDO
				
				break;
			}
			
			case PlayerState.LeadingAttack : {	
				
				currentAnimation.Play("apanhando");
				GetComponent<SoundJumba>().startSoundGetting();
				gettingAttack.addPush();
				break;
			}
		
			case PlayerState.JumpAttacking : {
							
				currentAnimation.Play("golpePulo");
		

				//ANIMAÇAO GOLPE PULANDO			
				break;
			}
		
			case PlayerState.Attacking : {
				
				// Verificar qual a rotaçao do PLayer
				if(transform.rotation.y == 0) this.valueNumber = 4f;
				else  this.valueNumber = -4f;
			
				switch(blowID){
				
					case 2 : {
				
						currentAnimation.Play("golpe01");

						strikeForce.setBlowDamage(20);

						Director.sharedDirector().playEffect(golpe01);

						break;
					}
				
					case 3 : {
				
						currentAnimation.Play("golpe01retorno");

						break;
					}
				
					case 4 : {
				
						currentAnimation.Play("golpe02");

						strikeForce.setBlowDamage(50);
						
						CharacterController control = GetComponent<CharacterController>();

						control.Move(new Vector3(valueNumber, 0, 0));
						
						Director.sharedDirector().playEffect(golpe02);

						break;
					}
				
					case 5 : {
				
						currentAnimation.Play("golpe02retorno");
				
						break;
					}
					case 6 : {
				
						currentAnimation.Play("golpe03");

						strikeForce.setBlowDamage(50);

						Director.sharedDirector().playEffect(golpe03);

						break;

					}

					case 7: {

						currentAnimation.Play("golpe03retorno");
						
						break;
					}
								
				}
		
				break;
			}
							
		}
		
	}




	public static PlayerStateControl sharePlayer(){

		return GameObject.Find("Jumba").GetComponent<PlayerStateControl>();

	}

}