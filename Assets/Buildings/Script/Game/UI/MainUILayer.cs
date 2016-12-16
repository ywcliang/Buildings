using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unit;

public class MainUILayer : MonoBehaviour {
	public Text m_CCoinText;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		m_CCoinText.text = UnitBase.s_TotalCoin.ToString ();
		//UnitBase.s_TotalCoin.m_FCoinProduce
	}
}
