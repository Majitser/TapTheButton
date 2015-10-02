using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour {
	
	public Text text0;
	public Text text1;

	public Button button0;
	public Button button1;

	public static GuiManager instance;


	// Use this for initialization
	void Awake () {
	
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateGui()
	{
		text0.text = GameManager.players [0].score.ToString();
		text1.text = GameManager.players [1].score.ToString();
	}
}
