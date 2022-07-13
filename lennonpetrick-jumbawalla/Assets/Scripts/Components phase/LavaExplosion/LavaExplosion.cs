using UnityEngine;
using System.Collections;

public class LavaExplosion : MonoBehaviour {

	public float speedLava;

	public float limitMax;
	public float limitMin;

	private float currentTimeExplosion;
	public float waitTimeExplosion;

	private bool downExplosion;
	private bool startExplosion;
	private bool play;

	public Animation camera;

	void Start() 
	{
		this.play = true;

		this.downExplosion = false;

		this.startExplosion = true;

		this.currentTimeExplosion = 0.0f;
	}

	void Update() {

		if( this.startExplosion ) {

			if(this.currentTimeExplosion >= this.waitTimeExplosion) {

				if(play) 
				{
					play = false;

					GetComponent<AudioSource>().Play();

					camera.Play("tremeCamera3");

				}

				transform.Translate( new Vector3 (0, this.downExplosion == true ? -this.speedLava: this.speedLava, 0 ) * Time.deltaTime );

				if( this.downExplosion ) {

					if(transform.localPosition.y <= this.limitMin)  {

						camera.Stop();

						GetComponent<AudioSource>().Stop();

						this.currentTimeExplosion = 0.0f;

						this.downExplosion = !this.downExplosion;

						play = true;
					}

				}
				else {

					if(transform.localPosition.y >= this.limitMax) {


						this.downExplosion = !this.downExplosion;


					}
				}
			}

			if(this.currentTimeExplosion <= this.waitTimeExplosion) 
				this.currentTimeExplosion += Time.deltaTime;
		}
	}
}
