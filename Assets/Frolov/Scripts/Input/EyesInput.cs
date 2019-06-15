using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EyesInput : BaseInput
{
	private static EyesInput instance = null;
	public static EyesInput Instance => instance;

	public bool enabled = false;
	
	[SerializeField] private Transform leftEye;
	[SerializeField] private Transform rightEye;

	[Space] 
	[SerializeField] private int queueMaxSize = 8;
	[SerializeField] private float additionalXCoefficient = 1f;
	[SerializeField] private float additionalYCoefficient = 1f;

	private Vector2 _rawMousePosition;
	private Vector2 _mousePosition;
	
	private Queue<Vector3> queue = new Queue<Vector3>();
	
	private Vector2 iPhoneXPointSize = new Vector2(375, 812) * 3;
	private Vector2 iPhoneXMeterSize = new Vector2(0.0623908297f, 0.135096943231532f);

	private Transform mainCamera;

	public override bool mousePresent => enabled || base.mousePresent;
	
	public override Vector2 mousePosition => enabled ? _mousePosition : base.mousePosition;
	public Vector2 rawMousePosition => enabled ? _rawMousePosition : base.mousePosition;
	
	private void Awake()
	{
		mainCamera = Camera.main.transform;

		if (!instance)
			instance = this;
	}

	private void Update()
	{
		if (!enabled)
			return;
		
		Vector3 li, ri;

		if (Math3d.LinePlaneIntersection(out li, leftEye.position, leftEye.forward, mainCamera.forward, Vector3.zero) &&
		    Math3d.LinePlaneIntersection(out ri, rightEye.position, rightEye.forward, mainCamera.forward, Vector3.zero))
		{
			Debug.Log($"found intersection");
			
			Vector3 mi = mainCamera.InverseTransformPoint(li);
			
			mi.x = mi.x / (iPhoneXMeterSize.x / 2.0f) * (iPhoneXPointSize.x / 2.0f) * additionalXCoefficient + iPhoneXPointSize.x / 2.0f;
			mi.x = Mathf.Max(Mathf.Min(mi.x, iPhoneXPointSize.x - 1), 0);
			
			mi.y = mi.y / (iPhoneXMeterSize.y / 2.0f) * (iPhoneXPointSize.y / 2.0f) * additionalYCoefficient + iPhoneXPointSize.y / 2.0f;
			mi.y = Mathf.Max(Mathf.Min(mi.y, iPhoneXPointSize.y - 1), 0);
			
			_rawMousePosition = mi;
			
			if (queue.Count < queueMaxSize)
			{
				queue.Enqueue(mi);
			}
			else
			{
				Vector3 res = mi;
				foreach (Vector3 vec in queue)
				{
					res += vec;
				}
				res /= queue.Count + 1;
				queue.Dequeue();
				queue.Enqueue(res);
				_mousePosition = res;
			}
		}
	}

	public void SetX(float value)
	{
		additionalXCoefficient = value;
	}
	
	public void SetY(float value)
	{
		additionalYCoefficient = value;
	}

	public void SwitchMode()
	{
		enabled = !enabled;
	}
}
