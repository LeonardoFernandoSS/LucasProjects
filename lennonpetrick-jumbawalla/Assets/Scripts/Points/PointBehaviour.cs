using UnityEngine;
using System.Collections;

public class PointBehaviour : MonoBehaviour 
{
	public AudioClip pontos;

	private GameObject target;
	private InterfaceJumba interfaceJumba;
	
	private float distancePlayer;
	private float currentTime;
	private int points;

	void Start()
	{
		this.currentTime = 0;
		this.target = GameObject.Find("Jumba");
		this.interfaceJumba = target.GetComponent<InterfaceJumba>();
	}

	void Update () 
	{
		distancePlayer = Vector3.Distance(transform.position, target.transform.position);
		
		if(distancePlayer <= 30) 
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 40 * Time.deltaTime);

		this.currentTime += Time.deltaTime;

		if(this.currentTime >= 6.0f) 
		{
			Destroy(this.gameObject);
			this.currentTime = 0.0f;
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.name == "Jumba")
		{
			Inventory.addPoints(this.points);
			interfaceJumba.setPoint(Inventory.getPoints());
			Director.sharedDirector().playEffect(pontos);
			Destroy(this.gameObject);
		}
	}

	#region SET ??? GET
	
	public void setPoints( int value )
	{
		this.points = value;
	}
	public int getPoints()
	{
		return this.points;
	}
	
	public float CurrentTime
	{
		get 
		{
			return this.currentTime;
		}
		set 
		{
			currentTime = value;
		}
	}
	#endregion
}
