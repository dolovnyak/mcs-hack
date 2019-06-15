using System.Collections;
using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableLoader : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField, Range(0, 100)] private int speed;
	[SerializeField] private ProgressBar progressBar;
	
	public void OnPointerEnter(PointerEventData eventData)
	{
		progressBar.speed = speed;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		progressBar.speed = 0;
		progressBar.currentPercent = 0;
	}
}
