using Application.Features.Anemic.Products.Queries;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Products;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber = PublicConstants.PageNumber, int pageSize = PublicConstants.PageSize)
        {
            var result = await Mediator.Send(new ProductGetQuery(pageNumber,pageSize));
            return Ok(result);
        }

        [HttpGet("GetDapper")]
        public async Task<IActionResult> GetDapper(ProductInputParamsViewModel productInputParamsViewModel)
        {
            var result = await Mediator.Send(new ProductDapperGetQuery(productInputParamsViewModel));
            return Ok(result);
        }
    }
}
