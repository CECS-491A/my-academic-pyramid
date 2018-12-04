using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;

namespace Service_Layer.UserManagement.TreeNodes
{
    /// <summary>
    /// Node that acts as the parent for all user
    /// nodes in each level of authorization.
    /// </summary>
    public class RootNode : INode
    {
        private Dictionary<int, INode> _children;

        public RootNode()
        {
            _children = new Dictionary<int, INode>();
        }

        public Dictionary<int, INode> Children
        {
            get
            {
                return _children;
            }
            set
            {
                _children = value;
            }
        }
        
        /// <summary>
        /// Add a user at any level in the tree.
        /// </summary>
        /// <param name="user">User to add</param>
        /// <param name="level">Depth of tree</param>
        /// <returns>Whether the add was successful or not</returns>
        public bool AddChild(User user, int level)
        {
            // Checks that the level exists in the tree and
            // that the user does not already exist in the tree.
            if(LevelIsValid(level - 1) && FindUserParent(user) == null)
            {
                // The parent of the user to be added
                RootNode root = GetRootAtLevel(level - 1);

                // The parent was found
                if(root != null)
                {
                    // Add the user
                    return root.AddDirectChild(user);
                }
            }
            // The level is invalid or the user already exists
            return false;
        }

        /// <summary>
        /// Adds a user as a direct child of the current node.
        /// </summary>
        /// <param name="user">User to be added</param>
        /// <returns>Whether the add was successful or not</returns>
        public bool AddDirectChild(User user)
        {
            // User node to be added
            UserNode node = new UserNode(user);

            // The current node has no children
            if (ChildrenIsNullOrEmpty(_children))
            {
                _children.Add(0, new RootNode()); // Add a root node
                _children.Add(node.NodeUser.Id, node); // Add the user
                return true; // Add successful
            }
            // The current node has children.  Check if the user exists
            else if (!_children.ContainsKey(node.NodeUser.Id))
            {
                _children.Add(node.NodeUser.Id, node); // Add the user
                return true; // Add successful
            }
            return false; // User already existed
        }

        /// <summary>
        /// Delete a child from anywhere in the tree.
        /// Must specify a level.
        /// </summary>
        /// <param name="user">User to be deleted</param>
        /// <param name="level">Level to delete from</param>
        /// <returns>Whether the delete was successful or not</returns>
        public bool DeleteChild(User user, int level)
        {
            // Checks that the level exists within the tree
            if (LevelIsValid(level))
            {
                // Parent of the user to be deleted
                RootNode root = GetRootAtLevel(level - 1);

                // Parent was found
                if (root != null)
                {
                    // Delete the user
                    return root.DeleteDirectChild(user);
                }
            }
            return false; // Level was invalid
        }

        /// <summary>
        /// Delete a user from anywhere in the tree,
        /// without specifying a level.
        /// </summary>
        /// <param name="user">User to be deleted</param>
        /// <returns>Whether the delete was successful or not</returns>
        public bool DeleteChild(User user)
        {
            // The current node has no children
            if (ChildrenIsNullOrEmpty(_children))
            {
                return false; // No user to delete
            }
            // The current node has children
            else
            {
                // Check if user is among direct children
                if (DeleteDirectChild(user))
                {
                    return true; // Delete was success
                }

                // Check next level for user
                RootNode root = GetRootAtLevel(1);
                return root.DeleteChild(user);
            }
        }

        /// <summary>
        /// Deletes a user that is a direct child of the current node.
        /// </summary>
        /// <param name="user">User to be deleted</param>
        /// <returns>Whether the delete was successful or not</returns>
        public bool DeleteDirectChild(User user)
        {
            // User to be deleted
            UserNode node = new UserNode(user);

            // Checks if current node has children and if user exists
            if (!ChildrenIsNullOrEmpty(_children) && _children.ContainsKey(node.NodeUser.Id))
            {
                // Delete user
                _children.Remove(node.NodeUser.Id);
                return true; // Deletion success
            }
            return false; // Current user has no children or user did not exist
        }

        /// <summary>
        /// Adds an authorization level anywhere in the tree.
        /// </summary>
        /// <param name="level">Level to add</param>
        /// <returns>Whether add was successful or not</returns>
        public bool AddLevel(int level)
        {
            // Checks if level is with tree
            if(LevelIsValid(level - 1))
            {
                // Parent of level to add
                RootNode root = GetRootAtLevel(level - 1);
                return root.AddLevelBelow(); // Add level
            }

            return false; // Level is invalid
        }

        /// <summary>
        /// Adds an authorization level directly below current node.
        /// </summary>
        /// <returns>Whether add was successful or not</returns>
        public bool AddLevelBelow()
        {
            // Parent of level to add
            RootNode parent = new RootNode();

            // Checks that current node has no children
            if(ChildrenIsNullOrEmpty(_children))
            {
                // Add the level
                _children.Add(0, parent);
            }
            // Current node has children
            else
            {
                parent.Children = _children; // Assign current node's children to parent's children
                _children.Clear(); // Clear current children
                _children.Add(0, parent); // Assign parent as current node's children
            }
            return true;
        }

        /// <summary>
        /// Delete a level from anywhere in the tree.
        /// </summary>
        /// <param name="level">Level to delete</param>
        /// <returns>Whether the delete was successful or not</returns>
        public bool DeleteLevel(int level)
        {
            // checks if the level is within the tree
            if (LevelIsValid(level))
            {
                // Parent of level to be deleted
                RootNode root = GetRootAtLevel(level - 1);
                return root.DeleteLevelBelow(); // Delete level
            }

            return false; // The level is invalid
        }

        /// <summary>
        /// Delete the level directly below the current node
        /// </summary>
        /// <returns>Whether the deletion was successful or not</returns>
        public bool DeleteLevelBelow()
        {
            // Checks that the current node has children
            if(!ChildrenIsNullOrEmpty(_children))
            {
                // The level to delete
                RootNode child = GetRootAtLevel(1);
                // Assign the current node's grandchildren as it's children
                _children = child.Children;
                return true;
            }
            return false; // There was nothing to delete
        }

        /// <summary>
        /// Find the parent of the user being searched.
        /// </summary>
        /// <param name="user">User to be found</param>
        /// <returns>The parent of the user</returns>
        public RootNode FindUserParent(User user)
        {
            // Checks that the current node does not have children
            if(ChildrenIsNullOrEmpty(_children))
            {
                // User not found
                return null;
            }
            // The user was found
            else if (_children.ContainsKey(user.Id))
            {
                return this;
            }
            // Check the next level
            else
            {
                // Next root the check
                RootNode root = GetRootAtLevel(1);
                return root.FindUserParent(user);
            }
            
        }

        /// <summary>
        /// Get the height of the current node.
        /// </summary>
        /// <returns>Height in the tree</returns>
        public int GetHeight()
        {
            // Checks that the current node has no children
            if(ChildrenIsNullOrEmpty(_children))
            {
                // The current node is at the bottom of the tree
                return 0;
            }
            // The current node has children
            else
            {
                // Next root to count
                RootNode child = GetRootAtLevel(1);
                return 1 + child.GetHeight();
            }
        }

        /// <summary>
        /// Gets the root parent at the specified level.
        /// </summary>
        /// <param name="level">Level of root</param>
        /// <returns>The root</returns>
        public RootNode GetRootAtLevel(int level)
        {
            // Current node
            if (level == 0)
            {
                return this;
            }
            // The level is invalid
            else if (ChildrenIsNullOrEmpty(_children))
            {
                return null;
            }
            // Check the next child root
            else
            {
                // Get the root node of the child
                if (_children.TryGetValue(0, out INode childNode))
                {
                    // The root node was found
                    if (childNode.GetType().Equals(typeof(RootNode)))
                    {
                        // Next root node to check
                        RootNode root = (RootNode)childNode;
                        return root.GetRootAtLevel(level - 1);
                    }
                }
            }
            return null; // The root node was not found
        }

        /// <summary>
        /// Checks that a level is within the height of the tree
        /// </summary>
        /// <param name="level">Level to check</param>
        /// <returns>Whether the level is within the tree or not</returns>
        public bool LevelIsValid(int level)
        {
            if(GetHeight() >= level)
            {
                return true;// The level is within the height of the tree
            }
            return false; // The level is not within the height of the tree
        }

        /// <summary>
        /// Checks whether a dictionary of children is empty or null.
        /// </summary>
        /// <param name="dictionary">Dictionary of children</param>
        /// <returns>Whether the dictionary is null or empty</returns>
        public bool ChildrenIsNullOrEmpty(Dictionary<int, INode> dictionary)
        {
            return (dictionary == null || dictionary.Count < 1);
        }

        /// <summary>
        /// Deletes all children from the current node.
        /// </summary>
        public void DeleteAllChildren()
        {
            _children.Clear();
        }

        /// <summary>
        /// Deletes all children directly under the current node.
        /// </summary>
        public void DeleteAllDirectChildren()
        {
            RootNode parent = GetRootAtLevel(1); // Hold onto the root node
            _children.Clear(); // Remove all the user nodes
            _children.Add(0, parent); // Add the root node back
        }

        // Methods I still need: Compare user levels, move a user to a different level

        public override string ToString()
        {
            return "\nChildren: " + string.Join("; ", _children.Select(x => x.Key + " => " + x.Value).ToArray()) + "\n";
        }
    }
}