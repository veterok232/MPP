using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    /// <summary>
    /// Tree struct to store the structure of trace results.
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public class TreeNode<T>
    {
        /// <summary>
        /// Data in tree node.
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Reference on parent node.
        /// </summary>
        public TreeNode<T> Parent { get; set; }
        /// <summary>
        /// Collection of children of this node.
        /// </summary>
        public ICollection<TreeNode<T>> Children { get; set; }

        /// <summary>
        /// Initializes a new instance of TreeNode.
        /// </summary>
        /// <param name="data">Data stored in the node.</param>
        public TreeNode(T data)
        {
            this.Data = data;
            this.Children = new LinkedList<TreeNode<T>>();
        }

        /// <summary>
        /// Adds a new child to this node.
        /// </summary>
        /// <param name="child">Data stored in child node.</param>
        /// <returns>Reference on added child node.</returns>
        public TreeNode<T> AddChild(T child)
        {
            var list = new List<object>();
            TreeNode<T> childNode = new TreeNode<T>(child) { Parent = this };
            this.Children.Add(childNode);
            return childNode;
        }
    }
}
