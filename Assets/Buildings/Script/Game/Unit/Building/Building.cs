using UnityEngine;
using System.Collections;
using Unit;
using G;

namespace Building
{
	public class Building : UnitBase
	{
		protected GameObject m_CAnimate;

		public Transform getTransform ()
		{
			return m_CAnimate.transform;  
		}

		public void SetTransform (Transform value)
		{
			m_CAnimate.transform.position = value.transform.position;
			m_CAnimate.transform.rotation = value.transform.rotation;
		}

		public Building ()
		{
			m_SUnitName = "buding";
			m_SUnitType = UnitType.DEFAULT;
			m_CAnimate = null;
			LoadingResource ();
		}

		public override void onTouch ()
		{

		}

		protected virtual void LoadingResource ()
		{
			GameObject obj = null;
			switch (m_SUnitType) {
			case UnitType.BUILDING_C2:
				{
					obj = (GameObject)Resources.Load ("Prefabs/buildingC2");
					break;
				}
			case UnitType.BUILDING_D2:
				{
					obj = (GameObject)Resources.Load ("Prefabs/buildingD2");
					break;
				}
			case UnitType.BUILDING_MAX:
				{
					obj = (GameObject)Resources.Load ("Prefabs/IMAX");
					break;
				}
			case UnitType.DEFAULT:
				{
					obj = null;
					break;
				}

			}
			if (obj) {
				m_CAnimate = Object.Instantiate(obj) as GameObject;
				m_CAnimate.SetActive (true);
			}
				
		}
	}
}