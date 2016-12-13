using Unit;
using UnityEngine;
using G;

namespace Building
{
	public class BuildingBase : Building {

		public BuildingBase()
		{
			m_SUnitName = "base";
			m_SUnitType = UnitType.BUILDING_BASE;
			m_ECurrentLevel = BuildingLevel.BASE_GROUND;
			m_CBuildingModel = null;
		}

		public override void onTouch (ref TapGesture e)
		{
			base.onTouch (ref e);
		}
	}
}