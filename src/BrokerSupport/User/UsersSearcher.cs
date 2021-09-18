using BrokerSupport.Extension;
using BrokerSupport.User.Interfaces;
using LT.DigitalOffice.Models.Broker.Requests.User;
using LT.DigitalOffice.Models.Broker.Responses.Search;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.User
{
  public class UsersSearcher : IUsersSearcher
  {
    private readonly ILogger<UsersSearcher> _logger;
    private readonly IRequestClient<ISearchUsersRequest> _requestClient;

    public UsersSearcher(
      ILogger<UsersSearcher> logger,
      IRequestClient<ISearchUsersRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<ISearchResponse> ExecuteAsync(
      List<string> errors,
      string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return null;
      }

      return await _requestClient.ProcessRequest<ISearchUsersRequest, ISearchResponse>(
        ISearchUsersRequest.CreateObj(
          value),
        errors,
        _logger);
    }
  }
}
