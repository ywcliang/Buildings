/// <summary>
/// This script use for control a touch controller
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class TouchController : MonoBehaviour {
	
	public int layerTouch; //layer ground
	public float distaceRay; //distance raycast
	public Camera cameraTarget; //camera layer cast
	public GameObject circle_Ring_Pref; //circle on bottom of character
	public GameObject circle_Touch_Pref; //circle when drag to target
	
	//Variable private field 
	private Controller controller;
	private LineRenderer lineRenderer;
	private bool readyDrag;
	private Ray ray;
	private RaycastHit hit;
	private Touch touch;
	private Vector3 radiausPos;
	private Vector3 posCircleStart;
	private Vector3 posCircleEnd;
	private GameObject transformRootStart;
	private GameObject transformLookStart;
	private GameObject transformRootEnd;
	private GameObject transformLookEnd;
	private GameObject circleRingStart;
	private GameObject circleRingEnd;
	private GameObject circleTouch;
	
	[HideInInspector]
	public Controller controllerGetSkill;
	
	public static TouchController instance;
	
	private enum StatInput{
		Down, Move, Up	
	}
	
	void Start(){
		instance = this;
		lineRenderer = GetComponent<LineRenderer>();	
	}
	
	void Update () {
		if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) 
		{
			InputTouch();
		}else
		{
			InpuMouse();
		}
		
		
	}
	
	void InputTouch(){
		int touchIndex = Input.touchCount;
		for(int i = 0; i < touchIndex; i++){
			touch = Input.GetTouch(i);
			if(touch.phase == TouchPhase.Began){
				DoRayCast(touch.position, StatInput.Down);
			}
			
			if(touch.phase == TouchPhase.Moved){
				DoRayCast(Input.mousePosition, StatInput.Move);
			}
			
			if(touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended){
				DoRayCast(Input.mousePosition, StatInput.Up);
			}
		}
	}
	
	void InpuMouse(){
		if(Input.GetMouseButtonDown(0)){
			DoRayCast(Input.mousePosition, StatInput.Down);
		}
		
		if(Input.GetMouseButton(0)){
			DoRayCast(Input.mousePosition, StatInput.Move);	
		}
		
		if(Input.GetMouseButtonUp(0)){
			DoRayCast(Input.mousePosition, StatInput.Up);
		}
	}
	
	private void DoRayCast(Vector3 position, StatInput statInput){
		ray = cameraTarget.ScreenPointToRay(position);
		if(Physics.Raycast(ray, out hit, distaceRay,1<<layerTouch)){
			switch(statInput){
				case StatInput.Down :{
					if(hit.collider.tag == "Player"){
						controller = hit.collider.GetComponent<Controller>();
						if(controller.actionStat != Controller.ActionStat.Dead){
							controller.actionStat = Controller.ActionStat.Idle;
						}
						controllerGetSkill = controller;
						transformRootStart = new GameObject("Root_Start");
						transformLookStart = new GameObject("Emtry_Start");
						transformRootEnd = new GameObject("Root_End");
						transformLookEnd = new GameObject("Emtry_End");
						posCircleStart.x = controller.transform.position.x;
						posCircleStart.y = 0.01f;
						posCircleStart.z = controller.transform.position.z;
						if(circleRingStart == null){
							circleRingStart = Instantiate(circle_Ring_Pref, posCircleStart, Quaternion.identity) as GameObject;
							circleRingStart.transform.parent = controller.transform;
						}else{
							circleRingStart.transform.parent = controller.transform;
							posCircleStart.x = 0;
							posCircleStart.z = 0;
							circleRingStart.transform.localPosition = posCircleStart;
						}
						posCircleStart.x = hit.point.x;
						posCircleStart.z = hit.point.z;
						circleTouch = Instantiate(circle_Touch_Pref, posCircleStart, Quaternion.identity) as GameObject;
						posCircleEnd.x = 1000;
						posCircleEnd.y = 0.01f;
						posCircleEnd.z = 1000;
						circleRingEnd = Instantiate(circle_Ring_Pref, posCircleEnd, Quaternion.identity) as GameObject;
					}
				}
				break;
				
				case StatInput.Move :{
					if(controller != null){
						lineRenderer.enabled = true;
						Vector3 pointStart = CirclePoint(transformRootStart.transform, transformLookStart.transform, controller.transform.position,hit.point);
						posCircleStart.x = hit.point.x;
						posCircleStart.z = hit.point.z;
						if(hit.collider.tag == "Enemy"){
							controller.target = hit.collider.gameObject;
							controller.positionWay = controller.target.transform.position;
							posCircleEnd.x = controller.target.transform.position.x;
							posCircleEnd.z = controller.target.transform.position.z;
							circleTouch.transform.position = posCircleEnd;
							circleRingEnd.transform.position = posCircleEnd;
							Vector3 pointEnd = CirclePoint(transformRootEnd.transform, transformLookEnd.transform, controller.target.transform.position, controller.transform.position);
							lineRenderer.SetPosition(0,new Vector3(pointStart.x,0.01f,pointStart.z));
							lineRenderer.SetPosition(1,new Vector3(pointEnd.x,0.01f,pointEnd.z));
						} else if(hit.collider.tag == "Player" && controller != hit.collider.GetComponent<Controller>()){
							controller.target = hit.collider.gameObject;
							controller.positionWay = controller.target.transform.position;
							posCircleEnd.x = controller.target.transform.position.x;
							posCircleEnd.z = controller.target.transform.position.z;
							circleTouch.transform.position = posCircleEnd;
							circleRingEnd.transform.position = posCircleEnd;
							Vector3 pointEnd = CirclePoint(transformRootEnd.transform, transformLookEnd.transform, controller.target.transform.position, controller.transform.position);
							lineRenderer.SetPosition(0,new Vector3(pointStart.x,0.01f,pointStart.z));
							lineRenderer.SetPosition(1,new Vector3(pointEnd.x,0.01f,pointEnd.z));
						}else{
							posCircleEnd.x = 1000;
							posCircleEnd.z = 1000;
							circleTouch.transform.position = posCircleStart;
							circleRingEnd.transform.position = posCircleEnd;
							controller.positionWay = hit.point;
							lineRenderer.SetPosition(0,new Vector3(pointStart.x,0.01f,pointStart.z));
							lineRenderer.SetPosition(1,new Vector3(hit.point.x,0.01f,hit.point.z));
						}
					}else{
						controller = null;
						lineRenderer.SetPosition(1,Vector3.zero);
						lineRenderer.enabled = false;
						Destroy(transformRootStart.gameObject);
						Destroy(transformRootEnd.gameObject);
						Destroy(transformLookEnd.gameObject);
						Destroy(circleRingEnd);
						Destroy(circleTouch);
					}
				}
				break;
				
				case StatInput.Up :{
					if(controller != null){
						//Controller Move to target
						if(controller.target != null){
							if(controller.typeCharacter == Controller.TypeChatacter.Melee){
								if(controller.target.tag == "Enemy"){
									if(controller.actionStat != Controller.ActionStat.Dead){
											controller.actionStat = Controller.ActionStat.Action;
									}
								}else if(controller.target.tag == "Player"){
									if(controller.actionStat != Controller.ActionStat.Dead){
										controller.actionStat = Controller.ActionStat.Move;
									}
								}
							}else if(controller.typeCharacter == Controller.TypeChatacter.Range){
								if(controller.target.tag == "Enemy"){
									if(controller.actionStat != Controller.ActionStat.Dead){
										controller.actionStat = Controller.ActionStat.Action;
									}
								}else if(controller.target.tag == "Player"){
									if(controller.actionStat != Controller.ActionStat.Dead){
										controller.actionStat = Controller.ActionStat.Move;
									}
								}
							}else if(controller.typeCharacter == Controller.TypeChatacter.Healer){
								if(controller.target.tag == "Enemy"){
									if(controller.actionStat != Controller.ActionStat.Dead){
										controller.actionStat = Controller.ActionStat.Move;
									}
								}else if(controller.target.tag == "Player"){
									if(controller.actionStat != Controller.ActionStat.Dead){
										controller.actionStat = Controller.ActionStat.Action;
									}
								}
							}
						}else{
							if(Vector3.Distance(controller.transform.position, hit.point) > 1.5f){
								if(controller.actionStat != Controller.ActionStat.Dead){
									controller.actionStat = Controller.ActionStat.Move;
								}
							}
						}
					
						if(hit.collider.tag != "Player" && hit.collider.tag != "Enemy"){
							controller.target = null;
							controller.positionWay = hit.point;
							controller.actionStat = Controller.ActionStat.Move;
						}
						controller = null;
						lineRenderer.SetPosition(1,Vector3.zero);
						lineRenderer.enabled = false;
						Destroy(transformRootStart.gameObject);
						Destroy(transformRootEnd.gameObject);
						Destroy(transformLookEnd.gameObject);
						Destroy(circleRingEnd);
						Destroy(circleTouch);
					}	
				}
				break;
			}
		}
	}
	
	private Vector3 CirclePoint(Transform transformRoot, Transform transformLook,Vector3 center, Vector3 lookAtPos){
		transformRoot.transform.position = center;
		transformLook.transform.parent = transformRoot.transform;
		transformLook.transform.localPosition = new Vector3(0,0,0.45f);
		transformRoot.transform.LookAt(lookAtPos);
		return transformLook.transform.position;
	}
}
