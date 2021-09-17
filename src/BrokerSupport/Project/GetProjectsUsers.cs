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
  public class GetProjectsUsers<T> : IGetProjectsUsers<T> where T : class
  {
    private readonly ILogger<GetProjectsUsers<T>> _logger;
    private readonly IRequestClient<IGetProjectsUsersRequest> _requestClient;

    public GetProjectsUsers(
      ILogger<GetProjectsUsers<T>> logger,
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

      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<IGetProjectsUsersResponse>>(
          IGetProjectsUsersRequest.CreateObj(
            usersIds,
            projectsIds,
            skipCount,
            takeCount,
            includeDisactiveted));

        if (response.Message.IsSuccess && response.Message.Body != null)
        {
          return response.Message.Body;
        }

        if (usersIds != null)
        {
          _logger.LogWarning(
            "Errors while getting projects users by users ids: {UsersIds}.\n Reason:{Errors}",
            string.Join(", ", usersIds),
            string.Join('\n', response.Message.Errors));
        }

        if (projectsIds != null)
        {
          _logger.LogWarning(
            "Errors while getting projects users by projects ids: {ProjectsIds}.\n Reason:{Errors}",
            string.Join(", ", projectsIds),
            string.Join('\n', response.Message.Errors));
        }
      }
      catch (Exception exc)
      {
        if (usersIds != null)
        {
          _logger.LogError(
            exc,
            "Сan't get projects users by users ids: {UsersIds}.",
            string.Join(", ", usersIds));
        }

        if (projectsIds != null)
        {
          _logger.LogError(
            exc,
            "Сan't get projects users by projects ids: {ProjectsIds}.",
            string.Join(", ", projectsIds));
        }
      }

      errors.Add("Сan't get projects users. Please try again later.");

      return null;
    }
  }
}
