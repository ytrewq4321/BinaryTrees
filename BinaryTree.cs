using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTrees
{  
    public class TreeNode<T>  where T: IComparable
    {
        public TreeNode(T data)
        {
            Data = data;
        }

        public TreeNode() { }

        public T Data { get; set; }

        public int WeightLeft;
        public int WeightRight;
        public TreeNode<T> Parrent { get; set; }
        public TreeNode<T> Right { get; set; } 
        public TreeNode<T> Left { get; set; }
    }

    public class BinaryTree<T>: IEnumerable<T> where T: IComparable
    {
        public int Count;
        public TreeNode<T> root { get; set; }
        public BinaryTree(T value)
        {
            root = new TreeNode<T>(value);          
        }

        public BinaryTree() { }

        public void Add(T value)
        {
            if (root == null)
            {
                root = new TreeNode<T>(value);
                Count++;
                return;
            }
            var current = root;

            while (true)
            {               
                if (current.Data.CompareTo(value) <= 0)
                {
                    if (current.Right == null)
                    {                       
                        current.Right = new TreeNode<T>(value);
                        current.Right.Parrent = current;
                        AddWeight(current.Right);
                        Count++;
                        break;
                    }                       
                    current = current.Right; 
                }
                else
                {
                    if (current.Left == null)
                    {
                        current.Left = new TreeNode<T>(value);
                        current.Left.Parrent = current;

                        AddWeight(current.Left);
                        Count++;
                        break;
                    }
                    current = current.Left;
                }     
            }
            return;
        }

        public bool Contains(T key)
        {
            if (root == null)
                return false;
            if (root.Data.CompareTo(key)==0)
                return true;
            var current = root;
            while (true)
            {
                if (current.Data.CompareTo(key) < 0)
                {
                    if (current.Right == null)
                        return false;
                    if (current.Right.Data.CompareTo(key)==0)
                        return true;
                    current = current.Right;
                }
                else
                {
                    if (current.Left == null)
                        return false;
                    if (current.Left.Data.CompareTo(key) == 0)
                        return true;
                    current = current.Left;
                }
            }
        }

        private void AddWeight(TreeNode<T> currentNode)
        {
            while(currentNode.Parrent!=null)
            {
                if(currentNode.Parrent.Right != currentNode)
                {
                    currentNode.Parrent.WeightLeft++;                   
                }
                else
                {
                    currentNode.Parrent.WeightRight++;
                }
                currentNode = currentNode.Parrent;
            } 
        }

       
        public IEnumerator<T> GetEnumerator()
        {   
            return GetValues(root).GetEnumerator();
        }

        public IEnumerable<T> GetValues(TreeNode<T> root)
        {
            if (root == null)
                yield break;
            if (root.Left != null)
                foreach (var value in GetValues(root.Left))
                    yield return value;
            yield return root.Data;
            if (root.Right != null)
                foreach (var value in GetValues(root.Right))
                    yield return value;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T this[int i]
        {
            get
            {
                var currentNode = root;
                var currentNodeIndex=root.WeightLeft;
                int parrentIndex;
                while (true)
                {
                    if (i == currentNodeIndex)
                        return currentNode.Data;
                    if(i<currentNodeIndex)
                    {
                        parrentIndex = currentNodeIndex;
                        currentNode = currentNode.Left;
                        currentNodeIndex = parrentIndex - currentNode.WeightRight-1;
                    }
                    else
                    {
                        parrentIndex = currentNodeIndex;
                        currentNode = currentNode.Right;
                        currentNodeIndex = parrentIndex + currentNode.WeightLeft+1;
                    }
                }
            }
        }
    }  
}
 