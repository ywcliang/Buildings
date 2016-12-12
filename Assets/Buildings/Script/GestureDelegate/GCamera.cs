using UnityEngine;
using System.Collections;
using G;
using UnityEngine.EventSystems;

public class GCamera : MonoBehaviour {
	private CameraContainer m_CCamContainer;

	/// <summary>
	/// indecate our first touch down was not block by any thing which priority level higher than us. (like UI)
	/// and then we can use this var to decide should we do drag,twist or other things.
	/// </summary>
	private bool m_bTouchFocus;

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
		if (twist)
			twist.OnGesture += OnTwist;

		PinchRecognizer pinch = GetComponent<PinchRecognizer> ();
		if (pinch)
			pinch.OnGesture += OnPinch;

		FingerDownDetector fD = GetComponent<FingerDownDetector> ();
		if (fD)
			fD.OnFingerDown += OnFingerDown;

		FingerUpDetector fU = GetComponent<FingerUpDetector> ();
		if (fU)
			fU.OnFingerUp += OnFingerUp;

	}
		

	// Use this for initialization
	void Start () {
		m_CCamContainer = Main.getMainIns ().GetCameraContainer ();
		//init touch filter.
		FingerGestures.GlobalTouchFilter = TouchFilter;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//destory all gesture delegate
	void OnDestroy(){
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
		if (twist)
			twist.OnGesture -= OnTwist;

		PinchRecognizer pinch = GetComponent<PinchRecognizer> ();
		if (pinch)
			pinch.OnGesture -= OnPinch;

		FingerDownDetector fD = GetComponent<FingerDownDetector> ();
		if (fD)
			fD.OnFingerDown += OnFingerDown;

		FingerUpDetector fU = GetComponent<FingerUpDetector> ();
		if (fU)
			fU.OnFingerUp += OnFingerUp;
	}

	//touch down, if touch blocked by ui then this event will not triggered
	void OnFingerDown (FingerDownEvent eventData)
	{
		//only receive first touch, others are ignore
		if (eventData.Finger.Index == 0 && eventData.Finger.Phase == FingerGestures.FingerPhase.Begin) {
			//if not touch ui item
			if (!EventSystem.current.IsPointerOverGameObject ()) {
				m_bTouchFocus = true;
			}
		}
	}

	//touch up
	void OnFingerUp (FingerUpEvent eventData)
	{
		//only receive first touch, others are ignore
		if (eventData.Finger.Index == 0) {
			m_bTouchFocus = false;
		}
	}

	//drag
	void OnDrag (DragGesture e)
	{
		if (m_bTouchFocus) {
			if (m_CCamContainer != null)
			{
				m_CCamContainer.OnDrag (ref e);
			}
		}
	}

	//drag2
	void OnDrag2 (DragGesture e)
	{
		if (m_bTouchFocus) {
			if (m_CCamContainer != null)
			{
				m_CCamContainer.OnDrag2 (ref e);
			}
		}
	}

	//twist
	void OnTwist (TwistGesture e)
	{
		if (m_bTouchFocus) {
			if (m_CCamContainer != null)
			{
				m_CCamContainer.OnTwist (ref e);
			}
		}
	}

	//pinch
	void OnPinch (PinchGesture e)
	{
		if (m_bTouchFocus) {
			if (m_CCamContainer != null) {
				m_CCamContainer.OnPinch (ref e);
			}
		}
	}

	//check is there any item filter our touch.
	public bool TouchFilter( int fingerIndex, Vector2 position )
	{
		Camera c = Main.getMainIns ().getMainCam ();
		if (c) {
//			int uiMask = LayerMask.GetMask ("UI");
//			Ray ray = c.ScreenPointToRay( position );
//			RaycastHit hit;
//			bool touchUI = Physics.Raycast (ray, 1, uiMask);
			return true;
		}
		return true;
	}
}
