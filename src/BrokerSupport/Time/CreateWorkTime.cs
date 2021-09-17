using BrokerSupport.Time.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Requests.Time;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerSupport.Time
{
  public class CreateWorkTime<T> : ICreateWorkTime<T> where T : class
  {
    private readonly ILogger<CreateWorkTime<T>> _logger;
    private readonly IRequestClient<ICreateWorkTimeRequest> _requestClient;

    public CreateWorkTime(
      ILogger<CreateWorkTime<T>> logger,
      IRequestClient<ICreateWorkTimeRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<bool> ExecuteAsync(
      List<string> errors,
      Guid projectId,
      List<Guid> usersIds)
    {
      if (usersIds == null || !usersIds.Any())
      {
        return false;
      }

      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<bool>>(
            ICreateWorkTimeRequest.CreateObj(projectId, usersIds));

        if (response.Message.IsSuccess && response.Message.Body)
        {
          return true;
        }

        _logger.LogWarning(
          "Errors while creating work time for project id '{ProjectId}' with users ids: {UsersIds}.\n Reason: {Errors}",
          projectId,
          string.Join(", ", usersIds));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, "Сan't creating work time for project id '{ProjectId}' with users ids: {UsersIds}.",
          projectId,
          string.Join(", ", usersIds));
      }

      errors.Add("Сan't create the project work time. Please try again later.");

      return false;
    }
  }
}
