using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace TimeLocker
{
	public sealed class WindowsLocker : Locker
	{
		[DllImport("user32.dll", EntryPoint = "LockWorkStation")]
		static extern bool LockWorkStation();

		public override event LockStatusChangedHandler LockStatusChanged;
		public override event EventHandler SystemShutdown;

		public WindowsLocker()
		{
			SystemEvents.SessionSwitch += SessionSwitch;
			SystemEvents.SessionEnding += SessionEnding;
		}

		public override void LockSystem()
		{
			LockWorkStation();
		}

		void SessionSwitch(object o, SessionSwitchEventArgs e)
		{
			if (e.Reason == SessionSwitchReason.SessionLock)
				LockStatusChanged(this, new LockStatusChangedEventArgs(LockStatusChangedReason.Lock));
			else if (e.Reason == SessionSwitchReason.SessionUnlock)
				LockStatusChanged(this, new LockStatusChangedEventArgs(LockStatusChangedReason.Unlock));
		}

		void SessionEnding(object o, SessionEndingEventArgs e)
		{
			SystemShutdown(this, EventArgs.Empty);
		}
	}
}
