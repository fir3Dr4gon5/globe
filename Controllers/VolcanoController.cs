using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

public class Volcano {
    public long key { get;set; }
    public string  name { get; set; }
    public int year { get; set; }
    public int month { get; set; }
    public int day { get; set; }
    public int damage { get; set; }
    public decimal lat { get; set; }
    public decimal lon { get; set; }

}

    [ApiController]
    [Route("api/[controller]")]
    public class  VolcanoController : ControllerBase
    {
        private readonly ILogger<VolcanoController> _logger;
	private readonly IHttpClientFactory _httpClientFactory;
	    


        public VolcanoController(
	    ILogger<VolcanoController> logger,
	    IHttpClientFactory httpClientFactory
        )
        {
            _logger = logger;
	    _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
	    var client = _httpClientFactory.CreateClient();
	    
	    // cheating. Just drop a json file on serer for now. 
	    // Later we will part csv values into json here
            var json = await client
                          .GetStringAsync("http://192.168.2.31/data/volcano.json");
	    var volcanos = JsonConvert.DeserializeObject<List<Volcano>>(json);
		    
	    return Ok(volcanos);	
        }
    }
    