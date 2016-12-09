using Unit;
using UnityEngine;
using G;

namespace Building
{
	public class BuildingC2 : Building {

		public BuildingC2()
		{
			m_SUnitName = "c2";
			m_SUnitType = UnitType.BUILDING_C2;
			m_ECurrentLevel = BuildingLevel.BASE_GROUND;
			m_CBuildingModel = null;
			InitWithSaveData ();
		}

		public override void onTouch ()
		{
			
		}
	}
}