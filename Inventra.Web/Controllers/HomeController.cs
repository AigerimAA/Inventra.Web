using Microsoft.AspNetCore.Mvc;
using MediatR;
using Inventra.Application.Inventories.Queries.GetLatestInventories;
using Inventra.Application.Inventories.Queries.GetPopularInventories;

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
            var latest = await _mediator.Send(new GetLatestInventoriesQuery(10));
            var popular = await _mediator.Send(new GetPopularInventoriesQuery(5));

            ViewBag.LatestInventories = latest;
            ViewBag.PopularInventories = popular;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
