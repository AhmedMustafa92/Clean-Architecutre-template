using WhatsBack.Application.Interfaces.Repositories;
using WhatsBack.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WhatsBack.SharedKernal.Exceptions;
using WhatsBack.SharedKernal.Wrappers;

namespace WhatsBack.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<Response<Product>>
    {
        public int Id { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response<Product>>
        {
            private readonly IProductRepository _productRepository;
            public GetProductByIdQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<Response<Product>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdIfNotDeleted(query.Id);
                if (product == null) throw new ApiException($"Product Not Found.");
                return new Response<Product>(product);
            }
        }
    }
}
