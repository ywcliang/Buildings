using UnityEngine;
using System.Collections;

public class CameraUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnRestCameraClick()
	{
		Main.Instance.ResetCamera ();
	}


	public void OnExitClick()
	{
		Main.Instance.ExitGame ();
	}
}
