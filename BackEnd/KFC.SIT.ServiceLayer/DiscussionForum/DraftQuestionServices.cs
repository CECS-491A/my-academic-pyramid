//using DataAccessLayer;
//using DataAccessLayer.DTOs;
//using DataAccessLayer.Models.DiscussionForum;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;

//namespace ServiceLayer.DiscussionForum
//{
//    public class DraftQuestionServices : IQuestionServices<Question>
//    {
//        private DatabaseContext _db;

//        public DraftQuestionServices(DatabaseContext _db)
//        {
//            this._db = _db;
//        }

//        public DraftQuestion PostQuestion(DraftQuestion question)
//        {
//            _db.Entry(question).State = EntityState.Added;
//            return question;
//        }

//        public DraftQuestion GetQuestion(int questionId)
//        {
//            return _db.DraftQuestions.Find(questionId);
//        }

//        public List<DraftQuestion> GetQuestions(List<int> ids)
//        {
//            try
//            {
//                return _db.DraftQuestions
//                    .Where(q => q.AccountId == ids[0])
//                    .ToList();
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }

//        public DraftQuestion UpdateQuestion(DraftQuestion question)
//        {
//            _db.Entry(question).State = EntityState.Modified;
//            return question;
//        }

//        public DraftQuestion DeleteQuestion(int questionId)
//        {
//            var question = GetQuestion(questionId);
//            if (question == null)
//            {
//                return null;
//            }
//            _db.Entry(question).State = EntityState.Deleted;
//            return question;
//        }

//        public QuestionResponseDTO ApplyQuestionResponseDTOFormat(DraftQuestion question)
//        {
//            QuestionResponseDTO qRDTO = new QuestionResponseDTO
//            {
//                QuestionId = question.Id,
//                // = q.SchoolId,
//                //SchoolName = question.School.Name,
//                //DepartmentId = q.DepartmentId,
//                //DepartmentName = null,
//                //CourseId = q.CourseId,
//                //CourseName = null,
//                AccountId = question.AccountId,
//                AccountName = question.Account.UserName,
//                Text = question.Text,
//                MinimumExpForAnswer = question.MinimumExpForAnswer,
//                //IsDraft = q.IsDraft,
//                //IsClosed = question.IsClosed,
//                //SpamCount = question.SpamCount,
//                //AnswerCount = question.Answers.Count
//            };
//            return qRDTO;
//        }

//    }
//}