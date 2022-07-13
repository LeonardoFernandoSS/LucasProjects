using UnityEngine;
using System.Collections;

public enum posEnumFlyingDinosour { FIRST, SECOND, THIRD }

public class InstantiateFlyingDinosaur : MonoBehaviour {

	private Vector3 positionInstanteate;

	public Transform dino;
	// Enumerator positions tags
	private posEnumFlyingDinosour currentEnumFlying;

	/// <summary>
	/// Components responsible for the positions where the object can be placed
	/// </summary>
	public Transform posInstantiateFirst;
	public Transform posInstantiateSecond;
	public Transform posInstantiateThird;

	/// <summary>
	/// current waiting time and timeout can drop the objects
	/// </summary>
	private float currentWaitingTime;
	private float timeout;

	void Start() {

		this.timeout = 1;

		this.currentWaitingTime = 0;

		this.currentEnumFlying = posEnumFlyingDinosour.FIRST;

	}

	void Update() {

		if(this.currentWaitingTime > timeout) {

			this.instantiateDinosaur();

			this.currentWaitingTime = 0;

		}
		else if(this.currentWaitingTime < timeout) 
			this.currentWaitingTime += Time.deltaTime;
	
	}

	void instantiateDinosaur ( ) {

		if(this.currentEnumFlying == posEnumFlyingDinosour.FIRST) {

			positionInstanteate = new Vector3 (posInstantiateFirst.position.x,
				                               Random.Range( posInstantiateFirst.position.y , posInstantiateSecond.position.y),
				                               posInstantiateFirst.position.z);

			currentEnumFlying = posEnumFlyingDinosour.SECOND;

		}
		else if(currentEnumFlying == posEnumFlyingDinosour.SECOND){

			float _random = UnityEngine.Random.Range( 0, 100 );

			if(_random < 50)  {

				positionInstanteate = new Vector3 (posInstantiateSecond.position.x,
				                                   Random.Range( posInstantiateFirst.position.y , posInstantiateSecond.position.y),
				                                   posInstantiateSecond.position.z);

				currentEnumFlying = posEnumFlyingDinosour.FIRST;

			}
			else {

				positionInstanteate = new Vector3 (posInstantiateSecond.position.x,
				                                   Random.Range( posInstantiateSecond.position.y , posInstantiateThird.position.y),
				                                   posInstantiateSecond.position.z);
				
				currentEnumFlying = posEnumFlyingDinosour.THIRD;

			}

		}
		else {

			positionInstanteate = new Vector3 (posInstantiateThird.position.x,
			                                   Random.Range( posInstantiateSecond.position.y , posInstantiateThird.position.y),
			                                   posInstantiateThird.position.z);
			
			currentEnumFlying = posEnumFlyingDinosour.SECOND;

		}

		Instantiate( dino, positionInstanteate, Quaternion.identity );

		//GameObject _fly = EnemyListControl.sharedEnemyList().addDisabledEnemy( EnemyType.Dino04MoveFly, positionInstanteate );

		this.timeout = Random.Range(2.0f, 3.0f);
	}
}
