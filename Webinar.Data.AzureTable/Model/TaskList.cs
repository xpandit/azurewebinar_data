using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;

namespace Webinar.Data.AzureTable.Model
{
    public class TaskList : AzureTableEntity
    {
        public string Title { get; set; }

        public List<TaskItem> Items { get; set; }

        public TaskList() : base()
        {
            this.PartitionKey = nameof(TaskList);
            this.Items = new List<TaskItem>();
        }
    }
}