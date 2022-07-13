using UnityEngine;
using System.Collections;

public class DragonController : MonoBehaviour {

#region public

	// minimum and maximum time to change the route
	public float minTimeChangeRoute = 2.0f,
				 maxTimeChangeRoute = 5.0f;

	// Every routes that the dragon will pass
	public DragonRoute[] dragonRoutes;

#endregion



#region private

	// the object of dragon head
	private Transform dragon;

#endregion


	// turn on every events
	void OnEnable(){
		
		DragonRoute.endOfRouteEvent += endOfRouteEvent;
		DragonRoute.searchVictimEvent += searchVictimEvent;

	}

	// turn off every events
	void OnDisable(){
		
		DragonRoute.endOfRouteEvent -= endOfRouteEvent;
		DragonRoute.searchVictimEvent -= searchVictimEvent;

	}

	void Start () {
	
		this.dragon = transform.FindChild("Head");

		float _variation = 0.1f; // used to change the scale of body

		// here pass through all body changing the scale
		for(int i = 0; i < transform.childCount; i++){

			if(transform.GetChild(i) != this.dragon){
				
				float _constantScale = 0.37834f;

				transform.GetChild(i).localScale = new Vector3(_constantScale + _variation, _constantScale + _variation, 1.0f);

				_variation += 0.1f;

			}
				
		}

		StartCoroutine(this.chooseTheRoute()); // choose a route

	}

	// method of event endOfRouteEvent
	private void endOfRouteEvent() {

		StartCoroutine(this.chooseTheRoute()); // choose a route

	}

	// method to choose a route after some seconds
	private IEnumerator chooseTheRoute(){

		// wait a random seconds
		yield return new WaitForSeconds(Random.Range(minTimeChangeRoute, maxTimeChangeRoute));

		GetComponent<DragonBodyMovement>().setHead(dragon); // pass the dragon's head to the DragonBodyMovement

		// and pass to the another route again
		dragonRoutes[Random.Range(0, dragonRoutes.Length)].startRoute(dragon);

	}

	// method of event searchVictimEvent
	private void searchVictimEvent() {
		
		StartCoroutine(this.waitAndSearchVictim()); // wait and call method search victim
		
	}

	// method to wait a random time and call searchVictim method
	private IEnumerator waitAndSearchVictim(){

		// wait a random seconds
		yield return new WaitForSeconds(Random.Range(minTimeChangeRoute, maxTimeChangeRoute));

		// call method in the DragonHead class
		this.dragon.GetComponent<DragonHead>().searchVictim();
		
	}

}