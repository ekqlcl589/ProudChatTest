using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProudChat
{
    internal class Filtering
    {
        private Trie m_Trie = null;

        public void FilteringText(ref System.String text)
        {
            if (m_Trie == null) return;

            for(int i = 0; i < text.Length; ++i)
            {
                System.String word = "";
                for(int j = i; j < text.Length; ++j)
                {
                    word += text[j];
                    if(m_Trie.searchWord(word) is true)
                    {
                        System.String replacement = new System.String('*' , j-i+1);
                        text = text.Replace(word, replacement);
                        i = j;
                        break;
                    }
                }
            }
        }

        public void AddFiltering(System.String filter)
        {
            if (m_Trie is null)
                m_Trie = new Trie();

            string[] records = filter.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string record in records)
            {
                string[] fields = record.Split(',');

                foreach (string field in fields)
                {
                    m_Trie.InsertWord(field);
                }
            }
        }

        public void RemoveFiltering()
        {
            m_Trie = null;
        }
    }
}
