using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Webinar.Data.SQL.Model
{
    public class TaskList : SQLEntity
    {
        [Required]
        public string Title { get; set; }

        [InverseProperty(nameof(TaskItem.List))]
        public virtual ICollection<TaskItem> Items { get; set; }

        public TaskList()
        {
            Items = new List<TaskItem>();
        }
    }
}
