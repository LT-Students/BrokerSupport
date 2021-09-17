using BrokerSupport.Project.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Requests.Project;
using LT.DigitalOffice.Models.Broker.Responses.Search;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Project
{
  public class SearchProjects<T> : ISearchProjects<T> where T : class
  {
    private readonly ILogger<SearchProjects<T>> _logger;
    private readonly IRequestClient<ISearchProjectsRequest> _requestClient;

    public SearchProjects(
      ILogger<SearchProjects<T>> logger,
      IRequestClient<ISearchProjectsRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<List<SearchInfo>> ExecuteAsync(List<string> errors, string value)
    {
      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<ISearchResponse>>(
          ISearchProjectsRequest.CreateObj(value));

        if (response.Message.IsSuccess && response.Message.Body != null)
        {
          return response.Message.Body.Entities;
        }

        _logger.LogWarning(
          "Errors while search projects.\n Reason {Errors}",
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, "Сan't search projects.");
      }

      errors.Add("Сan't search projects. Please try again later.");

      return null;
    }
  }
}
