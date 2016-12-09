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
			LoadingResource ();
		}

		public override void onTouch ()
		{

		}

	}
}