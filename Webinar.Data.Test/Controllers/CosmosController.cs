using Cosmonaut;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Webinar.Data.Cosmos.Model;

namespace Webinar.Data.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CosmosController : ControllerBase
    {
        private readonly ICosmosStore<TaskList> _tasksCosmosStore;

        public CosmosController(ICosmosStore<TaskList> tasksCosmosStore)
        {
            _tasksCosmosStore = tasksCosmosStore;
        }

        [HttpGet("List")]
        public async Task<IEnumerable<TaskList>> List()
        {
            var list = await _tasksCosmosStore.Query().ToListAsync();

            return list;
        }

        [HttpPost("CreateList")]
        public async Task<ActionResult<string>> CreateList(string title)
        {
            var taskList = new TaskList
            {
                Title = title
            };

            var result = await _tasksCosmosStore.UpsertAsync(taskList);

            return result.Entity.Id;
        }

        [HttpPost("AddTaskItem")]
        public async Task<ActionResult<string>> AddTaskItem(string listId, string taskName)
        {
            var taskList = await _tasksCosmosStore.FindAsync(listId);

            taskList.Items.Add(new TaskItem { Name = taskName });

            var result = await _tasksCosmosStore.UpsertAsync(taskList);

            return result.Entity.Id;
        }
    }
}