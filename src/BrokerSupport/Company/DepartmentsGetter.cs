using BrokerSupport.Company.Interfaces;
using BrokerSupport.Extension;
using LT.DigitalOffice.Models.Broker.Requests.Company;
using LT.DigitalOffice.Models.Broker.Responses.Company;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company
{
  public class DepartmentsGetter : IDepartmentsGetter
  {
    private readonly ILogger<DepartmentsGetter> _logger;
    private readonly IRequestClient<IGetDepartmentsRequest> _requestClient;

    public DepartmentsGetter(
      ILogger<DepartmentsGetter> logger,
      IRequestClient<IGetDepartmentsRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<IGetDepartmentsResponse> ExecuteAsync(
      List<string> errors,
      List<Guid> departmentsIds = null)
    {
      return await _requestClient.ProcessRequest<IGetDepartmentsRequest, IGetDepartmentsResponse>(
        IGetDepartmentsRequest.CreateObj(
          departmentsIds),
        errors,
        _logger);
    }
  }
}
