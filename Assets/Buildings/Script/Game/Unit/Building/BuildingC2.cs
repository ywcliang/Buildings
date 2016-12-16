using Unit;
using UnityEngine;
using G;

namespace Buildings
{
	public class BuildingC2 : Building {

		public BuildingC2()
		{
			m_SUnitName = "HouseA_Prefab";
			m_SUnitType = UnitType.BUILDING_C2;
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