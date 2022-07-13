using UnityEngine;
using System.Collections;

public class MoveBullet : MonoBehaviour {
	
	private int speedEnemy;
	
	void Update () {
		
		transform.Translate(new Vector3(this.speedEnemy * Time.deltaTime, 0, 0));
		
		Destroy(gameObject, 4.0f);
		
	}
	
	public void setSpeed(int speedEnemy ){
		this.speedEnemy = speedEnemy;
	}
	
	public float getSpeed(){
		return this.speedEnemy;
	}
	
}
