using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WhatsBack.Application.Interfaces;
using WhatsBack.SharedKernal.ResourcesReader.Messages;
using WhatsBack.SharedKernal.Wrappers;

namespace WhatsBack.Application.UseCases
{
    public class BaseHandler<T,R>:IRequestHandler<T, Response<R>> where T : IRequest<Response<R>>
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IMessageResourceReader MessageResourceReader { get; set; }
        public IMapper Mapper { get; set; }
        public virtual Task<Response<R>> Handle(T request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

    }
}
