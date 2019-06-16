using System.Collections;
using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableLoader : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField, Range(0, 100)] private int speed;
	[SerializeField] private ProgressBar progressBar;
	
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!EyesInput.Instance.enabled)
			return;
		progressBar.speed = speed;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!EyesInput.Instance.enabled)
			return;
		progressBar.speed = 0;
		progressBar.currentPercent = 0;
	}
}
