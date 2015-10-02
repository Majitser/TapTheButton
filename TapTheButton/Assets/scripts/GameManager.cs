﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public int freq;
	public int cooldownMusic;
	public int timeToPush;

	public static List<Player> players;

	public GameObject soundManager;


	// Use this for initialization
	void Start () {
	
		players = new List<Player> ();

		players.Add(new Player (0));
		players.Add(new Player (1));

		//Debug.Log ("Test : " + players [0].id);
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
			
			p.timerTemporisation = 0;
			p.timerPush = 0;
			p.isReceivingSound = true;
			GetComponent<PlaySounds>().takeSound(p);
		}

		p.timerTemporisation++;
		p.timerPush++;
	}

	public void buttonPress(int nbPlayer)
	{
		Debug.Log ("nbPlayer : " + nbPlayer + " / " + players[nbPlayer].isReceivingSound);


		if (players[nbPlayer].isReceivingSound)
		{
			players[nbPlayer].score++;
		}
		else
		{
			players[1 - nbPlayer].score++;
		}

		GuiManager.instance.updateGui ();
	}


}