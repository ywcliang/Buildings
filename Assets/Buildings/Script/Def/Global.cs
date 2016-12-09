using UnityEngine;

namespace G{

	public enum UnitType
	{
		DEFAULT,
		BUILDING_D2,
		BUILDING_C2,
		BUILDING_MAX,
		CAR
	}

	public class GlobalDef {
		public static int TOUCH_SINGLE = 1;
		public static int TOUCH_MULTIPLE = 2;


		public static int TOUCH_FIRST = 0;
		public static int TOUCH_SECOND = 1;

		public static float CameraDragFactor = 0.15f;
		public static float MaxCameraDragDistance = 20.0f;

		public static float CameraZoomFactor = 0.2f;

		public static float CameraRotateFactor = 1.5f;

	

		public static Vector3 CameraContainerOriginPos = new Vector3(0, 0, 30);

		public static Vector3 CameraContainerOriginAngle = new Vector3(45, 0, 0);


		//************** if camera is Orthographic ***************

		//Camera position at first begin the game 
		public static Vector3 OrtCameraOriginPos = new Vector3(0, 0, -30);
		//Camera rotation at first begin the game 
		public static Vector3 OrtCameraOriginRotation = new Vector3(0, 0, 0);
		public static int OrtCameraOriginSize = 10;
		public static int MaxOrtCameraOriginSize = 17;
		public static int MinOrtCameraOriginSize = 2;
		//********************************************************


		//************** if camera is perspective ***************
		public static Vector3 PersCameraOriginPos = new Vector3(0, 0, -100);
		public static Vector3 PersCameraOriginRotation = new Vector3(0, 0, 0);

		public static float PersCameraOriginFoV = 30;
		public static float MaxPersCameraEulerX = 30.0f;
		public static float MinPersCameraEulerX = -20.0f;

		public static int MaxPersCameraFov = 60;
		public static int MinPersCameraFov = 10;
		//********************************************************

	}
}

