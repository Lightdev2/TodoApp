using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Core.DTOs;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/stats")]
    public class StatisticsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GetStatsInPeriod(StatsRequest req)
        {
            var result = new StatsResponse();
            result.Stats = new Dictionary<string, int>();
            result.Stats.Add("January", 21);
            return Ok(req);
        }
    }

    public class StatsRequest
    {
        public string Type { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
