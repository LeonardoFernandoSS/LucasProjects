using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public static bool leadAttack(float damagePercent, bool enemySide, float timeForcePush, float speedPush, float speedPushY){

		if(PlayerStateControl.sharePlayer().getCurrentState() != PlayerState.Dead){

			GettingAttacks.checkGettingAttacks().setProperties(enemySide, timeForcePush, speedPush, speedPushY);
			
			// Estado do player sendo atacado
			PlayerStateControl.sharePlayer().setState(PlayerState.LeadingAttack);
			
			// Remove a vida do jogador
			ControllerLifePlayer.sharedLife().RemoveLifePlayer(damagePercent);
			
			// Limpa a lista
			ControllerHitSequence.checkHitSequence().ClearList();

			return true;

		}
		return false;
	}
}