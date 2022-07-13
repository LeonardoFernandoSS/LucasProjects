using UnityEngine;
using System.Collections;

public class Message : MonoBehaviour {

	public int maxLetters = 15;

	public static Message sharedMessage(){

		return GameObject.Find("Message").GetComponent<Message>();

	}

	public Message setText(string text){

		transform.FindChild("Text").GetComponent<TextMesh>().text = this.formatText(text);

		return this;
	}

	public void show(){
		
		GetComponent<Animator>().SetBool("AtivaCompra", true);
		
	}

	public void close(){
		
		GetComponent<Animator>().SetBool("AtivaCompra", false);
		
	}

	private string formatText(string text){

		string _text = text;
		
		if (text.Length > maxLetters){
			
			_text = "";
			
			int _cont = 0;
			
			for(int i = 0; i < text.Length; i++){
				
				_cont++;
				
				if(_cont >= (maxLetters + 1) && text[i] == ' '){
					
					_text += '\n';
					
					_cont = 0;
					
				}
				else{
					
					_text += text[i];
					
				}
				
			}
			
		}

		return _text;

	}

}