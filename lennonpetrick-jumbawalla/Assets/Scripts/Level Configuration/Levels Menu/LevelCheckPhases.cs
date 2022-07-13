/* 
 * Versao 1.0
 * 
 * None: LevelCheckPhases
 * 
 * Descriçao: 	Script responsavel por verificar e inserir informaçoes no objetos que contem o LevelInformation
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

public class LevelCheckPhases : MonoBehaviour {

	// Se o objeto onde esta variavel existir , no momento em que ele for instanciado ira executar o metodo start()
	void Start () {

		LevelManager.checkBlockedLevels();

		this.check();
	}

	/*
	 * Metodo onde ira checar os niveis e atribuir seus valores , se esta disponiveis
	 */
	void check(){

		GameObject[] phases = GameObject.FindGameObjectsWithTag("ScenePhases"); // Vetor de gameObjects onde fica os objeto encontrado com o nome de "ScenePhases"

		int blockedLevels = LevelManager.getBlockedLevels();  // recebe parametro ate qual niveis foi distravado

		// repete um grupo de instruções inseridos para cada elemento de uma matriz ou uma coleção do objeto
		foreach (GameObject phase in phases){
			// Pega o componente "levelInformation" que esta dentro do objeto
			LevelInformation _levelInformation = phase.GetComponent<LevelInformation>();

			_levelInformation.addRangeArray(3); // acessa o metodo para adicionar o tamanho do Array dos leveis


			switch(phase.name){ // Verifica cada nome existente de niveis

				case "fase01":{ // quando o nivel for igual ao informado no case 
					
					_levelInformation.setLevel(7,8,9); // acessar o metodo passando como parametro os id de cada cenas composta no jogo de acordo com seu tamanho
					break;
				}
				case "fase02":{

					_levelInformation.setLevel(10,11,12);
					break;
				}
				case "fase03":{

					_levelInformation.setLevel(13,14,15);
					break;
				}
				case "fase04":{

					_levelInformation.setLevel(16,17,18);
					break;
				}
				case "fase05":{

					_levelInformation.setLevel(19,20,21);
					break;
				}
				case "fase06":{

					_levelInformation.setLevel(22,23,24);
					break;
				}case "fase07":{

					_levelInformation.setLevel(25,26,27);
					break;
				}case "fase08":{

					_levelInformation.setLevel(28,29,30);
					break;
				}case "fase09":{

					_levelInformation.setLevel(31,32,33);
					break;
				}
				case "fase10":{

					_levelInformation.setLevel(34,35,36);
					break;
				}
				case "fase11":{

					_levelInformation.setLevel(37,38,39);
					break;
				}
			}

			// cria um vetor de inteiro que recebe tdos os niveis que esteja naquele objeto
			int[] _levelPhase = _levelInformation.getLevel();

			// caso o array[0] for maior que o BlockedLevels, significa que este nivel e maior do que o nivel que esta disponivel
			if(_levelPhase[0] >= blockedLevels){

				_levelInformation.checkLevelState(LevelState.locked); // informa que aquele nivel esta trancado

			}
			else {

				_levelInformation.checkLevelState(LevelState.normal); // informa que aquele nivel esta desbloqueado

			}
		}
	}
}
