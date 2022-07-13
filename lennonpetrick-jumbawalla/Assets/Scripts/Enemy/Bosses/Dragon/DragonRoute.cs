using System;
using UnityEngine;
using System.Collections;

public class DragonRoute : MonoBehaviour {

#region public

	public bool isRouteAttack = false;

	public AudioClip roarAudio;

	public DragonRoute backwardRoute; // your back route

	public Transform firstPoint, // first point
					 lastPoint, // last point
					 dragon; // the dragon's head	

	public Transform[] controlPoints; // the middle points

	public float speed = 5.0f; // the velocity of the dragon to this way

#endregion



#region private

	private float index = 0.0f; // used to multiply with speed to build the bezier curve

	private bool goReturn = false;
	
#endregion



#region events

	public static event Action endOfRouteEvent; // Event to fires when finish the route

	public static event Action searchVictimEvent; // Event to fires to search the victim

#endregion

	// this method is used to draw the way on Editor
	void OnDrawGizmos() {

		// add Gizmos in the points
		this.checkGSpheres(firstPoint);
		
		this.checkGSpheres(lastPoint);
		
		foreach(Transform controlPoint in controlPoints){
			
			this.checkGSpheres(controlPoint);
			
		}

		if(speed > 0){

			Vector3 _line = this.firstPoint.position;

			// draw the line with range of speed / 1000
			for(float i = 0.0f; i <= 1.0f; i += (speed / 1000)){

				Gizmos.DrawLine(_line, _line = Bezier.calc(this.firstPoint, this.controlPoints, this.lastPoint, i));

			}

		}
		
	}

	// turn on every events
	void OnEnable(){
		
		DragonHead.goBackEvent += goBackEvent;

		DragonLife.explodeDragonEvent += explodeDragon;
		
	}
	
	// turn off every events
	void OnDisable(){
		
		DragonHead.goBackEvent -= goBackEvent;

		DragonLife.explodeDragonEvent -= explodeDragon;
		
	}

	// method to check and add Gizmos
	private void checkGSpheres(Transform point){
		
		if(point.GetComponent<GSpheres>() == null){
			
			point.gameObject.AddComponent<GSpheres>();
			
		}
		
	}

	void Update () {

		// if has dragon and can run then
		if(dragon != null){

			// calculates the way point
			Vector3 _wayPoint = Bezier.calc(this.firstPoint, this.controlPoints, this.lastPoint, this.index);
			
			// fix the Z position
			_wayPoint = new Vector3(_wayPoint.x, _wayPoint.y, 0.0f);

			// gets the distance between the dragon position and the way point before change the position
			Vector3 _distance = (this.dragon.position - _wayPoint).normalized;

			if(_distance != Vector3.zero){
				
				Quaternion _rotation = Quaternion.LookRotation(_distance); // look to this distance

				_rotation = new Quaternion(0, 0, _rotation.z, _rotation.w); // fix the rotation
				
				this.dragon.localRotation = _rotation; // and change rotation
				
			}
						
			Transform _dragonChild = this.dragon.GetChild(0);

			// turn Y rotation of object child according the side
			if(_distance.x < 0){
				
				_dragonChild.localRotation = Quaternion.identity;
				
			}
			else{
				
				_dragonChild.localRotation = new Quaternion(0, 180, 0, 0);
				
			}
			
			// now the dragon can receive the wayPoint position
			this.dragon.position = _wayPoint;
		
			this.index += (this.speed / 10) * Time.deltaTime; // change the index to create the next wayPoint

			// if index arrive in the end, 
			// then the dragon will stop
			if (index > 1.0f){

				this.index = 0.0f;

				this.dragon = null;

				if (isRouteAttack){

					this.goReturn = true;

					searchVictimEvent(); // fires the event searchVictim

				}
				else{

					endOfRouteEvent(); // fires the event endOfRoute

				}
								
			}

		}
	
	}

	// always start forward
	public void startRoute(Transform firstOfQueue){

		this.dragon = firstOfQueue;

		if(roarAudio != null){
			
			Director.sharedDirector().playEffect(roarAudio);
			
		}

	}

	// this method do the dragon go back
	private void goBackEvent(Transform objectTransform){

		if(goReturn){

			backwardRoute.startRoute(objectTransform);

			backwardRoute.firstPoint.position = objectTransform.position;

			this.goReturn = false;

		}

	}

	private void explodeDragon(){

		this.dragon = null;

	}

}