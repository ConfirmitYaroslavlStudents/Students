using System.Collections.Generic;
namespace Mp3TagLib
{
    class TagTreeNode
    {
        public TagTreeNode()
        {
            Childs=new List<TagTreeNode>();
        }
        public TagTreeNode Parent { get;private set; }
        public List<TagTreeNode> Childs { get;private set; }
        public string TagName { get; set; }
        public string TagValue { get; set; }

        public void AddChild(TagTreeNode node)
        {
            node.Parent = this;
            Childs.Add(node);
        }
    }
}
