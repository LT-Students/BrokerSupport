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
  public class DepartmentUsersGetter : IDepartmentUsersGetter
  {
    private readonly ILogger<DepartmentUsersGetter> _logger;
    private readonly IRequestClient<IGetDepartmentUsersRequest> _requestClient;

    public DepartmentUsersGetter(
      ILogger<DepartmentUsersGetter> logger,
      IRequestClient<IGetDepartmentUsersRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<IGetDepartmentUsersResponse> ExecuteAsync(
      List<string> errors,
      Guid departmentId,
      int? skipCount = null,
      int? takeCount = null,
      DateTime? byEntryDate = null)
    {
      return await _requestClient.ProcessRequest<IGetDepartmentUsersRequest, IGetDepartmentUsersResponse>(
        IGetDepartmentUsersRequest.CreateObj(
          departmentId,
          skipCount,
          takeCount,
          byEntryDate),
        errors,
        _logger);
    }
  }
}
