using UnityEngine;
using System.Collections;

public class InterfaceJumba : MonoBehaviour {

	#region Variable

	private Transform lifeBar;
	private Transform hitBar;
	private Transform bloodScreen;
	public tk2dTextMesh point;
	private GameObject selectedWeapon;

	#endregion
	
	void Start()
	{
		Inventory.reset();

		GameObject jumba = GameObject.Find("Jumba");

		#region Find Objects

		lifeBar = GameObject.Find("VERMELHO").transform;
		lifeBar.localScale = new Vector3(1,1,1);
		
		hitBar = GameObject.Find("AZUL").transform;
		hitBar.localScale = Vector3.zero;

		bloodScreen = GameObject.Find("sangueTela").transform;

		#endregion

		// repositions the size of the bar with the amount of life
		LifeBar(jumba.GetComponent<ControllerLifePlayer>().getLifePlayer());
		HitBar(jumba.GetComponent<ControllerHitPlayer>().getHitCombo());
		
	}
	
	public void LifeBar(float life){

		this.lifeBar.localScale = new Vector3((life/100), transform.localScale.y, transform.localScale.z);
		if(this.lifeBar.localScale.x <= 0 ) {

			this.lifeBar.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);

		}

		bloodScreenScale(life);
	}
	
	public void HitBar(float hit){ 
		this.hitBar.localScale = new Vector3((hit/100), transform.localScale.y, transform.localScale.z);
	}
	
	void bloodScreenScale(float life){
		if(life >= 20) { this.bloodScreen.localScale = new Vector3(	(life/100) + 0.8f, (life/100) + 0.8f, transform.localScale.z); }
	}
	
	public void setPoint(int point){ this.point.text = point.ToString();}
	
	public void setSelectedWeapon(Vector3 selectedWeaponPosition) {this.selectedWeapon.transform.position = selectedWeaponPosition;}
	
}
