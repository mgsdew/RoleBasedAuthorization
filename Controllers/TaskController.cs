using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using RoleBasedAuthorization.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace RoleBasedAuthorization.Controllers
{
    //[Produces("application/json")]
    [Route("api/Task/")]
    public class TaskController : Controller
    {
        public TaskController(MyDbContext context)
        {
            _context = context;
        }

        private readonly MyDbContext _context;

        [HttpPost, Route("CreateTask")]
        public ActionResult Create([FromBody] IEnumerable<Models.Task> tasksList)
        {
            if (ModelState.IsValid)
            {
                foreach (var tasks in tasksList)
                {
                    var task = new Models.Task();
                    task.WorkOrder = tasks.WorkOrder;
                    task.WorkType = tasks.WorkType;
                    task.Client = tasks.Client;
                    task.ClientDueDate = tasks.ClientDueDate;

                    _context.Add(task);
                    _context.SaveChangesAsync();
                }

                return Json(new { status = true, message = "Task Upload Successfully" });
            }
            else
            {
                return Json(new { status = true, message = "Problem Occurs During Uploading..." });
            }
        }

        //[HttpPost, Route("UpdateTaskStatus")]
        //public async Task<IActionResult> UpdateStatus([FromBody] int taskId)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Models.Task tasks = await _context.Task.Update(s => s. == admins.Id).FirstOrDefaultAsync();
        //        admin.FullName = admins.FullName;
        //        admin.Email = admins.Email;
        //        admin.RolesId = admins.RolesId;
        //        await _context.SaveChangesAsync();

        //        return Json(new { status = true, message = "Task Upload Successfully" });
        //    }
        //    else
        //    {
        //        return Json(new { status = true, message = "Problem Occurs During Uploading..." });
        //    }
        //}

        [HttpGet, Route("TaskList")]
        public async Task<IEnumerable<Models.Task>> TaskList()
        {
            var list =  await _context.Task.ToListAsync();
            return list;
        }

    }
}