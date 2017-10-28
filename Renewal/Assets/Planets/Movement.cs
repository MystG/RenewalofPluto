using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public float speed;
	// Update is called once per frame
	void Update () {
		Transform player = GameObject.Find("Sun").transform;
		transform.RotateAround(player.position, player.up, speed * Time.deltaTime);
	}
}
