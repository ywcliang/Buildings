using System;
using Unit;
using UnityEngine;

namespace Buildings
{
	public class PreBuild : UnitState
	{
		override public void onPreChangeState()
		{
			//set building levels
			//loading config files from save data,licke world config
			base.onPreChangeState();
		}

		override public void onStateBeenChanged()
		{
			//release animations current state using
			//stateCall.Invoke();
			base.onStateBeenChanged();
		}

		override public void onStateChanging()
		{
			//loading animations and playing audios
			base.onStateChanging();
		}

		override public void update()
		{
			base.update ();
		}
	}

	public class constructing : UnitState
	{
		override public void onPreChangeState()
		{
			//set building levels
			base.onPreChangeState();
		}

		override public void onStateBeenChanged()
		{
			//release animations ?
			base.onStateBeenChanged();
		}

		override public void onStateChanging()
		{
			//load next animations or change animation clips
			base.onStateChanging();
		}


		override public void update()
		{
			//if state work is done then do something
			base.update ();
		}
	}
		
	public class Producing : UnitState
	{
		override public void onPreChangeState()
		{
			base.onPreChangeState();
		}

		override public void onStateBeenChanged()
		{
			//invisible animation ?
			base.onStateBeenChanged();
		}

		override public void onStateChanging()
		{
			//load or change animation clip
			base.onStateChanging();
		}


		override public void update()
		{
			//produce coin or something
			base.update ();
		}
	}

	public class Destory : UnitState
	{
		override public void onPreChangeState()
		{
			base.onPreChangeState();
		}

		override public void onStateBeenChanged()
		{
			base.onStateBeenChanged();
		}

		override public void onStateChanging()
		{
			//release all things, anmations object, or gameobject from manager pool
			base.onStateChanging();
		}


		override public void update()
		{
			base.update ();
		}
	}
	
	public class BuildingState
	{
		//all states list
		public static int s_StatePrebuild = 0;
		public static int s_StateConstruct = 1;
		public static int s_StateProduce = 2;
		public static int s_StateDestory = 3;
		public static int s_StateCount = 4;

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
			m_States = new UnitState[s_StateCount];
			m_States[s_StatePrebuild] = new PreBuild ();
			m_States[s_StateConstruct] = new constructing ();
			m_States[s_StateProduce] = new Producing ();
			m_States[s_StateDestory] = new Destory ();

			m_CCurrentState = null;
		}

		public virtual void update()
		{
			if (m_CCurrentState != null)
				m_CCurrentState.update ();
		}

		//change state
		public virtual void changeState(int state)
		{
			if (m_CCurrentState == m_States[state])
				return;


			if (m_CCurrentState != null) {
				m_CCurrentState.onStateBeenChanged ();

			}
			m_CCurrentState = m_States[state];
			m_CCurrentState.onStateChanging ();
		}


		//push delegates
		public virtual void pushPreCallback(int stateIndex, UnitState.StatePreCallback call)
		{
			if (m_States [stateIndex] != null) {
				m_States [stateIndex].OnPreStateCall += call;
			}
		}

		public virtual void pushChangingCallback(int stateIndex, UnitState.StateChangingCallback call)
		{
			if (m_States [stateIndex] != null) {
				m_States [stateIndex].ChangingStateCall += call;
			}
		}

		public virtual void pushBeenChangedCallback(int stateIndex, UnitState.StateBeenChangedCallback call)
		{
			if (m_States [stateIndex] != null) {
				m_States [stateIndex].BeenChangedStateCall += call;
			}
		}

		public virtual void pushUpdateCallback(int stateIndex, UnitState.StateUpdateCallback call)
		{
			if (m_States [stateIndex] != null) {
				m_States [stateIndex].UpdateCall += call;
			}
		}

		//pop delegates
		public virtual void popPreCallback(int stateIndex, UnitState.StatePreCallback call)
		{
			if (m_States [stateIndex] != null) {
				m_States [stateIndex].OnPreStateCall -= call;
			}
		}

		public virtual void popChangingCallback(int stateIndex, UnitState.StateChangingCallback call)
		{
			if (m_States [stateIndex] != null) {
				m_States [stateIndex].ChangingStateCall -= call;
			}
		}

		public virtual void popBeenChangedCallback(int stateIndex, UnitState.StateBeenChangedCallback call)
		{
			if (m_States [stateIndex] != null) {
				m_States [stateIndex].BeenChangedStateCall -= call;
			}
		}

		public virtual void popUpdateCallback(int stateIndex, UnitState.StateUpdateCallback call)
		{
			if (m_States [stateIndex] != null) {
				m_States [stateIndex].UpdateCall -= call;
			}
		}
	}
}

