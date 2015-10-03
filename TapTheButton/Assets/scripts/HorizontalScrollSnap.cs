using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ScrollRect))]
public class HorizontalScrollSnap : MonoBehaviour
{
	[Tooltip("the container the screens or pages belong to")]
	public Transform ScreensContainer;
	[Tooltip("how many screens or pages are there within the content")]
	public int Screens = 1;
	[Tooltip("which screen or page to start on (starting at 1 for you designers)")]
	public int StartingScreen = 1;
	
	private List<Vector3> _positions;
	private ScrollRect _scroll_rect;
	private Vector3 _lerp_target;
	private bool _lerp;
	private bool _lerpProgress;
	private float _lerp_targetProgress;
	public int _actualPosition;
	
	public GameObject rightBtn;
	public GameObject leftBtn;
	public Scrollbar progressBar;
	
	// Use this for initialization
	void Start()
	{
		_actualPosition = 0;
		_scroll_rect = gameObject.GetComponent<ScrollRect>();
		_scroll_rect.inertia = false;
		_lerp = true;
		
		_positions = new List<Vector3>();
		
		if (Screens > 0)
		{
			for (int i = 0; i < Screens; ++i)
			{
				_scroll_rect.horizontalNormalizedPosition = (float) i / (float)(Screens - 1);
				_positions.Add(ScreensContainer.localPosition);
			} 
		}
		
		_lerp_target = _positions [0];
		
		_scroll_rect.horizontalNormalizedPosition = (float)(StartingScreen - 1) / (float)(Screens - 1);
		
		//_scroll_rect.
	}
	
	void Update()
	{
		if (_lerp)
		{
			if (_lerpProgress == false && progressBar != null)
			{
				_lerpProgress = true;
				_lerp_targetProgress = ((float)_actualPosition+1)/(float)Screens;
			}
			
			ScreensContainer.localPosition = Vector3.Lerp(ScreensContainer.localPosition, _lerp_target, 5 * Time.deltaTime);
			if (Vector3.Distance(ScreensContainer.localPosition, _lerp_target) < 0.001f)
			{
				_lerp = false;
			}
		}
		
		if (_lerpProgress)
		{
			progressBar.size = Mathf.Lerp(progressBar.size, _lerp_targetProgress, 2 * Time.deltaTime);
			
			if (_lerp_targetProgress - progressBar.size < 0.01f)
			{
				_lerpProgress = false;
			}
		}
	}
	
	public void goToPosX(int pos)
	{
		if (_scroll_rect != null)
			//if (_scroll_rect.horizontal)
		{
			_lerp = true;
			_lerp_target = _positions[pos];
			_actualPosition = pos;
		}
		
		if (pos == 0)
		{
			if (leftBtn != null)
				leftBtn.SetActive(false);
			if (rightBtn != null)
				rightBtn.SetActive(true);
		}
		else if (pos == Screens-1)
		{
			if (leftBtn != null)
				leftBtn.SetActive(true);
			if (rightBtn != null)
				rightBtn.SetActive(false);
		}
		else
		{
			if (leftBtn != null)
				leftBtn.SetActive(true);
			if (rightBtn != null)
				rightBtn.SetActive(true);
		}
	}
	
	public void goRight()
	{
		//if (_scroll_rect.horizontal)
		{
			if (_actualPosition < Screens-1)
			{
				_lerp = true;
				_lerp_target = _positions[_actualPosition+1];
				_actualPosition++;
				
				// manage btn
				if (_actualPosition == Screens-1)
					if (rightBtn != null)
						rightBtn.SetActive(false);
				
				if (leftBtn != null)
					leftBtn.SetActive(true);
			}
		}
	}
	
	public void goLeft()
	{
		//if (_scroll_rect.horizontal)
		{
			if (_actualPosition > 0)
			{
				_lerp = true;
				_lerp_target = _positions[_actualPosition-1];
				_actualPosition--;
				
				// manage btn
				if (_actualPosition == 0)
					if (leftBtn != null)
						leftBtn.SetActive(false);
				
				if (rightBtn != null)
					rightBtn.SetActive(true);
			}
		}
	}
	
	public void DragEnd()
	{
		//if (_scroll_rect.horizontal)
		{
			_lerp = true;
			_lerp_target = FindClosestFrom(ScreensContainer.localPosition, _positions);
		}
	}
	
	public void OnDrag()
	{
		_lerp = false;
	}
	
	Vector3 FindClosestFrom(Vector3 start, List<Vector3> positions)
	{
		Vector3 closest = Vector3.zero;
		float distance = Mathf.Infinity;
		
		/*
		foreach (Vector3 position in _positions)
		{
			if (Vector3.Distance(start, position) < distance)
			{
				distance = Vector3.Distance(start, position);                
				closest = position;
			}
		}*/
		
		for (int i=0; i<Screens ;i++)
		{
			if (Vector3.Distance(start, positions[i]) < distance)
			{
				distance = Vector3.Distance(start, positions[i]);                
				closest = positions[i];
				_actualPosition=i;
			}
		}
		
		return closest;
	}
}


