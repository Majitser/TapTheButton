using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using System;

public class pubManager : MonoBehaviour {

	public static pubManager instance = null;

	public GameObject starButton;

	// Use this for initialization
	void Awake () {

		if (instance == null)
			instance = this;
		else
		{
			Destroy(this.gameObject);
			Destroy(this);
		}

		DontDestroyOnLoad(transform.gameObject);

#if UNITY_ANDROID
		Advertisement.Initialize ("1005607");
#elif UNITY_IPHONE
		Advertisement.Initialize ("1005608");
#endif

	}

	// Update is called once per frame
	void Update () {
	
		if(Advertisement.isReady())
		{ 
			if (starButton != null && starButton.activeSelf == false)
				starButton.SetActive(true);
		}
	}

	public void showAdd()
	{
		Advertisement.Show(); 
	}

	public void fbShare()
	{
		string facebookshare = "https://www.facebook.com/sharer/sharer.php?u=" + Uri.EscapeUriString("http://www.google.com");
		Application.OpenURL(facebookshare);
	}
}
