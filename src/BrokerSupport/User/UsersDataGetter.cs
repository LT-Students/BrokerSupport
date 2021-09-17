using BrokerSupport.Extension;
using BrokerSupport.User.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Requests.User;
using LT.DigitalOffice.Models.Broker.Responses.User;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerSupport.User
{
  public class UsersDataGetter : IUsersDataGetter
  {
    private readonly ILogger<UsersDataGetter> _logger;
    private readonly IRequestClient<IGetUsersDataRequest> _requestClient;

    public UsersDataGetter(
      ILogger<UsersDataGetter> logger,
      IRequestClient<IGetUsersDataRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<IGetUsersDataResponse> ExecuteAsync(
      List<string> errors,
      List<Guid> usersIds)
    {
      if (usersIds == null || !usersIds.Any())
      {
        return null;
      }

      return await _requestClient.ProcessRequest<IGetUsersDataRequest, IGetUsersDataResponse>(
        IGetUsersDataRequest.CreateObj(
          usersIds),
        errors,
        _logger);
    }
  }
}
