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
//    public class DepartmentQuestionServices : IQuestionServices<DepartmentQuestion>, INotADraftQuestionServices<DepartmentQuestion>
//    {
//        private DatabaseContext _db;

//        public DepartmentQuestionServices(DatabaseContext _db)
//        {
//            this._db = _db;
//        }

//        public DepartmentQuestion PostQuestion(DepartmentQuestion question)
//        {
//            _db.Entry(question).State = EntityState.Added;
//            return question;
//        }

//        public DepartmentQuestion GetQuestion(int questionId)
//        {
//            return _db.DepartmentQuestions.Find(questionId);

//        }

//        public List<DepartmentQuestion> GetQuestions(List<int> ids)
//        {
//            try
//            {
//                return _db.DepartmentQuestions
//                    .Where(q => q.SchoolId == ids[0] && q.SchoolDepartmentId == ids[1])
//                    .OrderBy(q => q.DateCreated)
//                    .ToList();
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }

//        public DepartmentQuestion UpdateQuestion(DepartmentQuestion question)
//        {
//            _db.Entry(question).State = EntityState.Modified;
//            return question;
//        }

//        public DepartmentQuestion DeleteQuestion(int questionId)
//        {
//            var question = GetQuestion(questionId);
//            if (question == null)
//            {
//                return null;
//            }
//            _db.Entry(question).State = EntityState.Deleted;
//            return question;
//        }

//        public DepartmentQuestion CloseQuestion(int questionId)
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

//        public DepartmentQuestion IncreaseQuestionSpamCount(int questionId)
//        {
//            var question = GetQuestion(questionId);
//            question.SpamCount++;
//            question = UpdateQuestion(question);
//            return question;
//        }

//        public QuestionResponseDTO ApplyQuestionResponseDTOFormat(DepartmentQuestion question)
//        {
//            QuestionResponseDTO qRDTO = new QuestionResponseDTO
//            {
//                QuestionId = question.Id,
//                // = q.SchoolId,
//                SchoolName = question.School.Name,
//                //DepartmentId = q.DepartmentId,
//                DepartmentName = question.SchoolDepartment.Department.Name,
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

//        public DepartmentQuestion DraftToQuestion(DraftQuestion draft, List<int> ids)
//        {
//            try
//            {
//                var question = (DepartmentQuestion)draft;
//                //question.IsDraft = false;
//                question.IsClosed = false;
//                question.SpamCount = 0;
//                question.Answers = new List<Answer>();
//                question.DateCreated = DateTime.Now;
//                question.SchoolId = ids[0];
//                question.SchoolDepartmentId = ids[1];
//                return question;
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }
//    }
//}