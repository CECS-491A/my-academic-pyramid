using DataAccessLayer.Models.DiscussionForum;
using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DiscussionForum
{
    public interface IQuestionServices
    {
        Question PostQuestion(Question question);
        Question GetQuestion(int questionId);
        PostedQuestion GetPostedQuestion(int questionId);
        DraftQuestion GetDraftQuestion(int questionId);
        List<SchoolQuestion> GetSchoolQuestions(int schoolId);
        List<DepartmentQuestion> GetSchoolDepartmentQuestions(int schoolDepartmentId);
        List<CourseQuestion> GetCourseQuestions(int courseId);
        List<DraftQuestion> GetDraftQuestionsForUser(int accountId);
        Question UpdateQuestion(Question question);
        Question DeleteQuestion(int questionId);
        PostedQuestion CloseQuestion(int questionId);
        PostedQuestion IncreaseQuestionSpamCount(int questionId);
    }


    //public interface IQuestionServices<T> where T : Question
    //{
    //    T PostQuestion(T question);
    //    //Answer PostAnswer(Answer answer);
    //    T GetQuestion(int questionId);
    //    //Answer GetAnswer(int answerId);
    //    //List<Question> GetQuestions(int schoolId, int departmentId, int courseId);
    //    List<T> GetQuestions(List<int> ids);
    //    T UpdateQuestion(T question);
    //    T DeleteQuestion(int questionId);
    //    QuestionResponseDTO ApplyQuestionResponseDTOFormat(T question);
    //}
}
