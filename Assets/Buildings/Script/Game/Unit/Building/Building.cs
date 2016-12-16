using UnityEngine;
using System.Collections;
using Unit;
using G;
using Buildings;

public enum BuildingLevel
{
	BASE_GROUND,
	LEVEL_FIRST,
	LEVEL_SECOND,
	LEVEL_TOP
}

namespace Buildings
{
	public class Building : UnitBase
	{
		protected BuildingLevel m_ECurrentLevel;
		protected BuildingState m_CState;


		protected override void generateBoxCollider ()
		{
			gameObject.AddComponent<BoxCollider> ();

			BoxCollider bx = gameObject.GetComponent<BoxCollider> ();
			BoxCollider bxModel = m_CModel.GetComponent<BoxCollider> ();
			//find in children
			if (bxModel == null)
			{
				bxModel = m_CModel.GetComponentInChildren<BoxCollider> ();
			}

			bx.center = bxModel.center;
			bx.size = bxModel.size;
			bx.transform.position = bxModel.transform.position;
			bx.transform.rotation = bxModel.transform.rotation;
			Destroy (bxModel);
		}

		public Building ()
		{
			m_SUnitName = "buding";
			m_SUnitType = UnitType.DEFAULT;
			m_ECurrentLevel = BuildingLevel.LEVEL_FIRST;
			m_CModel = null;
			m_CState = new BuildingState ();

			//init callbacks
			initCallbacks ();
		}

		~Building()
		{
			//release callbacks
			unInitCallbacks ();
			m_CState = null;
		}

		public void setLevelPhase(BuildingLevel phase)
		{
			m_ECurrentLevel = phase;	
		}

		public BuildingLevel getLevelPhase()
		{
			return m_ECurrentLevel;
		}


		public override void onTouch (ref TapGesture e)
		{
			//DebugConsole.Log ("building  ontouch  " + m_SUnitName);
		}

		protected virtual void LoadingResource ()
		{
			GameObject obj = null;
			switch (m_SUnitType) {
			case UnitType.BUILDING_BASE:
				{
					obj = (GameObject)Resources.Load ("Prefabs/Unit/War");
					break;
				}
			case UnitType.BUILDING_C2:
				{
					obj = (GameObject)Resources.Load ("Prefabs/Unit/HouseA_Prefab");
					break;
				}
			case UnitType.BUILDING_D2:
				{
					obj = (GameObject)Resources.Load ("Prefabs/Unit/buildingD2");
					break;
				}
			case UnitType.BUILDING_TOWN_HALL:
				{
					obj = (GameObject)Resources.Load ("Prefabs/Unit/IMAX");
					break;
				}
			case UnitType.DEFAULT:
				{
					obj = null;
					break;
				}

			}

			if (obj) {
				m_CModel = Object.Instantiate(obj) as GameObject;
				m_CModel.SetActive (true);
				m_CModel.transform.rotation = transform.rotation;
				m_CModel.transform.position = transform.position;

				generateBoxCollider ();
			}
				
		}

		public virtual void InitWithSaveData(ref Transform t)
		{
			//init state info
			transform.position = t.position;
			transform.rotation = t.rotation;
			m_CState.setBuildInstance (this);
		}

		void Start()
		{
			//give object unit name
			gameObject.name = m_SUnitName;
		}

		void Update()
		{
			m_CState.update ();

		}

		public virtual void DestorySelf()
		{
			m_CState.changeState (BuildingState.s_StateDestory);
			//release callbacks
			//unInitCallbacks ();
		}

//		public virtual void pushCallback<T>(int state, T call)
//		{
//			m_CState.pushPreCallback (state, call);
//		}
//
//		public virtual void popCallback<T>(int state, T call)
//		{
//			m_CState.popPreCallback (state, call);
//		}

		public virtual void initCallbacks()
		{
			//pre
			m_CState.pushPreCallback (BuildingState.s_StatePrebuild, PreStateOnPre);
			m_CState.pushChangingCallback (BuildingState.s_StatePrebuild, PreStateOnChanging);
			m_CState.pushBeenChangedCallback (BuildingState.s_StatePrebuild, PreStateOnChanged);

			//construct
			m_CState.pushPreCallback (BuildingState.s_StateConstruct, ConStateOnPre);
			m_CState.pushChangingCallback (BuildingState.s_StateConstruct, ConStateOnChanging);
			m_CState.pushBeenChangedCallback (BuildingState.s_StateConstruct, ConStateOnChanged);

			//produce
			m_CState.pushPreCallback (BuildingState.s_StateProduce, ProduceStateOnPre);
			m_CState.pushChangingCallback (BuildingState.s_StateProduce, ProduceStateOnChanging);
			m_CState.pushBeenChangedCallback (BuildingState.s_StateProduce, ProduceStateOnChanged);

			//destory
			m_CState.pushPreCallback (BuildingState.s_StateDestory, destoryStateOnPre);
			m_CState.pushChangingCallback (BuildingState.s_StateDestory, destoryStateOnChanging);
			m_CState.pushBeenChangedCallback (BuildingState.s_StateDestory, destoryStateOnChanged);

			//update
			m_CState.pushUpdateCallback (BuildingState.s_StatePrebuild, preUpdate);
			m_CState.pushUpdateCallback (BuildingState.s_StateConstruct, constructUpdate);
			m_CState.pushUpdateCallback (BuildingState.s_StateProduce, produceUpdate);
			m_CState.pushUpdateCallback (BuildingState.s_StateDestory, destoryUpdate);
		}

		public virtual void unInitCallbacks()
		{
			//pre
			m_CState.popPreCallback (BuildingState.s_StatePrebuild, PreStateOnPre);
			m_CState.popChangingCallback (BuildingState.s_StatePrebuild, PreStateOnChanging);
			m_CState.popBeenChangedCallback (BuildingState.s_StatePrebuild, PreStateOnChanged);

			//construct
			m_CState.popPreCallback (BuildingState.s_StateConstruct, ConStateOnPre);
			m_CState.popChangingCallback (BuildingState.s_StateConstruct, ConStateOnChanging);
			m_CState.popBeenChangedCallback (BuildingState.s_StateConstruct, ConStateOnChanged);

			//produce
			m_CState.popPreCallback (BuildingState.s_StateProduce, ProduceStateOnPre);
			m_CState.popChangingCallback (BuildingState.s_StateProduce, ProduceStateOnChanging);
			m_CState.popBeenChangedCallback (BuildingState.s_StateProduce, ProduceStateOnChanged);

			//destory
			m_CState.popPreCallback (BuildingState.s_StateDestory, destoryStateOnPre);
			m_CState.popChangingCallback (BuildingState.s_StateDestory, destoryStateOnChanging);
			m_CState.popBeenChangedCallback (BuildingState.s_StateDestory, destoryStateOnChanged);

			//update
			m_CState.popUpdateCallback (BuildingState.s_StatePrebuild, preUpdate);
			m_CState.popUpdateCallback (BuildingState.s_StateConstruct, constructUpdate);
			m_CState.popUpdateCallback (BuildingState.s_StateProduce, produceUpdate);
			m_CState.popUpdateCallback (BuildingState.s_StateDestory, destoryUpdate);

		}

		//prebuild logic 
		public virtual void PreStateOnPre()
		{
			//DebugConsole.Log ("PreStateOnPre","normal");
		}

		public virtual void PreStateOnChanging()
		{
			//state controled by BuildBehaviour,so first time load model automaticly.
			if (m_CModel == null) {
				LoadingResource ();
				if (m_CAnimatorController != null)
					m_CAnimatorController.PreloadOver ();
			}
		}

		public virtual void PreStateOnChanged()
		{
			//DebugConsole.Log ("PreStateOnChanged","error");
		}

		//constructing logic 
		public virtual void ConStateOnPre()
		{
			//DebugConsole.Log ("ConStateOnPre","normal");

		}

		public virtual void ConStateOnChanging()
		{
			m_ECurrentLevel++;
			//DebugConsole.Log ("ConStateOnChanging","waring");
		}

		public virtual void ConStateOnChanged()
		{
			//DebugConsole.Log ("ConStateOnChanged","error");
		}

		//produce logic 
		public virtual void ProduceStateOnPre()
		{
			//DebugConsole.Log ("ProduceStateOnPre","normal");
		}

		public virtual void ProduceStateOnChanging()
		{
			//DebugConsole.Log ("ProduceStateOnChanging","waring");
		}

		public virtual void ProduceStateOnChanged()
		{
			//DebugConsole.Log ("ProduceStateOnChanged","error");
		}

		//destory logic 
		public virtual void destoryStateOnPre()
		{
			//DebugConsole.Log ("destoryStateOnPre","normal");
		}

		public virtual void destoryStateOnChanging()
		{
			//DebugConsole.Log ("destoryStateOnChanging","waring");
		}

		public virtual void destoryStateOnChanged()
		{
			//DebugConsole.Log ("destoryStateOnChanged","error");
		}

		//state update
		//pre
		public virtual void preUpdate()
		{
			//DebugConsole.Log ("destoryStateOnPre","normal");
		}

		//construct
		public virtual void constructUpdate()
		{
			//DebugConsole.Log ("destoryStateOnChanging","waring");
		}

		//produce
		public virtual void produceUpdate()
		{
			//DebugConsole.Log ("destoryStateOnChanged","error");
			produceCoinAuto();
		}

		//destory
		public virtual void destoryUpdate()
		{
			//DebugConsole.Log ("destoryStateOnChanged","error");
		}
	
		// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
		override public void OnStateEnter(ref Animator animator, ref AnimatorStateInfo stateInfo, ref int layerIndex) {
			if (stateInfo.IsName (G.GlobalDef.s_GBuildAnimator_Prebuild_Doing)){
				m_CState.changeState (BuildingState.s_StatePrebuild);
			} else if(stateInfo.IsName (G.GlobalDef.s_GBuildAnimator_Construct_Doing)) {
				m_CState.changeState (BuildingState.s_StateConstruct);
			} else if (stateInfo.IsName (G.GlobalDef.s_GBuildAnimator_Produce_Doing)){
				m_CState.changeState (BuildingState.s_StateProduce);
			}
			else if (stateInfo.IsName (G.GlobalDef.s_GBuildAnimator_Destory_Doing)){
				m_CState.changeState (BuildingState.s_StateDestory);
			}
		}

		// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
//		override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
//		
//			}

		//OnStateExit is called when a transition ends and the state machine finishes evaluating this state
		override public void OnStateExit(ref Animator animator, ref AnimatorStateInfo stateInfo, ref int layerIndex) {
			if (stateInfo.IsName (G.GlobalDef.s_GBuildAnimator_Prebuild_Doing)){
				int dd = 123;
			} else if (stateInfo.IsName (G.GlobalDef.s_GBuildAnimator_Construct_Doing)) {
				int dd = 123;
			} else if (stateInfo.IsName (G.GlobalDef.s_GBuildAnimator_Produce_Doing)){
				int dd = 123;
			}
			else if (stateInfo.IsName (G.GlobalDef.s_GBuildAnimator_Destory_Doing)){
				int dd = 123;
			}
		}

	}
}