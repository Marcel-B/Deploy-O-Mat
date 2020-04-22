using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using com.b_velop.Deploy_O_Mat.Web.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerActiveService
{
    public class List
    {
        public class Query : IRequest<IEnumerable<DockerActiveServiceDto>> { }

        public class Handler : IRequestHandler<Query, IEnumerable<DockerActiveServiceDto>>
        {
            private readonly IMapper _mapper;
            private readonly WebContext _dockerImageService;

            public Handler(
                IMapper mapper,
                WebContext dockerImageService)
            {
                _mapper = mapper;
                _dockerImageService = dockerImageService;
            }

            public async Task<IEnumerable<DockerActiveServiceDto>> Handle(
                Query request,
                CancellationToken cancellationToken)
            {
                var images = await _dockerImageService.DockerActiveServices.ToListAsync();
                var r = _mapper.Map<IEnumerable<DockerActiveServiceDto>>(images);
                return r;
            }
        }
    }
}
