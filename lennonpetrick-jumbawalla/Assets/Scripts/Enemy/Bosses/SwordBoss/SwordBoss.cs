using System;
using UnityEngine;
using System.Collections;

#region Enumerator
public enum stateSwordBoss { 
	DEAD, 
	IDLE, 
	MOVE, 
	FIRSTSKILL, 
	SECONDSKILL, 
	THIRDSKILL, 
	FOURTHSKILL }
#endregion

public class SwordBoss : MonoBehaviour {

#region variable static

	public static event Action sword;

#endregion

#region variable include const

	private const int _MAX = 100;
	private const int _MEDIUM = 65;
	private const int _MIN = 40;

#endregion

#region variable private

	private float currentFirstHability;
	private float waitingFirstHability;

	private SwordBossLife life; //component responsible for the life of the enemy

	private stateSwordBoss stateBoss; //current state of the enemy

	private Transform player; // player information (position, scale, rotate) 

	private bool isAttacking; // verificar se pode atacar.
	private bool isAnimation; // verificar se a animaçao esta rodando
	private bool isMove; // variavel responsavel para verificar se esta em movimento

	private float speed; // velocity enemy 
	private float currentSpeed; // current velocity
	private float currentTime; // tempo atual para ativar os ataques
	private float waitingTime; // tempo de espera  de cada ataque
		
	private float distance; // distace to player

	private bool stageAttack; // resposavel pelo controle de cada intervalo entre o mesmo golpe
	
	private float currentTimeEachSkill; // tempo que ira ficar executando a skill 
	private float waitingTimeEachSkill; // tempo de espera que executara as skill

#endregion

#region variable public

	public Animator anim;

	public Transform posTeleportX;

	public Transform posTeleportY;

#endregion

#region Methods Unity
	void Start() {  

		this.speed = 10.0f;

		this.currentSpeed = this.speed;

		this.currentFirstHability = 0.0f;

		this.waitingFirstHability = 4.0f;

		this.currentTime = 0.0f;

		this.waitingTime = 8.0f;

		this.stageAttack = true;

		this.isAttacking = true;

		this.isAnimation = true;

		this.isMove = false;

		this.player = GameObject.Find("Jumba").transform;

		this.life = GetComponent<SwordBossLife>();

		this.stateBoss = stateSwordBoss.MOVE;

	}

	//Responsavel pela movimentaçao e condiçoes dos golpes
	void Update() {   

		if(stateBoss != stateSwordBoss.DEAD) {

			this.distance = Vector3.Distance(transform.position, player.position);

			this.timeOut();

		}

		if(stateBoss == stateSwordBoss.DEAD) {

			this.chargeAnimation( "State", 6);
	
		} 

		else if(stateBoss == stateSwordBoss.IDLE){

			this.chargeAnimation( "State", 0);

			this.chargeRotate();

			if(distance <= 12.0f) {

				if(this.currentFirstHability >= waitingFirstHability) {

					this.isAnimation = true;
					
					this.stageAttack = true;
					
					this.currentFirstHability = 0.0f;

					this.stateBoss = stateSwordBoss.FIRSTSKILL;

				}
			}

			else if( distance <= 100.0f ) {
				
				if(this.isAttacking) 
					this.changeState(this.stateBoss);

				
			}
		
		}
		else if(stateBoss == stateSwordBoss.MOVE) {

			this.chargeAnimation( "State", 1);

			this.moveForward();

			if(distance <= 12.0f) {

				this.resetState();

				this.stateBoss = stateSwordBoss.IDLE;

			}

			else if( distance <= 100.0f ) {

				if(this.isAttacking)
					this.changeState(this.stateBoss);

			}

		} 
		else if(stateBoss == stateSwordBoss.FIRSTSKILL) {

			this.chargeAnimation( "State", 2);

			if(this.isMove) 
				transform.Translate(new Vector3( this.currentSpeed, 0, 0) * Time.deltaTime);
			


		} 
		else if(stateBoss == stateSwordBoss.SECONDSKILL) { 

			this.chargeAnimation( "State", 3);

			if(this.isMove) {

				transform.Translate(new Vector3( this.currentSpeed * 2, 0, 0) * Time.deltaTime);

				if( distance >= 17.0f && distance <= 18.0f && this.stageAttack ) {

					this.chargePositionX();

					this.chargeRotate();

					this.currentSpeed = this.speed * 2;

					this.waitingTimeEachSkill = 1.5f;

					this.currentTimeEachSkill = 0;

					this.stageAttack = false;
				
				}

				if(this.currentTimeEachSkill >= waitingTimeEachSkill) {

					this.resetState();

				}
				else if(this.currentTimeEachSkill < waitingTimeEachSkill)

					this.currentTimeEachSkill += Time.deltaTime;

			}

		} 
		else if(stateBoss == stateSwordBoss.THIRDSKILL) {

			this.chargeAnimation( "State", 4 );

			if(this.isMove) {

				transform.Translate(new Vector3( 0,  -this.currentSpeed * 6 * Time.deltaTime , 0));

				if(this.currentTimeEachSkill >= waitingTimeEachSkill) {

					this.resetState();

				}
				else 
					this.currentTimeEachSkill += Time.deltaTime;

			}

		} 
		else if(stateBoss == stateSwordBoss.FOURTHSKILL) {

			this.chargeAnimation( "State", 5);

			if(this.stageAttack) {

				sword();

				this.stageAttack = false;

			}
		}
	}
#endregion

#region Private Methods

	/// <summary>
	/// Metodo que retorna um array de estados possiveis para o inimigo
	/// <returns>The state available.</returns>
	stateSwordBoss[] getStateAvailable() {  
		
		float _tempLife = this.life.getCurrentLife();
		stateSwordBoss[] _tempArrayState;

		if((_tempLife > (( this.life.life * _MEDIUM ) / _MAX))) {
			
			_tempArrayState = new stateSwordBoss[1];

			_tempArrayState[0] = stateSwordBoss.SECONDSKILL;
			
		}
		else if((_tempLife > ((this.life.life * _MIN) / _MAX)) && (_tempLife <= (( this.life.life * _MEDIUM ) / _MAX))) {

			this.waitingTime = 6.0f;
			_tempArrayState = new stateSwordBoss[2];
			
			_tempArrayState[0] = stateSwordBoss.SECONDSKILL;
			_tempArrayState[1] = stateSwordBoss.THIRDSKILL;
			
		}
		else {

			this.waitingTime = 4.0f;
			_tempArrayState = new stateSwordBoss[2];

			_tempArrayState[0] = stateSwordBoss.THIRDSKILL;
			_tempArrayState[1] = stateSwordBoss.FOURTHSKILL;
			
		}
		return _tempArrayState;
	}

	/// <summary>
	/// Times the out, Contador de intervalo para cada habilidade e tambem da habilidade principal
	/// </summary>
	void timeOut() {

		if( this.currentTime >= waitingTime) 
			this.isAttacking = true;


		if( this.currentTime < waitingTime && !this.isAnimation)
			this.currentTime += Time.deltaTime;

		if( this.currentFirstHability < this.waitingFirstHability )
			this.currentFirstHability += Time.deltaTime;
	}

	/// <summary>
	/// Charges the animation.
	/// Resposanvel por setar a animaçao em cada estado
	/// </summary>
	/// <param name="parameters">Parameters. </param> passa como parametro o nome da tag que identifica a animaçao
	/// <param name="id">Identifier.</param> passa por parametro o id responsavel pela animaçao
	void chargeAnimation( string parameters, int id) {

		if(this.isAnimation) 
		{
			this.anim.SetInteger(parameters, id);
			this.isAnimation = false;
		}

	}

	/// <summary>
	/// Moves the forward.
	/// Responsavel por movimentar o objeto em direçao ao jogador
	/// </summary>
	void moveForward() {

		transform.Translate(new Vector3( this.currentSpeed, 0, 0) * Time.deltaTime);
		
		this.chargeRotate();

	}

	/// <summary>
	/// Adds the each time.
	/// 
	/// Adiciona o tempo de cada habilidade ... conforme muda os estado do inimigo
	/// 
	/// </summary>
	void addEachTime() {

		if(this.stateBoss == stateSwordBoss.SECONDSKILL)

			this.waitingTimeEachSkill = 3.0f;

		else if(this.stateBoss == stateSwordBoss.THIRDSKILL)

			this.waitingTimeEachSkill = 0.49f;

	}

#endregion

#region Public Methods
	/// <summary>
	/// Charges the rotate.
	/// verifica  a distancia com o jogador , e rotaciona em direçao a ele.
	/// </summary>
	public void chargeRotate() {
		
		float _distace = player.position.x - transform.position.x;
		
		if(_distace - 1 > 0) {
			
			transform.rotation = new Quaternion( transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
			
		}
		else if (_distace + 1 < 0) {
			
			transform.rotation = new Quaternion( transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
			
		}
		
	}
	/// <summary>
	/// Resets the state.
	/// 
	/// Reseta as variaveis para cada estado
	/// 
	/// </summary>
	public void resetState() {

		this.currentTimeEachSkill = 0;
		
		this.isMove = false;
		
		this.currentSpeed = this.speed;
		
		this.isAttacking = false;

		this.stageAttack = true;

		this.changeState ( this.stateBoss );

	}

	/// <summary>
	/// Changes the state.
	/// 
	/// Metodo resposavel por fazer a troca de estado do inimigo
	/// 
	/// </summary>
	/// <param name="_currentStateBoss">_current state boss.</param>
	/// passa como parametro o estado atual do inimigo
	public void changeState ( stateSwordBoss _currentStateBoss ) {

		stateSwordBoss[] _arrayStateAvailable = getStateAvailable();

		float _randomNumber = UnityEngine.Random.Range(0,100);

		if( _currentStateBoss == stateSwordBoss.MOVE) {

			if(_randomNumber < _MIN)
				this.stateBoss = stateSwordBoss.IDLE;
			else
				this.stateBoss = _arrayStateAvailable[UnityEngine.Random.Range( 0, _arrayStateAvailable.Length )];
		}

		else if(_currentStateBoss == stateSwordBoss.IDLE) {

			if(_randomNumber < _MIN)
				this.stateBoss = stateSwordBoss.MOVE;
			else 
				this.stateBoss = _arrayStateAvailable[UnityEngine.Random.Range( 0, _arrayStateAvailable.Length )];

		}

		else {

			if(_randomNumber < _MEDIUM && stateBoss != stateSwordBoss.FIRSTSKILL)
				this.stateBoss = stateSwordBoss.MOVE;
			else 
				this.stateBoss = stateSwordBoss.IDLE;

		}

		this.addEachTime();

		this.chargeRotate();

		this.isAnimation = true;

		this.currentTimeEachSkill = 0;

		this.currentTime = 0;

	}

	/// <summary>
	/// Charges the position x.
	/// Metodo responsavel por mudar a posiçao do inimigo no eixo X
	/// </summary>
	public void chargePositionX () {
		transform.position = new Vector3 ( posTeleportX.position.x, transform.position.y, transform.position.z );
	}

	/// <summary>
	/// Charges the position y.
	/// Metodo responsavel por mudar a posiçao do inimigo no eixo Y
	/// </summary>
	public void chargePositionY () {	

		transform.position = new Vector3 ( player.position.x, posTeleportY.position.y , player.position.z );

	}
#endregion
	
#region Getter or Setter
	public float getCurrentSpeed ( ) { 
		return this.speed;
		
	}
	public void setCurrentSpeed( float value ) { 

		this.speed = value;

	}

	public stateSwordBoss getStateSwordBoss () {

		return this.stateBoss;

	}
	public void setStateSwordBoss ( stateSwordBoss state) {

		this.stateBoss = state;

	}

	public bool IsAttacking {
		get {
			return this.isAttacking;
		}
		set {
			isAttacking = value;
		}
	}

	public bool IsAnimation {
		get {
			return this.isAnimation;
		}
		set {
			isAnimation = value;
		}
	}
	public bool IsMove {
		get {
			return this.isMove;
		}
		set {
			isMove = value;
		}
	}

	public float CurrentTime {
		get {
			return this.currentTime;
		}
		set {
			currentTime = value;
		}
	}
	public bool StageAttack {
		get {
			return this.stageAttack;
		}
		set {
			stageAttack = value;
		}
	}

#endregion
	
}



