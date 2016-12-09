using Unit;
using UnityEngine;
using G;

namespace Building
{
	public class BuildingD2 : Building {
		public BuildingD2()
		{
			m_SUnitName = "d2";
			m_SUnitType = UnitType.BUILDING_D2;
			m_ECurrentLevel = BuildingLevel.BASE_GROUND;
			m_CBuildingModel = null;
			InitWithSaveData ();
		}

		public override void onTouch ()
		{

		}
			
	}
}