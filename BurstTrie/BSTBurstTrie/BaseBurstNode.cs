using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurstTrie.BSTBurstTrie
{
    /// <summary>
    /// Polymorphic base for the two types of nodes in the Trie
    /// </summary>
    public abstract class BurstNode
    {
        /// <summary>
        /// The Trie this node belongs to
        /// </summary>
        internal BurstTrie ParentTrie;
        /// <summary>
        /// The amount of values contained in this node
        /// </summary>
        public abstract int Count { get; }
        /// <summary>
        /// Creates the Node referencing its parent
        /// </summary>
        protected BurstNode(BurstTrie parent) => ParentTrie = parent;
        /// <summary>
        /// Abstract insertion function
        /// </summary>
        /// <param name="value">the string to insert</param>
        /// <param name="index">the current spot in the string</param>
        /// <returns>replacement node to backprop</returns>
        public abstract BurstNode Insert(string value, int index, int offset = 0);
        /// <summary>
        /// Abstract deletion function
        /// </summary>
        /// <param name="value">the value to remove</param>
        /// <param name="index">the current spot in the string</param>
        /// <param name="success">outputs if anything was deleted</param>
        /// <returns></returns>
        public abstract BurstNode? Remove(string value, int index, out bool success);
        /// <summary>
        /// Get a Node containing a definined prefix
        /// </summary>
        /// <param name="prefix">The prefix to search for</param>
        /// <param name="index">the current spot in the string</param>
        /// <returns></returns>
        public abstract BurstNode? Search(string prefix, int index);
        public List<string> GetAll()
        {
            List<string> starter = new();
            GetAll(starter);
            return starter;
        }
        /// <summary>
        /// Gets all items in order
        /// </summary>
        /// <param name="root"></param>
        /// <param name="starter"></param>
        internal abstract void GetAll(List<string> starter);
    }
}
