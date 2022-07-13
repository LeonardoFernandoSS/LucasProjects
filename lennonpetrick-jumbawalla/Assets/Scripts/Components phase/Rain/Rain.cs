using UnityEngine;
using System.Collections;

public class Rain : MonoBehaviour {

	public Transform 	player,
						rainPrefab;

	private float 	lastTimeRain = 0;

	public float	runTime = 5;

	private RaycastHit hit;
	public AudioClip rain,
					vulcao;

	void Update() 
	{ 

		if(this.lastTimeRain >= this.runTime) 
		{

			if(Physics.Raycast(transform.position, new Vector3(Vector3.down.x + Random.Range(-300,400) , Vector3.down.y * 1000, Vector3.down.z), out hit, Mathf.Infinity)) {

				if(hit.transform.tag.Equals("Floor")) 
				{

					Vector3 _newPosition = hit.point;

					if(player.GetComponent<CharacterController>().isGrounded)
					{

						createRain(_newPosition);

					}
				}
			}
		}

		this.lastTimeRain +=  Time.deltaTime;
	}

	private void createRain (Vector3 _newPositionPlayer) 
	{
		Instantiate ( rainPrefab,
		             new Vector3 (_newPositionPlayer.x , _newPositionPlayer.y, rainPrefab.gameObject.transform.position.z), 
		             rainPrefab.rotation );

		Director.sharedDirector().playEffect( rainPrefab.name.Equals("Raio") ? rain : vulcao );

		this.lastTimeRain = 0;
	}

}
