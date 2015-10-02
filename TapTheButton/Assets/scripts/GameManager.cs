using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public int freq;
	public int cooldownMusic;
	public int timeToPush;
	public int scoreToWin;

	public static List<Player> players;


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


		if(Input.GetKeyDown("q"))
		{
			buttonPress(0);
		}

		if(Input.GetKeyDown("p"))
		{
			buttonPress(1);
		}
	}

	public void checkPlayer(Player p)
	{


		if (p.isReceivingSound && p.timerPush > timeToPush)
		{
			p.isReceivingSound = false;
		}

		if (p.timerTemporisation > cooldownMusic && Random.Range(0, freq) == 0)
		{
			Debug.Log ("PLAYER : " + p.id);

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
			players[nbPlayer].isReceivingSound = false;
		}
		else
		{
			players[1 - nbPlayer].score++;

			// AJOUT FONCTION BIP ERROR

		}
			
		checkVictoire (players[0]);
		checkVictoire (players[1]);

		GuiManager.instance.updateGui ();
	}

	public void checkVictoire(Player p)
	{
		if (players[p.id].score >= scoreToWin)
		{
			GuiManager.instance.displayVictory(p);
		}
	}


}
