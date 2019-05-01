using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DataAccessLayer;
using DataAccessLayer.Models.DiscussionForum;
using DataAccessLayer.Models.School;

namespace ServiceLayer.DiscussionForum
{
    public class DiscussionForumServices
    {
        private DatabaseContext _db;

        public DiscussionForumServices(DatabaseContext _db)
        {
            this._db = _db;
        }

        public Question PostQuestion(Question question)
        {
            _db.Entry(question).State = EntityState.Added;
            return question;
        }

        public Answer PostAnswer(Question question, Answer answer)
        {
            question.Answers.Add(answer);
            UpdateQuestion(question);
                
            return answer;
        }

        public Question GetQuestion(int id)
        {
            return _db.Questions.Find(id);
        }

        //delete later. should be by school 
        public List<Question> GetQuestions()
        {
            try
            {
                return _db.Questions
                    .Where(q => q.IsDraft == false)
                    .OrderBy(q => q.CreatedDate)
                    .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        // To-do update to find by school, department, course
        //public List<Question> GetQuestions(School school)
        //{
        //    try
        //    {
        //return _db.Questions
        //    .Where(q => q.School == school.Name)
        //    .OrderBy(q => q.CreatedDate)
        //    .ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public List<Question> GetQuestions(School school, Department department)
        //{
        //    try
        //    {
        //        return _db.Questions
        //            .Where(q => q.School == school.Name && q.Department == department.Name)
        //            .OrderBy(q => q.CreatedDate)
        //            .ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public List<Question> GetQuestions(School school, Department department, Course course)
        //{
        //    try
        //    {
        //        return _db.Questions
        //            .Where(q => q.School == school.Name && q.Department == department.Name && q.Class == course.Name)
        //            .OrderBy(q => q.CreatedDate)
        //            .ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        public List<Question> GetQuestionDrafts(int accountId)
        {
            try
            {
                return _db.Questions
                    .Where(q => q.IsDraft == true && q.AccountId == accountId)
                    .OrderBy(q => q.CreatedDate)
                    .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Answer GetAnswer(int answerId)
        {
            return _db.Answers.Find(answerId);
        }

        public List<Answer> GetAnswers(int id)
        {
            Question question = GetQuestion(id);
            try
            {
                return _db.Answers
                    .Where(a => a.Question == question)
                    .OrderBy(a => a.HelpfulCount)
                    .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Question UpdateQuestion(Question question)
        {
            _db.Entry(question).State = EntityState.Modified;
            return question; 
        }

        public Answer UpdateAnswer(Answer answer)
        {
            _db.Entry(answer).State = EntityState.Modified;
            return answer;
        }

        public Question DeleteQuestion(int id)
        {
            var question = GetQuestion(id);
            if (question == null)
            {
                return null;
            }
            _db.Entry(question).State = EntityState.Deleted;
            return question;
        }

        public Answer DeleteAnswer(int id)
        {
            var answer = GetAnswer(id);
            if (answer == null)
            {
                return null;
            }
            _db.Entry(answer).State = EntityState.Deleted;
            return answer;
        }

        public Question CloseQuestion(int id)
        {
            var question = GetQuestion(id);
            if (question.IsClosed == false)
            {
                question.IsClosed = true;
                question = UpdateQuestion(question);
                return question;
            }
            else
                return question;
        }

        public Question IncreaseQuestionSpamCount(int id)
        {
            var question = GetQuestion(id);
            question.SpamCount++;
            question = UpdateQuestion(question);
            return question;
        }

        public Answer IncreaseAnswerSpamCount(int id)
        {
            var answer = GetAnswer(id);
            answer.SpamCount++;
            answer = UpdateAnswer(answer);
            return answer;
        }

        public Answer IncreaseHelpfulCount(int id)
        {
            var answer = GetAnswer(id);
            answer.HelpfulCount++;
            answer = UpdateAnswer(answer);
            return answer;
        }

        public Answer IncreaseUnHelpfulCount(int id)
        {
            var answer = GetAnswer(id);
            answer.UnHelpfulCount++;
            answer = UpdateAnswer(answer);
            return answer;
        }

        public Answer MarkAnswerAsCorrect(int id)
        {
            var answer = GetAnswer(id);
            answer.IsCorrectAnswer = true;
            answer = UpdateAnswer(answer);
            return answer;
        }
    }
}
