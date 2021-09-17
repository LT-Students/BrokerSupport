using BrokerSupport.Right.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Models.Broker.Requests.Rights;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Right
{
  public class ChangeUserRole<T> : IChangeUserRole<T> where T : class
  {
    private readonly ILogger<ChangeUserRole<T>> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRequestClient<IChangeUserRoleRequest> _requestClient;

    public ChangeUserRole(
      ILogger<ChangeUserRole<T>> logger,
      IHttpContextAccessor httpContextAccessor,
      IRequestClient<IChangeUserRoleRequest> requestClient)
    {
      _logger = logger;
      _httpContextAccessor = httpContextAccessor;
      _requestClient = requestClient;
    }

    public async Task<bool> ExecuteAsync(
      List<string> errors,
      Guid roleId,
      Guid userId)
    {
      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<bool>>(
          IChangeUserRoleRequest.CreateObj(
            roleId,
            userId,
            _httpContextAccessor.HttpContext.GetUserId()));

        if (response.Message.IsSuccess && response.Message.Body)
        {
          return true;
        }

        _logger.LogWarning(
          "Errors while assign role '{RoleId}' to the user with id '{UserId}.\n Reason: {Errors}",
          roleId,
          userId,
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(
          exc,
          "Сan't assign role '{RoleId}' to the user with id '{UserId}'",
          roleId,
          userId);
      }

      errors.Add("Сan't assign role to user. Please try again later.");

      return false;
    }
  }
}
