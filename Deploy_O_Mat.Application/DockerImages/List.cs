using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Domain;
using com.b_velop.Deploy_O_Mat.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Deploy_O_Mat.Application.Images
{
    public class List
    {
        public class Query : IRequest<List<DockerImage>> { }

        public class Handler : IRequestHandler<Query, List<DockerImage>>
        {
            private DataContext _dataContext;

            public Handler(
                DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<List<DockerImage>> Handle(
                Query request,
                CancellationToken cancellationToken)
            {
                var dockerImages = await _dataContext.DockerImages.ToListAsync();
                return dockerImages;
            }
        }
    }
}
