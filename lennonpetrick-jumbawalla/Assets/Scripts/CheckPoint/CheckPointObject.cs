using UnityEngine;
using System.Collections;

public class CheckPointObject {

	private Vector3 playerPosition,
					cameraPosition;

	private int 	amountPoints,
					amountEnemy;
				

	public CheckPointObject (){}

	public CheckPointObject (Vector3 playerPosition, Vector3 cameraPosition, int amountPoints, int amountEnemy){

		this.playerPosition = playerPosition;
		this.cameraPosition = cameraPosition;
		this.amountEnemy = amountEnemy;
		this.amountPoints = amountPoints;

	}

	public Vector3 PlayerPosition {
		get {
			return this.playerPosition;
		}
		set {
			playerPosition = value;
		}
	}

	public Vector3 CameraPosition {
		get {
			return this.cameraPosition;
		}
		set {
			cameraPosition = value;
		}
	}

	public int AmountPoints {
		get {
			return this.amountPoints;
		}
		set {
			amountPoints = value;
		}
	}

	public int AmountEnemy {
		get {
			return this.amountEnemy;
		}
		set {
			amountEnemy = value;
		}
	}

}