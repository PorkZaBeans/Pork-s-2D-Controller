using UnityEngine;
using System.Collections;
public class FlappyStyle : MonoBehaviour {


	private Vector3 moveDirection = Vector3.zero;


	public float speed = 0.5f;
	public float gravity = 8.0f;
	public float jumpForce = 1.0f;



	private CharacterController playerController;


	void Start () {

		moveDirection = new Vector3 (0, 0, 0);
		moveDirection = transform.TransformDirection (moveDirection);
		playerController = gameObject.GetComponent<CharacterController> ();

	}



	// Update is called once per frame
	void Update () {

	

		moveDirection.x = (speed);
		playerController.Move (moveDirection * speed * Time.deltaTime);

		moveDirection.y -= (gravity * Time.deltaTime);


		if (Input.GetKeyDown (KeyCode.Space)) {


				Jump ();
			 

		}
	}

	public void Jump ()
	{
		moveDirection.y = (jumpForce);

	}








}
