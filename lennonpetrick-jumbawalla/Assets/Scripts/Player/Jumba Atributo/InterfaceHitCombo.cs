using UnityEngine;
using System.Collections;

public class InterfaceHitCombo : MonoBehaviour {
	// Controlador do combohit do player -- Variavel para acesso de metodos
	private ControllerHitPlayer controllerHitPlayer;
	
	// numero conta os hit para controle de aparecer no display
	private float numberHit;
	
	// Text que vai ser modificado acada combo hit dado pelo jogador
	public TextMesh textHit;
	
	// Animaçao do hitCombo
	public Animation animationHit;
	
	// Animaçao da Camera tremendo
	public Animation animatinCam;
	
	void Start()
	{
		// busca componente de controle do Hit
		this.controllerHitPlayer = GetComponent<ControllerHitPlayer>();
		
	}
	/*
	 * AddHitAnimation adiciona hitcombo no texto da animaçao, executa o Play() da animaçao e controla o displey
	 * da animaçao na tela
	 * */
	public void AddHitAnimation(){

		if(controllerHitPlayer.getHitCombo() >= 2){
			
			// Se o hit combo for diferente do numberHit(controlador do displya da animaçao)
			if(controllerHitPlayer.getHitCombo() != this.numberHit){
				//Text da animaçao recebe hitcombo
				this.textHit.text = controllerHitPlayer.getHitCombo().ToString();
				// numberHit recebe o hit atual
				this.numberHit = controllerHitPlayer.getHitCombo();
				
				//disparar as animaçoes de do hitcombo e camera
				this.animationHit.Play();
				this.animatinCam.Play();
			}
		}
	}
}
