using UnityEngine;
using System.Collections;

public class ControllerLifePlayer : MonoBehaviour {

	private TouchLeft left;
	private TouchRight right;

	private InterfaceJumba interfaceJumba;

	public float lifePlayer;
	
	public void setLifePlayer(float life){ this.lifePlayer = life;}
	
	public float getLifePlayer() {return this.lifePlayer;}

	private bool 	diePlayer = false,
					playAudio = false;


	void Start()
	{
		this.interfaceJumba = GetComponent<InterfaceJumba>();
		right = GameObject.Find("DirTouch").GetComponent<TouchRight>();
		left = GameObject.Find("EsqTouch").GetComponent<TouchLeft>();
	}

	void Update() 
	{
		if (this.playAudio && !audio.isPlaying) 
		{
			audio.Play();
		}
		else if(!this.playAudio) 
		{
			audio.Stop ();
		}
	}

	public void AddLifePlayer(float life)
	{
	
		this.lifePlayer += life;
		
		if(this.lifePlayer >= 100)
		{
			
			this.lifePlayer = 100;
			
		}

		checkLifeAudio();

		interfaceJumba.LifeBar(this.lifePlayer);
		
	}
	
	public void RemoveLifePlayer(float life)
	{
		this.lifePlayer -= life;

		if(this.lifePlayer <= 1 && this.lifePlayer > -1000)
		{

			right.CanTouch = false;
			left.CanMove = false;

			gameObject.layer = (int) TypeLayer.Morto;

			Director.sharedDirector().endScene(Director.SceneEndedStatus.lost);

			this.lifePlayer = -10;

		}

		checkLifeAudio();

		interfaceJumba.LifeBar(this.lifePlayer);

	}

	void checkLifeAudio() {

		if(this.lifePlayer <= 35 && this.lifePlayer >= 1 ) 
			this.playAudio = true;
		else 
			this.playAudio = false;

	}

	public static ControllerLifePlayer sharedLife(){
		return GameObject.Find("Jumba").GetComponent<ControllerLifePlayer>();
	}
	
}
