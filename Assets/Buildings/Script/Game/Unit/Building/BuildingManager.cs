using System.Collections;
using System.Collections.Generic;
using G;
using UnityEngine;
using Unit;

namespace Buildings
{
	public class BuildingManager
	{
		public static List<Building> s_BuildingList = new List<Building>();

//		public static IEnumerator LoadBuildings(List<GameObject> Pos)
//		{
//			for (int i = 0; i < Pos.Count; ++i)
//			{
//				if (Pos [i] != null) {
//					Transform t = Pos [i].transform;
//					if (Pos [i].gameObject.tag == "BuildingMax") {
//						LoadBuildingByType (UnitType.BUILDING_MAX, ref t);
//					}	
//					else if (Pos [i].gameObject.tag == "BuildingC2")
//					{
//						LoadBuildingByType (UnitType.BUILDING_C2, ref t);
//					}
//					else if (Pos [i].gameObject.tag == "BuildingD2")
//					{
//						LoadBuildingByType (UnitType.BUILDING_D2, ref t);
//					}
//					else if (Pos [i].gameObject.tag == "BuildingBase")
//					{
//						LoadBuildingByType (UnitType.BUILDING_BASE, ref t);
//					}
//					else
//					{
//						int r = Random.Range (0, 10);
//						if (r >= 0 && r < 3) {
//							LoadBuildingByType (UnitType.BUILDING_D2, ref t);
//						} else if (r >= 3 && r < 5) {
//							LoadBuildingByType (UnitType.BUILDING_C2, ref t);
//						}
//						else if (r >= 5 && r < 7)
//						{
//							LoadBuildingByType (UnitType.BUILDING_BASE, ref t);
//						}
//						else {
//							LoadBuildingByType (UnitType.BUILDING_MAX, ref t);
//						}
//					}
//				}
//
//				yield return 0;
//			}
//		}

		public static void LoadBuildingByType(UnitType type, Transform t)
		{
			Building b = (Building)Building.CreateUnit (type, ref t);
			if (b != null) {
				b.InitWithSaveData ();
				s_BuildingList.Add (b);
			}
		}

		public static void RemoveBuilding(Building building)
		{
			if (s_BuildingList.Count != 0) {
				//building.DestorySelf ();
				UnitManager.instance.RemoveUnit (building);

			}
		}

		public static void RevmoveAllBuilding()
		{
			if (s_BuildingList.Count != 0) {
				for (int i = 0; i < s_BuildingList.Count; ++i)
				{
					//s_BuildingList [i].DestorySelf ();

					UnitManager.instance.RemoveUnit(s_BuildingList [i]);
				}
				s_BuildingList.Clear ();
			}	
		}

		public static void Update()
		{

		}
	}
}

