/* 
 * Versao 1.2
 * 
 * None: ControllerHitSequence
 * 
 * Descriçao: Controle da execuçao dos combos
 * 				
 * Autor: Mateus R.Culpo 
 * 
 * Date: 15/11/2013
 * 
 * Modificado por Mateus R.Culpo	(13/12/2013)
 * - Alteraçoes em todo processo de combo (adptaçao para combo com retorno ( toda animaçao de golpe tem sua animaçao de retorno )
 * - Criaçao de uma lista , onde sera armazenado os id's dos combos
 * 					(16/12/2013)
 * - Finalizaçao do processo de combo ( sem auteraçoes no mesmo )
 * ########			############	########
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 
 * ### AnimationSequenceBehaviour  ###
 * 
 * Script que trata de todos os combos que for passado como parametro
 * Cabe ao Ai do Inimigo e comandos nos Personagens
 * para aplicar o metodo nos mesmo.
 * 
 */


public class ControllerHitSequence : MonoBehaviour {

	private bool 	collisionEnemy, // colisao com inimigo
					checkCombo, // verifica se esta rodando as animaçoes de combo
					leadingHit, // verifica se esta levando golpe
					activeCombo; 

	private int 	currentIdCombo; // currentIdCombo, numeros de golpes que podem variar entre o combo
	
	[SerializeField] private List < int > runListCombo; // Lista de combos para serem executado

	private ControllerHitPlayer controllerHitPlayer;
	private PlayerStateControl playerStateControl;


	#region GETTER SETTER
	
	public void setCollisionEnemy(bool col){ 

		this.collisionEnemy = col; 

	}
	
	public bool getCollisionEnemy(){ return this.collisionEnemy; }

	public void setCurrentIdCombo (int value) { this.currentIdCombo = value; }

	public int getCurrentIdCombo () { return this.currentIdCombo; } 

	public void setLeadingHit (bool value) { this.leadingHit = value; }

	public bool getLeadingHit () { return this.leadingHit; }
	#endregion
	

	void Start()
	{

		this.currentIdCombo = 0;
		this.checkCombo = false;
		this.collisionEnemy = false;
		GameObject jumba =  GameObject.Find("Jumba");
		this.controllerHitPlayer = jumba.GetComponent<ControllerHitPlayer>();
		this.playerStateControl = jumba.GetComponent<PlayerStateControl>();

	}

	void LateUpdate () {

		runList();

	}

	// Limpa a lista e ja seta os valores nescessarios
	public void ClearList () {

		this.collisionEnemy = false;
		this.runListCombo.Clear();
		this.currentIdCombo = 0;
		this.checkCombo = false;

	}

	// Adiciona Combo Hit
	public void addComboHit() {

		// se o jogador estiver levando o golpe ele nao pode bater
		if(!leadingHit) {

			// Caso for verdadeiro o golpe no inimigo e as animaçoes estiverem rodando ainda
			if( playerStateControl.isBlowRunAnimation()) {

				// se o numero do tamanho da lista for maior que 3
				if(runListCombo.Count < 5) {

					activeCombo = true;
				
				}

			}
			 // Caso ele bateu sem colidir
			else  {

				// se a lista estiver vazia
				if(runListCombo.Count == 0 ) {

					// o idCombo recebe 2
					currentIdCombo = 2;

					// Adiciona a  animaçao 2 na lista
					AddListComboHit( checkIdCombo ( currentIdCombo ) );

				}
			}
		}

		if(this.collisionEnemy) {
			controllerHitPlayer.resetTimeTrue();
		}
	}

	// Checar id do combo
	int checkIdCombo ( int value ) {

		// Caso o valor do combo seja maior ou igual ao numero maximo
		if(value > 7)  
			currentIdCombo = 0; // currentIdCombo igual a 0
	

		if(currentIdCombo == 0) 
			currentIdCombo = 2;

		return currentIdCombo; // retorna o currentIdCombo

	}
	/*	Metodo responsavel por adicionar combo na lista , basicamente ele remove o ultimo id ( id que executaria o retorno do golpe sendo executado),
	 * 	depois adiciona um novo golpe junto com seu id de retorno
	 * 
	 */	
	void AddListComboHit ( int value ) {

		// se a lista nao for  vazia ela deleta o ultimo id!
		if(runListCombo.Count != 0)
			runListCombo.RemoveAt(runListCombo.Count - 1);

		// Adiciona o novo valor passado por parametro
		runListCombo.Add( value );

		// Adiciona o retorno do id Combo adicionado logo acima
		runListCombo.Add( value + 1 );

		/*
		 * Exemplo a lista tem [2,3] vc adiciona  combo 4 basicamente ele vai ficar nessa ordem [2,4,5]
		 */
	}

	/*
	 * Onde sera executada a animaçoes de acordo com o id, sempre sera executado o id que estiver na posiçao 0 da lista
	 */
	void runList() {

		// se a lista for vazia
		if(runListCombo.Count == 0) {

			ClearList(); // Limpo a lista  
			
		}
		// Se a lista for diferente de 0
		if(runListCombo.Count != 0){

			// se a lista for diferente de 0 e nenhuma animaçao estiver rodando
			if(runListCombo.Count != 0 && !playerStateControl.isBlowRunning() && !checkCombo) {

				checkCombo = true; // CheckCombo e igual true

				playerStateControl.setState ( PlayerState.Attacking, runListCombo[0] ); // seto o estado atacando ao jogando passando como parametro o idCombo que esta
				// na posiçao 0 na lista
			}

			//  Se nao estiver rodando nenhuma animaçao e o Checkcombo for true
			if( !playerStateControl.isBlowRunning() && checkCombo ) {
				// seta o checkcombo como false
				checkCombo = false;
				runListCombo.RemoveAt(0); // remova o id da lista que esteja na posiçao 0

				if(activeCombo) {
					currentIdCombo += 2; // current id recebe ele mais 2 (ou seja caso ele entre ele vai adicionar dois numero afrente, porq a sequenciade combo esta como 1, 3, 5 assim em diante)
					
					AddListComboHit( checkIdCombo(currentIdCombo) ); // adiciona os combos na lista

					activeCombo = false;
				}
			}
		}
	}

	public static ControllerHitSequence checkHitSequence() {

		return GameObject.Find("Jumba").GetComponent<ControllerHitSequence>();

	}

}

