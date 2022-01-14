using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorEx.DAL.Context;
using System.Linq;
using System.Threading.Tasks;

namespace razor_page_ex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApi : ControllerBase
    {
        private readonly RXContext _context;

        public ProductApi(RXContext context)
        {
            _context = context;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                var Terms = HttpContext.Request.Query["SearchQ"].ToString();
                var FindedTitles = _context.Posts
                    .Where(c => c.Title.Contains(Terms))
                    .Select(c => c.Title)
                    .ToList();
               return Ok(FindedTitles);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
