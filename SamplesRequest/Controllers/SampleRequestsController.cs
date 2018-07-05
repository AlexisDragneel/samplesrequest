using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SamplesRequest.Services;

namespace SamplesRequest.Controllers
{
    public class SampleRequestsController : Controller
    {
        private readonly ISampleRequestServiceController _requestService;

        public SampleRequestsController(ISampleRequestServiceController requestService)
        {
            
            _requestService = requestService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "Index";
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Create";
            return View();
        }

        [HttpGet]
        public IActionResult ReviewAndApprove()
        {
            ViewData["Title"] = "Review and Approve";
            return View();
        }
    }
}