using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webinar.Data.Cosmos.DataContext;
using Webinar.Data.Cosmos.Model;

namespace Webinar.Data.Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CosmosController : ControllerBase
    {
        private readonly CosmosDataContext _cosmosDataContext;

        public CosmosController(CosmosDataContext cosmosDataContext)
        {
            _cosmosDataContext = cosmosDataContext;

            _cosmosDataContext.Database.EnsureCreated();
        }

        [HttpGet("List")]
        public async Task<IEnumerable<TaskList>> List()
        {
            var list = _cosmosDataContext.TaskLists.Include(tl => tl.Items).ToList();

            return list;
        }

        [HttpPost("CreateList")]
        public async Task<ActionResult<int>> CreateList(string title)
        {
            var taskList = new TaskList
            {
                Title = title
            };

            _cosmosDataContext.TaskLists.Add(taskList);
            var result = await _cosmosDataContext.SaveChangesAsync();

            return result;
        }

        [HttpPost("AddTaskItem")]
        public async Task<ActionResult<int>> AddTaskItem(string listId, string taskName)
        {
            var taskList = _cosmosDataContext.TaskLists.FirstOrDefault(tl => tl.Id == listId);

            taskList.Items.Add(new TaskItem { Name = taskName });

            _cosmosDataContext.TaskLists.Update(taskList);
            var result = await _cosmosDataContext.SaveChangesAsync();

            return result;
        }
    }
}