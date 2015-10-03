using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChronoRound : MonoBehaviour {

	public GameObject timerText;
	public GameObject menu;

	public float cooldown;

	public bool canWinWithNoClick0;
	public bool canWinWithNoClick1;

	public static ChronoRound instance;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (createTimer (3));
		cooldown = 10;
	}
	
	// Update is called once per frame
	void Update () {

		if (cooldown > 0)
			cooldown -= Time.deltaTime;

		if(cooldown <= 0 && GameManager.isOver == false)
		{
			GameManager.players[0].hasClickOnThisRound = true;
			GameManager.players[1].hasClickOnThisRound = true;

			if (canWinWithNoClick0)
				GameManager.players[0].score++;
			if (canWinWithNoClick1)
				GameManager.players[1].score++;

			GuiManager.instance.updateGui();


			StartCoroutine(createTimer(3));
			cooldown = 10;
		}
	}

	public IEnumerator createTimer(float param)
	{
		while(param > -1)
		{
			StartCoroutine("afficherTimer", param);
			yield return new WaitForSeconds(1);

			GameManager.players[0].isReceivingSound = false;
			GameManager.players[1].isReceivingSound = false;

			param --;
			
			yield return true;
		}

		GameManager.players[0].hasClickOnThisRound = false;
		GameManager.players[1].hasClickOnThisRound = false;
		
		if(param == -1)
		{
			int alea = Random.Range(0,4);
			if(alea == 0)
			{
				GetComponent<GameManager>().checkPlayer(GameManager.players[0]);
				canWinWithNoClick0 = true;
				canWinWithNoClick1 = false;
			}
			else if(alea == 1)
			{
				GetComponent<GameManager>().checkPlayer(GameManager.players[1]);
				canWinWithNoClick0 = false;
				canWinWithNoClick1 = true;
			}
			else if (alea == 3)
			{
				GetComponent<GameManager>().checkPlayer(GameManager.players[0]);
				GetComponent<GameManager>().checkPlayer(GameManager.players[1]);
				
				canWinWithNoClick0 = false;
				canWinWithNoClick1 = false;
			}
			else
			{
				canWinWithNoClick0 = true;
				canWinWithNoClick1 = true;
			}

			yield break;
		}
	}
	
	IEnumerator afficherTimer(float param)
	{
		GameObject timer = Instantiate (timerText) as GameObject;
		timer.transform.SetParent (menu.transform);
		timer.GetComponent<Text>().text = ""+param;
		RectTransform rectTimer = (RectTransform)timer.transform;
		rectTimer.anchoredPosition = new Vector2 (0, -180);
		//timer.color = color;
		
		if(param == 0)
			timer.GetComponent<Text>().text = "GO !";
		
		float sizeY = 300;
		while(sizeY > 50)
		{
			sizeY -= 380*Time.deltaTime;
			rectTimer.sizeDelta = new Vector2(300, sizeY);
			yield return true;
		}
		
		Destroy (timer);
	}
}
