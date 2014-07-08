using System;

namespace TimeLocker
{
	public abstract class Locker
	{
		public abstract event LockStatusChangedHandler LockStatusChanged;
		public abstract event EventHandler SystemShutdown;

		public abstract void LockSystem();
	}
}
