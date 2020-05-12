using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using com.b_velop.Deploy_O_Mat.Web.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerService
{
    public class List
    {
        public class Query : IRequest<List<ListDockerServiceDto>> { }

        public class Handler : IRequestHandler<Query, List<ListDockerServiceDto>>
        {
            private readonly IMapper _mapper;
            private readonly IDockerServiceService _service;

            public Handler(
                IMapper mapper,
                IDockerServiceService service)
            {
                _mapper = mapper;
                _service = service;
            }

            public async Task<List<ListDockerServiceDto>> Handle(
                Query request,
                CancellationToken cancellationToken)
            {
                var dockerServices = await _service.Get(cancellationToken);
                var result = _mapper.Map<List<ListDockerServiceDto>>(dockerServices);
                return result;
            }
        }
    }
}