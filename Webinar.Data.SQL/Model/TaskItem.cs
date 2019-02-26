using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Webinar.Data.SQL.Model
{
    public class TaskItem : SQLEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid TaskListId { get; set; }

        [ForeignKey(nameof(TaskListId)), JsonIgnore]
        public virtual TaskList List { get; set; }
    }
}
