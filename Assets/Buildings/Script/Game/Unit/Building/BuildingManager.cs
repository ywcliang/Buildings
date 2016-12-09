using System.Collections;
using System.Collections.Generic;
using G;
using UnityEngine;
using UnityEditor;

namespace Building
{
	public class BuildingManager
	{
		public static List<Building> s_BuildingList = new List<Building>();

		public static void LoadBuildings(ref List<GameObject> Pos)
		{
			for (int i = 0; i < Pos.Count; ++i)
			{
				if (Pos [i] != null) {
					PrefabType pre = PrefabUtility.GetPrefabType (Pos[i]);
					Transform t = Pos [i].transform;
					if (Pos [i].gameObject.tag == "BuildingMax") {
						LoadBuildingByType (UnitType.BUILDING_MAX, ref t);
					}	
					else if (Pos [i].gameObject.tag == "BuildingC2")
					{
						LoadBuildingByType (UnitType.BUILDING_C2, ref t);
					}
					else if (Pos [i].gameObject.tag == "BuildingD2")
					{
						LoadBuildingByType (UnitType.BUILDING_D2, ref t);
					}
					else
					{
						int r = Random.Range (0, 10);
						if (r >= 0 && r < 3) {
							LoadBuildingByType (UnitType.BUILDING_D2, ref t);
						} else if (r >= 3 && r < 7) {
							LoadBuildingByType (UnitType.BUILDING_C2, ref t);
						} else {
							LoadBuildingByType (UnitType.BUILDING_MAX, ref t);
						}
					}
				}
			}
		}

		public static void LoadBuildingByType(UnitType type, ref Transform t)
		{
			Building b = (Building)Building.CreateUnit (type);
			if (b != null) {
				b.SetTransform (t);
				s_BuildingList.Add (b);
			}
		}

		public static void RemoveBuilding(ref Building building)
		{
			if (s_BuildingList.Count != 0) {
				s_BuildingList.Remove (building);
			}
		}
	}
}

