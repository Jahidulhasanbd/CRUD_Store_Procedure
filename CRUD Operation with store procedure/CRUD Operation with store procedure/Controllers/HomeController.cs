using CRUD_Operation_with_store_procedure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CRUD_Operation_with_store_procedure.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TestDbContext context;

        public HomeController(ILogger<HomeController> logger,TestDbContext context)
        {
            _logger = logger;
            this.context= context;
        }

        public IActionResult Index()
        {
            List<Customer> customers = this.context.SearchCustomer("").ToList();
            return View(customers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}