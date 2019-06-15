using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class UnityEyeManager : MonoBehaviour 
{
	[SerializeField]
	private GameObject leftEyePrefab;
	[SerializeField]
	private GameObject rightEyePrefab;

	private UnityARSessionNativeInterface m_session;
	[SerializeField]
	private GameObject leftEyeGo;
	[SerializeField]
	private GameObject rightEyeGo;

	// Use this for initialization
	void Start () {
		m_session = UnityARSessionNativeInterface.GetARSessionNativeInterface();

		Application.targetFrameRate = 60;
		ARKitFaceTrackingConfiguration config = new ARKitFaceTrackingConfiguration();
		config.alignment = UnityARAlignment.UnityARAlignmentGravity;
		config.enableLightEstimation = true;

		if (config.IsSupported )
		{

			m_session.RunWithConfig (config);

			UnityARSessionNativeInterface.ARFaceAnchorAddedEvent += FaceAdded;
			UnityARSessionNativeInterface.ARFaceAnchorUpdatedEvent += FaceUpdated;
			UnityARSessionNativeInterface.ARFaceAnchorRemovedEvent += FaceRemoved;

		}
		
		leftEyeGo.SetActive (false);
		rightEyeGo.SetActive (false);

	}

	void FaceAdded (ARFaceAnchor anchorData)
	{
		leftEyeGo.transform.position = anchorData.leftEyePose.position;
		leftEyeGo.transform.rotation = anchorData.leftEyePose.rotation;

		rightEyeGo.transform.position = anchorData.rightEyePose.position;
		rightEyeGo.transform.rotation = anchorData.rightEyePose.rotation;

		leftEyeGo.SetActive (true);
		rightEyeGo.SetActive (true);
	}

	void FaceUpdated (ARFaceAnchor anchorData)
	{
		leftEyeGo.transform.position = anchorData.leftEyePose.position;
		leftEyeGo.transform.rotation = anchorData.leftEyePose.rotation;

		rightEyeGo.transform.position = anchorData.rightEyePose.position;
		rightEyeGo.transform.rotation = anchorData.rightEyePose.rotation;

	}

	void FaceRemoved (ARFaceAnchor anchorData)
	{
		leftEyeGo.SetActive (false);
		rightEyeGo.SetActive (false);

	}

	// Update is called once per frame
	void Update () {
		
	}
}
