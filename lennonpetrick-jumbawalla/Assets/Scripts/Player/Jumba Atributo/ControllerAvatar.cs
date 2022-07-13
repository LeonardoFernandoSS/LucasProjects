using UnityEngine;
using System.Collections;
using SmoothMoves;
public class ControllerAvatar : MonoBehaviour {

	private BoneAnimation animationPlayer;
	
	private string 	weaponPlayer,
					rightArm,
					leftArm,
					headOne,
					headTwo,
					waist,
					neckOne,
					neckTwo,
					body,
					rightHand,
					leftHand,
					rightLeftFoot,
					rightFoot,
					leftFoot,
					breastplate,
					rightLeg,
					leftLeg,
					tail,
					skirtOne,
					skirtTwo;
	
	
	private string 	swapweaponPlayer,
					swaprightArm,
					swapleftArm,
					swapheadOne,
					swapheadTwo,
					swapwaist,
					swapneckOne,
					swapneckTwo,
					swapbody,
					swaprightHand,
					swapleftHand,
					swaprightLeftFoot,
					swaprightFoot,
					swapleftFoot,
					swapbreastplate,
					swaprightLeg,
					swapleftLeg,
					swaptail,
					swapskirtOne,
					swapskirtTwo;
	
	/*
	 * Setter e Getter para as variaveis do inventario no corpo
	 * 
	 * */
	#region SET E GET PLAYER -------------------
	public void setWeaponPlayer (string swapInv) {this.weaponPlayer = swapInv;}
	
	public string getWeaponPlayer () {return this.weaponPlayer;}
	
	//### -------------------- ###//
	
	public void setRightArm (string swapInv) {this.rightArm = swapInv;}
	
	public string getRightArm () {return this.rightArm;}
	
	//### -------------------- ###//
	
	public void setLeftArm (string swapInv) {this.leftArm = swapInv;}
	
	public string getLeftArm () {return this.leftArm;}
	
	//### -------------------- ###//
	
	public void setHeadOne (string swapInv) {this.headOne = swapInv;}
	
	public string getHeadOne () {return this.headOne;}
	
	//### -------------------- ###//
	
	public void setHeadTwo (string swapInv) {this.headTwo = swapInv;}
	
	public string getHeadTwo () {return this.headTwo;}
	
	//### -------------------- ###//
	
	public void setWaist (string swapInv) {this.waist = swapInv;}
	
	public string getWaist () {return this.waist;}
	
	//### -------------------- ###//
	
	public void setNeckOne (string swapInv) {this.neckOne = swapInv;}
	
	public string getNeckOne () {return this.neckOne;}
	
	//### -------------------- ###//
	
	public void setNeckTwo (string swapInv) {this.neckTwo = swapInv;}
	
	public string getNeckTwo () {return this.neckTwo;}
	
	//### -------------------- ###//
	
	public void setBody (string swapInv) {this.body = swapInv;}
	
	public string getBody () {return this.body;}
	
	//### -------------------- ###//
	
	public void setRightHand (string swapInv) {this.rightHand = swapInv;}
	
	public string getRightHand () {return this.rightHand;}
	
	//### -------------------- ###//
	
	public void setLeftHand (string swapInv) {this.leftHand = swapInv;}
	
	public string getLeftHand () {return this.leftHand;}	
	
	//### -------------------- ###//
	
	public void setRightLeftFoot (string swapInv) {this.rightLeftFoot = swapInv;}
	
	public string getRightLeftFoot () {return this.rightLeftFoot;}
	
	//### -------------------- ###//
	
	public void setRightFoot (string swapInv) {this.rightFoot = swapInv;}
	
	public string getRightFoot () {return this.rightFoot;}
	
	//### -------------------- ###//
	
	public void setLeftFoot (string swapInv) {this.leftFoot = swapInv;}
	
	public string getLeftFoot () {return this.leftFoot;}
	
	//### -------------------- ###//
	
	
	public void setBreastplate (string swapInv) {this.breastplate = swapInv;}
	
	public string getBreastplate () {return this.breastplate;}
	
	//### -------------------- ###//
	
	public void setRightLeg (string swapInv) {this.rightLeg = swapInv;}
	
	public string getRightLeg () {return this.rightLeg;}
	
	//### -------------------- ###//
	
	public void setLeftLeg (string swapInv) {this.leftLeg = swapInv;}
	
	public string getLeftLeg () {return this.leftLeg;}
	
	//### -------------------- ###//
	
	public void setTail (string swapInv) {this.tail = swapInv;}
	
	public string getTail () {return this.tail;}
	
	//### -------------------- ###//
	
	public void setSkirtOne(string swapInv) {this.skirtOne = swapInv;}
	
	public string getSkirtOne () {return this.skirtOne;}
	
	//### -------------------- ###//
	
	public void setSkirtTwo(string swapInv) {this.skirtTwo = swapInv;}
	
	public string getSkirtTwo () {return this.skirtTwo;}
	
	
	/*
	 * Setter e Getter para as variaveis do inventario no de troca
	 * 
	 * */
	#endregion 
	
	#region SET E GET PLAYERSWAP ----------------------
	 public string getSwapbody() {
        return swapbody;
    }

    public void setSwapbody(string swapbody) {
        this.swapbody = swapbody;
    }

    public string getSwapbreastplate() {
        return swapbreastplate;
    }

    public void setSwapbreastplate(string swapbreastplate) {
        this.swapbreastplate = swapbreastplate;
    }

    public string getSwapheadOne() {
        return swapheadOne;
    }

    public void setSwapheadOne(string swapheadOne) {
        this.swapheadOne = swapheadOne;
    }

    public string getSwapheadTwo() {
        return swapheadTwo;
    }

    public void setSwapheadTwo(string swapheadTwo) {
        this.swapheadTwo = swapheadTwo;
    }

    public string getSwapleftArm() {
        return swapleftArm;
    }

    public void setSwapleftArm(string swapleftArm) {
        this.swapleftArm = swapleftArm;
    }

    public string getSwapleftFoot() {
        return swapleftFoot;
    }

    public void setSwapleftFoot(string swapleftFoot) {
        this.swapleftFoot = swapleftFoot;
    }

    public string getSwapleftHand() {
        return swapleftHand;
    }

    public void setSwapleftHand(string swapleftHand) {
        this.swapleftHand = swapleftHand;
    }

    public string getSwapleftLeg() {
        return swapleftLeg;
    }

    public void setSwapleftLeg(string swapleftLeg) {
        this.swapleftLeg = swapleftLeg;
    }

    public string getSwapneckOne() {
        return swapneckOne;
    }

    public void setSwapneckOne(string swapneckOne) {
        this.swapneckOne = swapneckOne;
    }

    public string getSwapneckTwo() {
        return swapneckTwo;
    }

    public void setSwapneckTwo(string swapneckTwo) {
        this.swapneckTwo = swapneckTwo;
    }

    public string getSwaprightArm() {
        return swaprightArm;
    }

    public void setSwaprightArm(string swaprightArm) {
        this.swaprightArm = swaprightArm;
    }

    public string getSwaprightFoot() {
        return swaprightFoot;
    }

    public void setSwaprightFoot(string swaprightFoot) {
        this.swaprightFoot = swaprightFoot;
    }

    public string getSwaprightHand() {
        return swaprightHand;
    }

    public void setSwaprightHand(string swaprightHand) {
        this.swaprightHand = swaprightHand;
    }

    public string getSwaprightLeftFoot() {
        return swaprightLeftFoot;
    }

    public void setSwaprightLeftFoot(string swaprightLeftFoot) {
        this.swaprightLeftFoot = swaprightLeftFoot;
    }

    public string getSwaprightLeg() {
        return swaprightLeg;
    }

    public void setSwaprightLeg(string swaprightLeg) {
        this.swaprightLeg = swaprightLeg;
    }

    public string getSwapskirtOne() {
        return swapskirtOne;
    }

    public void setSwapskirtOne(string swapskirtOne) {
        this.swapskirtOne = swapskirtOne;
    }

    public string getSwapskirtTwo() {
        return swapskirtTwo;
    }

    public void setSwapskirtTwo(string swapskirtTwo) {
        this.swapskirtTwo = swapskirtTwo;
    }

    public string getSwaptail() {
        return swaptail;
    }

    public void setSwaptail(string swaptail) {
        this.swaptail = swaptail;
    }

    public string getSwapwaist() {
        return swapwaist;
    }

    public void setSwapwaist(string swapwaist) {
        this.swapwaist = swapwaist;
    }

    public string getSwapweaponPlayer() {
        return swapweaponPlayer;
    }

    public void setSwapweaponPlayer(string swapweaponPlayer) {
        this.swapweaponPlayer = swapweaponPlayer;
    }
	#endregion
	
	
	/*
	 * SwapWeaponTexture método para trocar texture 
	 * 
	 * */
	
	public void SwapWeaponTexture()
	{
		
		animationPlayer.SwapTexture("Weapon", getWeaponPlayer(), "Weapon", getSwapweaponPlayer());
		
	}
	
	public void SwapSetTexture()
	{
		
		animationPlayer.SwapTexture("Set", getRightArm(), "Set", getSwaprightArm());
		animationPlayer.SwapTexture("Set", getLeftArm(), "Set", getSwapleftArm());
		animationPlayer.SwapTexture("Set", getHeadOne(), "Set", getSwapheadOne());
		animationPlayer.SwapTexture("Set", getHeadTwo(), "Set", getSwapheadTwo());
		animationPlayer.SwapTexture("Set", getWaist(), "Set", getSwapwaist());
		animationPlayer.SwapTexture("Set", getNeckOne(), "Set", getSwapneckOne());
		animationPlayer.SwapTexture("Set", getNeckTwo(), "Set", getSwapneckTwo());
		animationPlayer.SwapTexture("Set", getBody(), "Set", getSwapbody());
		animationPlayer.SwapTexture("Set", getRightHand(), "Set", getSwaprightHand());
		animationPlayer.SwapTexture("Set", getLeftHand(), "Set", getSwapleftHand());
		animationPlayer.SwapTexture("Set", getRightLeftFoot(), "Set", getSwaprightLeftFoot());
		animationPlayer.SwapTexture("Set", getRightFoot(), "Set", getSwaprightFoot());
		animationPlayer.SwapTexture("Set", getLeftFoot(), "Set", getSwapleftFoot());
		animationPlayer.SwapTexture("Set", getBreastplate(), "Set", getSwapbreastplate());
		animationPlayer.SwapTexture("Set", getRightLeg(), "Set", getSwaprightLeg());
		animationPlayer.SwapTexture("Set", getLeftArm(), "Set", getSwapleftLeg());
		animationPlayer.SwapTexture("Set", getTail(), "Set", getSwaptail());
		animationPlayer.SwapTexture("Set", getSkirtOne(), "Set", getSwapskirtOne());
		animationPlayer.SwapTexture("Set", getSkirtTwo(), "Set", getSwapskirtTwo());
			
	}
	
	public void SaveAvatar()
	{
		
		
		
	}
	
	
	
	
	
	
	
	

	
	
}
