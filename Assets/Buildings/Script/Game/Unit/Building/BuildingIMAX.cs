using Unit;
using UnityEngine;
using G;

namespace Building
{
	public class BuildingIMax : Building {
		public BuildingIMax()
		{
			m_SUnitName = "max";
			m_SUnitType = UnitType.BUILDING_MAX;
			m_ECurrentLevel = BuildingLevel.BASE_GROUND;
		}

		public override void onTouch (ref TapGesture e)
		{
			base.onTouch (ref e);
		}

	}
}