using UnityEngine;
using System.Collections;

public class ControllerHitPlayer : MonoBehaviour {

	private InterfaceJumba interfaceJumba;
	private ControllerSpecial controllerSpecial;
	// Responsavel pelo texto display do combo
	private float hitComboText;
	
	// Contador dos hits em cada inimigo
	private float countHit;
	
	// Responsavel pela barra de hitCombo no display
	private float hitCombo;
	
	// Animaçao da camera
	public GameObject focusPrefab;
	private GameObject targetFocus;
	
	// Contador que dispara a animaçao da camera trabalha junto com o combo Hit
	private int countAnimationEffectCam;

	//Contador para executar o golpe extra
	private int countExtraAttack;
	
	private ControllerSpecial special;

	public bool resetTime;

	public float currentTime;

	void Start()
	{
		this.interfaceJumba = GetComponent<InterfaceJumba>();
		this.controllerSpecial = GetComponent<ControllerSpecial>();
	}

	void Update () {

		if(resetTime) {

			if(currentTime <= 2f) {

				currentTime += Time.deltaTime;

			} else if (currentTime >= 2f){

				currentTime = 0;
				RestartHitCombo();
				resetTime = false;

			}
		}

	}

	public void resetTimeTrue () {
		this.resetTime = true;
		this.currentTime = 0;
	}

	public void AddHitCombo()
	{
		//Contador do hit recebe +
		this.countHit++;
		
		//Contador do hit camera
		if(!this.targetFocus)
			this.countAnimationEffectCam++;

		/*
		 * Caso Counthit for igual a 2
		 * zero a variavel e adiciono um combo hit do jogador
		 * */
		if(this.countHit == 2){
			
			if(this.hitComboText <= 100){

				this.hitComboText ++;
				this.countExtraAttack ++;
				
			}
			this.countHit = 0;
		}
		#region CONTROLE DISPLEY FOCO
		/* contador da animaçao for maior ou igual a 16 verifica se pode tocar a animaçao,
		 * se sim, dispara animaçao da camera
		 * */
		if( this.countAnimationEffectCam >= 10 ) {

			if(!this.targetFocus) {

				this.targetFocus = Instantiate( focusPrefab, focusPrefab.transform.position, Quaternion.identity) as GameObject;

				this.targetFocus.transform.parent = GameObject.Find("Main Camera").transform;

				this.targetFocus.transform.localPosition = focusPrefab.transform.position;

				this.targetFocus.name = "focus";

				this.countAnimationEffectCam = 0;
			}
		}

		#endregion
		
		/*
		 * Verifica se a barra esta menor que 100% se estiver add hitcombo
		 * */
		
		if(!controllerSpecial.getJumbaSpecial()) 
		{
			
			if(this.hitCombo <= 100)
			{
				
				this.hitCombo += 2;

				interfaceJumba.HitBar(this.hitCombo);
				
			}
			
			if(this.hitCombo >= 100) 
			{
				this.hitCombo = 0;
				
				controllerSpecial.setActiveInstanteParticle(true); // seta true na variavel para instanciar a particula do especial
				
				controllerSpecial.setJumbaSpecial(true); // Especial esta ativa
			}
		}
	}

	/*
	 * Combo hit se torna zero!
	 * */

	void RestartHitCombo()
	{
		this.hitComboText = 0;
		this.countAnimationEffectCam = 0;
	}
	
	#region Setter Getter
	public void setHitCombo (float hitCombo) { this.hitComboText = hitCombo;}
	
	public float getHitCombo () {return this.hitComboText;}
	
	public void setCountExtraAttack(int count) {this.countExtraAttack = count;}
	
	public int	getCountExtraAttack() {return this.countExtraAttack;}
	#endregion
}
