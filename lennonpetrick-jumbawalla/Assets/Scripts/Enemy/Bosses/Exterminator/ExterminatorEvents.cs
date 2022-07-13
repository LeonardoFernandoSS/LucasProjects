using UnityEngine;
using System.Collections;

public class ExterminatorEvents : MonoBehaviour {

#region Events
	public static event System.Action onAttackEndsEvent;
	public static event System.Action onLeadingAttackEndsEvent;
	public static event System.Action onShootEvent;
	public static event System.Action onExplodeEvent;
	public static event System.Action onRunEvent;
	public static event System.Action onWheelieEvent;
	public static event System.Action onIdleEvent;
#endregion

	private void onAttackEnds(){

		onAttackEndsEvent();
	
	}
	
	private void onLeadingAttackEnds(){

		onLeadingAttackEndsEvent();

	}

	private void onShoot(){
		
		onShootEvent();
		
	}

	private void onExplode(){
		
		onExplodeEvent();
		
	}

	private void onRun(){
		
		onRunEvent();
		
	}

	private void onWheelie(){
		
		onWheelieEvent();
		
	}

	private void onIdle(){
		
		onIdleEvent();
		
	}

}