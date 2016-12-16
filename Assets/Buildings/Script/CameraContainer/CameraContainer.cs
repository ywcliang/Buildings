using UnityEngine;
using System.Collections;
using G;
using Unit;

public class CameraContainer : MonoBehaviour {
	public static CameraContainer instance = null;
	/// <summary>
	///camera limit in world size,factor to control how much distance camera can move to,
	///x in horizontal
	///y in vertical (z order)
	/// </summary>
	public Vector2 DragFactor;

	//activing camera  
	private Camera m_CCurrentCam = null;
	private bool m_bCamTypeOrthographic = false;
	//min drag potin in world
	private Vector3 m_CSizeCamMoveMin;
	//max drag potin in world
	private Vector3 m_CSizeCamMoveMax;

	//resolve drage conflict when 2 finger suddenly release one.
	private float dragConflictCoolDown = 0.5f;
	bool			m_bDragConflict = false;

	public IEnumerator wait()
	{
		for (float t = 0; t < dragConflictCoolDown; t += Time.deltaTime)
		{
			yield return 0;
		}
		m_bDragConflict = false;
	}

	void Awake()
	{
		instance = this;
		ChooseCamera();
	}

	// Use this for initialization
	void Start () {

		ResetCame ();
		//bottom vector are in x-z axis
		//sizeMap.y is used as z axis value
		Vector2 sizeMap = Ground.instance.GetSize ();

		Vector3 offset = new Vector3 (sizeMap.x * DragFactor.x, 0, sizeMap.y * DragFactor.y);
	
		m_CSizeCamMoveMin = Ground.instance.transform.position - offset * 0.5f;
		m_CSizeCamMoveMax = Ground.instance.transform.position + offset * 0.5f;
		//m_CSizeCamMove = new Vector2(GlobalDef.CameraContainerOriginPos.x + 0.5f * sizeMap.x, );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void ResovleDragConflict ()
	{
		m_bDragConflict = true;
		StartCoroutine (wait());
	}

	public void OnFingerUp(ref FingerUpEvent eventData)
	{
		
	}

	public void OnTap(ref TapGesture e)
	{
		if (e.Selection) {
			UnitBase b = e.Selection.GetComponent<UnitBase> ();
			if (b ==null) {
				b = e.Selection.GetComponentInParent<UnitBase> ();
			}
			if (b ==null) {
				b = e.Selection.GetComponentInChildren<UnitBase> ();
			}

			if (b != null) {
				b.onTouch (ref e);
			}
		}
	}

	public void OnDrag(ref DragGesture e)
	{
		if (e.DeltaMove == Vector2.zero || m_bDragConflict)
			return;

		//drag in z axi at vertical,so set y value to z
		Vector3 v = new Vector3 (e.DeltaMove.x,
								0, 
								e.DeltaMove.y);

		Vector3 vRight = transform.right;
		Vector3 vForward = transform.forward;
		//exclude y axis move.
		vRight.y = 0;
		vForward.y = 0;

		//if (e.DeltaMove.magnitude > GlobalDef.MaxCameraDragDistance) {
		Vector3 Vx = vRight* v.x * GlobalDef.MaxCameraDragDistance * GlobalDef.CameraDragFactor;
		Vector3 Vy = vForward * v.z * GlobalDef.MaxCameraDragDistance * GlobalDef.CameraDragFactor;
//		} else {
//			transform.position -= vRight * v.x * GlobalDef.CameraDragFactor;
//			transform.position -= vForward * v.z * GlobalDef.CameraDragFactor;
//		}
		//change drag speed when came zoom
		if (m_bCamTypeOrthographic) {
			float factor = m_CCurrentCam.orthographicSize / GlobalDef.MaxOrtCameraOriginSize;
			Vx *= factor;
			Vy *= factor;
		} else {
			float factor = m_CCurrentCam.fieldOfView / GlobalDef.MaxPersCameraFov;
			Vx *= factor;
			Vy *= factor;
		}

		transform.position -= Vx;
		transform.position -= Vy;

		Vector3 checkV = transform.position;

		//check limit range in min x
		if (checkV.x < m_CSizeCamMoveMin.x) {
			//if (e.DeltaMove.x > 0) {
				checkV.x = m_CSizeCamMoveMin.x;
			//}
		}
		//check limit range in min z
		if (checkV.z < m_CSizeCamMoveMin.z) {
			//if (e.DeltaMove.y > 0) {
				checkV.z = m_CSizeCamMoveMin.z;
			//}
		}
		//check limit range in max x
		if (checkV.x > m_CSizeCamMoveMax.x) {
			//if (e.DeltaMove.x < 0) {
				checkV.x = m_CSizeCamMoveMax.x;
			//}
		}
		//check limit range in max z
		if (checkV.z > m_CSizeCamMoveMax.z) {
			//if (e.DeltaMove.y < 0) {
				checkV.z = m_CSizeCamMoveMax.z;
			//}
		}
//		string form = string.Format ("transform.position.x  {0} transform.position.y   {1}  ",e.DeltaMove.x, e.DeltaMove.y);
//
//		DebugConsole.Log (form, "warning");
		if (checkV != transform.position)
			transform.position = checkV;

		if (m_bCamTypeOrthographic) {
		
		} else {
		
		}

	}

	public void OnDrag2(ref DragGesture e)
	{
		//rotate camera around with x axis
		float angles = -e.DeltaMove.y * 0.15f;
		//m_CCamera.transform.RotateAround(Vector3.zero, Vector3.right, angles);
		transform.Rotate(Vector3.right * angles);

		if (GlobalDef.MinPersCameraEulerX < 0) {
			//if angle <= 0 equals 360,so we change to check greater than 300;
			if (transform.eulerAngles.x > 300) {
				if (transform.eulerAngles.x < 360 + GlobalDef.MinPersCameraEulerX) {
					transform.eulerAngles = new Vector3 (GlobalDef.MinPersCameraEulerX, transform.eulerAngles.y, transform.eulerAngles.z);
					//m_CCamera.transform.rotation.Set (0.3f, m_CCamera.transform.rotation.y, m_CCamera.transform.rotation.z, m_CCamera.transform.rotation.w);
				}
			} else {
				if (transform.eulerAngles.x > GlobalDef.MaxPersCameraEulerX) {
					transform.eulerAngles = new Vector3 (GlobalDef.MaxPersCameraEulerX, transform.eulerAngles.y, transform.eulerAngles.z);
					//m_CCamera.transform.rotation.Set (0.6f, m_CCamera.transform.rotation.y, m_CCamera.transform.rotation.z, m_CCamera.transform.rotation.w);
				}
			}
		} else {
			if (transform.eulerAngles.x < GlobalDef.MinPersCameraEulerX) {
				transform.eulerAngles = new Vector3 (GlobalDef.MinPersCameraEulerX, transform.eulerAngles.y, transform.eulerAngles.z);
				//m_CCamera.transform.rotation.Set (0.3f, m_CCamera.transform.rotation.y, m_CCamera.transform.rotation.z, m_CCamera.transform.rotation.w);
			}else if (transform.eulerAngles.x > GlobalDef.MaxPersCameraEulerX) {
				transform.eulerAngles = new Vector3 (GlobalDef.MaxPersCameraEulerX, transform.eulerAngles.y, transform.eulerAngles.z);
				//m_CCamera.transform.rotation.Set (0.6f, m_CCamera.transform.rotation.y, m_CCamera.transform.rotation.z, m_CCamera.transform.rotation.w);
			}
		}

		if (m_bCamTypeOrthographic) {

		} else {

		}

		if (e.Phase == ContinuousGesturePhase.Ended)
		{
			//reslove drag conflict
			ResovleDragConflict();
		}
	}

	public void OnTwist(ref TwistGesture e)
	{
		transform.Rotate(Vector3.up * e.DeltaRotation * GlobalDef.CameraRotateFactor, Space.World);
		//Vector3 center = Main.getMainIns().GetWorldCenterPosition();
	

		//			Vector3 relativePos = center - transform.position;
		//			Vector3 pos = center + relativePos;
		//			this.transform.position = Vector3.Lerp(this.transform.position, pos, speed*Time.deltaTime);
		//
		//			Quaternion rotation = Quaternion.LookRotation(relativePos);
		//			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, e.DeltaRotation * GlobalDef.CameraRotateFactor);
		//transform.rotation = rotation;

		if (e.Phase == ContinuousGesturePhase.Ended)
		{
			//reslove drag conflict
			ResovleDragConflict();
		}
	}

	public void OnPinch(ref PinchGesture e)
	{
		if (m_bCamTypeOrthographic) {
			m_CCurrentCam.orthographicSize -= e.Delta * GlobalDef.CameraZoomFactor;
			//adjust fov between available value 
			if (m_CCurrentCam.orthographicSize > GlobalDef.MaxOrtCameraOriginSize)
				m_CCurrentCam.orthographicSize = GlobalDef.MaxOrtCameraOriginSize;
			if (m_CCurrentCam.orthographicSize < GlobalDef.MinOrtCameraOriginSize)
				m_CCurrentCam.orthographicSize = GlobalDef.MinOrtCameraOriginSize;

		} else {
			m_CCurrentCam.fieldOfView -= e.Delta * GlobalDef.CameraZoomFactor;
			//adjust fov between available value 
			if (m_CCurrentCam.fieldOfView > GlobalDef.MaxPersCameraFov)
				m_CCurrentCam.fieldOfView = GlobalDef.MaxPersCameraFov;
			if (m_CCurrentCam.fieldOfView < GlobalDef.MinPersCameraFov)
				m_CCurrentCam.fieldOfView = GlobalDef.MinPersCameraFov;
		}

		if (e.Phase == ContinuousGesturePhase.Ended)
		{
			//reslove drag conflict
			ResovleDragConflict();
		}
	}

	public void ResetCame()
	{
		transform.position = GlobalDef.CameraContainerOriginPos;
		transform.rotation = Quaternion.Euler(GlobalDef.CameraContainerOriginAngle);

		m_CCurrentCam.transform.localPosition = new Vector3(0,0, -30);
		m_CCurrentCam.transform.localRotation = Quaternion.Euler(new Vector3(1,1, 1));

		if (m_bCamTypeOrthographic) {
			m_CCurrentCam.orthographicSize = GlobalDef.OrtCameraOriginSize;
			m_CCurrentCam.transform.localPosition = GlobalDef.OrtCameraOriginPos;
			m_CCurrentCam.transform.localRotation = Quaternion.Euler(GlobalDef.OrtCameraOriginRotation);
		} else {
			m_CCurrentCam.fieldOfView = GlobalDef.PersCameraOriginFoV;
			m_CCurrentCam.transform.localPosition = GlobalDef.PersCameraOriginPos;
			m_CCurrentCam.transform.localRotation = Quaternion.Euler(GlobalDef.PersCameraOriginRotation);
		}
	}

	private void ChooseCamera()
	{
		Transform t = transform.Find("perspective");
		if (t != null) {
			Transform t2 = t.GetChild (0);
			Camera came = t2.gameObject.GetComponent<Camera> ();

			#if UNITY_3_5
			if(came.gameObject.active )
			{
				m_CCurrentCam = came;
				m_bCamTypeOrthographic = false;
			}
			#else
			if(came.gameObject.activeInHierarchy )
			{
				m_CCurrentCam = came;
				m_bCamTypeOrthographic = false;
			}
			#endif
		}


		t = transform.Find ("orthographic");
		if (t != null) {
			Transform t2 = t.GetChild (0);

			Camera came = t2.GetComponent<Camera>();

			#if UNITY_3_5
			if(came.gameObject.active)
			{
				m_CCurrentCam = came;
				m_bCamTypeOrthographic = true;
			}
			#else
			if(came.gameObject.activeInHierarchy )
			{
				m_CCurrentCam = came;
				m_bCamTypeOrthographic = true;
			}
			#endif
		}
	}


	//get current camera.
	public Camera getMainCamera()
	{
		return m_CCurrentCam;	
	}
}
