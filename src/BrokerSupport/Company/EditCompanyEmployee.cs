using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Models.Broker.Requests.Company;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.User
{
  public class EditCompanyEmployee<T> : IEditCompanyEmployee<T> where T : class
  {
    private readonly ILogger<EditCompanyEmployee<T>> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRequestClient<IEditCompanyEmployeeRequest> _requestClient;

    public EditCompanyEmployee(
      ILogger<EditCompanyEmployee<T>> logger,
      IHttpContextAccessor httpContextAccessor,
      IRequestClient<IEditCompanyEmployeeRequest> requestClient)
    {
      _logger = logger;
      _httpContextAccessor = httpContextAccessor;
      _requestClient = requestClient;
    }

    public async Task<bool> ExecuteAsync(
      List<string> errors,
      Guid userId,
      bool removeUserFromDepartment = false,
      Guid? departmentId = null,
      Guid? positionId = null,
      Guid? officeId = null)
    {
      string positionErrorMessage = "Сan't assign position to user. Please try again later.";
      string departmentErrorMessage = "Сan't assign department to user. Please try again later.";
      string officeErrorMessage = "Сan't assign office to user. Please try again later.";

      try
      {
        var response =
          await _requestClient.GetResponse<IOperationResult<(bool department, bool position, bool office)>>(
            IEditCompanyEmployeeRequest.CreateObj(
              userId,
              _httpContextAccessor.HttpContext.GetUserId(),
              removeUserFromDepartment,
              departmentId,
              positionId,
              officeId));

        if (response.Message.IsSuccess &&
          response.Message.Body.department &&
          response.Message.Body.position &&
          response.Message.Body.office)
        {
          return true;
        }

        _logger.LogWarning(
          "Errors while edit company employee info for user id '{UserId}'.\n Reason: {Errors}",
          userId,
          string.Join('\n', response.Message.Errors));

        if (departmentId.HasValue && !response.Message.Body.department)
        {
          errors.Add(departmentErrorMessage);
        }

        if (positionId.HasValue && !response.Message.Body.position)
        {
          errors.Add(positionErrorMessage);
        }

        if (officeId.HasValue && !response.Message.Body.office)
        {
          errors.Add(officeErrorMessage);
        }
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, "Can't edit company employee info for user id '{UserId}'.", userId);

        if (departmentId.HasValue)
        {
          errors.Add(departmentErrorMessage);
        }

        if (positionId.HasValue)
        {
          errors.Add(positionErrorMessage);
        }

        if (officeId.HasValue)
        {
          errors.Add(officeErrorMessage);
        }
      }

      return false;
    }
  }
}
