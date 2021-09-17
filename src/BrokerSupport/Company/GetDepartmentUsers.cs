using BrokerSupport.Company.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Requests.Company;
using LT.DigitalOffice.Models.Broker.Responses.Company;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company
{
  public class GetDepartmentUsers<T> : IGetDepartmentUsers<T> where T : class
  {
    private readonly ILogger<GetDepartmentUsers<T>> _logger;
    private readonly IRequestClient<IGetDepartmentUsersRequest> _requestClient;

    public GetDepartmentUsers(
      ILogger<GetDepartmentUsers<T>> logger,
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
      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<IGetDepartmentUsersResponse>>(
          IGetDepartmentUsersRequest.CreateObj(departmentId, skipCount, takeCount, byEntryDate));

        if (response.Message.IsSuccess && response.Message.Body != null)
        {
          return response.Message.Body;
        }

        _logger.LogWarning(
          "Errors while getting department users with department id '{DepartmentId}'.\n Reason: {Errors}",
          departmentId,
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, "Сan't get department users with department id '{DepartmentId}'.", departmentId);
      }

      errors.Add("Сan't get department users. Please try again later.");

      return null;
    }
  }
}
