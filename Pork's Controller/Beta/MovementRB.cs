/* This script requires a RigidBody 2D to work, and all of the parameters found here can be edited in the inspector*/
/*Input can be changed at all time*/
using UnityEngine;
using System.Collections;

public class MovementRB : MonoBehaviour {

	private Rigidbody2D playerRb; //The 2D Rigidbody of the player
	private GameObject player; //Player

	private float distToGround = 0.3f; //Distance to ground, to know if we are grounded
	private float startSpeed; //This saves the original speed at start
	private float distance; //Distance between Raycast hit and the player
	private bool canDoubleJump; //Now we can double jump.. can we?


	public float speed; //Speed
	public float crouchSpeed; //Crouch Speed
	public float sprintSpeed; //Sprint Speed

	public float jumpForce; //Jump Force
	public float doubleJumpForce; //Double Jump force

	public float gravity; //Gravity

	public bool doubleJumpAllowed; //Can we double jump at all?
	public bool sprintAllowed; //Are we even allowed sprinting?
	public bool crouchAllowed; //What about crouching?

	public bool facingLeft; //Are we facing left? (Unfortuntly, if the bools say you're facing right you're actually facing left, i accidently got the directions wrong)
	public bool facingRight; //Are we facing right?

	public bool isSprinting;//Are we sprinting?
	public bool isCrouching; //Are we crouching?

	public bool isGrounded; //Are we touching the ground?


	public Transform rayPosition; //Where will the raycast be spawned at?

	private Vector3 moveDirection; //Vector responsible for movement




	void Start () //Use this for initialization
	{
		startSpeed = speed; //Here we save our speed variable in another variable, so we can reset it later
		player = GameObject.FindWithTag ("Player"); //Here we ifnd the player
		playerRb = player.GetComponent<Rigidbody2D> (); //Here we find the RB2D attached to the player
	}

	void Update () //Update is called every frame
	{


		if (Input.GetKeyDown (KeyCode.LeftShift) && !isSprinting) { //If we hit this key and we aren't sprinting, we will sprint
			Sprint (); //Calls sprint method
		} 

		else if (Input.GetKeyDown (KeyCode.LeftControl) && !isCrouching) { //Or, if we hit this key and we ain't crouching
			Crouch (); //Calls crouch method
		} 

		if (Input.GetKeyDown (KeyCode.Q)) { //Or if we hit this key
			ResetSpeed (); //Calls the reset speed method
		}



		if (moveDirection.x < 0 && facingRight == false) { //have we got a negative value on our moveDirection vector?

			FlipRight (); //If so, call the FlipRight method

		} 

		else if (moveDirection.x > 0 && facingLeft == false) { //have we got a positive volue on our moveDirection Vector?
			FlipLeft (); //If so, call the FlipLeft method
		}

		// // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
		/* This part is a little bit more complicated. Since the Rigidbody does not have a isGrounded bool, we will
		 * cast a Ray that will allow us to see if there is any gameobject below us. It's not too hard to understand,
		 * try and catch up! */


		RaycastHit2D Hit = Physics2D.Raycast(rayPosition.position, Vector2.down); //Here, we declare our RaycastHit. The hit will help us determine what is below us



		if (Hit.collider != null) { //If we hit something

			distance = Mathf.Abs (Hit.point.y - transform.position.y); //This line basically calculates the distance between the player and the object

			if (distance < distToGround) { //If the distance is smaller than the minimal distance that is required to stand

				isGrounded = true; //We are grounded

			} else { //Or..
				isGrounded = false; //We are not grounded
			}
		
		} 	
			
		// // // // // // // // // // // // // // // // // // // // // // // // // // // // // //


		if (canDoubleJump == true && Input.GetKeyDown (KeyCode.Space)) {  //Is we hit space and we can double jump
			DoubleJump (); //Calls the double jump method
		}

		if (!isGrounded) { //When we are not grounded, we will apply a different type of gravity. We use this so the player looks a little bit more realistic, instead of a flying potato

			moveDirection.y -= (gravity * Time.deltaTime); //Artificial gravity
		}

		moveDirection.x = (Input.GetAxis ("Horizontal") * speed ); //Here, we set our RB's speed to be the Horizontal input * speed
		playerRb.velocity = moveDirection; //Moves the RB according to our Vector3



		if (isGrounded == true && Input.GetKeyDown (KeyCode.Space)) { //If we are grounded and we hit space

			Jump (); //Calls the jump method

		}


	
	
	}



	void Jump () //Jump method
	{
		moveDirection.y = jumpForce; //Adds a vertical force
		canDoubleJump = true; //Now we can double jump
	}

	void DoubleJump () //Double jump method
	{
		if (doubleJumpAllowed)
		{
		moveDirection.y = doubleJumpForce; //Adds a vertical force
		canDoubleJump = false; //Now we can't double jump
		}
	}

	private void FlipRight () //Flip right method. As i said, the methods have the wrong direction, so do the bools
	{
		facingRight = true; //Now, we are facing right
		facingLeft = false; //But we ain't facing left
		Vector3 theScale = transform.localScale; //Here we create a Vector3 that will hold the scale of our object
		theScale.x *= -1; //Here, we multiply the X axis by -1, so it flips. Note, if your scale is not 1, instead of "theScale.x *= -1;", you must use "theScale.x *= -scale;"
		transform.localScale = theScale; //This sets the scale to the vector, so it actually flips, otherwise theScale vector would do nothing
	}

	// // // // // // // // // // // // // // // // // // //

	private void FlipLeft () //Flip left method
	{
		facingLeft = true; //Now, we are facing left
		facingRight = false; //But we ain't facing right
		Vector3 theScale = transform.localScale; //These 3 lines are the same as the FlipRight method 
		theScale.x *= (theScale.x * -1); 
		transform.localScale = theScale;
	}


	void Sprint () //Sprint method
	{
		if (sprintAllowed)
		{
		speed = sprintSpeed; //Increases the speed
		isSprinting = true; //Now we are sprinting
		isCrouching = false; //Now we're not crouching
		}
	}

	void Crouch () //Crouch method
	{
		if (crouchAllowed)
		{
			speed = crouchSpeed; //Slows down the player
		isSprinting = false; //Now we ain't sprining
		isCrouching = true; //But we are crouching
		}
	}

	void ResetSpeed () //Reset Speed method
	{
		speed = startSpeed; //We reset speed to its original value
		isCrouching = false; //Now we ain't crouching
		isSprinting = false; //Nor sprinitng
	}


}
