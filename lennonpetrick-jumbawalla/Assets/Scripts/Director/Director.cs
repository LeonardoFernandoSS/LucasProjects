using UnityEngine;
using System.Collections;

public class Director : AudioController {
	
	/************ LOADING SCREEN ************/
	public static int sceneToLoad;
	
	
	
	/************ FADE SCREEN ************/
	public Texture fadeTextureBackground;
	
	private float screenFadeSpeed, 
	alpha, 
	screenFadeDirection;
	
	private float alphaMaximum = 1.0f;
	
	
	
	/************ PAUSE ************/
	
	private bool paused = false;
	
	
	
	/************ CHANGE SCENE ************/
	private float waitSecondsToLoadScene = 0.5f;
	
	
	
	/************ SCENE END ************/
	public enum SceneEndedStatus{
		won,
		lost
	}
	
	private bool sceneEndedPending = false, 
				 sceneEnded = false, 
				 stopFunction = false;
	
	private SceneEndedStatus sceneEndedState;

	private static Director self;
	
	public float waitSecondsToEndScene = 0.01f;
	
	public AudioClip wonAudio;
	
	public AudioClip lostAudio;
	
	public void Start () 
	{		
		base.Start();
						
		this.initScreenBackground();

		self = this;

	}
	
	private void initScreenBackground(){
		
		this.screenFadeSpeed = 1.0f;
		
		if(GUI.color.a == 1.0f){
			
			this.screenFadeIn(1.0f);
			
		}
		
	}
	
	
	
	private void Update () {

		base.Update();

		if (Input.GetKey(KeyCode.Escape))
		{
			System.Diagnostics.Process.GetCurrentProcess().Kill();
		}

		if(isSceneEndedPending()){
			
			this.setSceneEndedPending(false);
			
			this.endScene();
			
		}
		
		if(isSceneEnded() && !stopFunction){
			
			if(sceneEndedState == SceneEndedStatus.won){
				
				if(!PlayerStateControl.sharePlayer().getAnimation().IsPlaying("ganhando")){
					
					this.stopFunction = true;
					
					this.screenFadeOut();
					
				}
				
			}
			else{
				
				if(!PlayerStateControl.sharePlayer().getAnimation().IsPlaying("morrendo") && 
				   !PlayerStateControl.sharePlayer().getAnimation().IsPlaying("morreu")){
					
					this.stopFunction = true;
					
					ControlInstantiatePrefabs.sharedLayerControl().prefabLayerControl(1);
					
				}
				
			}
			
		}
		
	}
	
	
	
	public static Director sharedDirector(){
		
		return Camera.main.GetComponent<Director>();
		
	}
	
	
	
	/* Este metodo serve apenas para fazer o fade, 
	 * o resto dos objetos que aparecem 
	 * na tela sao feitos por outras classes */
	private void OnGUI() {
		
		if ((!PauseControlInterface.checkAnimationPause() || !DeadControlInterface.checkAnimationDie()) && 
		    (isPaused() || isSceneEnded())) {
			
			Time.timeScale = 0.0f;
			
		}
		
		if(isSceneEnded() && sceneEndedState == SceneEndedStatus.won && alpha >= alphaMaximum){
			
			Application.LoadLevel("Win");
			
		}
		
		alpha += screenFadeDirection * screenFadeSpeed * Time.deltaTime;
		
		alpha = Mathf.Clamp01(alpha);	
		
		Color _color = GUI.color;
		
		_color.a = alpha;
		
		GUI.color = _color;		 	
		
		GUI.depth = -980;
		
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTextureBackground);
		
	}
	
	
	
	public void screenFadeIn(float alpha) {
		
		this.alpha = alpha;
		this.screenFadeDirection = -1f;	
		
	}
	
	
	
	public void screenFadeOut() {
		
		this.alpha = 0.0f;
		this.screenFadeDirection = 1f;	
		
	}
	
	
	
	public void Pause() {
		
		if (!isSceneEnded()){
			
			this.paused = !isPaused();
			
			if(this.paused){
				
				ControlInstantiatePrefabs.sharedLayerControl().prefabLayerControl(0);
				
			}
			else{
				
				Time.timeScale = 1.0f;
				
			}
		}			
	}
	
	
	
	public bool isPaused() {
		
		return this.paused;
		
	}
	
	
	
	public void endScene(){
		
		if (!isPaused()){
			
			this.sceneEnded = !isSceneEnded();
			
			if(!sceneEnded){
				Time.timeScale = 1.0f;
			}
			
		}
		else {
			
			this.setSceneEndedPending(true);
			
		}
		
	}
	
	
	
	public void endScene(SceneEndedStatus status){
		/*
		if(ADSController.isFullBannerReady()){
			
			ADSController.instance.StartCoroutine(ADSController.showFullBanner());
			
		}
		else{
			
			ADSController.instance.StartCoroutine(ADSController.showBanner());
			
		}*/
		
		this.sceneEndedState = status;
		
		switch(this.sceneEndedState){
			
		case SceneEndedStatus.won : {
			
			this.playEffect(wonAudio);
			
			PlayerStateControl.sharePlayer().setState(PlayerState.Winner);
			
			break;
			
		}
			
		case SceneEndedStatus.lost : {
			
			this.audioBackground.Stop();
			
			PlayerStateControl.sharePlayer().setState(PlayerState.Dead);
			
			this.playEffect(lostAudio);
			
			Inventory.reset();
			
			break;
			
		}
			
		}
		
		this.endScene();
		
	}
	
	
	
	public bool isSceneEnded(){
		
		return this.sceneEnded;
		
	}
	
	
	
	public bool isSceneEndedPending(){
		
		return this.sceneEndedPending;
		
	}
	
	
	
	public void setSceneEndedPending(bool state){
		
		this.sceneEndedPending = state;
		
	}
	
	
	
	public SceneEndedStatus getSceneEndedStatus(){
		
		return this.sceneEndedState;
		
	}
	
	
	
	public void LoadLevelWithFade(int level) {
		
		//increases the fade speed to change the level
		this.screenFadeSpeed = 1.5f;
		
		this.screenFadeOut();
		
		this.LoadLevel(level);
		
	}
	
	
	
	public void LoadLevel(int level) {
		
		if (isPaused()) {
			
			this.Pause();
			
			this.LoadLevelLoading(level);
			
		}
		else if (isSceneEnded()) {
			
			this.endScene();
			
			this.LoadLevelLoading(level);
			
		}
		else {
			
			StartCoroutine(WaitToLoadLevel(level));
			
		}
		
	}
	
	
	
	public void restartScene() {
		
		//ADSController.canCreateFullBanner();
		
		this.LoadLevel(LevelManager.currentLevel);
		
	}
	
	
	
	private IEnumerator WaitToLoadLevel(int level) {
		
		yield return new WaitForSeconds(waitSecondsToLoadScene);
		
		this.LoadLevelLoading(level);
		
	}
	
	
	
	private void LoadLevelLoading(int level){
		
		Director.sceneToLoad = level;
		
		Application.LoadLevel(LevelManager.getLevelID(Level.loading));
		
	}
	
	
	
	public void nextLevel(){
		
		this.LoadLevelWithFade(LevelManager.nextLevel());
		
	}
	
	public float ScreenFadeSpeed {
		get {
			return this.screenFadeSpeed;
		}
		set {
			screenFadeSpeed = value;
		}
	}
}