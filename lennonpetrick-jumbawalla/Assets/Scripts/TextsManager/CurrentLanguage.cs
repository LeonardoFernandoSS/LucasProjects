using UnityEngine;
using System.Collections;

public class CurrentLanguage : MonoBehaviour {
	
	public static ITexts texts;

	public static LanguageType language;

	// it has to load before all script
	void Awake () {

		// check which language is in the device
		if(Application.systemLanguage.ToString().Equals("Portuguese")){
			
			texts = new PortuguesTexts();

			language = LanguageType.Portuguese;
				
		}
		else{
			
			texts = new EnglishTexts();

			language = LanguageType.English;
			
		}
	
	}
		
}