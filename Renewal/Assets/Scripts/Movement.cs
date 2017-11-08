using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {
	public float speed;
	public float speed2;
	// Update is called once per frame
	void Update () {
		Transform player = GameObject.Find("Sun").transform;
		Transform current = transform;
		transform.RotateAround(player.position, player.up, speed * Time.deltaTime);
		transform.RotateAround(current.position, current.up, speed2 *Time.deltaTime);
	}
}
