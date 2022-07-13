using UnityEngine;
using System.Collections;

public class EnemylsCollision : MonoBehaviour {
	
	public float damagePercent, 
				 timeForcePush, 
				 speedPush, 
				 speedPushY;

	public GameObject father;

	public bool aBlow,
				stay;

	private bool readyToHit = true;

	void OnTriggerEnter(Collider collider) 
	{
		if (collider.name == "Jumba")
		{

			if(!collider.GetComponent<ControllerSpecial>().getJumbaSpecial())
			{
				PlayerStateControl player = collider.GetComponent<PlayerStateControl>();
			
				if(player.getCurrentState() != PlayerState.Dead && player.getCurrentState() != PlayerState.DeadFloor )
				{
					PlayerControl.leadAttack(damagePercent, returnRotate(), timeForcePush, speedPush, speedPushY);
				}

				if(gameObject.name.Equals("balaTiro(Clone)")) {
					Destroy(this.gameObject);
				}
			}
		}
	}

	void OnTriggerStay ( Collider collider ) 
	{
		if(stay) {
			if (collider.name == "Jumba")
			{
				if(!collider.GetComponent<ControllerSpecial>().getJumbaSpecial())
				{
					PlayerStateControl player = collider.GetComponent<PlayerStateControl>();

					if(player.getCurrentState() != PlayerState.Dead && player.getCurrentState() != PlayerState.DeadFloor ) 
					{
						readyToHit = true;
						PlayerControl.leadAttack(damagePercent, returnRotate(), timeForcePush, speedPush, speedPushY);
					}
				}
			}
		}
	}

	void OnTriggerExit( Collider collider ) 
	{
		if (collider.name == "Jumba")
		{
			if(!collider.GetComponent<ControllerSpecial>().getJumbaSpecial())
			{
				if(!aBlow)
					readyToHit = true;
			}
		}

	}

	// Retorna a rotaçao do inimigo para verificar de qual lado ele esta atacando
	public bool returnRotate()
	{
		if(father.transform.rotation.y == 0)

			return false;
		
		else 

			return true;
	}

	public void setReadyToHit(bool parm)
	{
		readyToHit = parm;
	}
	
}