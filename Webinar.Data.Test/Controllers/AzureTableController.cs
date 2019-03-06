using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webinar.Data.AzureTable.Model;

namespace Webinar.Data.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureTableController : ControllerBase
    {
        private readonly CloudStorageAccount _cloudStorageAccount;
        private readonly CloudTableClient _cloudTableClient;
        private readonly CloudTable _tasksCloudTable;

        public AzureTableController(CloudStorageAccount cloudStorageAccount)
        {
            _cloudStorageAccount = cloudStorageAccount;
            _cloudTableClient = cloudStorageAccount.CreateCloudTableClient();

            _tasksCloudTable = CreateTable("Tasks").Result;
        }

        [HttpGet("List")]
        public async Task<IEnumerable<TaskList>> List()
        {
            TableContinuationToken token = null;

            var taskListTableQuery = new TableQuery<TaskList>
            {
                FilterString = $"PartitionKey eq '{nameof(TaskList)}'"
            };
            var taskLists = new List<TaskList>();

            var taskItemTableQuery = new TableQuery<TaskItem>
            {
                FilterString = $"PartitionKey eq '{nameof(TaskItem)}'"
            };
            var taskItems = new List<TaskItem>();

            do
            {
                var results = await _tasksCloudTable.ExecuteQuerySegmentedAsync(taskListTableQuery, token);
                taskLists.AddRange(results.Results);
                token = results.ContinuationToken;
            } while (token != null);

            do
            {
                var results = await _tasksCloudTable.ExecuteQuerySegmentedAsync(taskItemTableQuery, token);
                taskItems.AddRange(results.Results);
                token = results.ContinuationToken;
            } while (token != null);

            foreach (var taskList in taskLists)
            {
                taskList.Items = taskItems.Where(ti => ti.TaskListId == taskList.Id).ToList();
            }

            return taskLists;
        }

        [HttpPost("CreateList")]
        public async Task<ActionResult> CreateList(string title)
        {
            var taskList = new TaskList
            {
                Title = title
            };

            var tableOperation = TableOperation.Insert(taskList);
            var result = await _tasksCloudTable.ExecuteAsync(tableOperation);

            return Ok();
        }

        [HttpPost("AddTaskItem")]
        public async Task<ActionResult> AddTaskItem(Guid listId, string taskName)
        {
            var taskItem = new TaskItem { Name = taskName, TaskListId = listId };

            var tableOperation = TableOperation.Insert(taskItem);
            var result = await _tasksCloudTable.ExecuteAsync(tableOperation);

            return Ok();
        }

        private async Task<CloudTable> CreateTable(string tableName)
        {
            var cloudTable = _cloudTableClient.GetTableReference(tableName);

            var result = await cloudTable.CreateIfNotExistsAsync();

            return cloudTable;
        }
    }
}