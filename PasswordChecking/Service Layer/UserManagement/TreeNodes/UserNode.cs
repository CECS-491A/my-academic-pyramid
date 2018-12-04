using DataAccessLayer;

namespace Service_Layer.UserManagement.TreeNodes
{
    /// <summary>
    /// Node holding user data.
    /// </summary>
    public class UserNode: INode
    {
        public UserNode(User user)
        {
            NodeUser = user;
        }

        public User NodeUser { get; set; }

        public override string ToString()
        {
            return NodeUser.Id + ":" + NodeUser.UserName;
        }

    }
}