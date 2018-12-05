using DataAccessLayer;
using System.Collections.Generic;
// NOT DONE YET
namespace Service_Layer.UserManagement.TreeNodes
{
    /// <summary>
    /// Node that acts as the parent for all user
    /// nodes in each level of authorization.
    /// </summary>
    public class Node
    {
        public Node()
        {
            Children = new List<Node> ();
            Users = new HashSet<User>();
            Parent = null;
        }

        // do I need some sort of id?
        // should there be parent attribute?

        public HashSet<User> Users { get; set; }

        public List<Node> Children { get; set; }

        public Node Parent { get; set; }

        /// <summary>
        /// Add a user at any level in the tree.
        /// </summary>
        /// <param name="user">User to add</param>
        /// <param name="level">Depth of tree</param>
        /// <returns>Whether the add was successful or not</returns>
        public bool AddChild(User child, User parent)
        {
            Node parentNode = FindUserNode(parent);

            // Parent exists, but user does not already exist
            if (FindUserNode(child) == null && parentNode != null)
            {
                if (parentNode.ChildrenIsNullOrEmpty())
                {
                    Node childNode = new Node();
                    childNode.Users.Add(child);
                    childNode.Parent = parentNode;
                    parentNode.Children.Add(childNode);
                }
                else // How do you decide which child to put it in? ID?
                {

                }
                return true;
            }
            // The level is invalid or the user already exists
            return false;
        }

        /// <summary>
        /// Delete a user from anywhere in the tree,
        /// without specifying a level.
        /// </summary>
        /// <param name="user">User to be deleted</param>
        /// <returns>Whether the delete was successful or not</returns>
        public bool DeleteChild(User user)
        {
            Node userNode = FindUserNode(user);

            // User was found
            if(userNode != null && !userNode.UsersIsNullOrEmpty())
            {
                // Remove User
                userNode.Users.Remove(user);
                return true; // Delete success
            }
            // User to delete not found
            return false;
        }

        /// <summary>
        /// Find the parent of the user being searched.
        /// </summary>
        /// <param name="user">User to be found</param>
        /// <returns>The parent of the user</returns>
        public Node FindUserNode(User user)
        {
            // Current node has children, and correct user
            if (!UsersIsNullOrEmpty() && Users.Contains(user))
            {
                // User was found
                return this;
            }
            // User not found in current node
            else if (ChildrenIsNullOrEmpty())
            {
                // Current node has no children
                return null;
            }
            // Check if the user is in the children
            else
            {
                foreach (Node child in Children)
                {
                    // Search for user in children
                    Node node = child.FindUserNode(user);
                    // User was found
                    if(node != null)
                    {
                        return node;
                    }
                }
                // no user was found
                return null;
            }

        }
        
        
        public bool ChildrenIsNullOrEmpty()
        {
            return (Children == null || Children.Count < 1);
        }

        public bool UsersIsNullOrEmpty()
        {
            return (Users == null || Users.Count < 1);
        }

        // Methods I still need: Compare user levels, move a user to a different level

        public override string ToString()
        {
            return Users.ToString(); // idk
        }
    }
}