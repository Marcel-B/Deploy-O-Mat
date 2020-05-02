using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Data.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Deploy_O_Mat.Web.Application.DockerStack
{
    public class List
    {
        public class Query : IRequest<ActionResult<IEnumerable<com.b_velop.Deploy_O_Mat.Web.Domain.Models.DockerStack>>>
        {

        }

        public class Handler : IRequestHandler<Query, ActionResult<IEnumerable<com.b_velop.Deploy_O_Mat.Web.Domain.Models.DockerStack>>>
        {
            private readonly IDeployOMatWebRepository _repo;
            public Handler(IDeployOMatWebRepository repo)
            {
                _repo = repo;
            }

            public async Task<ActionResult<IEnumerable<com.b_velop.Deploy_O_Mat.Web.Domain.Models.DockerStack>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _repo.GetDockerStacks();
                if (result == null)
                    return new NoContentResult();
                return new OkObjectResult(result);
            }
        }
    }
}
