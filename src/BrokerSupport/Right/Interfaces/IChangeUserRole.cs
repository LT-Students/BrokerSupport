using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Right.Interfaces
{
  public interface IChangeUserRole<T> where T : class
  {
    Task<bool> ExecuteAsync(
      List<string> errors,
      Guid roleId,
      Guid userId);
  }
}
