using Inventra.Application.Tags.Queries.GetTagsByPrefix;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.Web.Controllers
{
    public class TagController : Controller
    {
        private readonly IMediator _mediator;
        public TagController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Json(new List<string>());
            var tags = await _mediator.Send(new GetTagsByPrefixQuery(query));
            return Json(tags);
        }
    }
}
