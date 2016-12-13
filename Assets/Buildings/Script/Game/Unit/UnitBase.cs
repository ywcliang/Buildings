using System;
using G;
using Building;
using UnityEngine;

public enum UnitType
{
	DEFAULT,
	BUILDING_BASE,
	BUILDING_D2,
	BUILDING_C2,
	BUILDING_MAX,
	CAR
}

namespace Unit
{	
	public class UnitBase : MonoBehaviour, UnitBehaviour
	{
		public static UnitBase CreateUnit(ref GameObject pre, UnitType type)
		{
			//GameObject unit = (GameObject)Instantiate (pre);
			GameObject unit = new GameObject ();
			UnitBase bases = null;

			switch (type)
			{
			case UnitType.BUILDING_BASE:
				{
					unit.AddComponent<BuildingBase> ();
					break;
				}
			case UnitType.BUILDING_C2:
				{
					unit.AddComponent<BuildingC2> ();
					break;
				}
			case UnitType.BUILDING_D2:
				{
					unit.AddComponent<BuildingD2> ();
					break;
				}
			case UnitType.BUILDING_MAX:
				{
					unit.AddComponent<BuildingIMax> ();
					break;
				}
			case UnitType.CAR:
				{
					unit.AddComponent<UnitBase> ();
					break;
				}
			case UnitType.DEFAULT:
				{
					unit.AddComponent<UnitBase> ();
					break;
				}
			}
			bases = unit.GetComponent<UnitBase> ();
			bases.m_SUnitType = type;
			return bases;
		}

		public UnitBase ()
		{
			m_SUnitName = "";
			m_SUnitType = UnitType.DEFAULT;
			m_CBoxCollider = null;
		}

		//unit name
		protected string m_SUnitName { get;set;}
		//unit type
		protected UnitType m_SUnitType { get;set;}

		protected BoxCollider m_CBoxCollider;

		//make a 3d box collider for Unit,generally get from model if we have, or make a empty one
		virtual protected void generateBoxCollider(){
			m_CBoxCollider = new BoxCollider ();
		}

		virtual public BoxCollider getBoxCollider()
		{
			return m_CBoxCollider;
		}

		public virtual void onTouch(ref TapGesture e) {}
	}
}

