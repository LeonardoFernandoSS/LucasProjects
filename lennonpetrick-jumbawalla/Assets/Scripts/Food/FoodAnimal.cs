using UnityEngine;
using System.Collections;

public class FoodAnimal : MonoBehaviour {
	
	public GameObject effect;
	public GameObject food;
	public AudioClip comida;
	
	void OnTriggerEnter(Collider collider){
		
		if(collider.tag == "AttackJumba"){
			
			Director.sharedDirector().playEffect(comida);

			GameObject _effect;
			_effect = Instantiate(this.effect, transform.position, Quaternion.identity) as GameObject;
			
			GameObject _food;
		
			_food = Instantiate(this.food, transform.position, Quaternion.identity) as GameObject;
			
			Destroy(gameObject);	
		}
		
	}
}
