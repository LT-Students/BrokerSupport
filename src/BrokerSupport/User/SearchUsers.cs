using BrokerSupport.User.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Requests.User;
using LT.DigitalOffice.Models.Broker.Responses.Search;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.User
{
  public class SearchUsers<T> : ISearchUsers<T> where T : class
  {
    private readonly ILogger<SearchUsers<T>> _logger;
    private readonly IRequestClient<ISearchUsersRequest> _requestClient;

    public SearchUsers(
      ILogger<SearchUsers<T>> logger,
      IRequestClient<ISearchUsersRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<List<SearchInfo>> ExecuteAsync(
      List<string> errors,
      string value)
    {
      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<ISearchResponse>>(
          ISearchUsersRequest.CreateObj(value));

        if (response.Message.IsSuccess && response.Message.Body != null)
        {
          return response.Message.Body.Entities;
        }

        _logger.LogWarning(
          "Errors while search users.\n Reason: {Errors}",
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, "Сan't search users.");

      }

      errors.Add("Сan't search users. Please try again later.");

      return null;
    }
  }
}
