﻿using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models.DiscussionForum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ServiceLayer.DiscussionForum
{
    public class AnswerServices : IAnswerServices
    {
        private DatabaseContext _db;

        public AnswerServices(DatabaseContext _db)
        {
            this._db = _db;
        }

        public Answer PostAnswer(Answer answer)
        {
            _db.Entry(answer).State = EntityState.Added;
            return answer;
        }

        public Answer GetAnswer(int answerId)
        {
            return _db.Answers.Find(answerId);
        }

        public List<Answer> GetAnswers(int questionId)
        {
            try
            {
                return _db.Answers
                    .Where(a => a.QuestionId == questionId)
                    .OrderBy(a => a.HelpfulCount)
                    .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Answer UpdateAnswer(Answer answer)
        {
            _db.Entry(answer).State = EntityState.Modified;
            return answer;
        }

        public Answer DeleteAnswer(int answerId)
        {
            var answer = GetAnswer(answerId);
            if (answer == null)
            {
                return null;
            }
            _db.Entry(answer).State = EntityState.Deleted;
            return answer;
        }

        public Answer IncreaseAnswerSpamCount(int answerId)
        {
            var answer = GetAnswer(answerId);
            answer.SpamCount++;
            answer = UpdateAnswer(answer);
            return answer;
        }

        public Answer IncreaseHelpfulCount(int answerId)
        {
            var answer = GetAnswer(answerId);
            answer.HelpfulCount++;
            answer = UpdateAnswer(answer);
            return answer;
        }

        public Answer IncreaseUnHelpfulCount(int answerId)
        {
            var answer = GetAnswer(answerId);
            answer.UnHelpfulCount++;
            answer = UpdateAnswer(answer);
            return answer;
        }

        public Answer MarkAnswerAsCorrect(int answerId)
        {
            var answer = GetAnswer(answerId);
            answer.IsCorrectAnswer = true;
            answer = UpdateAnswer(answer);
            return answer;
        }

        public AnswerResponseDTO ApplyAnswerFortmat(Answer answer)
        {
            AnswerResponseDTO aRDTO = new AnswerResponseDTO
            {
                AnswerId = answer.Id,
                QuestionId = answer.QuestionId,
                AccountId = answer.AccountId,
                AccountName = answer.Account.UserName,
                Text = answer.Text,
                HelpfulCount = answer.HelpfulCount,
                UnHelpfulCount = answer.UnHelpfulCount,
                SpamCount = answer.UnHelpfulCount,
                IsCorrectAnswer = answer.IsCorrectAnswer,
                CreatedDate = answer.CreatedDate
            };
            return aRDTO;
        }

        public Question GetPostedQuestion(int questionId)
        {
            return _db.Questions.Find(questionId);
        }

        public Question UpdateAnyPostedQuestion(Question question)
        {
            _db.Entry(question).State = EntityState.Modified;
            return question;
            
        }
    }
}