using UnityEngine;
using System.Collections;

public class ControllerSpecial : MonoBehaviour {

	private bool jumbaSpecial;// Veriavel para verificar se esta no especial ou nao.
	private bool activeInstanteParticle; // variavel para controlar o instanciamento da particula
	private float time; // contador de tempo do especial
	private InterfaceJumba interfaceJumba; // variavel da classe InterfaceJumba
	
	public float timeSpecial; // tempo total que vai executar o especial
	public ParticleSystem special; // GameObject  particula

	public Transform 	rotateFather,
						rotateParticle;

	private SoundJumba sound;

	public void setJumbaSpecial( bool special) {
		this.jumbaSpecial = special;
	}
	
	public bool getJumbaSpecial(){
		return this.jumbaSpecial;	
	}
	
	public void setActiveInstanteParticle(bool activeInstanteParticle){
		this.activeInstanteParticle = activeInstanteParticle;
	}
	
	public bool getActiveInstanteParticle(){
		return this.activeInstanteParticle;
	}
	
	void Start(){
		
		this.activeInstanteParticle = false;
		this.time = this.timeSpecial;
		this.sound = GetComponent<SoundJumba>();
		this.jumbaSpecial = false;
		this.special.particleSystem.Stop();
		interfaceJumba = GetComponent<InterfaceJumba>();
		
	}

	void Update()
	{

		if(rotateFather.rotation.y == 0) 
			rotateParticle.localPosition = new Vector3(0, 0, -2);
		else 
			rotateParticle.localPosition = new Vector3(0, 0, 2);

		if(this.jumbaSpecial)
		{

			sound.startSpecial();

			CountTimeSpecial();

			if(rotateFather.rotation.x == 0) 
				rotateParticle.Rotate( new Vector3(rotateParticle.rotation.x, rotateParticle.rotation.y, -2) );
			else 
				rotateParticle.Rotate( new Vector3(rotateParticle.rotation.x, rotateParticle.rotation.y, 2) );

			if(this.activeInstanteParticle){
				
				special.particleSystem.Play();
				
				this.activeInstanteParticle = false;
				
			}
		}
		
	}

	/*
	 * metodo que ira iniciar o tempo do especial caso o jumba esteja de especial
	 * */
	void CountTimeSpecial()
	{
		if( this.time <= this.timeSpecial )
		{
			this.time -= Time.deltaTime;	
			
			float _hitbar = (this.time*100) / this.timeSpecial;
			
			interfaceJumba.HitBar(_hitbar);
			
		}
		if( this.time <= 0)
		{
				
			this.jumbaSpecial = false;
			special.particleSystem.Stop();
			this.time = timeSpecial;
		}		
	}
}
