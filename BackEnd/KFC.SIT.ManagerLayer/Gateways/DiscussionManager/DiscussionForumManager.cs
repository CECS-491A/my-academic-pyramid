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
        //DiscussionForumServices _discussionservices;
        IAnswerServices _answerServices;
        //IQuestionServices<SchoolQuestion> _schoolQuestionServices;
        //IQuestionServices<DepartmentQuestion> _departmentQuestionServices;
        //IQuestionServices<CourseQuestion> _courseQuestionServices;
        //IQuestionServices<DraftQuestion> _draftQuestionServices;
        IQuestionServices _questionServices;
        IUserManagementServices _userManagementServices;
        //IEmailService _emailservice;
        private DatabaseContext _db;


        public DiscussionForumManager(DatabaseContext _db)
        {
            this._db = _db;
            //this._discussionservices = new DiscussionForumServices(_db);
            this._answerServices = new AnswerServices(_db);
            //this._schoolQuestionServices = new SchoolQuestionServices(_db);
            //this._departmentQuestionServices = new DepartmentQuestionServices(_db);
            //this._courseQuestionServices = new CourseQuestionServices(_db);
            //this._draftQuestionServices = new DraftQuestionServices(_db);
            this._questionServices = new QuestionServices(_db);
            this._userManagementServices = new UserManagementServices(_db);
            //this._emailservice = new EmailService();

        }

        //Business Rules constants
        private const int _questionCharMin = 50;
        private const int _questionCharMax = 2000;
        private const int _expGainCorrectAns = 10;
        private const int _expGainHelpfullAns = 2;
        private const int _spamLimit = 3;
        //Constants
        private const string _schoolQuestion = "SchoolQuestion";
        private const string _departmentQuestion = "DepartmentQuestion";
        private const string _courseQuestion = "CourseQuestion";
        private const string _draftQuestion = "DraftQuestion";
        //private string _schoolQuestionTypeName = typeof(SchoolQuestion).Name;
        //private string _departmentQuestionTypeName = typeof(DepartmentQuestion).Name;
        //private string _courseQuestionTypeName = typeof(CourseQuestion).Name;
        //private string _draftQuestionTypeName = typeof(CourseQuestion).Name;

        public Question PostQuestion(QuestionCreateRequestDTO q, int accountId)
        {
            //Validations
            if (q.Text == null || q.Text.Length < _questionCharMin || q.Text.Length > _questionCharMax)
            {
                throw new InvalidQuestionLengthException("Question must be between " + _questionCharMin + " and " + _questionCharMax + " characters.");
            }

            //Create question after validations based on type
            //Then post question and return posted question
            Question question;
            switch (q.QuestionType)
            {
                case _schoolQuestion:
                    //Validate Ids?
                    question = new SchoolQuestion()
                    {
                        AccountId = accountId,
                        SchoolId = q.SchoolId,
                        Text = q.Text,
                        ExpNeededToAnswer = q.ExpNeededToAnswer
                    };
                    return _questionServices.PostQuestion(question);
                case _departmentQuestion:
                    //Validate Ids?
                    question = new DepartmentQuestion()
                    {
                        AccountId = accountId,
                        SchoolDepartmentId = q.DepartmentId,
                        Text = q.Text,
                        ExpNeededToAnswer = q.ExpNeededToAnswer
                    };
                    return _questionServices.PostQuestion(question);
                case _courseQuestion:
                    question = new CourseQuestion()
                    {
                        AccountId = accountId,
                        CourseId = q.CourseId,
                        Text = q.Text,
                        ExpNeededToAnswer = q.ExpNeededToAnswer
                    };
                    return _questionServices.PostQuestion(question);
                case _draftQuestion:
                    question = new DraftQuestion()
                    {
                        AccountId = accountId,
                        Text = q.Text,
                        ExpNeededToAnswer = q.ExpNeededToAnswer
                    };
                    return _questionServices.PostQuestion(question);
                default:
                    throw new ArgumentException("Invalid Question type");
            }
        }

        public Question PostQuestionFromDraft(QuestionCreateFromDraftRequestDTO q, int accountId)
        {
            DraftQuestion question = _questionServices.GetDraftQuestion(q.QuestionDraftId);
            //Validations
            if (accountId != question.AccountId)
            {
                throw new InvalidAccountException("User cannot edit another user's question");
            }
            if (q.Text == null || q.Text.Length < _questionCharMin || q.Text.Length > _questionCharMax)
            {
                throw new InvalidQuestionLengthException("Question must be between " + _questionCharMin + " and " + _questionCharMax + " characters.");
            }

            //Create question after validations based on type
            //Then post question and return posted question
            Question questionToPost = question;
            switch (q.QuestionType)
            {
                case _schoolQuestion:
                    questionToPost = (SchoolQuestion)questionToPost;
                    return _questionServices.UpdateQuestion(questionToPost);
                case _departmentQuestion:
                    //Validate Ids?
                    questionToPost = new DepartmentQuestion()
                    {
                        AccountId = accountId,
                        SchoolDepartmentId = q.DepartmentId,
                        Text = q.Text,
                        ExpNeededToAnswer = q.ExpNeededToAnswer
                    };
                    return _questionServices.PostQuestion(questionToPost);
                case _courseQuestion:
                    questionToPost = new CourseQuestion()
                    {
                        AccountId = accountId,
                        CourseId = q.CourseId,
                        Text = q.Text,
                        ExpNeededToAnswer = q.ExpNeededToAnswer
                    };
                    return _questionServices.PostQuestion(questionToPost);
                case _draftQuestion:
                    questionToPost = new DraftQuestion()
                    {
                        AccountId = accountId,
                        Text = q.Text,
                        ExpNeededToAnswer = q.ExpNeededToAnswer
                    };
                    return _questionServices.PostQuestion(questionToPost);
                default:
                    throw new ArgumentException("Invalid Question type");
            }
        }

        public Answer PostAnswer(AnswerCreateRequestDTO a, int accountId)
        {
            //Validations
            Account answerer = _userManagementServices.FindById(accountId);
            PostedQuestion question = _questionServices.GetPostedQuestion(a.QuestionId);
            if (question.IsClosed)
            {
                throw new QuestionIsClosedException("Question is closed");
            }

            if (answerer.Exp < question.ExpNeededToAnswer)
            {
                throw new NotEnoughExpException("User does not have enough Exp to answer");
            }

            //Create Answer after passed in Answer is validated
            Answer answer = new Answer()
            {
                QuestionId = a.QuestionId,
                AccountId = accountId,
                Text = a.Text,
            };
            //Post Answer
            return _answerServices.PostAnswer(answer);
        }

        // TODO make methods to format different question types like for answers? 
        // TODO check to see if School exists in the database
        public List<QuestionResponseDTO> GetSchoolQuestions(int schoolId)
        {
            var questions = _questionServices.GetSchoolQuestions(schoolId);
            var questionDTOResponses = new List<QuestionResponseDTO>();
            if (questions is null || !questions.Any())
            {
                throw new ArgumentException("No Questions for this School");
            }
            //format
            foreach (SchoolQuestion q in questions)
            {
                questionDTOResponses.Add(new QuestionResponseDTO
                {
                    QuestionId = q.Id,
                    ////SchoolId = ,
                    SchoolName = q.School.Name,
                    ////DepartmentId = , 
                    //DepartmentName = ,
                    ////CourseId = ,
                    //CourseName = ,
                    AccountId = q.AccountId,
                    AccountName = q. Account.UserName,
                    Text = q.Text,
                    ExpNeededToAnswer = q.ExpNeededToAnswer,
                    IsClosed = q.IsClosed,
                    SpamCount = q.SpamCount,
                    AnswerCount = q.Answers.Count,
                    DateCreated  = q.DateCreated,
                    DateUpdated = q.DateUpdated
                });
            }
            return questionDTOResponses;
        }

        public List<QuestionResponseDTO> GetSchoolDepartmentQuestions(int schoolDepartmentId)
        {
            var questions = _questionServices.GetSchoolDepartmentQuestions(schoolDepartmentId);
            var questionDTOResponses = new List<QuestionResponseDTO>();
            if (questions is null || !questions.Any())
            {
                throw new ArgumentException("No Questions for this School Department");
            }
            //format
            foreach (DepartmentQuestion q in questions)
            {
                questionDTOResponses.Add(new QuestionResponseDTO
                {
                    QuestionId = q.Id,
                    ////SchoolId = ,
                    SchoolName = q.SchoolDepartment.School.Name,
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
                });
            }
            return questionDTOResponses;
        }

        public List<QuestionResponseDTO> GetCourseQuestions(int courseId)
        {
            var questions = _questionServices.GetCourseQuestions(courseId);
            var questionDTOResponses = new List<QuestionResponseDTO>();
            if (questions is null || !questions.Any())
            {
                throw new ArgumentException("No Questions for this Course at this School Department");
            }
            //format
            foreach (CourseQuestion q in questions)
            {
                questionDTOResponses.Add(new QuestionResponseDTO
                {
                    QuestionId = q.Id,
                    ////SchoolId = ,
                    SchoolName = q.Course.SchoolDepartment.School.Name,
                    ////DepartmentId = , 
                    DepartmentName = q.Course.SchoolDepartment.Department.Name,
                    ////CourseId = ,
                    CourseName = q.Course.Name,
                    AccountId = q.AccountId,
                    AccountName = q.Account.UserName,
                    Text = q.Text,
                    ExpNeededToAnswer = q.ExpNeededToAnswer,
                    IsClosed = q.IsClosed,
                    SpamCount = q.SpamCount,
                    AnswerCount = q.Answers.Count,
                    DateCreated = q.DateCreated,
                    DateUpdated = q.DateUpdated
                });
            }
            return questionDTOResponses;
        }

        public List<DraftQuestionResponseDTO> GetDraftQuestions(int accountId)
        {
            var questions = _questionServices.GetDraftQuestionsForUser(accountId);
            var draftQuestionDTOResponses = new List<DraftQuestionResponseDTO>();
            if (questions is null || !questions.Any())
            {
                throw new ArgumentException("No drafts for this user");
            }
            //format
            foreach (DraftQuestion q in questions)
            {
                draftQuestionDTOResponses.Add(new DraftQuestionResponseDTO
                {
                    AccountName = q.Account.UserName,
                    Text = q.Text,
                    ExpNeededToAnswer = q.ExpNeededToAnswer,
                    DateCreated = q.DateCreated,
                    DateUpdated = q.DateUpdated
                });
            }
            return draftQuestionDTOResponses;
        }

        public List<AnswerResponseDTO> GetAnswers(int questionId)
        {
            var answers = _answerServices.GetAnswers(questionId);
            var answerDTOResponses = new List<AnswerResponseDTO>();
            if (answers is null  || !answers.Any())
            {
                throw new ArgumentException("No answers for this question");
            }
            //format
            foreach (Answer a in answers)
            {
                answerDTOResponses.Add(_answerServices.ApplyAnswerFormat(a));
            }
            return answerDTOResponses;
        }

        // update question content... answers can never be updated 
        public PostedQuestion EditQuestion(QuestionUpdateRequestDTO q, int accountId)
        {
            //Todo - better way to do this? 
            var question = _questionServices.GetPostedQuestion(q.QuestionId);
            // Validations 
            if (accountId != question.AccountId)
            {
                throw new InvalidAccountException("User cannot edit another user's question");
            }
            if (question.IsClosed)
            {
                throw new QuestionIsClosedException("Question is closed and can no longer be updated");
            }
            if (question.Answers.Count > 0)
            {
                throw new QuestionUnavailableException("Question cannot be edited after an answer has been posted");
            }
            if (q.Text == null || q.Text.Length < _questionCharMin || q.Text.Length > _questionCharMax)
            {
                throw new InvalidQuestionLengthException("Question must be between " + _questionCharMin + " and " + _questionCharMax + " characters.");
            }
            
            // Update Question after passed in Question is validated
            question.Text = q.Text;
            question.ExpNeededToAnswer = q.ExpNeededToAnswer;

            // TODO - better way to do this? Does this work? Add method to services that returns PostedQuesiton on update
            return (PostedQuestion)_questionServices.UpdateQuestion(question);
        }

        public PostedQuestion CloseQuestion(int questionId, int accountId)
        {
            var question = _questionServices.GetPostedQuestion(questionId);
            if (question.IsClosed)
            {
                throw new QuestionIsClosedException("Question is already closed");
            }
            if (question.AccountId != questionId)
            {
                throw new InvalidAccountException("User cannot close another user's question");
            }

            return _questionServices.CloseQuestion(questionId);
        }

        public Answer MarkAsCorrectAnswer(int answerId, int accountId)
        {
            var answer = _answerServices.GetAnswer(answerId);
            var question = answer.PostedQuestion;
            var account = _userManagementServices.FindById(answer.AccountId);
            // Validations 
            if (question.IsClosed)
            {
                throw new QuestionIsClosedException("Question is closed");
            }
            if (question.AccountId != accountId)
            {
                throw new InvalidAccountException("User cannot mark an answer as correct on another user's question");
            }

            answer = _answerServices.MarkAnswerAsCorrect(answerId);
            question = _questionServices.CloseQuestion(question.Id);
            account.Exp += _expGainCorrectAns;
            account = _userManagementServices.UpdateUser(account);
            return answer;
        }





        // TODO update database - make spam, helpful, & unhelpful its own table with foreign keys QuestionId/AnswerId & accoundId
        // to check that the user hasn't already done that for that Question/Answer
        // rn a user can keep increase the counts forever

        // update spam count
        // email sys admin if a question or answer reaches spam limit 
        // TODO go back to database and 
        //public Question IncreaseQuestionSpamCount(int questionId, int accountId)
        //{
        //    Question question = _discussionservices.GetQuestion(questionId);

        //    // Validate
        //    if (accountId == question.AccountId)
        //    {
        //        throw new InvalidAccountException("User cannot mark their own question as spam");
        //    }

        //    question = _discussionservices.IncreaseQuestionSpamCount(questionId);
        //    if (question.SpamCount == _spamLimit)
        //    {
        //        //               // call service to email admin because question reached spam limit
        //    }
        //    return question;
        //}

        //// update spam count
        //// email sys admin if a question or answer reaches spam limit 
        //public Answer IncreaseAnswerSpamCount(int answerId, int accountId)
        //{
        //    Answer answer = _discussionservices.GetAnswer(answerId);

        //    // Validate
        //    if (accountId == answer.AccountId)
        //    {
        //        throw new InvalidAccountException("User cannot mark their own answer as spam");
        //    }

        //    answer = _discussionservices.IncreaseAnswerSpamCount(answerId);
        //    if (answer.SpamCount == _spamLimit)
        //    {
        //        //               // call service to email admin because question reached spam limit
        //    }
        //    return answer;
        //}



        //// update answer with increased helpful count and update user Exp
        //public Answer IncreaseHelpfulCount(int answerId, int accountId)
        //{
        //    Answer answer = _discussionservices.GetAnswer(answerId);

        //    // Validate
        //    if (accountId == answer.AccountId)
        //    {
        //        throw new InvalidAccountException("User cannot mark their own answer as helpful");
        //    }

        //    answer = _discussionservices.IncreaseHelpfulCount(answerId);
        //    // update user exp
        //    Account user = _usermanagementservices.FindById(answer.AccountId);
        //    user.Exp += _expGainHelpfullAns;
        //    user = _usermanagementservices.UpdateUser(user);
        //    _db.SaveChanges();
        //    return answer;
        //}

        //// update answer with increased unhelpful count 
        //// don't think UnHulpful affects a user's Exp? 
        //public Answer IncreaseUnHelpfulCount(int answerId, int accountId)
        //{
        //    Answer answer = _discussionservices.GetAnswer(answerId);

        //    // Validate
        //    if (accountId == answer.AccountId)
        //    {
        //        throw new InvalidAccountException("User cannot mark their own answer as unhelpful");
        //    }

        //    answer = _discussionservices.IncreaseUnHelpfulCount(answerId);
        //    // update user exp
        //    //User user = _usermanagementservices.FindById(answer.PosterId);
        //    //user.Exp -= 2;
        //    //user = _usermanagementservices.UpdateUser(user);
        //    //_db.SaveChanges();
        //    return answer;
        //}



        //public bool ValidateQuestionCharLength(QuestionDTO q)
        //{
        //    if (q.Text != null && (q.Text.Length > _questionCharMin && q.Text.Length < _questionCharMax))
        //        return true;
        //    else
        //        return false;
        //}
    }
}