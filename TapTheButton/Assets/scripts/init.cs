using UnityEngine;
using System.Collections;

public class init : MonoBehaviour {

	public bool isHeadsetConnected = false;
	public GameObject helpButton;

	// Use this for initialization
	void Start () {
	
		isHeadsetConnected = DetectHeadset.Detect();

		Debug.Log ("Headset : " + isHeadsetConnected);
		helpButton.SetActive (isHeadsetConnected);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
