using Microsoft.AspNetCore.Mvc;
using open_closed_poc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace open_closed_poc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsListController : ControllerBase
    {
        [HttpGet]
        public IActionResult CampaignsList()
        {
            return Ok(CampaignService.GetCampaignsListDTO());
        }

    }
}
