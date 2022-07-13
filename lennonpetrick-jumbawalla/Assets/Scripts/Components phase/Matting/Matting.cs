using UnityEngine;
using System.Collections;

public enum directionMatting { LEFT, RIGHT }

public class Matting  {

	private directionMatting m_direction;
	private float speed;
	private int direction;
	private bool enableMat;

	public Matting ()
	{
		this.speed = 0;
		this.direction = 1;
		this.enableMat = false;
		this.m_direction = directionMatting.RIGHT;
	}

	public Matting (float time, bool enableMat)
	{
		this.speed = time;
		this.enableMat = enableMat;
		this.direction = -1;
		this.m_direction = directionMatting.RIGHT;
	}
	 
	public directionMatting M_direction {
		get {
			return this.m_direction;
		}
		set {
			m_direction = value;
		}
	}

	public float Speed {
		get {
			return this.speed;
		}
		set {
			speed = value;
		}
	}

	public int Direction {
		get {
			return this.direction;
		}
		set {
			direction = value;
		}
	}

	public bool EnableMat {
		get {
			return this.enableMat;
		}
		set {
			enableMat = value;
		}
	}

	public void onMat() {

		this.enableMat = true;

	}
	public void offMat() {

		this.enableMat = false;

	}

	public void flipMat() {

		if(m_direction == directionMatting.RIGHT) {
			this.m_direction = directionMatting.LEFT;
			this.direction = 1;
		}
		else {
			this.m_direction = directionMatting.RIGHT;
			this.direction = -1;
		}
	}

	public void treadmillRun (GameObject _matting) {

		if(enableMat) {

			float offsetSpeed = this.direction * speed * Time.time;
			_matting.renderer.material.SetTextureOffset("_MainTex", new Vector2(offsetSpeed, 0));

		}
	}
}
