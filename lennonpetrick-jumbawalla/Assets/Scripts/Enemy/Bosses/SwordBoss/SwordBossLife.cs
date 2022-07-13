using UnityEngine;
using System.Collections;

public class SwordBossLife : MonoBehaviour {

	public float life;

	public float currentLife;

	private SwordBoss swordBoss;

	void Start() {

		this.currentLife = this.life;

		this.swordBoss = GetComponent<SwordBoss>();


	}

	public float getCurrentLife() {

		return this.currentLife;

	}

	public void removeLife( float value) {

		this.currentLife -= value;

		if(this.currentLife <= 0) {

			this.currentLife = -1;

		}
	}

	public void addLife( float value) {

		this.currentLife += value;

		if(this.currentLife >= this.life) 

			this.currentLife = this.life;

	}

}
