using System;
using G;
using Buildings;
using UnityEngine;

namespace Unit
{
	
public enum UnitType
{
	DEFAULT,
	BUILDING_BASE,
	BUILDING_D2,
	BUILDING_C2,
	BUILDING_TOWN_HALL,
	CAR
}

public enum CoinProduceType
{
	TOUCH,
	AUTO,
	Miraculous
}
	
public enum CoinUnit
{
	NULL = 0,
	A,
	B,
	C,
	D,
	E,
	F,
	G,
	H,
	I,
	J,
	K,
	L,
	M,
	N,
	O,
	P,
	Q,
	R,
	S,
	T,
	U,
	V,
	W,
	X,
	Y,
	Z
}

public struct CoinBundle
{
	//coin produce (show in ui)
	public float m_FCoinProduce;
	//current coin unit in single unit
	public CoinUnit m_ECurrentUnit;

	//use for calculate
	public float[] m_AFTotal;

	//index 和ratecount为了配置每秒产出率
	public CoinBundle(float coins = 0, CoinUnit u = CoinUnit.NULL, int index = 0, float RateCount = 0)
	{
		m_FCoinProduce = coins;
		m_ECurrentUnit = u;
		m_AFTotal = new float[(int)CoinUnit.Z + 1];
		m_AFTotal [index] = RateCount;
	}

	public CoinBundle(CoinBundle b)
	{
		m_FCoinProduce = b.m_FCoinProduce;
		m_ECurrentUnit = b.m_ECurrentUnit;
		m_AFTotal = new float[(int)CoinUnit.Z + 1];
		for (int i = 0; i <= (int)CoinUnit.Z; ++i) {
			m_AFTotal [i] = b.m_AFTotal [i];
		}
	}

	public static CoinBundle CalculateAdd(ref CoinBundle re, ref CoinBundle des)
	{
		CoinBundle b = new CoinBundle(re);
		for (int i = 0; i <= (int)CoinUnit.Z; ++i)
		{
			//no need to add 0 values,des.m_ECurrentUnit is highest unit CoinBundle can provide
			if (i > (int)des.m_ECurrentUnit)
				break;

			b.m_AFTotal [i] += des.m_AFTotal [i];

			//if value bigger than Unit.UnitBase.s_UnitPower
			float values = b.m_AFTotal [i];
			int j = 1;
			while (values >= Unit.UnitBase.s_UnitPower)
			{
					if ((i + j) > (int)CoinUnit.Z)
					break;
				float d = b.m_AFTotal [i] - Unit.UnitBase.s_UnitPower;
				b.m_AFTotal [i] = d;
				b.m_AFTotal [i + j] += 1;
				values = b.m_AFTotal [i + j];
				++j;
			}
		}

		//change show coin count
		bool bigger = re >= des;
		int distance = Mathf.Abs (b.m_ECurrentUnit - des.m_ECurrentUnit);
		if (distance <= 1) {
			int factor = Unit.UnitBase.s_UnitPower / (distance == 0 ? Unit.UnitBase.s_UnitPower : 1);

			if (bigger) {
				b.m_FCoinProduce = b.m_FCoinProduce + des.m_FCoinProduce / factor;
			} else {
				b.m_FCoinProduce = b.m_FCoinProduce / factor + des.m_FCoinProduce;
			}

			//if grater than 1000
			if (b.m_FCoinProduce > Unit.UnitBase.s_UnitPower) {
				b.m_FCoinProduce /= Unit.UnitBase.s_UnitPower;
				b.m_ECurrentUnit++;
			}

		} else {
			b.m_FCoinProduce = bigger ? re.m_FCoinProduce : des.m_FCoinProduce;
			b.m_ECurrentUnit = bigger ? re.m_ECurrentUnit : des.m_ECurrentUnit;
		}
		return b;
	}

	public static CoinBundle CalculateSub(ref CoinBundle re, ref CoinBundle des)
	{
		CoinBundle b = new CoinBundle(re);
		// if re lower than des then we shouldn't sub
		bool lower = b < des;
		if (lower) {
			b.m_FCoinProduce = 0;
			b.m_ECurrentUnit = CoinUnit.NULL;
			for (int i = 0; i <= (int)CoinUnit.Z; ++i)
			{
				b.m_AFTotal [i] = 0;
			}
			DebugConsole.Log ("CoinBundle should't sub !!!!!!!!!!!!!!!!","error");
			return b;
		}

		for (int i = 0; i <= (int)CoinUnit.Z; ++i)
		{
			b.m_AFTotal [i] -= des.m_AFTotal [i];

			//if value lowwer than 0
			float values = b.m_AFTotal [i];
			int j = 1;
			while (values < 0)
			{
				float d = values / Unit.UnitBase.s_UnitPower;
				b.m_AFTotal [i] = 0;
				b.m_AFTotal [i + j] -= 1 + d;
				values = b.m_AFTotal [i + j];
				++j;
			}
		}

		//change show coin count
		int distance = Mathf.Abs (b.m_ECurrentUnit - des.m_ECurrentUnit);
		if (distance <= 1) {
			int factor = Unit.UnitBase.s_UnitPower / (distance == 0 ? Unit.UnitBase.s_UnitPower : 1);

			b.m_FCoinProduce = b.m_FCoinProduce - des.m_FCoinProduce / factor;
			//if lower than 0
			if (b.m_FCoinProduce < 0) 
			{
				b.m_FCoinProduce *= Unit.UnitBase.s_UnitPower;
				b.m_ECurrentUnit -= 1;
			}
		}

		return b;
	}

	public override string ToString ()
	{
		string s = "";
		if (m_ECurrentUnit == CoinUnit.NULL) {
		
		} else {
			s = m_ECurrentUnit.ToString();
		}
		s = string.Format ("{0:##.000} " + s, m_FCoinProduce);
		return s;
	}

	public static CoinBundle operator +(CoinBundle re, CoinBundle des)
	{
		return CalculateAdd (ref re, ref des);
	}

	public static CoinBundle operator -(CoinBundle re, CoinBundle des)
	{
		return CalculateSub (ref re, ref des);
	}

	public static CoinBundle operator *(CoinBundle re, float factor)
	{
		CoinBundle b = new CoinBundle(re);

		if (factor == 0) {
			return b;
		}

			if (factor >= 1) {
				
				CoinUnit biggest = b.m_ECurrentUnit;
				float biggestCoin = b.m_FCoinProduce;
				for (int i = 0; i <= (int)CoinUnit.Z; ++i) {
					b.m_AFTotal [i] *= factor;

					float values = b.m_AFTotal [i];
					int j = 1;
					while (values >= Unit.UnitBase.s_UnitPower) {
						float d = values / Unit.UnitBase.s_UnitPower;
						//remain value
						b.m_AFTotal [i] = values - d * Unit.UnitBase.s_UnitPower;
						b.m_AFTotal [i + j] += d;
						values = b.m_AFTotal [i + j];
						++j;
					}
					if (biggest < (CoinUnit)i && b.m_AFTotal [i] > 0) {
						biggest = (CoinUnit)i;
						biggestCoin = b.m_AFTotal [i];
					}
				}

				b.m_ECurrentUnit = biggest;
				b.m_FCoinProduce = biggestCoin;

			} else {
				
				CoinUnit biggest = b.m_ECurrentUnit;
				float biggestCoin = b.m_FCoinProduce;
				for (int i = (int)CoinUnit.Z; i >= 0; --i) {
					b.m_AFTotal [i] *= factor;

					bool down = false;
					float values = b.m_AFTotal [i];
					int j = -1;
					while (values < 1.0f / Unit.UnitBase.s_UnitPower && values > 0) {
						if (i + j < 0)
							break;
						float d = values * Unit.UnitBase.s_UnitPower;
						//remain value
						b.m_AFTotal [i] = 0;
						b.m_AFTotal [i + j] += d;
						values = b.m_AFTotal [i + j];
						--j;
						down = true;
					}
					if (down) {
						biggest--;
						biggestCoin = b.m_AFTotal [i];
					}
				}

				b.m_ECurrentUnit = biggest;
				b.m_FCoinProduce = biggestCoin;

			}
		
		return b;
	}

	public static CoinBundle operator /(CoinBundle re, float factor)
	{
		return re * (1.0f / factor);
	}

	public static bool operator >(CoinBundle re, CoinBundle des)
	{
		bool condition1 = (re.m_ECurrentUnit == des.m_ECurrentUnit) && (re.m_FCoinProduce > des.m_FCoinProduce);

		bool condition2 = (re.m_ECurrentUnit == des.m_ECurrentUnit + 1) && (re.m_FCoinProduce * Unit.UnitBase.s_UnitPower > des.m_FCoinProduce);

		bool condition3 = (re.m_ECurrentUnit > des.m_ECurrentUnit + 1);

		return condition1 || condition2 || condition3;
	}

	public static bool operator <(CoinBundle re, CoinBundle des)
	{
		bool condition1 = (re.m_ECurrentUnit == des.m_ECurrentUnit) && (re.m_FCoinProduce < des.m_FCoinProduce);

		bool condition2 = (re.m_ECurrentUnit - 1 == des.m_ECurrentUnit) && (re.m_FCoinProduce * Unit.UnitBase.s_UnitPower < des.m_FCoinProduce);

		bool condition3 = (re.m_ECurrentUnit + 1 < des.m_ECurrentUnit);

		return condition1 || condition2 || condition3;
	}

	public static bool operator >=(CoinBundle re, CoinBundle des)
	{
		bool condition1 = (re.m_ECurrentUnit == des.m_ECurrentUnit) && (re.m_FCoinProduce >= des.m_FCoinProduce);

		bool condition2 = (re.m_ECurrentUnit == des.m_ECurrentUnit + 1) && (re.m_FCoinProduce * Unit.UnitBase.s_UnitPower >= des.m_FCoinProduce);

		bool condition3 = (re.m_ECurrentUnit > des.m_ECurrentUnit + 1);

		return condition1 || condition2 || condition3;
	}

	public static bool operator <=(CoinBundle re, CoinBundle des)
	{
		bool condition1 = (re.m_ECurrentUnit == des.m_ECurrentUnit) && (re.m_FCoinProduce <= des.m_FCoinProduce);

		bool condition2 = (re.m_ECurrentUnit - 1 == des.m_ECurrentUnit) && (re.m_FCoinProduce * Unit.UnitBase.s_UnitPower <= des.m_FCoinProduce);

		bool condition3 = (re.m_ECurrentUnit + 1 < des.m_ECurrentUnit);

		return condition1 || condition2 || condition3;
	}
}
		
	public enum UnitLevel
	{
		LEVEL_ZERO,
		LEVEL_FIRST,
		LEVEL_SECOND,
		LEVEL_TOP
	}

	public class UnitBase : MonoBehaviour, UnitBehaviour
	{
		public static UnitBase CreateUnit(UnitType type, ref Transform t)
		{
			GameObject buildPre = Instantiate(UnitManager.s_UnitMgr_BuildPrefab) as GameObject;

			UnitBase bases = null;

			switch (type)
			{
			case UnitType.BUILDING_BASE:
				{
					buildPre.AddComponent<BuildingBase> ();
					break;
				}
			case UnitType.BUILDING_C2:
				{
					buildPre.AddComponent<BuildingC2> ();
					break;
				}
			case UnitType.BUILDING_D2:
				{
					buildPre.AddComponent<BuildingD2> ();
					break;
				}
			case UnitType.BUILDING_TOWN_HALL:
				{
					buildPre.AddComponent<BuildingTownHall> ();
					break;
				}
			case UnitType.CAR:
				{
					buildPre.AddComponent<Building> ();
					break;
				}
			case UnitType.DEFAULT:
				{
					buildPre.AddComponent<Building> ();
					break;
				}
			}

			bases = buildPre.GetComponent<UnitBase> ();
			bases.m_SUnitType = type;
			bases.LoadingResource (ref t);

			//bases.StartCoroutine (bases.m_CAnimatorController.InitAnimator());
			return bases;
		}

		public static int[] s_UnitBase_Coin_level_Factor = new int[4] {0,1,2,3};

		public static float frameRate = 1f/60f;

		//coin produce config
		public static CoinBundle[] s_ProduceRate = new CoinBundle[15]{
			new CoinBundle(10 * frameRate, CoinUnit.NULL, 0, 10),
			new CoinBundle(100 * frameRate, CoinUnit.NULL, 1, 100),
			new CoinBundle(1.3f * frameRate, CoinUnit.A, 2, 1300),
			new CoinBundle(26 * frameRate, CoinUnit.A,3,26),
			new CoinBundle(860 * frameRate, CoinUnit.A,4 ,860),
			new CoinBundle(51 * frameRate, CoinUnit.B,5, 51),
			new CoinBundle(1.4f * frameRate, CoinUnit.C,6, 1.4f),
			new CoinBundle(142f * frameRate, CoinUnit.C,7 ,142),
			new CoinBundle(6.9f * frameRate, CoinUnit.D,8, 6.9f),
			new CoinBundle(630 * frameRate, CoinUnit.D, 9, 630),
			new CoinBundle(57 * frameRate, CoinUnit.E, 10, 57),
			new CoinBundle(4.9f * frameRate, CoinUnit.F,11 ,4.9f),
			new CoinBundle(412 * frameRate, CoinUnit.F,12, 412),
			new CoinBundle(69 * frameRate, CoinUnit.G,13, 69),
			new CoinBundle(0 * frameRate, CoinUnit.NULL, 0, 0),
		};

		public UnitBase ()
		{
			m_SUnitName = "";
			m_SUnitType = UnitType.DEFAULT;
			m_CBoxCollider = null;
			m_UICoinProduceRate = 0;
			m_ECoinProduceType = CoinProduceType.AUTO;
			m_CProducedCoin = new CoinBundle (0, CoinUnit.NULL);
			m_CProduceRate = s_ProduceRate[14];
			m_CAnimatorController = null;
		}

		//multiplying power for coin unit
		public static int s_UnitPower = 1000;
		//total coin
		public static CoinBundle s_TotalCoin = new CoinBundle(0, CoinUnit.NULL);

		//produce rate
		protected float m_UICoinProduceRate { get;set;}
		//produce type
		protected CoinProduceType m_ECoinProduceType { get;set;}
		protected CoinBundle m_CProducedCoin;
		protected CoinBundle m_CProduceRate;

		//unit name
		protected string m_SUnitName { get;set;}
		//unit type
		protected UnitType m_SUnitType { get;set;}

		protected GameObject m_CModel;

		protected BoxCollider m_CBoxCollider;

		public BuildAnimator m_CAnimatorController { get;set;}

		public UnitLevel m_ECurrentLevel { get; set;}

		//make a 3d box collider for Unit,generally get from model if we have, or make a empty one
		virtual protected void generateBoxCollider(){
			m_CBoxCollider = new BoxCollider ();
		}

		public virtual void LoadingResource (ref Transform t) {}

		public virtual BoxCollider getBoxCollider()
		{
			return m_CBoxCollider;
		}

		virtual public GameObject getModel()
		{
			return m_CModel;
		}

		public virtual void onTouch(ref TapGesture e) {}

		virtual public void produceCoinAuto(){
			if (m_CProduceRate.m_ECurrentUnit == CoinUnit.NULL && m_CProduceRate.m_FCoinProduce == 0) {

			} else {
				if (m_ECoinProduceType == CoinProduceType.AUTO) {
					m_CProducedCoin = m_CProducedCoin + m_CProduceRate * s_UnitBase_Coin_level_Factor[(int)m_ECurrentLevel];
					s_TotalCoin = s_TotalCoin + m_CProduceRate;
				}
			}
		}

		virtual public void produceCoinTouch(){
			if (m_CProduceRate.m_ECurrentUnit == CoinUnit.NULL && m_CProduceRate.m_FCoinProduce == 0) {

			} else {
				if (m_ECoinProduceType == CoinProduceType.TOUCH) {
					m_CProducedCoin = m_CProduceRate * s_UnitBase_Coin_level_Factor[(int)m_ECurrentLevel];
					s_TotalCoin = s_TotalCoin + m_CProduceRate;
				}
			}
		}

		// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
		virtual public void OnStateEnter(ref Animator animator, ref AnimatorStateInfo stateInfo, ref int layerIndex) {
			
		}

		// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
		//	public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//
		//	}

		//OnStateExit is called when a transition ends and the state machine finishes evaluating this state
		virtual public void OnStateExit(ref Animator animator, ref AnimatorStateInfo stateInfo, ref int layerIndex) {
			
		}

	}
}

