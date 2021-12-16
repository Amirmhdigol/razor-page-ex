using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace razor_page_ex.Areas.Adminstration.Controllers
{
    [Area("Adminstration")]
    [Authorize(Policy = "AdminPolicy")]
    public class AdminBase : Controller
    {

    }
}
