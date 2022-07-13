using UnityEngine;
using System.Collections;

public class ControlInstantiatePrefabs : MonoBehaviour {

	public GameObject[] gameObjectPrefabs; /* Obs: 	gameObjectPrefabs[0] - Layer de Pause - idIdentity = 0
													gameObjectPrefabs[1] - Layer de Morto - idIdentity = 1
													gameObjectPrefabs[2] - Layer de Ganhar - idIdentity = 2
	                                        */

	private Transform transformPrefabs; // posiçao pro prefabInicial

	/*
	 * Instancia os prefabs dentro do ArrayList
	 * 
	 */

	public void prefabLayerControl(int idIdentity){

		for(int i = 0; i < gameObjectPrefabs.Length; i++){

			if(i == idIdentity){

				GameObject _mainCamera = GameObject.Find("Main Camera"); // Cria um Object e pegue a camera como referencia
				
				GameObject _prefabLayer = Instantiate(gameObjectPrefabs[i], transform.position, Quaternion.identity) as GameObject; // Instancia o prefab 
				//em um outro object para instancialo dentro da scena
				
				_prefabLayer.transform.parent = _mainCamera.transform;// tranforma a nova Layer parente da cameraMain
				
				_prefabLayer.transform.localPosition = gameObjectPrefabs[i].transform.position; // Tranforma a posiçao local da LayerPause na posiçao
				// do prefab, assim ficando dentro da camera nos parametros certos
				
				_prefabLayer.name = "Layer" + i.ToString(); // Colocar um nome no prefabLayer para poder encontrar o objeto facilmente

				break;
			}
		}
	}

	public static ControlInstantiatePrefabs sharedLayerControl(){

		return Camera.main.GetComponent<ControlInstantiatePrefabs>();

	}
}
