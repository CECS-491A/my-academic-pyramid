//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using DataAccessLayer.Repository;
    using System;
    using System.Collections.Generic;
    
    public partial class User : IEntity
    {
 
        public User()
        {
            this.ChildUsers = new HashSet<User>();
            this.Claims = new HashSet<Claim>();
        }

        public User(string userName)

        {
            UserName = userName;
            this.Claims = new HashSet<Claim>();
        }

        public User(string userName, PasswordQA passwordQA)

        {
            UserName = userName;
            this.Claims = new HashSet<Claim>();
            this.passwordQA = passwordQA;
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public String CreatedDate { get; set; }
        public String HashPassword { get; set; }
        public Nullable<int> ParentUser_Id { get; set; }

        public PasswordQA passwordQA { get; set; }
    
        /// <summary>
        /// Children users below user.
        /// </summary>
        public virtual ICollection<User> ChildUsers { get; set; }
        /// <summary>
        /// Parent user above user.
        /// </summary>
        public virtual User ParentUser { get; set; }

        public virtual ICollection<Claim> Claims { get; set; }

        public virtual ICollection<UserSession> userSessions { get; set; }

        /// <summary>
        /// Override Equals method.  The UserName of each User is unique.
        /// </summary>
        /// <param name="obj">Another User</param>
        /// <returns>Whether Users are equal or not</returns>
        public override bool Equals(object obj)
        {
            var user = obj as User;
            return UserName.Equals(user.UserName);
        }

        /// <summary>
        /// Override HashCode.  The UserName of each User is unique.
        /// </summary>
        /// <returns>UserName hashcode</returns>
        public override int GetHashCode()
        {
            return UserName.GetHashCode();
        }

        /// <summary>
        /// Override ToString.  Output username
        /// </summary>
        /// <returns>username</returns>
        public override string ToString()
        {
            return UserName;
        }
    }
}
