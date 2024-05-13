﻿using Microsoft.Diagnostics.Tracing.Parsers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurstTrie.BSTBurstTrie
{
    public sealed class BurstContainer : BurstNode
    {
        /// <summary>
        /// BST bucket for the strings in this container
        /// </summary>
        BST container = new();
        public int ContainerLimit => ParentTrie.ContainerLimit;

        internal int countOffset = int.MaxValue;

        public override int Count => container.Count;

        internal BurstContainer(BurstTrie parent)
            : base(parent) { }

        /// <summary>
        /// Inserts a value into the container, bursts when out of space
        /// </summary>
        /// <param name="value">string to insert</param>
        /// <param name="index">slot in the string we are currently on</param>
        /// <param name="offset">whether or not to offset</param>
        /// <returns></returns>
        public override BurstNode Insert(string value, int index, int offset = 0)
        {
            //var parentTrie
            //Adding to difference of the old container count & new container count
            //if a duplicate is ignored, this does nothing, otherwise, it just increments by one
            var oldCount = container.Count;
            container.Insert(value);
            ParentTrie.Count += container.Count - oldCount;

            //checking if we need to burst
            if (container.Count - countOffset >= ContainerLimit)
            {
                //making an internal burst node to recieve our data
                var replacement = new BurstInternalNode(ParentTrie);

                //grabbing all our strings and placing them into our replacement node
                List<BurstContainer> progeny = new();
                List<string> reinsertionStrings = new(container.Count);

                CollectValues(container.Root);
                //offsetting the trie's count, since the insert function will increment it
                ParentTrie.Count -= container.Count;

                void CollectValues(BST.Node? current)
                {
                    if (current == null) return;

                    reinsertionStrings.Add(current.Value);

                    CollectValues(current.Left);
                    CollectValues(current.Right);
                }

                var rand = ParentTrie.Rand;

                for (int i = 0; i < reinsertionStrings.Count / 2; i++)
                {
                    var randSpot = rand.Next(reinsertionStrings.Count);
                    (reinsertionStrings[i], reinsertionStrings[randSpot]) = (reinsertionStrings[randSpot], reinsertionStrings[i]);
                }
                foreach (var item in reinsertionStrings)
                {
                    var insertionIndex = replacement.GetSlot(item, index);
                    if (replacement.nexts[insertionIndex] == null)
                    {
                        replacement.count++;
                        var newContainer = new BurstContainer(ParentTrie);
                        replacement.nexts[insertionIndex] = newContainer;
                        progeny.Add(newContainer);
                    }
                    replacement.nexts[insertionIndex]!.Insert(item, index + 1);
                }
                for (int i = 0; i < progeny.Count; i++)
                {
                    progeny[i].countOffset = Math.Max(0, progeny[i].Count * 2 - ContainerLimit);
                }

                //returning a replacement so our parent throws this container-node away, and points to that instead
                return replacement;
            }
            //returning ourselves so nothing changes
            return this;
        }

        /// <summary>
        /// Removes a value from the container
        /// </summary>
        /// <param name="value">the value to remove</param>
        /// <param name="index">the position in said value</param>
        /// <param name="success">whether or not the remove was successful</param>
        /// <returns>null if empty, itself if not</returns>
        public override BurstNode? Remove(string value, int index, out bool success)
        {
            //if our container managed to remove the value
            if (success = container.Remove(value))
            {
                //decrement trie count
                ParentTrie.Count--;
                //if our container is empty, return null to delete this node
                if (container.Count == 0)
                {
                    return null;
                }
            }
            //returning ourself so nothing changes
            return this;
        }

        /// <summary>
        /// returns this node if its container contains the wanted value
        /// </summary>
        /// <param name="value">value to search for</param>
        /// <param name="index">position to search at</param>
        /// <returns>if found, this, else null</returns>
        public override BurstNode? Search(string value, int index) => container.Contains(value) ? this : null;

        /// <summary>
        /// Adds all items from the container in-order to a given list
        /// </summary>
        /// <param name="starter">list to add to>/param>
        internal override void GetAll(List<string> starter) => container.GetAll(starter);
    }
}
