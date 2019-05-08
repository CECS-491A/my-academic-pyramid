using DataAccessLayer.DTOs;
using DataAccessLayer.Models.DiscussionForum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DiscussionForum
{
    public interface IAnswerServices
    {
        Answer PostAnswer(Answer answer);
        Answer GetAnswer(int answerId);
        List<Answer> GetAnswers(int questionId);
        Answer UpdateAnswer(Answer answer);
        Answer DeleteAnswer(int answerId);
        Answer IncreaseAnswerSpamCount(int answerId);
        Answer IncreaseHelpfulCount(int answerId);
        Answer IncreaseUnHelpfulCount(int answerId);
        Answer MarkAnswerAsCorrect(int answerId);
        AnswerResponseDTO ApplyAnswerFormat(Answer answer);
        //Question GetPostedQuestion(int questionId);
        //Question UpdateAnyPostedQuestion(Question question);
    }
}
