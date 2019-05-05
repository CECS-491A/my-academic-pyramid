//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Data.Entity;
//using DataAccessLayer;
//using DataAccessLayer.DTOs;
//using DataAccessLayer.Models.DiscussionForum;
//using DataAccessLayer.Models.School;

//namespace ServiceLayer.DiscussionForum
//{
//    public class DiscussionForumServices
//    {
//        private DatabaseContext _db;

//        public DiscussionForumServices(DatabaseContext _db)
//        {
//            this._db = _db;
//        }

//        public Question PostQuestion(Question question)
//        {
//            _db.Entry(question).State = EntityState.Added;
//            return question;
//        }

//        public QuestionDraft PostQuestionDraft(QuestionDraft qDraft)
//        {
//            _db.Entry(qDraft).State = EntityState.Added;
//            return qDraft;
//        }

//        public Answer PostAnswer(Answer answer)
//        {
//            _db.Entry(answer).State = EntityState.Added;
//            return answer;
//        }

//        public Question GetQuestion(int questionId)
//        {
//            return _db.Questions.Find(questionId);
//        }

//        public QuestionDraft GetQuestionDraft(int qDraftId)
//        {
//            return _db.QuestionDrafts.Find(qDraftId);
//        }

//        public Answer GetAnswer(int answerId)
//        {
//            return _db.Answers.Find(answerId);
//        }

//        //delete later. should be by school 
//        public List<Question> GetQuestions()
//        {
//            try
//            {
//                return _db.Questions
//                    //.Where(q => q.IsDraft == false)
//                    .OrderBy(q => q.CreatedDate)
//                    .ToList();
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }

//        // To-do update to find by school, department, course
//        public List<SchoolQuestion> GetQuestions(int schoolId)
//        {
//            try
//            {
//                return _db.SchoolQuestions
//                    .Where(q => q.SchoolId == schoolId)
//                    .OrderBy(q => q.CreatedDate)
//                    .ToList();
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }

//        public List<DepartmentQuestion> GetQuestions(int schoolId, int departmentId)
//        {
//            try
//            {
//                return _db.DepartmentQuestions
//                    .Where(q => q.SchoolId == schoolId && q.SchoolDepartmentId == departmentId)
//                    .OrderBy(q => q.CreatedDate)
//                    .ToList();
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }

//        public List<CourseQuestion> GetQuestions(int schoolId, int departmentId, int courseId)
//        {
//            try
//            {
//                return _db.CourseQuestions
//                    .Where(q => q.SchoolId == schoolId && q.SchoolDepartmentId == departmentId && q.CourseId == courseId)
//                    .OrderBy(q => q.CreatedDate)
//                    .ToList();
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }

//        public List<QuestionDraft> GetQuestionDrafts(int accoundId)
//        {
//            try
//            {
//                return _db.QuestionDrafts
//                    .Where(q => q.AccountId == accoundId)
//                    .OrderBy(q => q.CreatedDate)
//                    .ToList();
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }

//        //public List<Question> GetQuestionDrafts(int accountId)
//        //{
//        //    try
//        //    {
//        //        return _db.Questions
//        //            .Where(q => q.IsDraft == true && q.AccountId == accountId)
//        //            .OrderBy(q => q.CreatedDate)
//        //            .ToList();
//        //    }
//        //    catch (Exception)
//        //    {
//        //        return null;
//        //    }
//        //}

//        public List<Answer> GetAnswers(int questionId)
//        {
//            var question = GetQuestion(questionId);
//            try
//            {
//                return _db.Answers
//                    .Where(a => a.QuestionId == questionId)
//                    .OrderBy(a => a.HelpfulCount)
//                    .ToList();
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }

//        public Question UpdateQuestion(Question question)
//        {
//            _db.Entry(question).State = EntityState.Modified;
//            return question; 
//        }

//        public QuestionDraft UpdateQuestionDraft(QuestionDraft qDraft)
//        {
//            _db.Entry(qDraft).State = EntityState.Modified;
//            return qDraft;
//        }

//        public Answer UpdateAnswer(Answer answer)
//        {
//            _db.Entry(answer).State = EntityState.Modified;
//            return answer;
//        }

//        public Question DeleteQuestion(int questionId)
//        {
//            var question = GetQuestion(questionId);
//            if (question == null)
//            {
//                return null;
//            }
//            _db.Entry(question).State = EntityState.Deleted;
//            return question;
//        }

//        public QuestionDraft DeleteQuestionDraft(int qDraftId)
//        {
//            var questionDraft = GetQuestionDraft(qDraftId);
//            if (questionDraft == null)
//            {
//                return null;
//            }
//            _db.Entry(questionDraft).State = EntityState.Deleted;
//            return questionDraft;
//        }

//        public Answer DeleteAnswer(int answerId)
//        {
//            var answer = GetAnswer(answerId);
//            if (answer == null)
//            {
//                return null;
//            }
//            _db.Entry(answer).State = EntityState.Deleted;
//            return answer;
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

//        public Question IncreaseQuestionSpamCount(int questionId)
//        {
//            var question = GetQuestion(questionId);
//            question.SpamCount++;
//            question = UpdateQuestion(question);
//            return question;
//        }

//        public Answer IncreaseAnswerSpamCount(int answerId)
//        {
//            var answer = GetAnswer(answerId);
//            answer.SpamCount++;
//            answer = UpdateAnswer(answer);
//            return answer;
//        }

//        public Answer IncreaseHelpfulCount(int answerId)
//        {
//            var answer = GetAnswer(answerId);
//            answer.HelpfulCount++;
//            answer = UpdateAnswer(answer);
//            return answer;
//        }

//        public Answer IncreaseUnHelpfulCount(int answerId)
//        {
//            var answer = GetAnswer(answerId);
//            answer.UnHelpfulCount++;
//            answer = UpdateAnswer(answer);
//            return answer;
//        }

//        public Answer MarkAnswerAsCorrect(int answerId)
//        {
//            var answer = GetAnswer(answerId);
//            answer.IsCorrectAnswer = true;
//            answer = UpdateAnswer(answer);
//            return answer;
//        }

//        public QuestionResponseDTO ApplyQuestionFormat(int questionId)
//        {

//            QuestionResponseDTO qRDTO = new QuestionResponseDTO
//            {
//                QuestionId = question.Id,
//                // = q.SchoolId,
//                SchoolName = question.School.Name,
//                //DepartmentId = q.DepartmentId,
//                DepartmentName = question.Department.Department.Name,
//                //CourseId = q.CourseId,
//                CourseName = question.Course.Name,
//                AccountId = question.AccountId,
//                AccountName = question.Account.UserName,
//                Text = question.Text,
//                MinimumExpForAnswer = question.MinimumExpForAnswer,
//                //IsDraft = q.IsDraft,
//                IsClosed = question.IsClosed,
//                SpamCount = question.SpamCount,
//                AnswerCount = question.Answers.Count
//            };
//            return qRDTO;
//        }

//        public QuestionDraftResponseDTO ApplyQuestionDraftFormat(Question question)
//        {
//            QuestionDraftResponseDTO qDRDTO = new QuestionDraftResponseDTO
//            {
//                QuestionDraftId = question.Id,
//                SchoolId = question.SchoolId,
//                SchoolName = question.School.Name,
//                DepartmentId = question.DepartmentId,
//                DepartmentName = question.Department.Department.Name,
//                CourseId = question.CourseId,
//                CourseName = question.Course.Name,
//                AccountId = question.AccountId,
//                Text = question.Text,
//                MinimumExpForAnswer = question.MinimumExpForAnswer//,
//            };
//            return qDRDTO;
//        }

//        public AnswerResponseDTO ApplyAnswerFortmat(Answer answer)
//        {
//            AnswerResponseDTO aRDTO = new AnswerResponseDTO
//            {
//                AnswerId = answer.Id, 
//                QuestionId = answer.QuestionId,
//                AccountId = answer.AccountId,
//                AccountName = answer.Account.UserName, 
//                Text = answer.Text,
//                HelpfulCount = answer.HelpfulCount, 
//                UnHelpfulCount = answer.UnHelpfulCount, 
//                SpamCount = answer.UnHelpfulCount,
//                IsCorrectAnswer = answer.IsCorrectAnswer, 
//                CreatedDate = answer.CreatedDate 
//            };
//            return aRDTO;
//        }
//    }
//}
