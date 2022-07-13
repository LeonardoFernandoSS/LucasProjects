using UnityEngine;
using System.Collections;

public class EnglishTexts : ITexts {
#region GET TUTORIAL TEXTS

	public string getTutorial_TipOne() {
		return "SLIDE FINGER TO MOVE";
	}
	public string getTutorial_TipTwo() {
		return "SLIDE FINGER DOWN \n UP TO JUMP";
	}
	public string getTutorial_TipTree(){
		return "SLIDE FINGER 2 TIMES LOW \nUP FOR DOUBLE JUMP";
	}
	public string getTutorial_TipFour(){
		return "CLICK ON THE SCREEN \nTO ATTACK";
	}
	public string getTutorial_TipFive(){
		return "CLICK TO CHANGE \nOF WEAPON";
	}
	public string getTutorial_TipSix(){
		return "JUMP";
	}

#endregion

#region GET INTERFACE EVENTS (PAUSED, DEAD )
	public string get_Menu(){
		return "MENU";
	}
	public string get_Paused() {
		return "PAUSED";
	}
	public string get_Continue() {
		return "CONTINUE";
	}
	public string get_Restart() {
		return "RESTART";
	}
	public string getDead_Texts_Dead() {
		return "JUMBA \n DEAD!!!";
	}
#endregion

#region GET SCENE MENU
	public string getMenu_Collections(){
		
		return "COLLECTIONS";
		
	}
	public string getMenu_Play(){
		
		return "PLAY";
		
	}
	public string getMenu_Shop(){
		
		return "SHOP";
		
	}
	public string getMenu_Options(){
		
		return "OPTIONS";
		
	}
	public string getMenu_Callout_Collection() {
		
		return 	"collect all \n the treasures \n hidden to \n unlock \n secret stage";
		
	}

	public string getMenu_Callout_Levels_textOne() {

		return "Collect all the hidden treasures";

	}

	public string getMenu_Callout_Levels_textTwo() {

		return "to unlock the secret stage";

	}
	public string getMenu_Options_Tutorial(){

		return "Tutorial";

	}
	public string getMenu_Options_Credits(){

		return "Credits";

	}
	public string getMenu_Options_Character(){

		return "Character";

	}
	public string getMenu_Level_NameLevel(int _idLevel) {
		
		switch( _idLevel ) {
			
			case 1 : {
				return "INVASION";
				break;
			}
			case 2 : {
				return "DESERT'S UFO'S";
				break;
			}
			case 3 : {
				return "FOREST SHOOTER";
				break;
			}
			case 4 : {
				return "TRANSCENDENT STORM";
				break;
			}
			case 5 : {
				return "BOILING JUMBA";
				break;
			}
			case 6 : {
				return "UNWELCOME VISIT";
				break;
			}
			case 7 : {
				return "FLIGHT";
				break;
			}
			case 8 : {
				return "HUNTING IN CAVES";
				break;
			}
			case 9 : {
				return "METEOR SHOWER";
				break;
			}
			case 10 : {
				return "HOSTILE WINTER";
				break;
			}
			case 11 : {
				return "ABDUCTED";
				break;
			}
			default : {
				return "DESCONHECIDO";
			}
		}
	}
#endregion

#region GET SCENE WIN

	public string getWin_Next(){
		return "   NEXT";
	}
	public string getWin_Enemys(){
		return "ENEMYS";
	}
	public string getWin_Points(){
		return "POINTS";
	}
	public string getWin_Texts_Level(){
		switch(LevelManager.currentLevel) {
			case 7 : {
				return "fase 1-1";
				break;
			}
			case 8 : {
				return "fase 1-2";
				break;
			}case 9 : {
				return "fase 1-3";
				break;
			}case 10 : {
				return "fase 2-1";
				break;
			}case 11 : {
				return "fase 2-2";
				break;
			}case 12 : {
				return "fase 2-3";
				break;
			}case 13 : {
				return "fase 3-1";
				break;
			}case 14 : {
				return "fase 3-2";
				break;
			}case 15 : {
				return "fase 3-3";
				break;
			}case 16 : {
				return "fase 4-1";
				break;
			}case 17 : {
				return "fase 4-2";
				break;
			}case 18 : {
				return "fase 4-3";
				break;
			}case 19 : {
				return "fase 5-1";
				break;
			}case 20 : {
				return "fase 5-2";
				break;
			}case 21 : {
				return "fase 5-3";
				break;
			}case 22 : {
				return "fase 6-1";
				break;
			}case 23 : {
				return "fase 6-2";
				break;
			}case 24 : {
				return "fase 6-3";
				break;
			}case 25 : {
				return "fase 7-1";
				break;
			}case 26 : {
				return "fase 7-2";
				break;
			}case 27 : {
				return "fase 7-3";
				break;
			}
			case 28 : {
				return "fase 8-1";
				break;
			}
			case 29 : {
				return "fase 8-2";
				break;
			}
			case 30 : {
				return "fase 8-3";
				break;
			}
			case 31 : {
				return "fase 9-1";
				break;
			}
			case 32 : {
				return "fase 9-2";
				break;
			}
			case 33 : {
				return "fase 9-3";
				break;
			}
			case 34 : {
				return "fase 10-1";
				break;
			}
			case 35 : {
				return "fase 10-2";
				break;
			}
			case 36 : {
				return "fase 10-3";
				break;
			}
			case 37 : {
				return "fase 11-1";
				break;
			}
			case 38 : {
				return "fase 11-2";
				break;
			}
			case 39 : {
				return "fase 11-3";
				break;
			}
			default: {
				return "Unlock";
				break;
			}
		}
	}
	public string getWin_Texts_Complet(){
		return "COMPLETE";
	}
	public string getWin_Texts_Congratulations(){
		return "CONGRATULATIONS";
	}
#endregion

#region GET INTRO SCENE
	public string getIntro_One() {
		return "Welcome";
	}
	public string getIntro_Two(){
		return "This is the \nFamily Walla";
	}
	public string getIntro_Tree(){
		return "Not!";
	}
	public string getIntro_Four(){
		return "Jumba \nNERVOUS!";
	}
	public string getIntro_Five(){
		return "A portal opened. \nland is being \nINVADED!";
	}
	public string getIntro_Six(){
		return "COME \nJUMBA!";
	}
	public string getIntro_Seven(){
		return "Save the pre history....";
	}
#endregion

#region GET INVENTORY SCENE	
	public string getInventory_ChooseYourWeapons(){
		
		return "CHOOSE YOUR WEAPONS";
		
	}
	
	public string getInventoru_ChooseYourAvatar(){
		
		return "CHOOSE YOUR AVATAR";
		
	}
	public string getInventory_ClickHere(){
		
		return "CLICK HERE";
		
	}
#endregion

#region GET LOADING
	public string getLoading() {
		return "      LOADING";
	}
#endregion
}