using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RazorEx.DAL.Entities;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.ForumDTOs;
using RazorEX.BAL.Utilities.Mapper;
using System;
using System.Security.Claims;


namespace razor_page_ex.Controllers
{
    public class Forum : Controller
    {
        readonly IForum _service;

        public Forum(IForum service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
 
        [Route("/Forum/AddQuestion/{id}")]
        public IActionResult AddQuestion(int id)
        {
            Question question = new()
            {
                ProductsId = id,
            };
            return View(QuestionToDTO.MapAdd(question));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("/Forum/AddQuestion/addquestion")]
        public IActionResult AddQuestion(Question question)
        {
            if (!ModelState.IsValid)
            {
                return View(QuestionToDTO.Map(question));
            }
            question.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            int QId = _service.AddQuestion(QuestionToDTO.MapAdd(question));
            return View($"/Forum/ShowQuestion/{QId}");
        }
        [Route("/Forum/ShowQuestion/{id}")]
        public IActionResult ShowQuestion(int id)
        {
            return View(_service.ShowQuestion(id));
        }
        [HttpPost]
        [Route("/Forum/ShowQuestion/")]
        public IActionResult AddAnswer(int id, string body)
        {
            if (!string.IsNullOrEmpty(body))
            {
                _service.AddAnswer(new AddAnswerDTO()
                {
                    Body = body,
                    CreationDate = DateTime.Now,
                    UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString()),
                    QuestionId = id,
                });
            }
            return RedirectToAction("ShowQuestion", new { id = id });
        }
        [Authorize]
        [Route("/Forum/ShowQustion")]
        public IActionResult SelectIsTrueAnswer(int questionId, int answerId)
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var question = _service.ShowQuestion(questionId);
            if (question.Question.UserId == currentUserId)
            {
                _service.ChangeIsTrueAnswer(questionId, answerId);
            }
            return RedirectToAction("ShowQuestion", new { id = questionId });
        }
    }
}
