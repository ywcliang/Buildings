using System;
using Unit;
using UnityEngine;

namespace Building
{
	public class PreBuild : UnitState
	{
		override public void onPreChangeState()
		{

		}

		override public void onStateChanged()
		{

		}

		override public void update()
		{

		}
	}

	public class constructing : UnitState
	{
		override public void onPreChangeState()
		{

		}

		override public void onStateChanged()
		{

		}

		override public void update()
		{

		}
	}
		
	public class Growing : UnitState
	{
		override public void onPreChangeState()
		{

		}

		override public void onStateChanged()
		{

		}

		override public void update()
		{

		}
	}

	public class Destory : UnitState
	{
		override public void onPreChangeState()
		{

		}

		override public void onStateChanged()
		{

		}

		override public void update()
		{

		}
	}
	
	public class BuildingState
	{
		public static int statePrebuild = 0;
		public static int constructing = 1;
		public static int stateGrowing = 2;
		public static int stateDestory = 3;
		public static int stateCount = 4;

		public delegate void BuildDelegate();

		private Building m_CBuildInstance;

		public void setBuildInstance(Building b)
		{
			m_CBuildInstance = b;
		}

		public Building getBuildInstance()
		{
			return m_CBuildInstance;
		}

//		public event BuildDelegate loadEvent;
//		public event BuildDelegate destoryEvent;
//		public event BuildDelegate runningEvent;

		UnitState m_CCurrentState;

		UnitState[] m_States;

		public BuildingState ()
		{
			m_States = new UnitState[stateCount];
			m_States[statePrebuild] = new PreBuild ();
			m_States[constructing] = new constructing ();
			m_States[stateGrowing] = new Growing ();
			m_States[stateDestory] = new Destory ();

			m_CCurrentState = null;
		}

		public void update()
		{
			if (m_CCurrentState != null)
				m_CCurrentState.update ();
		}

		//change state
		public void changeState(int state)
		{
			if (m_CCurrentState == m_States[state])
				return;


			if (m_CCurrentState != null)
				m_CCurrentState.onStateChanged ();
			m_CCurrentState = m_States[state];
			m_CCurrentState.onChangeState ();
		}


	}
}

