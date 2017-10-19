using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

	private float currentSpeed;
	public float normalSpeed;
	public float sprintSpeed;

	private Rigidbody playerRigidBody;

	public GameObject projectilePrefab;
	private GameObject projectile;
	private Rigidbody projectileRigidBody;
	private bool playerFloats = false;

	private Vector3 jumpVector;
	private Vector3 playerStartPosition;
	private Vector3 enemyStartPosition;



	void Start ()
	{
		currentSpeed = normalSpeed;
		jumpVector = new Vector3 (0, 150, 0);
		playerStartPosition = new Vector3 (-3, 3, 3);
		enemyStartPosition = new Vector3 (0, 3, -3);
			
		playerRigidBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow) ||
		    Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow)) {
			playerMove ();
		}

		if (Input.GetKey (KeyCode.Space) && playerFloats == false) {
			playerJump ();
		}

		if (Input.GetKey (KeyCode.LeftShift)) {
			playerSprint ();
		}

		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			playerStopSprint ();
		}

		if (Input.GetKeyUp (KeyCode.LeftCommand)) {
			playerShoot ();
		}
	}

	private void playerMove ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		transform.rotation = Quaternion.LookRotation (movement);


		playerRigidBody.AddForce (movement * currentSpeed);
	}

	private void playerJump ()
	{
		playerRigidBody.AddForce (jumpVector);
		playerFloats = true;
	}

	private void playerSprint ()
	{
		currentSpeed = sprintSpeed;
	}

	private void playerStopSprint ()
	{
		currentSpeed = normalSpeed;
	}

	private void playerShoot ()
	{
		projectile = Instantiate (projectilePrefab, transform.position, transform.rotation);
		projectile.SetActive (true);
		projectileRigidBody = projectile.GetComponent<Rigidbody> ();
		projectileRigidBody.AddForce (transform.right * 150000.0f * Time.deltaTime);

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("PickUp")) {
			other.gameObject.SetActive (false);
		} else if (other.gameObject.CompareTag ("DeathPlane")) {
			transform.position = playerStartPosition;
		} else if (other.gameObject.CompareTag ("Enemy")) {
			transform.position = playerStartPosition;
			other.transform.position = enemyStartPosition;
		} else if (other.gameObject.CompareTag ("GoalPlatform")) {
			playerRigidBody.AddForce (new Vector3 (0, 10000, 0));
		} 
	}

	void OnCollisionStay (Collision other)
	{
		Debug.Log (playerFloats);
		if (other.gameObject.CompareTag ("Floor") || other.gameObject.CompareTag ("MovingPlatform")) {
			playerFloats = false;
		}
	}
}
