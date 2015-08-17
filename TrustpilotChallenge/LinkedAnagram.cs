using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrustpilotChallenge
{
    public class LinkedAnagram : LinkedList<String>
    {
        /**
         * Overrides tostring.
         * Returns a string containing the chars in the current tree.
         */
        public override string ToString()
        {
            var ret = new StringBuilder();
            foreach(var nd in this)
                ret.Append(nd);
            return String.Concat(ret.ToString().Replace(" ", String.Empty));
        }

        /**
         * Renegrates all possible anagrams.
         */
        public List<String> AnagramsFromTree(Dictionary<String, List<String>> wordTable)
        {
            // Loads the word groups from the dictionary
            var wordGroups = new List<List<String>>();
            List<String> group;
            foreach (var llo in this)
            {
                wordTable.TryGetValue(llo, out group);
                wordGroups.Add(group);
            }

            // Creates the permutations of the groups
            var groupsArray = wordGroups.ToArray();
            var permutRes = Permut<List<String>>(groupsArray);

            // Generates all possible anagrams based on the current tree
            var ret = new List<String>();
            foreach (var permutt in permutRes)
                ret.AddRange(anagramsFromTree(permutt, 0));
            return ret;
        }

        /**
         * Recursive function for generating the anagrams.
         */
        private List<String> anagramsFromTree(List<String>[] node, int iv)
        {
            // Last node. just return what we got.
            if (iv == node.Length-1) return node[iv];

            var ret = new List<String>();
            var childs = anagramsFromTree(node, iv+1);
            foreach (var word in node[iv])
            {
                foreach (var after in childs)
                {
                    var sb = new StringBuilder();
                    sb.Append(word);
                    sb.Append(" ");
                    sb.Append(after);
                    ret.Add(sb.ToString());
                }
            }
            return ret;
        }

        /**
         * Generates all permitations of the given array, and returns a list with the generated arrays.
         */
        public static T[][] Permut<T>(T[] sp) {
            if (sp.Length == 1) return new T[][] { sp };
            if(sp.Length == 2) return new T[][] { sp, new T[]{sp[1], sp[0]}};

            var ret = new List<T[]>();
            for (var i = 0; i < sp.Length; i++){
                // removes the one locked for the first position.
                var next = new T[sp.Length-1];
                Array.Copy(sp, next, i);
                Array.Copy(sp, i + 1, next, i, next.Length - i);

                foreach (var result in Permut<T>(next))
                    ret.Add((new T[] { sp[i] }).Concat(result).ToArray());
            }
            return ret.ToArray();
        }
    }
}
