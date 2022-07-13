using UnityEngine;
using System.Collections;

public class StonperBehaviour : EnemyAIBehaviour {

#region Variaveis 
	private float currentTimeOneSkill;
    private float currentTimeTwoSkill;
	private float currentTimeTreeSkill;

	private float waitingTimeOneSkill;
	private float waitingTimeTwoSkill;
	private float waitingTimeTreeSkill;


	private float currentDistance; // distancia atual do inimigo
	private float distanceBetweenPoints; /// <summary> distancia entre os dois pontos, ponto do boss e ponto do player (distance real (+/-))	/// </summary>
	private float positionFjump; /// <summary> posiçao do player quando ativa a habilidade do pulo	/// </summary>
	private float distanceSkillPlayer;

	private Vector3 velocity; // velocidade do character control
	private bool isJump; // verifica se opulo e verdadeiro
	private float speedX; // velocidade em x durante o pulo
	private float forceJump; // velocidade em y quando ah o pulo

	
	public Transform JumpDistance; // posiçao onde ele vai cair depois do pulo
	public float timeToBeat; // tempo onde vai ficar disponivel para levar golpes
#endregion

	void Start () {

		base.Start();

		this.speedX = 53.0f;

		this.forceJump = 200.0f;

		this.isJump = false;

		this.controllerHitSequence = GameObject.Find("Jumba").GetComponent<ControllerHitSequence>();
		
		this.interfaceHitCombo = GameObject.Find("Jumba").GetComponent<InterfaceHitCombo>();
		
		this.controllerHit = GameObject.Find("Jumba").GetComponent<ControllerHitPlayer>();
		
		this.speedEnemy = Random.Range(6.0f,8.5f); // Randomiza um nivel de velocidade para nao agrupar os inimigos

		this.enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);

		this.enemyLife = GetComponent<EnemyLife>();

		this.waitingTimeOneSkill = 3;
		this.waitingTimeTwoSkill = Random.Range(5,9);
		this.waitingTimeTreeSkill = Random.Range(8, 15);

		this.currentTimeOneSkill = 0;
		this.currentTimeTreeSkill = 0;
		this.currentTimeTwoSkill = 0;
	}
	
	void Update () { 
	
		float _distaceReal = (transform.position.x - target.transform.position.x); // diferença entre os pontos distancia real (positiva / negativa)

		this.currentDistance = _distaceReal > 0 ? (transform.position.x - target.transform.position.x) : (target.transform.position.x - transform.position.x );

		this.enemyLife.CheckDied(); // metodo para verificar se morreu

		this.checkTimeSkills(); // tempo para executar suas habilidade

		this.moveDirection = Vector3.zero;

		if(!characterController.isGrounded)
		{
			this.enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
		}

		if(characterController.isGrounded) {

			if(enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Death) {
				
				if(enableCoroutine) 
				{
					StopAllCoroutines();
					enemyAnimation.setState(stateAnim.dead);
					enableCoroutine = false;
				}
				
			}
		
			else if(enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Stop) {

				if(enableCoroutine) {

					StopAllCoroutines();

					StartCoroutine(startFMS( stateAnim.idle, 1f));

					enableCoroutine = false;
				}

				Stop();

				if(distanceToPlayer() <= 12) 
					this.skillNear();

			}

			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Move ) {

				if(enableCoroutine) {

					StopAllCoroutines();

					StartCoroutine(startFMS( stateAnim.run , 2f));

					enableCoroutine = false;
				}

				Move();
				if(distanceToPlayer() <= 12) 
					this.skillNear();


			}
			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Retreat ) {

				if(enableCoroutine) {
					
					StopAllCoroutines();
					
					StartCoroutine(startFMS( stateAnim.idle , 1f));
					
					enableCoroutine = false;
				}
				
				Stop();
				
				if(distanceToPlayer() <= 12) 
					this.skillNear();

			}
			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Knock ) {

				if(enableCoroutine) {
					
					StopAllCoroutines();
					
					StartCoroutine(FMSSkill( stateAnim.knock_two ));
					
					enableCoroutine = false;
				}

			}
			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.SpecialAttack ) {

				if(enableCoroutine) {
					
					StopAllCoroutines();
					
					StartCoroutine(FMSSkillRun( stateAnim.knock , 2));

					waitingTimeTwoSkill = Random.Range(5,9);

					enableCoroutine = false;

				}

				moveDirection = transform.rotation.y == 0 ? new Vector3(-40,0,0) : new Vector3(40,0,0); // Movimentaçao do Boss

			}
			else if ( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.SpecialAttack2) {

				if(enableCoroutine) {
					this.distanceBetweenPoints = (transform.position.x - JumpDistance.transform.position.x);
					this.positionFjump = JumpDistance.transform.position.x;
					this.distanceSkillPlayer = distanceBetweenPoints > 0 ? (transform.position.x - JumpDistance.transform.position.x): 
																		   (JumpDistance.transform.position.x - transform.position.x);

					this.isJump = true;
					this.velocity = this.characterController.velocity;
					this.velocity.y = this.forceJump;
					this.velocity.x = distanceBetweenPoints > 0 ? -speedX : speedX;
					enemyAnimation.setState(stateAnim.knock_up);
					this.enableCoroutine = false;
				}
			}
			else if(enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Catch) 
			{
				if(enableCoroutine) {
					enemyAnimation.setState ( stateAnim.stun );
					enableCoroutine = false;
				}
			}
		}

		if(isJump) {

			float _currentDistance = distanceBetweenPoints > 0 ? (transform.position.x - positionFjump): 
																 (positionFjump - transform.position.x);
			// Apply gravity to our velocity to diminish it over time					
			this.velocity.y += (Physics.gravity.y - this.gravity) * Time.deltaTime;

			if(this.velocity.y < 0) {
				enemyAnimation.setState(stateAnim.knock_down);
			}

			if(_currentDistance <= 1) {

				this.isJump = false;
				this.enableCoroutine = true;
				this.moveDirection = Vector3.zero;
				this.velocity = Vector3.zero;
				this.enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Catch);
			}
		}

		moveDirection += this.velocity;
		
		moveDirection += Physics.gravity;
		
		moveDirection *= Time.deltaTime;

		characterController.Move( moveDirection );

	}
	// Verifica o tempo para ativar as Skill
	void checkTimeSkills () {

		if(currentTimeOneSkill < waitingTimeOneSkill 
		   && enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.Knock
		   && enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.SpecialAttack 
		   && enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.SpecialAttack2) 
			currentTimeOneSkill += Time.deltaTime;

		if(currentTimeTwoSkill < waitingTimeTwoSkill 
		   && enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.Knock
		   && enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.SpecialAttack 
		   && enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.SpecialAttack2) 
			currentTimeTwoSkill += Time.deltaTime;

		if(currentTimeTreeSkill < waitingTimeTreeSkill 
		   && enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.Knock
		   && enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.SpecialAttack2) 
			currentTimeTreeSkill += Time.deltaTime;

		if(this.currentTimeTwoSkill >= this.waitingTimeTwoSkill) {

			if( enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.Catch && enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.SpecialAttack 
			   && enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.SpecialAttack2 && enemyStateMachine.getStateMachineEnemy() != StateMachineEnemy.Knock)
			{
					StopAllCoroutines();
					rotationEnemy();
					enableCoroutine = true;
					enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.SpecialAttack);
					this.currentTimeTwoSkill = 0;
					this.waitingTimeTwoSkill = Random.Range(5,9);
			}
		}

		if(this.currentTimeTreeSkill >= this.waitingTimeTreeSkill)
		{
			if(this.currentDistance >= 20 && this.currentTimeTreeSkill <= 25 && enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Move  ||
			   this.currentDistance >= 20 && this.currentTimeTreeSkill <= 25 && enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Stop ) 
			{
				StopAllCoroutines();
				rotationEnemy();
				enableCoroutine = true;
				enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.SpecialAttack2);
				this.currentTimeTreeSkill = 0;
				this.waitingTimeTreeSkill = Random.Range(8,15);
			}
		}
	}
	// Ativar a skill de perto
	void skillNear() 
	{
		if(currentTimeOneSkill > waitingTimeOneSkill) 
		{
			StopAllCoroutines();
			enableCoroutine = true;
			this.waitingTimeOneSkill = Random.Range(5,8);
			this.currentTimeOneSkill = 0;
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Knock);
		}
		else
		{
			enableCoroutine = true;
			
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
		}

	}

	IEnumerator FMSSkill (stateAnim typeState) 
	{
		moveDirection = Vector3.zero; // movimento do personagem sempre zerando

		enemyAnimation.setState( typeState );

		yield return null;
	}

	IEnumerator FMSSkillRun (stateAnim typeState, float time) 
	{
		moveDirection = Vector3.zero; // movimento do personagem sempre zerando
		
		enemyAnimation.setState( typeState );
		
		yield return new WaitForSeconds(time);

		enableCoroutine = true;

		enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Catch);
	}

	IEnumerator startFMS ( stateAnim typeState , float time )
	{
		moveDirection = Vector3.zero; // movimento do personagem sempre zerando

		enemyAnimation.setState( typeState );
		
		yield return new WaitForSeconds(time);

		enableCoroutine = true;
	
		if(typeState == stateAnim.run) 
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Stop);
		else 
			enemyStateMachine.setStateMachineEnemy(StateMachineEnemy.Move);
	}

	public void setEnable ( bool value ) 
	{
		this.enableCoroutine = value;
	}

	void OnTriggerEnter(Collider other) 
	{
		if(other.tag.Equals("AttackJumba"))
		{
			if( enemyStateMachine.getStateMachineEnemy() == StateMachineEnemy.Catch )
			{
				this.controllerHitSequence.setCollisionEnemy(true);
				this.controllerHit.AddHitCombo();
				this.interfaceHitCombo.AddHitAnimation();

				enemyLife.RemoveLife(StrikeForce.checkStrikeForce().getPowerAttack());
				this.enemyAnimation.receiveAttack();
				GameObject.Find("EnemyLife").GetComponent<InterfaceLifeBoss>().checkLifeBar(enemyLife.life);
			}
		}
	}
}


