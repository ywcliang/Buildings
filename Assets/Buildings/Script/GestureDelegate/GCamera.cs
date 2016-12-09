using UnityEngine;
using System.Collections;
using G;

//[RequireComponent (typeof(TwistRecognizer))]

public class GCamera : MonoBehaviour {
	private CameraContainer m_CCamContainer;
	public GameObject o;

	void Awake()
	{
		//init drag gestures
		DragRecognizer[] dragList = GetComponents<DragRecognizer>();

		foreach (DragRecognizer drag in dragList) {
			if (drag.RequiredFingerCount == GlobalDef.TOUCH_SINGLE) {
				drag.OnGesture += OnDrag;
			}
			else if (drag.RequiredFingerCount == GlobalDef.TOUCH_MULTIPLE)
			{
				drag.OnGesture += OnDrag2;
			}
		}

		TwistRecognizer twist = GetComponent<TwistRecognizer> ();
		twist.OnGesture += OnTwist;

		PinchRecognizer pinch = GetComponent<PinchRecognizer> ();
		pinch.OnGesture += OnPinch;
	}

	// Use this for initialization
	void Start () {
		m_CCamContainer = Main.getMainIns ().GetCameraContainer ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//destory all gesture delegate
	void Destory(){
		DragRecognizer[] dragList = GetComponents<DragRecognizer>();
		foreach (DragRecognizer drag in dragList) {
			if (drag.RequiredFingerCount == GlobalDef.TOUCH_SINGLE) {
				drag.OnGesture -= OnDrag;
			}
			else if (drag.RequiredFingerCount == GlobalDef.TOUCH_MULTIPLE)
			{
				drag.OnGesture -= OnDrag2;
			}
		}

		TwistRecognizer twist = GetComponent<TwistRecognizer> ();
		twist.OnGesture -= OnTwist;

		PinchRecognizer pinch = GetComponent<PinchRecognizer> ();
		pinch.OnGesture -= OnPinch;
	}

	//drag
	void OnDrag (DragGesture e)
	{
		if (m_CCamContainer != null)
		{
			m_CCamContainer.OnDrag (ref e);
		}

		//DebugConsole.Log (" OnDrag gesture  " + e.Fingers.Count, "normal");
	}

	//drag
	void OnDrag2 (DragGesture e)
	{
		if (m_CCamContainer != null)
		{
			m_CCamContainer.OnDrag2 (ref e);
		}

		//DebugConsole.Log (" m_CCamera.transform.rotation.x " + m_CCamera.transform.rotation.x, "normal");
	}

	//twist
	void OnTwist (TwistGesture e)
	{
		if (m_CCamContainer != null)
		{
			m_CCamContainer.OnTwist (ref e);
		}


		//DebugConsole.Log (" OnTwist gesture  " + e.DeltaRotation, "warning");
	}

	//pinch
	void OnPinch (PinchGesture e)
	{
		if (m_CCamContainer != null)
		{
			m_CCamContainer.OnPinch (ref e);
		}


		//DebugConsole.Log (" OnPinch gesture  " + e.Delta, "error");
	}
}
