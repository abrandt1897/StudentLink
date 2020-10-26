using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication_mc_02.Models.DTO;

namespace WebApplication_mc_02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        [HttpGet("{user}")]
        public ActionResult<List<dynamic>> Get(int user)
        {
            return SQLConnection.get(typeof(Notifications), $"WHERE StudentID={user}");
        }
    }
}
