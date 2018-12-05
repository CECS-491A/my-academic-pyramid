using DataAccessLayer;
using Service_Layer.UserManagement.UserTree;

namespace ServiceLayer.UserManagement.UserTree
{
    public class Tree
    {
        public Tree(Node root)
        {
            Root = root;
        }

        public Node Root { get; set; }

        public bool AddUser(User user, User parent)
        {
            Node parentNode = FindUser(parent);
            if(parentNode != null)
            {
                return parentNode.AddChild(user);
            }
            return false;
        }

        public bool AddUser(User user, int level)
        {
            Node parent = Root.FindNodeAtLevel(level);
            return parent.AddChild(user);
        }

        // delete user - move children to same level, different branch
        public bool DeleteUser(User user)
        {
            Node node = FindUser(user);
            
            if(node == null)
            {
                return false;
            }

            node.Parent.Children.Remove(node);

            if (node.Children.Count < 1) // Node to be deleted has no children.
            {
                return true;
            }
            else if(node.Parent.Children.Count > 1) // Node has siblings
            {
                if (node.Parent.Children[0].User.Equals(user))
                {
                    foreach(Node child in node.Children)
                    {
                        return node.Parent.Children[1].AddChild(child);
                    }
                }
                else
                {
                    foreach (Node child in node.Children)
                    {
                        return node.Parent.Children[0].AddChild(child);
                    }
                }
            }
            else // find different branch
            {
                node.Parent.Children.Remove(node);
                Node parent = Root.FindNodeAtLevel(node.Depth());
                foreach(Node child in node.Children)
                {
                    return parent.AddChild(child);
                }
            }
            return false;
        }

        public bool MoveUser(User user, User parent)
        {
            DeleteUser(user);

            return AddUser(user, parent);
        }

        public bool MoveUser(User user, int level)
        {
            DeleteUser(user);

            return AddUser(user, level);
        }

        public Node FindUser(User user)
        {
            return Root.FindUser(user);
        }

        public int CompareUserDepth(User user1, User user2)
        {
            Node node1 = FindUser(user1);
            Node node2 = FindUser(user2);

            return node1.CompareTo(node2);
        }
    }
}