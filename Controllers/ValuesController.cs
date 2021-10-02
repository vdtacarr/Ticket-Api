using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaYazılım.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IProcess _process;

        public ValuesController(IProcess process)
        {
            _process = process;
        }

        [HttpPost("post")]
        public IActionResult post([FromBody] Base data)
        {
            var result = _process.DoEverything(data);
            return Ok(result);

        }

        [HttpGet("get")]
        public IActionResult get()
        {
         
            return Ok("başarılı");

        }


    }
}
