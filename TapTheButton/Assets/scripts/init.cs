using UnityEngine;
using System.Collections;

public class init : MonoBehaviour {

	public bool isHeadsetConnected = false;

	// Use this for initialization
	void Start () {
	
		isHeadsetConnected = DetectHeadset.Detect();

		Debug.Log ("Headset : " + isHeadsetConnected);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
