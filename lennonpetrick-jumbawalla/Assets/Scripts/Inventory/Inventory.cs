using UnityEngine;
using System.Collections;

public class Inventory {

	public static int indexWeapon;

	private static Clothes clothes;
	private static Weapon[] weapons;
	private static int currentWeapon, 					   
					   points,
					   enemyDead;
	
	public static void init() {

		Inventory.weapons = new Weapon[2];
						
		Inventory.indexWeapon = 0;
		
		Inventory.reset();
	
	}
	
	public static void reset(){
		
		Inventory.points = 0;

		Inventory.enemyDead = 0;
		
	}

	public static void changeClothes(Clothes clothes){
		
		Inventory.clothes = clothes;
		
	}

	public static Clothes getClothes() {
		return Inventory.clothes;
	}

	public static void changeWeapon(){
		
		if (Inventory.currentWeapon == 0){

			Inventory.currentWeapon = 1;

		}
		else{

			Inventory.currentWeapon = 0;

		}
		
	}
	
	public static Weapon getWeapon() {
		
		return Inventory.weapons[Inventory.currentWeapon];
		
	}

	public static Weapon[] getWeapons() {
		
		return Inventory.weapons;
		
	}
	
	public static void addWeapon(Weapon weapon){

		weapons.SetValue(weapon, Inventory.indexWeapon);

	}

	public static void addWeapon(Weapon _weapon, int _value){
		weapons[_value] = _weapon;
	}
	
	public static int getPoints(){
		
		return Inventory.points;

	}
	
	public static void addPoints(){
		
		Inventory.points++;

	}
	
	public static void addPoints(int points){
		
		Inventory.points += points;

	}

	public static void setPoints(int points){
		
		Inventory.points = points;
		
	}

	public static void addEnemyDead(int amount){

		Inventory.enemyDead += amount;

	}

	public static void setEnemyDead(int amount){
		
		Inventory.enemyDead = amount;
		
	}

	public static int getEnemyDead(){

		return Inventory.enemyDead;

	}

}