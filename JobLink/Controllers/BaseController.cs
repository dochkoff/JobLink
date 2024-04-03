using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
