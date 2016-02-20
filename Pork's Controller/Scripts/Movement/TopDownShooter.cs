/*This script is optimized to work for 2D top-down shooters*/
/*For this script to work, it requires a CharacterController*/	
using UnityEngine;
using System.Collections;
public class TopDownShooter : MonoBehaviour {


	private Vector3 moveDirection = Vector3.zero; //Here, we declare a new Vector3, wich will take care of moving the player!

	public float gravity; //Gravity applied to the player
	public float speed; //This float can be set by the player in the inspector. It is the speed that our player will have!

	private CharacterController playerController; //Here, we declare the CharacterController component!


	void Start () { //Use this for initialization

		moveDirection = new Vector3 (0, 0, 0); //Here, we say that moveDirection is a new Vector3!
		playerController = gameObject.GetComponent<CharacterController> (); //Here, we set playerController to be the CharacterController component in the player. Keep in ming the script must be attached in the same gameobject, otherwise changes to this script is necessary

	}



	// Update is called once per frame
	void Update () { 


		moveDirection.z = (Input.GetAxis ("Vertical")); //Here, we basically controll the player on the Z axis, by using Input.GetAxis. You can, of course, change this to any king of input
		moveDirection.x = (Input.GetAxis("Horizontal")); //Here, we do the same thing, but on the X axis
		moveDirection.y -= (gravity * Time.deltaTime);

		playerController.Move (moveDirection * speed * Time.deltaTime); //Here, we move our player. We multiply it by speed so it goes by the speed we want to, and we multiply it by Time.deltatime to make it time-based, otherside it would just move way too fast






	}
}
