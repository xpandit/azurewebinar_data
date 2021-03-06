﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webinar.Data.SQL.DataContext;
using Webinar.Data.SQL.Model;

namespace Webinar.Data.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SQLController : ControllerBase
    {
        private readonly SQLDataContext _sqlDataContext;

        public SQLController(SQLDataContext sqlDataContext)
        {
            _sqlDataContext = sqlDataContext;
        }

        [HttpGet("List")]
        public async Task<IEnumerable<TaskList>> List()
        {
            var list = await _sqlDataContext.TaskLists.Include(tl => tl.Items).ToListAsync();

            return list;
        }

        [HttpPost("CreateList")]
        public async Task<ActionResult<int>> CreateList(string title)
        {
            var taskList = new TaskList
            {
                Title = title
            };

            _sqlDataContext.TaskLists.Add(taskList);
            var result = await _sqlDataContext.SaveChangesAsync();

            return result;
        }

        [HttpPost("AddTaskItem")]
        public async Task<ActionResult<int>> AddTaskItem(Guid listId, string taskName)
        {
            var taskList = _sqlDataContext.TaskLists.FirstOrDefault(tl => tl.Id == listId);

            taskList.Items.Add(new TaskItem { Name = taskName });

            var result = await _sqlDataContext.SaveChangesAsync();

            return result;
        }
    }
}