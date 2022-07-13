using UnityEngine;
using System.Collections;

public class LevelManager {
	//responsible for managing the shop which scene was activated
	public static int lastIdSceneShop;

	public static int currentLevel;

	private static int blockedLevels;

	/* This variable is used when chooses a phase on menu.
	 * This saves the scene id to goes in the Inventory and then know which scene has to load 
	 */
	public static int idSceneToLoad;
	
	public static void checkBlockedLevels(){
		
		int _level;
		
		if(SaveSystem.hasObject("blockedLevels")){
			
			_level = SaveSystem.loadInt("blockedLevels");
			
		}
		else{
			
			_level = getLevelID(Level.fase01_2);
			
			SaveSystem.saveInt("blockedLevels", _level);
			
		}
		
		blockedLevels = _level;
		
	}
	
	public static int getBlockedLevels(){
		
		return blockedLevels;
		
	}
	
	public static void unlockLevel(int level){
		
		blockedLevels = level;
		
		blockedLevels++;		
		
		SaveSystem.saveInt("blockedLevels", blockedLevels);	
		
	}
	
	public static int nextLevel(){

		currentLevel++;
		
		if (currentLevel >= blockedLevels){

			unlockLevel(currentLevel);
			
		}
			
		return currentLevel;

	}
	
	public static int getLevelID(Level level){
				
		switch(level){

			case Level.neocubo : {
				return 0;
			}
			case Level.menu : {
				return 1;
			}
			case Level.intro : {
				return 2;
			}
			case Level.loading : {
				return 3;
			}
			case Level.shop : {
				return 4;
			}
			case Level.inventory : {
				return 5;
			}
			case Level.win : {
				return 6;
			}
			case Level.fase01_1: {
				return 7;
			}
			case Level.fase01_2: {
				return 8;
			}
			case Level.fase01_3: {
				return 9;
			}
			case Level.fase02_1: {
				return 10;
			}
			case Level.fase02_2: {
				return 11;
			}
			case Level.fase02_3: {
				return 12;
			}
			case Level.fase03_1: {
				return 13;
			}
			case Level.fase03_2: {
				return 14;
			}
			case Level.fase03_3: {
				return 15;
			}
			case Level.fase04_1: {
				return 16;
			}
			case Level.fase04_2: {
				return 17;
			}
			case Level.fase04_3: {
				return 18;
			}
			case Level.fase05_1:{
				return 19;
			}
			case Level.fase05_2:{
				return 20;
			}
			case Level.fase05_3:{
				return 21;
			}
			case Level.fase06_1:{
				return 22;
			}
			case Level.fase06_2:{
				return 23;
			}
			case Level.fase06_3:{
				return 24;
			}
			case Level.fase07_1:{
				return 25;
			}
			case Level.fase07_2:{
				return 26;
			}
			case Level.fase07_3:{
				return 27;
			}
			case Level.fase08_1:{
				return 28;
			}
			case Level.fase08_2:{
				return 29;
			}
			case Level.fase08_3:{
				return 30;
			}
			case Level.fase09_1:{
				return 31;
			}
			case Level.fase09_2:{
				return 32;
			}
			case Level.fase09_3:{
				return 33;
			}
			case Level.fase10_1:{
				return 34;
			}
			case Level.fase10_2:{
				return 35;
			}
			case Level.fase10_3:{
				return 36;
			}
			case Level.fase11_1:{
				return 37;
			}
			case Level.fase11_2:{
				return 38;
			}
			case Level.fase11_3:{
				return 39;
			}
			case Level.finalScene:{
				return 40;
				break;
			}
			default : {

				return 0;

			}
			
		}

	}
	
}