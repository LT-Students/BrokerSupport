using BrokerSupport.Extension;
using BrokerSupport.Project.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Requests.Project;
using LT.DigitalOffice.Models.Broker.Responses.Project;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Project
{
  public class ProjectsUsersGetter : IProjectsUsersGetter
  {
    private readonly ILogger<ProjectsUsersGetter> _logger;
    private readonly IRequestClient<IGetProjectsUsersRequest> _requestClient;

    public ProjectsUsersGetter(
      ILogger<ProjectsUsersGetter> logger,
      IRequestClient<IGetProjectsUsersRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }
    public async Task<IGetProjectsUsersResponse> ExecuteAsync(
      List<string> errors,
      List<Guid> usersIds = null,
      List<Guid> projectsIds = null,
      int? skipCount = null,
      int? takeCount = null,
      bool includeDisactiveted = false)
    {
      if (usersIds == null && projectsIds == null)
      {
        return null;
      }

      return await _requestClient.ProcessRequest<IGetProjectsUsersRequest, IGetProjectsUsersResponse>(
        IGetProjectsUsersRequest.CreateObj(
          usersIds,
          projectsIds,
          skipCount,
          takeCount,
          includeDisactiveted),
        errors,
        _logger);
    }
  }
}
