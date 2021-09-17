using BrokerSupport.Company.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Requests.Company;
using LT.DigitalOffice.Models.Broker.Responses.Search;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company
{
  public class SearchDepartments<T> : ISearchDepartments<T> where T : class
  {
    private readonly ILogger<SearchDepartments<T>> _logger;
    private readonly IRequestClient<ISearchDepartmentsRequest> _requestClient;

    public SearchDepartments(
      ILogger<SearchDepartments<T>> logger,
      IRequestClient<ISearchDepartmentsRequest> requestClient)
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
          ISearchDepartmentsRequest.CreateObj(value));

        if (response.Message.IsSuccess && response.Message.Body != null)
        {
          return response.Message.Body.Entities;
        }

        _logger.LogWarning(
          "Errors while search departments.\n Reason: {Errors}",
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, "Сan't search departments.");

      }

      errors.Add("Сan't search departments. Please try again later.");

      return null;
    }
  }
}
