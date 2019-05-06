//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using DataAccessLayer;
//using DataAccessLayer.DTOs;
//using DataAccessLayer.Models.DiscussionForum;


//// Fix services for each
//// Update migration to include all types of questions in DBcontext
//// Go from there 

//namespace ServiceLayer.DiscussionForum
//{
//    public class SchoolQuestionServices : IQuestionServices<SchoolQuestion>, IPostedQuestionServices<SchoolQuestion>
//    {
//        private DatabaseContext _db;

//        public SchoolQuestionServices(DatabaseContext _db)
//        {
//            this._db = _db;
//        }

//        //public Question GetAnyQuestion(int questionId)
//        //{
//        //    return _db.Questions.Find(questionId);
//        //}

//        public SchoolQuestion PostQuestion(SchoolQuestion question)
//        {
//            _db.Entry(question).State = EntityState.Added;
//            return question;
//        }

//        public Question GetQuestion(int questionId)
//        {
//            SchoolQuestion q = new SchoolQuestion();
//            var a = q.GetType();
//            if (a is SchoolQuestion)
//                return q;

//            //return 
//            //return _db.SchoolQuestions.Find(questionId);
//        }

//        public List<SchoolQuestion> GetQuestions(List<int> ids)
//        {
//            try
//            {
//                return _db.SchoolQuestions
//                    .Where(q => q.SchoolId == ids[0])
//                    .OrderBy(q => q.DateCreated)
//                    .ToList();
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }

//        public SchoolQuestion UpdateQuestion(SchoolQuestion question)
//        {
//            _db.Entry(question).State = EntityState.Modified;
//            return question;
//        }

//        public SchoolQuestion DeleteQuestion(int questionId)
//        {
//            var question = GetQuestion(questionId);
//            if (question == null)
//            {
//                return null;
//            }
//            _db.Entry(question).State = EntityState.Deleted;
//            return question;
//        }

//        public SchoolQuestion CloseQuestion(int questionId)
//        {
//            var question = GetQuestion(questionId);
//            if (question.IsClosed == false)
//            {
//                question.IsClosed = true;
//                question = UpdateQuestion(question);
//                return question;
//            }
//            else
//                return question;
//        }

//        public SchoolQuestion IncreaseQuestionSpamCount(int questionId)
//        {
//            var question = GetQuestion(questionId);
//            question.SpamCount++;
//            question = UpdateQuestion(question);
//            return question;
//        }

//        public QuestionResponseDTO ApplyQuestionResponseDTOFormat(SchoolQuestion question)
//        {
//            QuestionResponseDTO qRDTO = new QuestionResponseDTO
//            {
//                QuestionId = question.Id,
//                // = q.SchoolId,
//                SchoolName = question.School.Name,
//                //DepartmentId = q.DepartmentId,
//                DepartmentName = null,
//                //CourseId = q.CourseId,
//                CourseName = null,
//                AccountId = question.AccountId,
//                AccountName = question.Account.UserName,
//                Text = question.Text,
//                MinimumExpForAnswer = question.ExpNeededToAnswer,
//                //IsDraft = q.IsDraft,
//                IsClosed = question.IsClosed,
//                SpamCount = question.SpamCount,
//                AnswerCount = question.Answers.Count
//            };
//            return qRDTO;
//        }

//        public SchoolQuestion DraftToQuestion(DraftQuestion draft, List<int> ids)
//        {
//            try
//            {
//                var question = (SchoolQuestion)draft;
//                //question.IsDraft = false;
//                question.IsClosed = false;
//                question.SpamCount = 0;
//                question.Answers = new List<Answer>();
//                question.DateCreated = DateTime.Now;
//                question.SchoolId = ids[0];
//                return question;
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }
//    }
//}