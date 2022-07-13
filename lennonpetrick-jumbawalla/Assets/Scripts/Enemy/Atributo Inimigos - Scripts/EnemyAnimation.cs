using UnityEngine;
using System.Collections;
using SmoothMoves;


public class EnemyAnimation : MonoBehaviour {
	private TypeEnemyState typeEnemyState;
	
	public BoneAnimation animation;
	
	public void setTypeEnemyState(TypeEnemyState typeEnemyState){

		this.typeEnemyState = typeEnemyState;

		animationEnemy();

	}
	public TypeEnemyState getTypeEnemyState(){
		return this.typeEnemyState;
	}

	public BoneAnimation getAnimation(){
		return this.animation;
	}

	public bool isAnimationPlaying(){
		if(	animation.IsPlaying("batendo") || animation.IsPlaying("batendo2") ){

			return true;
		}
		else {

			return false;
		}
	}
	
	void animationEnemy()
	{
		
		switch(typeEnemyState)
		{
			
			case TypeEnemyState.Idle:	
			{
				
				animation.Play("parado");
			
				break;
			}
		
			case TypeEnemyState.Attack:	
			{

				animation.Play("batendo");

			
				break;
			}
			case TypeEnemyState.Attack2:	
			{
				
				animation.Play("batendo2");
			
				break;
			}
			case TypeEnemyState.GettingAttack:	
			{
				
				animation.Play("golpe");
				break;
			}
			case TypeEnemyState.Death:	
			{
				
				animation.Play("morrendo");
				break;
			}
			case TypeEnemyState.Run:	
			{
				
				animation.Play("andando");
				break;
			}
			case TypeEnemyState.BackRun:	
			{
				
				animation.Play("andandoTraz");
				break;
			}
			
			case TypeEnemyState.DeathFloor:	
			{
				
				animation.Play("morreu");
				break;
			}
			case TypeEnemyState.attackJump:	
			{
				
				animation.Play("golpe03Pulo");
				break;
			}
			case TypeEnemyState.attackFalling:	
			{
				
				animation.Play("golpe03Queda");
				break;
			}
	
		}
		
		
	}
}




public enum TypeEnemyState
{
	
	Run,
	Attack,
	Attack2,
	attackJump,
	attackFalling,
	GettingAttack,
	Death,
	Idle,
	BackRun,
	DeathFloor
}
