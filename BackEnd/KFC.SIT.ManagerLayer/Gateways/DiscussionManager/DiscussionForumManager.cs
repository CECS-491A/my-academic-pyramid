using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Models.DiscussionForum;
using ServiceLayer.DiscussionForum;
using WebAPI.UserManagement;
using DataAccessLayer.DTOs;

namespace ManagerLayer.DiscussionManager
{
    public class DiscussionForumManager
    {
        public DiscussionForumManager()
        {

        }

        private const int questionCharMin = 500;
        private const int questionCharMax = 2000;
        private const int expGainCorrectAns = 10;
        private const int expGainHelpfullAns = 2;
        private const int spamLimit = 3;
        private int minimumExpToAnswer = 0;
        DiscussionForumServices discussionservice = new DiscussionForumServices();
        //EmailService emailservice = new EmailService();
        UserManager usermanager = new UserManager();

        //TokenPayLoad
        //GetPayLoad(Token)
        //{
        //}

        //create question in front end and pass that in then 
        public Question CreateQuestion(QuestionDTO q)
        {
            Question question = new Question
            {
                AccountId = q.AccountId,
                Content = q.Content,
                MinimumExpForAnswer = q.MinimumExpForAnswer,
                Closed = false,
                Draft = q.Draft,
                Spam = 0,
                CreatedDate = DateTime.Now,
            };
            if (ValidateQuestionCharLength(question) == true)
            {
                using (var _db = new DatabaseContext())
                {
                    question = discussionservice.PostQuestion(_db, question);
                    return question;
                }
            }
            else
                return null;
        }

        // Todo 
        // check that question is not closed
        // update answer to not editable if answer is posted successfully 
        public Answer CreateAnswer(AnswerDTO a)
        {
            Answer answer = new Answer
            {
                AccountId = a.AccountId,
                Question = a.Question,
                Content = a.Content,
                Helpful = 0,
                UnHelpful = 0,
                CorrectAnswer = false,
                Spam = 0,
                CreatedDate = DateTime.Now,
            };
            if (ValidateUserHasEnoughExpToAnswer(answer) == true)
            {
                using (var _db = new DatabaseContext())
                {
                    answer = discussionservice.PostAnswer(_db, answer.Question.Id, answer);
                    return answer;
                }
            }
            else
                return null;
        }

        // mark question as closed give points to user with correct answer if there is one
        //public Question CloseQuestion(Question q)
        //{

        //}

        // update spam count
        // email sys admin if a question or answer reaches spam limit 
        //public Question IncreaseSpamCount(Question q)
        //{

        //}

        // update spam count
        // email sys admin if a question or answer reaches spam limit 
        //public Answer IncreaseSpamCount(Answer a)
        //{

        //}

        // update question content... answers can never be updated
        // check that question is open 
        //public Question EditQuestion(Question q)
        //{

        //}

        // update answer with increased helpful count and update user Exp
        //public Answer IncreaseHelpfulCount(Answer a)
        //{

        //}

        // update answer with increased unhelpful count and update user Exp
        //public Answer IncreaseUnHelpfulCount(Answer a)
        //{

        //}

        public bool ValidateQuestionCharLength(Question q)
        {
            if (q.Content.Length > 500 && q.Content.Length < 2000)
                return true;
            else
                return false;
        }

        public bool ValidateUserHasEnoughExpToAnswer(Answer a)
        {
            User answerer = usermanager.FindUserById(a.AccountId);
            if (answerer.Exp >= a.Question.MinimumExpForAnswer)
                return true;
            else
                return false;
        }









        //Error handling and call services (check that answer isnt already there, and if not then post it)
        //Then in controller, create a manager that calls the services 

        //Check for concurrency problem when updating both answer and user (mark question as correct answer and update user Exp)
        //If one fails, both need to rollback 



    }
}