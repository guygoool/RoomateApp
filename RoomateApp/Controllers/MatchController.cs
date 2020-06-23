using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoomateApp.Entities;
using RoomateApp.Models;

namespace RoomateApp.Controllers
{
    [Route("[controller]")]
    public class MatchController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoomateContext _dbContext;

        public MatchController(ILogger<HomeController> logger, RoomateContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Index(int userId)
        {
            var result = new MatchViewModel();
            try
            {
                var matchResult = await _dbContext.Set<SP_FindMatch>().FromSqlRaw("exec FindMatch @userId={0}", userId).ToListAsync();
                result.Matches = matchResult.Select(c => c.ToViewModel()).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(result);
        }
    }
}