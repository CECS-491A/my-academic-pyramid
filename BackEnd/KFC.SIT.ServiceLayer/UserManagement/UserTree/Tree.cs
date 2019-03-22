using DataAccessLayer;
using Service_Layer.UserManagement.UserTree;
using System;

namespace ServiceLayer.UserManagement.UserTree
{
    /// <summary>
    /// This class creates a user tree.
    /// </summary>
    public class Tree
    {
        public Tree(Node root)
        {
            Root = root;
        }

        public Node Root { get; set; }

        /// <summary>
        /// Add a user to the tree by specifying a parent.
        /// </summary>
        /// <param name="user">User to add</param>
        /// <param name="parent">Parent of user</param>
        /// <returns>Whether the user was added.</returns>
        public bool AddUser(User user, User parent)
        {
            // Attempt to find the user and parent in the tree.
            Node userNode = FindUser(user);
            Node parentNode = FindUser(parent);

            // The user is not already in the tree and the parent was found in the tree.
            if (userNode == null && parentNode != null)
            {
                // Add the user to the tree
                return parentNode.AddChild(user);
            }
            // The user is already in the tree or the parent was not found the the tree.
            return false;
        }

        /// <summary>
        /// Add a user to the tree by specifying a level
        /// </summary>
        /// <param name="user">User to add</param>
        /// <param name="level">Depth of the user</param>
        /// <returns>Whether the user was added</returns>
        public bool AddUser(User user, int level)
        {
            // Attempt to find the user and parent in the tree
            Node userNode = FindUser(user);
            Node parentNode = Root.FindNodeAtLevel(level - 1);

            // The user is not already in the tree and a parent was found in the tree.
            if (userNode == null && parentNode != null)
            {
                // Add the user to the tree
                return parentNode.AddChild(user);
            }
            // The user is already in the tree or a parent was not found the the tree.
            return false;
        }

        /// <summary>
        /// Delete a user from the tree.
        /// </summary>
        /// <param name="user">User to delete</param>
        /// <returns>Whether the user was deleted</returns>
        public bool DeleteUser(User user)
        {
            // Attempt to find user in the tree
            Node node = FindUser(user);
            
            // User was not found in the tree
            if(node == null)
            {
                return false;
            }

            // User to delete is the root of the tree
            if(Root.User.Equals(user) || node.Parent == null)
            {
                // Create a new empty root node
                Root = new Node("");
                // Move children under new root node
                MoveChildren(node, Root);
            }
            else
            {
                // Remove user as child of its parent
                node.Parent.Children.Remove(node);

                // User to be deleted has no children to move
                if (node.Children.Count < 1)
                {
                    return true;
                }
                // User has siblings under same parent to move children to
                else if (node.Parent.Children.Count > 1)
                {
                    // Parent's first child is the user to delete
                    if (node.Parent.Children[0].User.Equals(user))
                    {
                        // Move user's children to parent's second child
                        MoveChildren(node, node.Parent.Children[1]);
                    }
                    // Parent's first child the user's sibling
                    else
                    {
                        // Move user's children to parent's first child
                        MoveChildren(node, node.Parent.Children[0]);
                    }
                }
                // User to be deleted does not have a sibling under the same parent.
                else
                {
                    // Find another node with the same level as the user to be deleted.
                    Node parent = Root.FindNodeAtLevel(node.Depth());

                    // Another parent was found.
                    if (parent != null)
                    {
                        // Move the children to the new parent.
                        MoveChildren(node, parent);
                    }
                    // Another parent was not found.
                    else
                    {
                        // Move the children a level up to the user's parent.
                        MoveChildren(node, node.Parent);
                    }

                }
            }
            
            return true;
        }
        /// <summary>
        /// Move children from one parent to another parent.
        /// </summary>
        /// <param name="parent">Parent with children</param>
        /// <param name="newParent">New parent to move children to</param>
        public void MoveChildren(Node parent, Node newParent)
        {
            foreach (Node child in parent.Children)
            {
                // Add child to parent
                newParent.AddChild(child);
            }
        }

        /// <summary>
        /// Move a user node by specifying a new parent.
        /// </summary>
        /// <param name="user">User to move</param>
        /// <param name="parent">Parent to move to</param>
        /// <returns>Whether the move was successful</returns>
        public bool MoveUser(User user, User parent)
        {
            // Delete the user
            DeleteUser(user);
            // Re-add the user to new location
            return AddUser(user, parent);
        }

        /// <summary>
        /// Move a user node by specifying a new level
        /// </summary>
        /// <param name="user">User to move</param>
        /// <param name="level">Depth to move to</param>
        /// <returns>Whether the move was successful</returns>
        public bool MoveUser(User user, int level)
        {
            // Delete the user
            DeleteUser(user);
            // Re-add the user
            return AddUser(user, level);
        }

        /// <summary>
        /// Find a user in the tree.
        /// </summary>
        /// <param name="user">User to find</param>
        /// <returns>The user node if found, null if not found</returns>
        public Node FindUser(User user)
        {
            // Start search from root node
            return Root.FindUser(user);
        }

        /// <summary>
        /// Compare the depths of two users
        /// </summary>
        /// <param name="user1">first user to compare</param>
        /// <param name="user2">second user to compare</param>
        /// <returns>0 if equal, negative if user1's depth is less than user2's,
        /// or positive is user2's depth is less than user1's</returns>
        public int CompareUserDepth(User user1, User user2)
        {
            // Attempt to find each node in the tree
            Node node1 = FindUser(user1);
            Node node2 = FindUser(user2);

            return node1.CompareTo(node2);
        }
        
    }
}