using BrokerSupport.Company.Interfaces;
using BrokerSupport.Extension;
using LT.DigitalOffice.Models.Broker.Requests.Company;
using LT.DigitalOffice.Models.Broker.Responses.Search;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company
{
  public class DepartmentsSearcher : IDepartmentsSearcher
  {
    private readonly ILogger<DepartmentsSearcher> _logger;
    private readonly IRequestClient<ISearchDepartmentsRequest> _requestClient;

    public DepartmentsSearcher(
      ILogger<DepartmentsSearcher> logger,
      IRequestClient<ISearchDepartmentsRequest> requestClient)
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

      return await _requestClient.ProcessRequest<ISearchDepartmentsRequest, ISearchResponse>(
        ISearchDepartmentsRequest.CreateObj(
          value),
        errors,
        _logger);
    }
  }
}
