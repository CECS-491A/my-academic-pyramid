using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Models.DiscussionForum;
using ServiceLayer.DiscussionForum;
using ServiceLayer.UserManagement.UserAccountServices;
using DataAccessLayer.DTOs;
using System.Net.Http;
using System.Net;
using ServiceLayer;

namespace ManagerLayer.DiscussionManager
{
    public class DiscussionForumManager
    {
        DiscussionForumServices _discussionservices;
        UserManagementServices _usermanagementservices;
        //EmailService _emailservice; = new EmailService();
        private DatabaseContext _db;


        public DiscussionForumManager(DatabaseContext _db)
        {
            this._db = _db;
            this._discussionservices = new DiscussionForumServices(_db);
            this._usermanagementservices = new UserManagementServices(_db);
            //this._emailservice = new EmailService();

        }

        // Business Rules constants
        private const int _questionCharMin = 500;
        private const int _questionCharMax = 2000;
        private const int _expGainCorrectAns = 10;
        private const int _expGainHelpfullAns = 2;
        private const int _spamLimit = 3;
        

        public Question PostQuestion(QuestionDTO q)
        {
            // Validations 
            if (!ValidateQuestionCharLength(q))
            {
                throw new InvalidQuestionLengthException("Question length is incorrect");
            }

            // Create Question after passed in Question is validated
            Question question = new Question
            {
                //PoserId = q.AccountId,
                //PosterUserName = q.PosterUserName,
                Text = q.Text,
                MinimumExpForAnswer = q.MinimumExpForAnswer,
                IsDraft = q.IsDraft,
            };
            // Post Question
            return _discussionservices.PostQuestion(question);
        }

        public Answer PostAnswer(AnswerDTO a)
        {
            // Validations
            Account answerer = _usermanagementservices.FindById(a.PosterId);
            Question question = _discussionservices.GetQuestion(a.QuestionID);

            if (question.IsClosed)
            {
                throw new QuestionIsClosedException("Question is closed");
            }

            if (answerer.Exp < question.MinimumExpForAnswer)
            {
                throw new NotEnoughExpException("User does not have enough Exp to answer");
            }
            
            // Creat Answer after passed in Answer is validated
            Answer answer = new Answer
            {
                PosterId = a.PosterId,
                PosterUserName = a.PosterUserName,
                Question = question,
                Text = a.Text,
            };
            // Post Answer
            return _discussionservices.PostAnswer(question, answer);
        }



        // update spam count
        // email sys admin if a question or answer reaches spam limit 
        public Question IncreaseQuestionSpamCount(int id)
        {
            Question question = _discussionservices.GetQuestion(id);
            question = _discussionservices.IncreaseQuestionSpamCount(id);
            if (question.SpamCount == _spamLimit)
            {
 //               // call service to email admin because question reached spam limit
            }
            return question;
        }

        // update spam count
        // email sys admin if a question or answer reaches spam limit 
        public Answer IncreaseAnswerSpamCount(int id)
        {
            Answer answer = _discussionservices.GetAnswer(id);
            answer = _discussionservices.IncreaseAnswerSpamCount(id);
            if (answer.SpamCount == _spamLimit)
            {
//               // call service to email admin because question reached spam limit
            }
            return answer;
        }

        // update question content... answers can never be updated 
        public Question EditQuestion(QuestionDTO q, int userId)
        {
            Question question = _discussionservices.GetQuestion(q.Id);

            // Validations 
            if (question.IsClosed) 
            {
                throw new QuestionIsClosedException("Question is closed");
            }
            if (!ValidateQuestionCharLength(q))
            {
                throw new InvalidQuestionLengthException("Question length is incorrect");
            }
            if (userId != question.PosterId)
            {
                throw new InvalidUserException("User cannot edit this question");
            }

            // Update Question after passed in Question is validated
            question.Text = q.Text;
            question.MinimumExpForAnswer = q.MinimumExpForAnswer;
            question.IsDraft = q.IsDraft;
            //question.IsClosed = q.IsClosed;
            return _discussionservices.UpdateQuestion(question);
        }

        // update answer with increased helpful count and update user Exp
        public Answer IncreaseHelpfulCount(int id)
        {
            Answer answer = _discussionservices.GetAnswer(id);
            answer = _discussionservices.IncreaseHelpfulCount(id);
            // update user exp
            Account user = _usermanagementservices.FindById(answer.PosterId);
            user.Exp += _expGainHelpfullAns;
            user = _usermanagementservices.UpdateUser(user);
            _db.SaveChanges();
            return answer;
        }

        // update answer with increased unhelpful count 
        // don't think UnHulpful affects a user's Exp? 
        public Answer IncreaseUnHelpfulCount(int id)
        {
            Answer answer = _discussionservices.GetAnswer(id);
            answer = _discussionservices.IncreaseUnHelpfulCount(id);
            // update user exp
            //User user = _usermanagementservices.FindById(answer.PosterId);
            //user.Exp -= 2;
            //user = _usermanagementservices.UpdateUser(user);
            //_db.SaveChanges();
            return answer;
        }

        public Answer MarkAsCorrectAnswer(int id, int userId)
        {
            Answer answer = _discussionservices.GetAnswer(id);
            Question question = answer.Question;
            Account user = _usermanagementservices.FindById(answer.PosterId);
            // Validations 
            if (question.IsClosed)
            {
                throw new QuestionIsClosedException("Question is closed");
            }
            if (userId != question.PosterId)
            {
                throw new InvalidUserException("User cannot edit this question");
            }

            answer = _discussionservices.MarkAnswerAsCorrect(id);
            question = _discussionservices.CloseQuestion(question.Id);
            user.Exp += _expGainCorrectAns;
            user = _usermanagementservices.UpdateUser(user);
            _db.SaveChanges();
            return answer;

        }

        public bool ValidateQuestionCharLength(QuestionDTO q)
        {
            if (q.Text != null || (q.Text.Length > _questionCharMin && q.Text.Length < _questionCharMax))
                return true;
            else
                return false;
        }
    }
}