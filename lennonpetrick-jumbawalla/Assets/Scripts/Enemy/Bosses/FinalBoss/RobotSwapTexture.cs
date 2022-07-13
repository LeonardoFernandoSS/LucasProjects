using UnityEngine;
using System.Collections;

public class RobotSwapTexture : MonoBehaviour {
	
	public ParticleSystem explosionLeft;
	public ParticleSystem explosionRight;
	public ParticleSystem explosionHead_p;

	public RobotControl control;
	public RobotLife life;

	private bool explosionArms = false;
	private bool explosionHead = false;

	private float currentTime;

	public SpriteRenderer armsLeft;
	public SpriteRenderer armsRight;
	public SpriteRenderer brain;
	public SpriteRenderer misseleLeft;
	public SpriteRenderer misseleRight;
	public SpriteRenderer back_fundo;

	public Sprite arms_left;
	public Sprite brain_;
	public Sprite m_left;
	public Sprite fundo;

	public AudioClip explosion;

	void Start() {
		/*
		armsLeft.sprite = arms_left;
		armsRight.sprite = arms_left;

		brain.sprite = brain_;
		misseleLeft.sprite = m_left;
		misseleRight.sprite = m_left;*/

	}

	void Update() {

		if(this.control.braco.getSubLife() <= 0 && this.control.stageBoss == StageBoss.STAGE_TWO && !this.explosionArms) {

			StartCoroutine( startExplosionArms() );

			this.checkLife();

			this.explosionArms = true;

		}
		if(this.control.missiles.getSubLife() <= 0 && this.control.stageBoss == StageBoss.STAGE_TWO && !this.explosionHead) {
				
			control.StopAllCoroutines();
				
			StartCoroutine( startExplosionHead() );

			this.checkLife();

			this.explosionHead = true;
		}

	}

	void checkLife() {

		if(life.getLife() < 0) {
			this.back_fundo.sprite = fundo;
		}
	}

	public void start_explosion() {

		Director.sharedDirector().playEffect(explosion);

		explosionLeft.Stop();
		explosionRight.Stop();
		explosionHead_p.Stop();

		explosionLeft.Play();
		explosionRight.Play();
		explosionHead_p.Play();

	}

	public IEnumerator startAllExplosion() {

		control.StopAllCoroutines();
		
		control.braco.StopAllCoroutines();
		
		control.missiles.StopAllCoroutines();

		explosionLeft.Play();
		explosionRight.Play();
		explosionHead_p.Play();

		yield return new WaitForSeconds( 1.0f );

		armsLeft.sprite = arms_left;
		armsRight.sprite = arms_left;

		brain.sprite = brain_;
		misseleLeft.sprite = m_left;
		misseleRight.sprite = m_left;

		back_fundo.sprite = fundo;

	}

	public IEnumerator startExplosionArms() {

		explosionLeft.Play();
		explosionRight.Play();
		
		yield return new WaitForSeconds( 1.0f );

		armsLeft.sprite = arms_left;
		armsRight.sprite = arms_left;
	}

	public IEnumerator startExplosionHead() {
		
		explosionHead_p.Play();
		
		yield return new WaitForSeconds( 1.0f );
		
		brain.sprite = brain_;
		misseleLeft.sprite = m_left;
		misseleRight.sprite = m_left;
	}	
}
