using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Webinar.Data.AzureTable.Model
{
    public class TaskItem : AzureTableEntity
    {
        public string Name { get; set; }

        public Guid TaskListId { get; set; }

        public TaskItem() : base()
        {
            this.PartitionKey = nameof(TaskItem);
        }
    }
}