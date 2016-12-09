using UnityEngine;
using System.Collections;
using G;

public class CameraContainer : MonoBehaviour {

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

	void Awake()
	{
		Main.getMainIns().SetCameraContainer(this);
		ChooseCamera();
	}

	// Use this for initialization
	void Start () {
		ResetCame ();
		//bottom vector are in x-z axis
		//sizeMap.y is used as z axis value
		Vector2 sizeMap = Main.getMainIns ().GetGround ().GetSize ();

		Vector3 offset = new Vector3 (sizeMap.x * DragFactor.x, 0, sizeMap.y * DragFactor.y);
	
		m_CSizeCamMoveMin = GlobalDef.CameraContainerOriginPos - offset * 0.5f;
		m_CSizeCamMoveMax = GlobalDef.CameraContainerOriginPos + offset * 0.5f;
		//m_CSizeCamMove = new Vector2(GlobalDef.CameraContainerOriginPos.x + 0.5f * sizeMap.x, );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnDrag(ref DragGesture e)
	{
		//drag in z axi at vertical,so set y value to z
		Vector3 v = new Vector3 (e.DeltaMove.x, 0, e.DeltaMove.y);

		if (e.DeltaMove.magnitude > GlobalDef.MaxCameraDragDistance) {
			transform.position -= v.normalized * GlobalDef.MaxCameraDragDistance * GlobalDef.CameraDragFactor;
		} else {
			transform.position -= v * GlobalDef.CameraDragFactor;
		}

		Vector3 checkV = transform.position;
		//check limit range in min x
		if (checkV.x < m_CSizeCamMoveMin.x) {
			if (e.DeltaMove.x > 0) {
				checkV.x = m_CSizeCamMoveMin.x;
			}
		}
		//check limit range in min z
		if (checkV.z < m_CSizeCamMoveMin.z) {
			if (e.DeltaMove.y > 0) {
				checkV.z = m_CSizeCamMoveMin.z;
			}
		}
		//check limit range in max x
		if (checkV.x > m_CSizeCamMoveMax.x) {
			if (e.DeltaMove.x < 0) {
				checkV.x = m_CSizeCamMoveMax.x;
			}
		}
		//check limit range in max z
		if (checkV.z > m_CSizeCamMoveMax.z) {
			if (e.DeltaMove.y < 0) {
				checkV.z = m_CSizeCamMoveMax.z;
			}
		}
		string form = string.Format ("check limit range  min!!!  transform.position.x  {0} transform.position.x   {1}  ",e.DeltaMove.x, e.DeltaMove.y);
		DebugConsole.Log (form, "normal");
		if (checkV != transform.position)
			transform.position = checkV;

		if (transform.position.x < m_CSizeCamMoveMin.x || transform.position.z < m_CSizeCamMoveMin.z)
		{
//			string form = string.Format ("check limit range  min!!!  transform.position.x  {0} transform.position.x   {1}  ",transform.position.x, transform.position.z);
//			DebugConsole.Log (form, "normal");
			transform.position = m_CSizeCamMoveMin;

		}
		if (transform.position.x > m_CSizeCamMoveMax.x || transform.position.z > m_CSizeCamMoveMax.z)
		{
//			string form = string.Format ("check limit range  max!!!  transform.position.x  {0} transform.position.x   {1}  ",transform.position.x, transform.position.z);
//			DebugConsole.Log (form, "warning");
			transform.position = m_CSizeCamMoveMax;
		}

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



		DebugConsole.Log ("transform.eulerAngles.x   " + transform.eulerAngles.x, "normal");
		if (m_bCamTypeOrthographic) {

		} else {

		}
	}

	public void OnTwist(ref TwistGesture e)
	{
		
		transform.rotation *= Quaternion.Euler (Vector3.up * e.DeltaRotation * GlobalDef.CameraRotateFactor);
		//Vector3 center = Main.getMainIns().GetWorldCenterPosition();
	

		//			Vector3 relativePos = center - transform.position;
		//			Vector3 pos = center + relativePos;
		//			this.transform.position = Vector3.Lerp(this.transform.position, pos, speed*Time.deltaTime);
		//
		//			Quaternion rotation = Quaternion.LookRotation(relativePos);
		//			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, e.DeltaRotation * GlobalDef.CameraRotateFactor);
		//transform.rotation = rotation;
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

			DebugConsole.Log ("m_CCurrentCam.orthographicSize    " + m_CCurrentCam.orthographicSize, "normal");
		} else {
			m_CCurrentCam.fieldOfView -= e.Delta * GlobalDef.CameraZoomFactor;
			//adjust fov between available value 
			if (m_CCurrentCam.fieldOfView > GlobalDef.MaxPersCameraFov)
				m_CCurrentCam.fieldOfView = GlobalDef.MaxPersCameraFov;
			if (m_CCurrentCam.fieldOfView < GlobalDef.MinPersCameraFov)
				m_CCurrentCam.fieldOfView = GlobalDef.MinPersCameraFov;
			DebugConsole.Log ("m_CCurrentCam.fieldOfView    " + m_CCurrentCam.fieldOfView, "normal");
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
			if (came.gameObject.activeInHierarchy) 
			{
				m_CCurrentCam = came;
				m_bCamTypeOrthographic = false;
			}
		}


		t = transform.Find ("orthographic");
		if (t != null) {
			Transform t2 = t.GetChild (0);

			Camera came = t2.GetComponent<Camera>();
			if (came.gameObject.activeInHierarchy) 
			{
				m_CCurrentCam = came;
				m_bCamTypeOrthographic = true;
			}
		}

	}

	//get current camera.
	public Camera getMainCamera()
	{
		return m_CCurrentCam;	
	}
}
