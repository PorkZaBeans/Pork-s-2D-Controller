/*This script requires a CharacterController to work, that must be attached to this gameobject, or you need to modify the script*/
/*Input can be changed at all time*/


using UnityEngine;
using System.Collections;
public class TwoDController : MonoBehaviour {


	public Vector3 moveDirection = Vector3.zero; //Created a Vector3 that will take care of movement


	public float speed = 5.0f; //Speed of the player
	public float gravity = 8.0f; //Gravity applied on the plauer
	public float jumpForce = 6.0f; //Force that the player jumps with
	public float doubleJumpForce = 4.5f; //Force player has when double jumping
	public float crouchSpeed = 2.5f; //Speed when crouching
	public float sprintSpeed = 6.25f; //Speed when sprinting
	public Vector3 mousePos;


	public bool doubleJumpAllowed; //Can the player double jump?
	public bool crouchAllowed; //Can the player crouch?
	public bool sprintAllowed; //Can the player sprint?
	public bool isCrouching; //Is the player crouching?
	public bool isSprinting; //Is the player sprinting?



	private float normalSpeed = 5.0f; //This float takes the speed value on Start and saves it to reset speed after crouch and sprint



	public bool facingLeft; //Facing left and facing right are wrong. I accidently used the wrong one. I guess you can either fix it, ignore it, or get used to is (i'm sorry)
	public bool facingRight; //The voids are also messed up, left = right and right = left
	private bool canDoubleJump; //Can the player do a double jump right now?



	private CharacterController playerController; //This is the controller you need to add in your player!


	void Start () { //Upon start, do this..

		sprintAllowed = true; //For Debug purposes, these tree bools are true, feel free to customize
		crouchAllowed = true;
		doubleJumpAllowed = true;



		normalSpeed = speed; //This gets the starting value of speed, so we can reset it later



		moveDirection = new Vector3 ((Input.GetAxis ("Horizontal")), 0, 0); //Create a vector 3 that is controlled with user input!



		playerController = gameObject.GetComponent<CharacterController> (); //This sets the playercontroller to the charactercontroller you must have in your player!

	}



	// Update is called once per frame
	void Update () {
		

		if (Input.GetKeyUp (KeyCode.LeftShift) && isSprinting) {  //Are we sprinting and did we let go the sprinting key?
			ResetSpeed (); 
		} 

		else if (Input.GetKeyUp (KeyCode.LeftControl) && isCrouching) { //Are we crouching and ded we let go the crouching key?
			ResetSpeed (); 
		}




		if (moveDirection.x < 0 && facingLeft == false) { //have we got a negative value on our moveDirection vector?

			FlipLeft (); //If so, call the FlipRight method

		} 

		else if (moveDirection.x > 0 && facingRight == false) { //have we got a positive volue on our moveDirection Vector?
			FlipRight (); //If so, call the FlipLeft method
		}
	


		if (Input.GetKey (KeyCode.Q)) {  //The key that is currently set is for debugging. This key resets the crouch/sprinting
			ResetSpeed ();
		}



		if (playerController.isGrounded) { //If the controller is grounded..
			if (Input.GetKey (KeyCode.LeftControl) && crouchAllowed == true) { //And we have LeftControl pressed, we will crouch
				speed = crouchSpeed;
				isCrouching = true;
			}


			else if (Input.GetKey (KeyCode.LeftShift) && sprintAllowed == true) { //Or, if we have LeftShift pressed, we will sprint! Such fast! Much wow!
				speed = sprintSpeed;
				isSprinting = true;
			}
		} 




		if (!playerController.isGrounded) { //If the player isn't grounded..
			if (canDoubleJump == true && doubleJumpAllowed == true) { //Can we double jump?
				if (Input.GetKeyDown(KeyCode.Space)) { //Checks when player hits space
					DoubleJump (); //Calls the DoubleJump method
				}
			}
		}





		moveDirection.x = (Input.GetAxis ("Horizontal")); //So we move our player on the X axis, we use input.getaxis! You can change this to any input form you want
		playerController.Move (moveDirection * speed * Time.deltaTime); //This uses the built-in Move method in the Character Controller. This moves our player based on moveDirection Vector

		moveDirection.y -= (gravity * Time.deltaTime); //This applies gravity, subtracting it on the Z axis, and we use Time.deltatime to make it time based





		if (Input.GetKeyDown (KeyCode.Space)) { //Did we press space?

			if (playerController.isGrounded) { //And is our controller grounded?
				Jump (); //If so, we call the Jump method
			} 

		}
	}



	public void Jump () //This is the jump method
	{
		moveDirection.y = (jumpForce); //In here, we set the Y position to our jumpforce float! This will add a force to our player making it go upwards
		canDoubleJump = true; //Now, we can doublejump
	}





	public void DoubleJump () //This is the double jump method
	{
		moveDirection.y = doubleJumpForce; //This, like in the Jump method, sets the Y position to the DoubleJumpForcea, wich, by default, is smaller than the jump force
		canDoubleJump = false; //Now, we can't doublejump! If you want to make a game where you can double jump forever check out the FlappyStyle script, also included in this package!
	}






	public void ResetSpeed () //If you press a certain key, this method will be called. This method sets speed to its original value
	{
		speed = normalSpeed;
		isSprinting = false;
		isCrouching = false;
	}





	private void FlipLeft () //Flip left method
	{
		facingLeft = true; //Now, we are facing right
		facingRight = false; //But we ain't facing left
		Vector3 theScale = transform.localScale; //Here we create a Vector3 that will hold the scale of our object
		theScale.x *= -1; //Here, we multiply the X axis by -1, so it flips. Note, if your scale is not 1, instead of "theScale.x *= -1;", you must use "theScale.x *= -scale;"
		transform.localScale = theScale; //This sets the scale to the vector, so it actually flips, otherwise theScale vector would do nothing
	}





	private void FlipRight () //Flip right method
	{
		facingRight = true; //Now, we are facing left
		facingLeft = false; //But we ain't facing right
		Vector3 theScale = transform.localScale; //These 3 lines are the same as the FlipRight method 
		theScale.x *= (theScale.x * -1);
		transform.localScale = theScale;
	}



}

