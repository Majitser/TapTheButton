using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public int freq = 10;
	public int cooldownMusic = 100;
	public int timeToPush = 20;

	public List<Player> players;


	// Use this for initialization
	void Start () {
	
		players = new List<Player> ();

		players.Add(new Player (0));
		players.Add(new Player (1));
	}


	void FixedUpdate() {

		// FOR PLAYERS
		checkPlayer (players [0]);
		checkPlayer (players [1]);
	}

	public void checkPlayer(Player p)
	{
		if (p.isReceivingSound && p.timerPush > timeToPush)
		{
			p.isReceivingSound = false;
		}

		if (p.timerTemporisation > cooldownMusic && Random.Range(0, freq) == 0)
		{
			Debug.Log("Fire Sound for Player " + p.id);
			
			p.timerTemporisation = 0;
			p.timerPush = 0;
			p.isReceivingSound = true;
		}

		p.timerTemporisation++;
		p.timerPush++;
	}

	public void buttonPress()
	{

	}


}
