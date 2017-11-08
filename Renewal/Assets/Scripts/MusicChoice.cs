using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicChoice : MonoBehaviour {

	public static MusicChoice instance = null;
	private AudioSource audioSource;
	public AudioClip [] audioFiles;
	
	private bool SongLoaded;
	
	void Awake(){
		if (instance == null){
			instance = this;
		}else if (instance != null && instance != this){
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
	}
	void OnEnable(){
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	void OnDisable(){
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		AudioClip thisLevelMusic = audioFiles[scene.buildIndex];
		if(thisLevelMusic){
			audioSource.clip = thisLevelMusic;
			audioSource.loop = true;
			audioSource.Play();
		}
	}
}
