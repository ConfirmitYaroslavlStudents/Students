using System;
using System.Collections.Generic;
using Mp3Handler;

namespace FolderLib
{
    public class LateWriteFileHandler:IFileHandler
    {
        public List<RenameAction> ToRename { get; private set; }
        public List<RetagAction> ToRetag { get; private set; }
        public List<String> NameCollisions { get; private set; } 

        public string FilePath { get { return _fileHandler.FilePath; } set { _fileHandler.FilePath = value; } }
        public string FileName { get { return _fileHandler.FileName; } }

        public LateWriteFileHandler(IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;
            _actions = new Queue<LateAction>();
            ToRename = new List<RenameAction>();
            ToRetag = new List<RetagAction>();
            _namesList = new List<string>();
            NameCollisions = new List<string>();
        }

        public Dictionary<FrameType, string> GetTags(GetTagsOption option = GetTagsOption.RemoveEmptyTags)
        {
            if(!_namesList.Contains(_fileHandler.FilePath))
                _namesList.Add(_fileHandler.FilePath);
            return _fileHandler.GetTags(option);
        }

        public void SetTags(Dictionary<FrameType, string> tags)
        {
                var act = new RetagAction(_fileHandler.SetTags, tags, _fileHandler.FilePath);
                ToRetag.Add(act);
                _actions.Enqueue(act);
        }

        public void Rename(string newName)
        {
            if (!NameCollisions.Contains(newName))
            {
                _namesList.Add(newName);
                _namesList.Remove(_fileHandler.FilePath);
                var act = new RenameAction(_fileHandler.Rename, newName, _fileHandler.FilePath);
                ToRename.Add(act);
                _actions.Enqueue(act);
            }
            else
            {
                //NameCollisions.Add();
            }
        }

        public void Dispose()
        {
            _fileHandler.Dispose();
        }

        private IFileHandler _fileHandler;

        public void WriteFiles(int[] notToWrite)
        {
            while (_actions.Count!=0)
            {
                var action = _actions.Dequeue();
                _fileHandler.FilePath = action.FilePath;
                action.Invoke();
            }
        }

        private List<string> _namesList;
        private Queue<LateAction> _actions;
    }

    abstract public class LateAction
    {
        public virtual string FilePath { get; set; }
        public virtual ActionType Action { get; protected set; }
        public abstract void Invoke();
    }

    public enum ActionType
    {
        Rename,
        Retag
    }

    public class RenameAction:LateAction
    {
        public string NewName { get; set; }
        public override string FilePath { get; set; }
        public override ActionType Action { get; protected set; }

        public RenameAction(Action<string> action,string newName,string filePath)
        {
            _action = action;
            NewName = newName;
            FilePath = filePath;
            Action = ActionType.Rename;
        }

        public override void Invoke()
        {
            _action.Invoke(NewName);
        }

        private Action<string> _action;
    }

    public class RetagAction:LateAction
    {
        public Dictionary<FrameType, string> NewTags { get; set; }
        public override string FilePath { get; set; }
        public override ActionType Action { get; protected set; }

        public RetagAction(Action<Dictionary<FrameType,string>> action,Dictionary<FrameType,string> newTags,string filePath)
        {
            _action = action;
            NewTags = newTags;
            FilePath = filePath;
            Action = ActionType.Retag;
        }

        public override void Invoke()
        {
            _action.Invoke(NewTags);
        }

        private Action<Dictionary<FrameType, string>> _action;
    }
}
