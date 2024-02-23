using BinarySearchTreeExample;

using System;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace BurstTrieTypes
{
    public class BurstTrie
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
        public bool Contains(BurstNode node) => node.Parent == this;
        public List<string> GetAll() => root.GetAll();
    }
    /// <summary>
    /// Polymorphic base for the two types of nodes in the Trie
    /// </summary>
    public abstract class BurstNode
    {
        /// <summary>
        /// Thre Trie this node belongs to
        /// </summary>
        internal BurstTrie Parent;
        /// <summary>
        /// The amount of values contained in this node
        /// </summary>
        public abstract int Count { get; }
        /// <summary>
        /// Creates the Node referencing its parent
        /// </summary>
        protected BurstNode(BurstTrie parent) => Parent = parent;
        /// <summary>
        /// Abstract insertion function
        /// </summary>
        /// <param name="value">the string to insert</param>
        /// <param name="index">the current spot in the string</param>
        /// <returns>replacement node to backprop</returns>
        public abstract BurstNode Insert(string value, int index);
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
    public sealed class BurstInternalNode : BurstNode
    {
        /// <summary>
        /// Set of next nodes within range of Alphabet
        /// </summary>
        BurstNode?[] nexts;

        public char AlphabetStart => Parent.AlphabetStart;
        public char AlphabetEnd => Parent.AlphabetEnd;

        private int count;
        public override int Count => count;

        /// <summary>
        /// Gets the index in nexts denoted by the character at a specified position in the input string
        /// </summary>
        /// <param name="value">the string</param>
        /// <param name="index">the position in value to check for</param>
        /// <returns>the index in nexts</returns>
        /// <exception cref="ArgumentException">throws if the character is outside the bounds of the alpha</exception>
        int GetSlot(string value, int index)
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
                nexts[insertionIndex] = new BurstContainer(Parent);
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
        internal override void GetAll(List<string> starter)
        {
            for (int i = 0; i < nexts.Length; i++)
            {
                //recurses down every non-null next until it reaches the containers
                if (nexts[i] == null) continue;
                nexts[i]!.GetAll(starter);
            }
        }
    }
    public sealed class BurstContainer : BurstNode
    {
        /// <summary>
        /// BST bucket for the strings in this container
        /// </summary>
        BST container = new();
        public int ContainerLimit => Parent.ContainerLimit;

        public override int Count => container.Count;


        internal BurstContainer(BurstTrie parent)
            : base(parent) { }

        /// <summary>
        /// Inserts a value into the container, bursts when out of space
        /// </summary>
        /// <param name="value">string to insert</param>
        /// <param name="index">slot in the string we are currently on</param>
        /// <returns></returns>
        public override BurstNode Insert(string value, int index)
        {
            var oldCount = container.Count;
            container.Insert(value);

            Parent.Count += container.Count - oldCount;

            if (container.Count >= ContainerLimit)
            {
                var replacement = new BurstInternalNode(Parent);

                CollectValues(container.Root);
                Parent.Count -= container.Count;

                void CollectValues(BST.Node? current)
                {
                    if (current == null) return;

                    replacement.Insert(current.Value, index);
                    CollectValues(current.Left);
                    CollectValues(current.Right);
                }
                
                return replacement;
            }
            return this;
        }

        public override BurstNode? Remove(string value, int index, out bool success)
        {
            if (success = container.Remove(value))
            {
                Parent.Count--;
                if (container.Count == 0)
                {
                    return null;
                }
            }
            return this;
        }

        public override BurstNode? Search(string value, int index) => container.Contains(value) ? this : null;

        internal override void GetAll(List<string> starter) => container.GetAll(starter);


    }
}