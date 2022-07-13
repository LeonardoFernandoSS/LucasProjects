/* 
 * Versao 1.0
 * 
 * None: LevelInformation
 * 
 * Descriçao: 	Script responsavel por carregar toda informaçao contida em cada objeto que ela estara como componente
 * 				
 * Autor: Mateus R.Culpo
 * 
 * Date: 04/12/2013
 * 
 * Modificado por  	(DD/MM/YYYY)
 * 
 * ########			############	#########
	
 */
using UnityEngine;
using System.Collections;
 
// Enum onde e informado qual estado de cada nivel e de cada subnivel
public enum LevelState{
	normal,
	locked	
}

public class LevelInformation : MonoBehaviour {

	public GameObject locked; // objeto (cadiado)

	public GameObject subPhases; // Objeto onde e instanciado a Scena onde tem os subniveis (ele e montado de acordo com )

	private LevelState levelState = LevelState.locked;
	
	private int[] levels;

	public int levelId; // Numero da fase {1,2,3,4,5,6 etc}

	public void addRangeArray (int range)  { this.levels = new int[range]; } // Metodo que adiciona o tamanho do array do levels

	public void setLevelId(int value) { this.levelId = value; } // adiciona qual valor do levelID

	public int getLevelId() { return this.levelId; } // retorna o valor do LevelID

	public void setLevelState(LevelState level) { this.levelState = level; }

	public LevelState getLevelState () { return this.levelState; }

	/*
	 * recebe 3 valores onde os mesmo sao os valores das cenas dos subniveis
	 */
	public void setLevel(int value1, int value2, int value3) {

		this.levels[0] = value1;
		this.levels[1] = value2;
		this.levels[2] = value3;

	}

	public int[] getLevel () { return this.levels; }

	/*
	 * Metodo que check qual estado do level (se esta aberto ou trancado )
	 * assim setando seu estado atual
	 */
	public void checkLevelState(LevelState level){

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
	 * Metodo responsavel por chamar o diretor ou a message para carregar o nivel desejado, passando como parametro o componente LevelInformation onde contem informaçao das cenas dos subniveis.
	 */
	public void buttonClickEnter (LevelInformation other) {

		switch (this.levelState){
			// quando clicar e o statos do level for igual a normal faça as operaçoes dentro desse case
			case LevelState.normal:{

				SubLevelCheckPhases subLevel = GameObject.Find("menuFases.CheckPhase").GetComponent<SubLevelCheckPhases>(); // Procura o objeto menufases>checkPhases  e recupera seu componente SubLevelCheckPhase
				//para poder checar o Subnivel e atribuir seus valor (se esta diponivel , e o valor da Cena que ele carrega)
				
				subLevel.check(other); // acessa o metodo Check do componente subnivel psaando como parametro o componente LevelInformation onde tem a informaçao dos subniveis, que antes armazenada nos niveis.

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
