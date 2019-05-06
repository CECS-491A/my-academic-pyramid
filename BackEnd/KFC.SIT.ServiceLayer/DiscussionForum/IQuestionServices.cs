using DataAccessLayer.Models.DiscussionForum;
using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DiscussionForum
{
    public interface IQuestionServices<T> where T : Question
    {
        T PostQuestion(T question);
        //Answer PostAnswer(Answer answer);
        T GetQuestion(int questionId);
        //Answer GetAnswer(int answerId);
        //List<Question> GetQuestions(int schoolId, int departmentId, int courseId);
        List<T> GetQuestions(List<int> ids);
        T UpdateQuestion(T question);
        T DeleteQuestion(int questionId);
        QuestionResponseDTO ApplyQuestionResponseDTOFormat(T question);
    }
}
