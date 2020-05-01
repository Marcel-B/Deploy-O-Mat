﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Interfaces
{
    public interface IDockerServiceService
    {
        Task RemoveDockerService(Guid id, CancellationToken cancellationToken = default);
        Task CreateDockerService(Guid id, CancellationToken cancellationToken = default);
        Task<IServiceResponse> UpdateDockerService(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Domain.Models.DockerService>> Get(CancellationToken cancellationToken = default);
    }
}