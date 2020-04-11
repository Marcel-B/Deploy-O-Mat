using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Domain.Models;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Application.DockerStackServices
{
    public class List
    {
        public class Query : IRequest<IEnumerable<DockerStackService>> { }

        public class Handler : IRequestHandler<Query, IEnumerable<DockerStackService>>
        {
            private readonly IRepositoryWrapper _repository;

            public Handler(
                IRepositoryWrapper repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<DockerStackService>> Handle(
                Query request,
                CancellationToken cancellationToken)
                => await _repository.DockerStackServices.SelectAllAsync(cancellationToken);
        }
    }
}
