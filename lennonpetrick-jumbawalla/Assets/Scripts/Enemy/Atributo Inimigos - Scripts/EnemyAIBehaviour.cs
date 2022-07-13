using UnityEngine;
using System.Collections;

/*	
 * ESSA SCRIPT PODE SER MODIFICADA PARA MELHORIAS
 * 
 * Script responsavel pela AI dos personagens 2D, Script generica que tem o funcionamento
 * para todos os inimigo.
 * Descrição Geral :
 * {{ Script calcula a distancia entre o objeto que leva a script e o objeto Player em jogo.
 	  Rotaciona o objeto em direção ao Player, depois fazendo-o movimentar até seu limite de distancia
 	  Possui dois metodos que tem como finalidade a implementação dos ataques de cada personagem, os metodos são
 	  MeleeAttack e Ranged Attack, metodos abstratos que a classe que irá herdala devem implementar.
 	  Tem uma distancia que definira seu ataque, ou seja sua distancia de combate, ao chegar perto ou distancia equivalente ao combate. 
 	  Implemetação da vida do personagem para verificar se o mesmo ainda pode estar presenta na cena.}}
 	  
 	Notas:
 	- Metodos de verificação de Dano , não está implementado na mesma.
 
 *   
 *
 *
 */ 

public  class EnemyAIBehaviour : MonoBehaviour {
	// seta estado do inimigo para animação
	#region VARIAVEIS Da Classe EnemyAIBeaviour #####

	public GameObject father;

	protected EnemyCanAttack canAttack;

	protected RaycastEnemy raycastEnemy;

	protected Transform myTransform;

	protected int randomNumberChooseState;

	protected ControllerHitSequence controllerHitSequence; // controle da sequencia de Hit
	
	protected InterfaceHitCombo interfaceHitCombo; // Controle da interface de hitCombo
	
	protected ControllerHitPlayer controllerHit; // Controle de Hit do player
	
	protected EnemyLife enemyLife; // controle da vida do inimigo

	public CharacterController characterController;
	
	protected EnemyStateMachine enemyStateMachine;
	
	protected EnemyAnimator enemyAnimation;

	protected StrikeForce strikeForce;

	protected bool enableCoroutine = true; // verifica se a coroutine esta apta para começar a tocar

	protected Vector3 moveDirection;
	/*
	 *	speedEnemy é a velocidade que o inimigo move em direção ao seu destino
	 *	speedEnemyFend é a velocidade dele fugindo ou ferido
	 *
	 */
	public float	speedEnemy,
					gravity;

	/*
	 *	Target variavel que carrega a target onde o inimigo ira compara sua distance
	 *	e moverá em direção ao mesmo
	 *
	 */
	
	protected Transform target;	
	
	/*
	*	Variavel controladora do tempo para passar cada processo da morte do inimigo
	*	
	*/
	
	#endregion

	public void enableCoroutineTrue ( ) { this.enableCoroutine = true; }
	
	public void Start()
	{
		this.raycastEnemy = GetComponent<RaycastEnemy>();

		this.myTransform = gameObject.transform;

		this.canAttack = GetComponent<EnemyCanAttack>();
		
		this.enemyStateMachine = GetComponent<EnemyStateMachine>();

		this.enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);

		this.target = GameObject.Find("Jumba").transform;

		this.controllerHitSequence = target.GetComponent<ControllerHitSequence>();
		
		this.interfaceHitCombo = target.GetComponent<InterfaceHitCombo>();
		
		this.controllerHit = target.GetComponent<ControllerHitPlayer>();

		this.strikeForce = target.GetComponent<StrikeForce>();
		
		this.enemyLife = GetComponent<EnemyLife>();

		this.characterController = GetComponent<CharacterController>();

		this.enemyAnimation = GetComponent<EnemyAnimator>();

	}
	/*
	 * Controle de movimentaçao do jogador de acordo com sua direçao
	 * */
	public virtual void controlerMove()
	{
		this.moveDirection.y -= this.gravity * Time.deltaTime;
        characterController.Move(this.moveDirection * Time.deltaTime);
	}
	/*
	 * Metodo que tem todos os processos quando sua maquina de estado estiver parada!
	 
	 * */
	public virtual void Stop()
	{

		if(myTransform.position.x < target.transform.position.x)
		{
			this.moveDirection = new Vector3(0,0,0);
			myTransform.rotation = new Quaternion(myTransform.rotation.x,180,myTransform.rotation.z,myTransform.rotation.w);
		}
		else if(myTransform.position.x > target.transform.position.x)
		{
			this.moveDirection = new Vector3(0,0,0);
			myTransform.rotation = new Quaternion(myTransform.rotation.x,0,myTransform.rotation.z,myTransform.rotation.w);
		}
	}
	/*
	 * Metodo que tem todos os processos quando sua maquina de estado estiver Recuando!	
	  * * quando recuando sua rotaçao nunca muda sempre fica em direçao ao personagem so sua direçao
	 * */
	public virtual void Retreat()
	{
		if(myTransform.position.x < target.transform.position.x)
		{
			myTransform.rotation = new Quaternion(myTransform.rotation.x,180,myTransform.rotation.z,myTransform.rotation.w);
			this.moveDirection = new Vector3(-speedEnemy,0,0);
		}
		else if(myTransform.position.x > target.transform.position.x)
		{
			myTransform.rotation = new Quaternion(myTransform.rotation.x,0,myTransform.rotation.z,myTransform.rotation.w);
			this.moveDirection = new Vector3(speedEnemy,0,0);
		}
	}

	//Metodo que tem todos os processos quando sua maquina de estado estiver Andando!
	public virtual void Move()
	{
	
		if(myTransform.position.x < target.transform.position.x - 0.5f)
		{
			myTransform.rotation = new Quaternion(myTransform.rotation.x,180,myTransform.rotation.z,myTransform.rotation.w);
			this.moveDirection = new Vector3(speedEnemy, 0, 0);
		}
		else if(myTransform.position.x > target.transform.position.x + 0.5f)
		{
			myTransform.rotation = new Quaternion(myTransform.rotation.x,0,myTransform.rotation.z,myTransform.rotation.w);
			this.moveDirection = new Vector3(-speedEnemy, 0, 0);
		}
		else if( !characterController.isGrounded  )
		{
			enableCoroutine = true;
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
		}
		else {
			enableCoroutine = true;
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
		}		
	}

	public void rotationEnemy() 
	{

		if(myTransform.position.x < target.transform.position.x)
			myTransform.rotation = new Quaternion(myTransform.rotation.x,180,myTransform.rotation.z,myTransform.rotation.w);
		else if(transform.position.x > target.transform.position.x)
			myTransform.rotation = new Quaternion(myTransform.rotation.x,0,myTransform.rotation.z,myTransform.rotation.w);
	}

	/*
	 * ##### DistanceToPlayer #####
	 * 
	 *	Metodo que carrega o target desejado
	 *	depois setta a distancia entre o objeto da Script e o Object Target
	 */
	public float distanceToPlayer()
	{
		return Mathf.FloorToInt( Vector3.Distance(target.transform.position, myTransform.position)/myTransform.lossyScale.magnitude);
	}

	#region GETTER SETTER	das Variaveis
	
	/*
	 *	Getter e Seeter da variavel SpeedEnemy
	 *
	 *
	 */
	public void setSpeedEnemy(float d)
	{ 
		this.speedEnemy = d;
	}
	
	public float getSpeedEnemy()
	{ 
		return this.speedEnemy;
	}

	#endregion
}


