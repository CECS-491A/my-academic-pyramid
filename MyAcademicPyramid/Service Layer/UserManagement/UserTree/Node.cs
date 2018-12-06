using DataAccessLayer;
using System;
using System.Collections.Generic;

namespace Service_Layer.UserManagement.UserTree
{
    /// <summary>
    /// Node in a user tree.
    /// </summary>
    public class Node
    {
        public Node()
        {
            Children = new List<Node>();
            User = new User("");
            Parent = null;
        }

        public Node(string userName)
        {
            Children = new List<Node> ();
            User = new User(userName);
            Parent = null;
        }

        public User User { get; set; }

        public List<Node> Children { get; set; }

        public Node Parent { get; set; }

        /// <summary>
        /// Add a direct child to the node, by specifying a user.
        /// </summary>
        /// <param name="user">User to add</param>
        /// <returns>Whether the user add was successful</returns>
        public bool AddChild(User user)
        {
            // Create a user node
            Node child = new Node(user.UserName)
            {
                // Set the parent to the current node
                Parent = this
            };

            // User is not already a direct child
            if (!Children.Contains(child))
            {
                // Add the user
                Children.Add(child);
                return true;
            }
            // User was already a child
            return false;
        }

        /// <summary>
        /// Add a direct child to the node, by specifying a user node.
        /// </summary>
        /// <param name="user">User to add</param>
        /// <returns>Whether the user add was successful</returns>
        public bool AddChild(Node user)
        {
            user.Parent = this;

            // User is not already a direct child
            if (!Children.Contains(user))
            {
                // Add the user
                Children.Add(user);
                return true;
            }
            // User was already a child
            return false;
        }


        /// <summary>
        /// Gets the depth of the node in a tree
        /// </summary>
        /// <returns>depth</returns>
        public int Depth()
        {
            // Node is the root
            if(Parent == null)
            {
                return 0;
            }
            // Node is not the root
            else
            {
                // Get the depth of the next level up
                return 1 + Parent.Depth();
            }
        }

        /// <summary>
        /// Finds a user below the current node.
        /// </summary>
        /// <param name="user">User to find</param>
        /// <returns>User node if found, null if not found</returns>
        public Node FindUser(User user)
        {
            // Current node matches the user
            if(user.Equals(User))
            {
                return this;
            }
            // Current node has no children
            else if(Children.Count < 1)
            {
                // User not found
                return null;
            }
            // Current node has children
            else
            {
                // Search each child
                foreach (Node child in Children)
                {
                    // Find node in child
                    Node node = child.FindUser(user);
                    // User is found
                    if(node != null)
                    {
                        // Return user
                        return node;
                    }
                }
                // User was not found
                return null;
            }
        }

        /// <summary>
        /// Find a node at a specified depth
        /// </summary>
        /// <param name="level">Depth of node</param>
        /// <returns>Node is found, null if not found</returns>
        public Node FindNodeAtLevel(int level)
        {
            // Specified level is current node
            if(level == 0)
            {
                return this;
            }
            // Current node has children
            else if(Children.Count > 0)
            {
                if(level == 1)
                {
                    // Return a child
                    return Children[0];
                }

                // Check the level of each child
                foreach (Node child in Children)
                {
                    Node node = child.FindNodeAtLevel(level - 1);
                    if (node != null)
                    {
                        // A node was found.
                        return node;
                    }
                }
            }
            // No node was found
            return null;
        }

        /// <summary>
        /// Checks if current node is a direct parent of a user.
        /// </summary>
        /// <param name="user">User to check</param>
        /// <returns>Whether node is a parent</returns>
        public bool IsDirectParentOf(User user)
        {
            // Compare each child with user
            foreach(Node child in Children)
            {
                if (child.User.Equals(user))
                {
                    // User was found as child
                    return true;
                }
            }
            // User was not found
            return false;
        }

        /// <summary>
        /// Compare the depth of current node to another node.
        /// </summary>
        /// <param name="user">User node to compare</param>
        /// <returns>0 if equal, negative is this node's depth is less than the user node's,
        /// and positive is this node's depth is greater than the user node's</returns>
        public int CompareTo(Node user)
        {
            return Depth() - user.Depth();
        }

        /// <summary>
        /// Checks if the current node's depth is lower than another node's
        /// </summary>
        /// <param name="user">User node to compare</param>
        /// <returns>Whether the current node is higher in the tree than the other node</returns>
        public bool IsAbove(Node user)
        {
            if(Depth() < user.Depth())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Prints the tree in the console.
        /// Use: For demo and testing purposes.
        /// </summary>
        /// <param name="indent">Indendtion string</param>
        /// <param name="last">Last node</param>
        public void PrintTree(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "| ";
            }
            Console.WriteLine(User);

            for (int i = 0; i < Children.Count; i++)
                Children[i].PrintTree(indent, i == Children.Count - 1);
        }
    }
}