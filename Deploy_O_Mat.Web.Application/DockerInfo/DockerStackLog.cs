using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Interfaces;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerInfo
{
    public class DockerStackLog
    {
        public class Query : IRequest<IEnumerable<Domain.Models.DockerStackLog>> { }

        public class Handler : IRequestHandler<Query, IEnumerable<Domain.Models.DockerStackLog>>
        {
            private readonly IDeployOMatWebRepository _rep;

            public Handler(
                IDeployOMatWebRepository rep)
            {
                _rep = rep;
            }

            public async Task<IEnumerable<Domain.Models.DockerStackLog>> Handle(
                Query request,
                CancellationToken cancellationToken)
                => await _rep.GetDockerStackLogs();
        }
    }
}
