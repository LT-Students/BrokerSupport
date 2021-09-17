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
  public class CompanyEmployeesGetter : ICompanyEmployeesGetter
  {
    private readonly ILogger<CompanyEmployeesGetter> _logger;
    private readonly IRequestClient<IGetCompanyEmployeesRequest> _requestClient;

    public CompanyEmployeesGetter(
      ILogger<CompanyEmployeesGetter> logger,
      IRequestClient<IGetCompanyEmployeesRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<IGetCompanyEmployeesResponse> ExecuteAsync(
      List<string> errors,
      List<Guid> usersIds = null,
      bool includeDepartments = false,
      bool includePositions = false,
      bool includeOffices = false)
    {
      if (!includeDepartments && !includePositions && !includeOffices)
      {
        return null;
      }

      return await _requestClient.ProcessRequest<IGetCompanyEmployeesRequest, IGetCompanyEmployeesResponse>(
        IGetCompanyEmployeesRequest.CreateObj(
          usersIds,
          includeDepartments,
          includePositions,
          includeOffices),
        errors,
        _logger);
    }
  }
}
