using LT.DigitalOffice.Models.Broker.Responses.Company;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company.Interfaces
{
  public interface ICompanyEmployeesGetter
  {
    Task<IGetCompanyEmployeesResponse> ExecuteAsync(
      List<string> errors,
      List<Guid> usersIds = null,
      bool includeDepartments = false,
      bool includePositions = false,
      bool includeOffices = false);
  }
}
