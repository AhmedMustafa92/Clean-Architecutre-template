using WhatsBack.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WhatsBack.SharedKernal.Exceptions;
using WhatsBack.SharedKernal.Wrappers;

namespace WhatsBack.Application.Features.Products.Commands.DeleteProductById
{
    public class DeleteProductByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, Response<int>>
        {
            private readonly IProductRepository _productRepository;
            public DeleteProductByIdCommandHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<Response<int>> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdIfNotDeleted(command.Id);
                if (product == null) throw new ApiException($"Product Not Found.");
                 _productRepository.Delete(product);
                return new Response<int>(product.Id);
            }
        }
    }
}
