using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Building;

public class UnitManager : MonoBehaviour {
	
	public GameObject BuildingPrefab;

	public List<GameObject> SpawnList;

	// Use this for initialization
	void Start () {
		
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
				Transform t = Pos [i].transform;
				if (Pos [i].gameObject.tag == "BuildingMax") {
					BuildingManager.LoadBuildingByType (ref BuildingPrefab, UnitType.BUILDING_MAX, ref t);
				}	
				else if (Pos [i].gameObject.tag == "BuildingC2")
				{
					BuildingManager.LoadBuildingByType (ref BuildingPrefab, UnitType.BUILDING_C2, ref t);
				}
				else if (Pos [i].gameObject.tag == "BuildingD2")
				{
					BuildingManager.LoadBuildingByType (ref BuildingPrefab, UnitType.BUILDING_D2, ref t);
				}
				else if (Pos [i].gameObject.tag == "BuildingBase")
				{
					BuildingManager.LoadBuildingByType (ref BuildingPrefab, UnitType.BUILDING_BASE, ref t);
				}
				else
				{
					int r = Random.Range (0, 10);
					if (r >= 0 && r < 3) {
						BuildingManager.LoadBuildingByType (ref BuildingPrefab, UnitType.BUILDING_D2, ref t);
					} else if (r >= 3 && r < 5) {
						BuildingManager.LoadBuildingByType (ref BuildingPrefab, UnitType.BUILDING_C2, ref t);
					}
					else if (r >= 5 && r < 7)
					{
						BuildingManager.LoadBuildingByType (ref BuildingPrefab, UnitType.BUILDING_BASE, ref t);
					}
					else {
						BuildingManager.LoadBuildingByType (ref BuildingPrefab, UnitType.BUILDING_MAX, ref t);
					}
				}
			}

			yield return 0;
		}
	}
}
