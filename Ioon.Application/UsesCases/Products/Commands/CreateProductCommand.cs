using MediatR;

namespace Ioon.Application.UsesCases.Products.Commands
{
    public record CreateProductCommand(
        string BusinessUuid,
        string Name,
        decimal Price,
        int Stock,
        decimal? discount,
        string CategoryUuid,
        string ImageUrl,
        string Description,
        int Quantity
    ) : IRequest;

}
