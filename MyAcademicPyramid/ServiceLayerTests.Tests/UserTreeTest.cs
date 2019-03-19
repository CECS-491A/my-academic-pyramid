using DataAccessLayer;
using Service_Layer.UserManagement.UserTree;
using ServiceLayer.UserManagement.UserTree;
using System;
using Xunit;

namespace ServiceLayerTests.Tests
{
    public class UserTreeTest
    {
        /*
        Node root = new Node("Root");
        User krystal = new User("Krystal");
        User luis = new User("Luis");

        [Fact]
        public void Node_AddChild_InputUser_UniqueChildShouldReturnTrue()
        {
            // Arrange
            bool expected = true;

            // Act
            bool actual = root.AddChild(krystal);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_AddChild_InputUser_DuplicateChildShouldReturnFalse()
        {
            // Arrange
            root.AddChild(krystal);
            bool expected = false;

            // Act
            bool actual = root.AddChild(krystal);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_AddChild_InputUser_NullInputShouldThrowException()
        {
            // Arrange
            User user = null;
            bool expected = true;

            // Act
            bool actual = false;
            try
            {
                root.AddChild(user);
            }
            catch(NullReferenceException e)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_AddChild_InputNode_UniqueChildShouldReturnTrue()
        {
            // Arrange
            Node child = new Node(krystal.UserName);
            bool expected = true;

            // Act
            bool actual = root.AddChild(child);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_AddChild_InputNode_DuplicateChildShouldReturnFalse()
        {
            // Arrange
            Node child = new Node(krystal.UserName);
            root.AddChild(child);
            bool expected = false;

            // Act
            bool actual = root.AddChild(child);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_AddChild_InputNode_NullInputShouldThrowException()
        {
            // Arrange
            Node user = null;
            bool expected = true;

            // Act
            bool actual = false;
            try
            {
                root.AddChild(user);
            }
            catch (NullReferenceException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_Depth_RootShouldReturnZero()
        {
            // Arrange
            int expected = 0;

            // Act
            int actual = root.Depth();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_Depth_ChildShouldReturnValidDepth()
        {
            // Arrange
            Node n1 = new Node(krystal.UserName);
            Node n2 = new Node(luis.UserName);
            n1.AddChild(n2);
            root.AddChild(n1);
            int expected = 2;

            // Act
            int actual = n2.Depth();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_Depth_NullShouldThrowException()
        {
            // Arrange
            Node user = null;
            bool expected = true;

            // Act
            bool actual = false;
            try
            {
                user.Depth();
            }
            catch(NullReferenceException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_FindUser_ExistingUserShouldReturnUser()
        {
            // Arrange
            root.AddChild(krystal);
            Node user = new Node(krystal.UserName);
            user.Parent = root;
            Node expected = user;

            // Act
            Node actual = root.FindUser(krystal);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_FindUser_NonExistingUserShouldReturnNull()
        {
            // Arrange
            Node expected = null;

            // Act
            Node actual = root.FindUser(krystal);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_FindUser_NullShouldThrowException()
        {
            // Arrange
            User user = null;
            bool expected = true;

            // Act
            bool actual = false;
            try
            {
                root.FindUser(user);
            }
            catch (NullReferenceException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_FindNodeAtLevel_ValidShouldReturnNode()
        {
            // Arrange
            root.AddChild(krystal);
            Node user = new Node(krystal.UserName);
            user.Parent = root;
            Node expected = user;

            // Act
            Node actual = root.FindNodeAtLevel(1);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_FindNodeAtLevel_InvalidShouldReturnNull()
        {
            // Arrange
            Node expected = null;

            // Act
            Node actual = root.FindNodeAtLevel(1);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_IsDirectParentOf_DirectParentShouldReturnTrue()
        {
            // Arrange
            root.AddChild(krystal);
            Node user = new Node(krystal.UserName);
            user.Parent = root;
            bool expected = true;

            // Act
            bool actual = root.IsDirectParentOf(krystal);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_IsDirectParentOf_NotDirectParentShouldReturnFalse()
        {
            // Arrange
            root.AddChild(krystal);
            Node krystalNode = root.FindUser(krystal);
            krystalNode.AddChild(luis);
            bool expected = false;

            // Act
            bool actual = root.IsDirectParentOf(luis);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_IsDirectParentOf_NullShouldReturnFalse()
        {
            // Arrange
            User user = null;
            bool expected = false;

            // Act
            bool actual = root.IsDirectParentOf(user);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_CompareTo_LesserDepthShouldReturnPositive()
        {
            // Arrange
            root.AddChild(krystal);
            Node user = root.FindUser(krystal);
            int expected = 1;

            // Act
            int actual = user.CompareTo(root);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_CompareTo_GreaterDepthShouldReturnNegative()
        {
            // Arrange
            root.AddChild(krystal);
            Node user = root.FindUser(krystal);
            int expected = -1;

            // Act
            int actual = root.CompareTo(user);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_CompareTo_EqualDepthShouldReturnZero()
        {
            // Arrange
            int expected = 0;

            // Act
            int actual = root.CompareTo(root);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_CompareTo_NullShouldThrowException()
        {
            // Arrange
            Node user = null;
            bool expected = true;

            // Act
            bool actual = false;
            try
            {
                root.CompareTo(user);
            }
            catch (NullReferenceException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_IsAbove_AboveShouldReturnTrue()
        {
            // Arrange
            Node krystalNode = new Node(krystal.UserName);
            Node luisNode = new Node(luis.UserName);
            krystalNode.AddChild(luisNode);
            root.AddChild(krystalNode);
            Node user = root.FindUser(luis);
            bool expected = true;

            // Act
            bool actual = root.IsAbove(user);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_IsAbove_BelowShouldReturnFalse()
        {
            // Arrange
            Node krystalNode = new Node(krystal.UserName);
            Node luisNode = new Node(luis.UserName);
            krystalNode.AddChild(luisNode);
            root.AddChild(krystalNode);
            Node user = root.FindUser(luis);
            bool expected = false;

            // Act
            bool actual = user.IsAbove(root);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Node_IsAbove_NullShouldThrowException()
        {
            // Arrange
            Node user = null;
            bool expected = true;

            // Act
            bool actual = false;
            try
            {
                root.IsAbove(user);
            }
            catch (NullReferenceException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Tree_AddUser_ByParent_ExistingParentShouldReturnTrue()
        {
            // Arrange
            Tree tree = new Tree(root);
            tree.Root.AddChild(krystal);
            bool expected = true;

            // Act
            bool actual = tree.AddUser(luis,krystal);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Tree_AddUser_ByParent_NonExistingParentShouldReturnFalse()
        {
            // Arrange
            Tree tree = new Tree(root);
            bool expected = false;

            // Act
            bool actual = tree.AddUser(luis,krystal);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Tree_AddUser_ByParent_NullShouldThrowException()
        {
            // Arrange
            Tree tree = new Tree(root);
            bool expected = true;

            // Act
            bool actual = false;
            try
            {
                tree.AddUser(krystal, null);
            }
            catch (NullReferenceException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Tree_AddUser_ByLevel_ValidLevelShouldReturnTrue()
        {
            // Arrange
            Tree tree = new Tree(root);
            bool expected = true;

            // Act
            bool actual = tree.AddUser(krystal,1);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Tree_AddUser_ByLevel_InvalidLevelReturnFalse()
        {
            // Arrange
            Tree tree = new Tree(root);
            bool expected = false; ;

            // Act
            bool actual = tree.AddUser(krystal, 5);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Tree_DeleteUser_ExistingUserShouldReturnTrue()
        {
            // Arrange
            Tree tree = new Tree(root);
            tree.AddUser(krystal,1);
            bool expected = true;

            // Act
            bool actual = tree.DeleteUser(krystal);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Tree_DeleteUser_NonExistingUserShouldReturnFalse()
        {
            // Arrange
            Tree tree = new Tree(root);
            bool expected = false;

            // Act
            bool actual = tree.DeleteUser(krystal);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Tree_DeleteUser_NullInputShouldThrowException()
        {
            // Arrange
            Tree tree = new Tree(root);
            User user = null;
            bool expected = true;

            // Act
            bool actual = false;
            try
            {
                tree.DeleteUser(user);
            }
            catch (NullReferenceException e)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }
        */
    }
}
