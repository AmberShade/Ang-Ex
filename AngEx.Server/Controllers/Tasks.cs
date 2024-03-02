using Microsoft.AspNetCore.Mvc;
using System.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngEx.Server.Controllers
{
    [Route("tasks")]
    [ApiController]
    public class Tasks : ControllerBase
    {
        public Tasks()
        {
            //AppSettingsReader reader= new AppSettingsReader();
            //_dbContext = new DbContext((string)reader.GetValue("DefaultConnection", typeof(String)));//I really hope this should work
            //_dbContext = HttpContext.RequestServices.GetService(typeof(DbContext)) as DbContext;
        }
        // GET: api/<TaskController>
        [HttpGet]
        public IEnumerable<Task> Get()
        {
            DbContext dbContext = HttpContext.RequestServices.GetService(typeof(DbContext)) as DbContext;
            return dbContext.GetTasks();
        }

        // GET api/<TaskController>/5
        [HttpGet("{id}")]
        public Task Get(int id)
        {
            DbContext dbContext = HttpContext.RequestServices.GetService(typeof(DbContext)) as DbContext;
            return dbContext.GetTask(id);
        }

        // POST api/<TaskController>
        [HttpPost]
        public void Post([FromBody] Task value)
        {
            DbContext dbContext = HttpContext.RequestServices.GetService(typeof(DbContext)) as DbContext;
            dbContext.AddTask(value);
        }

        // PUT api/<TaskController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Task value)
        {
            DbContext dbContext = HttpContext.RequestServices.GetService(typeof(DbContext)) as DbContext;
            dbContext.EditTask(id,value);
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            DbContext dbContext = HttpContext.RequestServices.GetService(typeof(DbContext)) as DbContext;
            dbContext.DeleteTask(id);
        }
    }
}
