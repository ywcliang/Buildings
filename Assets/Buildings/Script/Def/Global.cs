using UnityEngine;

namespace G{
	public class GlobalDef {
		//***** touch def*****  start
		public static int TOUCH_SINGLE = 1;
		public static int TOUCH_MULTIPLE = 2;


		public static int TOUCH_FIRST = 0;
		public static int TOUCH_SECOND = 1;
		//***** touch def***** 	end

		//camera control def start
		public static float CameraDragFactor = 0.005f;
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
		public static float MaxPersCameraEulerX = 70.0f;
		public static float MinPersCameraEulerX = 35.0f;

		public static int MaxPersCameraFov = 45;
		public static int MinPersCameraFov = 10;
		//********************************************************

		//camera control def end

		//map def start
		//one map cell size in width
		public static float MapCellWidth = 10;
		//one map cell size in height
		public static float MapCellHeight = 10;
		//is map position returned in left bottom corner or center
		public static bool MapAnchorInCenter = false;
		//map def end

		//animation def start
		public static string s_GBuildAnimator_Construct_Var = "Construct";
		public static string s_GBuildAnimator_BuildLevel_Var = "Build_level";
		public static string s_GBuildAnimator_Destory_Var = "Destroy";
		public static string s_GBuildAnimator_PreLoadDone_Var = "PreLoadDone";

		public static string s_GBuildAnimator_Construct_Doing = "AniConstruct_doing";
		public static string s_GBuildAnimator_Produce_Doing = "AniProduce_doing";
		public static string s_GBuildAnimator_Destory_Doing = "AniDestory_doing";
		public static string s_GBuildAnimator_Prebuild_Doing = "AniPrebuild_doing";

		//animation def end
	}
}

