using UnityEngine;
using System.Collections;

public class LoadTutorial : MonoBehaviour {

	public tk2dUILayout prefabTutorial;
	
	public tk2dUIScrollableArea scrollableTutorialArea;
		
	public void loadTutorial(){

		// start setting the beginning position of the objects and the width of them
		
		// position X that the tutorial will instantiate
		float _xTutorial = 7f,			  
			 // width of the tutorial object. Here it was used Y because the object is with rotation 90
			 _wTutorial = (prefabTutorial.GetMaxBounds() - prefabTutorial.GetMinBounds()).y;
		
		foreach(Item tutorial in this.getTutorials()){
			
			// instantiates the prefabTutorial
			tk2dUILayout _layout = Instantiate(prefabTutorial) as tk2dUILayout;
			
			// changes the parent
			_layout.transform.parent = scrollableTutorialArea.contentContainer.transform;
			
			// and changes the position
			_layout.transform.localPosition = new Vector3(_xTutorial, 0, 0);
			
			// fills the tutorial informations
			_layout.transform.GetChild(0).GetComponent<tk2dSprite>().SetSprite(tutorial.Image);

			_layout.transform.GetChild(0).GetChild(0).GetComponent<tk2dTextMesh>().text = tutorial.Name;
			
			_layout.transform.GetChild(1).GetComponent<tk2dTextMesh>().text = 
				CurrentLanguage.language == LanguageType.Portuguese ? tutorial.DescriptionPT : tutorial.DescriptionEN;
			
			// sum the position with the width to other item
			_xTutorial += _wTutorial;
			
		}
		
		// the ContentLength receive the length of list
		scrollableTutorialArea.ContentLength = _xTutorial;
		
	}

	// method to destroy all object in container
	public IEnumerator destroyTutorial(){

		yield return new WaitForSeconds(1.0f);

		Transform _transformContainer = scrollableTutorialArea.contentContainer.transform;

		for(int i = 0; i < _transformContainer.childCount; i++){

			Destroy(_transformContainer.GetChild(i).gameObject);

		}
				
	}
	
	private ArrayList getTutorials(){
		
		ArrayList _tutorialsList = new ArrayList();
		
		// id, nome, descricao em portugues, descricao em ingles, nomeNaColecao, damage, preco, tipo
		
		Item _tutorialItem = new Item("tutorial1", "Mover", "clique e araste para mover", "click and drag to move", "tutorial01", 0, 0, ItemType.None);
		_tutorialsList.Add(_tutorialItem);
		
		_tutorialItem = new Item("tutorial1", "Mover", "clique e araste para mover", "click and drag to move", "tutorial02", 0, 0, ItemType.None);
		_tutorialsList.Add(_tutorialItem);
		
		_tutorialItem = new Item("tutorial1", "Mover", "clique e araste para mover", "click and drag to move", "tutorial03", 0, 0, ItemType.None);
		_tutorialsList.Add(_tutorialItem);
		
		_tutorialItem = new Item("tutorial1", "Mover", "clique e araste para mover", "click and drag to move", "tutorial04", 0, 0, ItemType.None);
		_tutorialsList.Add(_tutorialItem);
		
		_tutorialItem = new Item("tutorial1", "Mover", "clique e araste para mover", "click and drag to move", "tutorial05", 0, 0, ItemType.None);
		_tutorialsList.Add(_tutorialItem);
		
		_tutorialItem = new Item("tutorial1", "Mover", "clique e araste para mover", "click and drag to move", "tutorial06", 0, 0, ItemType.None);
		_tutorialsList.Add(_tutorialItem);
		
		_tutorialItem = new Item("tutorial1", "Mover", "clique e araste para mover", "click and drag to move", "tutorial07", 0, 0, ItemType.None);
		_tutorialsList.Add(_tutorialItem);
		
		return _tutorialsList;
	}

}