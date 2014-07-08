using System;

namespace TimeLocker
{
	public sealed class LockStatusChangedEventArgs
	{
		public LockStatusChangedReason Reason { get; private set; }

		public LockStatusChangedEventArgs(LockStatusChangedReason reason)
		{
			Reason = reason;
		}
	}
}
