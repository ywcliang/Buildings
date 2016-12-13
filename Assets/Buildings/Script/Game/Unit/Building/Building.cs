using UnityEngine;
using System.Collections;
using Unit;
using G;
using Building;

public enum BuildingLevel
{
	BASE_GROUND,
	LEVEL_FIRST,
	LEVEL_SECOND,
	LEVEL_TOP
}

namespace Building
{
	public class Building : UnitBase
	{
		protected BuildingLevel m_ECurrentLevel;
		protected GameObject m_CBuildingModel;
		protected BuildingState m_CState;

		protected override void generateBoxCollider ()
		{
			gameObject.AddComponent<BoxCollider> ();

			BoxCollider bx = gameObject.GetComponent<BoxCollider> ();
			BoxCollider bxModel = m_CBuildingModel.GetComponent<BoxCollider> ();
			//find in children
			if (bxModel == null)
			{
				bxModel = m_CBuildingModel.GetComponentInChildren<BoxCollider> ();
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
			m_ECurrentLevel = BuildingLevel.BASE_GROUND;
			m_CBuildingModel = null;
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

		public void setPhase(BuildingLevel phase)
		{
			m_ECurrentLevel = phase;	
		}

		public BuildingLevel setPhase()
		{
			return m_ECurrentLevel;
		}


		public override void onTouch (ref TapGesture e)
		{
			DebugConsole.Log ("building  ontouch  " + m_SUnitName);
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
					obj = (GameObject)Resources.Load ("Prefabs/Unit/buildingC2");
					break;
				}
			case UnitType.BUILDING_D2:
				{
					obj = (GameObject)Resources.Load ("Prefabs/Unit/buildingD2");
					break;
				}
			case UnitType.BUILDING_MAX:
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
				m_CBuildingModel = Object.Instantiate(obj) as GameObject;
				m_CBuildingModel.SetActive (true);
				m_CBuildingModel.transform.rotation = transform.rotation;
				m_CBuildingModel.transform.position = transform.position;

				generateBoxCollider ();
			}
				
		}

		public virtual void InitWithSaveData(ref Transform t)
		{
			//init state info
			transform.position = t.position;
			transform.rotation = t.rotation;
			m_CState.setBuildInstance (this);
			m_CState.changeState (BuildingState.s_StatePrebuild);
		}

		void Start()
		{
			
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

		public virtual void pushCallback(int state, UnitState.StateCallback call)
		{
			m_CState.pushPreCallback (state, call);
		}

		public virtual void popCallback(int state, UnitState.StateCallback call)
		{
			m_CState.popPreCallback (state, call);
		}

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

		}

		//prebuild logic 
		public virtual void PreStateOnPre()
		{
			DebugConsole.Log ("PreStateOnPre","normal");
		}

		public virtual void PreStateOnChanging()
		{
			LoadingResource ();

			m_CState.changeState (BuildingState.s_StateProduce);
		}

		public virtual void PreStateOnChanged()
		{
			DebugConsole.Log ("PreStateOnChanged","error");
		}

		//constructing logic 
		public virtual void ConStateOnPre()
		{
			DebugConsole.Log ("ConStateOnPre","normal");
		}

		public virtual void ConStateOnChanging()
		{
			DebugConsole.Log ("ConStateOnChanging","waring");
		}

		public virtual void ConStateOnChanged()
		{
			DebugConsole.Log ("ConStateOnChanged","error");
		}

		//produce logic 
		public virtual void ProduceStateOnPre()
		{
			DebugConsole.Log ("ProduceStateOnPre","normal");
		}

		public virtual void ProduceStateOnChanging()
		{
			DebugConsole.Log ("ProduceStateOnChanging","waring");
		}

		public virtual void ProduceStateOnChanged()
		{
			DebugConsole.Log ("ProduceStateOnChanged","error");
		}

		//destory logic 
		public virtual void destoryStateOnPre()
		{
			DebugConsole.Log ("destoryStateOnPre","normal");
		}

		public virtual void destoryStateOnChanging()
		{
			DebugConsole.Log ("destoryStateOnChanging","waring");
		}

		public virtual void destoryStateOnChanged()
		{
			DebugConsole.Log ("destoryStateOnChanged","error");
		}
	}
}