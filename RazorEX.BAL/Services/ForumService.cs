using Microsoft.EntityFrameworkCore;
using RazorEx.DAL.Context;
using RazorEx.DAL.Entities;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.ForumDTOs;
using RazorEX.BAL.Utilities.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Services
{
    public class ForumService : IForum
    {
        private readonly RXContext _context;

        public ForumService(RXContext context)
        {
            _context = context;
        }

        public void AddAnswer(AddAnswerDTO addAnswer)
        {
            var answer = new Answer()
            {
                AnswerId = addAnswer.AnswerId,
                Body = addAnswer.Body,
                CreationDate = addAnswer.CreationDate,
                QuestionId = addAnswer.QuestionId,
                UserId = addAnswer.UserId,
            };
            _context.Answers.Add(answer);
            _context.SaveChanges();
        }

        public int AddQuestion(AddQuestionDTO question)
        {
            question.CreationDate = DateTime.Now;
            question.ModifiedDate = DateTime.Now;
            var a = _context.Questions.Add(QuestionToDTO.MapBackAdd(question));
            _context.SaveChanges();
            return a.Entity.QuestionId;
        }

        public void ChangeIsTrueAnswer(int questionId, int answerId)
        {
            var answers = _context.Answers.Where(a => a.QuestionId == questionId);
            foreach (var ans in answers)
            {
                ans.IsTheTrueAnswer = false;

                if (ans.AnswerId == answerId)
                {
                    ans.IsTheTrueAnswer = true;
                }
            }
            _context.UpdateRange(answers);
            _context.SaveChanges();
        }

        public IEnumerable<Question> GetAllQuestionById(int? productId, string filter = "")
        {
            IQueryable<Question> result = _context.Questions.Include(a=>a.User).Include(a=>a.Answers).Include(a=>a.Products).Where(a => EF.Functions.Like(a.Title, $"%{filter}%"));
            if (productId != null)
            {
                result = result.Where(a => a.ProductsId == productId);
            }
            return result.ToList();
        }

        public ShowQuestionDTO ShowQuestion(int questionid)
        {
            var Question = _context.Questions.Include(a => a.User)
                .FirstOrDefault(a => a.QuestionId == questionid);
            var QuestionAnsewers = new ShowQuestionDTO()
            {
                Question = QuestionToDTO.Map(Question),
                Answers = _context.Answers.Include(a => a.User).Where(a => a.QuestionId == questionid).ToList(),
            };
            return QuestionAnsewers;
        }
    }
}
