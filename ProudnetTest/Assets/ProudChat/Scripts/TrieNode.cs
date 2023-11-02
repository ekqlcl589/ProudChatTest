using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProudChat
{
    internal class TrieNode
    {
        private Dictionary<System.Char, TrieNode> children = new Dictionary<System.Char, TrieNode>();
        private System.Boolean isEndOfWord = false;
        public System.Boolean IsEndOfWord { get { return isEndOfWord; } set { isEndOfWord = value; } }

        public TrieNode() { }
        public TrieNode AddChildren(System.Char word)
        {
            if(false == children.ContainsKey(word))
                children.Add(word, new TrieNode());
            return children[word];
        }
        public TrieNode GetChildren(System.Char word)
        {
            if(true == children.ContainsKey(word))
                return children[word];
            return null;
        }
        public bool isChildren(System.Char word)
        {
            return children.ContainsKey(word);
        }
    }
}
