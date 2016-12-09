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

		public Transform getTransform ()
		{
			return m_CBuildingModel.transform;  
		}

		public void SetTransform (Transform value)
		{
			m_CBuildingModel.transform.position = value.transform.position;
			m_CBuildingModel.transform.rotation = value.transform.rotation;
		}

		public Building ()
		{
			m_SUnitName = "buding";
			m_SUnitType = UnitType.DEFAULT;
			m_ECurrentLevel = BuildingLevel.BASE_GROUND;
			m_CBuildingModel = null;
			m_CState = new BuildingState ();

			InitWithSaveData ();
		}

		public void setPhase(BuildingLevel phase)
		{
			m_ECurrentLevel = phase;	
		}

		public BuildingLevel setPhase()
		{
			return m_ECurrentLevel;
		}


		public override void onTouch ()
		{

		}

		protected virtual void LoadingResource ()
		{
			GameObject obj = null;
			switch (m_SUnitType) {
			case UnitType.BUILDING_C2:
				{
					obj = (GameObject)Resources.Load ("Prefabs/buildingC2");
					break;
				}
			case UnitType.BUILDING_D2:
				{
					obj = (GameObject)Resources.Load ("Prefabs/buildingD2");
					break;
				}
			case UnitType.BUILDING_MAX:
				{
					obj = (GameObject)Resources.Load ("Prefabs/IMAX");
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
				m_CState.changeState (BuildingState.statePrebuild);
			}
				
		}

		public void InitWithSaveData()
		{
			LoadingResource ();
		}

		public void update()
		{
			m_CState.update ();
		}
	}
}