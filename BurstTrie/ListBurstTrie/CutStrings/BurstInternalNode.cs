using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BurstTrie.BSTBurstTrie;

namespace BurstTrie.ListBurstTrie.CutStrings
{
    public sealed class BurstInternalNode : BurstNode
    {
        /// <summary>
        /// Set of next nodes within range of Alphabet
        /// </summary>
        internal BurstNode?[] nexts;

        public char AlphabetStart => ParentTrie.AlphabetStart;
        public char AlphabetEnd => ParentTrie.AlphabetEnd;

        internal int count;
        public override int Count => count;

        /// <summary>
        /// Gets the index in nexts denoted by the character at a specified position in the input string
        /// </summary>
        /// <param name="value">the string</param>
        /// <param name="index">the position in value to check for</param>
        /// <returns>the index in nexts</returns>
        /// <exception cref="ArgumentException">throws if the character is outside the bounds of the alphabet</exception>
        internal int GetSlot(string value, int index)
        {
            if (index < value.Length)
            {
                var slot = value[index] - AlphabetStart;
                if (slot < 0) throw new ArgumentException("character outside the accepted range");
                return slot + 1;
            }

            return 0;
        }

        internal BurstInternalNode(BurstTrie parent)
            : base(parent)
        {
            nexts = new BurstNode[AlphabetEnd - AlphabetStart + 2];
        }

        /// <summary>
        /// Inserts a value into the trie, duplicates will be ignored
        /// </summary>
        /// <param name="value">value to add</param>
        /// <param name="index">current spot in the value</param>
        /// <returns>replacement for this current node</returns>
        public override BurstNode Insert(string value, int index)
        {
            //finds the slot to insert into
            var insertionIndex = GetSlot(value, index);
            //if the slot is empty, generates a new container node, and increments this node's count
              if (nexts[insertionIndex] == null)
            {
                count++;
                nexts[insertionIndex] = new BurstContainer(ParentTrie);
            }
            //Sets the chosen slot to the replacement value returned after the value is inserted into there
            nexts[insertionIndex] = nexts[insertionIndex]!.Insert(value, index + 1);
            //returning this, since nothing should replace an internal node during insertion
            return this;
        }
        /// <summary>
        /// Removes a value from the trie, outputs if it succeeds
        /// </summary>
        /// <param name="value">string to remove</param>
        /// <param name="index">position in value</param>
        /// <param name="success">whether the string was removed</param>
        /// <returns>replacement for this current node</returns>
        public override BurstNode? Remove(string value, int index, out bool success)
        {
            success = false;
            //finds the slot to delete from
            var deletionIndex = GetSlot(value, index);
            //if a value exists in that slot, try to delete in that slot
            if (nexts[deletionIndex] != null)
            {
                //Sets the chosen slot to the replacement value returned after the value is deleted from there
                nexts[deletionIndex] = nexts[deletionIndex]!.Remove(value, index + 1, out success);

                //if the slot we have removed from empties itself, we decrement our count
                //if our newly decremented count is 0, that means we don't have any occupied slots, so we will return a null to delete ourselves
                if (nexts[deletionIndex] == null && --count == 0) return null;
            }
            //otherwise, just return ourselves to avoid replacement
            return this;
        }

        /// <summary>
        /// Gets the container node housing this string
        /// </summary>
        /// <param name="value">string to search for</param>
        /// <param name="index">position in value</param>
        /// <returns>container node housing this string, null if none exists</returns>
        public override BurstNode? Search(string value, int index)
        {
            //finds the slot to look in
            var searchNode = nexts[GetSlot(value, index)];
            //if that slot is empty, return null, saying no node was found,
            //otherwise, continue searching in that slot
            return searchNode == null ? null : searchNode.Search(value, index + 1);
        }

        /// <summary>
        /// appends every string past this node to a given list
        /// </summary>
        /// <param name="starter">list to append to</param>
        internal override void GetAll(string prefix, List<string> starter)
        {
            string change = "";
            for (int i = 0; i < nexts.Length; i++)
            {
                var currNode = nexts[i];
                //recurses down every non-null next until it reaches the containers
                if (currNode == null) continue;
                currNode!.GetAll(prefix + change, starter);
                change = ((char)(AlphabetStart + i)).ToString();
            }
        }
    }
}
