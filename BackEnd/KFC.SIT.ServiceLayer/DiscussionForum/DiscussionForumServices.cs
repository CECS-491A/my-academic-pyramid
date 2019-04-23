using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DataAccessLayer;
using DataAccessLayer.Models.DiscussionForum;

namespace ServiceLayer.DiscussionForum
{
    public class DiscussionForumServices
    {
        public Question PostQuestion(DatabaseContext _db, Question question)
        {
            try
            {
                _db.Entry(question).State = EntityState.Added;
                return question;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Answer PostAnswer(DatabaseContext _db, int questionId, Answer answer)
        {
            try
            {
                var question = GetQuestion(_db, questionId);
                question.Answers.Add(answer);
                UpdateQuestion(_db, question);

                _db.Entry(answer).State = EntityState.Added;
                return answer;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Question GetQuestion(DatabaseContext _db, int id)
        {
            try
            {
                var response = _db.Questions.Find(id);
                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //delete later. should be by school 
        public List<Question> GetQuestions(DatabaseContext _db)
        {
            try
            {
                var questions = _db.Questions
                .OrderBy(q => q.CreatedDate)
                .ToList();
                return questions;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //To-do update with school, department, course, etc. being their own classes 
        //public List<Question> GetQuestions(DatabaseContext _db, string school)
        //{
        //    try
        //    {
        //        var questions = _db.Questions
        //        .Where(q => q.School == school)
        //        .OrderBy(q => q.CreatedDate)
        //        .ToList();
        //        return questions;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public List<Question> GetQuestions(DatabaseContext _db, string school, string department)
        //{
        //    try
        //    {
        //        var questions = _db.Questions
        //        .Where(q => q.School == school && q.Department == department)
        //        .OrderBy(q => q.CreatedDate)
        //        .ToList();
        //        return questions;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public List<Question> GetQuestions(DatabaseContext _db, string school, string department, string className)
        //{
        //    try
        //    {
        //        var questions = _db.Questions
        //        .Where(q => q.School == school && q.Department == department && q.Class == className)
        //        .OrderBy(q => q.CreatedDate)
        //        .ToList();
        //        return questions;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public List<Question> GetQuestions(DatabaseContext _db, string school, string department, string className, string section)
        //{
        //    try
        //    {
        //        var questions = _db.Questions
        //        .Where(q => q.School == school && q.Department == department && q.Class == className && q.Section == section)
        //        .OrderBy(q => q.CreatedDate)
        //        .ToList();
        //        return questions;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        public Answer GetAnswer(DatabaseContext _db, int id)
        {
            try
            {
                var response = _db.Answers.Find(id);
                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Answer> GetAnswers(DatabaseContext _db, Question question)
        {
            try
            {
                var answers = _db.Answers
                .Where(a => a.Question == question)
                .OrderBy(q => q.CreatedDate)
                .ToList();
                return answers;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Question UpdateQuestion(DatabaseContext _db, Question question)
        {
            try
            {
                var result = GetQuestion(_db, question.Id);
                if (result == null)
                {
                    return null;
                }
                _db.Entry(question).State = EntityState.Modified;
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Answer UpdateAnswer(DatabaseContext _db, Answer answer)
        {
            try
            {
                var result = GetAnswer(_db, answer.Id);
                if (result == null)
                {
                    return null;
                }
                _db.Entry(answer).State = EntityState.Modified;
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Question DeleteQuestion(DatabaseContext _db, int id)
        {
            try
            {
                var question = GetQuestion(_db, id);
                if (question == null)
                {
                    return null;
                }
                _db.Entry(question).State = EntityState.Deleted;
                return question;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Answer DeleteAnswer(DatabaseContext _db, int id)
        {
            try
            {
                var answer = GetAnswer(_db, id);
                if (answer == null)
                {
                    return null;
                }
                _db.Entry(answer).State = EntityState.Deleted;
                return answer;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
