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
  public class GetCompanyEmployees<T> : IGetCompanyEmployees<T> where T : class
  {
    private readonly ILogger<GetCompanyEmployees<T>> _logger;
    private readonly IRequestClient<IGetCompanyEmployeesRequest> _requestClient;

    public GetCompanyEmployees(
      ILogger<GetCompanyEmployees<T>> logger,
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

      try
      {
        var response =
          await _requestClient.GetResponse<IOperationResult<IGetCompanyEmployeesResponse>>(
          IGetCompanyEmployeesRequest.CreateObj(
            usersIds,
            includeDepartments,
            includePositions,
            includeOffices));

        if (response.Message.IsSuccess)
        {
          return response.Message.Body;
        }

        _logger.LogWarning("Errors while getting users departments/positions/offices with users ids {UsersIds}.\n Reason: {Errors}",
          string.Join(", ", usersIds),
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, "Сan't get users departments/positions/offices with users ids {UsersIds}.", string.Join('\n', usersIds));
      }

      errors.Add("Сan't get users departments/positions/offices. Please try again later.");

      return null;
    }
  }
}
