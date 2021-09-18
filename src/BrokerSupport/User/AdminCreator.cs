using BrokerSupport.Extension;
using BrokerSupport.User.Interfaces;
using LT.DigitalOffice.Models.Broker.Requests.User;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.User
{
  public class AdminCreator : IAdminCreator
  {
    private readonly ILogger<AdminCreator> _logger;
    private readonly IRequestClient<ICreateAdminRequest> _requestClient;

    public AdminCreator(
      ILogger<AdminCreator> logger,
      IRequestClient<ICreateAdminRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<bool> ExecuteAsync(
      List<string> errors,
      string firstName,
      string middleName,
      string lastName,
      string email,
      string login,
      string password)
    {
      return await _requestClient.ProcessRequest<ICreateAdminRequest, bool>(
        ICreateAdminRequest.CreateObj(
          firstName,
          middleName,
          lastName,
          email,
          login,
          password));
    }
  }
}
