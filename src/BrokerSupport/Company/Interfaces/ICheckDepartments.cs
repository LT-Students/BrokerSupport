using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company.Interfaces
{
  public interface ICheckDepartments<T> where T : class
  {
    Task<List<Guid>> ExecuteAsync(
      List<string> errors,
      List<Guid> departmentsIds);
  }
}
