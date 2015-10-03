using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour 
{

	[SerializeField]
	private string m_animationPropertyName;
	
	[SerializeField]
	private GameObject m_initialScreen;
	
	[SerializeField]
	private GameObject objErrorMdp;
	
	[SerializeField]
	private List<GameObject> m_navigationHistory;
	
	
	public string sceneToLoad;
	
	public AsyncOperation async;
	private bool fading = false;
	private bool preloading = false;
	
	private GameObject OppeningObj;

	public GameObject MainCanvas;
	public static MenuManager instance;
	
	public void customScene(){
		
		Application.LoadLevel(5);
		
	}
	
	public void PistacheFall(){
		
		Application.LoadLevel(6);
		
	}
	
	public void testVideo(){
		Application.LoadLevel(11);
	}
	
	
	// Update is called once per frame
	void Update () {
		
		if (async != null)
		{
			//Debug.Log ("async : " + async.isDone);
			//Debug.Log ("async : " + async.progress);
			//Debug.Log ("preloading : " + preloading);
		}
		
		if (async != null && (async.isDone || async.progress >= 0.9f) && preloading == false)
		{
			Debug.Log("done loading");
			SwitchScene();
		}
	}
	
	public IEnumerator launcher()
	{
		StartCoroutine(LoadScene());
		
		Debug.Log ("1");
		
		yield return new WaitForSeconds(0.1f);
		
		Debug.Log ("2");
		
		// Load MainScene
		Resources.UnloadUnusedAssets();
		//Application.LoadLevel(sceneToLoad);
		
		
	}
	
	IEnumerator LoadScene()
	{
		if (sceneToLoad == "")
			yield break;
		
		async = Application.LoadLevelAsync(sceneToLoad);
		async.allowSceneActivation = true;
		
		Debug.Log("Loading " + sceneToLoad);
		
		yield return async;
	}
	
	IEnumerator PreLoader()
	{
		if (sceneToLoad == "")
			yield break;
		
		async = Application.LoadLevelAsync(sceneToLoad);
		async.allowSceneActivation = false;
		
		Debug.Log("Pre loading " + sceneToLoad);
		
		yield return async;
	}
	
	private void SwitchScene()
	{
		Debug.Log("switching");
		
		if (async != null)
			async.allowSceneActivation = true;
	}
	
	
	public void testIfGoToMenu(GameObject target)
	{
		GameObject obj = m_navigationHistory[m_navigationHistory.Count - 1];
		
		Debug.Log("OBJ : " + obj.transform.FindChild("CustomFormMenu").FindChild("InputField").GetComponent<InputField>().text);
		
		if (obj.transform.FindChild("CustomFormMenu").FindChild("InputField").GetComponent<InputField>().text.Equals("mdp"))
		{
			// activate ParentOptions
			
			
			if (target == null)
			{
				return;
			}
			
			if (m_navigationHistory.Count > 0)
			{
				Animate(m_navigationHistory[m_navigationHistory.Count - 1], false);
			}
			
			m_navigationHistory.Add(target);
			Animate(target, true);
		}
		else
		{
			
			if (objErrorMdp == null)
			{
				return;
			}
			
			if (m_navigationHistory.Count > 0)
			{
				Animate(m_navigationHistory[m_navigationHistory.Count - 1], false);
			}
			
			m_navigationHistory.Add(objErrorMdp);
			Animate(objErrorMdp, true);
		}
	}
	
	public void GoBack()
	{
		if (m_navigationHistory.Count > 1)
		{
			int index = m_navigationHistory.Count - 1;
			
			Animate(m_navigationHistory[index - 1], true);
			
			GameObject target = m_navigationHistory[index];
			m_navigationHistory.RemoveAt(index);
			Animate(target, false);
			
		}
	}
	
	public void CloseCurrent()
	{
		if (m_navigationHistory.Count > 1)
		{
			int index = m_navigationHistory.Count - 1;
			GameObject target = m_navigationHistory[index];
			m_navigationHistory.RemoveAt(index);
			Animate(target, false);
		}
	}
	
	public void GoToMenuWithoutClosingPrevious(GameObject target)
	{
		if (target == null)
		{
			return;
		}
		
		m_navigationHistory.Add(target);
		Animate(target, true);
	}
	
	public void GoToMenu(GameObject target)
	{
		if (target == null)
		{
			return;
		}
		
		if (m_navigationHistory.Count > 0)
		{
			Animate(m_navigationHistory[m_navigationHistory.Count - 1], false);
		}
		
		m_navigationHistory.Add(target);
		Animate(target, true);
	}
	
	private void Animate(GameObject target, bool direction)
	{
		//Debug.Log ("obj : " + target.name + " / dir : " + direction);
		
		//if (direction)
		//	OppeningObj = target;
		
		if (target == null)
		{
			return;
		}
		
		target.SetActive(true);
		
		Canvas canvasComponent = target.GetComponent<Canvas>();
		if (canvasComponent != null)
		{
			canvasComponent.overrideSorting = true;
			canvasComponent.sortingOrder = m_navigationHistory.Count +1;
		}
		
		Animator animatorComponent = target.GetComponent<Animator>();
		if (animatorComponent != null)
		{
			animatorComponent.SetBool(m_animationPropertyName, direction);
			
			//if (direction == false)
			//	StartCoroutine(setFalseAfterTime(target));
		}
	}
	
	public IEnumerator setFalseAfterTime(GameObject g)
	{
		yield return new WaitForSeconds(0.3f);
		
		if (g != OppeningObj)
			g.SetActive(false);
	}
	
	private void Awake()
	{
		instance = this;
		m_navigationHistory = new List<GameObject>{m_initialScreen};
	}
	
	public void preLoadScene(string name)
	{
		sceneToLoad = name;
		preloading = true;
		StartCoroutine(PreLoader());
	}
	
	public void loadLevelAsync(string name)
	{
		Application.LoadLevelAsync(name);
	}
	
	public IEnumerator loadSceneWithFade(string name)
	{
		
		//Debug.Log("LOADING2 : " + name);
		
		async = Application.LoadLevelAsync(name);
		async.allowSceneActivation = false;
		preloading = true;
		
		//Debug.Log("Pre loading " + name);
		
		yield return new WaitForSeconds(1f);
		
		async.allowSceneActivation = true;
		
		yield return async;
	}
	
	public void goToScene(string name)
	{
		if (preloading == false)
		{
			//if (SceneFadeInOut.instance)
			//	SceneFadeInOut.instance.sceneExiting = true;
			
			StartCoroutine(loadSceneWithFade(name));
		}
	}
	
	public void goToSceneNumber(int number)
	{
		Application.LoadLevel(number);
	}
}

