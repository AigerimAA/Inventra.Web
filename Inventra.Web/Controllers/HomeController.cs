using Microsoft.AspNetCore.Mvc;
using MediatR;
using Inventra.Application.Inventories.Queries.GetLatestInventories;
using Inventra.Application.Inventories.Queries.GetPopularInventories;
using Inventra.Web.Models;

namespace Inventra.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                LatestInventories = await _mediator.Send(new GetLatestInventoriesQuery(10)),
                PopularInventories = await _mediator.Send(new GetPopularInventoriesQuery(5))
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
