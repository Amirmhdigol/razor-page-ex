using RazorEx.DAL.Entities;
using RazorEX.BAL.DTOs.ForumDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Utilities.Mapper
{
    public static class QuestionToDTO
    {
        public static QuestionDTO Map(Question question)
        {
            return new QuestionDTO()
            {
                Title = question.Title,
                Body = question.Body,
                CreationDate = question.CreationDate,
                ModifiedDate = question.ModifiedDate,
                ProductsId = question.ProductsId,
                UserId = question.UserId,
                QuestionId = question.QuestionId,
                User = question.User
            };
        }
        public static Question MapBack(QuestionDTO question)
        {
            return new Question()
            {
                Title = question.Title,
                Body = question.Body,
                CreationDate = question.CreationDate,
                ModifiedDate = question.ModifiedDate,
                ProductsId = question.ProductsId,
                UserId = question.UserId,
                QuestionId = question.QuestionId,
                User = question.User
            };
        }
        public static Question MapBackAdd(AddQuestionDTO question)
        {
            return new Question()
            {
                Title = question.Title,
                Body = question.Body,
                ProductsId = question.ProductsId,
                UserId = question.UserId,
                QuestionId = question.QuestionId,
                CreationDate = question.CreationDate,
                ModifiedDate = question.ModifiedDate,
            };
        }
        public static AddQuestionDTO MapAdd(Question question)
        {
            return new AddQuestionDTO()
            {
                CreationDate = question.CreationDate,
                ModifiedDate = question.ModifiedDate,
                QuestionId = question.QuestionId,
                Title = question.Title,
                Body = question.Body,
                ProductsId = question.ProductsId,
                UserId = question.UserId,
            };
        }
    }
}
