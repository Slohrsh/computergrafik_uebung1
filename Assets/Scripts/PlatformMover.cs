using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{

	int speed = 10;
	float min;
	float max;

	public GameObject player;

	// Use this for initialization
	void Start ()
	{
		min = transform.position.x;
		max = transform.position.x + 7;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = new Vector3 (Mathf.PingPong (Time.time * 2, max - min) + min, transform.position.y, transform.position.z);
	}
		


}
