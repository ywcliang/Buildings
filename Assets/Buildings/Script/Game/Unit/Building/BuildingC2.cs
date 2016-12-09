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
			m_CAnimate = null;
			LoadingResource ();
		}

		public override void onTouch ()
		{
			
		}
	}
}