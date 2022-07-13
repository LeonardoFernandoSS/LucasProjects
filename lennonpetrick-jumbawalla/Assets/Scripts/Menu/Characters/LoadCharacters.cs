using UnityEngine;
using System.Collections;

public class LoadCharacters : MonoBehaviour {

	public tk2dUILayout prefabCharacter;

	public tk2dUIScrollableArea scrollableCharacterArea;

	public void loadCharacters(){
	
		// start setting the beginning position of the objects and the width of them

		// position X that the character will instantiate
		float _xCharacter = 5.5f,			  
			  // width of the character object. Here it was used Y because the object is with rotation 90
			  _wCharacter = (prefabCharacter.GetMaxBounds() - prefabCharacter.GetMinBounds()).y;

		foreach(Item character in this.getCharacters()){
			
			// instantiates the prefabCharacter
			tk2dUILayout _layout = Instantiate(prefabCharacter) as tk2dUILayout;
			
			// changes the parent
			_layout.transform.parent = scrollableCharacterArea.contentContainer.transform;
			
			// and changes the position
			_layout.transform.localPosition = new Vector3(_xCharacter, 0, 0);
			
			// fills the character informations
			_layout.transform.GetChild(0).GetComponent<tk2dSprite>().SetSprite(character.Image);

			_layout.transform.GetChild(1).GetComponent<tk2dTextMesh>().text = 
				CurrentLanguage.language == LanguageType.Portuguese ? character.DescriptionPT : character.DescriptionEN;

			// sum the position with the width to other item
			_xCharacter += _wCharacter;
			
		}
				
		// the ContentLength receive the length of list
		scrollableCharacterArea.ContentLength = _xCharacter;
		
	}

	// method to destroy all object in container
	public IEnumerator destroyCharacters(){

		yield return new WaitForSeconds(1.0f);
		
		Transform _transformContainer = scrollableCharacterArea.contentContainer.transform;
		
		for(int i = 0; i < _transformContainer.childCount; i++){
			
			Destroy(_transformContainer.GetChild(i).gameObject);
			
		}

	}

	private ArrayList getCharacters(){

		ArrayList _charactersList = new ArrayList();

		// id, nome, descricao em portugues, descricao em ingles, nomeNaColecao, damage, preco, tipo

		Item _characterItem = new Item("character1", "Robo", "Robo fraco e facil de matar", "Robot weak and easy to kill", "inimigo01", 0, 0, ItemType.None);
		_charactersList.Add(_characterItem);
		
		_characterItem = new Item("character1", "Robo", "Robo fraco e facil de matar", "Robot weak and easy to kill", "inimigo02", 0, 0, ItemType.None);
		_charactersList.Add(_characterItem);

		_characterItem = new Item("character1", "Robo", "Robo fraco e facil de matar", "Robot weak and easy to kill", "inimigo03", 0, 0, ItemType.None);
		_charactersList.Add(_characterItem);

		_characterItem = new Item("character1", "Robo", "Robo fraco e facil de matar", "Robot weak and easy to kill", "inimigo04", 0, 0, ItemType.None);
		_charactersList.Add(_characterItem);

		_characterItem = new Item("character1", "Robo", "Robo fraco e facil de matar", "Robot weak and easy to kill", "inimigo05", 0, 0, ItemType.None);
		_charactersList.Add(_characterItem);

		_characterItem = new Item("character1", "Robo", "Robo fraco e facil de matar", "Robot weak and easy to kill", "inimigo06", 0, 0, ItemType.None);
		_charactersList.Add(_characterItem);

		_characterItem = new Item("character1", "Robo", "Robo fraco e facil de matar", "Robot weak and easy to kill", "inimigo07", 0, 0, ItemType.None);
		_charactersList.Add(_characterItem);

		return _charactersList;
	}

}