using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.User
{
  public interface IEditCompanyEmployee<T> where T : class
  {
    Task<bool> ExecuteAsync(
      List<string> errors,
      Guid userId,
      bool removeUserFromDepartment = false,
      Guid? departmentId = null,
      Guid? positionId = null,
      Guid? officeId = null);
  }
}
