using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Building;

public class Ground : MonoBehaviour {

	private Vector2 m_CGroundSize;
	public List<GameObject> buildings;
	 

	void Awake()
	{
		Main.getMainIns ().SetGround (this);
		m_CGroundSize = gameObject.transform.GetComponent<MeshCollider>().transform.localScale;

	}

	// Use this for initialization
	void Start () {
		
//		string strNeed = string.Format("{0}  {1}  {2}","ground size   x   " + center.x,"ground size  y" + center.y,"ground size  z" + center.z);
//		DebugConsole.Log (strNeed, "normal" );

		//spawn buildings 
		BuildingManager.LoadBuildings(ref buildings);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetSize(Vector2 v)
	{
		m_CGroundSize = v;
	}

	public Vector2 GetSize ()
	{
		return m_CGroundSize;
	}
}
