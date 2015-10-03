using UnityEngine;
using System.Collections;

public class Player {

	public int timerTemporisation;
	public int timerPush;
	public int id;

	public int score;
	public bool isReceivingSound;

	public bool hasClickOnThisRound;


	public Player(int id)
	{
		this.timerTemporisation = 0;
		this.timerPush = 0;
		this.id = id;
		this.isReceivingSound = false;
		this.score = 0;
		this.hasClickOnThisRound = false;
	}


}
