using DataAccessLayer;
using System.Collections.Generic;

namespace Service_Layer.UserManagement.UserTree
{
    /// <summary>
    /// Node that acts as the parent for all user
    /// nodes in each level of authorization.
    /// </summary>
    public class Node
    {
        public Node(string userName)
        {
            Children = new List<Node> ();
            User = new User(userName);
            Parent = null;
        }

        public User User { get; set; }

        public List<Node> Children { get; set; }

        public Node Parent { get; set; }

        public bool AddChild(User user)
        {
            Node child = new Node(user.UserName)
            {
                Parent = this
            };

            if (!Children.Contains(child))
            {
                Children.Add(child);
                return true;
            }
            return false;
        }

        public bool AddChild(Node child)
        {
            child.Parent = this;

            if (!Children.Contains(child))
            {
                Children.Add(child);
                return true;
            }
            return false;
        }

        public bool AddParent(User user)
        {
            Node parent = new Node(user.UserName);
            if (Parent != null && !Parent.Children.Contains(parent))
            {
                Parent.Children.Add(parent);
                Parent = parent; // does this reference right parent?
                return true;
            }
            return false;
        }

        // depth
        public int Depth()
        {
            if(Parent == null)
            {
                return 0;
            }
            else if(Parent.User == null)
            {
                return 1;
            }
            else
            {
                return 1 + Parent.Depth();
            }
        }

        public Node FindUser(User user)
        {
            if(user == User)
            {
                return this;
            }
            else if(Children.Count < 1)
            {
                return null;
            }
            else
            {
                foreach(Node child in Children)
                {
                    Node node = child.FindUser(user);
                    if(node != null)
                    {
                        return node;
                    }
                }
                return null;
            }
        }

        public Node FindNodeAtLevel(int level)
        {
            if(level == 0)
            {
                return this;
            }
            else if(level == 1)
            {
                if(Children.Count > 0)
                {
                    return Children[0];
                }
                return null;
            }
            else
            {
                if(Children.Count > 0)
                {
                    foreach (Node child in Children)
                    {
                        Node node = FindNodeAtLevel(level-1);
                        if(node != null)
                        {
                            return node;
                        }
                    }
                }

                return null;
            }
        }

        public bool IsDirectParentOf(User user)
        {
            foreach(Node child in Children)
            {
                if (child.User.Equals(user))
                {
                    return true;
                }
            }
            return false;
        }

        public int CompareTo(Node user)
        {
            return Depth() - user.Depth();
        }

        public bool IsAbove(Node user)
        {
            if(Depth() < user.Depth())
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return User.ToString();
        }
    }
}