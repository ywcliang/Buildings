//using UnityEngine;
//using System.Collections;
//
//public class MeshSquare : MonoBehaviour
//{
//	public Material material;
//	// Use this for initialization
//	void Start ()
//	{
//		int i = 0;
//		GameObject go = GetSquare(50, 50, material);
//		go.transform.position = new Vector3(0, 0, 55 * i++);
//
//		//go = GetSquare(50, 50, Color.black, material);
//		//go.transform.position = new Vector3(0, 0, 55 * i++);
//
//		GetTriangle(50, material);
//	}
//
//	/// <summary>
//	/// 获取一个正方形
//	/// </summary>
//	/// <returns></returns>
//	public GameObject GetSquare(int width, int height, Material material)
//	{
//		GameObject go = new GameObject("Square");
//		MeshFilter filter = go.AddComponent<MeshFilter>();
//		Mesh mesh = new Mesh();
//		filter.sharedMesh = mesh;
//		mesh.vertices = new Vector3[6] { new Vector3(0, 0, 0), new Vector3(0, 0, height), new Vector3(width, 0, 0), new Vector3(width, 0, height), new Vector3(width, 0, 0), new Vector3(0, 0, height) };
//		//mesh.colors = new Color[6] { color, color, color, color, color, color }; //设置每个顶点颜色
//		//mesh.uv = new Vector2[6] { new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 1), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 0) };
//		mesh.triangles = new int[6] { 0, 1, 2, 3, 4, 5 };
//		mesh.RecalculateNormals();
//		mesh.RecalculateBounds();
//
//		MeshRenderer render = go.AddComponent<MeshRenderer>();
//		//Material mate = new Material(Shader.Find("Diffuse"));
//		//mate.SetColor("_Color", color);
//		//material.SetTexture("_MainTex", texture);
//		render.sharedMaterial = material;
//
//		return go;
//	}
//
//	/// <summary>
//	/// 获取一个三角形
//	/// </summary>
//	public GameObject GetTriangle(int size, Material material)
//	{
//		GameObject go = new GameObject("Triangle");
//		MeshFilter filter = go.AddComponent<MeshFilter>();
//		Mesh mesh = new Mesh();
//		filter.sharedMesh = mesh;
//		mesh.vertices = new Vector3[3] { new Vector3(0, 0, 0), new Vector3(0, 0, size), new Vector3(size, 0, 0)};
//		//mesh.colors = new Color[3] { color, color, color };   //设置每个顶点颜色
//		//mesh.uv = new Vector2[3] { new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 1) };
//		mesh.triangles = new int[3] { 0, 1, 2 };
//		mesh.RecalculateNormals();
//		mesh.RecalculateBounds();
//
//		MeshRenderer render = go.AddComponent<MeshRenderer>();
//		//Material material = new Material(Shader.Find("Diffuse"));
//		//material.SetColor("_Color", color);
//		//material.SetTexture("_MainTex", texture);
//		render.sharedMaterial = material;
//
//		return go;
//	}
//}