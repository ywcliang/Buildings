using System;
using G;
using Building;

namespace Unit
{	
	public class UnitBase : UnitBehaviour
	{
		public static UnitBase CreateUnit(UnitType type)
		{
			switch (type) {
			case UnitType.BUILDING_C2:
				{
					return new BuildingC2 ();
				}
			case UnitType.BUILDING_D2:
				{
					return new BuildingD2 ();
				}
			case UnitType.BUILDING_MAX:
				{
					return new BuildingIMax ();
				}
			case UnitType.CAR:
				{
					return null;
				}
			case UnitType.DEFAULT:
				{
					return null;
				}
			}
			return null;
		}

		public UnitBase ()
		{
			m_SUnitName = "";
			m_SUnitType = UnitType.DEFAULT;
		}

		//unit name
		protected string m_SUnitName { get;set;}
		//unit type
		protected UnitType m_SUnitType { get;set;}

		public virtual void onTouch() {}
	}
}

