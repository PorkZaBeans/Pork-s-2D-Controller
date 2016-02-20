using UnityEngine;
using System.Collections;

public class BasicShooting : MonoBehaviour {


	public float speed; //Speed of the bullet!

	// // // // // // // // 

	public GameObject bullet; //The bullet

	// // // // // // // // 

	public Transform shootLocation; //Where will the bullet appear?

	// // // // // // // // 

	public GameObject player; //The player, that will be needed 

	// // // // // // // // 

	private bool canShoot; //Can the player shoot now?
	private Vector3 scale = Vector3.zero; //A new Vector3 called scale

	// // // // // // // // 

	void Start () //Use this for initialization
	{
		
		canShoot = true; //At start, we can shoot

		// // // // // // // // // // // // // // // // // // //

		scale = new Vector3 (player.transform.localScale.x, player.transform.localScale.y, player.transform.localScale.z ); //We set the Vector3 scale to the player's scale. We will use this to determine the side where the bullet shoots
	}

	// // // // // // // // // // // // // // // // // // //

	void Update () //Method is called every second
	{
		scale.x = (player.transform.localScale.x); //We use this in Update to allways have the player scale updated
		// // // // // // // // // // // // // // // // // // //
		if (Input.GetKeyDown (KeyCode.Mouse0) && (canShoot == true)) //Did you press mouse, and can we shoot?
		{
			StartCoroutine (Shoot ()); //If so, let's shoot
		}

	}


	public IEnumerator Shoot () //Shoot method
	{
		
		canShoot = false; //Now, we can't shoot

		// // // // // // // // // // // // // // // // // // //

		GameObject bulletInstance; //This represents the bullet we will instantiate

		bulletInstance = Instantiate (bullet, shootLocation.position, shootLocation.rotation) as GameObject; //We set bulletInstance to the bullet we spawned
		Rigidbody bulletrb = bulletInstance.GetComponent<Rigidbody> (); //This assigns a rigidbody to the bullet

		// // // // // // // // // // // // // // // // // // //

		if (scale.x < 0) { //If the scale of the player is < 0
			bulletrb.velocity = new Vector3 (speed, 0, 0); //Make the bullet go in that direcetion
		} 

		// // // // // // // // // // // // // // // // // // //

		else if (scale.x > 0) { //Or, if the scale is > 0
			bulletrb.velocity = new Vector3 (-speed, 0, 0); //Make the bullet go in the other direction
		}

		// // // // // // // // // // // // // // // // // // //

		yield return new WaitForSeconds (0.3f); //Wait for X amount of seconds	

		canShoot = true; //Now, we can shoot again
	}
		
}
