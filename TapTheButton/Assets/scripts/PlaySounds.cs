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

	public void takeSound(Player p)
	{
		int aleaSound = Random.Range (0, soundsList.Count);
		if (p.id == 0)
		{
			sourcePlayer1.PlayOneShot(soundsList[aleaSound], 1);
			Debug.Log (sourcePlayer1);
			//sourcePlayer1.clip = soundsList[aleaSound];
			//sourcePlayer1.Play();
		}
		else 
		{
			sourcePlayer2.PlayOneShot(soundsList[aleaSound], 1);
			//sourcePlayer2.clip = soundsList[aleaSound];
			//sourcePlayer2.Play();
		}
	}
}
