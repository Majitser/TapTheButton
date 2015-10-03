using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChronoRound : MonoBehaviour {

	public GameObject timerText;
	public GameObject menu;

	public float cooldown;

	// Use this for initialization
	void Start () {
		StartCoroutine (createTimer (3));
		cooldown = 10;
	}
	
	// Update is called once per frame
	void Update () {
		if (cooldown > 0)
			cooldown -= Time.deltaTime;

		if(cooldown <= 0)
		{
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
			param --;
			
			yield return true;
		}
		
		if(param == -1)
		{
			int alea = Random.Range(0,3);
			if(alea != 2)
				GetComponent<GameManager>().checkPlayer(GameManager.players[alea]);
			else 
			{
				GetComponent<GameManager>().checkPlayer(GameManager.players[0]);
				GetComponent<GameManager>().checkPlayer(GameManager.players[1]);
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
