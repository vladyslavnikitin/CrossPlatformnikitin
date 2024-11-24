using Lab5.Library;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.WebApp.Controllers
{
    public class LabsController : Controller
    {
        private readonly LabRunner _labRunner = new LabRunner();

        public IActionResult Lab1()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Lab1(string inputFile)
        {
            var result = _labRunner.RunLab1(inputFile);
            ViewBag.Result = result;
            return View();
        }

        public IActionResult Lab2()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Lab2(string inputFile)
        {
            var result = _labRunner.RunLab2(inputFile);
            ViewBag.Result = result;
            return View();
        }

        public IActionResult Lab3()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Lab3(string inputFile)
        {
            var result = _labRunner.RunLab3(inputFile);
            ViewBag.Result = result;
            return View();
        }
    }
}
