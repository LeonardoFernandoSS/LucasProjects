using UnityEngine;
using System.Collections;

public class ControllerComboPlayer : MonoBehaviour {

	private float comboBar;
	
	public void setComboBar(float combo) {this.comboBar = combo;}
	
	public float getComboBar() {return this.comboBar;}
	
	public void AddCombo(float combo) { this.comboBar += combo;	}
	
	public void CheckCombo()
	{
		if(this.comboBar >= 100) {
			this.comboBar = 0;
		}
	}
}
