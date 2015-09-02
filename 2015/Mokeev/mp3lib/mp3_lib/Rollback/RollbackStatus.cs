namespace mp3lib.Rollback
{
	public class RollbackStatus
	{

		public RollbackState State { get; private set; }
		public string FailReason { get; private set; }

		public RollbackStatus(RollbackState state, string failReason = "")
		{
			State = state;
			FailReason = failReason;
		}
	}
}