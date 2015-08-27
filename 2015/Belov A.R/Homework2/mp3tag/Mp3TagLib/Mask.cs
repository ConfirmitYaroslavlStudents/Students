using System;
using System.Collections.Generic;
using System.Linq;


namespace Mp3TagLib
{
     [Serializable]
    public class Mask:IEnumerable<string>
    {
        private LinkedList<MaskItem> _body;
        private string _stringBody;
        private List<Dictionary<string, string>> _posibleTagValues; 

        public Mask(string mask)
        {
            if (string.IsNullOrEmpty(mask))
            {
                throw new ArgumentException("Incorrect mask");
            }
            Init(mask);
        }

        void Init(string mask)
        {
            _stringBody = mask;
            _body=new LinkedList<MaskItem>();
            _posibleTagValues = new List<Dictionary<string, string>>();
            var stack=new Stack<char>();
            
            for (var i = 0; i < mask.Length; i++)
            {
                switch (mask[i])
                {
                    case '{':
                        if(i==mask.Length-1)
                            throw new ArgumentException("Incorrect mask");
                        AddTagToBody(mask.Substring(i+1,mask.Length-i-1));
                        stack.Push(mask[i]);
                        break;
                    case '}':
                        if (i != mask.Length - 1)
                        {
                            AddDelimiterToBody(mask.Substring(i + 1, mask.Length - i - 1));
                        }
                        stack.Push(mask[i]);
                        break;
                    default:
                        if(i==0)
                            AddDelimiterToBody(mask);
                        break;
                }
            }
            if(!ValidateMask(stack))
            { throw new ArgumentException("Incorrect mask"); }
            
        }

        bool ValidateMask(Stack<char> stack)
        {
            if (stack.Count == 0)
                return false;
            var flag = true;
            while (stack.Count!=0)
            {
                if (stack.Pop() == '}')
                {
                    if (!flag)
                    {
                        return false;
                    }
                    flag = false;
                }
                else
                {
                    if (flag)
                    {
                        return false;
                    }
                    flag = true;
                }
            }
            return flag;
        }

        bool ValidateString(string str, LinkedListNode<MaskItem> currentDelimiter)
        {
            for(var node=currentDelimiter;node!=null;node=node.Next)
            {
                if (node.Value.Type == MaskItemType.TagName)
                {
                    continue;
                }
                if (!str.Contains(node.Value.Value))
                {
                    return false;
                }
                str = str.Substring(str.IndexOf(node.Value.Value)+1, str.Length - str.IndexOf(node.Value.Value)-1);
                
            }
            return true;
        }

        void AddTagToBody(string str)
        {
            var indexOfNextCloseParenthesis = str.IndexOf("}");
            if(indexOfNextCloseParenthesis==-1)
                throw new ArgumentException("Incorrect mask");
            var item=new MaskItem() {Type =MaskItemType.TagName,Value = str.Substring(0,indexOfNextCloseParenthesis)};
            if(_body.Contains(item))
                throw new ArgumentException("Incorrect mask");
            _body.AddLast(item);
        }

        void AddDelimiterToBody(string str)
        {
            var indexOfNextOpenParenthesis = str.IndexOf("{");
            if (indexOfNextOpenParenthesis == -1)
                _body.AddLast(new MaskItem() { Type = MaskItemType.Delimiter, Value = str });
            else
            {
                _body.AddLast(new MaskItem() { Type = MaskItemType.Delimiter, Value = str.Substring(0,indexOfNextOpenParenthesis) });
            }
        }

        TagTreeNode GetTagTree(string name)
        {
            var root = new TagTreeNode() { TagValue = "Root", TagName = "Root" };
            name=name.Replace('}', ']');
            name=name.Replace('{', '[');
            BuildTree(name,_body.First,root);
            return root;
        }

        void BuildTree(string str, LinkedListNode<MaskItem> currentMaskItem, TagTreeNode currentTagTreeNode)
        {
            if (currentMaskItem == null)
            {
                return;
            }
            switch (currentMaskItem.Value.Type)
            {
                case MaskItemType.Delimiter:
                    SkipDelimiter(str, currentMaskItem, currentTagTreeNode);
                    break;
                case MaskItemType.TagName:
                    AddNewNode(str, currentMaskItem, currentTagTreeNode);
                    break;
            } 
        }

        void SkipDelimiter(string str, LinkedListNode<MaskItem> currentMaskItem, TagTreeNode currentTagTreeNode)
        {
            BuildTree(str.Substring(currentMaskItem.Value.Value.Length), currentMaskItem.Next,currentTagTreeNode);
        }

        void AddNewNode(string str, LinkedListNode<MaskItem> currentMaskItem, TagTreeNode currentTagTreeNode)
        {
            if (currentMaskItem.Next == null)
            {
                var newNode = new TagTreeNode() { TagName = currentMaskItem.Value.Value, TagValue = str };
                currentTagTreeNode.AddChild(newNode);
                return;
            }
           
            var nextDelimiter = currentMaskItem.Next.Value.Value;
            var currentTagName = currentMaskItem.Value.Value;
            var nextDelimiterIndex = str.IndexOf(nextDelimiter);
           
            if(nextDelimiterIndex==-1)
                return;
            while (true)
            {
                var newNode = new TagTreeNode() { TagName = currentTagName, TagValue = str.Substring(0, nextDelimiterIndex) };
               
                currentTagTreeNode.AddChild(newNode);
                BuildTree(str.Substring(nextDelimiterIndex, str.Length - newNode.TagValue.Length), currentMaskItem.Next, newNode);
               
                var strAfterNextDelimiter = str.Substring(nextDelimiterIndex + nextDelimiter.Length,
                    str.Length - nextDelimiter.Length-nextDelimiterIndex);
                if (nextDelimiter == "")
                {
                    nextDelimiterIndex++;
                    if (nextDelimiterIndex > str.Length)
                        return;
                }
                else if (ValidateString(strAfterNextDelimiter,currentMaskItem))
                {
                    nextDelimiterIndex = newNode.TagValue.Length + nextDelimiter.Length + strAfterNextDelimiter.IndexOf(nextDelimiter);
                }
                else
                {
                    return;
                }
            }
        }

        public List<Dictionary<string, string>> GetTagValuesFromString(string str)
        {
            if (!ValidateString(str,_body.First))
            {
                throw new ArgumentException("Can't apply mask");   
            }
          
            var root=GetTagTree(str);
            _posibleTagValues.Clear();
           
            foreach (var child in root.Childs)
            {
                BuildResults(child, new Dictionary<string, string>());
            }
            return _posibleTagValues;
        }

        void BuildResults(TagTreeNode node,Dictionary<string,string> tagValues)
        {
            tagValues.Add(node.TagName, node.TagValue);
            
            foreach (var child in node.Childs)
            {
                BuildResults(child,new Dictionary<string, string>(tagValues));
            }
           
            if (node.Childs.Count == 0)
            {
                _posibleTagValues.Add(tagValues);
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return (from maskItem in _body where maskItem.Type == MaskItemType.TagName select maskItem.Value).GetEnumerator();

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return _stringBody;
        }
    }
}
