/* 
 * Versao 1.0
 * 
 * None: MenuController
 * 
 * Descriçao: 	O controlador de todas variaveis composta no menu, qual cena que ele esta no momento e qual animaçao executar
 * 				
 * Autor: Mateus R.Culpo
 * 
 * Date: 02/12/2013
 * 
 * Modificado por  	(DD/MM/YYYY)
 * 
 * ########			############	#########
 */

using UnityEngine;
using System.Collections;
 
public class MenuController : MonoBehaviour {

	private EnumSceneMenu enumSceneMenu; // Enum das miniCenas que tem o menu principal do jogo

	public Animation animationMenu; // Controlador da animaçao 

	void Start(){ this.enumSceneMenu = EnumSceneMenu.Menu; }

	public void setEnumSceneMenu (EnumSceneMenu value) { this.enumSceneMenu = value; }

	public EnumSceneMenu getEnumSceneMenu () { return this.enumSceneMenu; }
	 
	public void buttonScenePlay (EnumSceneMenu value) { 

		switch (value){

			case EnumSceneMenu.Menu:{ 
				
				animationMenu.Play("menu-fases");
				
				this.enumSceneMenu = EnumSceneMenu.Fases;
				
				break;
			}

			case EnumSceneMenu.Armas:{
				
				animationMenu.Play("fases-armas");
				
				this.enumSceneMenu = EnumSceneMenu.Armas;
				
				break;
			}
		}
	}
	 
	public void buttonSceneReturn (EnumSceneMenu value){
		switch (value){
				
			case EnumSceneMenu.Fases:{
				
				animationMenu.Play("fases-menu");

				this.enumSceneMenu = EnumSceneMenu.Menu;

				break;
			}
				
			case EnumSceneMenu.Opcoes:{
				
				animationMenu.Play("opcoes-menu");
				
				this.enumSceneMenu = EnumSceneMenu.Menu;
				
				break;
			}
				
			case EnumSceneMenu.Colecao:{

				animationMenu.Play("colecao-menu");
				
				this.enumSceneMenu = EnumSceneMenu.Menu;

				break;
			}
			case EnumSceneMenu.Creditos:{

				animationMenu.Play("creditos-opcoes");
				
				this.enumSceneMenu = EnumSceneMenu.Opcoes;

				break;
			}
				
			case EnumSceneMenu.Tutorial:{

				animationMenu.Play("tutorial-opcoes");
				
				this.enumSceneMenu = EnumSceneMenu.Opcoes;

				// destroy the objects in the scrollable area of tutorial
				StartCoroutine(GetComponent<LoadTutorial>().destroyTutorial());

				break;
			}
				
			case EnumSceneMenu.Personagem:{
				
				animationMenu.Play("personagem-opcoes");

				this.enumSceneMenu = EnumSceneMenu.Opcoes;

				// destroy the objects in the scrollable area of characters
				StartCoroutine(GetComponent<LoadCharacters>().destroyCharacters());

				break;
			}
				
			case EnumSceneMenu.Armas:{
				
				animationMenu.Play("armas-fases");

				this.enumSceneMenu = EnumSceneMenu.Fases;
				
				break;
			}
			
		}
	}

	public void buttonSceneOption (EnumSceneMenu value){
		switch (value){
			case EnumSceneMenu.Menu:{

				animationMenu.Play("menu-opcoes");

				this.enumSceneMenu = EnumSceneMenu.Opcoes;

				break;
			}
		}
	}
	  
	public void buttonSceneStore (EnumSceneMenu value){
		switch (value){
			case EnumSceneMenu.Menu:{
				
				animationMenu.Play("menu-personagem");
					
				this.enumSceneMenu = EnumSceneMenu.Personagem;
				
				break;
			}
		}
	}

	public void buttonSceneCollections(EnumSceneMenu value){
		
		switch (value){
			case EnumSceneMenu.Colecao:{
				
				animationMenu.Play("menu-colecao");
				
				this.enumSceneMenu = EnumSceneMenu.Colecao;
				
				break;
			}
		}
	}
	public void buttonScenePersonagem(EnumSceneMenu value){
		
		switch (value){
			case EnumSceneMenu.Personagem:{
				
				animationMenu.Play("opcoes-personagem");
				
				this.enumSceneMenu = EnumSceneMenu.Personagem;
				
				break;
			}
		}
	}

	public void buttonSceneTutorial(EnumSceneMenu value){
		
		switch (value){
			case EnumSceneMenu.Tutorial:{
				
				animationMenu.Play("opcoes-tutorial");
				
				this.enumSceneMenu = EnumSceneMenu.Tutorial;
				
				break;
			}
		}
	}

	public void buttonSceneCreditos(EnumSceneMenu value){
		
		switch (value){
			case EnumSceneMenu.Creditos:{
				
				animationMenu.Play("opcoes-creditos");	
				
				this.enumSceneMenu = EnumSceneMenu.Creditos;
				
				break;
			}
		}
	}

}

public enum EnumSceneMenu {
	
	Menu,
	Fases,
	Opcoes,
	Colecao,
	Creditos,
	Tutorial,
	Personagem,
	Armas
}
