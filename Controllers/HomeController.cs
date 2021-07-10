using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using RoleBasedAuthorization.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RoleBasedAuthorization.Controllers
{
    public class HomeController : Controller
    {
        //MyDbContext _context = new MyDbContext();

        public HomeController(MyDbContext context)
        {
            _context = context;
        }
        private IHostingEnvironment _hostingEnvironment;
        private readonly MyDbContext _context;

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return Redirect("/Account/Login");
            }

            return View();
        }

        public IActionResult CreateTask()
        {
            return View();
        }

        public IActionResult OperateTask()
        {
            return View();
        }
    }
}