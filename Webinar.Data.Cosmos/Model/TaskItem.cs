using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Webinar.Data.Cosmos.Model
{
    public class TaskItem : CosmosEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
