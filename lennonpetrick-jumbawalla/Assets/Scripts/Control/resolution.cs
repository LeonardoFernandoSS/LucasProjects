using UnityEngine;
using System.Collections;

public class resolution : MonoBehaviour {

	private GUITexture myGuiTexture;

	public const float BASE_WIDTH = 800;
	public const float BASE_HEIGHT = 600;
	public const float BASE_X_Y_POSITION = -64;
	
	private float baseHeightInverted;
	
	private float originalPixelInsetWidth;
	private float originalPixelInsetHeight;
	
	void Awake()
	{
		myGuiTexture = GetComponent<GUITexture>();
	}
	
	void Start() 
	{
		baseHeightInverted = 1/BASE_HEIGHT;
		originalPixelInsetWidth = myGuiTexture.pixelInset.width;
		originalPixelInsetHeight = myGuiTexture.pixelInset.height;

	}
	
	void FixedUpdate() 
	{   
		float ratio = Screen.height * baseHeightInverted;     
		
		myGuiTexture.pixelInset = new Rect(	((Screen.height * BASE_X_Y_POSITION) / BASE_HEIGHT ), 
		                                  	((Screen.width * BASE_X_Y_POSITION) / BASE_WIDTH ), 
		                                   	originalPixelInsetWidth * ratio,
		                                   	originalPixelInsetHeight * ratio);

	}
}
