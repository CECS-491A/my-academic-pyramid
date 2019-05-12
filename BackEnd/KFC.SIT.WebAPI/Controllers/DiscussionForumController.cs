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
using WebAPI.Gateways.UserManagement;
using DataAccessLayer.Models.DiscussionForum;
using DataAccessLayer.Models;
using ManagerLayer.DiscussionManager;
using DataAccessLayer.DTOs;
using DataAccessLayer;
using ServiceLayer;
using ServiceLayer.DiscussionForum;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using DataAccessLayer.Models.School;

namespace KFC.SIT.WebAPI.Controllers
{
    // TODO consistency throughout project on (DepartmentQuestions) or (SchoolDepartmentQuestions)


    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DiscussionForumController : ApiController
    {
        private bool securityPass = false;
        private int authUserId;

        [HttpPost]
        [ActionName("PostQuestion")]
        public IHttpActionResult PostQuestion([FromBody] QuestionCreateRequestDTO questionDTO)
        {
            using (var _db = new DatabaseContext())
            {
                DiscussionForumManager discussionForumManager = new DiscussionForumManager(_db);
                try
                {
                    // TODO accountId should come from authorization and take out hard-coded value
                    discussionForumManager.PostQuestion(questionDTO, questionDTO.AccountId);
                    _db.SaveChanges();
                    return Content(HttpStatusCode.OK, "Question posted successfully");
                }
                catch (Exception ex)
                {
                    return Content(HttpStatusCode.InternalServerError, ex.InnerException);
                }
            }
        }

        [HttpGet]
        [ActionName("GetQuestionsBySchool")]
        public IHttpActionResult GetQuestionsBySchool(int schoolId)
        {
            try
            {
                using (var _db = new DatabaseContext())
                {
                    DiscussionForumManager _discussionForumManager = new DiscussionForumManager(_db);
                    var questions = _discussionForumManager.GetSchoolQuestions(schoolId);
                    return Content(HttpStatusCode.OK, questions);
                }
            }
            catch (Exception ex) when (ex is ArgumentException)
            {
                return Content(HttpStatusCode.NoContent, ex.Message);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetQuestionsByDepartment")]
        public IHttpActionResult GetQuestionsByDepartment(int departmentId)
        {
            try
            {
                using (var _db = new DatabaseContext())
                {
                    DiscussionForumManager _discussionForumManager = new DiscussionForumManager(_db);
                    var questions = _discussionForumManager.GetSchoolDepartmentQuestions(departmentId);
                    return Content(HttpStatusCode.OK, questions);
                }
            }
            catch (Exception ex) when (ex is ArgumentException)
            {
                return Content(HttpStatusCode.NoContent, ex.Message);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetQuestionsByCourse")]
        public IHttpActionResult GetQuestionsByCourse(int courseId)
        {
            try
            {
                using (var _db = new DatabaseContext())
                {
                    DiscussionForumManager _discussionForumManager = new DiscussionForumManager(_db);
                    var questions = _discussionForumManager.GetCourseQuestions(courseId);
                    return Content(HttpStatusCode.OK, questions);
                }
            }
            catch (Exception ex) when (ex is ArgumentException)
            {
                return Content(HttpStatusCode.NoContent, ex.Message);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetDraftQuestions")]
        public IHttpActionResult GetDraftQuestions()
        {
            try
            {
                using (var _db = new DatabaseContext())
                {
                    DiscussionForumManager _discussionForumManager = new DiscussionForumManager(_db);
                    // TODO change hardcoded userId to 
                    var questions = _discussionForumManager.GetDraftQuestions(2);
                    return Content(HttpStatusCode.OK, questions);
                }
            }
            catch (Exception ex) when (ex is ArgumentException)
            {
                return Content(HttpStatusCode.NoContent, ex.Message);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ActionName("PostAnswer")]
        public IHttpActionResult PostAnswer([FromBody] AnswerCreateRequestDTO answerDTO)
        {
            using (var _db = new DatabaseContext())
            {
                DiscussionForumManager discussionForumManager = new DiscussionForumManager(_db);
                try
                {
                    // TODO accountId should come from authorization and take out hard-coded value
                    discussionForumManager.PostAnswer(answerDTO, 3);
                    _db.SaveChanges();
                    return Content(HttpStatusCode.OK, "Answer posted successfully");
                }
                catch (QuestionIsClosedException ex)
                {
                    return Content(HttpStatusCode.Forbidden, ex.Message);
                }
                catch (NotEnoughExpException ex)
                {
                    // TODO BadRequest?
                    return Content(HttpStatusCode.BadRequest, ex.Message);
                }
                catch (Exception ex)
                {
                    return Content(HttpStatusCode.InternalServerError, ex.InnerException);
                }
            }
        }

        [HttpGet]
        [ActionName("GetAnswers")]
        public IHttpActionResult GetAnswers(int questionId)
        {
            try
            {
                using (var _db = new DatabaseContext())
                {
                    DiscussionForumManager _discussionForumManager = new DiscussionForumManager(_db);
                    var answers = _discussionForumManager.GetAnswers(questionId);
                    return Content(HttpStatusCode.OK, answers);
                }
            }
            catch (Exception ex) when (ex is ArgumentException)
            {
                return Content(HttpStatusCode.NoContent, ex.Message);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Test for <T>
        [HttpGet]
        [ActionName("GetAnyQuestion")]
        public IHttpActionResult GetAnyQuestion(int questionId)
        {
            try
            {
                using (var _db = new DatabaseContext())
                {
                    QuestionServices qServices = new QuestionServices(_db);
                    var q = qServices.GetAnyQuestion<DepartmentQuestion>(questionId);
                    QuestionResponseDTO questionResponseDTO = new QuestionResponseDTO
                    {
                        QuestionId = q.Id,
                        ////SchoolId = ,
                        //SchoolName = q.School.Name,
                        ////DepartmentId = , 
                        DepartmentName = q.SchoolDepartment.Department.Name,
                        ////CourseId = ,
                        //CourseName = ,
                        AccountId = q.AccountId,
                        AccountName = q.Account.UserName,
                        Text = q.Text,
                        ExpNeededToAnswer = q.ExpNeededToAnswer,
                        IsClosed = q.IsClosed,
                        SpamCount = q.SpamCount,
                        AnswerCount = q.Answers.Count,
                        DateCreated = q.DateCreated,
                        DateUpdated = q.DateUpdated
                    };
                    return Content(HttpStatusCode.OK, questionResponseDTO);
                }
            }
            catch (Exception ex) when (ex is ArgumentException)
            {
                return Content(HttpStatusCode.NoContent, ex.Message);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //[HttpPost]
        //[ActionName("PostQuestionFromDraft")]
        //public IHttpActionResult PostQuestionFromDraft([FromBody] QuestionCreateFromDraftRequestDTO questionDTO)
        //{
        //    using (var _db = new DatabaseContext())
        //    {
        //        Question question;
        //        try
        //        {
        //            questionDTO.AccountId = authUserId;
        //            DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
        //            question = discussionManager.PostQuestion(questionDTO, authUserId);
        //        }
        //        catch (InvalidQuestionLengthException ex)
        //        {
        //            return Content(HttpStatusCode.BadRequest, ex.Message);
        //        }

        //        DiscussionForumServices discussionServices = new DiscussionForumServices(_db);
        //        var qDraft = discussionServices.DeleteQuestionDraft(qDraftId);

        //        try
        //        {
        //            _db.SaveChanges();
        //        }
        //        catch (Exception ex)
        //        {
        //            return Content(HttpStatusCode.InternalServerError, ex.Message);
        //        }

        //        return Content(HttpStatusCode.OK, question);
        //    }
        //}



        //        [HttpPost]
        //        [ActionName("PostAnswer")]
        //        public IHttpActionResult PostAnswer([FromBody] AnswerCreateRequestDTO answerDTO)
        //        {
        //            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
        //                 Request.Headers
        //             );
        //            if (securityContext == null)
        //            {
        //                return Unauthorized();
        //            }
        //            SessionManager sm = new SessionManager();
        //            if (!sm.ValidateSession(securityContext.Token))
        //            {
        //                return Unauthorized();
        //            }

        //            AuthorizationManager authorizationManager = new AuthorizationManager(
        //                securityContext
        //            );
        //            // TODO get this from table in database.
        //            //List<string> requiredClaims = new List<string>()
        //            //{
        //            //    "CanPostAnswer"
        //            //};
        //            //if (!authorizationManager.CheckClaims(requiredClaims))
        //            //{
        //            //    return Unauthorized();
        //            //}
        //            //else
        //            //{
        //                UserManager um = new UserManager();
        //                Account user = um.FindByUserName(securityContext.UserName);
        //                if (user == null)
        //                {
        //                    return Unauthorized();
        //                }
        //                authUserId = um.FindByUserName(securityContext.UserName).Id;
        //            //}

        //            using (var _db = new DatabaseContext())
        //            {
        //                Answer answer;
        //                try
        //                {
        //                    answerDTO.AccountId = authUserId;
        //                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
        //                    answer = discussionManager.PostAnswer(answerDTO);
        //                }
        //                catch (QuestionIsClosedException ex)
        //                {
        //                    return Content(HttpStatusCode.Forbidden, ex.Message);
        //                }
        //                catch (NotEnoughExpException ex)
        //                {
        //                    return Content(HttpStatusCode.Conflict, ex.Message);
        //                }
        //                return Content(HttpStatusCode.OK, "Answer was posted succesfully.");
        //            }
        //        }

        //        [HttpPost]
        //        [ActionName("UpdateQuestions")]
        //        public IHttpActionResult UpdateQuestion([FromBody] QuestionUpdateRequestDTO questionDTO)
        //        {
        //            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
        //                 Request.Headers
        //             );
        //            if (securityContext == null)
        //            {
        //                return Unauthorized();
        //            }
        //            SessionManager sm = new SessionManager();
        //            if (!sm.ValidateSession(securityContext.Token))
        //            {
        //                return Unauthorized();
        //            }

        //            AuthorizationManager authorizationManager = new AuthorizationManager(
        //                securityContext
        //            );
        //            // TODO get this from table in database.
        //            //List<string> requiredClaims = new List<string>()
        //            //{
        //            //    "CanPostQuestion"
        //            //};
        //            //if (!authorizationManager.CheckClaims(requiredClaims))
        //            //{
        //            //    return Unauthorized();
        //            //}
        //            //else
        //            //{
        //                UserManager um = new UserManager();
        //                Account user = um.FindByUserName(securityContext.UserName);
        //                if (user == null)
        //                {
        //                    return Unauthorized();
        //                }
        //                authUserId = um.FindByUserName(securityContext.UserName).Id;
        //            //}



        //            using (var _db = new DatabaseContext())
        //            {
        //                Question question;
        //                try
        //                {
        //                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
        //                    question = discussionManager.EditQuestion(questionDTO, authUserId);
        //                    return Content(HttpStatusCode.OK, "Question was posted succesfully.");
        //                }
        //                catch (QuestionIsClosedException ex)
        //                {
        //                    return Content(HttpStatusCode.Forbidden, ex.Message);
        //                }
        //                catch (InvalidQuestionLengthException ex)
        //                {
        //                    return Content(HttpStatusCode.BadRequest, ex.Message);
        //                }
        //                catch (InvalidAccountException ex)
        //                {
        //                    return Content(HttpStatusCode.Unauthorized, ex.Message);
        //                }
        //                catch (QuestionUnavailableException ex)
        //                {
        //                    return Content(HttpStatusCode.Forbidden, ex.Message);
        //                }
        //            }
        //        }

        //        [HttpPost]
        //        [ActionName("IncreaseQuestionSpamCount")]
        //        public IHttpActionResult IncreaseQuestionSpamCount(int questionId)
        //        {
        //            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
        //                 Request.Headers
        //             );
        //            if (securityContext == null)
        //            {
        //                return Unauthorized();
        //            }
        //            SessionManager sm = new SessionManager();
        //            if (!sm.ValidateSession(securityContext.Token))
        //            {
        //                return Unauthorized();
        //            }

        //            AuthorizationManager authorizationManager = new AuthorizationManager(
        //                securityContext
        //            );
        //            // TODO get this from table in database.
        //            List<string> requiredClaims = new List<string>()
        //            {
        //                "CanMarkQuestionAsSpam"
        //            };
        //            if (!authorizationManager.CheckClaims(requiredClaims))
        //            {
        //                return Unauthorized();
        //            }
        //            else
        //            {
        //                UserManager um = new UserManager();
        //                Account user = um.FindByUserName(securityContext.UserName);
        //                if (user == null)
        //                {
        //                    return Unauthorized();
        //                }
        //                authUserId = um.FindByUserName(securityContext.UserName).Id;
        //            }

        //            using (var _db = new DatabaseContext())
        //            {
        //                Question question;
        //                try
        //                {
        //                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
        //                    DiscussionForumServices discussionForumServices = new DiscussionForumServices(_db);
        //                    question = discussionManager.IncreaseQuestionSpamCount(questionId, authUserId);
        //                    var questionDTO = discussionForumServices.ApplyQuestionFormat(question);
        //                    return Content(HttpStatusCode.OK, question);
        //                }
        //                catch (ArgumentNullException)
        //                {
        //                    return Content(HttpStatusCode.BadRequest, "Answer does not exist");
        //                }
        //                catch (InvalidAccountException ex)
        //                {
        //                    return Content(HttpStatusCode.Unauthorized, ex.Message);
        //                }
        //            }
        //        }

        //        [HttpPost]
        //        [ActionName("IncreaseAnswerSpamCount")]
        //        public IHttpActionResult IncreaseAnswerSpamCount(int answerId)
        //        {
        //            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
        //                 Request.Headers
        //             );
        //            if (securityContext == null)
        //            {
        //                return Unauthorized();
        //            }
        //            SessionManager sm = new SessionManager();
        //            if (!sm.ValidateSession(securityContext.Token))
        //            {
        //                return Unauthorized();
        //            }

        //            AuthorizationManager authorizationManager = new AuthorizationManager(
        //                securityContext
        //            );
        //            // TODO get this from table in database.
        //            List<string> requiredClaims = new List<string>()
        //            {
        //                "CanMarkAnswerAsSpam"
        //            };
        //            if (!authorizationManager.CheckClaims(requiredClaims))
        //            {
        //                return Unauthorized();
        //            }
        //            else
        //            {
        //                UserManager um = new UserManager();
        //                Account user = um.FindByUserName(securityContext.UserName);
        //                if (user == null)
        //                {
        //                    return Unauthorized();
        //                }
        //                authUserId = um.FindByUserName(securityContext.UserName).Id;
        //            }

        //            using (var _db = new DatabaseContext())
        //            {
        //                Answer answer;
        //                try
        //                {
        //                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
        //                    DiscussionForumServices discussionForumServices = new DiscussionForumServices(_db);
        //                    answer = discussionManager.IncreaseAnswerSpamCount(answerId, authUserId);
        //                    var answerDTO = discussionForumServices.ApplyAnswerFortmat(answer);
        //                    return Content(HttpStatusCode.OK, answerDTO);
        //                }
        //                catch (ArgumentNullException)
        //                {
        //                    return Content(HttpStatusCode.BadRequest, "Answer does not exist");
        //                }
        //                catch (InvalidAccountException ex)
        //                {
        //                    return Content(HttpStatusCode.Unauthorized, ex.Message);
        //                }
        //            }
        //        }

        //        [HttpPost]
        //        [ActionName("IncreaseAnswerHelpfulCount")]
        //        public IHttpActionResult IncreaseAnswerHelpfulCount(int answerId)
        //        {
        //            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
        //                 Request.Headers
        //             );
        //            if (securityContext == null)
        //            {
        //                return Unauthorized();
        //            }
        //            SessionManager sm = new SessionManager();
        //            if (!sm.ValidateSession(securityContext.Token))
        //            {
        //                return Unauthorized();
        //            }

        //            AuthorizationManager authorizationManager = new AuthorizationManager(
        //                securityContext
        //            );
        //            // TODO get this from table in database.
        //            List<string> requiredClaims = new List<string>()
        //            {
        //                "CanMarkAnswerAsHelpful"
        //            };
        //            if (!authorizationManager.CheckClaims(requiredClaims))
        //            {
        //                return Unauthorized();
        //            }
        //            else
        //            {
        //                UserManager um = new UserManager();
        //                Account user = um.FindByUserName(securityContext.UserName);
        //                if (user == null)
        //                {
        //                    return Unauthorized();
        //                }
        //                authUserId = um.FindByUserName(securityContext.UserName).Id;
        //            }

        //            using (var _db = new DatabaseContext())
        //            {
        //                try
        //                {
        //                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
        //                    DiscussionForumServices discussionServices = new DiscussionForumServices(_db);
        //                    var answer = discussionManager.IncreaseHelpfulCount(answerId, authUserId);
        //                    var answerDTO = discussionServices.ApplyAnswerFortmat(answer);
        //                    return Content(HttpStatusCode.OK, answerDTO);
        //                }
        //                catch (ArgumentNullException)
        //                {
        //                    return Content(HttpStatusCode.BadRequest, "Answer does not exist");
        //                }
        //                catch (InvalidAccountException ex)
        //                {
        //                    return Content(HttpStatusCode.Unauthorized, ex.Message);
        //                }
        //                catch (DbUpdateConcurrencyException)
        //                {

        //                    return Content(HttpStatusCode.InternalServerError, "There was an error on the server");
        //                }
        //            }
        //        }

        //        [HttpPost]
        //        [ActionName("IncreaseAnswerUnHelpfulCount")]
        //        public IHttpActionResult IncreaseAnswerUnHelpfulCount(int answerId)
        //        {
        //            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
        //                 Request.Headers
        //             );
        //            if (securityContext == null)
        //            {
        //                return Unauthorized();
        //            }
        //            SessionManager sm = new SessionManager();
        //            if (!sm.ValidateSession(securityContext.Token))
        //            {
        //                return Unauthorized();
        //            }

        //            AuthorizationManager authorizationManager = new AuthorizationManager(
        //                securityContext
        //            );
        //            // TODO get this from table in database.
        //            List<string> requiredClaims = new List<string>()
        //            {
        //                "CanMarkAnswerAsUnHelpful"
        //            };
        //            if (!authorizationManager.CheckClaims(requiredClaims))
        //            {
        //                return Unauthorized();
        //            }
        //            else
        //            {
        //                UserManager um = new UserManager();
        //                Account user = um.FindByUserName(securityContext.UserName);
        //                if (user == null)
        //                {
        //                    return Unauthorized();
        //                }
        //                authUserId = um.FindByUserName(securityContext.UserName).Id;
        //            }

        //            using (var _db = new DatabaseContext())
        //            {
        //                Answer answer;
        //                try
        //                {
        //                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
        //                    DiscussionForumServices discussionForumServices = new DiscussionForumServices(_db);
        //                    answer = discussionManager.IncreaseUnHelpfulCount(answerId, authUserId);
        //                    var answerDTO = discussionForumServices.ApplyAnswerFortmat(answer);
        //                    return Content(HttpStatusCode.OK, answerDTO);
        //                }
        //                catch (ArgumentNullException)
        //                {
        //                    return Content(HttpStatusCode.BadRequest, "Answer does not exist");
        //                }
        //                catch (InvalidAccountException ex)
        //                {
        //                    return Content(HttpStatusCode.Unauthorized, ex.Message);
        //                }
        //            }
        //        }

        //        [HttpPost]
        //        [ActionName("CloseQuestion")]
        //        public IHttpActionResult CloseQuestion(int questionId)
        //        {
        //            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
        //                Request.Headers
        //            );
        //            if (securityContext == null)
        //            {
        //                return Unauthorized();
        //            }
        //            SessionManager sm = new SessionManager();
        //            if (!sm.ValidateSession(securityContext.Token))
        //            {
        //                return Unauthorized();
        //            }

        //            AuthorizationManager authorizationManager = new AuthorizationManager(
        //                securityContext
        //            );
        //            // TODO get this from table in database.
        //            List<string> requiredClaims = new List<string>()
        //            {
        //                "CanCloseQuestion"
        //            };
        //            if (!authorizationManager.CheckClaims(requiredClaims))
        //            {
        //                return Unauthorized();
        //            }
        //            else
        //            {
        //                UserManager um = new UserManager();
        //                Account user = um.FindByUserName(securityContext.UserName);
        //                if (user == null)
        //                {
        //                    return Unauthorized();
        //                }
        //                authUserId = um.FindByUserName(securityContext.UserName).Id;
        //            }


        //            using (var _db = new DatabaseContext())
        //            {
        //                Question question;
        //                try
        //                {
        //                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
        //                    DiscussionForumServices discussionForumServices = new DiscussionForumServices(_db);
        //                    question = discussionManager.CloseQuestion(questionId, authUserId);
        //                    var questionDTO = discussionForumServices.ApplyQuestionFormat(question);
        //                    return Content(HttpStatusCode.OK, questionDTO);
        //                }
        //                catch (QuestionIsClosedException ex)
        //                {
        //                    return Content(HttpStatusCode.BadRequest, ex.Message);
        //                }
        //                catch (InvalidAccountException ex)
        //                {
        //                    return Content(HttpStatusCode.Unauthorized, ex.Message);
        //                }

        //            }
        //        }

        //        [HttpPost]
        //        [ActionName("MarkAsCorrectAnswer")]
        //        public IHttpActionResult MarkAsCorrectAnswer(int answerId)
        //        {
        //            List<string> requiredClaims = new List<string>()
        //            {
        //                "CanMarkAnswerAsCorrect"
        //            };
        //            authUserId = AuthorizeUser(requiredClaims);
        //            if (authUserId == 0)
        //                return Unauthorized();

        //            using (var _db = new DatabaseContext())
        //            {
        //                try
        //                {
        //                    DiscussionForumManager discussionManager = new DiscussionForumManager(_db);
        //                    DiscussionForumServices discussionServices = new DiscussionForumServices(_db);
        //                    var answer = discussionManager.MarkAsCorrectAnswer(answerId, authUserId);
        //                    var answerDTO = discussionServices.ApplyAnswerFortmat(answer);
        //                                  _db.SaveChanges();
        //                    return Content(HttpStatusCode.OK, answerDTO);
        //                }
        //                catch (ArgumentNullException)
        //                {
        //                    return Content(HttpStatusCode.BadRequest, "Answer does not exist");
        //                }
        //                catch (QuestionIsClosedException ex)
        //                {
        //                    return Content(HttpStatusCode.Forbidden, ex.Message);
        //                }
        //                catch (InvalidAccountException ex)
        //                {
        //                    return Content(HttpStatusCode.Unauthorized, ex.Message);
        //                }
        //                catch (DbUpdateConcurrencyException)
        //                {

        //                    return Content(HttpStatusCode.InternalServerError, "There was an error on the server");
        //                }
        //            }
        //        }

        //        public int AuthorizeUser(List<string> requiredClaims)
        //        {
        //            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
        //                 Request.Headers
        //             );
        //            if (securityContext == null)
        //            {
        //                return 0;
        //            }
        //            SessionManager sm = new SessionManager();
        //            if (!sm.ValidateSession(securityContext.Token))
        //            {
        //                return 0;
        //            }

        //            AuthorizationManager authorizationManager = new AuthorizationManager(
        //                securityContext
        //            );
        //            // TODO get this from table in database.
        //            //List<string> requiredClaims = new List<string>()
        //            //{
        //            //    "CanPostQuestion"
        //            //};
        //            if (!authorizationManager.CheckClaims(requiredClaims))
        //            {
        //                return 0;
        //            }
        //            else
        //            {
        //                UserManager um = new UserManager();
        //                Account user = um.FindByUserName(securityContext.UserName);
        //                if (user == null)
        //                {
        //                    return 0;
        //                }
        //                authUserId = um.FindByUserName(securityContext.UserName).Id;
        //            }
        //            return authUserId;
        //        }




        // Authorization code

        //        SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
        //                 Request.Headers
        //             );
        //            if (securityContext == null)
        //            {
        //                return Unauthorized();
        //    }
        //    SessionManager sm = new SessionManager();
        //            if (!sm.ValidateSession(securityContext.Token))
        //            {
        //                return Unauthorized();
        //}

        //AuthorizationManager authorizationManager = new AuthorizationManager(
        //    securityContext
        //);
        //TODO get this from table in database.
        //List<string> requiredClaims = new List<string>()
        //{
        //    "CanPostQuestion"
        //};
        //if (!authorizationManager.CheckClaims(requiredClaims))
        //{
        //    return Unauthorized();
        //}
        //else
        //{
        //    UserManager um = new UserManager();
        //    Account user = um.FindByUserName(securityContext.UserName);
        //    if (user == null)
        //    {
        //        return Unauthorized();
        //    }
        //    authUserId = um.FindByUserName(securityContext.UserName).Id;
        //}






        //Testing
        //// delete later. Don't need
        //[HttpGet]
        //[ActionName("GetAllQuestions")]
        //public IHttpActionResult GetQuestions()
        //{
        //    using (var _db = new DatabaseContext())
        //    {
        //        var questions = _db.Questions
        //            .OfType<PostedQuestion>()
        //            .OrderBy(q => q.DateCreated)
        //            .Select(t => new QuestionResponseDTO { QuestionId = t.Id, AccountId = t.AccountId, Text = t.Text, ExpNeededToAnswer = t.ExpNeededToAnswer, IsClosed = t.IsClosed, SpamCount = t.SpamCount })
        //             .ToList();


        //        return Content(HttpStatusCode.OK, questions);
        //    }
        //}

        //[HttpPost]
        //[ActionName("PostQuestion")]
        //public IHttpActionResult PostQuestion()
        //{
        //    using (var _db = new DatabaseContext())
        //    {
        //        //School school = new School()
        //        //{
        //        //    Id = 1,
        //        //    Name = "Lbsu",
        //        //    ContactEmail = "Lbsu@csulb.edu",
        //        //    EmailDomain = "@csulb.edu"
        //        //};
        //        //Question schoolQuestion = new SchoolQuestion(2, 1, "Testing this from controller", 20);
        //        PostedQuestion schoolQuestion = new SchoolQuestion()
        //        {
        //            Text = "This is a test quesiton",
        //            ExpNeededToAnswer = 0,
        //            //DateCreated = DateTime.Now,
        //            //DateUpdated = DateTime.Now,
        //            AccountId = 2,
        //            //IsClosed = false,
        //            //SpamCount = 0,
        //            SchoolId = 1,
        //            //School = school,

        //        };
        //        try
        //        {
        //            _db.Questions.Add(schoolQuestion);
        //            _db.SaveChanges();
        //            //_db.Entry(schoolQuestion).State = EntityState.Added;
        //            //            return question;
        //            var questions = _db.Questions
        //            .OfType<SchoolQuestion>()
        //            .OrderBy(q => q.DateCreated)
        //            .Select(t => new QuestionResponseDTO { QuestionId = t.Id, AccountId = t.AccountId, Text = t.Text, ExpNeededToAnswer = t.ExpNeededToAnswer, IsClosed = t.IsClosed, SpamCount = t.SpamCount, SchoolName = t.School.Name, AccountName = t.Account.UserName })
        //             .ToList();
        //            return Content(HttpStatusCode.OK, questions);
        //        }
        //        catch (Exception ex)
        //        {
        //            return Content(HttpStatusCode.ExpectationFailed, ex.InnerException);
        //        }
        //    }
        //}






    }
}
