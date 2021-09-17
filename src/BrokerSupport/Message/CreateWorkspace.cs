using BrokerSupport.Message.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Models.Broker.Requests.Message;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Message
{
  public class CreateWorkspace<T> : ICreateWorkspace<T> where T : class
  {
    private readonly ILogger<CreateWorkspace<T>> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRequestClient<ICreateWorkspaceRequest> _requestClient;

    public CreateWorkspace(
      ILogger<CreateWorkspace<T>> logger,
      IHttpContextAccessor httpContextAccessor,
      IRequestClient<ICreateWorkspaceRequest> requestClient)
    {
      _logger = logger;
      _httpContextAccessor = httpContextAccessor;
      _requestClient = requestClient;
    }

    public async Task<bool> ExecuteAsync(
      List<string> errors,
      string name,
      List<Guid> usersIds)
    {
      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<bool>>(
          ICreateWorkspaceRequest.CreateObj(
            name,
            _httpContextAccessor.HttpContext.GetUserId(),
            usersIds));

        if (response.Message.IsSuccess && response.Message.Body)
        {
          return true;
        }

        _logger.LogWarning(
          "Errors while creating workspace.\n Reason: {Errors}",
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, "Сan't create workspace.");
      }

      errors.Add("Сan't create workspace. Please try again later.");

      return false;
    }
  }
}
