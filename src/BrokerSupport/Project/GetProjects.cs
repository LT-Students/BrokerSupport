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
  public class GetProjects<T> : IGetProjects<T> where T : class
  {
    private readonly ILogger<GetProjects<T>> _logger;
    private readonly IRequestClient<IGetProjectsRequest> _requestClient;

    public GetProjects(
      ILogger<GetProjects<T>> logger,
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

      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<IGetProjectsResponse>>(
          IGetProjectsRequest.CreateObj(
            projectsIds,
            departmentId,
            userId,
            includeUsers,
            skipCount,
            takeCount));

        if (response.Message.IsSuccess && response.Message.Body != null)
        {
          return response.Message.Body;
        }

        if (projectsIds != null)
        {
          _logger.LogWarning(
            "Errors while getting projects by ids: {ProjectsIds}.\n Reason:{Errors}",
            string.Join(", ", projectsIds),
            string.Join('\n', response.Message.Errors));
        }

        if (departmentId != null)
        {
          _logger.LogWarning(
            "Errors while getting projects by department id: {DepartmentId}.\n Reason:{Errors}",
            departmentId,
            string.Join('\n', response.Message.Errors));
        }

        if (userId != null)
        {
          _logger.LogWarning(
            "Errors while getting projects by user id '{UserId}'.\n Reason:{Errors}",
            userId,
            string.Join('\n', response.Message.Errors));
        }
      }
      catch (Exception exc)
      {
        if (projectsIds != null)
        {
          _logger.LogError(exc, "Сan't get projects by ids: {ProjectsIds}.", string.Join(", ", projectsIds));
        }

        if (departmentId != null)
        {
          _logger.LogError(exc, "Сan't get projects by department id '{DepartmentId}'.", departmentId);
        }

        if (userId != null)
        {
          _logger.LogError(exc, "Сan't get projects by user id '{UserId}'.", userId);
        }
      }

      errors.Add("Сan't get projects. Please try again later.");

      return null;
    }
  }
}
