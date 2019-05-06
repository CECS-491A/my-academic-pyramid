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
        //// To post Question 
        //public QuestionDTO(int schoolId, int departmentId, int courseId, int accountId, string text,
        //    int minimumExpForAnswer, bool isDraft)
        //{
        //    SchoolId = schoolId;
        //    DepartmentId = departmentId;
        //    CourseId = courseId;
        //    AccountId = accountId;
        //    Text = text;
        //    MinimumExpForAnswer = minimumExpForAnswer;
        //    IsDraft = isDraft;
        //}

        //// To update Question 
        //public QuestionDTO(int id, int schoolId, int departmentId, int courseId, int accountId, string text,
        //    int minimumExpForAnswer, bool isDraft)
        //{
        //    QuestionId = id;
        //    SchoolId = schoolId;
        //    DepartmentId = departmentId;
        //    CourseId = courseId;
        //    AccountId = accountId;
        //    Text = text;
        //    MinimumExpForAnswer = minimumExpForAnswer;
        //    IsDraft = isDraft;
        //}

        // need Id for updateQuestion not PostQuestion
        public string QuestionType { get; set; }
        public int SchoolId { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public int AccountId { get; set; }
        public string Text { get; set; }
        public int MinimumExpForAnswer { get; set; }
        //public bool IsDraft { get; set; }
    }

    //public class QuestionCreateFromDraftRequestDTO
    //{
    //    public int QuestionDraftId { get; set; }
    //    public int SchoolId { get; set; }
    //    public int DepartmentId { get; set; }
    //    public int CourseId { get; set; }
    //    public int AccountId { get; set; }
    //    public string Text { get; set; }
    //    public int MinimumExpForAnswer { get; set; }
    //}

    public class QuestionUpdateRequestDTO
    {
        public int QuestionId { get; set; }
        //public int AccountId { get; set; }
        public string Text { get; set; }
        public int MinimumExpForAnswer { get; set; }
        //public bool IsDraft { get; set; }
    }

    public class QuestionResponseDTO
    {
        public int QuestionId { get; set; }
        //public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        //public int DepartmentId { get; set; }
 //       public string DepartmentName { get; set; }
        //public int CourseId { get; set; }
 //       public string CourseName { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string Text { get; set; }
        public int MinimumExpForAnswer { get; set; }
        public bool IsClosed { get; set;}
        public int SpamCount { get; set; }
 //       public int AnswerCount { get; set; }
        //public bool IsDraft { get; set; }

        //public QuestionDTOResponse(Question question, string accountName)
        //{
        //    Question = question;
        //    AccountName = accountName;
        //}
    }

    public class QuestionDraftCreateRequestDTO
    {
        //// To post Question 
        //public QuestionDTO(int schoolId, int departmentId, int courseId, int accountId, string text,
        //    int minimumExpForAnswer, bool isDraft)
        //{
        //    SchoolId = schoolId;
        //    DepartmentId = departmentId;
        //    CourseId = courseId;
        //    AccountId = accountId;
        //    Text = text;
        //    MinimumExpForAnswer = minimumExpForAnswer;
        //    IsDraft = isDraft;
        //}

        //// To update Question 
        //public QuestionDTO(int id, int schoolId, int departmentId, int courseId, int accountId, string text,
        //    int minimumExpForAnswer, bool isDraft)
        //{
        //    QuestionId = id;
        //    SchoolId = schoolId;
        //    DepartmentId = departmentId;
        //    CourseId = courseId;
        //    AccountId = accountId;
        //    Text = text;
        //    MinimumExpForAnswer = minimumExpForAnswer;
        //    IsDraft = isDraft;
        //}

        // need Id for updateQuestion not PostQuestion
        public int SchoolId { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public int AccountId { get; set; }
        public string Text { get; set; }
        public int MinimumExpForAnswer { get; set; }
    }

    public class QuestionDraftUpdateRequestDTO
    {
        public int QuestionDraftId { get; set; }
        //public int AccountId { get; set; }
        public string Text { get; set; }
        public int MinimumExpForAnswer { get; set; }
    }

    public class QuestionDraftResponseDTO
    {
        public int QuestionDraftId { get; set; }
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int AccountId { get; set; }
        //public string AccountName { get; set; }
        public string Text { get; set; }
        public int MinimumExpForAnswer { get; set; }
        //public bool IsDraft { get; set; }

        //public QuestionDTOResponse(Question question, string accountName)
        //{
        //    Question = question;
        //    AccountName = accountName;
        //}
    }
}
