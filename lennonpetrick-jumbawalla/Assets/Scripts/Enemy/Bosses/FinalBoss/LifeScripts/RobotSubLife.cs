using UnityEngine;
using System.Collections;

public class RobotSubLife : MonoBehaviour {

	public RobotArms arms;
	public float currentPartRobotLife;
	public RobotLife life;

	public float getSubLife() {
		return this.currentPartRobotLife;
	}

	public void addLife ( float life ) {

		this.currentPartRobotLife += life;

	}

	public void removeLife ( float life ) {

		if( this.currentPartRobotLife > 0 ) {

			this.currentPartRobotLife -= life;

			this.life.updateLife( life );
		}
	}
}
