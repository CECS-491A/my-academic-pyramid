//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using DataAccessLayer.Models.Messenger;
using DataAccessLayer.Models.School;

namespace DataAccessLayer.Models
{    
    public partial class User : IEntity 
    {

        public User()
        {
            this.ChildUsers = new HashSet<User>();
            this.Claims = new HashSet<Claim>();
            //this.Students = new List<Student>();
            //this.Questions = new List<Question>();
        }

        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Nullable<int> CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required, Column(TypeName = "datetime2"), DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
      
        public string Email { get; set; }
        //[Required, Column(TypeName = "datetime2"), DataType(DataType.DateTime)]
        //public DateTime UpdatedAt { get; set; }
        [Column(TypeName = "datetime2"), DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        ////public DateTime ModifiedDate { get; set; }
        //public string Location { get; set; }

        public Nullable<int> ParentUser_Id { get; set; }

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

        public virtual ICollection<Conversation> ChatRecords { get; set; }


        public virtual ICollection<FriendRelationship> FriendRelationship { get; set; }

        //public virtual ICollection<Student> Students { get; set; }

        //public ICollection<Question> Questions;

        /// <summary>
        /// Override Equals method.  The UserName of each User is unique.
        /// </summary>
        /// <param name="obj">Another User</param>
        /// <returns>Whether Users are equal or not</returns>
        public override bool Equals(object obj)
        {
            var user = obj as User;
            if(user == null)
            {
                return false;
            }
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
