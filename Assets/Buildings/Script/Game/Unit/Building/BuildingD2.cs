using Unit;
using UnityEngine;
using G;

namespace Buildings
{
	public class BuildingD2 : Building {
		public BuildingD2()
		{
			m_SUnitName = "buildingD2";
			m_SUnitType = UnitType.BUILDING_D2;
			m_ECurrentLevel = BuildingLevel.LEVEL_FIRST;
			m_CModel = null;
			m_CProduceRate = s_ProduceRate [1];
		}

		public override void onTouch (ref TapGesture e)
		{
			base.onTouch (ref e);
		}
			
	}
}