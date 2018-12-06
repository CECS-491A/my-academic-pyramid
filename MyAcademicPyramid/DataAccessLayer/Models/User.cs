using System;
using System.Collections.Generic;
using DataAccessLayer.Repository;

namespace DataAccessLayer
{
    /// <summary>
    /// Class that implements IEntity class
    /// an instance of User class will be created for every people who own an account of My Academic Pyramid web application. 
    /// </summary>
    public class User : IEntity
    {

        /// <summary>
        /// Constructor of class User 
        /// Takes in userName and id 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="id"></param>
        /// 

        /// <summary>
        /// Overloading Constructor of class User
        /// Takes in the userName
        /// </summary>
        /// <param name="userName"></param>
        public User ()
        {
            Claims = new List<Claim>();
        }
        public User(string userName)
        {
            UserName = userName;
            Claims = new List<Claim>();
        }
        
        public int Id { get; set; }

        public string UserName { get; set; }

        // List of string to store user Claims
        public virtual List<Claim> Claims { get; set; }

        /// <summary>
        /// Compares the UserName with other object
        /// It's used to check the existence of the user in the tree.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns> true/false </returns>
        public override bool Equals(object obj)
        {
            if (obj is User item)
            {
                return UserName.Equals(item.UserName);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// returns the hash code of the UserName
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return UserName.GetHashCode();
        }

        /// <summary>
        /// returns a name of the user
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return UserName;
        }

    }




}