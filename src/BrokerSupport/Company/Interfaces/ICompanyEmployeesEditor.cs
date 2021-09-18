using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.User
{
  public interface ICompanyEmployeeEditor
  {
    Task<(bool department, bool position, bool office)> ExecuteAsync(
      List<string> errors,
      Guid userId,
      bool removeUserFromDepartment = false,
      Guid? departmentId = null,
      Guid? positionId = null,
      Guid? officeId = null);
  }
}
