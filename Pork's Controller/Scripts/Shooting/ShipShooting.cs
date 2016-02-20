/*This script needs 2 Transforms, where you want bullets to spawn from. You can modify it to require as many as you wish.
 * Input can be changed at all time */


using UnityEngine;
using System.Collections;

public class ShipShooting : MonoBehaviour {


	public float speed; //Speed of the bullet!
	public float bulletLifetime; //How long will the bullet last untill its destroyed?
	public float shootTimer;

	public GameObject bullet; //The bullet



	public Transform shootLocation; //Where will the bullet appear?
	public Transform shootLocation2;






	private bool canShoot; //Can the player shoot now?


	// // // // // // // // 

	void Start () //Use this for initialization
	{

		canShoot = true; //At start, we can shoot

	}

	// // // // // // // // // // // // // // // // // // //

	void Update () //Method is called every second
	{
		

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
		GameObject bulletInstance2;

		bulletInstance = Instantiate (bullet, shootLocation.position, shootLocation.rotation) as GameObject; //We set bulletInstance to the bullet we spawned
		bulletInstance2 = Instantiate (bullet, shootLocation2.position, shootLocation2.rotation) as GameObject;

		Rigidbody bulletrb = bulletInstance.GetComponent<Rigidbody> (); //This assigns a rigidbody to the bullet
		Rigidbody bulletrb2 = bulletInstance2.GetComponent<Rigidbody>();

		bulletrb.velocity = new Vector3 (speed, 0, 0);
		bulletrb2.velocity = new Vector3 (speed, 0, 0);

		Destroy (bulletInstance, bulletLifetime);
		Destroy (bulletInstance2, bulletLifetime);

		yield return new WaitForSeconds (shootTimer); //Wait for X amount of seconds	

		canShoot = true; //Now, we can shoot again
	}

}
