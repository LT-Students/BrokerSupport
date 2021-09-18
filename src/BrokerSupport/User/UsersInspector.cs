using BrokerSupport.Extension;
using BrokerSupport.User.Interfaces;
using LT.DigitalOffice.Models.Broker.Common;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerSupport.User
{
  public class UsersInspector : IUsersInspector
  {
    private readonly ILogger<UsersInspector> _logger;
    private readonly IRequestClient<ICheckUsersExistence> _requestClient;

    public UsersInspector(
      ILogger<UsersInspector> logger,
      IRequestClient<ICheckUsersExistence> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<ICheckUsersExistence> ExecuteAsync(
      List<string> errors,
      List<Guid> usersIds)
    {
      if (usersIds == null || !usersIds.Any())
      {
        return null;
      }

      return await _requestClient.ProcessRequest<ICheckUsersExistence, ICheckUsersExistence>(
        ICheckUsersExistence.CreateObj(
          usersIds),
        errors,
        _logger);
    }
  }
}
