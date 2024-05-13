using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurstTrie.ListBurstTrie.CutStrings
{
    public class BurstTrie : ITrie
    {
        /// <summary>
        /// Limit at which a container will burst past        
        /// </summary>
        public readonly int ContainerLimit;
        /// <summary>
        /// Minimum character accepted as part of an input string
        /// </summary>
        public readonly char AlphabetStart;
        /// <summary>
        /// Maximum character accepted as part of an input string
        /// </summary>
        public readonly char AlphabetEnd;

        /// <summary>
        /// Root of the tree, can be either a container, or an internal node
        /// </summary>
        BurstNode root;

        /// <summary>
        /// You know what this is...
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Starts with an empty container node
        /// </summary>
        public BurstTrie(int containerLimit = 125, char alphabetStart = 'a', char alphabetEnd = 'z')
        {
            ContainerLimit = containerLimit;
            AlphabetStart = alphabetStart;
            AlphabetEnd = alphabetEnd;

            root = new BurstContainer(this);
        }

        /// <summary>
        /// Begins recursive insert
        /// </summary>
        /// <param name="value">value to insert</param>
        public void Insert(string value)
        {
            root = root.Insert(value ?? throw new ArgumentNullException("Inputted string was null"), 0);
        }
        /// <summary>
        /// Begins recursive remove
        /// </summary>
        /// <param name="value">value to remove</param>
        /// <returns>if anything was found & removed</returns>
        public bool Remove(string value)
        {
            root.Remove(value, 0, out var removed);
            return removed;
        }
        /// <summary>
        /// Checks if a node is contained
        /// </summary>
        /// <param name="node">the node to check</param>
        /// <returns>if the node points to this tree</returns>       
        public bool Contains(BurstNode node) => node.ParentTrie == this;
        public List<string> GetAll() => root.GetAll();
        public static List<string> BurstSort(IList<string> listToSort, char alphabetStart = 'a', char alphabetEnd = 'z', int bucketSize = 125)
        {
            var trie = new BurstTrie(bucketSize, alphabetStart, alphabetEnd);
            for (int i = 0; i < listToSort.Count; i++)
            {
                trie.Insert(listToSort[i]);
            }

            List<string> output = new(listToSort.Count);
            trie.root.GetAll("", output);
            return output;
        }
    }
}
