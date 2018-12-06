using DataAccessLayer;
using Service_Layer.UserManagement.UserTree;
using Xunit;

namespace ServiceLayerTests.Tests
{
    public class UserTreeTest
    {
        Node root = new Node("Root");
        User krystal = new User("Krystal", 100);
        User luis = new User("Luis", 200);
        User trong = new User("Trong", 300);
        User arturo = new User("Arturo", 400);
        User kevin = new User("Kevin", 500);
        User victor = new User("Victor", 600);

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

    }
}
