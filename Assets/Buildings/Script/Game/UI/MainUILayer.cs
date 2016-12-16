using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unit;

public class MainUILayer : MonoBehaviour {
	public Text m_CCoinText;
	private float m_FCoinUpdateInterval = 1;

	// Use this for initialization
	void Start () {
		StartCoroutine (CoinTextUpdate(m_FCoinUpdateInterval));
	}
	
	// Update is called once per frame
	void Update () {

		//UnitBase.s_TotalCoin.m_FCoinProduce
	}

	public IEnumerator CoinTextUpdate(float interval)
	{
		while (interval > 0)
		{
			interval -= Time.deltaTime;
			yield return 0;
		}
		if (UnitBase.s_TotalCoin.m_FCoinProduce > 0)
			m_CCoinText.text = UnitBase.s_TotalCoin.ToString ();
		yield return CoinTextUpdate (m_FCoinUpdateInterval);
	}
}
