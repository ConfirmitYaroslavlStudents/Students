using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace mp3lib.Rollback
{
	public class RollbackManager : IDisposable
	{
		private readonly ISaver _saver;

		private Stack<RollbackInfo> _data;

		public RollbackManager(ISaver saveTo)
		{
			_saver = saveTo;
			_data = new Stack<RollbackInfo>();

			Load();
		}

		public IDisposable AddAction(RollbackInfo info)
		{
			_data.Push(info);
			return this;
		}

		public RollbackStatus Rollback()
		{
			if(_data.Count <= 0) return new RollbackStatus(RollbackState.Success);

			var info = _data.Pop();

			switch (info.Action)
			{
				case ProgramAction.Mp3Edit:
					new Mp3TagChanger(new Mp3File(info.FilesChanged[0]), info.Mask, _saver).Rollback(info);
					return new RollbackStatus(RollbackState.Success, ProgramAction.Mp3Edit);
				case ProgramAction.FileRename:
					new Mp3FileNameChanger(new Mp3File(info.FilesChanged[0]), info.Mask, _saver).Rollback(info);
					return new RollbackStatus(RollbackState.Success, ProgramAction.FileRename);
				case ProgramAction.Sync:
					new Mp3Syncing(info.FilesChanged.Select(file => new Mp3File(file)), info.Mask, null, _saver).Rollback(info);
					return new RollbackStatus(RollbackState.Success, ProgramAction.Sync);
			}

			return new RollbackStatus(RollbackState.Fail, null, $"For action {info.Action} it's not implemented");
		}

		private void Load()
		{
			try
			{
				_data = JsonConvert.DeserializeObject<Stack<RollbackInfo>>(_saver.GetResponse()) ?? new Stack<RollbackInfo>();
			}
			catch
			{
				// ignored
			}
		}

		private void Save()
		{
			_saver.SendMessage(JsonConvert.SerializeObject(_data, Formatting.Indented));
		}

		public void Dispose()
		{
			Save();
		}

	}
}