
using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.GetProductByCatagory
{
    //public record GetProductByCatagoryRequest();
    public record GetProductByCatagoryResponse(IEnumerable<Product> Products);
    public class GetProductByCatagoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category));
                var response = result.Adapt<GetProductByCatagoryResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCatagoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("GetProductByCategory")
            .WithDescription("Get Product By Category");
        }
    }
}
