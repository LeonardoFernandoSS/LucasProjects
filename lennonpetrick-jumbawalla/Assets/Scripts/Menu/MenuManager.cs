/* 
 * Versao 1.0
 * 
 * None: MenuManager
 * 
 * Descriçao: 	Script que tem objetivo passar a mensagem e oque deve fazer seu destinatario
 * 				
 * Autor: Mateus R.Culpo
 * 
 * Date: 03/12/2013
 * 
 * Modificado por  	(DD/MM/YYYY)
 * 
 * ########			############	#########
 */
using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject 	playButton, 
						collectionButton, 
						optionsButton,
						storeButton,
						tutorialButton,
						characterButton,
						creditsButton, 
						audioButton, 
						effectButton;

	public AudioClip soundButton;
	public MenuController menuController;

	/*** About Button Play ***/
	
	public IEnumerator playButtonClicked(){

		Director.sharedDirector().playEffect( soundButton );

		playButton.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);

		yield return new WaitForSeconds(0.2f);

		playButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

		yield return new WaitForSeconds(0.1f);

		this.menuController.buttonScenePlay(menuController.getEnumSceneMenu());

	}
	/*** Finish About Button Play ***/
	
	/*** About Button Credits ***/
	
	public IEnumerator creditsButtonClicked(){

		Director.sharedDirector().playEffect( soundButton );

		creditsButton.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);

		yield return new WaitForSeconds(0.2f);

		creditsButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

		yield return new WaitForSeconds(0.1f);

		menuController.buttonSceneCreditos(EnumSceneMenu.Creditos);

	}
	

	/*** Finish About Button Credits ***/
	

	
	/*** About Button Options ***/	

	
	public IEnumerator optionButtonClicked(){

		Director.sharedDirector().playEffect( soundButton );

		optionsButton.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);

		yield return new WaitForSeconds(0.2f);

		optionsButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

		yield return new WaitForSeconds(0.1f);

		this.menuController.buttonSceneOption(menuController.getEnumSceneMenu());
	
	}
	
		
	/*** Finish About Button Options ***/
	
	/*** About Button Store ***/

	public IEnumerator storeButtonClicked(){

		Director.sharedDirector().playEffect( soundButton );

		storeButton.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);

		yield return new WaitForSeconds(0.2f);

		storeButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

		yield return new WaitForSeconds(0.1f);

		menuController.animationMenu.Play("menu-shop") ;

		Director.sharedDirector().ScreenFadeSpeed = 0.3f;

		Director.sharedDirector().screenFadeOut();

	}

	/*** Finish About Button Store ***/

	/*** About Button Back ***/

	public IEnumerator backButtonClicked( RaycastHit _obj ){

		Director.sharedDirector().playEffect( soundButton );

		_obj.transform.localScale = new Vector3( 1.1f, 1.1f, 1.0f );

		yield return new WaitForSeconds(0.2f);

		_obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

		yield return new WaitForSeconds(0.1f);

		this.menuController.buttonSceneReturn(menuController.getEnumSceneMenu());

	}


	/*** Finish About Button Back ***/	

	/*** About Button tutorial ***/

	public IEnumerator tutorialButtonClicked(){

		Director.sharedDirector().playEffect( soundButton );

		tutorialButton.transform.localScale = new Vector3( 1.1f, 1.1f, 1.0f );

		yield return new WaitForSeconds(0.2f);

		tutorialButton.transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f );

		yield return new WaitForSeconds(0.1f);

		// instantiate the objects
		GetComponent<LoadTutorial>().loadTutorial();

		// Chama a animaçao que faz toda tragetoria da Tela De Opçoes Ate a tela de tutoriais
		menuController.buttonSceneTutorial(EnumSceneMenu.Tutorial);
	}
	

	/*** Finish About Button tutorial ***/	

	/*** About Button character ***/
	
	public IEnumerator characterButtonClicked(){

		Director.sharedDirector().playEffect( soundButton );

		characterButton.transform.localScale = new Vector3( 1.1f, 1.1f, 1.0f );

		yield return new WaitForSeconds(0.2f);

		characterButton.transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f );

		yield return new WaitForSeconds(0.1f);

		// instantiate the objects
		GetComponent<LoadCharacters>().loadCharacters();

		// Chama a animaçao que faz toda tragetoria da Tela De Opçoes Ate a tela de Personagem
		menuController.buttonScenePersonagem(EnumSceneMenu.Personagem);
	}
	

	/*** Finish About Button character ***/	

	/*** About Button collection ***/

	public IEnumerator collectionButtonClicked(){

		Director.sharedDirector().playEffect( soundButton );

		collectionButton.transform.localScale = new Vector3( 1.1f, 1.1f, 1.0f);

		yield return new WaitForSeconds(0.2f);

		collectionButton.transform.localScale = new Vector3( 1.0f, 1.1f, 1.0f);

		yield return new WaitForSeconds(0.1f);
		// Chama a animaçao que faz toda tragetoria da Tela De Opçoes Ate a tela de Coleçao
		menuController.buttonSceneCollections(EnumSceneMenu.Colecao);

	}

	/*** Finish About Button collection ***/

	/*** About Audio Background Button ***/
	
	public IEnumerator audioButtonClicked(){

		Director.sharedDirector().playEffect( soundButton );

		audioButton.transform.localScale = new Vector3( 1.1f, 1.1f, 1.0f);
		
		yield return new WaitForSeconds(0.2f);
		
		audioButton.transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f);

		Director.sharedDirector().muteAudioBackground();
		
	}
	
	/*** Finish About Audio Background Button ***/

	/*** About Audio Effect Button ***/
	
	public IEnumerator effectButtonClicked(){

		Director.sharedDirector().playEffect( soundButton );

		effectButton.transform.localScale = new Vector3( 1.1f, 1.1f, 1.0f);
		
		yield return new WaitForSeconds(0.2f);
		
		effectButton.transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f);

		Director.sharedDirector().muteAudioEffects();
						
	}
	
	/*** Finish About Audio Effect Button ***/

#region Rotinas da escolha de Fases
	public static IEnumerator playLevel (RaycastHit _obj , AudioClip sound) {

		Director.sharedDirector().playEffect( sound  );
		
		_obj.transform.localScale = new Vector3( 1.1f, 1.1f, 1.0f);

		yield return new WaitForSeconds( 0.2f );

		_obj.transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f);

		yield return new WaitForSeconds( 0.1f );

		LevelInformation _level = _obj.collider.GetComponent<LevelInformation>();
		_level.buttonClickEnter(_level);

	}

	public static IEnumerator playSubLevel (RaycastHit _obj,  AudioClip sound) {

		Director.sharedDirector().playEffect( sound  );

		_obj.transform.localScale = new Vector3( 1.1f, 1.1f, 1.0f);
		
		yield return new WaitForSeconds( 0.2f );
		
		_obj.transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f);

		yield return new WaitForSeconds( 0.1f );
		
		LevelManager.idSceneToLoad = _obj.collider.GetComponent<SubLevelInformation>().getLevel();
		_obj.collider.GetComponent<SubLevelInformation>().buttonClickEnter();
		
	}
#endregion
}