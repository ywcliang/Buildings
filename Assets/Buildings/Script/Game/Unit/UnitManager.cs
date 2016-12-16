using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using Unit;

public class UnitManager : MonoBehaviour {
	public static UnitManager instance = null;

	public List<GameObject> SpawnList;

	public static GameObject s_UnitMgr_BuildPrefab = null;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		//preload build prefab
		s_UnitMgr_BuildPrefab = (GameObject)Resources.Load("Prefabs/Unit/BuildPrefab");

		//spawn buildings
		StartCoroutine(LoadBuildings(SpawnList));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator LoadBuildings(List<GameObject> Pos)
	{
		for (int i = 0; i < Pos.Count; ++i)
		{
			if (Pos [i] != null) {
//				Transform t = Pos [i].transform;
//				if (Pos [i].gameObject.tag == "BuildingMax") {
//					BuildingManager.LoadBuildingByType (UnitType.BUILDING_TOWN_HALL, t);
//				}	
//				else if (Pos [i].gameObject.tag == "BuildingC2")
//				{
//					BuildingManager.LoadBuildingByType (UnitType.BUILDING_C2, t);
//				}
//				else if (Pos [i].gameObject.tag == "BuildingD2")
//				{
//					BuildingManager.LoadBuildingByType (UnitType.BUILDING_D2, t);
//				}
//				else if (Pos [i].gameObject.tag == "BuildingBase")
//				{
//					BuildingManager.LoadBuildingByType (UnitType.BUILDING_BASE, t);
//				}
//				else
//				{
//					int r = Random.Range (0, 10);
//					if (r >= 0 && r < 3) {
//						BuildingManager.LoadBuildingByType (UnitType.BUILDING_D2, t);
//					} else if (r >= 3 && r < 5) {
//						BuildingManager.LoadBuildingByType (UnitType.BUILDING_C2, t);
//					}
//					else if (r >= 5 && r < 7)
//					{
//						BuildingManager.LoadBuildingByType (UnitType.BUILDING_BASE, t);
//					}
//					else {
//						BuildingManager.LoadBuildingByType (UnitType.BUILDING_TOWN_HALL, t);
//					}
//				}
				BuildingManager.LoadBuildingByType (UnitType.BUILDING_BASE, Pos[i].transform);
			}


			Destroy (Pos[i]);
			yield return 0;
		}
	}

	public void RemoveUnit(UnitBase unit)
	{
		//Destory (obj);
		Destroy(unit.getModel());
		Destroy(unit.gameObject);
	}
}
