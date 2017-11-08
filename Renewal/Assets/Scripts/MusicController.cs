using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
	
	public static bool mcExists;
	
	public AudioSource[] musicLevel;
	
	public int currentTrack;
	
	public bool musicCanPlay;
	// Use this for initialization
	void Start () {
		if(!mcExists){
			mcExists = true;
			DontDestroyOnLoad(transform.gameObject);
		}else {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(musicCanPlay){
			if(!musicLevel[currentTrack].isPlaying){
				musicLevel[currentTrack].Play();
			}
		} else{
			musicLevel[currentTrack].Stop();
		}
	}
	public void SwitchTrack(int newTrack){
		musicLevel[currentTrack].Stop();
		currentTrack = newTrack;
		musicLevel[currentTrack].Play();
	}
}
