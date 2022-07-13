using UnityEngine;
using System.Collections;

public interface ITexts {

	string getLoading();

	string getInventory_ChooseYourWeapons();
	string getInventoru_ChooseYourAvatar();
	string getInventory_ClickHere();

	string getMenu_Collections();
	string getMenu_Play();
	string getMenu_Shop();
	string getMenu_Options();
	string getMenu_Callout_Collection();
	string getMenu_Callout_Levels_textOne();
	string getMenu_Callout_Levels_textTwo();
	string getMenu_Options_Tutorial();
	string getMenu_Options_Credits();
	string getMenu_Options_Character();
	string getMenu_Level_NameLevel(int _idLevel);

	string get_Menu();
	string get_Paused();
	string get_Continue();
	string get_Restart();

	string getWin_Next();
	string getWin_Enemys();
	string getWin_Points();
	string getWin_Texts_Level();
	string getWin_Texts_Complet();
	string getWin_Texts_Congratulations();

	string getDead_Texts_Dead();

	string getTutorial_TipOne();
	string getTutorial_TipTwo();
	string getTutorial_TipTree();
	string getTutorial_TipFour();
	string getTutorial_TipFive();
	string getTutorial_TipSix();

	string getIntro_One();
	string getIntro_Two();
	string getIntro_Tree();
	string getIntro_Four();
	string getIntro_Five();
	string getIntro_Six();
	string getIntro_Seven();
	
}