using LT.DigitalOffice.Models.Broker.Models.Company;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company.Interfaces
{
  public interface IGetDepartments<T> where T : class
  {
    Task<List<DepartmentData>> ExecuteAsync(
      List<string> errors,
      List<Guid> departmentsIds = null);
  }
}
