using System;
using System.Collections.Generic;
using System.Linq;
using mp3lib.Core;
using Newtonsoft.Json;

namespace mp3lib.Rollback
{
	public class RollbackManager : IDisposable
	{
		private readonly ISaver _saver;

		private Stack<Info> _data;

		public RollbackManager(ISaver saveTo)
		{
			_saver = saveTo;
			_data = new Stack<Info>();

			Load();
		}

		public IDisposable AddAction(Info info)
		{
			_data.Push(info);
			return this;
		}

		public RollbackStatus Rollback()
		{
			if(_data.Count <= 0) return new RollbackStatus(RollbackState.Success);

			var info = _data.Pop();

			//TODO: ROLLBACK
			try
			{
				var mp3 = new Mp3File(info.NewFileName);
				mp3.ChangeFileName(info.OldFileName);
				foreach (var tag in info.Tags)
				{
					mp3[tag.Key] = tag.Value;
				}
				return new RollbackStatus(RollbackState.Success);
			}
			catch (Exception e)
			{
				return new RollbackStatus(RollbackState.Fail, e.Message);
			}


		}

		private void Load()
		{
			try
			{
				_data = JsonConvert.DeserializeObject<Stack<Info>>(_saver.GetResponse()) ?? new Stack<Info>();
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