using UnityEngine;
using System.Collections;

public class init : MonoBehaviour {

	public bool isHeadsetConnected = false;
	public GameObject helpButton;

	public GameObject tuto;

	// Use this for initialization
	void Start () {
	
		//isHeadsetConnected = DetectHeadset.Detect();
		//Debug.Log ("Headset : " + isHeadsetConnected);
		//helpButton.SetActive (isHeadsetConnected);

		int firstUse = PlayerPrefs.GetInt ("firstUse");

		if (firstUse != null && firstUse != 0)
		{

		}
		else
		{
			MenuManager.instance.GoToMenu(tuto);
			PlayerPrefs.SetInt ("firstUse", 1);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
