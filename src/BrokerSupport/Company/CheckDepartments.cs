using BrokerSupport.Company.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Common;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerSupport.Company
{
  public class CheckDepartments<T> : ICheckDepartments<T> where T : class
  {
    private readonly ILogger<CheckDepartments<T>> _logger;
    private readonly IRequestClient<ICheckDepartmentsExistence> _requestClient;

    public CheckDepartments(
      ILogger<CheckDepartments<T>> logger,
      IRequestClient<ICheckDepartmentsExistence> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<List<Guid>> ExecuteAsync(
      List<string> errors,
      List<Guid> departmentsIds)
    {
      if (departmentsIds == null || !departmentsIds.Any())
      {
        return null;
      }

      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<ICheckDepartmentsExistence>>(
          ICheckDepartmentsExistence.CreateObj(departmentsIds));

        if (response.Message.IsSuccess && response.Message.Body != null)
        {
          return response.Message.Body.DepartmentIds;
        }

        _logger.LogWarning(
          "Errors while checking departments existence with departments ids: {DepartmentsIds}.\n Reason: {Errors}",
          string.Join(", ", departmentsIds),
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {

        _logger.LogError(
          exc,
          "Сan't check departments existence with departments ids: {DepartmentsIds}.",
          departmentsIds);
      }

      errors.Add("Сan't check departments existence. Please try again later.");

      return null;
    }
  }
}
