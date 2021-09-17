using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Right.Interfaces
{
  public interface IUserRoleEditor
  {
    Task<bool> ExecuteAsync(
      List<string> errors,
      Guid roleId,
      Guid userId);
  }
}
