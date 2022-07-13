using UnityEngine;
using System.Collections;

public class FoodBehaviour : MonoBehaviour {

	public float velFood;
	
	public int amountLife;
	
	private GameObject jumba;
	
	private bool 	activeUpFood,
					activeMoveTowardFood;

	public AudioClip restaurar;
	
	
	void Start(){
		
		this.activeUpFood = true;
		this.activeMoveTowardFood = false;
		this.jumba = GameObject.Find("Jumba");	
	}
	
	void Update(){
		
		if(this.activeUpFood){
			StartCoroutine("ActiveUpFood");
		}
		if(this.activeMoveTowardFood){
			ActiveMoveTowardFood();
		}
		
	}
	
	
	IEnumerator ActiveUpFood()
	{
		
		transform.Translate(new Vector3(0, 20, 0) * Time.deltaTime);
		
		yield return new WaitForSeconds(0.5f);
		
		this.activeMoveTowardFood = true;
		this.activeUpFood = false;
		
	}
	
	void ActiveMoveTowardFood()
	{
		
		transform.position = Vector3.MoveTowards(transform.position, jumba.transform.position, velFood * Time.deltaTime);
		
	}
	
	void OnTriggerEnter(Collider collider){
		
		if(collider.tag == "Player"){

			Director.sharedDirector().playEffect(restaurar);

			ControllerLifePlayer controler = collider.GetComponent<ControllerLifePlayer>();
			
			controler.AddLifePlayer(amountLife);
			
			Destroy(gameObject);
			
		}
		
	}
}
