using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurstTrie.BSTBurstTrie
{
    public class BST
    {
        internal Node? Root;
        public int Count;

        // Recursive insert that doesn't insert duplicates
        public void Insert(string value) => Root = Insert(Root, value);

        //Yes, it's recursive, stack overflow won't happen because the container limits, so recusion is safe!
        Node Insert(Node? current, string value)
        {
            if (current == null)
            {
                Count++;
                return new(value);
            }

            //if you're wondering about the order of these checks, why the compares are in an equality check, instead of just being if-else-chained
            //CompareTo will return a 0 if two values aren't equal sometimes, basically that 0 means 'not comparable' 
            //Since we want to stop duplicates, this behavior can lead to different strings being counted as dupes, and blocked from adding
            //https://learn.microsoft.com/en-us/dotnet/standard/base-types/best-practices-strings
            if (value != current.Value)
            {
                if (value.CompareTo(current.Value) < 0)
                {
                    current.Left = Insert(current.Left, value);
                }
                else
                {
                    current.Right = Insert(current.Right, value);
                }
            }

            return current;
        }

        public bool Contains(string value) => Contains(Root, value);

        bool Contains(Node? current, string value)
        {
            if (current == null) return false;

            int comparisonResult = value.CompareTo(current.Value);

            if (comparisonResult < 0) return Contains(current.Left, value);

            else if (comparisonResult > 0) return Contains(current.Right, value);

            return true;
        }

        public bool Remove(string value) => Remove(ref Root, value);

        private bool Remove(ref Node? current, string value)
        {
            if (current == null)
                return false;


            if (value != current.Value)
            {
                if (value.CompareTo(current.Value) < 0) return Remove(ref current.Left, value);

                else return Remove(ref current.Right, value);
            }

            Count--;
            if (current.Left != null)
            {
                if (current.Right != null)
                {
                    current.Value = StealLeaf(ref current.Left);
                }
                else
                {
                    current = current.Left;
                }
            }
            else
            {
                current = current.Right;
            }
            return true;
        }

        string StealLeaf(ref Node probe)
        {
            if (probe.Right == null)
            {
                var val = probe.Value;
                probe = probe.Left!;
                return val;
            }
            return StealLeaf(ref probe.Right);
        }

        public void GetAll(List<string> starter) => GetAll(Root, starter);

        private void GetAll(Node? current, List<string> starter)
        {
            if (current == null) return;

            GetAll(current.Left, starter);

            starter.Add(current.Value);

            GetAll(current.Right, starter);
        }

        internal class Node
        {
            public string Value;
            public Node? Left;
            public Node? Right;

            public Node(string value)
            {
                Value = value;
            }
        }
    }
}
