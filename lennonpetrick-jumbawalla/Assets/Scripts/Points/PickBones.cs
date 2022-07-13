using UnityEngine;
using System.Collections;

public class PickBones : MonoBehaviour {
	
	public Animator anim;
	public TextMesh numberBones;
	public int amountBones;
	public AudioClip coin;
	void OnTriggerEnter( Collider other)
	{
		if(other.name.Equals("Jumba"))
		{
			MoneyTeeth.addTeeth( (float) amountBones );
			numberBones.text = "+" + amountBones.ToString();
			anim.SetTrigger("pick");
			Director.sharedDirector().playEffect(coin);
		}
	}

}
