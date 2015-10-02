using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaySounds : MonoBehaviour {

	public List<AudioClip> soundsList = new List<AudioClip>();
	public AudioSource sourcePlayer1;
	public AudioSource sourcePlayer2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void takeSound(int idPlayer)
	{
		int aleaSound = Random.Range (0, soundsList.Count);
		if (idPlayer == 0)
			sourcePlayer1.PlayOneShot (soundsList[aleaSound], 1f);
		else 
			sourcePlayer2.PlayOneShot (soundsList[aleaSound], 1f);
	}
}
