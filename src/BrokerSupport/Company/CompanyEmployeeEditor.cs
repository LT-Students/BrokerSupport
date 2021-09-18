using BrokerSupport.Extension;
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
  public class CompanyEmployeeEditor : ICompanyEmployeeEditor
  {
    private readonly ILogger<CompanyEmployeeEditor> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRequestClient<IEditCompanyEmployeeRequest> _requestClient;

    public CompanyEmployeeEditor(
      ILogger<CompanyEmployeeEditor> logger,
      IHttpContextAccessor httpContextAccessor,
      IRequestClient<IEditCompanyEmployeeRequest> requestClient)
    {
      _logger = logger;
      _httpContextAccessor = httpContextAccessor;
      _requestClient = requestClient;
    }

    public async Task<(bool department, bool position, bool office)> ExecuteAsync(
      List<string> errors,
      Guid userId,
      bool removeUserFromDepartment = false,
      Guid? departmentId = null,
      Guid? positionId = null,
      Guid? officeId = null)
    {
      if (!removeUserFromDepartment && !departmentId.HasValue && !positionId.HasValue && !officeId.HasValue)
      {
        return default;
      }

      return await _requestClient.ProcessRequest<IEditCompanyEmployeeRequest, (bool department, bool position, bool office)>(
        IEditCompanyEmployeeRequest.CreateObj(
          userId,
          _httpContextAccessor.HttpContext.GetUserId(),
          removeUserFromDepartment,
          departmentId,
          positionId,
          officeId),
        errors,
        _logger);
    }
  }
}
