using BrokerSupport.Extension;
using BrokerSupport.Right.Interfaces;
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
  public class UserRoleEditor : IUserRoleEditor
  {
    private readonly ILogger<UserRoleEditor> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRequestClient<IChangeUserRoleRequest> _requestClient;

    public UserRoleEditor(
      ILogger<UserRoleEditor> logger,
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
      return await _requestClient.ProcessRequest<IChangeUserRoleRequest, bool>(
        IChangeUserRoleRequest.CreateObj(
          roleId,
          userId,
          _httpContextAccessor.HttpContext.GetUserId()),
        errors,
        _logger);
    }
  }
}
