using LT.DigitalOffice.Models.Broker.Responses.Company;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company.Interfaces
{
  public interface IGetDepartmentUsers<T> where T : class
  {
    Task<IGetDepartmentUsersResponse> ExecuteAsync(
      List<string> errors,
      Guid departmentId,
      int? skipCount = null,
      int? takeCount = null,
      DateTime? byEntryDate = null);
  }
}
