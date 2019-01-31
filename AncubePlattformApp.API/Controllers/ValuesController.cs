using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AncubePlattformApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AncubePlattformApp.API.Controllers
{
    // localhost:5000/api/values
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext context;

        public ValuesController(DataContext context)
        {
            this.context = context;
        }

        // 1 Request == 1 Action == 1 Thread aus ThreadPool
        //      es existieren 2 versch. Thread 1. WorkerThreads 2. IOCP-Threads
        // synchronous: 1 thread / action does i/0 blocking (aka waiting for other-programms / 
        //      os-files / db or similar) -> thread will wait till result is present
        // asynchronous: 1 thread / action does i/0 blocking (aka waiting for other-programms / 
        //      os-files / db or similar) -> thread is released and (another) thread
        //      continues the work, if the result is present
        // How? Combination of async,Task<> and await



        // GET api/values
        // // IActionResult allows HTTP-responses like ok
        public async Task<IActionResult> GetValues()
        {
            var values = await context.Values.ToListAsync();
            return Ok(values);
        }

        // Synchronous-version
        
        // public IActionResult GetValues()
        // {
        //     var values = context.Values.ToList();
        //     return Ok(values);
        // }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            // FirstOrDefault returnes default (and no Exception like in first) if no value got found
            var value = await context.Values.FirstOrDefaultAsync(x=>x.Id==id);
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
