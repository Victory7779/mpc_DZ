using Microsoft.AspNetCore.Mvc;
using mpc_DZ.Model;
using mpc_DZ.Services;

namespace mpc_DZ.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomersService? _customersService;
        public CustomersController(ICustomersService? customersService)
        {
            _customersService = customersService;
        }
        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var customers = await _customersService.Read();
            return View(customers);
        }
        [HttpGet]
        public async Task<ViewResult> Details(int id)
        {
            var customer = await _customersService.Details(id);
            return View(customer);
        }

        [HttpGet]
        public ViewResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id", "FirstName", "LastName", "Email")]Customer customer)
        {
            await _customersService.Create(customer);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public ActionResult Edit() => View();
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "FirstName", "LastName", "Email")] Customer customer)
        {
            await _customersService.Edit(id, customer);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Delete() => View();
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _customersService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
