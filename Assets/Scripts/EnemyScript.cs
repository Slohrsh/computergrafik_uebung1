using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

	public GameObject player;
	int moveSpeed = 4;


	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.LookAt (player.transform);
		transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
	
	}
}
