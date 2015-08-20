using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace mp3lib.Rollback
{
	public class RollbackManager : IDisposable
	{
		private readonly ISaver _communicator;

		private Stack<RollbackInfo> _data;

		public RollbackManager(ISaver saveTo)
		{
			_communicator = saveTo;
			_data = new Stack<RollbackInfo>();

			Load();
		}

		public void AddAction(RollbackInfo info)
		{
			_data.Push(info);
		}

		public RollbackStatus Rollback()
		{
			if(_data.Count <= 0) return new RollbackStatus(RollbackState.Success);

			var info = _data.Pop();

			switch (info.Action)
			{
				case ProgramAction.Mp3Edit:
					new Mp3TagChanger(new Mp3File(info.FilesChanged[0]), info.Mask, _communicator).Rollback(info);
					return new RollbackStatus(RollbackState.Success, ProgramAction.Mp3Edit);
				case ProgramAction.FileRename:
					new Mp3FileNameChanger(new Mp3File(info.FilesChanged[0]), info.Mask).Rollback(info);
					return new RollbackStatus(RollbackState.Success, ProgramAction.FileRename);
				case ProgramAction.Sync:
					new Mp3Syncing(info.FilesChanged.Select(file => new Mp3File(file)), info.Mask, _communicator).Rollback(info);
					break;
			}

			return new RollbackStatus(RollbackState.Fail, null, $"For action {info.Action} it's not implemented");
		}

		private void Load()
		{
			try
			{
				var data = Encoding.UTF8.GetBytes(_communicator.GetResponse());
				using (var fStream = new MemoryStream(data))
				{
					_data = (new XmlSerializer(typeof(List<RollbackInfo>), new[] { typeof(RollbackInfo), typeof(Stack<RollbackInfo>) })).Deserialize(fStream) as Stack<RollbackInfo>;
				}
			}
			catch
			{
				// ignored
			}
		}

		private void Save()
		{
			using (var fStream = new MemoryStream())
			{
				(new XmlSerializer(typeof(List<RollbackInfo>), new[] { typeof(RollbackInfo), typeof(Stack<RollbackInfo>) })).Serialize(fStream, _data);
				var data = Encoding.UTF8.GetString(fStream.ToArray());
				_communicator.SendMessage(data);
			}
		}


		~RollbackManager()
		{
			Dispose();
		}

		public void Dispose()
		{
			Save();
		}

	}
}