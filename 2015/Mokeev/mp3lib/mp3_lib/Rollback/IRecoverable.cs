using System;

namespace mp3lib.Rollback
{
	public interface IRecoverable : IDisposable
	{
		void Rollback(RollbackInfo info);
	}
}