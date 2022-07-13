using UnityEngine;
using System.Collections;

public class SaveSystem : MonoBehaviour {

	public static bool save(string key, object obj){

		bool _return = false;
		
		try{

			string _json = JsonConverter.objectToJson(obj);
						
			_return = saveString(key, _json);
			
		}
		catch(System.Exception ex){
			
			_return = false;
			
		}
		
		return _return;
		
	}

	public static bool saveInt(string key, int value){

		try{
			
			PlayerPrefs.SetInt(key, value);
			
		}
		catch(PlayerPrefsException ex){

			return false;

		}

		return true;

	}

	public static bool saveFloat(string key, float value){

		try{
			
			PlayerPrefs.SetFloat(key, value);
			
		}
		catch(PlayerPrefsException ex){
			
			return false;
			
		}
		
		return true;
		
	}

	public static bool saveString(string key, string value){

		try{
			
			PlayerPrefs.SetString(key, value);
			
		}
		catch(PlayerPrefsException ex){
			
			return false;
			
		}
		
		return true;
		
	}
	
	public static bool hasObject(string key){
		
		try{
			
			return PlayerPrefs.HasKey(key);
	
		}
		catch(PlayerPrefsException ex){
			
			return false;
			
		}
			
	}
	
	public static object load(string key, System.Type classType){

		try{
			
			return JsonConverter.jsonToObject(loadString(key), classType);
			
		}
		catch(System.Exception ex){
			
			return null;
			
		}

	}

	public static int loadInt(string key){

		try{
			
			return PlayerPrefs.GetInt(key);
			
		}
		catch(PlayerPrefsException ex){

			return 0;

		}
		
	}

	public static float loadFloat(string key){

		try{
			
			return PlayerPrefs.GetFloat(key);
			
		}

		catch(PlayerPrefsException ex){
			
			return 0.0f;
			
		}

	}

	public static string loadString(string key){

		try{
			
			return PlayerPrefs.GetString(key);
			
		}
		catch(PlayerPrefsException ex){
			
			return null;
			
		}

	}
		
	public static bool deleteAll(){
		
		try{
			
			PlayerPrefs.DeleteAll();
			
			return true;
	
		}
		catch(PlayerPrefsException ex){
			
			return false;
			
		}
				
	}
	
	public static bool deleteObject(string key){
		
		try{
			
			PlayerPrefs.DeleteKey(key);
			
			return true;
	
		}
		catch(PlayerPrefsException ex){
			
			return false;
			
		}
				
	}
	
}