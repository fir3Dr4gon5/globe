using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using globe_webapi;

namespace globe_webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class  CpuTempController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CpuTempController> _logger;

        public CpuTempController(ILogger<CpuTempController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<CpuTemp> Get()
        {
	    var tempWorker = new TempWorker();
	    CpuTemp cpu_temp = tempWorker.GetTempFromProcess();
	    
	    var list = new List<CpuTemp>();
	    list.Add(cpu_temp);
	    
	    return list.ToArray();

        }
    }
}
