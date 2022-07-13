using UnityEngine;
using System.Collections;

// class to manager the money in the game
public class MoneyTeeth {

	public static float getAmount(){

		return SaveSystem.loadFloat("teeth_amount");

	}

	public static void addTeeth(float amount){

		float _amount = MoneyTeeth.getAmount();

		_amount += amount;

		SaveSystem.saveFloat("teeth_amount", _amount);
		
	}

	public static bool removeTeeth(float amount){
		
		float _amount = MoneyTeeth.getAmount();

		if(amount <= _amount){
			
			_amount -= amount;

			SaveSystem.saveFloat("teeth_amount", _amount);

			return true;
				
		}
		else{

			return false;

		}

	}

}