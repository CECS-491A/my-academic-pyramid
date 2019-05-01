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
using ServiceLayer;
using ServiceLayer.DiscussionForum;
using System.Data.Entity.Infrastructure;

namespace KFC.SIT.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DiscussionForumController : ApiController
    {
        private bool securityPass = false;
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
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanPostQuestion"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                if (user == null)
                {
                    return Unauthorized();
                }
                authUserId = um.FindByUserName(securityContext.UserName).Id;
            }



            using (var _db = new DatabaseContext())
            {
                Question question;
                try
                {
                    questionDTO.UserId = authUserId;
                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
                    question = discussionManager.PostQuestion(questionDTO);
                    return Content(HttpStatusCode.OK, "Question was posted succesfully.");
                }
                catch (InvalidQuestionLengthException ex)
                {
                    return Content(HttpStatusCode.BadRequest, ex.Message);
                }
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
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanPostAnswer"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                if (user == null)
                {
                    return Unauthorized();
                }
                authUserId = um.FindByUserName(securityContext.UserName).Id;
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
                catch (QuestionIsClosedException ex)
                {
                    return Content(HttpStatusCode.Forbidden, ex.Message);
                }
                catch (NotEnoughExpException ex)
                {
                    return Content(HttpStatusCode.Conflict, ex.Message);
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
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanPostQuestion"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                if (user == null)
                {
                    return Unauthorized();
                }
                authUserId = um.FindByUserName(securityContext.UserName).Id;
            }



            using (var _db = new DatabaseContext())
            {
                Question question;
                try
                {
                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
                    question = discussionManager.EditQuestion(questionDTO, authUserId);
                    return Content(HttpStatusCode.OK, "Question was posted succesfully.");
                }
                catch (QuestionIsClosedException ex)
                {
                    return Content(HttpStatusCode.Forbidden, ex.Message);
                }
                catch (InvalidQuestionLengthException ex)
                {
                    return Content(HttpStatusCode.BadRequest, ex.Message);
                }
                catch (InvalidUserException ex)
                {
                    return Content(HttpStatusCode.Unauthorized, ex.Message);
                }
                catch (QuestionUnavailableException ex)
                {
                    return Content(HttpStatusCode.Forbidden, ex.Message);
                }
            }
        }

        [HttpPost]
        [ActionName("IncreaseQuestionSpamCount")]
        public IHttpActionResult IncreaseQuestionSpamCount(int questionId)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                 Request.Headers
             );
            if (securityContext == null)
            {
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanMarkQuestionAsSpam"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                if (user == null)
                {
                    return Unauthorized();
                }
                authUserId = um.FindByUserName(securityContext.UserName).Id;
            }

            using (var _db = new DatabaseContext())
            {
                Question question;
                try
                {
                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
                    question = discussionManager.IncreaseQuestionSpamCount(questionId, authUserId);
                    return Content(HttpStatusCode.OK, question);
                }
                catch (ArgumentNullException)
                {
                    return Content(HttpStatusCode.BadRequest, "Answer does not exist");
                }
                catch (InvalidUserException ex)
                {
                    return Content(HttpStatusCode.Unauthorized, ex.Message);
                }
            }
        }

        [HttpPost]
        [ActionName("IncreaseAnswerSpamCount")]
        public IHttpActionResult IncreaseAnswerSpamCount(int answerId)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                 Request.Headers
             );
            if (securityContext == null)
            {
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanMarkAnswerAsSpam"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                if (user == null)
                {
                    return Unauthorized();
                }
                authUserId = um.FindByUserName(securityContext.UserName).Id;
            }

            using (var _db = new DatabaseContext())
            {
                Answer answer;
                try
                {
                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
                    answer = discussionManager.IncreaseAnswerSpamCount(answerId, authUserId);
                    return Content(HttpStatusCode.OK, answer);
                }
                catch (ArgumentNullException)
                {
                    return Content(HttpStatusCode.BadRequest, "Answer does not exist");
                }
                catch (InvalidUserException ex)
                {
                    return Content(HttpStatusCode.Unauthorized, ex.Message);
                }
            }
        }

        [HttpPost]
        [ActionName("IncreaseAnswerHelpfulCount")]
        public IHttpActionResult IncreaseAnswerHelpfulCount(int answerId)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                 Request.Headers
             );
            if (securityContext == null)
            {
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanMarkAnswerAsHelpful"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                if (user == null)
                {
                    return Unauthorized();
                }
                authUserId = um.FindByUserName(securityContext.UserName).Id;
            }

            using (var _db = new DatabaseContext())
            {
                Answer answer;
                try
                {
                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
                    answer = discussionManager.IncreaseHelpfulCount(answerId, authUserId);
                    return Content(HttpStatusCode.OK, answer);
                }
                catch (ArgumentNullException)
                {
                    return Content(HttpStatusCode.BadRequest, "Answer does not exist");
                }
                catch (InvalidUserException ex)
                {
                    return Content(HttpStatusCode.Unauthorized, ex.Message);
                }
                catch (DbUpdateConcurrencyException)
                {

                    return Content(HttpStatusCode.InternalServerError, "There was an error on the server");
                }
            }
        }

        [HttpPost]
        [ActionName("IncreaseAnswerUnHelpfulCount")]
        public IHttpActionResult IncreaseAnswerUnHelpfulCount(int answerId)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                 Request.Headers
             );
            if (securityContext == null)
            {
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanMarkAnswerAsUnHelpful"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                if (user == null)
                {
                    return Unauthorized();
                }
                authUserId = um.FindByUserName(securityContext.UserName).Id;
            }

            using (var _db = new DatabaseContext())
            {
                Answer answer;
                try
                {
                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
                    answer = discussionManager.IncreaseUnHelpfulCount(answerId, authUserId);
                    return Content(HttpStatusCode.OK, answer);
                }
                catch (ArgumentNullException)
                {
                    return Content(HttpStatusCode.BadRequest, "Answer does not exist");
                }
                catch (InvalidUserException ex)
                {
                    return Content(HttpStatusCode.Unauthorized, ex.Message);
                }
            }
        }

        [HttpPost]
        [ActionName("CloseQuestion")]
        public IHttpActionResult CloseQuestion(int questionId)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                Request.Headers
            );
            if (securityContext == null)
            {
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanCloseQuestion"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                if (user == null)
                {
                    return Unauthorized();
                }
                authUserId = um.FindByUserName(securityContext.UserName).Id;
            }


            using (var _db = new DatabaseContext())
            {
                Question question;
                try
                {
                    DiscussionForumManager discussionServices = new DiscussionForumManager(_db);
                    question = discussionServices.CloseQuestion(questionId, authUserId);
                    return Content(HttpStatusCode.OK, question);
                }
                catch (QuestionIsClosedException ex)
                {
                    return Content(HttpStatusCode.BadRequest, ex.Message);
                }
                catch (InvalidUserException ex)
                {
                    return Content(HttpStatusCode.Unauthorized, ex.Message);
                }

            }
        }

        [HttpPost]
        [ActionName("MarkAsCorrectAnswer")]
        public IHttpActionResult MarkAsCorrectAnswer(int answerId)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                Request.Headers
            );
            if (securityContext == null)
            {
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanMarkAnswerAsCorrect"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                if (user == null)
                {
                    return Unauthorized();
                }
                authUserId = um.FindByUserName(securityContext.UserName).Id;
            }

            using (var _db = new DatabaseContext())
            {
                Answer answer;
                try
                {
                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
                    answer = discussionManager.MarkAsCorrectAnswer(answerId, authUserId);
                    return Content(HttpStatusCode.OK, answer);
                }
                catch (ArgumentNullException)
                {
                    return Content(HttpStatusCode.BadRequest, "Answer does not exist");
                }
                catch (QuestionIsClosedException ex)
                {
                    return Content(HttpStatusCode.Forbidden, ex.Message);
                }
                catch (InvalidUserException ex)
                {
                    return Content(HttpStatusCode.Unauthorized, ex.Message);
                }
                catch (DbUpdateConcurrencyException)
                {

                    return Content(HttpStatusCode.InternalServerError, "There was an error on the server");
                }
            }
        }
    }
}
