using BrokerSupport.Company.Interfaces;
using BrokerSupport.Extension;
using LT.DigitalOffice.Models.Broker.Common;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerSupport.Company
{
  public class DepartmentsInspector : IDepartmentsInspector
  {
    private readonly ILogger<DepartmentsInspector> _logger;
    private readonly IRequestClient<ICheckDepartmentsExistence> _requestClient;

    public DepartmentsInspector(
      ILogger<DepartmentsInspector> logger,
      IRequestClient<ICheckDepartmentsExistence> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<ICheckDepartmentsExistence> ExecuteAsync(
      List<string> errors,
      List<Guid> departmentsIds)
    {
      if (departmentsIds == null || !departmentsIds.Any())
      {
        return null;
      }

      return await _requestClient.ProcessRequest<ICheckDepartmentsExistence, ICheckDepartmentsExistence>(
        ICheckDepartmentsExistence.CreateObj(
          departmentsIds),
        errors,
        _logger);

    }
  }
}
