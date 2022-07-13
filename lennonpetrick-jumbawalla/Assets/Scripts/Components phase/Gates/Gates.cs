using UnityEngine;
using System.Collections;

public class Gates : MonoBehaviour {

	public Animator animDoor;

	private bool door;

	public void reverseDoor() {

		this.door = !this.door;

		this.playAnimationDoor();

	}

	public void openDoor () {

		this.door = true;

		this.playAnimationDoor();

	}

	public void lockDoor() {

		this.door = false;

		this.playAnimationDoor();

	}

	void playAnimationDoor() {

		if(this.door && this.animDoor.GetInteger("idDoor") == 2)
			this.animDoor.SetInteger("idDoor", 1);
		else if(!this.door && this.animDoor.GetInteger("idDoor") == 1)
			this.animDoor.SetInteger("idDoor", 2);

	}
}
