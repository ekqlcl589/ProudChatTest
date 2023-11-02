using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProudChat
{
    internal class Trie
    {
        public Trie() { }

        public void InsertWord(System.String word)
        {
            TrieNode node = root;
            foreach(var item in word) {
                if(node is null) return;

                if(node.isChildren(item) is false)
                    node.AddChildren(item);

                node = node.GetChildren(item);
            }

            if (node != null && node != root)
                node.IsEndOfWord = true;
        }

        public bool searchWord(System.String word)
        {
            TrieNode node = root;
            foreach(var item in word)
            {
                if(node is null) return false;
                if (node.isChildren(item) is false) return false;
                if (node.IsEndOfWord is true) return true;
                node = node.GetChildren(item);
            }
            return node.IsEndOfWord;
        }


        private TrieNode root = new TrieNode();
    }
}
