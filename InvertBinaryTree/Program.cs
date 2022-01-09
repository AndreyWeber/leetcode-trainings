using System;
using System.Collections.Generic;

namespace InvertBinaryTree
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    class Program
    {
        private static TreeNode Init3Levels()
        {
            var result = new TreeNode(val: 4);

            result.left = new TreeNode(
                val: 2,
                left: new TreeNode(val: 1),
                right: new TreeNode(val: 3)
            );

            result.right = new TreeNode(
                val: 7,
                left: new TreeNode(val: 6),
                right: new TreeNode(val: 9)
            );

            return result;
        }

        private static TreeNode Init2Levels() =>
            new TreeNode(
                val: 2,
                left: new TreeNode(1),
                right: new TreeNode(3)
            );

        /**
         * Invert binary tree using recursion
         */
        private static TreeNode InvertTreeRecur(TreeNode root)
        {
            TreeNode Traverse(TreeNode node)
            {
                if (node is null)
                {
                    return null;
                }

                var right = Traverse(node.right);
                var left = Traverse(node.left);

                node.left = right;
                node.right = left;

                return node;
            }

            return Traverse(root);
        }

        /**
         * Invert binary tree using queue and while loop (iterative approach)
         */
        private static TreeNode InvertTreeIter(TreeNode root)
        {
            var queue = new Queue<TreeNode>();

            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node is null)
                {
                    continue;
                }

                var right = node.right;
                var left = node.left;

                queue.Enqueue(right);
                queue.Enqueue(left);

                node.right = left;
                node.left = right;
            }

            return root;
        }

        public static void Main(string[] args)
        {
            var root = Init3Levels();

            //var result = InvertTree(root);
            var result = InvertTreeIter(root);
        }
    }
}
