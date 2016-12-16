using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using G;

public struct MapCell
{
	public Vector3 position;
}

public class Ground : MonoBehaviour {
	public static Ground instance = null;

	private Vector2 m_CGroundSize;
	private MapCell[,] m_SCells;

	void Awake()
	{
		instance = this;
		m_CGroundSize = new Vector2(gameObject.transform.GetComponent<BoxCollider> ().size.x, gameObject.transform.GetComponent<BoxCollider> ().size.z);
		//m_CGroundSize = gameObject.transform.GetComponent<MeshCollider>().transform.localScale;
		InitCells ();
	}

	// Use this for initialization
	void Start () {
		
//		string strNeed = string.Format("{0}  {1}  {2}","ground size   x   " + center.x,"ground size  y" + center.y,"ground size  z" + center.z);
//		DebugConsole.Log (strNeed, "normal" );


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

	private void InitCells()
	{
		int widthCount = (int)(m_CGroundSize.x / GlobalDef.MapCellWidth);
		int heightCount = (int)(m_CGroundSize.y / GlobalDef.MapCellHeight);
		//ground left bottom position (index 0,0)
		Vector3 MapLBPos = new Vector3 (transform.position.x - m_CGroundSize.x * 0.5f, 
										transform.position.y, 
										transform.position.z - m_CGroundSize.y * 0.5f
		);

		m_SCells = new MapCell[widthCount,heightCount];
		for(int i = 0; i < widthCount; ++i)
		{
			for(int j = 0; j < widthCount; ++j)
			{
				m_SCells [i,j] = new MapCell ();
				m_SCells [i,j].position = new Vector3 (	MapLBPos.x + i * GlobalDef.MapCellWidth,
														MapLBPos.y,
														MapLBPos.z + j * GlobalDef.MapCellHeight
				);
				//if in center
				if (GlobalDef.MapAnchorInCenter) {
					m_SCells [i,j].position += new Vector3 (GlobalDef.MapCellWidth * 0.5f, 0, GlobalDef.MapCellHeight * 0.5f);
				}
			}
		}
	}

	public MapCell getCellByCord(Vector2 pos)
	{
		if (pos.x >= m_SCells.GetLength (0) || pos.y > m_SCells.GetLength (1))
			return new MapCell ();
		return m_SCells [(int)pos.x, (int)pos.y];
	}


//	public GameObject GetSquare(Vector3 pos)
//	{
//		GameObject go = new GameObject("Square");
//		MeshFilter filter = go.AddComponent<MeshFilter>();
//		Mesh mesh = new Mesh();
//		filter.sharedMesh = mesh;
//		mesh.vertices = new Vector3[6] { new Vector3(pos.x, pos.y, pos.z), 
//			new Vector3(pos.x, pos.y, pos.z + GlobalDef.MapCellHeight), 
//			new Vector3(pos.x + GlobalDef.MapCellWidth, 0, 0), 
//			new Vector3(pos.x + GlobalDef.MapCellWidth, 0, pos.y + GlobalDef.MapCellHeight), 
//			new Vector3(pos.x + GlobalDef.MapCellWidth, 0, 0), 
//			new Vector3(0, 0, pos.y + GlobalDef.MapCellHeight) };
//
//		//mesh.vertices = new Vector3[6] { new Vector3(0, 0, 0), new Vector3(0, 0, height), new Vector3(width, 0, 0), new Vector3(width, 0, height), new Vector3(width, 0, 0), new Vector3(0, 0, height) };
//		//mesh.colors = new Color[6] { color, color, color, color, color, color }; //设置每个顶点颜色
//		//mesh.uv = new Vector2[6] { new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 1), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 0) };
//		mesh.triangles = new int[6] { 0, 1, 2, 3, 4, 5 };
//
//		mesh.RecalculateNormals();
//		mesh.RecalculateBounds();
//
//		MeshRenderer render = go.AddComponent<MeshRenderer>();
//		//Material mate = new Material(Shader.Find("Diffuse"));
//		//mate.SetColor("_Color", Color.green);
//		//material.SetTexture("_MainTex", texture);
//		drawMaterial.SetColor("_Color", Color.green);
//		render.sharedMaterial = drawMaterial;
//		go.transform.position = pos;
//		go.SetActive (true);
//
//		return go;
//	}
}
