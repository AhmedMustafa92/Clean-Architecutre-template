using WhatsBack.Application.Interfaces.Repositories;
using AutoMapper;
using WhatsBack.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WhatsBack.SharedKernal.Wrappers;
using WhatsBack.Application.Interfaces;
using WhatsBack.SharedKernal.ResourcesReader.Messages;

namespace WhatsBack.Application.UseCases.Products.Commands.CreateProduct
{
    public partial class CreateProductCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
    }
    public class CreateProductCommandHandler : BaseHandler<CreateProductCommand, int>
    {
        public  IProductRepository ProductRepository { get; set; } 
        public override async Task<Response<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product =Mapper.Map<Product>(request);
            await ProductRepository.InsertAsync(product);
            await UnitOfWork.Commit();
            return new Response<int>(product.Id);
        }
    }
}
