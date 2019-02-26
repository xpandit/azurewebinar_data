using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webinar.Data.Cosmos.DataContext;
using Webinar.Data.Cosmos.Model;

namespace Webinar.Data.Test.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class CosmosController : ControllerBase
    {
        private readonly CosmosDataContext _cosmosDataContext;

        public CosmosController(CosmosDataContext cosmosDataContext)
        {
            _cosmosDataContext = cosmosDataContext;
        }

        [HttpGet]
        public async Task<IEnumerable<TaskList>> List()
        {
            await _cosmosDataContext.Database.EnsureCreatedAsync();

            var list = _cosmosDataContext.TaskLists.Include(tl => tl.Items).ToList();

            return list;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateList(string title)
        {
            await _cosmosDataContext.Database.EnsureCreatedAsync();

            var taskList = new TaskList
            {
                Title = title
            };

            _cosmosDataContext.TaskLists.Add(taskList);
            var result = await _cosmosDataContext.SaveChangesAsync();

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddTaskItem(string listId, string taskName)
        {
            await _cosmosDataContext.Database.EnsureCreatedAsync();

            var taskList = _cosmosDataContext.TaskLists.FirstOrDefault(tl => tl.Id == listId);

            taskList.Items.Add(new TaskItem { Name = taskName });

            _cosmosDataContext.TaskLists.Update(taskList);
            var result = await _cosmosDataContext.SaveChangesAsync();

            return result;
        }
    }
}