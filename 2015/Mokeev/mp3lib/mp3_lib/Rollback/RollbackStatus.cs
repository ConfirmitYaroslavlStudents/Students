using System;

namespace mp3lib.Rollback
{
	public class RollbackStatus
	{

		public RollbackState State { get; private set; }
		public ProgramAction RollbackedAction { get; private set; }
		public string FailReason { get; private set; }

		public RollbackStatus(RollbackState state, ProgramAction? action = null, string failReason = "")
		{
			State = state;
			if(action != null) RollbackedAction = action.Value;
			FailReason = failReason;
		}
	}
}