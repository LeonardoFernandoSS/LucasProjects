using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	private static Transform player;

	private bool checkedPoint = false;

	private static CheckPointObject checkPointObject;

	public AudioClip telefone;

	void Start () {
		
		player = GameObject.Find("Jumba").GetComponent<Transform>();


		if(checkPointObject != null)
		{
			player.position = checkPointObject.PlayerPosition;
			Camera.main.transform.position = checkPointObject.CameraPosition;
			Inventory.setPoints( checkPointObject.AmountPoints );
			Inventory.setEnemyDead( checkPointObject.AmountEnemy);
			player.GetComponent<InterfaceJumba>().setPoint( Inventory.getPoints() );
		}
		
	}

	public static bool hasSaved(){
		
		if (checkPointObject == null){
			
			return false;
			
		}
		else{
			
			return true;
			
		}
		
	}

	public static void save(){

		checkPointObject = new CheckPointObject(player.position, Camera.main.transform.position, Inventory.getPoints(), Inventory.getEnemyDead());

	}
	
	public static void reset(){

		checkPointObject = null;

	}

	private void OnTriggerEnter(Collider collider){
		
		if(collider.gameObject.name == player.name && !this.checkedPoint){
			
			transform.FindChild("Acende").gameObject.SetActive(true);

			Director.sharedDirector().playEffect(telefone);
			
			save();
			
			this.checkedPoint = true;
			
		}
		
	}

}