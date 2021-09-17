using BrokerSupport.Extension;
using BrokerSupport.Project.Interfaces;
using LT.DigitalOffice.Models.Broker.Requests.Project;
using LT.DigitalOffice.Models.Broker.Responses.Project;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Project
{
  public class ProjectsGetter : IProjectsGetter
  {
    private readonly ILogger<ProjectsGetter> _logger;
    private readonly IRequestClient<IGetProjectsRequest> _requestClient;

    public ProjectsGetter(
      ILogger<ProjectsGetter> logger,
      IRequestClient<IGetProjectsRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<IGetProjectsResponse> ExecuteAsync(
      List<string> errors,
      List<Guid> projectsIds = null,
      Guid? departmentId = null,
      Guid? userId = null,
      bool includeUsers = false,
      int? skipCount = null,
      int? takeCount = null)
    {
      if (projectsIds == null && departmentId == null && userId == null)
      {
        return null;
      }

      return await _requestClient.ProcessRequest<IGetProjectsRequest, IGetProjectsResponse>(
        IGetProjectsRequest.CreateObj(
          projectsIds,
          departmentId,
          userId,
          includeUsers,
          skipCount,
          takeCount),
        errors,
        _logger);
    }
  }
}
