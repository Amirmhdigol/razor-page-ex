using RazorEx.DAL.Entities;
using RazorEX.BAL.DTOs.ForumDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Contracts
{
    public interface IForum
    {
        int AddQuestion(AddQuestionDTO question);
        ShowQuestionDTO ShowQuestion(int questionid);
        void AddAnswer(AddAnswerDTO addAnswer);
        void ChangeIsTrueAnswer(int questionId, int answerId);
    }
}
