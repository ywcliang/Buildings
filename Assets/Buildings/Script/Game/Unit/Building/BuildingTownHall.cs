using Unit;
using UnityEngine;
using G;

namespace Buildings
{
	public class BuildingTownHall : Building {
		public BuildingTownHall()
		{
			m_SUnitName = "townhall";
			m_SUnitType = UnitType.BUILDING_TOWN_HALL;
			m_CModel = null;
			m_CProduceRate = s_ProduceRate [0];
			m_ECoinProduceType = CoinProduceType.TOUCH;
			m_ECurrentLevel = UnitLevel.LEVEL_ZERO;
		}

		public override void onTouch (ref TapGesture e)
		{
			base.onTouch (ref e);
			produceCoinTouch ();

			//m_CAnimatorController.StartConstruct ((int)m_ECurrentLevel);
		}

		public override void produceUpdate ()
		{
			base.produceUpdate ();
		}
	}
}