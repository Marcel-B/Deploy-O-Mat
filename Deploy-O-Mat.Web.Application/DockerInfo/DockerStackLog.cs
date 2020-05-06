using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Data.Contracts;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerInfo
{
    public class DockerStackLog
    {
        public class Query : IRequest<List<Domain.Models.DockerStackLog>> { }

        public class Handler : IRequestHandler<Query, List<Domain.Models.DockerStackLog>>
        {
            private readonly IDeployOMatWebRepository _rep;

            public Handler(
                IDeployOMatWebRepository rep)
            {
                _rep = rep;
            }

            public Task<List<Domain.Models.DockerStackLog>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_rep.GetDockerStackLogs());
            }
        }
    }
}
