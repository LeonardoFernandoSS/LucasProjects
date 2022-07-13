/* 
 * Versao 1.0
 * 
 * None: SubLevelInformation
 * 
 * Descriçao: 	Script responsavel por toda informaçao contida em cada objeto que ela estara como componente, exclusivamente os Subniveis
 * 				em cada nivel.
 * 				
 * Autor: Mateus R.Culpo
 * 
 * Date: 04/12/2013
 * 
 * Modificado por  	(DD/MM/YYYY)
 * ########			############	#########
	
 */

using UnityEngine;
using System.Collections;

public class SubLevelInformation : MonoBehaviour {

	public GameObject locked; // Objeto que vai mostrar o cadioado

	public TextMesh textLevel; // Texto que vai ser mostrado no botao
	
	private LevelState levelState = LevelState.locked; // Inicia todos os leveis ja trancados
	
	public int level; // valor da scene
	
	public void setLevelState(LevelState level) { this.levelState = level; }
	
	public LevelState getLevelState () { return this.levelState; }
	
	public void setLevel(int value) { this.level = value;}
	
	public int getLevel () { return this.level; }

	public void setTextLevel (string value) { this.textLevel.text = value;}

	/*
	 * Metodo que check qual estado do level (se esta aberto ou trancado )
	 * assim setando seu estado atual
	 */
	public void checkLevelState(LevelState level){
		// estadoatual recebe o level passado por parametro
		this.levelState = level;
		
		switch(level) {
			// caso ele estiver trancado  ativar o cadiado nao deixando ter a visao e nem clicar
			case LevelState.locked :{
				locked.SetActive(true); 
				break;	
			}
			// caso o estado for normal ele desativa o cadido	
			case LevelState.normal :{
				locked.SetActive(false); 
				break;
			}
		}
	}
	/*
	 * Metodo responsavel por chamar o diretor ou a message para carregar o nivel desejado
	 */
	public void buttonClickEnter () {
		switch (this.levelState){
			// caso o level seja normal (chamar diretor para atribuir a cena que vai carregar)
			case LevelState.normal:{
				
				LevelManager.idSceneToLoad = level;

				Director.sharedDirector().LoadLevelWithFade(Level.inventory.GetHashCode());

				break;
			}

			// Caso o level esteja trancado chama Mensage para poder ativar uma mensagem que nao sera possivel acessar a fase
			case  LevelState.locked:{

				// quando clicar e o statos do level for igual a locked faça as operaçoes dentro desse case
				break;
				
			}
		}
	}
}
