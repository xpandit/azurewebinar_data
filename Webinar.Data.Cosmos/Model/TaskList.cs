using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Webinar.Data.Cosmos.Model
{
    public class TaskList : CosmosEntity
    {
        [Required]
        public string Title { get; set; }

        public List<TaskItem> Items { get; set; }

        public TaskList()
        {
            Items = new List<TaskItem>();
        }
    }
}
