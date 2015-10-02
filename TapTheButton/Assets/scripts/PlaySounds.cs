using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaySounds : MonoBehaviour {

	public List<AudioClip> soundsList = new List<AudioClip>();
	public List<AudioSource> sourcePlayer = new List<AudioSource>();
	public AudioSource sourcePlayer1;
	public AudioSource sourcePlayer2;
	public AudioClip wrongAnswerAudio;

	// Use this for initialization
	void Start () {
		sourcePlayer.Add (sourcePlayer1);
		sourcePlayer.Add (sourcePlayer2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void takeSound(Player p)
	{
		int aleaSound = Random.Range (0, soundsList.Count);
		sourcePlayer [p.id].PlayOneShot (soundsList[aleaSound], 1f);
	}

	public void wrongAnswer(Player p)
	{
		sourcePlayer [p.id].PlayOneShot (wrongAnswerAudio, 1f);
	}
}
