using UnityEngine;
using System.Collections;
using G;
using Building;

public class Main : MonoBehaviour {
	public static Main Instance = null;
	private CameraContainer m_pCameraContainer = null;
	private Ground m_CGround = null;

	public static Main getMainIns()
	{
		if (Instance == null) {
			Instance = (Main)FindObjectOfType (typeof(Main));
		}
		return Instance;
	}

	private Main()
	{
		
	}

	public Camera getMainCam()
	{
		return m_pCameraContainer.getMainCamera ();
	}

	public void SetCameraContainer(CameraContainer c)
	{
		m_pCameraContainer = c;
	}

	public CameraContainer GetCameraContainer()
	{
		return m_pCameraContainer;
	}

	public void SetGround(Ground g)
	{
		m_CGround = g;
	}

	public Ground GetGround()
	{
		return m_CGround;
	}
		
	void Awake()
	{
		//init contorller
//		#if (UNITY_EDITOR || UNITY_STANDALONE)
//		m_pControled = new CameraControllerImEditor();
//		#elif (UNITY_ANDROID)
//		m_pControled = new CameraControllerImDevice();
//		#endif
		Main.getMainIns();
	}

	// Use this for initialization
	void Start () {
//		if (m_pControled != null)
//			m_pControled.Init (ref m_pMainCame);
		//Main.getMainIns();
	}

	// Update is called once per frame
	void Update () {
//		if (m_pControled != null)
//			m_pControled.Update ();


	}

	public IEnumerator CutDown()
	{
		for (int i = 0; i < 5; ++i)
		{
			DebugConsole.Log ("CutDown   start " + i);
			yield return StartCoroutine (Wait(2));
			DebugConsole.Log ("CutDown   end " + i);
		}

	}

	public IEnumerator Wait(float duration)
	{
		float i = 0;
		while (i < 5)
		{
			i += Time.deltaTime;
			DebugConsole.Log ("Waiting " + i);
			yield return 0;
		}

	}

	public void ResetCamera()
	{
		if (m_pCameraContainer) {
			m_pCameraContainer.ResetCame ();
		}
	}

	void OnDestroy(){
		
	}

	public void ExitGame()
	{
		
		#if UNITY_IOS
			// ios platform ---> Apple Technical Page qa1561 

			BuildingManager.RevmoveAllBuilding ();
			Application.Quit ();
		#endif

		#if UNITY_ANDROID
			BuildingManager.RevmoveAllBuilding ();
			Application.Quit ();
		#endif
	}

}
