using BrokerSupport.Extension;
using BrokerSupport.Right.Interfaces;
using LT.DigitalOffice.Models.Broker.Requests.Rights;
using LT.DigitalOffice.Models.Broker.Responses.Rights;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerSupport.Right
{
  public class UsersRolesGetter : IUsersRolesGetter
  {
    private readonly ILogger<UsersRolesGetter> _logger;
    private readonly IRequestClient<IGetUserRolesRequest> _requestClient;

    public UsersRolesGetter(
      ILogger<UsersRolesGetter> logger,
      IRequestClient<IGetUserRolesRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<IGetUserRolesResponse> ExecuteAsync(
      List<string> errors,
      List<Guid> usersIds)
    {
      if (usersIds == null || !usersIds.Any() || usersIds.Contains(Guid.Empty))
      {
        return null;
      }

      return await _requestClient.ProcessRequest<IGetUserRolesRequest, IGetUserRolesResponse>(
        IGetUserRolesRequest.CreateObj(
          usersIds),
        errors,
        _logger);
    }
  }
}
