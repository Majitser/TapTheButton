using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public int freq;
	public int cooldownMusic;
	public int timeToPush;
	public int scoreToWin;

	public static bool isOver;
	public static List<Player> players;

    public Image barBlue;
    public Image barGreen;

    private float aleaPlayer1;
	private float aleaPlayer2;

	public static GameManager instance;



	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
		players = new List<Player> ();

		players.Add(new Player (0));
		players.Add(new Player (1));

		isOver = false;
	}


	void FixedUpdate() {

		// FOR PLAYERS
		//checkPlayer (players [0]);
		//checkPlayer (players [1]);
	}

	void Update()
	{
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
		if (isOver)
			return;

		Debug.Log ("ok");
		/*
		if (p.isReceivingSound && p.timerPush > timeToPush)
		{
			p.isReceivingSound = false;
		}
		*/

		/*if (p.timerTemporisation > cooldownMusic && Random.Range(0, freq) == 0)
		{*/
			//Debug.Log ("PLAYER : " + p.id);

			//p.timerTemporisation = 0;
			//p.timerPush = 0;
			p.isReceivingSound = true;
			GetComponent<PlaySounds>().takeSound(p);
	}

	public void buttonPress(int nbPlayer)
	{
		if (isOver || players[nbPlayer].hasClickOnThisRound)
			return;


		if (nbPlayer == 0)
			ChronoRound.instance.canWinWithNoClick0 = false;
		else if (nbPlayer == 1)
			ChronoRound.instance.canWinWithNoClick1 = false;

        int oldScore = players[nbPlayer].score;
        if (players[1 - nbPlayer].isReceivingSound)
        {
            players[nbPlayer].score++;
            StartCoroutine(fill_score(nbPlayer, oldScore));
        }
        else
        {
            oldScore = players[1 - nbPlayer].score;
            players[1 - nbPlayer].score++;
            GetComponent<PlaySounds>().wrongAnswer(nbPlayer);
            StartCoroutine(fill_score(1 - nbPlayer, oldScore));
        }

		players [nbPlayer].hasClickOnThisRound = true;
		checkVictoire (players[0]);
		checkVictoire (players[1]);

        GuiManager.instance.updateGui ();
	}

    public IEnumerator fill_score(int nbPlayer, int oldScore)
    {
        float rate = 0;
        while (rate < 1.0)
        {
            if (nbPlayer == 0)
                barBlue.fillAmount = Mathf.Lerp((float)oldScore / 10, (float)players[nbPlayer].score / 10, rate);
            else if (nbPlayer == 1)
                barGreen.fillAmount = Mathf.Lerp((float)oldScore / 10, (float)players[nbPlayer].score / 10, rate);
            rate += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

	public void checkVictoire(Player p)
	{
		if (players[p.id].score >= scoreToWin)
		{
			isOver = true;
			GuiManager.instance.displayVictory(p);
		}
	}


}
