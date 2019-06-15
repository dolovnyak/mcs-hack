using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EyesInput : BaseInput
{
	protected Vector2 _mousePosition;
	
	public override bool mousePresent
	{
		get { return true; }
	}
	
	public override Vector2 mousePosition
	{
		get { return mousePresent ? _mousePosition : Vector2.zero; }
	}
}
