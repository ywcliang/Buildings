using System;

namespace Unit
{
	public class UnitState
	{
		public delegate void StateCallback();

		public event StateCallback onPreStateCall;
		public event StateCallback ChangeingStateCall;
		public event StateCallback BeenChangedStateCall;

		public UnitState ()
		{
			
		}

		~UnitState()
		{
			onPreStateCall = null;
			ChangeingStateCall = null;
			BeenChangedStateCall = null;
		}

		virtual public void onPreChangeState()
		{
			if (onPreStateCall != null)
				onPreStateCall.Invoke ();
		}

		virtual public void onStateChanging()
		{
			if (ChangeingStateCall != null)
				ChangeingStateCall.Invoke ();
		}

		virtual public void onStateBeenChanged()
		{
			if (BeenChangedStateCall != null)
				BeenChangedStateCall.Invoke ();
		}

		virtual public void update()
		{
			
		}
	}
}

