using UnityEngine;
using System.Collections;

public class WinControlInterface : MonoBehaviour {

	#region VARIABLE
	public TextMesh amountPointText,
					amountEnemyText,
					amountBoneText;

	private int countPoint,
				countEnemy,
				countBone,
				countBonePoint,
				countBoneEnemy;

	public Animation 	animBone,
						animPoint,
						animEnemy;

	public GameObject particula;

	public AudioClip dentes;

	private bool active;
	#endregion

	void Start(){

		CheckPoint.reset();



		this.active = false;

		this.countPoint = 0;
		this.countEnemy = 0;
		this.countBone = 0;
		this.countBoneEnemy = 0;
		this.countBonePoint = 0;

	}

	void Update(){

		if( Inventory.getPoints() == 0 && Inventory.getEnemyDead() == 0 ) 
		{
			this.active = true;
		}

		if( Inventory.getPoints() != 0 && Inventory.getEnemyDead() != 0 && !this.active)
			StartCoroutine(countingBone(2));


		if(Input.GetMouseButtonDown(0) && this.active){
			
			RaycastHit _hit;
		
			Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(_ray, out _hit, 100.0f)){

				if(_hit.collider.name == "MENU"){

					//ADSController.instance.StartCoroutine(ADSController.hideBanner());

					Director.sharedDirector().LoadLevelWithFade(LevelManager.getLevelID(Level.menu));

				}
				else if(_hit.collider.name == "PROXIMO"){

					//ADSController.instance.StartCoroutine(ADSController.hideBanner());

					Director.sharedDirector().nextLevel();
				
				}
				else if(_hit.collider.name == "VOLTAR"){

					//ADSController.instance.StartCoroutine(ADSController.hideBanner());

					Director.sharedDirector().restartScene();

				}	

			}
		}	

	}

	// Contador das pontuaçoes gradativamente
	IEnumerator countingBone(float time){

		yield return new WaitForSeconds(time);

		// se o contador do ponto (this) for menor que o inventarioPoints ele ira adicionar sempre ++ dentro do contadorPontos(this),
		// e adicionar TextMesh dentro do textPoints
		if(this.countPoint < Inventory.getPoints()){

			this.animPoint.Play();
			this.animBone.Play();
			this.countPoint += 200;
			this.countBonePoint += 200;

			if(this.countPoint >= Inventory.getPoints()){ this.countPoint = Inventory.getPoints(); }

			this.amountPointText.text = countPoint.ToString();

		}
		// Caso o contador(this) ser igual aos inventorioPoints e o Contador de Inimigos(this) se menor que o InventorioEnemyDead,
		// adiciona ++ ao contadorEnemy(this) e adiciona TextMesh no textEnemy
		else if(this.countPoint >= Inventory.getPoints() && 
		        this.countEnemy < Inventory.getEnemyDead() && 
		        !this.animPoint.isPlaying) {

					this.animEnemy.Play();
					this.animBone.Play();
					this.animPoint.Stop();
					this.countEnemy++;
					this.countBoneEnemy++;
					this.amountEnemyText.text = countEnemy.ToString();
		}

		stopAnimation();

		addBoneText();
	}

	// verifica as animaçoes para parar as animaçoes
	void stopAnimation(){

		if( !this.animEnemy.isPlaying && !this.animBone.isPlaying && 
		   	this.countPoint >= Inventory.getPoints() && 
		   	this.countEnemy >= Inventory.getEnemyDead() && !this.active) {

			this.animEnemy.Stop();
			this.animBone.Stop();

			MoneyTeeth.addTeeth(countBone);

			this.active = true;
			particula.SetActive(true);

			Director.sharedDirector().playEffect(dentes);
		}
	}

	// Adcionar Bone dentro do TextMesh
	void addBoneText(){

		if(this.countBonePoint >= 900){
			
			this.countBone++;
			this.amountBoneText.text = this.countBone.ToString();
			this.countBonePoint = 0;
			
		}
		
		if(this.countBoneEnemy >= 3){
			
			this.countBone++;
			this.amountBoneText.text = this.countBone.ToString();
			this.countBoneEnemy = 0;
			
		}
	}

	public static bool checkAnimationWin(){

		GameObject _layer2 = GameObject.Find("Layer2");

		if(_layer2) { 
		
			return _layer2.GetComponent<WinControlInterface>().animation.isPlaying; 

		}
		else {

			return true;
		
		}
	}
}
