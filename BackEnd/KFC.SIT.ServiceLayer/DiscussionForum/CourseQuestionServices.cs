//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using DataAccessLayer;
//using DataAccessLayer.DTOs;
//using DataAccessLayer.Models.DiscussionForum;

//namespace ServiceLayer.DiscussionForum
//{
//    public class CourseQuestionServices : IQuestionServices<CourseQuestion>, IPostedQuestionServices<CourseQuestion>
//    {
//        private DatabaseContext _db;

//        public CourseQuestionServices(DatabaseContext _db)
//        {
//            this._db = _db;
//        }

//        public CourseQuestion PostQuestion(CourseQuestion question)
//        {
//            _db.Entry(question).State = EntityState.Added;
//            return question;
//        }

//        public CourseQuestion GetQuestion(int questionId)
//        {
//            return _db.CourseQuestions.Find(questionId);

//        }

//        public List<CourseQuestion> GetQuestions(List<int> ids)
//        {
//            try
//            {
//                return _db.CourseQuestions
//                    .Where(q => q.SchoolId == ids[0] && q.SchoolDepartmentId == ids[1] && q.CourseId == ids[3])
//                    .OrderBy(q => q.DateCreated)
//                    .ToList();
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }

//        public CourseQuestion UpdateQuestion(CourseQuestion question)
//        {
//            _db.Entry(question).State = EntityState.Modified;
//            return question;
//        }

//        public CourseQuestion DeleteQuestion(int questionId)
//        {
//            var question = GetQuestion(questionId);
//            if (question == null)
//            {
//                return null;
//            }
//            _db.Entry(question).State = EntityState.Deleted;
//            return question;
//        }

//        public CourseQuestion CloseQuestion(int questionId)
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

//        public CourseQuestion IncreaseQuestionSpamCount(int questionId)
//        {
//            var question = GetQuestion(questionId);
//            question.SpamCount++;
//            question = UpdateQuestion(question);
//            return question;
//        }

//        public QuestionResponseDTO ApplyQuestionResponseDTOFormat(CourseQuestion question)
//        {
//            QuestionResponseDTO qRDTO = new QuestionResponseDTO
//            {
//                QuestionId = question.Id,
//                // = q.SchoolId,
//                SchoolName = question.Course.SchoolDepartment.School.Name;
//                //DepartmentId = q.DepartmentId,
//                DepartmentName = question.SchoolDepartment.Department.Name,
//                //CourseId = q.CourseId,
//                CourseName = question.Course.Name,
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

//        public CourseQuestion DraftToQuestion(DraftQuestion draft, List<int> ids)
//        {
//            try
//            {
//                var question = (CourseQuestion)draft;
//                //question.IsDraft = false;
//                question.IsClosed = false;
//                question.SpamCount = 0;
//                question.Answers = new List<Answer>();
//                question.DateCreated = DateTime.Now;
//                question.SchoolId = ids[0];
//                question.SchoolDepartmentId = ids[1];
//                question.CourseId = ids[2];
//                return question;
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }
//    }
//}