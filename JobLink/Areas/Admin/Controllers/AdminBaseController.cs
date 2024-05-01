using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobLink.Core.Constants.AdministratorConstants;

namespace JobLink.Areas.Admin.Controllers
{
    [Area(AdminAriaName)]
    [Authorize(Roles = AdminRole)]
    public class AdminBaseController : Controller
    {

    }
}
