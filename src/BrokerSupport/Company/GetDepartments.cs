using BrokerSupport.Company.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Models.Company;
using LT.DigitalOffice.Models.Broker.Requests.Company;
using LT.DigitalOffice.Models.Broker.Responses.Company;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company
{
  public class GetDepartments<T> : IGetDepartments<T> where T : class
  {
    private readonly ILogger<GetDepartments<T>> _logger;
    private readonly IRequestClient<IGetDepartmentsRequest> _requestClient;

    public GetDepartments(
      ILogger<GetDepartments<T>> logger,
      IRequestClient<IGetDepartmentsRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<List<DepartmentData>> ExecuteAsync(
      List<string> errors,
      List<Guid> departmentsIds = null)
    {
      try
      {
        var response =
          await _requestClient.GetResponse<IOperationResult<IGetDepartmentsResponse>>(
            IGetDepartmentsRequest.CreateObj(departmentsIds));

        if (response.Message.IsSuccess && response.Message.Body != null)
        {
          return response.Message.Body.Departments;
        }

        if (departmentsIds != null)
        {
          _logger.LogWarning(
            "Errors while getting departments with departments ids: {DepartmentsIds}.\n Reason: {Errors}",
            string.Join(", ", departmentsIds),
            string.Join('\n', response.Message.Errors));
        }
        else
        {
          _logger.LogWarning(
            "Errors while getting all departments.\n Reason: {Errors}",
            string.Join('\n', response.Message.Errors));
        }
      }
      catch (Exception exc)
      {
        if (departmentsIds != null)
        {
          _logger.LogError(
            exc,
            "Сan't get departments with departments ids: {DepartmentsIds}.",
            string.Join(", ", departmentsIds));
        }
        else
        {
          _logger.LogError(
            exc,
            "Сan't get all departments.");
        }
      }

      errors.Add("Сan't get departments. Please try again later.");

      return null;
    }
  }
}
