using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitch : MonoBehaviour {

	private MusicController MC;
	
	public int newTrack;

	public bool switchOnStart;
	// Use this for initialization
	void Start () {	
			MC = FindObjectOfType<MusicController>();
			if(switchOnStart){
				MC.SwitchTrack(newTrack);
				gameObject.SetActive(false); 	 	
			}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D C){
		if(C.gameObject.name == "Player"){
			MC.SwitchTrack(newTrack);
			gameObject.SetActive(false);
		}
	}
}
