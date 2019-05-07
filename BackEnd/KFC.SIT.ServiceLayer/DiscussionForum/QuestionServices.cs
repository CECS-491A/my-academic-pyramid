using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models.DiscussionForum;


// Fix services for each
// Update migration to include all types of questions in DBcontext
// Go from there 

namespace ServiceLayer.DiscussionForum
{
    public class QuestionServices : IQuestionServices //: IQuestionServices<Question>, IPostedQuestionServices<Question>
    {
        private DatabaseContext _db;

        public QuestionServices(DatabaseContext _db)
        {
            this._db = _db;
        }

        public T GetAnyQuestion<T>(int questionId) where T : Question
        {
            return (T)_db.Questions.Find(questionId);
        }

        public Question PostQuestion(Question question)
        {
            question.DateCreated = DateTime.Now;
            return _db.Questions.Add(question);
        }

        //public PostedQuestion PostQuestionFromDraft(Question question)
        //{
        //    question.DateCreated = DateTime.Now;
        //    return _db.Questions.Add(question);
        //}

        public Question GetQuestion(int questionId)
        {
            return _db.Questions
                .Where(q => q.Id == questionId)
                .FirstOrDefault();
        }

        public PostedQuestion GetPostedQuestion(int questionId)
        {
            return _db.Questions
                .OfType<PostedQuestion>()
                .Where(q => q.Id == questionId)
                .FirstOrDefault();
        }

        public DraftQuestion GetDraftQuestion(int questionId)
        {
            return _db.Questions
                .OfType<DraftQuestion>()
                .Where(q => q.Id == questionId)
                .FirstOrDefault();
        }

        public List<SchoolQuestion> GetSchoolQuestions(int schoolId)
        {
            return _db.Questions
                .OfType<SchoolQuestion>()
                .Where(q => q.SchoolId == schoolId)
                .OrderBy(q => q.DateCreated)
                .ToList();
        }

        public List<DepartmentQuestion> GetSchoolDepartmentQuestions(int schoolDepartmentId)
        {
            return _db.Questions
                .OfType<DepartmentQuestion>()
                .Where(q => q.SchoolDepartmentId == schoolDepartmentId)
                .OrderBy(q => q.DateCreated)
                .ToList();
        }

        public List<CourseQuestion> GetCourseQuestions(int courseId)
        {
            return _db.Questions
                .OfType<CourseQuestion>()
                .Where(q => q.CourseId == courseId)
                .OrderBy(q => q.DateCreated)
                .ToList();
        }

        public List<DraftQuestion> GetDraftQuestionsForUser(int accountId)
        {
            return _db.Questions
                .OfType<DraftQuestion>()
                .Where(q => q.AccountId == accountId)
                .OrderBy(q => q.DateCreated)
                .ToList();
        }

        public Question UpdateQuestion(Question question)
        {
            question.DateUpdated = DateTime.Now;
            _db.Entry(question).State = EntityState.Modified;
            return question;
        }

        public Question DeleteQuestion(int questionId)
        {
            var question = GetQuestion(questionId);
            if (question == null)
            {
                return null;
            }
            return _db.Questions.Remove(question);
        }

        public PostedQuestion CloseQuestion(int questionId)
        {
            var question = _db.Questions
                .OfType<PostedQuestion>()
                .Where(q => q.Id == questionId)
                .FirstOrDefault();

            if (question.IsClosed == false)
            {
                question.IsClosed = true;
                _db.Entry(question).State = EntityState.Modified;
                return question;
            }
            else
                return question;
        }

        public PostedQuestion IncreaseQuestionSpamCount(int questionId)
        {
            var question = _db.Questions
                .OfType<PostedQuestion>()
                .Where(q => q.Id == questionId)
                .FirstOrDefault();

            question.SpamCount++;
            _db.Entry(question).State = EntityState.Modified;
            return question;
        }

        public string GetType(int questionId)
        {
            var question = _db.Questions
                .Where(q => q.Id == questionId)
                .FirstOrDefault();
            return question.GetType().Name;
        }


        //public PostedQuestion DraftToQuestion(QuestionCreateFromDraftRequestDTO draft)
        //{
        //    public int QuestionDraftId { get; set; }
        //public string QuestionType { get; set; }
        //public int SchoolId { get; set; }
        //public int DepartmentId { get; set; }
        //public int CourseId { get; set; }
        ////public int AccountId { get; set; }
        //public string Text { get; set; }
        //public int MinimumExpForAnswer { get; set; }
        //    try
        //    {
        //        var question = (Question)draft;
        //        var postedQuestion = (PostedQuestion)question;
        //        postedQuestion.IsClosed = false;
        //        question.SpamCount = 0;
        //        question.Answers = new List<Answer>();
        //        question.DateCreated = DateTime.Now;
        //        question.SchoolId = ids[0];
        //        return question;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public QuestionResponseDTO ApplyQuestionResponseDTOFormat(SchoolQuestion question)
        //{
        //    QuestionResponseDTO qRDTO = new QuestionResponseDTO
        //    {
        //        QuestionId = question.Id,
        //        // = q.SchoolId,
        //        SchoolName = question.School.Name,
        //        //DepartmentId = q.DepartmentId,
        //        DepartmentName = null,
        //        //CourseId = q.CourseId,
        //        CourseName = null,
        //        AccountId = question.AccountId,
        //        AccountName = question.Account.UserName,
        //        Text = question.Text,
        //        MinimumExpForAnswer = question.ExpNeededToAnswer,
        //        //IsDraft = q.IsDraft,
        //        IsClosed = question.IsClosed,
        //        SpamCount = question.SpamCount,
        //        AnswerCount = question.Answers.Count
        //    };
        //    return qRDTO;
        //}
    }
}