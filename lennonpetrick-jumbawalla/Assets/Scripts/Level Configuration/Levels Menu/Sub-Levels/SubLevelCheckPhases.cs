/* 
 * Versao 1.0
 * 
 * None: SubLevelCheckPhases
 * 
 * Descriçao: 	Script responsavel por verificar e inserir informaçoes no objetos de subniveis
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

public class SubLevelCheckPhases : MonoBehaviour {

	public GameObject subStage;
	public LoadMenuLevelNameTexts text;

	void Start() {
		subStage.SetActive(false);
	}

	/*
	 * 
	 * Metodo onde ira checar os subniveis e atribuir seus valores de cenas e se esta disponiveis, passando como parametro o componente levelInformation
	 *
	 * 
	 */
	public void check( LevelInformation level ){

		subStage.SetActive(true);

		text.loadMenuLevelTexts( level.getLevelId() );

		int[] _valueLevel = level.getLevel(); // vetor de inteiro onde recebe os subniveis que estavao armazenados no LevelInformation passa no parametro

		GameObject[] phases = GameObject.FindGameObjectsWithTag("SubLevel"); // Vetor de gameObjects onde fica os objeto encontrado com o nome de "SubLevel"
		
		int blockedLevels = LevelManager.getBlockedLevels(); // inteiro que recebe ate qual nivel esta desbloqueado

		// repete um grupo de instruções inseridos para cada elemento de uma matriz ou uma coleção do objeto
		foreach (GameObject phase in phases){
			
			SubLevelInformation _subLevelInformation = phase.GetComponent<SubLevelInformation>(); // Cria componente que recebe o componente criado do objeto dentro do forech

			switch(phase.name){ // Faz uma checagem nos nomes
					
				case "SubFase1":{ // Caso nome seja igual ao do "case"
					
					_subLevelInformation.setLevel(_valueLevel[0]); // informa que este subnivel tem um seu reespectivo valor de cena

					_subLevelInformation.setTextLevel((level.getLevelId().ToString() + "-1")); // informa  que o text que sera escrito recebe o numero do levelID mais seu subnivel

					break;
				}
				case "SubFase2":{

					_subLevelInformation.setLevel(_valueLevel[1]);

					_subLevelInformation.setTextLevel((level.getLevelId().ToString() + "-2"));

					break;
				}
				case "SubFase3":{

					_subLevelInformation.setLevel(_valueLevel[2]);

					_subLevelInformation.setTextLevel((level.getLevelId().ToString() + "-3"));

					break;
				}
			}
		
			if(_subLevelInformation.getLevel() >= blockedLevels){
				
				_subLevelInformation.checkLevelState(LevelState.locked);
				
			}
			else {
				
				_subLevelInformation.checkLevelState(LevelState.normal);
				
			}
		}
	}
}
