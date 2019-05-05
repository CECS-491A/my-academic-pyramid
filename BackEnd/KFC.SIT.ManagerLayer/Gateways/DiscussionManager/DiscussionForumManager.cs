//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using DataAccessLayer;
//using DataAccessLayer.Models;
//using DataAccessLayer.Models.DiscussionForum;
//using ServiceLayer.DiscussionForum;
//using ServiceLayer.UserManagement.UserAccountServices;
//using DataAccessLayer.DTOs;
//using System.Net.Http;
//using System.Net;
//using ServiceLayer;

//namespace ManagerLayer.DiscussionManager
//{
//    public class DiscussionForumManager
//    {
//        //DiscussionForumServices _discussionservices;
//        IAnswerServices _answerServices;
//        IQuestionServices<SchoolQuestion> _schoolQuestionServices;
//        IQuestionServices<DepartmentQuestion> _departmentQuestionServices;
//        IQuestionServices<CourseQuestion> _courseQuestionServices;
//        IQuestionServices<DraftQuestion> _draftQuestionServices;
//        UserManagementServices _userManagementServices;
//        //IEmailService _emailservice;
//        private DatabaseContext _db;


//        public DiscussionForumManager(DatabaseContext _db)
//        {
//            this._db = _db;
//            //this._discussionservices = new DiscussionForumServices(_db);
//            this._answerServices = new AnswerServices(_db);
//            this._schoolQuestionServices = new SchoolQuestionServices(_db);
//            this._departmentQuestionServices = new DepartmentQuestionServices(_db);
//            this._courseQuestionServices = new CourseQuestionServices(_db);
//            this._draftQuestionServices = new DraftQuestionServices(_db);
//            this._userManagementServices = new UserManagementServices(_db);
//            //this._emailservice = new EmailService();

//        }

//        // Business Rules constants
//        private const int _questionCharMin = 50;
//        private const int _questionCharMax = 2000;
//        private const int _expGainCorrectAns = 10;
//        private const int _expGainHelpfullAns = 2;
//        private const int _spamLimit = 3;
        

//        public DraftQuestion PostQuestion(QuestionCreateRequestDTO q, int accountId)
//        {
//            // Validations 
//            if (q.Text == null || q.Text.Length < _questionCharMin || q.Text.Length > _questionCharMax)
//            {
//                throw new InvalidQuestionLengthException("Question must be between " + _questionCharMin + " and " + _questionCharMax + " characters.");
//            }

//            // Create question after validations based on type 
//            // Then post question and return posted question 
//            switch (q.QuestionType)
//            {
//                case "School":
//                    // Validate Ids?
//                    SchoolQuestion schoolQuestion = new SchoolQuestion
//                    {
//                        AccountId = q.AccountId,
//                        SchoolId = q.SchoolId
//                    };
//                    return _schoolQuestionServices.PostQuestion(schoolQuestion);
//                case "Department":
//                    // Validate Ids?
//                    DepartmentQuestion departmentQuestion = new DepartmentQuestion
//                    {
//                        AccountId = q.AccountId,
//                        SchoolId = q.SchoolId,
//                        SchoolDepartmentId = q.DepartmentId
//                    };
//                    return _departmentQuestionServices.PostQuestion(departmentQuestion);
//                case "Course":
//                    CourseQuestion courseQuestion = new CourseQuestion
//                    {
//                        AccountId = q.AccountId,
//                        SchoolId = q.SchoolId,
//                        SchoolDepartmentId = q.DepartmentId,
//                        CourseId = q.CourseId
//                    };
//                    return _courseQuestionServices.PostQuestion(courseQuestion);
//                case "Draft":
//                    DraftQuestion draftQuestion = new DraftQuestion
//                    {
//                        AccountId = q.AccountId,
//                        Text = q.Text,
//                        MinimumExpForAnswer = q.MinimumExpForAnswer
//                    };
//                    return _draftQuestionServices.PostQuestion(draftQuestion);
//                default:
//                    throw new ArgumentException("Invalid Question type");
//            }
//        }

//        //public DraftQuestion PostQuestionDraft(QuestionDraftCreateRequestDTO q)
//        //{
//        //    // Create Question draft after passed in Question is validated
//        //    DraftQuestion qDraft = new DraftQuestion
//        //    {
//        //        SchoolId = q.SchoolId,
//        //        DepartmentId = q.DepartmentId,
//        //        CourseId = q.CourseId,
//        //        AccountId = q.AccountId,
//        //        Text = q.Text,
//        //        MinimumExpForAnswer = q.MinimumExpForAnswer,
//        //        //IsDraft = q.IsDraft,
//        //    };
//        //    // Post Draft
//        //    return _discussionservices.PostQuestionDraft(qDraft);
//        //}

//        public Answer PostAnswer(AnswerCreateRequestDTO a)
//        {
//            // Validations
//            Account answerer = _userManagementServices.FindById(a.AccountId);
//            //Question question = _discussionservices.GetQuestion(a.QuestionId);
//            Question question = _answerServices.GetPostedQuestion(a.QuestionId);
//            if (question.IsClosed)
//            {
//                throw new QuestionIsClosedException("Question is closed");
//            }

//            if (answerer.Exp < question.ExpNeededToAnswer)
//            {
//                throw new NotEnoughExpException("User does not have enough Exp to answer");
//            }

//            // Creat Answer after passed in Answer is validated
//            Answer answer = new Answer
//            {
//                QuestionId = a.QuestionId,
//                AccountId = a.AccountId,
//                Text = a.Text,
//            };
//            // Post Answer
//            //return _discussionservices.PostAnswer(answer);
//            return _answerServices.PostAnswer(answer);
//        }

//        //public List<QuestionResponseDTO> GetQuestions()
//        //{
//        //    var questions = _discussionservices.GetQuestions();
//        //    var questionDTOResponses = new List<QuestionResponseDTO>();
//        //    if (questionDTOResponses is null)
//        //    {
//        //        throw new ArgumentException("No Questions");
//        //    }
//        //    //format
//        //    foreach (Question q in questions)
//        //    {
//        //        questionDTOResponses.Add(_discussionservices.ApplyQuestionFormat(q));
//        //        //questionDTOResponses.Add(new QuestionResponseDTO
//        //        //{
//        //        //    QuestionId = q.Id,
//        //        //    // = q.SchoolId,
//        //        //    SchoolName = q.School.Name,
//        //        //    //DepartmentId = q.DepartmentId,
//        //        //    DepartmentName = q.Department.Department.Name,
//        //        //    //CourseId = q.CourseId,
//        //        //    CourseName = q.Course.Name,
//        //        //    AccountId = q.AccountId,
//        //        //    AccountName = q.Account.UserName,
//        //        //    Text = q.Text,
//        //        //    MinimumExpForAnswer = q.MinimumExpForAnswer//,
//        //        //    //IsDraft = q.IsDraft
//        //        //}
//        //        //);
//        //    }
//        //    return questionDTOResponses;
//        //}

//        public List<QuestionResponseDTO> GetQuestions(int schoolId)
//        {
//            var questions = _schoolQuestionServices.GetQuestions(new List<int> { schoolId });
//            var questionDTOResponses = new List<QuestionResponseDTO>();
//            if (questionDTOResponses is null)
//            {
//                throw new ArgumentException("No Questions in this School");
//            }
//            //format
//            foreach (SchoolQuestion q in questions)
//            {
//                questionDTOResponses.Add(_schoolQuestionServices.ApplyQuestionResponseDTOFormat(q));
//            }
//            return questionDTOResponses;
//            //var questions = _discussionservices.GetQuestions(schoolId);
//            //var questionDTOResponses = new List<QuestionResponseDTO>();
//            //if (questionDTOResponses is null)
//            //{
//            //    throw new ArgumentException("No Questions in this school");
//            //}
//            ////format
//            //foreach (Question q in questions)
//            //{
//            //    questionDTOResponses.Add(_discussionservices.ApplyQuestionFormat(q));
//            //}
//            //return questionDTOResponses;
//        }

//        public List<QuestionResponseDTO> GetQuestions(int schoolId, int departmentId)
//        {
//            var questions = _departmentQuestionServices.GetQuestions(new List<int> { schoolId, departmentId });
//            var questionDTOResponses = new List<QuestionResponseDTO>();
//            if (questionDTOResponses is null)
//            {
//                throw new ArgumentException("No Questions in this Department at this School");
//            }
//            //format
//            foreach (DepartmentQuestion q in questions)
//            {
//                questionDTOResponses.Add(_departmentQuestionServices.ApplyQuestionResponseDTOFormat(q));
//            }
//            return questionDTOResponses;

//            //var questions = _discussionservices.GetQuestions(schoolId, departmentId);
//            //var questionDTOResponses = new List<QuestionResponseDTO>();
//            //if (questionDTOResponses is null)
//            //{
//            //    throw new ArgumentException("No Questions in this department at this school");
//            //}
//            ////format
//            //foreach (Question q in questions)
//            //{
//            //    questionDTOResponses.Add(_discussionservices.ApplyQuestionFormat(q));
//            //}
//            //return questionDTOResponses;
//        }

//        public List<QuestionResponseDTO> GetQuestions(int schoolId, int departmentId, int courseId)
//        {
//            var questions = _courseQuestionServices.GetQuestions(new List<int> { schoolId, departmentId, courseId });
//            var questionDTOResponses = new List<QuestionResponseDTO>();
//            if (questionDTOResponses is null)
//            {
//                throw new ArgumentException("No Questions in this Course in this Department at this School");
//            }
//            //format
//            foreach (CourseQuestion q in questions)
//            {
//                questionDTOResponses.Add(_courseQuestionServices.ApplyQuestionResponseDTOFormat(q));
//            }
//            return questionDTOResponses;

//            //var questions = _discussionservices.GetQuestions(schoolId, departmentId, courseId);
//            //var questionDTOResponses = new List<QuestionResponseDTO>();
//            //if (questionDTOResponses is null)
//            //{
//            //    throw new ArgumentException("No Questions in this course from this department at this school");
//            //}
//            ////format
//            //foreach (Question q in questions)
//            //{
//            //    questionDTOResponses.Add(_discussionservices.ApplyQuestionFormat(q));
//            //}
//            //return questionDTOResponses;
//        }

////        // update question content... answers can never be updated 
////        public Question EditQuestion(QuestionUpdateRequestDTO q, int accountId)
////        {
////            Question question = _answerServices.GetPostedQuestion(q.QuestionId);

////            // Validations 
////            if (question.IsClosed)
////            {
////                throw new QuestionIsClosedException("Question is closed");
////            }
////            if (q.Text == null || q.Text.Length < _questionCharMin || q.Text.Length > _questionCharMax)
////            {
////                throw new InvalidQuestionLengthException("Question must be between " + _questionCharMin + " and " + _questionCharMax + " characters.");
////            }
////            if (accountId != question.AccountId)
////            {
////                throw new InvalidAccountException("User cannot edit another user's question");
////            }
////            if (question.Answers.Count > 0)
////            {
////                throw new QuestionUnavailableException("Question cannot be edited after an answer has been posted");
////            }

////            // Update Question after passed in Question is validated
////            question.Text = q.Text;
////            question.MinimumExpForAnswer = q.MinimumExpForAnswer;
////            //question.IsDraft = q.IsDraft;
////            //question.IsClosed = q.IsClosed;
////            return _answerServices.UpdateAnyPostedQuestion(question);
////            //return _discussionservices.UpdateQuestion(question);
////        }

////        // update spam count
////        // email sys admin if a question or answer reaches spam limit 
////        public Question IncreaseQuestionSpamCount(int questionId, int accountId)
////        {
////            Question question = _discussionservices.GetQuestion(questionId);

////            // Validate
////            if (accountId == question.AccountId)
////            {
////                throw new InvalidAccountException("User cannot mark their own question as spam");
////            }

////            question = _discussionservices.IncreaseQuestionSpamCount(questionId);
////            if (question.SpamCount == _spamLimit)
////            {
//// //               // call service to email admin because question reached spam limit
////            }
////            return question;
////        }

////        // update spam count
////        // email sys admin if a question or answer reaches spam limit 
////        public Answer IncreaseAnswerSpamCount(int answerId, int accountId)
////        {
////            Answer answer = _discussionservices.GetAnswer(answerId);

////            // Validate
////            if (accountId == answer.AccountId)
////            {
////                throw new InvalidAccountException("User cannot mark their own answer as spam");
////            }

////            answer = _discussionservices.IncreaseAnswerSpamCount(answerId);
////            if (answer.SpamCount == _spamLimit)
////            {
//////               // call service to email admin because question reached spam limit
////            }
////            return answer;
////        }

        

////        // update answer with increased helpful count and update user Exp
////        public Answer IncreaseHelpfulCount(int answerId, int accountId)
////        {
////            Answer answer = _discussionservices.GetAnswer(answerId);

////            // Validate
////            if (accountId == answer.AccountId)
////            {
////                throw new InvalidAccountException("User cannot mark their own answer as helpful");
////            }

////            answer = _discussionservices.IncreaseHelpfulCount(answerId);
////            // update user exp
////            Account user = _usermanagementservices.FindById(answer.AccountId);
////            user.Exp += _expGainHelpfullAns;
////            user = _usermanagementservices.UpdateUser(user);
////            _db.SaveChanges();
////            return answer;
////        }

////        // update answer with increased unhelpful count 
////        // don't think UnHulpful affects a user's Exp? 
////        public Answer IncreaseUnHelpfulCount(int answerId, int accountId)
////        {
////            Answer answer = _discussionservices.GetAnswer(answerId);

////            // Validate
////            if (accountId == answer.AccountId)
////            {
////                throw new InvalidAccountException("User cannot mark their own answer as unhelpful");
////            }

////            answer = _discussionservices.IncreaseUnHelpfulCount(answerId);
////            // update user exp
////            //User user = _usermanagementservices.FindById(answer.PosterId);
////            //user.Exp -= 2;
////            //user = _usermanagementservices.UpdateUser(user);
////            //_db.SaveChanges();
////            return answer;
////        }

////        public Question CloseQuestion(int questionId, int accountId)
////        {
////            Question question = _discussionservices.GetQuestion(questionId);
////            if (question.IsClosed)
////            {
////                throw new QuestionIsClosedException("Question is already closed");
////            }
////            if (accountId != question.AccountId)
////            {
////                throw new InvalidAccountException("User cannot edit this question");
////            }

////            return _discussionservices.CloseQuestion(questionId);
////        }

////        public Answer MarkAsCorrectAnswer(int id, int accountId)
////        {
////            Answer answer = _discussionservices.GetAnswer(id);
////            Question question = answer.Question;
////            Account account = _usermanagementservices.FindById(answer.AccountId);
////            // Validations 
////            if (question.IsClosed)
////            {
////                throw new QuestionIsClosedException("Question is closed");
////            }
////            if (accountId != question.AccountId)
////            {
////                throw new InvalidAccountException("User cannot edit this question");
////            }

////            answer = _discussionservices.MarkAnswerAsCorrect(id);
////            question = _discussionservices.CloseQuestion(question.Id);
////            account.Exp += _expGainCorrectAns;
////            account = _usermanagementservices.UpdateUser(account);
////            _db.SaveChanges();
////            return answer;

////        }

//        //public bool ValidateQuestionCharLength(QuestionDTO q)
//        //{
//        //    if (q.Text != null && (q.Text.Length > _questionCharMin && q.Text.Length < _questionCharMax))
//        //        return true;
//        //    else
//        //        return false;
//        //}
//    }
//}