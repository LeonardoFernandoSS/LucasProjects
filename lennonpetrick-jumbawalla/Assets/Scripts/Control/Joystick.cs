//////////////////////////////////////////////////////////////
// Joystick.cs
// Penelope iPhone Tutorial
//
// Joystick creates a movable joystick (via GUITexture) that 
// handles touch input, taps, and phases. Dead zones can control
// where the joystick input gets picked up and can be normalized.
//
// Optionally, you can enable the touchPad property from the editor
// to treat this Joystick as a TouchPad. A TouchPad allows the finger
// to touch down at any point and it tracks the movement relatively 
// without moving the graphic
//////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
public class Joystick : MonoBehaviour {

	public const float BASE_WIDTH = 800;
	public const float BASE_HEIGHT = 600;
	public const float BASE_X_Y_POSITION = -64;

	private float baseHeightInverted;
	
	private float originalPixelInsetWidth;
	private float originalPixelInsetHeight;
	private float ratio;

	public bool touchPad; 													// Is this a TouchPad?
	
	public Rect touchZone;
	
	public Vector2 position; 												// [-1, 1] in x,y
	
	public Vector2 deadZone = Vector2.zero;									// Control when position is output
	
	public bool normalize = false; 											// Normalize output after the dead-zone?
	
	public int tapCount;													// Current tap count
	
	
	
	
	private static Joystick[] joysticks;			    					// A static collection of all joysticks
	
	private static bool enumeratedJoysticks = false;
	
	private static float tapTimeDelta = 0.3f;								// Time allowed between taps
	
	private int lastFingerId = -1;											// Finger last used for this joystick
	
	private float tapTimeWindow;											// How much time there is left for a tap to occur
	
	private Vector2 fingerDownPos;
	
	//private float fingerDownTime, 
				  //firstDeltaTime = 0.5f;
	
	private GUITexture gui;													// Joystick graphic
	
	private Rect defaultRect;												// Default position / extents of the joystick graphic
	
	private Boundary guiBoundary = new Boundary();							// Boundary for joystick graphic
	
	private Vector2 guiTouchOffset,
					guiCenter;												// Center of joystick

	void Start () {

		// Cache this component at startup instead of looking up every frame	
		this.gui = GetComponent<GUITexture>();

		baseHeightInverted = 1/BASE_HEIGHT;
		originalPixelInsetWidth = gui.pixelInset.width;
		originalPixelInsetHeight = gui.pixelInset.height;
		
		// Store the default rect for the gui, so we can snap back to it
		this.defaultRect = this.gui.pixelInset;	
	    
	    this.defaultRect.x += transform.position.x * Screen.width;// + gui.pixelInset.x; // -  Screen.width * 0.5;
	    this.defaultRect.y += transform.position.y * Screen.height;// - Screen.height * 0.5;
	    
		transform.position = new Vector3(0.0f, 0.0f, transform.position.z);
				        
		if (touchPad){
			
			// If a texture has been assigned, then use the rect ferom the gui as our touchZone
			if (gui.texture){
				
				this.touchZone = this.defaultRect;
				
			}
			
		}
		else{
			
			// This is an offset for touch input to match with the top left
			// corner of the GUI
			this.guiTouchOffset.x = this.defaultRect.width * 0.5f;
			
			this.guiTouchOffset.y = this.defaultRect.height * 0.5f;
			
			
			
			// Cache the center of the GUI, since it doesn't change
			this.guiCenter.x = this.defaultRect.x + this.guiTouchOffset.x;
			
			this.guiCenter.y = this.defaultRect.y + this.guiTouchOffset.y;
			
			
			
			
			// Let's build the GUI boundary, so we can clamp joystick movement
			this.guiBoundary.min.x = this.defaultRect.x - this.guiTouchOffset.x;
			
			this.guiBoundary.max.x = this.defaultRect.x + this.guiTouchOffset.x;
			
			this.guiBoundary.min.y = this.defaultRect.y - this.guiTouchOffset.y;
			
			this.guiBoundary.max.y = this.defaultRect.y + this.guiTouchOffset.y;
			
		}
		
	}
	
	void Update () {

		ratio = Screen.height * baseHeightInverted; 

		if (!enumeratedJoysticks){
			
			// Collect all joysticks in the game, so we can relay finger latching messages
			joysticks = FindObjectsOfType(typeof(Joystick)) as Joystick[];
			enumeratedJoysticks = true;
			
		}	
			
		int _count = Input.touchCount;
		
		// Adjust the tap time window while it still available
		if (this.tapTimeWindow > 0 ){
			
			this.tapTimeWindow -= Time.deltaTime;
			
		}
		else{
			
			this.tapCount = 0;
			
		}
		
		if (_count == 0 ){
			
			this.resetJoystick();
			
		}
		else{
			
			for(int i = 0; i < _count; i++){
				
				Touch _touch = Input.GetTouch(i);
				
				Vector2 _guiTouchPos = _touch.position - this.guiTouchOffset;
		
				bool _shouldLatchFinger = false;
				
				if (touchPad){				
					
					if (touchZone.Contains(_touch.position)){
						
						_shouldLatchFinger = true;
						
					}
					
				}
				else if (gui.HitTest(_touch.position)){
					
					_shouldLatchFinger = true;
					
				}		
		
				// Latch the finger if this is a new touch
				if (_shouldLatchFinger && (lastFingerId == -1 || lastFingerId != _touch.fingerId)){
					
					if (touchPad){
						
						this.changeAlphaColorGUI(0);
						
						this.lastFingerId = _touch.fingerId;
						
						this.fingerDownPos = _touch.position;
						
						//this.fingerDownTime = Time.time;
						
					}
					
					this.lastFingerId = _touch.fingerId;
					
					// Accumulate taps if it is within the time window
					if (tapTimeWindow > 0){
						
						tapCount++;
						
					}
					else{
						
						tapCount = 1;
						tapTimeWindow = tapTimeDelta;
						
					}
												
					// Tell other joysticks we've latched this finger
					foreach(Joystick joy in joysticks){
						
						if (joy != this){
							
							joy.latchedFinger(_touch.fingerId);
							
						}
						
					}
					
				}				
		
				if (lastFingerId == _touch.fingerId){	
					
					// Override the tap count with what the iPhone SDK reports if it is greater
					// This is a workaround, since the iPhone SDK does not currently track taps
					// for multiple touches
					if (_touch.tapCount > tapCount){
					
						this.tapCount = _touch.tapCount;
						
					}
					
					if (touchPad){	
						
						// For a touchpad, let's just set the position directly based on distance from initial touchdown
						this.position.x = Mathf.Clamp((_touch.position.x - this.fingerDownPos.x) / (touchZone.width / 2), -1, 1);
						this.position.y = Mathf.Clamp((_touch.position.y - this.fingerDownPos.y) / (touchZone.height / 2), -1, 1);

					}
					else{					
						
						Rect _pixelInset = this.gui.pixelInset;
						
						// Change the location of the joystick graphic to match where the touch is
						_pixelInset.x =  Mathf.Clamp(_guiTouchPos.x, this.guiBoundary.min.x, this.guiBoundary.max.x);
						_pixelInset.y =  Mathf.Clamp(_guiTouchPos.y, this.guiBoundary.min.y, this.guiBoundary.max.y);	
						
						this.gui.pixelInset = new Rect( _pixelInset.x, 
						                                _pixelInset.y,
						                               	originalPixelInsetWidth * ratio,
						                               	originalPixelInsetHeight * ratio);
						
					}
					
					if (_touch.phase == TouchPhase.Ended || _touch.phase == TouchPhase.Canceled){
						
						this.resetJoystick();					
						
					}
					
				}	
				
			}
			
		}
		
		if (!touchPad){
			
			// Get a value between -1 and 1 based on the joystick graphic location
			this.position.x = ( this.gui.pixelInset.x + this.guiTouchOffset.x - this.guiCenter.x ) / this.guiTouchOffset.x;
			this.position.y = ( this.gui.pixelInset.y + this.guiTouchOffset.y - this.guiCenter.y ) / this.guiTouchOffset.y;
			
		}
	
		// Adjust for dead zone	
		float _absoluteX = Mathf.Abs(this.position.x);		
		float _absoluteY = Mathf.Abs(this.position.y);
	
		if (_absoluteX < this.deadZone.x){
			
			// Report the joystick as being at the center if it is within the dead zone
			this.position.x = 0;
			
		}
		else if (normalize){
			
			// Rescale the output after taking the dead zone into account
			this.position.x = Mathf.Sign(this.position.x) * (_absoluteX - this.deadZone.x) / ( 1 - this.deadZone.x );
			
		}
			
		if (_absoluteY < this.deadZone.y){
			
			// Report the joystick as being at the center if it is within the dead zone
			this.position.y = 0;
			
		}
		else if (normalize){
			
			// Rescale the output after taking the dead zone into account
			this.position.y = Mathf.Sign(this.position.y) * (_absoluteY - this.deadZone.y) / (1 - this.deadZone.y);
			
		}
	
	}
	
	public void disable(){
		
		gameObject.SetActive(false);
		
		enumeratedJoysticks = false;
		
	}
	
	private void resetJoystick(){
		
		// Release the finger control and set the joystick back to the default position
		this.gui.pixelInset = this.defaultRect;
		
		this.lastFingerId = -1;
		
		this.position = Vector2.zero;
		
		this.fingerDownPos = Vector2.zero;
		
		if (touchPad){
			
			this.changeAlphaColorGUI(0f);
			
		}
		
	}
	
	public bool isFingerDown(){
		
		return (this.lastFingerId != -1);
		
	}
	
	private void latchedFinger(int fingerId){
		
		// If another joystick has latched this finger, then we must release it
		if (lastFingerId == fingerId){
			
			this.resetJoystick();
			
		}
		
	}
	
	private void changeAlphaColorGUI(float alpha){
		
		Color _color = this.gui.color;
		
		_color.a = alpha;
		
		this.gui.color = _color;
		
	}
		
	// A simple class for bounding how far the GUITexture will move
	private class Boundary {
		
		public Vector2 min = Vector2.zero,
					   max = Vector2.zero;
		
	}
	
}