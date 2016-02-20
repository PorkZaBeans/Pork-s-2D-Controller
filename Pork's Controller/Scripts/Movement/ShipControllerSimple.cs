//Like the ShipControler Script, this script is also optimized for top-down space shooters. This one on the other hand, only gives you controll of the Y axis.
using UnityEngine;
using System.Collections;
public class ShipControllerSimple : MonoBehaviour {


	private Vector3 moveDirection = Vector3.zero; //Here, we declare a Vector3 that will take care of moving our player

	public float speed; //Here, we declare a float called speed that will determine the player's speed


	private CharacterController playerController; //Here we declare a Character Controller

	 
	void Start () { //Use this for initialization

		moveDirection = new Vector3 (0, 0, 0); //Here, we say that moveDirection is a new Vector3
		playerController = gameObject.GetComponent<CharacterController> (); //Here, we declare playerController. It will get the CharacterController in this gameobject

	}



	// Update is called once per frame
	void Update () { 

		moveDirection.y = (Input.GetAxis ("Vertical")); //Here, we set the Y axis to be the vertical Input, using Input.GetAxis. Feel free to use any kind of input
		moveDirection.x = (0); //
		playerController.Move (moveDirection * speed * Time.deltaTime);
	}
}