using BrokerSupport.Extension;
using BrokerSupport.Message.Interfaces;
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
  public class WorkspaceCreator : IWorkspaceCreator
  {
    private readonly ILogger<WorkspaceCreator> _logger;
    private readonly HttpContext _httpContext;
    private readonly IRequestClient<ICreateWorkspaceRequest> _requestClient;

    public WorkspaceCreator(
      ILogger<WorkspaceCreator> logger,
      IHttpContextAccessor httpContextAccessor,
      IRequestClient<ICreateWorkspaceRequest> requestClient)
    {
      _logger = logger;
      _httpContext = httpContextAccessor?.HttpContext;
      _requestClient = requestClient;
    }

    public async Task<bool> ExecuteAsync(
      List<string> errors,
      string name,
      List<Guid> usersIds)
    {
      return await _requestClient.ProcessRequest<ICreateWorkspaceRequest, bool>(
        ICreateWorkspaceRequest.CreateObj(
          name,
          _httpContext?.GetUserId() ?? Guid.Empty,
          usersIds),
        errors,
        _logger);
    }
  }
}
