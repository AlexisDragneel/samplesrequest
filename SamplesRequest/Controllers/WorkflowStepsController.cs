using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SamplesRequest.Controllers
{
    public class WorkflowStepsController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Workflow";
            return View();
        }
    }
}