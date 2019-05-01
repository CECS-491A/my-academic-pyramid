using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using SecurityLayer.Authorization;
using SecurityLayer.Sessions;
using SecurityLayer.Authorization.AuthorizationManagers;
using System.Web.Http;
using KFC.SIT.WebAPI.Utility;
using System.Web.Http.Cors;
using WebAPI.UserManagement;
using DataAccessLayer.Models.DiscussionForum;
using DataAccessLayer.Models;
using ManagerLayer.DiscussionManager;
using DataAccessLayer.DTOs;
using DataAccessLayer;
using ServiceLayer.DiscussionForum;

namespace KFC.SIT.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DiscussionForumController : ApiController
    {
        private bool securityPass = true;
        private int authUserId;


        // everyone can see questions 
        // only students can post questions? 

        // delete later. Don't need
        [HttpGet]
        [ActionName("GetAllQuestions")]
        public IHttpActionResult GetQuestions()
        {
            using (var _db = new DatabaseContext())
            {
                DiscussionForumServices _discussionServices = new DiscussionForumServices(_db);
                var questions = _discussionServices.GetQuestions();
                return Content(HttpStatusCode.OK, questions);
            }
        }

        //[HttpGet]
        //[ActionName("GetQuestionsBySchool")]
        //public IHttpActionResult GetQuestions()
        //{

        //}

        //[HttpGet]
        //[ActionName("GetQuestionsByDepartment")]
        //public IHttpActionResult GetQuestions()
        //{

        //}

        // where to get userId
        [HttpGet]
        [ActionName("GetQuestionDrafts")]
        public IHttpActionResult GetQuestionDrafts(int userId)
        {
            using (var _db = new DatabaseContext())
            {
                DiscussionForumServices _dicussionServices = new DiscussionForumServices(_db);
                var drafts = _dicussionServices.GetQuestionDrafts(userId);
                return Content(HttpStatusCode.OK, drafts);
            }
        }

        [HttpGet]
        [ActionName("GetAnswers")]
        public IHttpActionResult GetAnswers(int questionId)
        {
            using (var _db = new DatabaseContext())
            {
                DiscussionForumServices _discussionServices = new DiscussionForumServices(_db);
                var answers = _discussionServices.GetAnswers(questionId);
                return Content(HttpStatusCode.OK, answers);
            }
        }

        [HttpPost]
        [ActionName("PostQuestions")]
        public IHttpActionResult PostQuestion([FromBody] QuestionDTO questionDTO)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                 Request.Headers
             );
            if (securityContext == null)
            {
                securityPass = false;
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                securityPass = false;
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                //"CanPostQuestion"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                securityPass = false;
            }
            else
            {
                UserManager um = new UserManager();
                Account user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                if (user == null)
                {
                    securityPass = false;
                }
            }

            using (var _db = new DatabaseContext())
            {
                Question question;
                try
                {
                    questionDTO.UserId = authUserId;
                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
                    question = discussionManager.PostQuestion(questionDTO);
                }
                catch
                {

                }
                return Content(HttpStatusCode.OK, "Question was posted succesfully.");
            }
        }

        [HttpPost]
        [ActionName("PostAnswer")]
        public IHttpActionResult PostAnswer([FromBody] AnswerDTO answerDTO)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                 Request.Headers
             );
            if (securityContext == null)
            {
                securityPass = false;
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                securityPass = false;
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                //"CanPostAnswer"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                securityPass = false;
            }
            else
            {
                UserManager um = new UserManager();
                Account user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                if (user == null)
                {
                    securityPass = false;
                }
            }

            using (var _db = new DatabaseContext())
            {
                Answer answer;
                try
                {
                    answerDTO.UserId = authUserId;
                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
                    answer = discussionManager.PostAnswer(answerDTO);
                }
                catch
                {

                }
                return Content(HttpStatusCode.OK, "Answer was posted succesfully.");
            }
        }

        [HttpPost]
        [ActionName("UpdateQuestions")]
        public IHttpActionResult UpdateQuestion([FromBody] QuestionDTO questionDTO)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                 Request.Headers
             );
            if (securityContext == null)
            {
                securityPass = false;
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                securityPass = false;
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                //"CanPostQuestion"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                securityPass = false;
            }
            else
            {
                UserManager um = new UserManager();
                Account user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                if (user == null)
                {
                    securityPass = false;
                }
            }

            using (var _db = new DatabaseContext())
            {
                Question question;
                try
                {
                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
                    question = discussionManager.EditQuestion(questionDTO, authUserId);
                }
                catch
                {

                }
                return Content(HttpStatusCode.OK, "Question was posted succesfully.");
            }
        }

        [HttpPost]
        [ActionName("IncreaseQuestionSpamCount")]
        public IHttpActionResult IncreaseQuestionSpamCount(int questionId)
        {
            using (var _db = new DatabaseContext())
            {
                Question question;
                try
                {
                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
                    question = discussionManager.IncreaseQuestionSpamCount(questionId);
                    return Content(HttpStatusCode.OK, question);
                }
                catch
                {

                }
                return Content(HttpStatusCode.ExpectationFailed, "Error");
            }
        }

        [HttpPost]
        [ActionName("IncreaseAnswerSpamCount")]
        public IHttpActionResult IncreaseAnswerSpamCount(int answerId)
        {
            using (var _db = new DatabaseContext())
            {
                Answer answer;
                try
                {
                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
                    answer = discussionManager.IncreaseAnswerSpamCount(answerId);
                    return Content(HttpStatusCode.OK, answer);
                }
                catch
                {

                }
                return Content(HttpStatusCode.ExpectationFailed, "Error");

            }
        }

        [HttpPost]
        [ActionName("IncreaseAnswerHelpfulCount")]
        public IHttpActionResult IncreaseAnswerHelpfulCount(int answerId)
        {
            using (var _db = new DatabaseContext())
            {
                Answer answer;
                try
                {
                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
                    answer = discussionManager.IncreaseHelpfulCount(answerId);
                    return Content(HttpStatusCode.OK, answer);
                }
                catch
                {

                }
                return Content(HttpStatusCode.ExpectationFailed, "Error");

            }
        }

        [HttpPost]
        [ActionName("IncreaseAnswerUnHelpfulCount")]
        public IHttpActionResult IncreaseAnswerUnHelpfulCount(int answerId)
        {
            using (var _db = new DatabaseContext())
            {
                Answer answer;
                try
                {
                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
                    answer = discussionManager.IncreaseUnHelpfulCount(answerId);
                    return Content(HttpStatusCode.OK, answer);
                }
                catch
                {

                }
                return Content(HttpStatusCode.ExpectationFailed, "Error");

            }
        }

        [HttpPost]
        [ActionName("CloseQuestion")]
        public IHttpActionResult CloseQuestion(int questionId)
        {
            using (var _db = new DatabaseContext())
            {
                Question question;
                try
                {
                    DiscussionForumServices discussionServices = new DiscussionForumServices(_db);
                    question = discussionServices.CloseQuestion(questionId);
                    return Content(HttpStatusCode.OK, question);
                }
                catch
                {

                }
                return Content(HttpStatusCode.ExpectationFailed, "Error");
            }
        }

        [HttpPost]
        [ActionName("MarkAsCorrectAnswer")]
        public IHttpActionResult MarkAsCorrectAnswer(int answerId)
        {
            //Todo get userId

            using (var _db = new DatabaseContext())
            {
                Answer answer;
                try
                {
                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
                    //answer = discussionManager.MarkAsCorrectAnswer(answerId);
                    //return Content(HttpStatusCode.OK, answer);
                }
                catch
                {

                }
                return Content(HttpStatusCode.ExpectationFailed, "Error");

            }
        }

    }
}
