using BrokerSupport.User.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Requests.User;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.User
{
  public class CreateAdmin<T> : ICreateAdmin<T> where T : class
  {
    private readonly ILogger<CreateAdmin<T>> _logger;
    private readonly IRequestClient<ICreateAdminRequest> _requestClient;

    public CreateAdmin(
      ILogger<CreateAdmin<T>> logger,
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
      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<bool>>(
          ICreateAdminRequest.CreateObj(
            firstName,
            middleName,
            lastName,
            email,
            login,
            password));

        if (response.Message.IsSuccess && response.Message.Body)
        {
          return true;
        }

        _logger.LogWarning(
          "Errors while creating admin of company.\n Reason: {Errors}",
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, "Сan't create admin of company.");
      }

      errors.Add("Сan't create admin of company. Please try again later.");

      return false;
    }
  }
}
