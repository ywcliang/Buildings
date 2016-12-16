using System;

namespace Unit
{
	public class UnitState
	{
		public delegate void StatePreCallback();
		public delegate void StateChangingCallback();
		public delegate void StateBeenChangedCallback();
		public delegate void StateUpdateCallback();

		public event StatePreCallback OnPreStateCall;
		public event StateChangingCallback ChangingStateCall;
		public event StateBeenChangedCallback BeenChangedStateCall;
		public event StateUpdateCallback UpdateCall;

		public UnitState ()
		{
			
		}

		~UnitState()
		{
			OnPreStateCall = null;
			ChangingStateCall = null;
			BeenChangedStateCall = null;
			UpdateCall = null;
		}

		virtual public void onPreChangeState()
		{
			if (OnPreStateCall != null)
				OnPreStateCall.Invoke ();
		}

		virtual public void onStateChanging()
		{
			if (ChangingStateCall != null)
				ChangingStateCall.Invoke ();
		}

		virtual public void onStateBeenChanged()
		{
			if (BeenChangedStateCall != null)
				BeenChangedStateCall.Invoke ();
		}

		virtual public void update()
		{
			if (UpdateCall != null)
				UpdateCall.Invoke ();
		}
	}
}

