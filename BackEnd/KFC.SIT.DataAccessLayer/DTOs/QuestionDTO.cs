using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.DiscussionForum;

namespace DataAccessLayer.DTOs
{
    public class QuestionCreateRequestDTO
    {
        public string QuestionType { get; set; }
        public int SchoolId { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public int AccountId { get; set; }
        public string Text { get; set; }
        public int ExpNeededToAnswer { get; set; }
    }

    public class QuestionUpdateRequestDTO
    {
        public int QuestionId { get; set; }
        //public int AccountId { get; set; }
        public string Text { get; set; }
        public int ExpNeededToAnswer { get; set; }
    }

    public class QuestionResponseDTO
    {
        public int QuestionId { get; set; }
        //public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        //public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        //public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string Text { get; set; }
        public int ExpNeededToAnswer { get; set; }
        public bool IsClosed { get; set;}
        public int SpamCount { get; set; }
        public int AnswerCount { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }

    //public class QuestionDraftCreateRequestDTO
    //{
    //    //public int SchoolId { get; set; }
    //    //public int DepartmentId { get; set; }
    //    //public int CourseId { get; set; }
    //    //public int AccountId { get; set; }
    //    public string Text { get; set; }
    //    public int MinimumExpForAnswer { get; set; }
    //}

    //public class QuestionDraftUpdateRequestDTO
    //{
    //    public int QuestionDraftId { get; set; }
    //    //public int AccountId { get; set; }
    //    public string Text { get; set; }
    //    public int MinimumExpForAnswer { get; set; }
    //}


    public class QuestionCreateFromDraftRequestDTO
    {
        public int QuestionDraftId { get; set; }
        public string QuestionType { get; set; }
        public int SchoolId { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        //public int AccountId { get; set; }
        public string Text { get; set; }
        public int ExpNeededToAnswer { get; set; }
    }

    public class DraftQuestionResponseDTO
    {
        public int QuestionDraftId { get; set; }
 //       public int SchoolId { get; set; }
 //       public string SchoolName { get; set; }
 //       public int DepartmentId { get; set; }
 //       public string DepartmentName { get; set; }
 //       public int CourseId { get; set; }
 //       public string CourseName { get; set; }
 //       public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string Text { get; set; }
        public int ExpNeededToAnswer { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
